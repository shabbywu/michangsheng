using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000CF7 RID: 3319
	public abstract class PlatformAccessorBase : IPlatformAccessor
	{
		// Token: 0x06005CDC RID: 23772
		public abstract string GetPlatformNamePrefix();

		// Token: 0x06005CDD RID: 23773 RVA: 0x00261BD4 File Offset: 0x0025FDD4
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

		// Token: 0x06005CDE RID: 23774 RVA: 0x00261C98 File Offset: 0x0025FE98
		private string GetUnityRuntimeName()
		{
			return "mono";
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x00261C9F File Offset: 0x0025FE9F
		private string GetUnityPlatformName()
		{
			return "WIN";
		}

		// Token: 0x06005CE0 RID: 23776
		public abstract void DefaultPrint(string content);

		// Token: 0x06005CE1 RID: 23777 RVA: 0x000306E7 File Offset: 0x0002E8E7
		[Obsolete("Replace with DefaultInput(string)")]
		public virtual string DefaultInput()
		{
			return null;
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x00261CA6 File Offset: 0x0025FEA6
		public virtual string DefaultInput(string prompt)
		{
			return this.DefaultInput();
		}

		// Token: 0x06005CE3 RID: 23779
		public abstract Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode);

		// Token: 0x06005CE4 RID: 23780
		public abstract Stream IO_GetStandardStream(StandardFileType type);

		// Token: 0x06005CE5 RID: 23781
		public abstract string IO_OS_GetTempFilename();

		// Token: 0x06005CE6 RID: 23782
		public abstract void OS_ExitFast(int exitCode);

		// Token: 0x06005CE7 RID: 23783
		public abstract bool OS_FileExists(string file);

		// Token: 0x06005CE8 RID: 23784
		public abstract void OS_FileDelete(string file);

		// Token: 0x06005CE9 RID: 23785
		public abstract void OS_FileMove(string src, string dst);

		// Token: 0x06005CEA RID: 23786
		public abstract int OS_Execute(string cmdline);

		// Token: 0x06005CEB RID: 23787
		public abstract CoreModules FilterSupportedCoreModules(CoreModules module);

		// Token: 0x06005CEC RID: 23788
		public abstract string GetEnvironmentVariable(string envvarname);

		// Token: 0x06005CED RID: 23789 RVA: 0x00261CAE File Offset: 0x0025FEAE
		public virtual bool IsRunningOnAOT()
		{
			return PlatformAutoDetector.IsRunningOnAOT;
		}
	}
}
