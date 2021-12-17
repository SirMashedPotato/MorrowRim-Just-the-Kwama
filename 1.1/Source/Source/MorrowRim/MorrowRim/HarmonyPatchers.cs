using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Reflection;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;
using Verse.AI;
using Verse.AI.Group;
using System.Text;

namespace MorrowRim_Kwama
{

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.MorrowRim");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //prefix to spawn kwama nests instead of insectoid hives on map gen in ashlands
    [HarmonyPatch(typeof(GenStep_CaveHives))]
    [HarmonyPatch("Generate")]

    public static class Gen_KwamaNest_NotInsectoidHive
    {
        [HarmonyPrefix]
        public static bool Gen_KwamaNest(Map map, GenStepParams parms)
        {
            //basic version, 100% replacement in ashlands
            /*if (map.Biome.defName == "MorrowRim_Ashlands")
            {
                Kwama.GenStep_KwamaNest kwamaNest = new Kwama.GenStep_KwamaNest();
                kwamaNest.Generate(map, parms);
                return false;
            }
            return true;*/

            //advanced version, chance based on proximity to egg mine
            WorldGrid worldGrid = Find.WorldGrid;
            WorldObjectsHolder worldObjectsHolder = Find.WorldObjects;
            List<Settlement> kwamaSettlements = new List<Settlement>(worldObjectsHolder.SettlementBases.Where(x => x.Faction.def == FactionDefOf.MorrowRim_Kwama));
            float distance = -1f;
            for(int i = 0; i != kwamaSettlements.Count; i++)
            {
                //checks for if neighbour of neighbour
                /*for (int j = 0; j != worldGrid.GetTileNeighborCount(map.Tile); j++)
                {
                    if (worldGrid.IsNeighbor(worldGrid.GetTileNeighbor(map.Tile, j), kwamaSettlements[i].Tile))
                    {
                        Kwama.GenStep_KwamaNest kwamaNest = new Kwama.GenStep_KwamaNest();
                        kwamaNest.Generate(map, parms);
                        return false;
                    }
                }*/
                float distance2 = worldGrid.ApproxDistanceInTiles(map.Tile, kwamaSettlements[i].Tile);
                if (distance == -1 || distance > distance2) distance = distance2;
                }
            //try to spawn in kwama nests
            if (map.Biome.defName == "MorrowRim_Ashlands") distance /= 2;   //doubles probability if the biome is ashlands
            if (map.Biome.defName == "MorrowRim_BlightedAshlands") distance *= 2;   //halves probability if the biome is blighted ashlands
            Log.Message("Checking for spawn chance of kwama nests, distance from nearest nest is: " + distance);
            if (Rand.Range(0, distance + 1) <= 5)
            {
                Log.Message("Attempting to spawn kwama nests, distance from nearest nest is: " + distance);
                Kwama.GenStep_KwamaNest kwamaNest = new Kwama.GenStep_KwamaNest();
                kwamaNest.Generate(map, parms);
                return false;
            }
            return true;
        }
    }

    //patch to spawn kwama settlements on impassable tiles
    //also limits to certain biomes
    [HarmonyPatch(typeof(TileFinder))]
    [HarmonyPatch("RandomSettlementTileFor")]
    public static class KwamaSettlementSpawnerPatch
    {
        [HarmonyPrefix]
        public static bool SettlementPatch(ref int __result, Faction faction)
        {
            if (faction?.def == FactionDefOf.MorrowRim_Kwama)
            {
                __result = DoTheThing(faction);
                return false;
            }
            return true;
        }

        public static int DoTheThing(Faction faction)
        {
            for (int i = 0; i < 500; i++)
            {
                int num;
                if ((from _ in Enumerable.Range(0, 100) select Rand.Range(0, Find.WorldGrid.TilesCount)).TryRandomElementByWeight(delegate (int x)
                {
                    Tile tile = Find.WorldGrid[x];
                    if (!tile.biome.canBuildBase || !tile.biome.implemented || tile.hilliness != Hilliness.Impassable)
                    {
                        return 0f;
                    }
                    if (biomeDefNames.Contains(tile.biome.defName))
                    {
                        if(tile.biome.defName == biomeDefNames[0]) return tile.biome.settlementSelectionWeight * 2;
                        return tile.biome.settlementSelectionWeight;
                    }

                    return 0f;
                }, out num))
                    if (CheckTileIsValid(num, null))
                    {
                        return num;
                    }
            }
            Log.Error("Failed to find faction base tile for " + faction, false);
            return 0;
        }

        public static bool CheckTileIsValid(int tile, StringBuilder reason = null)
        {
            Tile tile2 = Find.WorldGrid[tile];
            if (!tile2.biome.canBuildBase)
            {
                if (reason != null)
                {
                    reason.Append("CannotLandBiome".Translate(tile2.biome.LabelCap));
                }
                return false;
            }
            if (!tile2.biome.implemented)
            {
                if (reason != null)
                {
                    reason.Append("BiomeNotImplemented".Translate() + ": " + tile2.biome.LabelCap);
                }
                return false;
            }
            /*if (tile2.hilliness == Hilliness.Impassable)
            {
                if (reason != null)
                {
                    reason.Append("CannotLandImpassableMountains".Translate());
                }
                return false;
            }*/
            Settlement settlement = Find.WorldObjects.SettlementBaseAt(tile);
            if (settlement != null)
            {
                if (reason != null)
                {
                    if (settlement.Faction == null)
                    {
                        reason.Append("TileOccupied".Translate());
                    }
                    else if (settlement.Faction == Faction.OfPlayer)
                    {
                        reason.Append("YourBaseAlreadyThere".Translate());
                    }
                    else
                    {
                        reason.Append("BaseAlreadyThere".Translate(settlement.Faction.Name));
                    }
                }
                return false;
            }
            if (Find.WorldObjects.AnySettlementBaseAtOrAdjacent(tile))
            {
                if (reason != null)
                {
                    reason.Append("FactionBaseAdjacent".Translate());
                }
                return false;
            }
            if (Find.WorldObjects.AnyMapParentAt(tile) || Current.Game.FindMap(tile) != null || Find.WorldObjects.AnyWorldObjectOfDefAt(WorldObjectDefOf.AbandonedSettlement, tile))
            {
                if (reason != null)
                {
                    reason.Append("TileOccupied".Translate());
                }
                return false;
            }
            return true;
        }

        static readonly string[] biomeDefNames = { "MorrowRim_Ashlands", BiomeDefOf.TemperateForest.defName, BiomeDefOf.TropicalRainforest.defName, BiomeDefOf.AridShrubland.defName };
    }
}