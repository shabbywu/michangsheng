using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x020010D2 RID: 4306
	public interface IPlatformAccessor
	{
		// Token: 0x060067D7 RID: 26583
		CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x060067D8 RID: 26584
		string GetEnvironmentVariable(string envvarname);

		// Token: 0x060067D9 RID: 26585
		bool IsRunningOnAOT();

		// Token: 0x060067DA RID: 26586
		string GetPlatformName();

		// Token: 0x060067DB RID: 26587
		void DefaultPrint(string content);

		// Token: 0x060067DC RID: 26588
		string DefaultInput(string prompt);

		// Token: 0x060067DD RID: 26589
		Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x060067DE RID: 26590
		Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x060067DF RID: 26591
		string IO_OS_GetTempFilename();

		// Token: 0x060067E0 RID: 26592
		void OS_ExitFast(int exitCode);

		// Token: 0x060067E1 RID: 26593
		bool OS_FileExists(string file);

		// Token: 0x060067E2 RID: 26594
		void OS_FileDelete(string file);

		// Token: 0x060067E3 RID: 26595
		void OS_FileMove(string src, string dst);

		// Token: 0x060067E4 RID: 26596
		int OS_Execute(string cmdline);
	}
}
