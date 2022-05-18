using System;
using System.IO;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010DA RID: 4314
	public class FileSystemScriptLoader : ScriptLoaderBase
	{
		// Token: 0x0600682D RID: 26669 RVA: 0x000477B7 File Offset: 0x000459B7
		public override bool ScriptFileExists(string name)
		{
			return File.Exists(name);
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0004785E File Offset: 0x00045A5E
		public override object LoadFile(string file, Table globalContext)
		{
			return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}
	}
}
