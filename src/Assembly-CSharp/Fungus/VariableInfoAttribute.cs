using System;

namespace Fungus
{
	// Token: 0x02000E8B RID: 3723
	public class VariableInfoAttribute : Attribute
	{
		// Token: 0x0600697E RID: 27006 RVA: 0x0029124B File Offset: 0x0028F44B
		public VariableInfoAttribute(string category, string variableType, int order = 0)
		{
			this.Category = category;
			this.VariableType = variableType;
			this.Order = order;
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600697F RID: 27007 RVA: 0x00291268 File Offset: 0x0028F468
		// (set) Token: 0x06006980 RID: 27008 RVA: 0x00291270 File Offset: 0x0028F470
		public string Category { get; set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06006981 RID: 27009 RVA: 0x00291279 File Offset: 0x0028F479
		// (set) Token: 0x06006982 RID: 27010 RVA: 0x00291281 File Offset: 0x0028F481
		public string VariableType { get; set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06006983 RID: 27011 RVA: 0x0029128A File Offset: 0x0028F48A
		// (set) Token: 0x06006984 RID: 27012 RVA: 0x00291292 File Offset: 0x0028F492
		public int Order { get; set; }
	}
}
