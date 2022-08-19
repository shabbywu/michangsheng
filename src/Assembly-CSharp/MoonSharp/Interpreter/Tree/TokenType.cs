using System;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CCB RID: 3275
	internal enum TokenType
	{
		// Token: 0x0400530F RID: 21263
		Eof,
		// Token: 0x04005310 RID: 21264
		HashBang,
		// Token: 0x04005311 RID: 21265
		Name,
		// Token: 0x04005312 RID: 21266
		And,
		// Token: 0x04005313 RID: 21267
		Break,
		// Token: 0x04005314 RID: 21268
		Do,
		// Token: 0x04005315 RID: 21269
		Else,
		// Token: 0x04005316 RID: 21270
		ElseIf,
		// Token: 0x04005317 RID: 21271
		End,
		// Token: 0x04005318 RID: 21272
		False,
		// Token: 0x04005319 RID: 21273
		For,
		// Token: 0x0400531A RID: 21274
		Function,
		// Token: 0x0400531B RID: 21275
		Lambda,
		// Token: 0x0400531C RID: 21276
		Goto,
		// Token: 0x0400531D RID: 21277
		If,
		// Token: 0x0400531E RID: 21278
		In,
		// Token: 0x0400531F RID: 21279
		Local,
		// Token: 0x04005320 RID: 21280
		Nil,
		// Token: 0x04005321 RID: 21281
		Not,
		// Token: 0x04005322 RID: 21282
		Or,
		// Token: 0x04005323 RID: 21283
		Repeat,
		// Token: 0x04005324 RID: 21284
		Return,
		// Token: 0x04005325 RID: 21285
		Then,
		// Token: 0x04005326 RID: 21286
		True,
		// Token: 0x04005327 RID: 21287
		Until,
		// Token: 0x04005328 RID: 21288
		While,
		// Token: 0x04005329 RID: 21289
		Op_Equal,
		// Token: 0x0400532A RID: 21290
		Op_Assignment,
		// Token: 0x0400532B RID: 21291
		Op_LessThan,
		// Token: 0x0400532C RID: 21292
		Op_LessThanEqual,
		// Token: 0x0400532D RID: 21293
		Op_GreaterThanEqual,
		// Token: 0x0400532E RID: 21294
		Op_GreaterThan,
		// Token: 0x0400532F RID: 21295
		Op_NotEqual,
		// Token: 0x04005330 RID: 21296
		Op_Concat,
		// Token: 0x04005331 RID: 21297
		VarArgs,
		// Token: 0x04005332 RID: 21298
		Dot,
		// Token: 0x04005333 RID: 21299
		Colon,
		// Token: 0x04005334 RID: 21300
		DoubleColon,
		// Token: 0x04005335 RID: 21301
		Comma,
		// Token: 0x04005336 RID: 21302
		Brk_Close_Curly,
		// Token: 0x04005337 RID: 21303
		Brk_Open_Curly,
		// Token: 0x04005338 RID: 21304
		Brk_Close_Round,
		// Token: 0x04005339 RID: 21305
		Brk_Open_Round,
		// Token: 0x0400533A RID: 21306
		Brk_Close_Square,
		// Token: 0x0400533B RID: 21307
		Brk_Open_Square,
		// Token: 0x0400533C RID: 21308
		Op_Len,
		// Token: 0x0400533D RID: 21309
		Op_Pwr,
		// Token: 0x0400533E RID: 21310
		Op_Mod,
		// Token: 0x0400533F RID: 21311
		Op_Div,
		// Token: 0x04005340 RID: 21312
		Op_Mul,
		// Token: 0x04005341 RID: 21313
		Op_MinusOrSub,
		// Token: 0x04005342 RID: 21314
		Op_Add,
		// Token: 0x04005343 RID: 21315
		Comment,
		// Token: 0x04005344 RID: 21316
		String,
		// Token: 0x04005345 RID: 21317
		String_Long,
		// Token: 0x04005346 RID: 21318
		Number,
		// Token: 0x04005347 RID: 21319
		Number_HexFloat,
		// Token: 0x04005348 RID: 21320
		Number_Hex,
		// Token: 0x04005349 RID: 21321
		SemiColon,
		// Token: 0x0400534A RID: 21322
		Invalid,
		// Token: 0x0400534B RID: 21323
		Brk_Open_Curly_Shared,
		// Token: 0x0400534C RID: 21324
		Op_Dollar
	}
}
