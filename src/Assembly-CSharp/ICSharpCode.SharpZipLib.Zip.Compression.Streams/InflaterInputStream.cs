using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams;

public class InflaterInputStream : Stream
{
	protected Inflater inf;

	protected InflaterInputBuffer inputBuffer;

	private Stream baseInputStream;

	protected long csize;

	private bool isClosed;

	public bool IsStreamOwner { get; set; } = true;


	public virtual int Available
	{
		get
		{
			if (!inf.IsFinished)
			{
				return 1;
			}
			return 0;
		}
	}

	public override bool CanRead => baseInputStream.CanRead;

	public override bool CanSeek => false;

	public override bool CanWrite => false;

	public override long Length
	{
		get
		{
			throw new NotSupportedException("InflaterInputStream Length is not supported");
		}
	}

	public override long Position
	{
		get
		{
			return baseInputStream.Position;
		}
		set
		{
			throw new NotSupportedException("InflaterInputStream Position not supported");
		}
	}

	public InflaterInputStream(Stream baseInputStream)
		: this(baseInputStream, new Inflater(), 4096)
	{
	}

	public InflaterInputStream(Stream baseInputStream, Inflater inf)
		: this(baseInputStream, inf, 4096)
	{
	}

	public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
	{
		if (baseInputStream == null)
		{
			throw new ArgumentNullException("baseInputStream");
		}
		if (inflater == null)
		{
			throw new ArgumentNullException("inflater");
		}
		if (bufferSize <= 0)
		{
			throw new ArgumentOutOfRangeException("bufferSize");
		}
		this.baseInputStream = baseInputStream;
		inf = inflater;
		inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
	}

	public long Skip(long count)
	{
		if (count <= 0)
		{
			throw new ArgumentOutOfRangeException("count");
		}
		if (baseInputStream.CanSeek)
		{
			baseInputStream.Seek(count, SeekOrigin.Current);
			return count;
		}
		int num = 2048;
		if (count < num)
		{
			num = (int)count;
		}
		byte[] buffer = new byte[num];
		int num2 = 1;
		long num3 = count;
		while (num3 > 0 && num2 > 0)
		{
			if (num3 < num)
			{
				num = (int)num3;
			}
			num2 = baseInputStream.Read(buffer, 0, num);
			num3 -= num2;
		}
		return count - num3;
	}

	protected void StopDecrypting()
	{
		inputBuffer.CryptoTransform = null;
	}

	protected void Fill()
	{
		if (inputBuffer.Available <= 0)
		{
			inputBuffer.Fill();
			if (inputBuffer.Available <= 0)
			{
				throw new SharpZipBaseException("Unexpected EOF");
			}
		}
		inputBuffer.SetInflaterInput(inf);
	}

	public override void Flush()
	{
		baseInputStream.Flush();
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		throw new NotSupportedException("Seek not supported");
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException("InflaterInputStream SetLength not supported");
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException("InflaterInputStream Write not supported");
	}

	public override void WriteByte(byte value)
	{
		throw new NotSupportedException("InflaterInputStream WriteByte not supported");
	}

	protected override void Dispose(bool disposing)
	{
		if (!isClosed)
		{
			isClosed = true;
			if (IsStreamOwner)
			{
				baseInputStream.Dispose();
			}
		}
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (inf.IsNeedingDictionary)
		{
			throw new SharpZipBaseException("Need a dictionary");
		}
		int num = count;
		while (true)
		{
			int num2 = inf.Inflate(buffer, offset, num);
			offset += num2;
			num -= num2;
			if (num == 0 || inf.IsFinished)
			{
				break;
			}
			if (inf.IsNeedingInput)
			{
				Fill();
			}
			else if (num2 == 0)
			{
				throw new ZipException("Invalid input data");
			}
		}
		return count - num;
	}
}
