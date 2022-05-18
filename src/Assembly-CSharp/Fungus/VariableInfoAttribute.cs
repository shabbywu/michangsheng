using System;

namespace Fungus
{
	// Token: 0x020012FC RID: 4860
	public class VariableInfoAttribute : Attribute
	{
		// Token: 0x0600767D RID: 30333 RVA: 0x00050A4E File Offset: 0x0004EC4E
		public VariableInfoAttribute(string category, string variableType, int order = 0)
		{
			this.Category = category;
			this.VariableType = variableType;
			this.Order = order;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600767E RID: 30334 RVA: 0x00050A6B File Offset: 0x0004EC6B
		// (set) Token: 0x0600767F RID: 30335 RVA: 0x00050A73 File Offset: 0x0004EC73
		public string Category { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06007680 RID: 30336 RVA: 0x00050A7C File Offset: 0x0004EC7C
		// (set) Token: 0x06007681 RID: 30337 RVA: 0x00050A84 File Offset: 0x0004EC84
		public string VariableType { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x00050A8D File Offset: 0x0004EC8D
		// (set) Token: 0x06007683 RID: 30339 RVA: 0x00050A95 File Offset: 0x0004EC95
		public int Order { get; set; }
	}
}
