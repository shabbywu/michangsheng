using System;

namespace Fungus
{
	// Token: 0x020012CB RID: 4811
	public class EventHandlerInfoAttribute : Attribute
	{
		// Token: 0x060074C1 RID: 29889 RVA: 0x0004FB90 File Offset: 0x0004DD90
		public EventHandlerInfoAttribute(string category, string eventHandlerName, string helpText)
		{
			this.Category = category;
			this.EventHandlerName = eventHandlerName;
			this.HelpText = helpText;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x0004FBAD File Offset: 0x0004DDAD
		// (set) Token: 0x060074C3 RID: 29891 RVA: 0x0004FBB5 File Offset: 0x0004DDB5
		public string Category { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060074C4 RID: 29892 RVA: 0x0004FBBE File Offset: 0x0004DDBE
		// (set) Token: 0x060074C5 RID: 29893 RVA: 0x0004FBC6 File Offset: 0x0004DDC6
		public string EventHandlerName { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060074C6 RID: 29894 RVA: 0x0004FBCF File Offset: 0x0004DDCF
		// (set) Token: 0x060074C7 RID: 29895 RVA: 0x0004FBD7 File Offset: 0x0004DDD7
		public string HelpText { get; set; }
	}
}
