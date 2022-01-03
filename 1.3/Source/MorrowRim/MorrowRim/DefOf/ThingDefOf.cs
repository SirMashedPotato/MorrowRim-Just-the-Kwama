using System;
using Verse;
using RimWorld;

namespace MorrowRim_Kwama
{
    [DefOf]
    public static class ThingDefOf
    {
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }

        //kwama
        public static ThingDef MorrowRim_KwamaNest;
        public static ThingDef MorrowRim_KwamaEgg;
        public static ThingDef MorrowRim_KwamaEggSac;
        public static ThingDef KwamaTunnelSpawner;
        public static ThingDef MorrowRim_FungalMash;

        public static ThingDef MorrowRim_KwamaScrib;
        public static ThingDef MorrowRim_KwamaForager;
        public static ThingDef MorrowRim_KwamaWorker;
        public static ThingDef MorrowRim_KwamaWarrior;

    }
}
