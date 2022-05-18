using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001167 RID: 4455
	internal enum OpCode
	{
		// Token: 0x04006164 RID: 24932
		Nop,
		// Token: 0x04006165 RID: 24933
		Debug,
		// Token: 0x04006166 RID: 24934
		Pop,
		// Token: 0x04006167 RID: 24935
		Copy,
		// Token: 0x04006168 RID: 24936
		Swap,
		// Token: 0x04006169 RID: 24937
		Literal,
		// Token: 0x0400616A RID: 24938
		Closure,
		// Token: 0x0400616B RID: 24939
		NewTable,
		// Token: 0x0400616C RID: 24940
		TblInitN,
		// Token: 0x0400616D RID: 24941
		TblInitI,
		// Token: 0x0400616E RID: 24942
		StoreLcl,
		// Token: 0x0400616F RID: 24943
		Local,
		// Token: 0x04006170 RID: 24944
		StoreUpv,
		// Token: 0x04006171 RID: 24945
		Upvalue,
		// Token: 0x04006172 RID: 24946
		IndexSet,
		// Token: 0x04006173 RID: 24947
		Index,
		// Token: 0x04006174 RID: 24948
		IndexSetN,
		// Token: 0x04006175 RID: 24949
		IndexN,
		// Token: 0x04006176 RID: 24950
		IndexSetL,
		// Token: 0x04006177 RID: 24951
		IndexL,
		// Token: 0x04006178 RID: 24952
		Clean,
		// Token: 0x04006179 RID: 24953
		Meta,
		// Token: 0x0400617A RID: 24954
		BeginFn,
		// Token: 0x0400617B RID: 24955
		Args,
		// Token: 0x0400617C RID: 24956
		Call,
		// Token: 0x0400617D RID: 24957
		ThisCall,
		// Token: 0x0400617E RID: 24958
		Ret,
		// Token: 0x0400617F RID: 24959
		Jump,
		// Token: 0x04006180 RID: 24960
		Jf,
		// Token: 0x04006181 RID: 24961
		JNil,
		// Token: 0x04006182 RID: 24962
		JFor,
		// Token: 0x04006183 RID: 24963
		JtOrPop,
		// Token: 0x04006184 RID: 24964
		JfOrPop,
		// Token: 0x04006185 RID: 24965
		Concat,
		// Token: 0x04006186 RID: 24966
		LessEq,
		// Token: 0x04006187 RID: 24967
		Less,
		// Token: 0x04006188 RID: 24968
		Eq,
		// Token: 0x04006189 RID: 24969
		Add,
		// Token: 0x0400618A RID: 24970
		Sub,
		// Token: 0x0400618B RID: 24971
		Mul,
		// Token: 0x0400618C RID: 24972
		Div,
		// Token: 0x0400618D RID: 24973
		Mod,
		// Token: 0x0400618E RID: 24974
		Not,
		// Token: 0x0400618F RID: 24975
		Len,
		// Token: 0x04006190 RID: 24976
		Neg,
		// Token: 0x04006191 RID: 24977
		Power,
		// Token: 0x04006192 RID: 24978
		CNot,
		// Token: 0x04006193 RID: 24979
		MkTuple,
		// Token: 0x04006194 RID: 24980
		Scalar,
		// Token: 0x04006195 RID: 24981
		Incr,
		// Token: 0x04006196 RID: 24982
		ToNum,
		// Token: 0x04006197 RID: 24983
		ToBool,
		// Token: 0x04006198 RID: 24984
		ExpTuple,
		// Token: 0x04006199 RID: 24985
		IterPrep,
		// Token: 0x0400619A RID: 24986
		IterUpd,
		// Token: 0x0400619B RID: 24987
		Invalid
	}
}
