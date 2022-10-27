using System.Collections.Generic;
using System.Linq;
using Outposts;
using MorrowRim_Kwama;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace MorrowRim_KwamaOutpost
{
    public class Outpost_KwamaEgg : Outpost
    {

        public static int maxDistance = 5;  //make setting in main assembly?

        public static float DistanceFromKwamaNest(int tile)
        {
            WorldGrid worldGrid = Find.WorldGrid;
            WorldObjectsHolder worldObjectsHolder = Find.WorldObjects;
            List<Settlement> kwamaSettlements = new List<Settlement>(worldObjectsHolder.SettlementBases.Where(x => x.Faction.def == MorrowRim_Kwama.FactionDefOf.MorrowRim_Kwama));
            float distance = -1f;
            for (int i = 0; i != kwamaSettlements.Count; i++)
            {
                float distance2 = worldGrid.ApproxDistanceInTiles(tile, kwamaSettlements[i].Tile);
                if (distance == -1 || distance > distance2) distance = distance2;
            }
            return distance;
        }

        public static bool CloseEnough(float distance)
        {
            return distance <= maxDistance;
        }

        public static string CanSpawnOnWith(int tile, List<Pawn> pawns) => !CloseEnough(DistanceFromKwamaNest(tile)) ? "MorrowRimOutposts.MustBeMade.KwamaNest".Translate(maxDistance) : null;

        public static string RequirementsString(int tile, List<Pawn> pawns)
        {
            return Utils.Requirement("MorrowRimOutposts.MustBeMade.KwamaNest".Translate(maxDistance), CloseEnough(DistanceFromKwamaNest(tile)));
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref workUntilReady, "MorrowRim_KwamaEggOutpost_WorkUntilReady", 0);
        }

        /* set up stuff */

        public override string ProductionString()
        {
            if (this.workUntilReady < WorkNeeded) 
            {
                return "MorrowRimOutposts.PreparingMine".Translate(((float)this.workUntilReady / (float)this.WorkNeeded).ToStringPercent(), 
                    ((WorkNeeded - workUntilReady) / (base.TotalSkill(SkillDefOf.Mining) + base.TotalSkill(SkillDefOf.Construction))).ToStringTicksToPeriodVerbose(true, true));
            }  
            
            return base.ProductionString();
        }

        protected virtual int WorkNeeded
        {
            get
            {
                return 420000;
            }
        }

        public override void PostMake()
        {
            base.PostMake();
            this.workUntilReady = 0;
        }

        public override void Tick()
        {
            base.Tick();
            bool flag = this.workUntilReady < WorkNeeded && !base.Packing;
            if (flag)
            {
                this.workUntilReady += (base.TotalSkill(SkillDefOf.Mining) + base.TotalSkill(SkillDefOf.Construction));
            }
        }

        private int workUntilReady;
    }
}
