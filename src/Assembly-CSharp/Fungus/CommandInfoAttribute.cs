using System;

namespace Fungus
{
	// Token: 0x02000E67 RID: 3687
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class CommandInfoAttribute : Attribute
	{
		// Token: 0x060067B4 RID: 26548 RVA: 0x0028B1D5 File Offset: 0x002893D5
		public CommandInfoAttribute(string category, string commandName, string helpText, int priority = 0)
		{
			this.Category = category;
			this.CommandName = commandName;
			this.HelpText = helpText;
			this.Priority = priority;
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x0028B1FA File Offset: 0x002893FA
		// (set) Token: 0x060067B6 RID: 26550 RVA: 0x0028B202 File Offset: 0x00289402
		public string Category { get; set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060067B7 RID: 26551 RVA: 0x0028B20B File Offset: 0x0028940B
		// (set) Token: 0x060067B8 RID: 26552 RVA: 0x0028B213 File Offset: 0x00289413
		public string CommandName { get; set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x0028B21C File Offset: 0x0028941C
		// (set) Token: 0x060067BA RID: 26554 RVA: 0x0028B224 File Offset: 0x00289424
		public string HelpText { get; set; }

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060067BB RID: 26555 RVA: 0x0028B22D File Offset: 0x0028942D
		// (set) Token: 0x060067BC RID: 26556 RVA: 0x0028B235 File Offset: 0x00289435
		public int Priority { get; set; }
	}
}
