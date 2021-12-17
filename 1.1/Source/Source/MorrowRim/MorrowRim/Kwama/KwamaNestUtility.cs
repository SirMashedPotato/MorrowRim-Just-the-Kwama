using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Kwama.Kwama
{
    public static class KwamaNestUtility
    {
		// Token: 0x06004D03 RID: 19715 RVA: 0x0019CC51 File Offset: 0x0019AE51
		public static int TotalSpawnedHivesCount(Map map)
		{
			return map.listerThings.ThingsOfDef(ThingDefOf.MorrowRim_KwamaNest).Count;
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0019CC68 File Offset: 0x0019AE68
		public static bool AnyHivePreventsClaiming(Thing thing)
		{
			if (!thing.Spawned)
			{
				return false;
			}
			int num = GenRadial.NumCellsInRadius(HivePreventsClaimingInRadius);
			for (int i = 0; i < num; i++)
			{
				IntVec3 c = thing.Position + GenRadial.RadialPattern[i];
				if (c.InBounds(thing.Map) && c.GetFirstThing(thing.Map, thing.def) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0019CCCC File Offset: 0x0019AECC
		public static void Notify_HiveDespawned(KwamaNest hive, Map map)
		{
			int num = GenRadial.NumCellsInRadius(2f);
			for (int i = 0; i < num; i++)
			{
				IntVec3 c = hive.Position + GenRadial.RadialPattern[i];
				if (c.InBounds(map))
				{
					List<Thing> thingList = c.GetThingList(map);
					for (int j = 0; j < thingList.Count; j++)
					{
						if (thingList[j].Faction == FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_Kwama) && !KwamaNestUtility.AnyHivePreventsClaiming(thingList[j]) && !(thingList[j] is Pawn))
						{
							thingList[j].SetFaction(null, null);
						}
					}
				}
			}
		}

		// Token: 0x04002B4B RID: 11083
		private const float HivePreventsClaimingInRadius = 2f;

		public static Thing SpawnTunnels(int hiveCount, Map map, bool spawnAnywhereIfNoGoodCell = false, bool ignoreRoofedRequirement = false, string questTag = null)
		{
			IntVec3 loc;
			if (!InfestationCellFinder.TryFindCell(out loc, map))
			{
				if (!spawnAnywhereIfNoGoodCell)
				{
					return null;
				}
				if (!RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(delegate (IntVec3 x)
				{
					if (!x.Standable(map) || x.Fogged(map))
					{
						return false;
					}
					bool result = false;
					int num = GenRadial.NumCellsInRadius(3f);
					for (int j = 0; j < num; j++)
					{
						IntVec3 c = x + GenRadial.RadialPattern[j];
						if (c.InBounds(map))
						{
							RoofDef roof = c.GetRoof(map);
							if (roof != null && roof.isThickRoof)
							{
								result = true;
								break;
							}
						}
					}
					return result;
				}, map, out loc))
				{
					return null;
				}
			}
			Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.KwamaTunnelSpawner, null), loc, map, WipeMode.FullRefund);
			QuestUtility.AddQuestTag(thing, questTag);
			for (int i = 0; i < hiveCount - 1; i++)
			{
				loc = CompSpawnerKwamaNest.FindChildHiveLocation(thing.Position, map, ThingDefOf.MorrowRim_KwamaNest, ThingDefOf.MorrowRim_KwamaNest.GetCompProperties<CompProperties_SpawnerKwamaNest>(), ignoreRoofedRequirement, true);
				if (loc.IsValid)
				{
					thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.KwamaTunnelSpawner, null), loc, map, WipeMode.FullRefund);
					QuestUtility.AddQuestTag(thing, questTag);
				}
			}
			return thing;
		}
	}
}
