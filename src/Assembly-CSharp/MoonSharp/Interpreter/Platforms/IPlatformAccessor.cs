using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000CF5 RID: 3317
	public interface IPlatformAccessor
	{
		// Token: 0x06005CC1 RID: 23745
		CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x06005CC2 RID: 23746
		string GetEnvironmentVariable(string envvarname);

		// Token: 0x06005CC3 RID: 23747
		bool IsRunningOnAOT();

		// Token: 0x06005CC4 RID: 23748
		string GetPlatformName();

		// Token: 0x06005CC5 RID: 23749
		void DefaultPrint(string content);

		// Token: 0x06005CC6 RID: 23750
		string DefaultInput(string prompt);

		// Token: 0x06005CC7 RID: 23751
		Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x06005CC8 RID: 23752
		Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x06005CC9 RID: 23753
		string IO_OS_GetTempFilename();

		// Token: 0x06005CCA RID: 23754
		void OS_ExitFast(int exitCode);

		// Token: 0x06005CCB RID: 23755
		bool OS_FileExists(string file);

		// Token: 0x06005CCC RID: 23756
		void OS_FileDelete(string file);

		// Token: 0x06005CCD RID: 23757
		void OS_FileMove(string src, string dst);

		// Token: 0x06005CCE RID: 23758
		int OS_Execute(string cmdline);
	}
}
