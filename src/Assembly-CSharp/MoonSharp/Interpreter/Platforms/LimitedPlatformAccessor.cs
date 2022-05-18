using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x020010D3 RID: 4307
	public class LimitedPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x060067E5 RID: 26597 RVA: 0x0000B171 File Offset: 0x00009371
		public override string GetEnvironmentVariable(string envvarname)
		{
			return null;
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x0004765B File Offset: 0x0004585B
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module & ~(CoreModules.OS_System | CoreModules.IO);
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x00047664 File Offset: 0x00045864
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x00047664 File Offset: 0x00045864
		public override Stream IO_GetStandardStream(StandardFileType type)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x00047664 File Offset: 0x00045864
		public override string IO_OS_GetTempFilename()
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x00047664 File Offset: 0x00045864
		public override void OS_ExitFast(int exitCode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x00047664 File Offset: 0x00045864
		public override bool OS_FileExists(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x00047664 File Offset: 0x00045864
		public override void OS_FileDelete(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x00047664 File Offset: 0x00045864
		public override void OS_FileMove(string src, string dst)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x00047664 File Offset: 0x00045864
		public override int OS_Execute(string cmdline)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x00047670 File Offset: 0x00045870
		public override string GetPlatformNamePrefix()
		{
			return "limited";
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x000042DD File Offset: 0x000024DD
		public override void DefaultPrint(string content)
		{
		}
	}
}
