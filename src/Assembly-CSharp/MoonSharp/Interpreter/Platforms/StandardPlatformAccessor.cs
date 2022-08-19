using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.Platforms
{
	// Token: 0x02000CFA RID: 3322
	public class StandardPlatformAccessor : PlatformAccessorBase
	{
		// Token: 0x06005D00 RID: 23808 RVA: 0x00261E54 File Offset: 0x00260054
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

		// Token: 0x06005D01 RID: 23809 RVA: 0x00261EB0 File Offset: 0x002600B0
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

		// Token: 0x06005D02 RID: 23810 RVA: 0x00261F0C File Offset: 0x0026010C
		public override Stream IO_OpenFile(Script script, string filename, Encoding encoding, string mode)
		{
			return new FileStream(filename, StandardPlatformAccessor.ParseFileMode(mode), StandardPlatformAccessor.ParseFileAccess(mode), FileShare.Read | FileShare.Write | FileShare.Delete);
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x00261F23 File Offset: 0x00260123
		public override string GetEnvironmentVariable(string envvarname)
		{
			return Environment.GetEnvironmentVariable(envvarname);
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x00261F2B File Offset: 0x0026012B
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

		// Token: 0x06005D05 RID: 23813 RVA: 0x00261F5D File Offset: 0x0026015D
		public override void DefaultPrint(string content)
		{
			Console.WriteLine(content);
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x00261F65 File Offset: 0x00260165
		public override string IO_OS_GetTempFilename()
		{
			return Path.GetTempFileName();
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x00261F6C File Offset: 0x0026016C
		public override void OS_ExitFast(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x00261F74 File Offset: 0x00260174
		public override bool OS_FileExists(string file)
		{
			return File.Exists(file);
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x00261F7C File Offset: 0x0026017C
		public override void OS_FileDelete(string file)
		{
			File.Delete(file);
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x00261F84 File Offset: 0x00260184
		public override void OS_FileMove(string src, string dst)
		{
			File.Move(src, dst);
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x00261F8D File Offset: 0x0026018D
		public override int OS_Execute(string cmdline)
		{
			Process process = Process.Start(new ProcessStartInfo("cmd.exe", string.Format("/C {0}", cmdline))
			{
				ErrorDialog = false
			});
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x001086F1 File Offset: 0x001068F1
		public override CoreModules FilterSupportedCoreModules(CoreModules module)
		{
			return module;
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x00261FBB File Offset: 0x002601BB
		public override string GetPlatformNamePrefix()
		{
			return "std";
		}
	}
}
