using RimWorld;

namespace MorrowRim_Kwama
{
    [DefOf]
    public static class FactionDefOf
    {
        static FactionDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FactionDefOf));
        }
        public static FactionDef MorrowRim_Kwama;

    }
}