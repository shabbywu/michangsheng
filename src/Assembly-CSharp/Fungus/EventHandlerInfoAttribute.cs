using System;

namespace Fungus
{
	// Token: 0x02000E6E RID: 3694
	public class EventHandlerInfoAttribute : Attribute
	{
		// Token: 0x0600680F RID: 26639 RVA: 0x0028BB88 File Offset: 0x00289D88
		public EventHandlerInfoAttribute(string category, string eventHandlerName, string helpText)
		{
			this.Category = category;
			this.EventHandlerName = eventHandlerName;
			this.HelpText = helpText;
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x0028BBA5 File Offset: 0x00289DA5
		// (set) Token: 0x06006811 RID: 26641 RVA: 0x0028BBAD File Offset: 0x00289DAD
		public string Category { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06006812 RID: 26642 RVA: 0x0028BBB6 File Offset: 0x00289DB6
		// (set) Token: 0x06006813 RID: 26643 RVA: 0x0028BBBE File Offset: 0x00289DBE
		public string EventHandlerName { get; set; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06006814 RID: 26644 RVA: 0x0028BBC7 File Offset: 0x00289DC7
		// (set) Token: 0x06006815 RID: 26645 RVA: 0x0028BBCF File Offset: 0x00289DCF
		public string HelpText { get; set; }
	}
}
