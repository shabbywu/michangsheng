using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.BZip2;

public static class BZip2
{
	public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
	{
		if (inStream == null)
		{
			throw new ArgumentNullException("inStream");
		}
		if (outStream == null)
		{
			throw new ArgumentNullException("outStream");
		}
		try
		{
			using BZip2InputStream bZip2InputStream = new BZip2InputStream(inStream);
			bZip2InputStream.IsStreamOwner = isStreamOwner;
			StreamUtils.Copy(bZip2InputStream, outStream, new byte[4096]);
		}
		finally
		{
			if (isStreamOwner)
			{
				outStream.Dispose();
			}
		}
	}

	public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
	{
		if (inStream == null)
		{
			throw new ArgumentNullException("inStream");
		}
		if (outStream == null)
		{
			throw new ArgumentNullException("outStream");
		}
		try
		{
			using BZip2OutputStream bZip2OutputStream = new BZip2OutputStream(outStream, level);
			bZip2OutputStream.IsStreamOwner = isStreamOwner;
			StreamUtils.Copy(inStream, bZip2OutputStream, new byte[4096]);
		}
		finally
		{
			if (isStreamOwner)
			{
				inStream.Dispose();
			}
		}
	}
}
