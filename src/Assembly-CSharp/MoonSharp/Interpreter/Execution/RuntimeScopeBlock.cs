using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4E RID: 3406
	internal class RuntimeScopeBlock
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06005FE8 RID: 24552 RVA: 0x0026D149 File Offset: 0x0026B349
		// (set) Token: 0x06005FE9 RID: 24553 RVA: 0x0026D151 File Offset: 0x0026B351
		public int From { get; internal set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06005FEA RID: 24554 RVA: 0x0026D15A File Offset: 0x0026B35A
		// (set) Token: 0x06005FEB RID: 24555 RVA: 0x0026D162 File Offset: 0x0026B362
		public int To { get; internal set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06005FEC RID: 24556 RVA: 0x0026D16B File Offset: 0x0026B36B
		// (set) Token: 0x06005FED RID: 24557 RVA: 0x0026D173 File Offset: 0x0026B373
		public int ToInclusive { get; internal set; }

		// Token: 0x06005FEE RID: 24558 RVA: 0x0026D17C File Offset: 0x0026B37C
		public override string ToString()
		{
			return string.Format("ScopeBlock : {0} -> {1} --> {2}", this.From, this.To, this.ToInclusive);
		}
	}
}
