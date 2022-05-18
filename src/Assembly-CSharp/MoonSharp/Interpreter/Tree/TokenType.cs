using System;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020010A1 RID: 4257
	internal enum TokenType
	{
		// Token: 0x04005EF2 RID: 24306
		Eof,
		// Token: 0x04005EF3 RID: 24307
		HashBang,
		// Token: 0x04005EF4 RID: 24308
		Name,
		// Token: 0x04005EF5 RID: 24309
		And,
		// Token: 0x04005EF6 RID: 24310
		Break,
		// Token: 0x04005EF7 RID: 24311
		Do,
		// Token: 0x04005EF8 RID: 24312
		Else,
		// Token: 0x04005EF9 RID: 24313
		ElseIf,
		// Token: 0x04005EFA RID: 24314
		End,
		// Token: 0x04005EFB RID: 24315
		False,
		// Token: 0x04005EFC RID: 24316
		For,
		// Token: 0x04005EFD RID: 24317
		Function,
		// Token: 0x04005EFE RID: 24318
		Lambda,
		// Token: 0x04005EFF RID: 24319
		Goto,
		// Token: 0x04005F00 RID: 24320
		If,
		// Token: 0x04005F01 RID: 24321
		In,
		// Token: 0x04005F02 RID: 24322
		Local,
		// Token: 0x04005F03 RID: 24323
		Nil,
		// Token: 0x04005F04 RID: 24324
		Not,
		// Token: 0x04005F05 RID: 24325
		Or,
		// Token: 0x04005F06 RID: 24326
		Repeat,
		// Token: 0x04005F07 RID: 24327
		Return,
		// Token: 0x04005F08 RID: 24328
		Then,
		// Token: 0x04005F09 RID: 24329
		True,
		// Token: 0x04005F0A RID: 24330
		Until,
		// Token: 0x04005F0B RID: 24331
		While,
		// Token: 0x04005F0C RID: 24332
		Op_Equal,
		// Token: 0x04005F0D RID: 24333
		Op_Assignment,
		// Token: 0x04005F0E RID: 24334
		Op_LessThan,
		// Token: 0x04005F0F RID: 24335
		Op_LessThanEqual,
		// Token: 0x04005F10 RID: 24336
		Op_GreaterThanEqual,
		// Token: 0x04005F11 RID: 24337
		Op_GreaterThan,
		// Token: 0x04005F12 RID: 24338
		Op_NotEqual,
		// Token: 0x04005F13 RID: 24339
		Op_Concat,
		// Token: 0x04005F14 RID: 24340
		VarArgs,
		// Token: 0x04005F15 RID: 24341
		Dot,
		// Token: 0x04005F16 RID: 24342
		Colon,
		// Token: 0x04005F17 RID: 24343
		DoubleColon,
		// Token: 0x04005F18 RID: 24344
		Comma,
		// Token: 0x04005F19 RID: 24345
		Brk_Close_Curly,
		// Token: 0x04005F1A RID: 24346
		Brk_Open_Curly,
		// Token: 0x04005F1B RID: 24347
		Brk_Close_Round,
		// Token: 0x04005F1C RID: 24348
		Brk_Open_Round,
		// Token: 0x04005F1D RID: 24349
		Brk_Close_Square,
		// Token: 0x04005F1E RID: 24350
		Brk_Open_Square,
		// Token: 0x04005F1F RID: 24351
		Op_Len,
		// Token: 0x04005F20 RID: 24352
		Op_Pwr,
		// Token: 0x04005F21 RID: 24353
		Op_Mod,
		// Token: 0x04005F22 RID: 24354
		Op_Div,
		// Token: 0x04005F23 RID: 24355
		Op_Mul,
		// Token: 0x04005F24 RID: 24356
		Op_MinusOrSub,
		// Token: 0x04005F25 RID: 24357
		Op_Add,
		// Token: 0x04005F26 RID: 24358
		Comment,
		// Token: 0x04005F27 RID: 24359
		String,
		// Token: 0x04005F28 RID: 24360
		String_Long,
		// Token: 0x04005F29 RID: 24361
		Number,
		// Token: 0x04005F2A RID: 24362
		Number_HexFloat,
		// Token: 0x04005F2B RID: 24363
		Number_Hex,
		// Token: 0x04005F2C RID: 24364
		SemiColon,
		// Token: 0x04005F2D RID: 24365
		Invalid,
		// Token: 0x04005F2E RID: 24366
		Brk_Open_Curly_Shared,
		// Token: 0x04005F2F RID: 24367
		Op_Dollar
	}
}
