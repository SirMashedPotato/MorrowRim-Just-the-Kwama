using System;
using Verse;
using RimWorld;

namespace MorrowRim_Kwama
{
    [DefOf]
    public static class PawnKindDefOf
    {
        static PawnKindDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }

        //kwama
        public static PawnKindDef MorrowRim_KwamaScrib;
        public static PawnKindDef MorrowRim_KwamaForager;
        public static PawnKindDef MorrowRim_KwamaWorker;
        public static PawnKindDef MorrowRim_KwamaWarrior;

    }
}
