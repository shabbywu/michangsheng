using System;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020011AA RID: 4522
	internal class FileUserData : StreamFileUserDataBase
	{
		// Token: 0x06006EAC RID: 28332 RVA: 0x0029F444 File Offset: 0x0029D644
		public FileUserData(Script script, string filename, Encoding encoding, string mode)
		{
			Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(script, filename, encoding, mode);
			StreamReader reader = stream.CanRead ? new StreamReader(stream, encoding) : null;
			StreamWriter writer = stream.CanWrite ? new StreamWriter(stream, encoding) : null;
			base.Initialize(stream, reader, writer);
		}
	}
}
