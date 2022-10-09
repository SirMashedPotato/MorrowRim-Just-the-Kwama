using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace MorrowRim_Kwama
{
	class IncidentWorker_TrojanInfestation : IncidentWorker
	{
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			return base.CanFireNowSub(parms) && ModSettings_Utility.MorrowRim_SettingEnableKwamaTrojanInfestation()
				&& FactionUtility.DefaultFactionFrom(RimWorld.FactionDefOf.Insect) != null
				&& KwamaNestUtility.TotalSpawnedHivesCount_NotFogged(map) > 0;
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			Thing targetNest = GetSpawnedNests(map).Where(x => !x.Position.Fogged(x.Map)).RandomElement();
			spawnInsectoids(targetNest, map, parms);
			base.SendStandardLetter(parms, targetNest);
			Find.TickManager.slower.SignalForceNormalSpeedShort();
			return true;
		}

		public const float HivePoints = 220f;

		public static Thing[] GetSpawnedNests(Map map)
		{
			return map.listerThings.ThingsOfDef(ThingDefOf.MorrowRim_KwamaNest).ToArray();
		}

		public void spawnInsectoids(Thing targetNest, Map map, IncidentParms parms)
		{
			IntVec3 loc = targetNest.Position;

			TunnelHiveSpawner tunnelHiveSpawner = (TunnelHiveSpawner)ThingMaker.MakeThing(RimWorld.ThingDefOf.TunnelHiveSpawner, null);
			tunnelHiveSpawner.spawnHive = false;
			tunnelHiveSpawner.insectsPoints = Mathf.Clamp(parms.points * Rand.Range(0.3f, 0.6f), 200f, 1000f);
			tunnelHiveSpawner.spawnedByInfestationThingComp = true;
			GenSpawn.Spawn(tunnelHiveSpawner, loc, map, WipeMode.FullRefund);
		}
	}
}
