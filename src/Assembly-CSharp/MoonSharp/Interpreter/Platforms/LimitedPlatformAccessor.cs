using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000CF6 RID: 3318
	public class LimitedPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x06005CCF RID: 23759 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public override string GetEnvironmentVariable(string envvarname)
		{
			return null;
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00261BB0 File Offset: 0x0025FDB0
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module & ~(CoreModules.OS_System | CoreModules.IO);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override Stream IO_GetStandardStream(StandardFileType type)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override string IO_OS_GetTempFilename()
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override void OS_ExitFast(int exitCode)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override bool OS_FileExists(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override void OS_FileDelete(string file)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override void OS_FileMove(string src, string dst)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00261BB9 File Offset: 0x0025FDB9
		public override int OS_Execute(string cmdline)
		{
			throw new NotImplementedException("The current platform accessor does not support 'io' and 'os' operations. Provide your own implementation of platform to work around this limitation, if needed.");
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00261BC5 File Offset: 0x0025FDC5
		public override string GetPlatformNamePrefix()
		{
			return "limited";
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00004095 File Offset: 0x00002295
		public override void DefaultPrint(string content)
		{
		}
	}
}
