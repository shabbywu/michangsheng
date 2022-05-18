using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x020010D4 RID: 4308
	public abstract class PlatformAccessorBase : IPlatformAccessor
	{
		// Token: 0x060067F2 RID: 26610
		public abstract string GetPlatformNamePrefix();

		// Token: 0x060067F3 RID: 26611 RVA: 0x0028AD84 File Offset: 0x00288F84
		public string GetPlatformName()
		{
			string text;
			if (PlatformAutoDetector.IsRunningOnUnity)
			{
				if (PlatformAutoDetector.IsUnityNative)
				{
					text = "unity." + this.GetUnityPlatformName().ToLower() + "." + this.GetUnityRuntimeName();
				}
				else if (PlatformAutoDetector.IsRunningOnMono)
				{
					text = "unity.dll.mono";
				}
				else
				{
					text = "unity.dll.unknown";
				}
			}
			else if (PlatformAutoDetector.IsRunningOnMono)
			{
				text = "mono";
			}
			else
			{
				text = "dotnet";
			}
			if (PlatformAutoDetector.IsPortableFramework)
			{
				text += ".portable";
			}
			if (PlatformAutoDetector.IsRunningOnClr4)
			{
				text += ".clr4";
			}
			else
			{
				text += ".clr2";
			}
			if (PlatformAutoDetector.IsRunningOnAOT)
			{
				text += ".aot";
			}
			return this.GetPlatformNamePrefix() + "." + text;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x0004767F File Offset: 0x0004587F
		private string GetUnityRuntimeName()
		{
			return "mono";
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x00047686 File Offset: 0x00045886
		private string GetUnityPlatformName()
		{
			return "WIN";
		}

		// Token: 0x060067F6 RID: 26614
		public abstract void DefaultPrint(string content);

		// Token: 0x060067F7 RID: 26615 RVA: 0x0000B171 File Offset: 0x00009371
		[Obsolete("Replace with DefaultInput(string)")]
		public virtual string DefaultInput()
		{
			return null;
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x0004768D File Offset: 0x0004588D
		public virtual string DefaultInput(string prompt)
		{
			return this.DefaultInput();
		}

		// Token: 0x060067F9 RID: 26617
		public abstract Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x060067FA RID: 26618
		public abstract Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x060067FB RID: 26619
		public abstract string IO_OS_GetTempFilename();

		// Token: 0x060067FC RID: 26620
		public abstract void OS_ExitFast(int exitCode);

		// Token: 0x060067FD RID: 26621
		public abstract bool OS_FileExists(string file);

		// Token: 0x060067FE RID: 26622
		public abstract void OS_FileDelete(string file);

		// Token: 0x060067FF RID: 26623
		public abstract void OS_FileMove(string src, string dst);

		// Token: 0x06006800 RID: 26624
		public abstract int OS_Execute(string cmdline);

		// Token: 0x06006801 RID: 26625
		public abstract CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x06006802 RID: 26626
		public abstract string GetEnvironmentVariable(string envvarname);

		// Token: 0x06006803 RID: 26627 RVA: 0x00047695 File Offset: 0x00045895
		public virtual bool IsRunningOnAOT()
		{
			return PlatformAutoDetector.IsRunningOnAOT;
		}
	}
}
