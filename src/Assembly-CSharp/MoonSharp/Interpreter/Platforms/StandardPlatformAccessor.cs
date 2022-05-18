using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x020010D8 RID: 4312
	public class StandardPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x0600681A RID: 26650 RVA: 0x0028AF58 File Offset: 0x00289158
		public static FileAccess ParseFileAccess(string mode)
		{
			mode = mode.Replace("b", "");
			if (mode == "r")
			{
				return FileAccess.Read;
			}
			if (mode == "r+")
			{
				return FileAccess.ReadWrite;
			}
			if (mode == "w")
			{
				return FileAccess.Write;
			}
			mode == "w+";
			return FileAccess.ReadWrite;
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x0028AFB4 File Offset: 0x002891B4
		public static FileMode ParseFileMode(string mode)
		{
			mode = mode.Replace("b", "");
			if (mode == "r")
			{
				return FileMode.Open;
			}
			if (mode == "r+")
			{
				return FileMode.OpenOrCreate;
			}
			if (mode == "w")
			{
				return FileMode.Create;
			}
			if (mode == "w+")
			{
				return FileMode.Truncate;
			}
			return FileMode.Append;
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x0004774F File Offset: 0x0004594F
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			return new FileStream(filename, StandardPlatformAccessor.ParseFileMode(mode), StandardPlatformAccessor.ParseFileAccess(mode), FileShare.Read | FileShare.Write | FileShare.Delete);
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x00047766 File Offset: 0x00045966
		public override string GetEnvironmentVariable(string envvarname)
		{
			return Environment.GetEnvironmentVariable(envvarname);
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0004776E File Offset: 0x0004596E
		public override Stream IO_GetStandardStream(StandardFileType type)
		{
			switch (type)
			{
			case StandardFileType.StdIn:
				return Console.OpenStandardInput();
			case StandardFileType.StdOut:
				return Console.OpenStandardOutput();
			case StandardFileType.StdErr:
				return Console.OpenStandardError();
			default:
				throw new ArgumentException("type");
			}
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x000477A0 File Offset: 0x000459A0
		public override void DefaultPrint(string content)
		{
			Console.WriteLine(content);
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x000477A8 File Offset: 0x000459A8
		public override string IO_OS_GetTempFilename()
		{
			return Path.GetTempFileName();
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x000477AF File Offset: 0x000459AF
		public override void OS_ExitFast(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x000477B7 File Offset: 0x000459B7
		public override bool OS_FileExists(string file)
		{
			return File.Exists(file);
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x000477BF File Offset: 0x000459BF
		public override void OS_FileDelete(string file)
		{
			File.Delete(file);
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x000477C7 File Offset: 0x000459C7
		public override void OS_FileMove(string src, string dst)
		{
			File.Move(src, dst);
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x000477D0 File Offset: 0x000459D0
		public override int OS_Execute(string cmdline)
		{
			Process process = Process.Start(new ProcessStartInfo("cmd.exe", string.Format("/C {0}", cmdline))
			{
				ErrorDialog = false
			});
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module;
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x000477FE File Offset: 0x000459FE
		public override string GetPlatformNamePrefix()
		{
			return "std";
		}
	}
}
