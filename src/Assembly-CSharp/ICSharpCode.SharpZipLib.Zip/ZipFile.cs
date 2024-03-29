using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip;

public class ZipFile : IEnumerable, IDisposable
{
	public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

	[Flags]
	private enum HeaderTest
	{
		Extract = 1,
		Header = 2
	}

	private enum UpdateCommand
	{
		Copy,
		Modify,
		Add
	}

	private class UpdateComparer : IComparer<ZipUpdate>
	{
		public int Compare(ZipUpdate x, ZipUpdate y)
		{
			int num;
			if (x == null)
			{
				num = ((y != null) ? (-1) : 0);
			}
			else if (y == null)
			{
				num = 1;
			}
			else
			{
				int num2 = ((x.Command != 0 && x.Command != UpdateCommand.Modify) ? 1 : 0);
				int num3 = ((y.Command != 0 && y.Command != UpdateCommand.Modify) ? 1 : 0);
				num = num2 - num3;
				if (num == 0)
				{
					long num4 = x.Entry.Offset - y.Entry.Offset;
					num = ((num4 < 0) ? (-1) : ((num4 != 0L) ? 1 : 0));
				}
			}
			return num;
		}
	}

	private class ZipUpdate
	{
		private ZipEntry entry_;

		private ZipEntry outEntry_;

		private readonly UpdateCommand command_;

		private IStaticDataSource dataSource_;

		private readonly string filename_;

		private long sizePatchOffset_ = -1L;

		private long crcPatchOffset_ = -1L;

		private long _offsetBasedSize = -1L;

		public ZipEntry Entry => entry_;

		public ZipEntry OutEntry
		{
			get
			{
				if (outEntry_ == null)
				{
					outEntry_ = (ZipEntry)entry_.Clone();
				}
				return outEntry_;
			}
		}

		public UpdateCommand Command => command_;

		public string Filename => filename_;

		public long SizePatchOffset
		{
			get
			{
				return sizePatchOffset_;
			}
			set
			{
				sizePatchOffset_ = value;
			}
		}

		public long CrcPatchOffset
		{
			get
			{
				return crcPatchOffset_;
			}
			set
			{
				crcPatchOffset_ = value;
			}
		}

		public long OffsetBasedSize
		{
			get
			{
				return _offsetBasedSize;
			}
			set
			{
				_offsetBasedSize = value;
			}
		}

		public ZipUpdate(string fileName, ZipEntry entry)
		{
			command_ = UpdateCommand.Add;
			entry_ = entry;
			filename_ = fileName;
		}

		[Obsolete]
		public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
		{
			command_ = UpdateCommand.Add;
			entry_ = new ZipEntry(entryName)
			{
				CompressionMethod = compressionMethod
			};
			filename_ = fileName;
		}

		[Obsolete]
		public ZipUpdate(string fileName, string entryName)
			: this(fileName, entryName, CompressionMethod.Deflated)
		{
		}

		[Obsolete]
		public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
		{
			command_ = UpdateCommand.Add;
			entry_ = new ZipEntry(entryName)
			{
				CompressionMethod = compressionMethod
			};
			dataSource_ = dataSource;
		}

		public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
		{
			command_ = UpdateCommand.Add;
			entry_ = entry;
			dataSource_ = dataSource;
		}

		public ZipUpdate(ZipEntry original, ZipEntry updated)
		{
			throw new ZipException("Modify not currently supported");
		}

		public ZipUpdate(UpdateCommand command, ZipEntry entry)
		{
			command_ = command;
			entry_ = (ZipEntry)entry.Clone();
		}

		public ZipUpdate(ZipEntry entry)
			: this(UpdateCommand.Copy, entry)
		{
		}

		public Stream GetSource()
		{
			Stream result = null;
			if (dataSource_ != null)
			{
				result = dataSource_.GetSource();
			}
			return result;
		}
	}

	private class ZipString
	{
		private string comment_;

		private byte[] rawComment_;

		private readonly bool isSourceString_;

		public bool IsSourceString => isSourceString_;

		public int RawLength
		{
			get
			{
				MakeBytesAvailable();
				return rawComment_.Length;
			}
		}

		public byte[] RawComment
		{
			get
			{
				MakeBytesAvailable();
				return (byte[])rawComment_.Clone();
			}
		}

		public ZipString(string comment)
		{
			comment_ = comment;
			isSourceString_ = true;
		}

		public ZipString(byte[] rawString)
		{
			rawComment_ = rawString;
		}

		public void Reset()
		{
			if (isSourceString_)
			{
				rawComment_ = null;
			}
			else
			{
				comment_ = null;
			}
		}

		private void MakeTextAvailable()
		{
			if (comment_ == null)
			{
				comment_ = ZipStrings.ConvertToString(rawComment_);
			}
		}

		private void MakeBytesAvailable()
		{
			if (rawComment_ == null)
			{
				rawComment_ = ZipStrings.ConvertToArray(comment_);
			}
		}

		public static implicit operator string(ZipString zipString)
		{
			zipString.MakeTextAvailable();
			return zipString.comment_;
		}
	}

	private class ZipEntryEnumerator : IEnumerator
	{
		private ZipEntry[] array;

		private int index = -1;

		public object Current => array[index];

		public ZipEntryEnumerator(ZipEntry[] entries)
		{
			array = entries;
		}

		public void Reset()
		{
			index = -1;
		}

		public bool MoveNext()
		{
			return ++index < array.Length;
		}
	}

	private class UncompressedStream : Stream
	{
		private readonly Stream baseStream_;

		public override bool CanRead => false;

		public override bool CanWrite => baseStream_.CanWrite;

		public override bool CanSeek => false;

		public override long Length => 0L;

