using System;
using System.IO;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000CFC RID: 3324
	public class FileSystemScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06005D13 RID: 23827 RVA: 0x00261F74 File Offset: 0x00260174
		public override bool ScriptFileExists(string name)
		{
			return File.Exists(name);
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x00262083 File Offset: 0x00260283
		public override object LoadFile(string file, Table globalContext)
		{
			return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}
	}
}
