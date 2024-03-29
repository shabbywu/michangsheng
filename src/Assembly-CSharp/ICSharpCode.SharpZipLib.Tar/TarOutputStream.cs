using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar;

public class TarOutputStream : Stream
{
	private long currBytes;

	private int assemblyBufferLength;

	private bool isClosed;

	protected long currSize;

	protected byte[] blockBuffer;

	protected byte[] assemblyBuffer;

	protected TarBuffer buffer;

	protected Stream outputStream;

	protected Encoding nameEncoding;

	public bool IsStreamOwner
	{
		get
		{
			return buffer.IsStreamOwner;
		}
		set
		{
			buffer.IsStreamOwner = value;
		}
	}

	public override bool CanRead => outputStream.CanRead;

	public override bool CanSeek => outputStream.CanSeek;

	public override bool CanWrite => outputStream.CanWrite;

	public override long Length => outputStream.Length;

	public override long Position
	{
		get
		{
			return outputStream.Position;
		}
		set
		{
			outputStream.Position = value;
		}
	}

	public int RecordSize => buffer.RecordSize;

	private bool IsEntryOpen => currBytes < currSize;

	[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
	public TarOutputStream(Stream outputStream)
		: this(outputStream, 20)
	{
	}

	public TarOutputStream(Stream outputStream, Encoding nameEncoding)
		: this(outputStream, 20, nameEncoding)
	{
	}

	[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
	public TarOutputStream(Stream outputStream, int blockFactor)
	{
		if (outputStream == null)
		{
			throw new ArgumentNullException("outputStream");
		}
		this.outputStream = outputStream;
		buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
		assemblyBuffer = new byte[512];
		blockBuffer = new byte[512];
	}

	public TarOutputStream(Stream outputStream, int blockFactor, Encoding nameEncoding)
	{
		if (outputStream == null)
		{
			throw new ArgumentNullException("outputStream");
		}
		this.outputStream = outputStream;
		buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
		assemblyBuffer = new byte[512];
		blockBuffer = new byte[512];
		this.nameEncoding = nameEncoding;
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		return outputStream.Seek(offset, origin);
	}

	public override void SetLength(long value)
	{
		outputStream.SetLength(value);
	}

	public override int ReadByte()
	{
		return outputStream.ReadByte();
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		return outputStream.Read(buffer, offset, count);
	}

	public override void Flush()
	{
		outputStream.Flush();
	}

	public void Finish()
	{
		if (IsEntryOpen)
		{
			CloseEntry();
		}
		WriteEofBlock();
	}

	protected override void Dispose(bool disposing)
	{
		if (!isClosed)
		{
			isClosed = true;
			Finish();
			buffer.Close();
		}
	}

	[Obsolete("Use RecordSize property instead")]
	public int GetRecordSize()
	{
		return buffer.RecordSize;
	}

	public void PutNextEntry(TarEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		int num = ((nameEncoding != null) ? nameEncoding.GetByteCount(entry.TarHeader.Name) : entry.TarHeader.Name.Length);
		if (num > 100)
		{
			TarHeader obj = new TarHeader
			{
				TypeFlag = 76
			};
			obj.Name += "././@LongLink";
			obj.Mode = 420;
			obj.UserId = entry.UserId;
			obj.GroupId = entry.GroupId;
			obj.GroupName = entry.GroupName;
			obj.UserName = entry.UserName;
			obj.LinkName = "";
			obj.Size = num + 1;
			obj.WriteHeader(blockBuffer, nameEncoding);
			buffer.WriteBlock(blockBuffer);
			int num2 = 0;
			while (num2 < num + 1)
			{
				Array.Clear(blockBuffer, 0, blockBuffer.Length);
				TarHeader.GetAsciiBytes(entry.TarHeader.Name, num2, blockBuffer, 0, 512, nameEncoding);
				num2 += 512;
				buffer.WriteBlock(blockBuffer);
			}
		}
		entry.WriteEntryHeader(blockBuffer, nameEncoding);
		buffer.WriteBlock(blockBuffer);
		currBytes = 0L;
		currSize = (entry.IsDirectory ? 0 : entry.Size);
	}

	public void CloseEntry()
	{
		if (assemblyBufferLength > 0)
		{
			Array.Clear(assemblyBuffer, assemblyBufferLength, assemblyBuffer.Length - assemblyBufferLength);
			buffer.WriteBlock(assemblyBuffer);
			currBytes += assemblyBufferLength;
			assemblyBufferLength = 0;
		}
		if (currBytes < currSize)
		{
			throw new TarException($"Entry closed at '{currBytes}' before the '{currSize}' bytes specified in the header were written");
		}
	}

	public override void WriteByte(byte value)
	{
		Write(new byte[1] { value }, 0, 1);
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		if (offset < 0)
		{
			throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
		}
		if (buffer.Length - offset < count)
		{
			throw new ArgumentException("offset and count combination is invalid");
		}
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException("count", "Cannot be negative");
		}
		if (currBytes + count > currSize)
		{
			string message = $"request to write '{count}' bytes exceeds size in header of '{currSize}' bytes";
			throw new ArgumentOutOfRangeException("count", message);
		}
		if (assemblyBufferLength > 0)
		{
			if (assemblyBufferLength + count >= blockBuffer.Length)
			{
				int num = blockBuffer.Length - assemblyBufferLength;
				Array.Copy(assemblyBuffer, 0, blockBuffer, 0, assemblyBufferLength);
				Array.Copy(buffer, offset, blockBuffer, assemblyBufferLength, num);
				this.buffer.WriteBlock(blockBuffer);
				currBytes += blockBuffer.Length;
				offset += num;
				count -= num;
				assemblyBufferLength = 0;
			}
			else
			{
				Array.Copy(buffer, offset, assemblyBuffer, assemblyBufferLength, count);
				offset += count;
				assemblyBufferLength += count;
				count -= count;
			}
		}
		while (count > 0)
		{
			if (count < blockBuffer.Length)
			{
				Array.Copy(buffer, offset, assemblyBuffer, assemblyBufferLength, count);
				assemblyBufferLength += count;
				break;
			}
			this.buffer.WriteBlock(buffer, offset);
			int num2 = blockBuffer.Length;
			currBytes += num2;
			count -= num2;
			offset += num2;
		}
	}

	private void WriteEofBlock()
	{
		Array.Clear(blockBuffer, 0, blockBuffer.Length);
		buffer.WriteBlock(blockBuffer);
		buffer.WriteBlock(blockBuffer);
	}
}
