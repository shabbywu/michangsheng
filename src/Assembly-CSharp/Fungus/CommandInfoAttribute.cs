using System;

namespace Fungus
{
	// Token: 0x020012C4 RID: 4804
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class CommandInfoAttribute : Attribute
	{
		// Token: 0x06007466 RID: 29798 RVA: 0x0004F787 File Offset: 0x0004D987
		public CommandInfoAttribute(string category, string commandName, string helpText, int priority = 0)
		{
			this.Category = category;
			this.CommandName = commandName;
			this.HelpText = helpText;
			this.Priority = priority;
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06007467 RID: 29799 RVA: 0x0004F7AC File Offset: 0x0004D9AC
		// (set) Token: 0x06007468 RID: 29800 RVA: 0x0004F7B4 File Offset: 0x0004D9B4
		public string Category { get; set; }

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06007469 RID: 29801 RVA: 0x0004F7BD File Offset: 0x0004D9BD
		// (set) Token: 0x0600746A RID: 29802 RVA: 0x0004F7C5 File Offset: 0x0004D9C5
		public string CommandName { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600746B RID: 29803 RVA: 0x0004F7CE File Offset: 0x0004D9CE
		// (set) Token: 0x0600746C RID: 29804 RVA: 0x0004F7D6 File Offset: 0x0004D9D6
		public string HelpText { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600746D RID: 29805 RVA: 0x0004F7DF File Offset: 0x0004D9DF
		// (set) Token: 0x0600746E RID: 29806 RVA: 0x0004F7E7 File Offset: 0x0004D9E7
		public int Priority { get; set; }
	}
}
