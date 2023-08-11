using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar;

[Serializable]
public class InvalidHeaderException : TarException
{
	public InvalidHeaderException()
	{
	}

	public InvalidHeaderException(string message)
		: base(message)
	{
	}

	public InvalidHeaderException(string message, Exception exception)
		: base(message, exception)
	{
	}

	protected InvalidHeaderException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
