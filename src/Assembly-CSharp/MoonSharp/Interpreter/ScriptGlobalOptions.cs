using System;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Platforms;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CC4 RID: 3268
	public class ScriptGlobalOptions
	{
		// Token: 0x06005BA4 RID: 23460 RVA: 0x0025B164 File Offset: 0x00259364
		internal ScriptGlobalOptions()
		{
			this.Platform = PlatformAutoDetector.GetDefaultPlatform();
			this.CustomConverters = new CustomConvertersCollection();
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06005BA5 RID: 23461 RVA: 0x0025B182 File Offset: 0x00259382
		// (set) Token: 0x06005BA6 RID: 23462 RVA: 0x0025B18A File Offset: 0x0025938A
		public CustomConvertersCollection CustomConverters { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06005BA7 RID: 23463 RVA: 0x0025B193 File Offset: 0x00259393
		// (set) Token: 0x06005BA8 RID: 23464 RVA: 0x0025B19B File Offset: 0x0025939B
		public IPlatformAccessor Platform { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x0025B1A4 File Offset: 0x002593A4
		// (set) Token: 0x06005BAA RID: 23466 RVA: 0x0025B1AC File Offset: 0x002593AC
		public bool RethrowExceptionNested { get; set; }
	}
}
