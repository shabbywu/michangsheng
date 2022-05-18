using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200109A RID: 4250
	public class ScriptGlobalOptions
	{
		// Token: 0x060066AD RID: 26285 RVA: 0x00046C5A File Offset: 0x00044E5A
		internal ScriptGlobalOptions()
		{
			this.Platform = PlatformAutoDetector.GetDefaultPlatform();
			this.CustomConverters = new CustomConvertersCollection();
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x00046C78 File Offset: 0x00044E78
		// (set) Token: 0x060066AF RID: 26287 RVA: 0x00046C80 File Offset: 0x00044E80
		public CustomConvertersCollection CustomConverters { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x00046C89 File Offset: 0x00044E89
		// (set) Token: 0x060066B1 RID: 26289 RVA: 0x00046C91 File Offset: 0x00044E91
		public IPlatformAccessor Platform { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x00046C9A File Offset: 0x00044E9A
		// (set) Token: 0x060066B3 RID: 26291 RVA: 0x00046CA2 File Offset: 0x00044EA2
		public bool RethrowExceptionNested { get; set; }
	}
}
