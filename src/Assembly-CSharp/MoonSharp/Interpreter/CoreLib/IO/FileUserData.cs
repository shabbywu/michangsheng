using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x02000D88 RID: 3464
	internal class FileUserData : StreamFileUserDataBase
	{
		// Token: 0x0600627E RID: 25214 RVA: 0x00279478 File Offset: 0x00277678
		public FileUserData(Script script, string filename, Encoding encoding, string mode)
		{
			Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(script, filename, encoding, mode);
			StreamReader reader = stream.CanRead ? new StreamReader(stream, encoding) : null;
			StreamWriter writer = stream.CanWrite ? new StreamWriter(stream, encoding) : null;
			base.Initialize(stream, reader, writer);
		}
	}
}
