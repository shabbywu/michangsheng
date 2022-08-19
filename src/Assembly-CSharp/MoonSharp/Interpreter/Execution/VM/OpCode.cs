using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D56 RID: 3414
	internal enum OpCode
	{
		// Token: 0x040054E6 RID: 21734
		Nop,
		// Token: 0x040054E7 RID: 21735
		Debug,
		// Token: 0x040054E8 RID: 21736
		Pop,
		// Token: 0x040054E9 RID: 21737
		Copy,
		// Token: 0x040054EA RID: 21738
		Swap,
		// Token: 0x040054EB RID: 21739
		Literal,
		// Token: 0x040054EC RID: 21740
		Closure,
		// Token: 0x040054ED RID: 21741
		NewTable,
		// Token: 0x040054EE RID: 21742
		TblInitN,
		// Token: 0x040054EF RID: 21743
		TblInitI,
		// Token: 0x040054F0 RID: 21744
		StoreLcl,
		// Token: 0x040054F1 RID: 21745
		Local,
		// Token: 0x040054F2 RID: 21746
		StoreUpv,
		// Token: 0x040054F3 RID: 21747
		Upvalue,
		// Token: 0x040054F4 RID: 21748
		IndexSet,
		// Token: 0x040054F5 RID: 21749
		Index,
		// Token: 0x040054F6 RID: 21750
		IndexSetN,
		// Token: 0x040054F7 RID: 21751
		IndexN,
		// Token: 0x040054F8 RID: 21752
		IndexSetL,
		// Token: 0x040054F9 RID: 21753
		IndexL,
		// Token: 0x040054FA RID: 21754
		Clean,
		// Token: 0x040054FB RID: 21755
		Meta,
		// Token: 0x040054FC RID: 21756
		BeginFn,
		// Token: 0x040054FD RID: 21757
		Args,
		// Token: 0x040054FE RID: 21758
		Call,
		// Token: 0x040054FF RID: 21759
		ThisCall,
		// Token: 0x04005500 RID: 21760
		Ret,
		// Token: 0x04005501 RID: 21761
		Jump,
		// Token: 0x04005502 RID: 21762
		Jf,
		// Token: 0x04005503 RID: 21763
		JNil,
		// Token: 0x04005504 RID: 21764
		JFor,
		// Token: 0x04005505 RID: 21765
		JtOrPop,
		// Token: 0x04005506 RID: 21766
		JfOrPop,
		// Token: 0x04005507 RID: 21767
		Concat,
		// Token: 0x04005508 RID: 21768
		LessEq,
		// Token: 0x04005509 RID: 21769
		Less,
		// Token: 0x0400550A RID: 21770
		Eq,
		// Token: 0x0400550B RID: 21771
		Add,
		// Token: 0x0400550C RID: 21772
		Sub,
		// Token: 0x0400550D RID: 21773
		Mul,
		// Token: 0x0400550E RID: 21774
		Div,
		// Token: 0x0400550F RID: 21775
		Mod,
		// Token: 0x04005510 RID: 21776
		Not,
		// Token: 0x04005511 RID: 21777
		Len,
		// Token: 0x04005512 RID: 21778
		Neg,
		// Token: 0x04005513 RID: 21779
		Power,
		// Token: 0x04005514 RID: 21780
		CNot,
		// Token: 0x04005515 RID: 21781
		MkTuple,
		// Token: 0x04005516 RID: 21782
		Scalar,
		// Token: 0x04005517 RID: 21783
		Incr,
		// Token: 0x04005518 RID: 21784
		ToNum,
		// Token: 0x04005519 RID: 21785
		ToBool,
		// Token: 0x0400551A RID: 21786
		ExpTuple,
		// Token: 0x0400551B RID: 21787
		IterPrep,
		// Token: 0x0400551C RID: 21788
		IterUpd,
		// Token: 0x0400551D RID: 21789
		Invalid
	}
}