		public override long Position
		{
			get
			{
				return baseStream_.Position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public UncompressedStream(Stream baseStream)
		{
			baseStream_ = baseStream;
		}

		public override void Flush()
		{
			baseStream_.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			baseStream_.Write(buffer, offset, count);
		}
	}

	private class PartialInputStream : Stream
	{
		private ZipFile zipFile_;

		private Stream baseStream_;

		private readonly long start_;

		private readonly long length_;

		private long readPos_;

		private readonly long end_;

		public override long Position
		{
			get
			{
				return readPos_ - start_;
			}
			set
			{
				long num = start_ + value;
				if (num < start_)
				{
					throw new ArgumentException("Negative position is invalid");
				}
				if (num > end_)
				{
					throw new InvalidOperationException("Cannot seek past end");
				}
				readPos_ = num;
			}
		}

		public override long Length => length_;

		public override bool CanWrite => false;

		public override bool CanSeek => true;

		public override bool CanRead => true;

		public override bool CanTimeout => baseStream_.CanTimeout;

		public PartialInputStream(ZipFile zipFile, long start, long length)
		{
			start_ = start;
			length_ = length;
			zipFile_ = zipFile;
			baseStream_ = zipFile_.baseStream_;
			readPos_ = start;
			end_ = start + length;
		}

		public override int ReadByte()
		{
			if (readPos_ >= end_)
			{
				return -1;
			}
			lock (baseStream_)
			{
				baseStream_.Seek(readPos_++, SeekOrigin.Begin);
				return baseStream_.ReadByte();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			lock (baseStream_)
			{
				if (count > end_ - readPos_)
				{
					count = (int)(end_ - readPos_);
					if (count == 0)
					{
						return 0;
					}
				}
				if (baseStream_.Position != readPos_)
				{
					baseStream_.Seek(readPos_, SeekOrigin.Begin);
				}
				int num = baseStream_.Read(buffer, offset, count);
				if (num > 0)
				{
					readPos_ += num;
				}
				return num;
			}
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = readPos_;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = start_ + offset;
				break;
			case SeekOrigin.Current:
				num = readPos_ + offset;
				break;
			case SeekOrigin.End:
				num = end_ + offset;
				break;
			}
			if (num < start_)
			{
				throw new ArgumentException("Negative position is invalid");
			}
			if (num > end_)
			{
				throw new IOException("Cannot seek past end");
			}
			readPos_ = num;
			return readPos_;
		}

		public override void Flush()
		{
		}
	}

	public KeysRequiredEventHandler KeysRequired;

	private const int DefaultBufferSize = 4096;

	private bool isDisposed_;

	private string name_;

	private string comment_;

	private string rawPassword_;

	private Stream baseStream_;

	private bool isStreamOwner;

	private long offsetOfFirstEntry;

	private ZipEntry[] entries_;

	private byte[] key;

	private bool isNewArchive_;

	private UseZip64 useZip64_ = UseZip64.Dynamic;

	private List<ZipUpdate> updates_;

	private long updateCount_;

	private Dictionary<string, int> updateIndex_;

	private IArchiveStorage archiveStorage_;

	private IDynamicDataSource updateDataSource_;

	private bool contentsEdited_;

	private int bufferSize_ = 4096;

	private byte[] copyBuffer_;

	private ZipString newComment_;

	private bool commentEdited_;

	private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

	private byte[] Key
	{
		get
		{
			return key;
		}
		set
		{
			key = value;
		}
	}

	public string Password
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				key = null;
			}
			else
			{
				key = PkzipClassic.GenerateKeys(ZipStrings.ConvertToArray(value));
			}
			rawPassword_ = value;
		}
	}

	private bool HaveKeys => key != null;

	public bool IsStreamOwner
	{
		get
		{
			return isStreamOwner;
		}
		set
		{
			isStreamOwner = value;
		}
	}

	public bool IsEmbeddedArchive => offsetOfFirstEntry > 0;

	public bool IsNewArchive => isNewArchive_;

	public string ZipFileComment => comment_;

	public string Name => name_;

	[Obsolete("Use the Count property instead")]
	public int Size => entries_.Length;

	public long Count => entries_.Length;

	[IndexerName("EntryByIndex")]
	public ZipEntry this[int index] => (ZipEntry)entries_[index].Clone();

	public INameTransform NameTransform
	{
		get
		{
			return updateEntryFactory_.NameTransform;
		}
		set
		{
			updateEntryFactory_.NameTransform = value;
		}
	}

	public IEntryFactory EntryFactory
	{
		get
		{
			return updateEntryFactory_;
		}
		set
		{
			if (value == null)
			{
				updateEntryFactory_ = new ZipEntryFactory();
			}
			else
			{
				updateEntryFactory_ = value;
			}
		}
	}

	public int BufferSize
	{
		get
		{
			return bufferSize_;
		}
		set
		{
			if (value < 1024)
			{
				throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
			}
			if (bufferSize_ != value)
			{
				bufferSize_ = value;
				copyBuffer_ = null;
			}
		}
	}

	public bool IsUpdating => updates_ != null;

	public UseZip64 UseZip64
	{
		get
		{
			return useZip64_;
		}
		set
		{
			useZip64_ = value;
		}
	}

	private void OnKeysRequired(string fileName)
	{
		if (KeysRequired != null)
		{
			KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, key);
			KeysRequired(this, keysRequiredEventArgs);
			key = keysRequiredEventArgs.Key;
		}
	}

	public ZipFile(string name)
	{
		name_ = name ?? throw new ArgumentNullException("name");
		baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
		isStreamOwner = true;
		try
		{
			ReadEntries();
		}
		catch
		{
			DisposeInternal(disposing: true);
			throw;
		}
	}

	public ZipFile(FileStream file)
		: this(file, leaveOpen: false)
	{
	}

	public ZipFile(FileStream file, bool leaveOpen)
	{
		if (file == null)
		{
			throw new ArgumentNullException("file");
		}
		if (!file.CanSeek)
		{
			throw new ArgumentException("Stream is not seekable", "file");
		}
		baseStream_ = file;
		name_ = file.Name;
		isStreamOwner = !leaveOpen;
		try
		{
			ReadEntries();
		}
		catch
		{
			DisposeInternal(disposing: true);
			throw;
		}
	}

	public ZipFile(Stream stream)
		: this(stream, leaveOpen: false)
	{
	}

	public ZipFile(Stream stream, bool leaveOpen)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (!stream.CanSeek)
		{
			throw new ArgumentException("Stream is not seekable", "stream");
		}
		baseStream_ = stream;
		isStreamOwner = !leaveOpen;
		if (baseStream_.Length > 0)
		{
			try
			{
				ReadEntries();
				return;
			}
			catch
			{
				DisposeInternal(disposing: true);
				throw;
			}
		}
		entries_ = new ZipEntry[0];
		isNewArchive_ = true;
	}

	internal ZipFile()
	{
		entries_ = new ZipEntry[0];
		isNewArchive_ = true;
	}

	~ZipFile()
	{
		Dispose(disposing: false);
	}

	public void Close()
	{
		DisposeInternal(disposing: true);
		GC.SuppressFinalize(this);
	}

	public static ZipFile Create(string fileName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		FileStream fileStream = File.Create(fileName);
		return new ZipFile
		{
			name_ = fileName,
			baseStream_ = fileStream,
			isStreamOwner = true
		};
	}

	public static ZipFile Create(Stream outStream)
	{
		if (outStream == null)
		{
			throw new ArgumentNullException("outStream");
		}
		if (!outStream.CanWrite)
		{
			throw new ArgumentException("Stream is not writeable", "outStream");
		}
		if (!outStream.CanSeek)
		{
			throw new ArgumentException("Stream is not seekable", "outStream");
		}
		return new ZipFile
		{
			baseStream_ = outStream
		};
	}

	public IEnumerator GetEnumerator()
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		return new ZipEntryEnumerator(entries_);
	}

	public int FindEntry(string name, bool ignoreCase)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		for (int i = 0; i < entries_.Length; i++)
		{
			if (string.Compare(name, entries_[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	public ZipEntry GetEntry(string name)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		int num = FindEntry(name, ignoreCase: true);
		if (num < 0)
		{
			return null;
		}
		return (ZipEntry)entries_[num].Clone();
	}

	public Stream GetInputStream(ZipEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		long num = entry.ZipFileIndex;
		if (num < 0 || num >= entries_.Length || entries_[num].Name != entry.Name)
		{
			num = FindEntry(entry.Name, ignoreCase: true);
			if (num < 0)
			{
				throw new ZipException("Entry cannot be found");
			}
		}
		return GetInputStream(num);
	}

	public Stream GetInputStream(long entryIndex)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		long start = LocateEntry(entries_[entryIndex]);
		CompressionMethod compressionMethod = entries_[entryIndex].CompressionMethod;
		Stream stream = new PartialInputStream(this, start, entries_[entryIndex].CompressedSize);
		if (entries_[entryIndex].IsCrypted)
		{
			stream = CreateAndInitDecryptionStream(stream, entries_[entryIndex]);
			if (stream == null)
			{
				throw new ZipException("Unable to decrypt this entry");
			}
		}
		switch (compressionMethod)
		{
		case CompressionMethod.Deflated:
			stream = new InflaterInputStream(stream, new Inflater(noHeader: true));
			break;
		case CompressionMethod.BZip2:
			stream = new BZip2InputStream(stream);
			break;
		default:
			throw new ZipException("Unsupported compression method " + compressionMethod);
		case CompressionMethod.Stored:
			break;
		}
		return stream;
	}

	public bool TestArchive(bool testData)
	{
		return TestArchive(testData, TestStrategy.FindFirstError, null);
	}

	public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		TestStatus testStatus = new TestStatus(this);
		resultHandler?.Invoke(testStatus, null);
		HeaderTest tests = (testData ? (HeaderTest.Extract | HeaderTest.Header) : HeaderTest.Header);
		bool flag = true;
		try
		{
			int num = 0;
			while (flag && num < Count)
			{
				if (resultHandler != null)
				{
					testStatus.SetEntry(this[num]);
					testStatus.SetOperation(TestOperation.EntryHeader);
					resultHandler(testStatus, null);
				}
				try
				{
					TestLocalHeader(this[num], tests);
				}
				catch (ZipException ex)
				{
					testStatus.AddError();
					resultHandler?.Invoke(testStatus, "Exception during test - '" + ex.Message + "'");
					flag = flag && strategy != TestStrategy.FindFirstError;
				}
				if (flag && testData && this[num].IsFile)
				{
					if (resultHandler != null)
					{
						testStatus.SetOperation(TestOperation.EntryData);
						resultHandler(testStatus, null);
					}
					Crc32 crc = new Crc32();
					using (Stream stream = GetInputStream(this[num]))
					{
						byte[] array = new byte[4096];
						long num2 = 0L;
						int num3;
						while ((num3 = stream.Read(array, 0, array.Length)) > 0)
						{
							crc.Update(new ArraySegment<byte>(array, 0, num3));
							if (resultHandler != null)
							{
								num2 += num3;
								testStatus.SetBytesTested(num2);
								resultHandler(testStatus, null);
							}
						}
					}
					if (this[num].Crc != crc.Value)
					{
						testStatus.AddError();
						resultHandler?.Invoke(testStatus, "CRC mismatch");
						flag = flag && strategy != TestStrategy.FindFirstError;
					}
					if (((uint)this[num].Flags & 8u) != 0)
					{
						ZipHelperStream zipHelperStream = new ZipHelperStream(baseStream_);
						DescriptorData descriptorData = new DescriptorData();
						zipHelperStream.ReadDataDescriptor(this[num].LocalHeaderRequiresZip64, descriptorData);
						if (this[num].Crc != descriptorData.Crc)
						{
							testStatus.AddError();
							resultHandler?.Invoke(testStatus, "Descriptor CRC mismatch");
						}
						if (this[num].CompressedSize != descriptorData.CompressedSize)
						{
							testStatus.AddError();
							resultHandler?.Invoke(testStatus, "Descriptor compressed size mismatch");
						}
						if (this[num].Size != descriptorData.Size)
						{
							testStatus.AddError();
							resultHandler?.Invoke(testStatus, "Descriptor size mismatch");
						}
					}
				}
				if (resultHandler != null)
				{
					testStatus.SetOperation(TestOperation.EntryComplete);
					resultHandler(testStatus, null);
				}
				num++;
			}
			if (resultHandler != null)
			{
				testStatus.SetOperation(TestOperation.MiscellaneousTests);
				resultHandler(testStatus, null);
			}
		}
		catch (Exception ex2)
		{
			testStatus.AddError();
			resultHandler?.Invoke(testStatus, "Exception during test - '" + ex2.Message + "'");
		}
		if (resultHandler != null)
		{
			testStatus.SetOperation(TestOperation.Complete);
			testStatus.SetEntry(null);
			resultHandler(testStatus, null);
		}
		return testStatus.ErrorCount == 0;
	}

	private long TestLocalHeader(ZipEntry entry, HeaderTest tests)
	{
		lock (baseStream_)
		{
			bool num = (tests & HeaderTest.Header) != 0;
			bool num2 = (tests & HeaderTest.Extract) != 0;
			long num3 = offsetOfFirstEntry + entry.Offset;
			baseStream_.Seek(num3, SeekOrigin.Begin);
			int num4 = (int)ReadLEUint();
			if (num4 != 67324752)
			{
				throw new ZipException($"Wrong local header signature at 0x{num3:x}, expected 0x{67324752:x8}, actual 0x{num4:x8}");
			}
			short num5 = (short)(ReadLEUshort() & 0xFF);
			short num6 = (short)ReadLEUshort();
			short num7 = (short)ReadLEUshort();
			short num8 = (short)ReadLEUshort();
			short num9 = (short)ReadLEUshort();
			uint num10 = ReadLEUint();
			long num11 = ReadLEUint();
			long num12 = ReadLEUint();
			int num13 = ReadLEUshort();
			int num14 = ReadLEUshort();
			byte[] array = new byte[num13];
			StreamUtils.ReadFully(baseStream_, array);
			byte[] array2 = new byte[num14];
			StreamUtils.ReadFully(baseStream_, array2);
			ZipExtraData zipExtraData = new ZipExtraData(array2);
			if (zipExtraData.Find(1))
			{
				num12 = zipExtraData.ReadLong();
				num11 = zipExtraData.ReadLong();
				if (((uint)num6 & 8u) != 0)
				{
					if (num12 != -1 && num12 != entry.Size)
					{
						throw new ZipException("Size invalid for descriptor");
					}
					if (num11 != -1 && num11 != entry.CompressedSize)
					{
						throw new ZipException("Compressed size invalid for descriptor");
					}
				}
			}
			else if (num5 >= 45 && ((int)num12 == -1 || (int)num11 == -1))
			{
				throw new ZipException("Required Zip64 extended information missing");
			}
			if (num2 && entry.IsFile)
			{
				if (!entry.IsCompressionMethodSupported())
				{
					throw new ZipException("Compression method not supported");
				}
				if (num5 > 51 || (num5 > 20 && num5 < 45))
				{
					throw new ZipException($"Version required to extract this entry not supported ({num5})");
				}
				if (((uint)num6 & 0x3060u) != 0)
				{
					throw new ZipException("The library does not support the zip version required to extract this entry");
				}
			}
			if (num)
			{
				if (num5 <= 63 && num5 != 10 && num5 != 11 && num5 != 20 && num5 != 21 && num5 != 25 && num5 != 27 && num5 != 45 && num5 != 46 && num5 != 50 && num5 != 51 && num5 != 52 && num5 != 61 && num5 != 62 && num5 != 63)
				{
					throw new ZipException($"Version required to extract this entry is invalid ({num5})");
				}
				if (((uint)num6 & 0xC010u) != 0)
				{
					throw new ZipException("Reserved bit flags cannot be set.");
				}
				if (((uint)num6 & (true ? 1u : 0u)) != 0 && num5 < 20)
				{
					throw new ZipException($"Version required to extract this entry is too low for encryption ({num5})");
				}
				if (((uint)num6 & 0x40u) != 0)
				{
					if ((num6 & 1) == 0)
					{
						throw new ZipException("Strong encryption flag set but encryption flag is not set");
					}
					if (num5 < 50)
					{
						throw new ZipException($"Version required to extract this entry is too low for encryption ({num5})");
					}
				}
				if (((uint)num6 & 0x20u) != 0 && num5 < 27)
				{
					throw new ZipException($"Patched data requires higher version than ({num5})");
				}
				if (num6 != entry.Flags)
				{
					throw new ZipException("Central header/local header flags mismatch");
				}
				if (entry.CompressionMethodForHeader != (CompressionMethod)num7)
				{
					throw new ZipException("Central header/local header compression method mismatch");
				}
				if (entry.Version != num5)
				{
					throw new ZipException("Extract version mismatch");
				}
				if (((uint)num6 & 0x40u) != 0 && num5 < 62)
				{
					throw new ZipException("Strong encryption flag set but version not high enough");
				}
				if (((uint)num6 & 0x2000u) != 0 && (num8 != 0 || num9 != 0))
				{
					throw new ZipException("Header masked set but date/time values non-zero");
				}
				if ((num6 & 8) == 0 && num10 != (uint)entry.Crc)
				{
					throw new ZipException("Central header/local header crc mismatch");
				}
				if (num12 == 0L && num11 == 0L && num10 != 0)
				{
					throw new ZipException("Invalid CRC for empty entry");
				}
				if (entry.Name.Length > num13)
				{
					throw new ZipException("File name length mismatch");
				}
				string text = ZipStrings.ConvertToStringExt(num6, array);
				if (text != entry.Name)
				{
					throw new ZipException("Central header and local header file name mismatch");
				}
				if (entry.IsDirectory)
				{
					if (num12 > 0)
					{
						throw new ZipException("Directory cannot have size");
					}
					if (entry.IsCrypted)
					{
						if (num11 > entry.EncryptionOverheadSize + 2)
						{
							throw new ZipException("Directory compressed size invalid");
						}
					}
					else if (num11 > 2)
					{
						throw new ZipException("Directory compressed size invalid");
					}
				}
				if (!ZipNameTransform.IsValidName(text, relaxed: true))
				{
					throw new ZipException("Name is invalid");
				}
			}
			if ((num6 & 8) == 0 || ((num12 > 0 || num11 > 0) && entry.Size > 0))
			{
				if (num12 != 0L && num12 != entry.Size)
				{
					throw new ZipException($"Size mismatch between central header({entry.Size}) and local header({num12})");
				}
				if (num11 != 0L && num11 != entry.CompressedSize && num11 != uint.MaxValue && num11 != -1)
				{
					throw new ZipException($"Compressed size mismatch between central header({entry.CompressedSize}) and local header({num11})");
				}
			}
			int num15 = num13 + num14;
			return offsetOfFirstEntry + entry.Offset + 30 + num15;
		}
	}

	public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		if (IsEmbeddedArchive)
		{
			throw new ZipException("Cannot update embedded/SFX archives");
		}
		archiveStorage_ = archiveStorage ?? throw new ArgumentNullException("archiveStorage");
		updateDataSource_ = dataSource ?? throw new ArgumentNullException("dataSource");
		updateIndex_ = new Dictionary<string, int>();
		updates_ = new List<ZipUpdate>(entries_.Length);
		ZipEntry[] array = entries_;
		foreach (ZipEntry zipEntry in array)
		{
			int count = updates_.Count;
			updates_.Add(new ZipUpdate(zipEntry));
			updateIndex_.Add(zipEntry.Name, count);
		}
		updates_.Sort(new UpdateComparer());
		int num = 0;
		foreach (ZipUpdate item in updates_)
		{
			if (num == updates_.Count - 1)
			{
				break;
			}
			item.OffsetBasedSize = updates_[num + 1].Entry.Offset - item.Entry.Offset;
			num++;
		}
		updateCount_ = updates_.Count;
		contentsEdited_ = false;
		commentEdited_ = false;
		newComment_ = null;
	}

	public void BeginUpdate(IArchiveStorage archiveStorage)
	{
		BeginUpdate(archiveStorage, new DynamicDiskDataSource());
	}

	public void BeginUpdate()
	{
		if (Name == null)
		{
			BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
		}
		else
		{
			BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
		}
	}

	public void CommitUpdate()
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		CheckUpdating();
		try
		{
			updateIndex_.Clear();
			updateIndex_ = null;
			if (contentsEdited_)
			{
				RunUpdates();
			}
			else if (commentEdited_)
			{
				UpdateCommentOnly();
			}
			else if (entries_.Length == 0)
			{
				byte[] comment = ((newComment_ != null) ? newComment_.RawComment : ZipStrings.ConvertToArray(comment_));
				using ZipHelperStream zipHelperStream = new ZipHelperStream(baseStream_);
				zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
				return;
			}
		}
		finally
		{
			PostUpdateCleanup();
		}
	}

	public void AbortUpdate()
	{
		PostUpdateCleanup();
	}

	public void SetComment(string comment)
	{
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		CheckUpdating();
		newComment_ = new ZipString(comment);
		if (newComment_.RawLength > 65535)
		{
			newComment_ = null;
			throw new ZipException("Comment length exceeds maximum - 65535");
		}
		commentEdited_ = true;
	}

	private void AddUpdate(ZipUpdate update)
	{
		contentsEdited_ = true;
		int num = FindExistingUpdate(update.Entry.Name, isEntryName: true);
		if (num >= 0)
		{
			if (updates_[num] == null)
			{
				updateCount_++;
			}
			updates_[num] = update;
		}
		else
		{
			num = updates_.Count;
			updates_.Add(update);
			updateCount_++;
			updateIndex_.Add(update.Entry.Name, num);
		}
	}

	public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		if (isDisposed_)
		{
			throw new ObjectDisposedException("ZipFile");
		}
		CheckSupportedCompressionMethod(compressionMethod);
		CheckUpdating();
		contentsEdited_ = true;
		ZipEntry zipEntry = EntryFactory.MakeFileEntry(fileName);
		zipEntry.IsUnicodeText = useUnicodeText;
		zipEntry.CompressionMethod = compressionMethod;
		AddUpdate(new ZipUpdate(fileName, zipEntry));
	}

	public void Add(string fileName, CompressionMethod compressionMethod)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		CheckSupportedCompressionMethod(compressionMethod);
		CheckUpdating();
		contentsEdited_ = true;
		ZipEntry zipEntry = EntryFactory.MakeFileEntry(fileName);
		zipEntry.CompressionMethod = compressionMethod;
		AddUpdate(new ZipUpdate(fileName, zipEntry));
	}

	public void Add(string fileName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		CheckUpdating();
		AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(fileName)));
	}

	public void Add(string fileName, string entryName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		CheckUpdating();
		AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(fileName, entryName, useFileSystem: true)));
	}

	public void Add(IStaticDataSource dataSource, string entryName)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		CheckUpdating();
		AddUpdate(new ZipUpdate(dataSource, EntryFactory.MakeFileEntry(entryName, useFileSystem: false)));
	}

	public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		CheckSupportedCompressionMethod(compressionMethod);
		CheckUpdating();
		ZipEntry zipEntry = EntryFactory.MakeFileEntry(entryName, useFileSystem: false);
		zipEntry.CompressionMethod = compressionMethod;
		AddUpdate(new ZipUpdate(dataSource, zipEntry));
	}

	public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
	{
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		CheckSupportedCompressionMethod(compressionMethod);
		CheckUpdating();
		ZipEntry zipEntry = EntryFactory.MakeFileEntry(entryName, useFileSystem: false);
		zipEntry.IsUnicodeText = useUnicodeText;
		zipEntry.CompressionMethod = compressionMethod;
		AddUpdate(new ZipUpdate(dataSource, zipEntry));
	}

	public void Add(ZipEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		CheckUpdating();
		if (entry.Size != 0L || entry.CompressedSize != 0L)
		{
			throw new ZipException("Entry cannot have any data");
		}
		AddUpdate(new ZipUpdate(UpdateCommand.Add, entry));
	}

	public void Add(IStaticDataSource dataSource, ZipEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		if (dataSource == null)
		{
			throw new ArgumentNullException("dataSource");
		}
		if (entry.AESKeySize > 0)
		{
			throw new NotSupportedException("Creation of AES encrypted entries is not supported");
		}
		CheckSupportedCompressionMethod(entry.CompressionMethod);
		CheckUpdating();
		AddUpdate(new ZipUpdate(dataSource, entry));
	}

	public void AddDirectory(string directoryName)
	{
		if (directoryName == null)
		{
			throw new ArgumentNullException("directoryName");
		}
		CheckUpdating();
		ZipEntry entry = EntryFactory.MakeDirectoryEntry(directoryName);
		AddUpdate(new ZipUpdate(UpdateCommand.Add, entry));
	}

	private void CheckSupportedCompressionMethod(CompressionMethod compressionMethod)
	{
		if (compressionMethod != CompressionMethod.Deflated && compressionMethod != 0 && compressionMethod != CompressionMethod.BZip2)
		{
			throw new NotImplementedException("Compression method not supported");
		}
	}

	public bool Delete(string fileName)
	{
		if (fileName == null)
		{
			throw new ArgumentNullException("fileName");
		}
		CheckUpdating();
		bool flag = false;
		int num = FindExistingUpdate(fileName);
		if (num >= 0 && updates_[num] != null)
		{
			flag = true;
			contentsEdited_ = true;
			updates_[num] = null;
			updateCount_--;
			return flag;
		}
		throw new ZipException("Cannot find entry to delete");
	}

	public void Delete(ZipEntry entry)
	{
		if (entry == null)
		{
			throw new ArgumentNullException("entry");
		}
		CheckUpdating();
		int num = FindExistingUpdate(entry);
		if (num >= 0)
		{
			contentsEdited_ = true;
			updates_[num] = null;
			updateCount_--;
			return;
		}
		throw new ZipException("Cannot find entry to delete");
	}

	private void WriteLEShort(int value)
	{
		baseStream_.WriteByte((byte)((uint)value & 0xFFu));
		baseStream_.WriteByte((byte)((uint)(value >> 8) & 0xFFu));
	}

	private void WriteLEUshort(ushort value)
	{
		baseStream_.WriteByte((byte)(value & 0xFFu));
		baseStream_.WriteByte((byte)(value >> 8));
	}

	private void WriteLEInt(int value)
	{
		WriteLEShort(value & 0xFFFF);
		WriteLEShort(value >> 16);
	}

	private void WriteLEUint(uint value)
	{
		WriteLEUshort((ushort)(value & 0xFFFFu));
		WriteLEUshort((ushort)(value >> 16));
	}

	private void WriteLeLong(long value)
	{
		WriteLEInt((int)(value & 0xFFFFFFFFu));
		WriteLEInt((int)(value >> 32));
	}

	private void WriteLEUlong(ulong value)
	{
		WriteLEUint((uint)(value & 0xFFFFFFFFu));
		WriteLEUint((uint)(value >> 32));
	}

	private void WriteLocalEntryHeader(ZipUpdate update)
	{
		ZipEntry outEntry = update.OutEntry;
		outEntry.Offset = baseStream_.Position;
		if (update.Command != 0)
		{
			if (outEntry.CompressionMethod == CompressionMethod.Deflated)
			{
				if (outEntry.Size == 0L)
				{
					outEntry.CompressedSize = outEntry.Size;
					outEntry.Crc = 0L;
					outEntry.CompressionMethod = CompressionMethod.Stored;
				}
			}
			else if (outEntry.CompressionMethod == CompressionMethod.Stored)
			{
				outEntry.Flags &= -9;
			}
			if (HaveKeys)
			{
				outEntry.IsCrypted = true;
				if (outEntry.Crc < 0)
				{
					outEntry.Flags |= 8;
				}
			}
			else
			{
				outEntry.IsCrypted = false;
			}
			switch (useZip64_)
			{
			case UseZip64.Dynamic:
				if (outEntry.Size < 0)
				{
					outEntry.ForceZip64();
				}
				break;
			case UseZip64.On:
				outEntry.ForceZip64();
				break;
			}
		}
		WriteLEInt(67324752);
		WriteLEShort(outEntry.Version);
		WriteLEShort(outEntry.Flags);
		WriteLEShort((byte)outEntry.CompressionMethodForHeader);
		WriteLEInt((int)outEntry.DosTime);
		if (!outEntry.HasCrc)
		{
			update.CrcPatchOffset = baseStream_.Position;
			WriteLEInt(0);
		}
		else
		{
			WriteLEInt((int)outEntry.Crc);
		}
		if (outEntry.LocalHeaderRequiresZip64)
		{
			WriteLEInt(-1);
			WriteLEInt(-1);
		}
		else
		{
			if (outEntry.CompressedSize < 0 || outEntry.Size < 0)
			{
				update.SizePatchOffset = baseStream_.Position;
			}
			WriteLEInt((int)outEntry.CompressedSize);
			WriteLEInt((int)outEntry.Size);
		}
		byte[] array = ZipStrings.ConvertToArray(outEntry.Flags, outEntry.Name);
		if (array.Length > 65535)
		{
			throw new ZipException("Entry name too long.");
		}
		ZipExtraData zipExtraData = new ZipExtraData(outEntry.ExtraData);
		if (outEntry.LocalHeaderRequiresZip64)
		{
			zipExtraData.StartNewEntry();
			zipExtraData.AddLeLong(outEntry.Size);
			zipExtraData.AddLeLong(outEntry.CompressedSize);
			zipExtraData.AddNewEntry(1);
		}
		else
		{
			zipExtraData.Delete(1);
		}
		outEntry.ExtraData = zipExtraData.GetEntryData();
		WriteLEShort(array.Length);
		WriteLEShort(outEntry.ExtraData.Length);
		if (array.Length != 0)
		{
			baseStream_.Write(array, 0, array.Length);
		}
		if (outEntry.LocalHeaderRequiresZip64)
		{
			if (!zipExtraData.Find(1))
			{
				throw new ZipException("Internal error cannot find extra data");
			}
			update.SizePatchOffset = baseStream_.Position + zipExtraData.CurrentReadIndex;
		}
		if (outEntry.ExtraData.Length != 0)
		{
			baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
		}
	}

	private int WriteCentralDirectoryHeader(ZipEntry entry)
	{
		if (entry.CompressedSize < 0)
		{
			throw new ZipException("Attempt to write central directory entry with unknown csize");
		}
		if (entry.Size < 0)
		{
			throw new ZipException("Attempt to write central directory entry with unknown size");
		}
		if (entry.Crc < 0)
		{
			throw new ZipException("Attempt to write central directory entry with unknown crc");
		}
		WriteLEInt(33639248);
		WriteLEShort((entry.HostSystem << 8) | entry.VersionMadeBy);
		WriteLEShort(entry.Version);
		WriteLEShort(entry.Flags);
		WriteLEShort((byte)entry.CompressionMethodForHeader);
		WriteLEInt((int)entry.DosTime);
		WriteLEInt((int)entry.Crc);
		bool flag = false;
		if (entry.IsZip64Forced() || entry.CompressedSize >= uint.MaxValue)
		{
			flag = true;
			WriteLEInt(-1);
		}
		else
		{
			WriteLEInt((int)(entry.CompressedSize & 0xFFFFFFFFu));
		}
		bool flag2 = false;
		if (entry.IsZip64Forced() || entry.Size >= uint.MaxValue)
		{
			flag2 = true;
			WriteLEInt(-1);
		}
		else
		{
			WriteLEInt((int)entry.Size);
		}
		byte[] array = ZipStrings.ConvertToArray(entry.Flags, entry.Name);
		if (array.Length > 65535)
		{
			throw new ZipException("Entry name is too long.");
		}
		WriteLEShort(array.Length);
		ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
		if (entry.CentralHeaderRequiresZip64)
		{
			zipExtraData.StartNewEntry();
			if (flag2)
			{
				zipExtraData.AddLeLong(entry.Size);
			}
			if (flag)
			{
				zipExtraData.AddLeLong(entry.CompressedSize);
			}
			if (entry.Offset >= uint.MaxValue)
			{
				zipExtraData.AddLeLong(entry.Offset);
			}
			zipExtraData.AddNewEntry(1);
		}
		else
		{
			zipExtraData.Delete(1);
		}
		byte[] entryData = zipExtraData.GetEntryData();
		WriteLEShort(entryData.Length);
		WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
		WriteLEShort(0);
		WriteLEShort(0);
		if (entry.ExternalFileAttributes != -1)
		{
			WriteLEInt(entry.ExternalFileAttributes);
		}
		else if (entry.IsDirectory)
		{
			WriteLEUint(16u);
		}
		else
		{
			WriteLEUint(0u);
		}
		if (entry.Offset >= uint.MaxValue)
		{
			WriteLEUint(uint.MaxValue);
		}
		else
		{
			WriteLEUint((uint)entry.Offset);
		}
		if (array.Length != 0)
		{
			baseStream_.Write(array, 0, array.Length);
		}
		if (entryData.Length != 0)
		{
			baseStream_.Write(entryData, 0, entryData.Length);
		}
		byte[] array2 = ((entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0]);
		if (array2.Length != 0)
		{
			baseStream_.Write(array2, 0, array2.Length);
		}
		return 46 + array.Length + entryData.Length + array2.Length;
	}

	private void PostUpdateCleanup()
	{
		updateDataSource_ = null;
		updates_ = null;
		updateIndex_ = null;
		if (archiveStorage_ != null)
		{
			archiveStorage_.Dispose();
			archiveStorage_ = null;
		}
	}

	private string GetTransformedFileName(string name)
	{
		INameTransform nameTransform = NameTransform;
		if (nameTransform == null)
		{
			return name;
		}
		return nameTransform.TransformFile(name);
	}

	private string GetTransformedDirectoryName(string name)
	{
		INameTransform nameTransform = NameTransform;
		if (nameTransform == null)
		{
			return name;
		}
		return nameTransform.TransformDirectory(name);
	}

	private byte[] GetBuffer()
	{
		if (copyBuffer_ == null)
		{
			copyBuffer_ = new byte[bufferSize_];
		}
		return copyBuffer_;
	}

	private void CopyDescriptorBytes(ZipUpdate update, Stream dest, Stream source)
	{
		int num = GetDescriptorSize(update, includingSignature: false);
		if (num == 0)
		{
			return;
		}
		byte[] buffer = GetBuffer();
		source.Read(buffer, 0, 4);
		dest.Write(buffer, 0, 4);
		if (BitConverter.ToUInt32(buffer, 0) != 134695760)
		{
			num -= buffer.Length;
		}
		while (num > 0)
		{
			int count = Math.Min(buffer.Length, num);
			int num2 = source.Read(buffer, 0, count);
			if (num2 > 0)
			{
				dest.Write(buffer, 0, num2);
				num -= num2;
				continue;
			}
			throw new ZipException("Unxpected end of stream");
		}
	}

	private void CopyBytes(ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
	{
		if (destination == source)
		{
			throw new InvalidOperationException("Destination and source are the same");
		}
		Crc32 crc = new Crc32();
		byte[] buffer = GetBuffer();
		long num = bytesToCopy;
		long num2 = 0L;
		int num4;
		do
		{
			int num3 = buffer.Length;
			if (bytesToCopy < num3)
			{
				num3 = (int)bytesToCopy;
			}
			num4 = source.Read(buffer, 0, num3);
			if (num4 > 0)
			{
				if (updateCrc)
				{
					crc.Update(new ArraySegment<byte>(buffer, 0, num4));
				}
				destination.Write(buffer, 0, num4);
				bytesToCopy -= num4;
				num2 += num4;
			}
		}
		while (num4 > 0 && bytesToCopy > 0);
		if (num2 != num)
		{
			throw new ZipException($"Failed to copy bytes expected {num} read {num2}");
		}
		if (updateCrc)
		{
			update.OutEntry.Crc = crc.Value;
		}
	}

	private int GetDescriptorSize(ZipUpdate update, bool includingSignature)
	{
		if (!((GeneralBitFlags)update.Entry.Flags).HasFlag(GeneralBitFlags.Descriptor))
		{
			return 0;
		}
		int num = (update.Entry.LocalHeaderRequiresZip64 ? 24 : 16);
		if (!includingSignature)
		{
			return num - 4;
		}
		return num;
	}

	private void CopyDescriptorBytesDirect(ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
	{
		byte[] buffer = GetBuffer();
		stream.Position = sourcePosition;
		stream.Read(buffer, 0, 4);
		bool includingSignature = BitConverter.ToUInt32(buffer, 0) == 134695760;
		int num = GetDescriptorSize(update, includingSignature);
		while (num > 0)
		{
			stream.Position = sourcePosition;
			int num2 = stream.Read(buffer, 0, num);
			if (num2 > 0)
			{
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num2);
				num -= num2;
				destinationPosition += num2;
				sourcePosition += num2;
				continue;
			}
			throw new ZipException("Unexpected end of stream");
		}
	}

	private void CopyEntryDataDirect(ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
	{
		long num = update.Entry.CompressedSize;
		Crc32 crc = new Crc32();
		byte[] buffer = GetBuffer();
		long num2 = num;
		long num3 = 0L;
		int num5;
		do
		{
			int num4 = buffer.Length;
			if (num < num4)
			{
				num4 = (int)num;
			}
			stream.Position = sourcePosition;
			num5 = stream.Read(buffer, 0, num4);
			if (num5 > 0)
			{
				if (updateCrc)
				{
					crc.Update(new ArraySegment<byte>(buffer, 0, num5));
				}
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num5);
				destinationPosition += num5;
				sourcePosition += num5;
				num -= num5;
				num3 += num5;
			}
		}
		while (num5 > 0 && num > 0);
		if (num3 != num2)
		{
			throw new ZipException($"Failed to copy bytes expected {num2} read {num3}");
		}
		if (updateCrc)
		{
			update.OutEntry.Crc = crc.Value;
		}
	}

	private int FindExistingUpdate(ZipEntry entry)
	{
		int result = -1;
		if (updateIndex_.ContainsKey(entry.Name))
		{
			result = updateIndex_[entry.Name];
		}
		return result;
	}

	private int FindExistingUpdate(string fileName, bool isEntryName = false)
	{
		int result = -1;
		string text = ((!isEntryName) ? GetTransformedFileName(fileName) : fileName);
		if (updateIndex_.ContainsKey(text))
		{
			result = updateIndex_[text];
		}
		return result;
	}

	private Stream GetOutputStream(ZipEntry entry)
	{
		Stream stream = baseStream_;
		if (entry.IsCrypted)
		{
			stream = CreateAndInitEncryptionStream(stream, entry);
		}
		switch (entry.CompressionMethod)
		{
		case CompressionMethod.Stored:
			if (!entry.IsCrypted)
			{
				stream = new UncompressedStream(stream);
			}
			break;
		case CompressionMethod.Deflated:
			stream = new DeflaterOutputStream(stream, new Deflater(9, noZlibHeaderOrFooter: true))
			{
				IsStreamOwner = entry.IsCrypted
			};
			break;
		case CompressionMethod.BZip2:
			stream = new BZip2OutputStream(stream)
			{
				IsStreamOwner = entry.IsCrypted
			};
			break;
		default:
			throw new ZipException("Unknown compression method " + entry.CompressionMethod);
		}
		return stream;
	}

	private void AddEntry(ZipFile workFile, ZipUpdate update)
	{
		Stream stream = null;
		if (update.Entry.IsFile)
		{
			stream = update.GetSource();
			if (stream == null)
			{
				stream = updateDataSource_.GetSource(update.Entry, update.Filename);
			}
		}
		if (stream != null)
		{
			using (stream)
			{
				long length = stream.Length;
				if (update.OutEntry.Size < 0)
				{
					update.OutEntry.Size = length;
				}
				else if (update.OutEntry.Size != length)
				{
					throw new ZipException("Entry size/stream size mismatch");
				}
				workFile.WriteLocalEntryHeader(update);
				long position = workFile.baseStream_.Position;
				using (Stream destination = workFile.GetOutputStream(update.OutEntry))
				{
					CopyBytes(update, destination, stream, length, updateCrc: true);
				}
				long position2 = workFile.baseStream_.Position;
				update.OutEntry.CompressedSize = position2 - position;
				if ((update.OutEntry.Flags & 8) == 8)
				{
					new ZipHelperStream(workFile.baseStream_).WriteDataDescriptor(update.OutEntry);
				}
				return;
			}
		}
		workFile.WriteLocalEntryHeader(update);
		update.OutEntry.CompressedSize = 0L;
	}

	private void ModifyEntry(ZipFile workFile, ZipUpdate update)
	{
		workFile.WriteLocalEntryHeader(update);
		long position = workFile.baseStream_.Position;
		if (update.Entry.IsFile && update.Filename != null)
		{
			using Stream destination = workFile.GetOutputStream(update.OutEntry);
			using Stream stream = GetInputStream(update.Entry);
			CopyBytes(update, destination, stream, stream.Length, updateCrc: true);
		}
		long position2 = workFile.baseStream_.Position;
		update.Entry.CompressedSize = position2 - position;
	}

	private void CopyEntryDirect(ZipFile workFile, ZipUpdate update, ref long destinationPosition)
	{
		bool num = update.Entry.Offset == destinationPosition;
		if (!num)
		{
			baseStream_.Position = destinationPosition;
			workFile.WriteLocalEntryHeader(update);
			destinationPosition = baseStream_.Position;
		}
		long num2 = 0L;
		long num3 = update.Entry.Offset + 26;
		baseStream_.Seek(num3, SeekOrigin.Begin);
		uint num4 = ReadLEUshort();
		uint num5 = ReadLEUshort();
		num2 = baseStream_.Position + num4 + num5;
		if (num)
		{
			if (update.OffsetBasedSize != -1)
			{
				destinationPosition += update.OffsetBasedSize;
				return;
			}
			destinationPosition += num2 - num3 + 26;
			destinationPosition += update.Entry.CompressedSize;
			baseStream_.Seek(destinationPosition, SeekOrigin.Begin);
			bool includingSignature = ReadLEUint() == 134695760;
			destinationPosition += GetDescriptorSize(update, includingSignature);
		}
		else
		{
			if (update.Entry.CompressedSize > 0)
			{
				CopyEntryDataDirect(update, baseStream_, updateCrc: false, ref destinationPosition, ref num2);
			}
			CopyDescriptorBytesDirect(update, baseStream_, ref destinationPosition, num2);
		}
	}

	private void CopyEntry(ZipFile workFile, ZipUpdate update)
	{
		workFile.WriteLocalEntryHeader(update);
		if (update.Entry.CompressedSize > 0)
		{
			long offset = update.Entry.Offset + 26;
			baseStream_.Seek(offset, SeekOrigin.Begin);
			uint num = ReadLEUshort();
			uint num2 = ReadLEUshort();
			baseStream_.Seek(num + num2, SeekOrigin.Current);
			CopyBytes(update, workFile.baseStream_, baseStream_, update.Entry.CompressedSize, updateCrc: false);
		}
		CopyDescriptorBytes(update, workFile.baseStream_, baseStream_);
	}

	private void Reopen(Stream source)
	{
		isNewArchive_ = false;
		baseStream_ = source ?? throw new ZipException("Failed to reopen archive - no source");
		ReadEntries();
	}

	private void Reopen()
	{
		if (Name == null)
		{
			throw new InvalidOperationException("Name is not known cannot Reopen");
		}
		Reopen(File.Open(Name, FileMode.Open, FileAccess.Read, FileShare.Read));
	}

	private void UpdateCommentOnly()
	{
		long length = baseStream_.Length;
		ZipHelperStream zipHelperStream = null;
		if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
		{
			zipHelperStream = new ZipHelperStream(archiveStorage_.MakeTemporaryCopy(baseStream_))
			{
				IsStreamOwner = true
			};
			baseStream_.Dispose();
			baseStream_ = null;
		}
		else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
		{
			baseStream_ = archiveStorage_.OpenForDirectUpdate(baseStream_);
			zipHelperStream = new ZipHelperStream(baseStream_);
		}
		else
		{
			baseStream_.Dispose();
			baseStream_ = null;
			zipHelperStream = new ZipHelperStream(Name);
		}
		using (zipHelperStream)
		{
			if (zipHelperStream.LocateBlockWithSignature(101010256, length, 22, 65535) < 0)
			{
				throw new ZipException("Cannot find central directory");
			}
			zipHelperStream.Position += 16L;
			byte[] rawComment = newComment_.RawComment;
			zipHelperStream.WriteLEShort(rawComment.Length);
			zipHelperStream.Write(rawComment, 0, rawComment.Length);
			zipHelperStream.SetLength(zipHelperStream.Position);
		}
		if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
		{
			Reopen(archiveStorage_.ConvertTemporaryToFinal());
		}
		else
		{
			ReadEntries();
		}
	}

	private void RunUpdates()
	{
		long num = 0L;
		long num2 = 0L;
		bool flag = false;
		long destinationPosition = 0L;
		ZipFile zipFile;
		if (IsNewArchive)
		{
			zipFile = this;
			zipFile.baseStream_.Position = 0L;
			flag = true;
		}
		else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
		{
			zipFile = this;
			zipFile.baseStream_.Position = 0L;
			flag = true;
			updates_.Sort(new UpdateComparer());
		}
		else
		{
			zipFile = Create(archiveStorage_.GetTemporaryOutput());
			zipFile.UseZip64 = UseZip64;
			if (key != null)
			{
				zipFile.key = (byte[])key.Clone();
			}
		}
		try
		{
			foreach (ZipUpdate item in updates_)
			{
				if (item == null)
				{
					continue;
				}
				switch (item.Command)
				{
				case UpdateCommand.Copy:
					if (flag)
					{
						CopyEntryDirect(zipFile, item, ref destinationPosition);
					}
					else
					{
						CopyEntry(zipFile, item);
					}
					break;
				case UpdateCommand.Modify:
					ModifyEntry(zipFile, item);
					break;
				case UpdateCommand.Add:
					if (!IsNewArchive && flag)
					{
						zipFile.baseStream_.Position = destinationPosition;
					}
					AddEntry(zipFile, item);
					if (flag)
					{
						destinationPosition = zipFile.baseStream_.Position;
					}
					break;
				}
			}
			if (!IsNewArchive && flag)
			{
				zipFile.baseStream_.Position = destinationPosition;
			}
			long position = zipFile.baseStream_.Position;
			foreach (ZipUpdate item2 in updates_)
			{
				if (item2 != null)
				{
					num += zipFile.WriteCentralDirectoryHeader(item2.OutEntry);
				}
			}
			byte[] comment = ((newComment_ != null) ? newComment_.RawComment : ZipStrings.ConvertToArray(comment_));
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(zipFile.baseStream_))
			{
				zipHelperStream.WriteEndOfCentralDirectory(updateCount_, num, position, comment);
			}
			num2 = zipFile.baseStream_.Position;
			foreach (ZipUpdate item3 in updates_)
			{
				if (item3 == null)
				{
					continue;
				}
				if (item3.CrcPatchOffset > 0 && item3.OutEntry.CompressedSize > 0)
				{
					zipFile.baseStream_.Position = item3.CrcPatchOffset;
					zipFile.WriteLEInt((int)item3.OutEntry.Crc);
				}
				if (item3.SizePatchOffset > 0)
				{
					zipFile.baseStream_.Position = item3.SizePatchOffset;
					if (item3.OutEntry.LocalHeaderRequiresZip64)
					{
						zipFile.WriteLeLong(item3.OutEntry.Size);
						zipFile.WriteLeLong(item3.OutEntry.CompressedSize);
					}
					else
					{
						zipFile.WriteLEInt((int)item3.OutEntry.CompressedSize);
						zipFile.WriteLEInt((int)item3.OutEntry.Size);
					}
				}
			}
		}
		catch
		{
			zipFile.Close();
			if (!flag && zipFile.Name != null)
			{
				File.Delete(zipFile.Name);
			}
			throw;
		}
		if (flag)
		{
			zipFile.baseStream_.SetLength(num2);
			zipFile.baseStream_.Flush();
			isNewArchive_ = false;
			ReadEntries();
		}
		else
		{
			baseStream_.Dispose();
			Reopen(archiveStorage_.ConvertTemporaryToFinal());
		}
	}

	private void CheckUpdating()
	{
		if (updates_ == null)
		{
			throw new InvalidOperationException("BeginUpdate has not been called");
		}
	}

	void IDisposable.Dispose()
	{
		Close();
	}

	private void DisposeInternal(bool disposing)
	{
		if (isDisposed_)
		{
			return;
		}
		isDisposed_ = true;
		entries_ = new ZipEntry[0];
		if (IsStreamOwner && baseStream_ != null)
		{
			lock (baseStream_)
			{
				baseStream_.Dispose();
			}
		}
		PostUpdateCleanup();
	}

	protected virtual void Dispose(bool disposing)
	{
		DisposeInternal(disposing);
	}

	private ushort ReadLEUshort()
	{
		int num = baseStream_.ReadByte();
		if (num < 0)
		{
			throw new EndOfStreamException("End of stream");
		}
		int num2 = baseStream_.ReadByte();
		if (num2 < 0)
		{
			throw new EndOfStreamException("End of stream");
		}
		return (ushort)((ushort)num | (ushort)(num2 << 8));
	}

	private uint ReadLEUint()
	{
		return (uint)(ReadLEUshort() | (ReadLEUshort() << 16));
	}

	private ulong ReadLEUlong()
	{
		return ReadLEUint() | ((ulong)ReadLEUint() << 32);
	}

	private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
	{
		using ZipHelperStream zipHelperStream = new ZipHelperStream(baseStream_);
		return zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
	}

	private void ReadEntries()
	{
		if (!baseStream_.CanSeek)
		{
			throw new ZipException("ZipFile stream must be seekable");
		}
		long num = LocateBlockWithSignature(101010256, baseStream_.Length, 22, 65535);
		if (num < 0)
		{
			throw new ZipException("Cannot find central directory");
		}
		ushort num2 = ReadLEUshort();
		ushort num3 = ReadLEUshort();
		ulong num4 = ReadLEUshort();
		ulong num5 = ReadLEUshort();
		ulong num6 = ReadLEUint();
		long num7 = ReadLEUint();
		uint num8 = ReadLEUshort();
		if (num8 != 0)
		{
			byte[] array = new byte[num8];
			StreamUtils.ReadFully(baseStream_, array);
			comment_ = ZipStrings.ConvertToString(array);
		}
		else
		{
			comment_ = string.Empty;
		}
		bool flag = false;
		bool flag2 = false;
		if (num2 == ushort.MaxValue || num3 == ushort.MaxValue || num4 == 65535 || num5 == 65535 || num6 == uint.MaxValue || num7 == uint.MaxValue)
		{
			flag2 = true;
		}
		if (LocateBlockWithSignature(117853008, num - 4, 20, 0) < 0)
		{
			if (flag2)
			{
				throw new ZipException("Cannot find Zip64 locator");
			}
		}
		else
		{
			flag = true;
			ReadLEUint();
			ulong num9 = ReadLEUlong();
			ReadLEUint();
			baseStream_.Position = (long)num9;
			if ((ulong)ReadLEUint() != 101075792)
			{
				throw new ZipException($"Invalid Zip64 Central directory signature at {num9:X}");
			}
			ReadLEUlong();
			ReadLEUshort();
			ReadLEUshort();
			ReadLEUint();
			ReadLEUint();
			num4 = ReadLEUlong();
			num5 = ReadLEUlong();
			num6 = ReadLEUlong();
			num7 = (long)ReadLEUlong();
		}
		entries_ = new ZipEntry[num4];
		if (!flag && num7 < num - (long)(4 + num6))
		{
			offsetOfFirstEntry = num - ((long)(4 + num6) + num7);
			if (offsetOfFirstEntry <= 0)
			{
				throw new ZipException("Invalid embedded zip archive");
			}
		}
		baseStream_.Seek(offsetOfFirstEntry + num7, SeekOrigin.Begin);
		for (ulong num10 = 0uL; num10 < num4; num10++)
		{
			if (ReadLEUint() != 33639248)
			{
				throw new ZipException("Wrong Central Directory signature");
			}
			int madeByInfo = ReadLEUshort();
			int versionRequiredToExtract = ReadLEUshort();
			int num11 = ReadLEUshort();
			int method = ReadLEUshort();
			uint num12 = ReadLEUint();
			uint num13 = ReadLEUint();
			long num14 = ReadLEUint();
			long num15 = ReadLEUint();
			int num16 = ReadLEUshort();
			int num17 = ReadLEUshort();
			int num18 = ReadLEUshort();
			ReadLEUshort();
			ReadLEUshort();
			uint externalFileAttributes = ReadLEUint();
			long offset = ReadLEUint();
			byte[] array2 = new byte[Math.Max(num16, num18)];
			StreamUtils.ReadFully(baseStream_, array2, 0, num16);
			ZipEntry zipEntry = new ZipEntry(ZipStrings.ConvertToStringExt(num11, array2, num16), versionRequiredToExtract, madeByInfo, (CompressionMethod)method)
			{
				Crc = ((long)num13 & 0xFFFFFFFFL),
				Size = (num15 & 0xFFFFFFFFu),
				CompressedSize = (num14 & 0xFFFFFFFFu),
				Flags = num11,
				DosTime = num12,
				ZipFileIndex = (long)num10,
				Offset = offset,
				ExternalFileAttributes = (int)externalFileAttributes
			};
			if ((num11 & 8) == 0)
			{
				zipEntry.CryptoCheckValue = (byte)(num13 >> 24);
			}
			else
			{
				zipEntry.CryptoCheckValue = (byte)((num12 >> 8) & 0xFFu);
			}
			if (num17 > 0)
			{
				byte[] array3 = new byte[num17];
				StreamUtils.ReadFully(baseStream_, array3);
				zipEntry.ExtraData = array3;
			}
			zipEntry.ProcessExtraData(localHeader: false);
			if (num18 > 0)
			{
				StreamUtils.ReadFully(baseStream_, array2, 0, num18);
				zipEntry.Comment = ZipStrings.ConvertToStringExt(num11, array2, num18);
			}
			entries_[num10] = zipEntry;
		}
	}

	private long LocateEntry(ZipEntry entry)
	{
		return TestLocalHeader(entry, HeaderTest.Extract);
	}

	private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
	{
		CryptoStream cryptoStream = null;
		if (entry.CompressionMethodForHeader == CompressionMethod.WinZipAES)
		{
			if (entry.Version < 51)
			{
				throw new ZipException("Decryption method not supported");
			}
			OnKeysRequired(entry.Name);
			if (rawPassword_ == null)
			{
				throw new ZipException("No password available for AES encrypted stream");
			}
			int aESSaltLen = entry.AESSaltLen;
			byte[] array = new byte[aESSaltLen];
			int num = StreamUtils.ReadRequestedBytes(baseStream, array, 0, aESSaltLen);
			if (num != aESSaltLen)
			{
				throw new ZipException("AES Salt expected " + aESSaltLen + " got " + num);
			}
			byte[] array2 = new byte[2];
			StreamUtils.ReadFully(baseStream, array2);
			int blockSize = entry.AESKeySize / 8;
			ZipAESTransform zipAESTransform = new ZipAESTransform(rawPassword_, array, blockSize, writeMode: false);
			byte[] pwdVerifier = zipAESTransform.PwdVerifier;
			if (pwdVerifier[0] != array2[0] || pwdVerifier[1] != array2[1])
			{
				throw new ZipException("Invalid password for AES");
			}
			cryptoStream = new ZipAESStream(baseStream, zipAESTransform, CryptoStreamMode.Read);
		}
		else
		{
			if (entry.Version >= 50 && ((uint)entry.Flags & 0x40u) != 0)
			{
				throw new ZipException("Decryption method not supported");
			}
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			OnKeysRequired(entry.Name);
			if (!HaveKeys)
			{
				throw new ZipException("No password available for encrypted stream");
			}
			cryptoStream = new CryptoStream(baseStream, pkzipClassicManaged.CreateDecryptor(key, null), CryptoStreamMode.Read);
			CheckClassicPassword(cryptoStream, entry);
		}
		return cryptoStream;
	}

	private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
	{
		CryptoStream cryptoStream = null;
		if (entry.Version < 50 || (entry.Flags & 0x40) == 0)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			OnKeysRequired(entry.Name);
			if (!HaveKeys)
			{
				throw new ZipException("No password available for encrypted stream");
			}
			cryptoStream = new CryptoStream(new UncompressedStream(baseStream), pkzipClassicManaged.CreateEncryptor(key, null), CryptoStreamMode.Write);
			if (entry.Crc < 0 || ((uint)entry.Flags & 8u) != 0)
			{
				WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
			}
			else
			{
				WriteEncryptionHeader(cryptoStream, entry.Crc);
			}
		}
		return cryptoStream;
	}

	private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
	{
		byte[] array = new byte[12];
		StreamUtils.ReadFully(classicCryptoStream, array);
		if (array[11] != entry.CryptoCheckValue)
		{
			throw new ZipException("Invalid password");
		}
	}

	private static void WriteEncryptionHeader(Stream stream, long crcValue)
	{
		byte[] array = new byte[12];
		using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
		{
			rNGCryptoServiceProvider.GetBytes(array);
		}
		array[11] = (byte)(crcValue >> 24);
		stream.Write(array, 0, array.Length);
	}
}
