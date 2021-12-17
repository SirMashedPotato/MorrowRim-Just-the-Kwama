using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim.Kwama
{
    public class KwamaThinkNode_ConditionalPawnKindDef : ThinkNode_Conditional
    {
		// Token: 0x060032D8 RID: 13016 RVA: 0x00119978 File Offset: 0x00117B78
		protected override bool Satisfied(Pawn pawn)
		{
			return pawn.def == ThingDefOf.MorrowRim_KwamaWorker;
		}

		public string pawnKind;
	}
}
