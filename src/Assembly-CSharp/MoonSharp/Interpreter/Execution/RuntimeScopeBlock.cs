using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115D RID: 4445
	internal class RuntimeScopeBlock
	{
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x00049708 File Offset: 0x00047908
		// (set) Token: 0x06006BD0 RID: 27600 RVA: 0x00049710 File Offset: 0x00047910
		public int From { get; internal set; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x00049719 File Offset: 0x00047919
		// (set) Token: 0x06006BD2 RID: 27602 RVA: 0x00049721 File Offset: 0x00047921
		public int To { get; internal set; }

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x0004972A File Offset: 0x0004792A
		// (set) Token: 0x06006BD4 RID: 27604 RVA: 0x00049732 File Offset: 0x00047932
		public int ToInclusive { get; internal set; }

		// Token: 0x06006BD5 RID: 27605 RVA: 0x0004973B File Offset: 0x0004793B
		public override string ToString()
		{
			return string.Format("ScopeBlock : {0} -> {1} --> {2}", this.From, this.To, this.ToInclusive);
		}
	}
}
