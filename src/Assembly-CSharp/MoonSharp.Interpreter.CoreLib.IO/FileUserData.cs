using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib.IO;

internal class FileUserData : StreamFileUserDataBase
{
	public FileUserData(Script script, string filename, Encoding encoding, string mode)
	{
		Stream stream = Script.GlobalOptions.Platform.IO_OpenFile(script, filename, encoding, mode);
		Initialize(stream, stream.CanRead ? new StreamReader(stream, encoding) : null, stream.CanWrite ? new StreamWriter(stream, encoding) : null);
	}
}
