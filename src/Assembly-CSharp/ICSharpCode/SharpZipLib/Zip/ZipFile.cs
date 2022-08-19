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

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200053F RID: 1343
	public class ZipFile : IEnumerable, IDisposable
	{
		// Token: 0x06002B13 RID: 11027 RVA: 0x00142E84 File Offset: 0x00141084
		private void OnKeysRequired(string fileName)
		{
			if (this.KeysRequired != null)
			{
				KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, this.key);
				this.KeysRequired(this, keysRequiredEventArgs);
				this.key = keysRequiredEventArgs.Key;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x00142EBF File Offset: 0x001410BF
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x00142EC7 File Offset: 0x001410C7
		private byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x00142ED0 File Offset: 0x001410D0
		public string Password
		{
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.key = null;
				}
				else
				{
					this.key = PkzipClassic.GenerateKeys(ZipStrings.ConvertToArray(value));
				}
				this.rawPassword_ = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x00142EFB File Offset: 0x001410FB
		private bool HaveKeys
		{
			get
			{
				return this.key != null;
			}
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x00142F08 File Offset: 0x00141108
		public ZipFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name_ = name;
			this.baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.isStreamOwner = true;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x00142F88 File Offset: 0x00141188
		public ZipFile(FileStream file) : this(file, false)
		{
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x00142F94 File Offset: 0x00141194
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
			this.baseStream_ = file;
			this.name_ = file.Name;
			this.isStreamOwner = !leaveOpen;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0014302C File Offset: 0x0014122C
		public ZipFile(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x00143038 File Offset: 0x00141238
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
			this.baseStream_ = stream;
			this.isStreamOwner = !leaveOpen;
			if (this.baseStream_.Length > 0L)
			{
				try
				{
					this.ReadEntries();
					return;
				}
				catch
				{
					this.DisposeInternal(true);
					throw;
				}
			}
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x001430E4 File Offset: 0x001412E4
		internal ZipFile()
		{
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0014311C File Offset: 0x0014131C
		~ZipFile()
		{
			this.Dispose(false);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0014314C File Offset: 0x0014134C
		public void Close()
		{
			this.DisposeInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0014315C File Offset: 0x0014135C
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

		// Token: 0x06002B21 RID: 11041 RVA: 0x00143198 File Offset: 0x00141398
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

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x001431EF File Offset: 0x001413EF
		// (set) Token: 0x06002B23 RID: 11043 RVA: 0x001431F7 File Offset: 0x001413F7
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x00143200 File Offset: 0x00141400
		public bool IsEmbeddedArchive
		{
			get
			{
				return this.offsetOfFirstEntry > 0L;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x0014320C File Offset: 0x0014140C
		public bool IsNewArchive
		{
			get
			{
				return this.isNewArchive_;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x00143214 File Offset: 0x00141414
		public string ZipFileComment
		{
			get
			{
				return this.comment_;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x0014321C File Offset: 0x0014141C
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x00143224 File Offset: 0x00141424
		[Obsolete("Use the Count property instead")]
		public int Size
		{
			get
			{
				return this.entries_.Length;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x0014322E File Offset: 0x0014142E
		public long Count
		{
			get
			{
				return (long)this.entries_.Length;
			}
		}

		// Token: 0x17000317 RID: 791
		[IndexerName("EntryByIndex")]
		public ZipEntry this[int index]
		{
			get
			{
				return (ZipEntry)this.entries_[index].Clone();
			}
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0014324D File Offset: 0x0014144D
		public IEnumerator GetEnumerator()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			return new ZipFile.ZipEntryEnumerator(this.entries_);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x00143270 File Offset: 0x00141470
		public int FindEntry(string name, bool ignoreCase)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			for (int i = 0; i < this.entries_.Length; i++)
			{
				if (string.Compare(name, this.entries_[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x001432C4 File Offset: 0x001414C4
		public ZipEntry GetEntry(string name)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			int num = this.FindEntry(name, true);
			if (num < 0)
			{
				return null;
			}
			return (ZipEntry)this.entries_[num].Clone();
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x00143308 File Offset: 0x00141508
		public Stream GetInputStream(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			long num = entry.ZipFileIndex;
			if (num < 0L || num >= (long)this.entries_.Length || this.entries_[(int)(checked((IntPtr)num))].Name != entry.Name)
			{
				num = (long)this.FindEntry(entry.Name, true);
				if (num < 0L)
				{
					throw new ZipException("Entry cannot be found");
				}
			}
			return this.GetInputStream(num);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x00143390 File Offset: 0x00141590
		public Stream GetInputStream(long entryIndex)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			checked
			{
				long start = this.LocateEntry(this.entries_[(int)((IntPtr)entryIndex)]);
				CompressionMethod compressionMethod = this.entries_[(int)((IntPtr)entryIndex)].CompressionMethod;
				Stream stream = new ZipFile.PartialInputStream(this, start, this.entries_[(int)((IntPtr)entryIndex)].CompressedSize);
				if (this.entries_[(int)((IntPtr)entryIndex)].IsCrypted)
				{
					stream = this.CreateAndInitDecryptionStream(stream, this.entries_[(int)((IntPtr)entryIndex)]);
					if (stream == null)
					{
						throw new ZipException("Unable to decrypt this entry");
					}
				}
				if (compressionMethod != CompressionMethod.Stored)
				{
					if (compressionMethod != CompressionMethod.Deflated)
					{
						if (compressionMethod != CompressionMethod.BZip2)
						{
							throw new ZipException("Unsupported compression method " + compressionMethod);
						}
						stream = new BZip2InputStream(stream);
					}
					else
					{
						stream = new InflaterInputStream(stream, new Inflater(true));
					}
				}
				return stream;
			}
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x00143451 File Offset: 0x00141651
		public bool TestArchive(bool testData)
		{
			return this.TestArchive(testData, TestStrategy.FindFirstError, null);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0014345C File Offset: 0x0014165C
		public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			TestStatus testStatus = new TestStatus(this);
			if (resultHandler != null)
			{
				resultHandler(testStatus, null);
			}
			ZipFile.HeaderTest tests = testData ? (ZipFile.HeaderTest.Extract | ZipFile.HeaderTest.Header) : ZipFile.HeaderTest.Header;
			bool flag = true;
			try
			{
				int num = 0;
				while (flag && (long)num < this.Count)
				{
					if (resultHandler != null)
					{
						testStatus.SetEntry(this[num]);
						testStatus.SetOperation(TestOperation.EntryHeader);
						resultHandler(testStatus, null);
					}
					try
					{
						this.TestLocalHeader(this[num], tests);
					}
					catch (ZipException ex)
					{
						testStatus.AddError();
						if (resultHandler != null)
						{
							resultHandler(testStatus, "Exception during test - '" + ex.Message + "'");
						}
						flag &= (strategy > TestStrategy.FindFirstError);
					}
					if (flag && testData && this[num].IsFile)
					{
						if (resultHandler != null)
						{
							testStatus.SetOperation(TestOperation.EntryData);
							resultHandler(testStatus, null);
						}
						Crc32 crc = new Crc32();
						using (Stream inputStream = this.GetInputStream(this[num]))
						{
							byte[] array = new byte[4096];
							long num2 = 0L;
							int num3;
							while ((num3 = inputStream.Read(array, 0, array.Length)) > 0)
							{
								crc.Update(new ArraySegment<byte>(array, 0, num3));
								if (resultHandler != null)
								{
									num2 += (long)num3;
									testStatus.SetBytesTested(num2);
									resultHandler(testStatus, null);
								}
							}
						}
						if (this[num].Crc != crc.Value)
						{
							testStatus.AddError();
							if (resultHandler != null)
							{
								resultHandler(testStatus, "CRC mismatch");
							}
							flag &= (strategy > TestStrategy.FindFirstError);
						}
						if ((this[num].Flags & 8) != 0)
						{
							ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_);
							DescriptorData descriptorData = new DescriptorData();
							zipHelperStream.ReadDataDescriptor(this[num].LocalHeaderRequiresZip64, descriptorData);
							if (this[num].Crc != descriptorData.Crc)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor CRC mismatch");
								}
							}
							if (this[num].CompressedSize != descriptorData.CompressedSize)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor compressed size mismatch");
								}
							}
							if (this[num].Size != descriptorData.Size)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor size mismatch");
								}
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
				if (resultHandler != null)
				{
					resultHandler(testStatus, "Exception during test - '" + ex2.Message + "'");
				}
			}
			if (resultHandler != null)
			{
				testStatus.SetOperation(TestOperation.Complete);
				testStatus.SetEntry(null);
				resultHandler(testStatus, null);
			}
			return testStatus.ErrorCount == 0;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0014375C File Offset: 0x0014195C
		private long TestLocalHeader(ZipEntry entry, ZipFile.HeaderTest tests)
		{
			Stream obj = this.baseStream_;
			long result;
			lock (obj)
			{
				bool flag2 = (tests & ZipFile.HeaderTest.Header) > (ZipFile.HeaderTest)0;
				bool flag3 = (tests & ZipFile.HeaderTest.Extract) > (ZipFile.HeaderTest)0;
				long num = this.offsetOfFirstEntry + entry.Offset;
				this.baseStream_.Seek(num, SeekOrigin.Begin);
				int num2 = (int)this.ReadLEUint();
				if (num2 != 67324752)
				{
					throw new ZipException(string.Format("Wrong local header signature at 0x{0:x}, expected 0x{1:x8}, actual 0x{2:x8}", num, 67324752, num2));
				}
				short num3 = (short)(this.ReadLEUshort() & 255);
				short num4 = (short)this.ReadLEUshort();
				short num5 = (short)this.ReadLEUshort();
				short num6 = (short)this.ReadLEUshort();
				short num7 = (short)this.ReadLEUshort();
				uint num8 = this.ReadLEUint();
				long num9 = (long)((ulong)this.ReadLEUint());
				long num10 = (long)((ulong)this.ReadLEUint());
				int num11 = (int)this.ReadLEUshort();
				int num12 = (int)this.ReadLEUshort();
				byte[] array = new byte[num11];
				StreamUtils.ReadFully(this.baseStream_, array);
				byte[] array2 = new byte[num12];
				StreamUtils.ReadFully(this.baseStream_, array2);
				ZipExtraData zipExtraData = new ZipExtraData(array2);
				if (zipExtraData.Find(1))
				{
					num10 = zipExtraData.ReadLong();
					num9 = zipExtraData.ReadLong();
					if ((num4 & 8) != 0)
					{
						if (num10 != -1L && num10 != entry.Size)
						{
							throw new ZipException("Size invalid for descriptor");
						}
						if (num9 != -1L && num9 != entry.CompressedSize)
						{
							throw new ZipException("Compressed size invalid for descriptor");
						}
					}
				}
				else if (num3 >= 45 && ((uint)num10 == 4294967295U || (uint)num9 == 4294967295U))
				{
					throw new ZipException("Required Zip64 extended information missing");
				}
				if (flag3 && entry.IsFile)
				{
					if (!entry.IsCompressionMethodSupported())
					{
						throw new ZipException("Compression method not supported");
					}
					if (num3 > 51 || (num3 > 20 && num3 < 45))
					{
						throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", num3));
					}
					if ((num4 & 12384) != 0)
					{
						throw new ZipException("The library does not support the zip version required to extract this entry");
					}
				}
				if (flag2)
				{
					if (num3 <= 63 && num3 != 10 && num3 != 11 && num3 != 20 && num3 != 21 && num3 != 25 && num3 != 27 && num3 != 45 && num3 != 46 && num3 != 50 && num3 != 51 && num3 != 52 && num3 != 61 && num3 != 62 && num3 != 63)
					{
						throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", num3));
					}
					if (((int)num4 & 49168) != 0)
					{
						throw new ZipException("Reserved bit flags cannot be set.");
					}
					if ((num4 & 1) != 0 && num3 < 20)
					{
						throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num3));
					}
					if ((num4 & 64) != 0)
					{
						if ((num4 & 1) == 0)
						{
							throw new ZipException("Strong encryption flag set but encryption flag is not set");
						}
						if (num3 < 50)
						{
							throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num3));
						}
					}
					if ((num4 & 32) != 0 && num3 < 27)
					{
						throw new ZipException(string.Format("Patched data requires higher version than ({0})", num3));
					}
					if ((int)num4 != entry.Flags)
					{
						throw new ZipException("Central header/local header flags mismatch");
					}
					if (entry.CompressionMethodForHeader != (CompressionMethod)num5)
					{
						throw new ZipException("Central header/local header compression method mismatch");
					}
					if (entry.Version != (int)num3)
					{
						throw new ZipException("Extract version mismatch");
					}
					if ((num4 & 64) != 0 && num3 < 62)
					{
						throw new ZipException("Strong encryption flag set but version not high enough");
					}
					if ((num4 & 8192) != 0 && (num6 != 0 || num7 != 0))
					{
						throw new ZipException("Header masked set but date/time values non-zero");
					}
					if ((num4 & 8) == 0 && num8 != (uint)entry.Crc)
					{
						throw new ZipException("Central header/local header crc mismatch");
					}
					if (num10 == 0L && num9 == 0L && num8 != 0U)
					{
						throw new ZipException("Invalid CRC for empty entry");
					}
					if (entry.Name.Length > num11)
					{
						throw new ZipException("File name length mismatch");
					}
					string text = ZipStrings.ConvertToStringExt((int)num4, array);
					if (text != entry.Name)
					{
						throw new ZipException("Central header and local header file name mismatch");
					}
					if (entry.IsDirectory)
					{
						if (num10 > 0L)
						{
							throw new ZipException("Directory cannot have size");
						}
						if (entry.IsCrypted)
						{
							if (num9 > (long)(entry.EncryptionOverheadSize + 2))
							{
								throw new ZipException("Directory compressed size invalid");
							}
						}
						else if (num9 > 2L)
						{
							throw new ZipException("Directory compressed size invalid");
						}
					}
					if (!ZipNameTransform.IsValidName(text, true))
					{
						throw new ZipException("Name is invalid");
					}
				}
				if ((num4 & 8) == 0 || ((num10 > 0L || num9 > 0L) && entry.Size > 0L))
				{
					if (num10 != 0L && num10 != entry.Size)
					{
						throw new ZipException(string.Format("Size mismatch between central header({0}) and local header({1})", entry.Size, num10));
					}
					if (num9 != 0L && num9 != entry.CompressedSize && num9 != (long)((ulong)-1) && num9 != -1L)
					{
						throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", entry.CompressedSize, num9));
					}
				}
				int num13 = num11 + num12;
				result = this.offsetOfFirstEntry + entry.Offset + 30L + (long)num13;
			}
			return result;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x00143C50 File Offset: 0x00141E50
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x00143C5D File Offset: 0x00141E5D
		public INameTransform NameTransform
		{
			get
			{
				return this.updateEntryFactory_.NameTransform;
			}
			set
			{
				this.updateEntryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x00143C6B File Offset: 0x00141E6B
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x00143C73 File Offset: 0x00141E73
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.updateEntryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.updateEntryFactory_ = new ZipEntryFactory();
					return;
				}
				this.updateEntryFactory_ = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x00143C8B File Offset: 0x00141E8B
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x00143C93 File Offset: 0x00141E93
		public int BufferSize
		{
			get
			{
				return this.bufferSize_;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
				}
				if (this.bufferSize_ != value)
				{
					this.bufferSize_ = value;
					this.copyBuffer_ = null;
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x00143CC4 File Offset: 0x00141EC4
		public bool IsUpdating
		{
			get
			{
				return this.updates_ != null;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00143CCF File Offset: 0x00141ECF
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x00143CD7 File Offset: 0x00141ED7
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00143CE0 File Offset: 0x00141EE0
		public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			if (this.IsEmbeddedArchive)
			{
				throw new ZipException("Cannot update embedded/SFX archives");
			}
			if (archiveStorage == null)
			{
				throw new ArgumentNullException("archiveStorage");
			}
			this.archiveStorage_ = archiveStorage;
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.updateDataSource_ = dataSource;
			this.updateIndex_ = new Dictionary<string, int>();
			this.updates_ = new List<ZipFile.ZipUpdate>(this.entries_.Length);
			foreach (ZipEntry zipEntry in this.entries_)
			{
				int count = this.updates_.Count;
				this.updates_.Add(new ZipFile.ZipUpdate(zipEntry));
				this.updateIndex_.Add(zipEntry.Name, count);
			}
			this.updates_.Sort(new ZipFile.UpdateComparer());
			int num = 0;
			foreach (ZipFile.ZipUpdate zipUpdate in this.updates_)
			{
				if (num == this.updates_.Count - 1)
				{
					break;
				}
				zipUpdate.OffsetBasedSize = this.updates_[num + 1].Entry.Offset - zipUpdate.Entry.Offset;
				num++;
			}
			this.updateCount_ = (long)this.updates_.Count;
			this.contentsEdited_ = false;
			this.commentEdited_ = false;
			this.newComment_ = null;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00143E64 File Offset: 0x00142064
		public void BeginUpdate(IArchiveStorage archiveStorage)
		{
			this.BeginUpdate(archiveStorage, new DynamicDiskDataSource());
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00143E72 File Offset: 0x00142072
		public void BeginUpdate()
		{
			if (this.Name == null)
			{
				this.BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
				return;
			}
			this.BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00143EA0 File Offset: 0x001420A0
		public void CommitUpdate()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			try
			{
				this.updateIndex_.Clear();
				this.updateIndex_ = null;
				if (this.contentsEdited_)
				{
					this.RunUpdates();
				}
				else if (this.commentEdited_)
				{
					this.UpdateCommentOnly();
				}
				else if (this.entries_.Length == 0)
				{
					byte[] comment = (this.newComment_ != null) ? this.newComment_.RawComment : ZipStrings.ConvertToArray(this.comment_);
					using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
					{
						zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
					}
				}
			}
			finally
			{
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00143F6C File Offset: 0x0014216C
		public void AbortUpdate()
		{
			this.PostUpdateCleanup();
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00143F74 File Offset: 0x00142174
		public void SetComment(string comment)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			this.newComment_ = new ZipFile.ZipString(comment);
			if (this.newComment_.RawLength > 65535)
			{
				this.newComment_ = null;
				throw new ZipException("Comment length exceeds maximum - 65535");
			}
			this.commentEdited_ = true;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x00143FD4 File Offset: 0x001421D4
		private void AddUpdate(ZipFile.ZipUpdate update)
		{
			this.contentsEdited_ = true;
			int num = this.FindExistingUpdate(update.Entry.Name, true);
			if (num >= 0)
			{
				if (this.updates_[num] == null)
				{
					this.updateCount_ += 1L;
				}
				this.updates_[num] = update;
				return;
			}
			num = this.updates_.Count;
			this.updates_.Add(update);
			this.updateCount_ += 1L;
			this.updateIndex_.Add(update.Entry.Name, num);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00144068 File Offset: 0x00142268
		public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x001440D4 File Offset: 0x001422D4
		public void Add(string fileName, CompressionMethod compressionMethod)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x00144124 File Offset: 0x00142324
		public void Add(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00144152 File Offset: 0x00142352
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
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName, entryName, true)));
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x00144190 File Offset: 0x00142390
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
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, this.EntryFactory.MakeFileEntry(entryName, false)));
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x001441D0 File Offset: 0x001423D0
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
			this.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00144228 File Offset: 0x00142428
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
			this.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00144288 File Offset: 0x00142488
		public void Add(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			if (entry.Size != 0L || entry.CompressedSize != 0L)
			{
				throw new ZipException("Entry cannot have any data");
			}
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x001442C8 File Offset: 0x001424C8
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
			this.CheckSupportedCompressionMethod(entry.CompressionMethod);
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, entry));
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00144324 File Offset: 0x00142524
		public void AddDirectory(string directoryName)
		{
			if (directoryName == null)
			{
				throw new ArgumentNullException("directoryName");
			}
			this.CheckUpdating();
			ZipEntry entry = this.EntryFactory.MakeDirectoryEntry(directoryName);
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0014435F File Offset: 0x0014255F
		private void CheckSupportedCompressionMethod(CompressionMethod compressionMethod)
		{
			if (compressionMethod != CompressionMethod.Deflated && compressionMethod != CompressionMethod.Stored && compressionMethod != CompressionMethod.BZip2)
			{
				throw new NotImplementedException("Compression method not supported");
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x00144378 File Offset: 0x00142578
		public bool Delete(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(fileName, false);
			if (num >= 0 && this.updates_[num] != null)
			{
				bool result = true;
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return result;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x001443EC File Offset: 0x001425EC
		public void Delete(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(entry);
			if (num >= 0)
			{
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00144447 File Offset: 0x00142647
		private void WriteLEShort(int value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8 & 255));
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x00144471 File Offset: 0x00142671
		private void WriteLEUshort(ushort value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x00144495 File Offset: 0x00142695
		private void WriteLEInt(int value)
		{
			this.WriteLEShort(value & 65535);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x001444AE File Offset: 0x001426AE
		private void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x001444C9 File Offset: 0x001426C9
		private void WriteLeLong(long value)
		{
			this.WriteLEInt((int)(value & (long)((ulong)-1)));
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x001444E1 File Offset: 0x001426E1
		private void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)-1));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x001444FC File Offset: 0x001426FC
		private void WriteLocalEntryHeader(ZipFile.ZipUpdate update)
		{
			ZipEntry outEntry = update.OutEntry;
			outEntry.Offset = this.baseStream_.Position;
			if (update.Command != ZipFile.UpdateCommand.Copy)
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
				if (this.HaveKeys)
				{
					outEntry.IsCrypted = true;
					if (outEntry.Crc < 0L)
					{
						outEntry.Flags |= 8;
					}
				}
				else
				{
					outEntry.IsCrypted = false;
				}
				switch (this.useZip64_)
				{
				case UseZip64.On:
					outEntry.ForceZip64();
					break;
				case UseZip64.Dynamic:
					if (outEntry.Size < 0L)
					{
						outEntry.ForceZip64();
					}
					break;
				}
			}
			this.WriteLEInt(67324752);
			this.WriteLEShort(outEntry.Version);
			this.WriteLEShort(outEntry.Flags);
			this.WriteLEShort((int)((byte)outEntry.CompressionMethodForHeader));
			this.WriteLEInt((int)outEntry.DosTime);
			if (!outEntry.HasCrc)
			{
				update.CrcPatchOffset = this.baseStream_.Position;
				this.WriteLEInt(0);
			}
			else
			{
				this.WriteLEInt((int)outEntry.Crc);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				this.WriteLEInt(-1);
				this.WriteLEInt(-1);
			}
			else
			{
				if (outEntry.CompressedSize < 0L || outEntry.Size < 0L)
				{
					update.SizePatchOffset = this.baseStream_.Position;
				}
				this.WriteLEInt((int)outEntry.CompressedSize);
				this.WriteLEInt((int)outEntry.Size);
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
			this.WriteLEShort(array.Length);
			this.WriteLEShort(outEntry.ExtraData.Length);
			if (array.Length != 0)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				if (!zipExtraData.Find(1))
				{
					throw new ZipException("Internal error cannot find extra data");
				}
				update.SizePatchOffset = this.baseStream_.Position + (long)zipExtraData.CurrentReadIndex;
			}
			if (outEntry.ExtraData.Length != 0)
			{
				this.baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x00144790 File Offset: 0x00142990
		private int WriteCentralDirectoryHeader(ZipEntry entry)
		{
			if (entry.CompressedSize < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown csize");
			}
			if (entry.Size < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown size");
			}
			if (entry.Crc < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown crc");
			}
			this.WriteLEInt(33639248);
			this.WriteLEShort(entry.HostSystem << 8 | entry.VersionMadeBy);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)entry.CompressionMethodForHeader));
			this.WriteLEInt((int)entry.DosTime);
			this.WriteLEInt((int)entry.Crc);
			bool flag = false;
			if (entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)-1))
			{
				flag = true;
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)(entry.CompressedSize & (long)((ulong)-1)));
			}
			bool flag2 = false;
			if (entry.IsZip64Forced() || entry.Size >= (long)((ulong)-1))
			{
				flag2 = true;
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)entry.Size);
			}
			byte[] array = ZipStrings.ConvertToArray(entry.Flags, entry.Name);
			if (array.Length > 65535)
			{
				throw new ZipException("Entry name is too long.");
			}
			this.WriteLEShort(array.Length);
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
				if (entry.Offset >= (long)((ulong)-1))
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
			this.WriteLEShort(entryData.Length);
			this.WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			if (entry.ExternalFileAttributes != -1)
			{
				this.WriteLEInt(entry.ExternalFileAttributes);
			}
			else if (entry.IsDirectory)
			{
				this.WriteLEUint(16U);
			}
			else
			{
				this.WriteLEUint(0U);
			}
			if (entry.Offset >= (long)((ulong)-1))
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEUint((uint)((int)entry.Offset));
			}
			if (array.Length != 0)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			if (entryData.Length != 0)
			{
				this.baseStream_.Write(entryData, 0, entryData.Length);
			}
			byte[] array2 = (entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0];
			if (array2.Length != 0)
			{
				this.baseStream_.Write(array2, 0, array2.Length);
			}
			return 46 + array.Length + entryData.Length + array2.Length;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00144A25 File Offset: 0x00142C25
		private void PostUpdateCleanup()
		{
			this.updateDataSource_ = null;
			this.updates_ = null;
			this.updateIndex_ = null;
			if (this.archiveStorage_ != null)
			{
				this.archiveStorage_.Dispose();
				this.archiveStorage_ = null;
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00144A58 File Offset: 0x00142C58
		private string GetTransformedFileName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformFile(name);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00144A78 File Offset: 0x00142C78
		private string GetTransformedDirectoryName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformDirectory(name);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00144A98 File Offset: 0x00142C98
		private byte[] GetBuffer()
		{
			if (this.copyBuffer_ == null)
			{
				this.copyBuffer_ = new byte[this.bufferSize_];
			}
			return this.copyBuffer_;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00144ABC File Offset: 0x00142CBC
		private void CopyDescriptorBytes(ZipFile.ZipUpdate update, Stream dest, Stream source)
		{
			int i = this.GetDescriptorSize(update, false);
			if (i == 0)
			{
				return;
			}
			byte[] buffer = this.GetBuffer();
			source.Read(buffer, 0, 4);
			dest.Write(buffer, 0, 4);
			if (BitConverter.ToUInt32(buffer, 0) != 134695760U)
			{
				i -= buffer.Length;
			}
			while (i > 0)
			{
				int count = Math.Min(buffer.Length, i);
				int num = source.Read(buffer, 0, count);
				if (num <= 0)
				{
					throw new ZipException("Unxpected end of stream");
				}
				dest.Write(buffer, 0, num);
				i -= num;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00144B3C File Offset: 0x00142D3C
		private void CopyBytes(ZipFile.ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
		{
			if (destination == source)
			{
				throw new InvalidOperationException("Destination and source are the same");
			}
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num = bytesToCopy;
			long num2 = 0L;
			int num4;
			do
			{
				int num3 = buffer.Length;
				if (bytesToCopy < (long)num3)
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
					bytesToCopy -= (long)num4;
					num2 += (long)num4;
				}
			}
			while (num4 > 0 && bytesToCopy > 0L);
			if (num2 != num)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num, num2));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x00144BF8 File Offset: 0x00142DF8
		private int GetDescriptorSize(ZipFile.ZipUpdate update, bool includingSignature)
		{
			if (!((GeneralBitFlags)update.Entry.Flags).HasFlag(GeneralBitFlags.Descriptor))
			{
				return 0;
			}
			int num = update.Entry.LocalHeaderRequiresZip64 ? 24 : 16;
			if (!includingSignature)
			{
				return num - 4;
			}
			return num;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00144C40 File Offset: 0x00142E40
		private void CopyDescriptorBytesDirect(ZipFile.ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
		{
			byte[] buffer = this.GetBuffer();
			stream.Position = sourcePosition;
			stream.Read(buffer, 0, 4);
			bool includingSignature = BitConverter.ToUInt32(buffer, 0) == 134695760U;
			int i = this.GetDescriptorSize(update, includingSignature);
			while (i > 0)
			{
				stream.Position = sourcePosition;
				int num = stream.Read(buffer, 0, i);
				if (num <= 0)
				{
					throw new ZipException("Unexpected end of stream");
				}
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num);
				i -= num;
				destinationPosition += (long)num;
				sourcePosition += (long)num;
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00144CCC File Offset: 0x00142ECC
		private void CopyEntryDataDirect(ZipFile.ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
		{
			long num = update.Entry.CompressedSize;
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num2 = num;
			long num3 = 0L;
			int num5;
			do
			{
				int num4 = buffer.Length;
				if (num < (long)num4)
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
					destinationPosition += (long)num5;
					sourcePosition += (long)num5;
					num -= (long)num5;
					num3 += (long)num5;
				}
			}
			while (num5 > 0 && num > 0L);
			if (num3 != num2)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num2, num3));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x00144DA8 File Offset: 0x00142FA8
		private int FindExistingUpdate(ZipEntry entry)
		{
			int result = -1;
			if (this.updateIndex_.ContainsKey(entry.Name))
			{
				result = this.updateIndex_[entry.Name];
			}
			return result;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00144DE0 File Offset: 0x00142FE0
		private int FindExistingUpdate(string fileName, bool isEntryName = false)
		{
			int result = -1;
			string text = (!isEntryName) ? this.GetTransformedFileName(fileName) : fileName;
			if (this.updateIndex_.ContainsKey(text))
			{
				result = this.updateIndex_[text];
			}
			return result;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x00144E1C File Offset: 0x0014301C
		private Stream GetOutputStream(ZipEntry entry)
		{
			Stream stream = this.baseStream_;
			if (entry.IsCrypted)
			{
				stream = this.CreateAndInitEncryptionStream(stream, entry);
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod != CompressionMethod.Deflated)
				{
					if (compressionMethod != CompressionMethod.BZip2)
					{
						throw new ZipException("Unknown compression method " + entry.CompressionMethod);
					}
					stream = new BZip2OutputStream(stream)
					{
						IsStreamOwner = entry.IsCrypted
					};
				}
				else
				{
					stream = new DeflaterOutputStream(stream, new Deflater(9, true))
					{
						IsStreamOwner = entry.IsCrypted
					};
				}
			}
			else if (!entry.IsCrypted)
			{
				stream = new ZipFile.UncompressedStream(stream);
			}
			return stream;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x00144EB8 File Offset: 0x001430B8
		private void AddEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			Stream stream = null;
			if (update.Entry.IsFile)
			{
				stream = update.GetSource();
				if (stream == null)
				{
					stream = this.updateDataSource_.GetSource(update.Entry, update.Filename);
				}
			}
			if (stream != null)
			{
				using (stream)
				{
					long length = stream.Length;
					if (update.OutEntry.Size < 0L)
					{
						update.OutEntry.Size = length;
					}
					else if (update.OutEntry.Size != length)
					{
						throw new ZipException("Entry size/stream size mismatch");
					}
					workFile.WriteLocalEntryHeader(update);
					long position = workFile.baseStream_.Position;
					using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
					{
						this.CopyBytes(update, outputStream, stream, length, true);
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

		// Token: 0x06002B65 RID: 11109 RVA: 0x00144FF8 File Offset: 0x001431F8
		private void ModifyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			long position = workFile.baseStream_.Position;
			if (update.Entry.IsFile && update.Filename != null)
			{
				using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
				{
					using (Stream inputStream = this.GetInputStream(update.Entry))
					{
						this.CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
					}
				}
			}
			long position2 = workFile.baseStream_.Position;
			update.Entry.CompressedSize = position2 - position;
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x001450A4 File Offset: 0x001432A4
		private void CopyEntryDirect(ZipFile workFile, ZipFile.ZipUpdate update, ref long destinationPosition)
		{
			bool flag = update.Entry.Offset == destinationPosition;
			if (!flag)
			{
				this.baseStream_.Position = destinationPosition;
				workFile.WriteLocalEntryHeader(update);
				destinationPosition = this.baseStream_.Position;
			}
			long num = 0L;
			long num2 = update.Entry.Offset + 26L;
			this.baseStream_.Seek(num2, SeekOrigin.Begin);
			uint num3 = (uint)this.ReadLEUshort();
			uint num4 = (uint)this.ReadLEUshort();
			num = this.baseStream_.Position + (long)((ulong)num3) + (long)((ulong)num4);
			if (!flag)
			{
				if (update.Entry.CompressedSize > 0L)
				{
					this.CopyEntryDataDirect(update, this.baseStream_, false, ref destinationPosition, ref num);
				}
				this.CopyDescriptorBytesDirect(update, this.baseStream_, ref destinationPosition, num);
				return;
			}
			if (update.OffsetBasedSize != -1L)
			{
				destinationPosition += update.OffsetBasedSize;
				return;
			}
			destinationPosition += num - num2 + 26L;
			destinationPosition += update.Entry.CompressedSize;
			this.baseStream_.Seek(destinationPosition, SeekOrigin.Begin);
			bool includingSignature = this.ReadLEUint() == 134695760U;
			destinationPosition += (long)this.GetDescriptorSize(update, includingSignature);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x001451B8 File Offset: 0x001433B8
		private void CopyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			if (update.Entry.CompressedSize > 0L)
			{
				long offset = update.Entry.Offset + 26L;
				this.baseStream_.Seek(offset, SeekOrigin.Begin);
				uint num = (uint)this.ReadLEUshort();
				uint num2 = (uint)this.ReadLEUshort();
				this.baseStream_.Seek((long)((ulong)(num + num2)), SeekOrigin.Current);
				this.CopyBytes(update, workFile.baseStream_, this.baseStream_, update.Entry.CompressedSize, false);
			}
			this.CopyDescriptorBytes(update, workFile.baseStream_, this.baseStream_);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0014524A File Offset: 0x0014344A
		private void Reopen(Stream source)
		{
			this.isNewArchive_ = false;
			if (source == null)
			{
				throw new ZipException("Failed to reopen archive - no source");
			}
			this.baseStream_ = source;
			this.ReadEntries();
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0014526F File Offset: 0x0014346F
		private void Reopen()
		{
			if (this.Name == null)
			{
				throw new InvalidOperationException("Name is not known cannot Reopen");
			}
			this.Reopen(File.Open(this.Name, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00145298 File Offset: 0x00143498
		private void UpdateCommentOnly()
		{
			long length = this.baseStream_.Length;
			ZipHelperStream zipHelperStream;
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				zipHelperStream = new ZipHelperStream(this.archiveStorage_.MakeTemporaryCopy(this.baseStream_))
				{
					IsStreamOwner = true
				};
				this.baseStream_.Dispose();
				this.baseStream_ = null;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				this.baseStream_ = this.archiveStorage_.OpenForDirectUpdate(this.baseStream_);
				zipHelperStream = new ZipHelperStream(this.baseStream_);
			}
			else
			{
				this.baseStream_.Dispose();
				this.baseStream_ = null;
				zipHelperStream = new ZipHelperStream(this.Name);
			}
			using (zipHelperStream)
			{
				if (zipHelperStream.LocateBlockWithSignature(101010256, length, 22, 65535) < 0L)
				{
					throw new ZipException("Cannot find central directory");
				}
				zipHelperStream.Position += 16L;
				byte[] rawComment = this.newComment_.RawComment;
				zipHelperStream.WriteLEShort(rawComment.Length);
				zipHelperStream.Write(rawComment, 0, rawComment.Length);
				zipHelperStream.SetLength(zipHelperStream.Position);
			}
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
				return;
			}
			this.ReadEntries();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x001453E4 File Offset: 0x001435E4
		private void RunUpdates()
		{
			long num = 0L;
			long length = 0L;
			bool flag = false;
			long position = 0L;
			ZipFile zipFile;
			if (this.IsNewArchive)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
				this.updates_.Sort(new ZipFile.UpdateComparer());
			}
			else
			{
				zipFile = ZipFile.Create(this.archiveStorage_.GetTemporaryOutput());
				zipFile.UseZip64 = this.UseZip64;
				if (this.key != null)
				{
					zipFile.key = (byte[])this.key.Clone();
				}
			}
			try
			{
				foreach (ZipFile.ZipUpdate zipUpdate in this.updates_)
				{
					if (zipUpdate != null)
					{
						switch (zipUpdate.Command)
						{
						case ZipFile.UpdateCommand.Copy:
							if (flag)
							{
								this.CopyEntryDirect(zipFile, zipUpdate, ref position);
							}
							else
							{
								this.CopyEntry(zipFile, zipUpdate);
							}
							break;
						case ZipFile.UpdateCommand.Modify:
							this.ModifyEntry(zipFile, zipUpdate);
							break;
						case ZipFile.UpdateCommand.Add:
							if (!this.IsNewArchive && flag)
							{
								zipFile.baseStream_.Position = position;
							}
							this.AddEntry(zipFile, zipUpdate);
							if (flag)
							{
								position = zipFile.baseStream_.Position;
							}
							break;
						}
					}
				}
				if (!this.IsNewArchive && flag)
				{
					zipFile.baseStream_.Position = position;
				}
				long position2 = zipFile.baseStream_.Position;
				foreach (ZipFile.ZipUpdate zipUpdate2 in this.updates_)
				{
					if (zipUpdate2 != null)
					{
						num += (long)zipFile.WriteCentralDirectoryHeader(zipUpdate2.OutEntry);
					}
				}
				byte[] comment = (this.newComment_ != null) ? this.newComment_.RawComment : ZipStrings.ConvertToArray(this.comment_);
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(zipFile.baseStream_))
				{
					zipHelperStream.WriteEndOfCentralDirectory(this.updateCount_, num, position2, comment);
				}
				length = zipFile.baseStream_.Position;
				foreach (ZipFile.ZipUpdate zipUpdate3 in this.updates_)
				{
					if (zipUpdate3 != null)
					{
						if (zipUpdate3.CrcPatchOffset > 0L && zipUpdate3.OutEntry.CompressedSize > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.CrcPatchOffset;
							zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Crc);
						}
						if (zipUpdate3.SizePatchOffset > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.SizePatchOffset;
							if (zipUpdate3.OutEntry.LocalHeaderRequiresZip64)
							{
								zipFile.WriteLeLong(zipUpdate3.OutEntry.Size);
								zipFile.WriteLeLong(zipUpdate3.OutEntry.CompressedSize);
							}
							else
							{
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.CompressedSize);
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Size);
							}
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
				zipFile.baseStream_.SetLength(length);
				zipFile.baseStream_.Flush();
				this.isNewArchive_ = false;
				this.ReadEntries();
				return;
			}
			this.baseStream_.Dispose();
			this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x001457F0 File Offset: 0x001439F0
		private void CheckUpdating()
		{
			if (this.updates_ == null)
			{
				throw new InvalidOperationException("BeginUpdate has not been called");
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x00145805 File Offset: 0x00143A05
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x00145810 File Offset: 0x00143A10
		private void DisposeInternal(bool disposing)
		{
			if (!this.isDisposed_)
			{
				this.isDisposed_ = true;
				this.entries_ = new ZipEntry[0];
				if (this.IsStreamOwner && this.baseStream_ != null)
				{
					Stream obj = this.baseStream_;
					lock (obj)
					{
						this.baseStream_.Dispose();
					}
				}
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x00145888 File Offset: 0x00143A88
		protected virtual void Dispose(bool disposing)
		{
			this.DisposeInternal(disposing);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00145894 File Offset: 0x00143A94
		private ushort ReadLEUshort()
		{
			int num = this.baseStream_.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			int num2 = this.baseStream_.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			return (ushort)num | (ushort)(num2 << 8);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x001458DD File Offset: 0x00143ADD
		private uint ReadLEUint()
		{
			return (uint)((int)this.ReadLEUshort() | (int)this.ReadLEUshort() << 16);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x001458EF File Offset: 0x00143AEF
		private ulong ReadLEUlong()
		{
			return (ulong)this.ReadLEUint() | (ulong)this.ReadLEUint() << 32;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x00145904 File Offset: 0x00143B04
		private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long result;
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
			{
				result = zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
			}
			return result;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x00145948 File Offset: 0x00143B48
		private void ReadEntries()
		{
			if (!this.baseStream_.CanSeek)
			{
				throw new ZipException("ZipFile stream must be seekable");
			}
			long num = this.LocateBlockWithSignature(101010256, this.baseStream_.Length, 22, 65535);
			if (num < 0L)
			{
				throw new ZipException("Cannot find central directory");
			}
			int num2 = (int)this.ReadLEUshort();
			ushort num3 = this.ReadLEUshort();
			ulong num4 = (ulong)this.ReadLEUshort();
			ulong num5 = (ulong)this.ReadLEUshort();
			ulong num6 = (ulong)this.ReadLEUint();
			long num7 = (long)((ulong)this.ReadLEUint());
			uint num8 = (uint)this.ReadLEUshort();
			if (num8 > 0U)
			{
				byte[] array = new byte[num8];
				StreamUtils.ReadFully(this.baseStream_, array);
				this.comment_ = ZipStrings.ConvertToString(array);
			}
			else
			{
				this.comment_ = string.Empty;
			}
			bool flag = false;
			bool flag2 = false;
			if (num2 == 65535 || num3 == 65535 || num4 == 65535UL || num5 == 65535UL || num6 == (ulong)-1 || num7 == (long)((ulong)-1))
			{
				flag2 = true;
			}
			if (this.LocateBlockWithSignature(117853008, num - 4L, 20, 0) < 0L)
			{
				if (flag2)
				{
					throw new ZipException("Cannot find Zip64 locator");
				}
			}
			else
			{
				flag = true;
				this.ReadLEUint();
				ulong num9 = this.ReadLEUlong();
				this.ReadLEUint();
				this.baseStream_.Position = (long)num9;
				if ((ulong)this.ReadLEUint() != 101075792UL)
				{
					throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num9));
				}
				this.ReadLEUlong();
				this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUint();
				this.ReadLEUint();
				num4 = this.ReadLEUlong();
				num5 = this.ReadLEUlong();
				num6 = this.ReadLEUlong();
				num7 = (long)this.ReadLEUlong();
			}
			this.entries_ = new ZipEntry[num4];
			if (!flag && num7 < num - (long)(4UL + num6))
			{
				this.offsetOfFirstEntry = num - (long)(4UL + num6 + (ulong)num7);
				if (this.offsetOfFirstEntry <= 0L)
				{
					throw new ZipException("Invalid embedded zip archive");
				}
			}
			this.baseStream_.Seek(this.offsetOfFirstEntry + num7, SeekOrigin.Begin);
			for (ulong num10 = 0UL; num10 < num4; num10 += 1UL)
			{
				if (this.ReadLEUint() != 33639248U)
				{
					throw new ZipException("Wrong Central Directory signature");
				}
				int madeByInfo = (int)this.ReadLEUshort();
				int versionRequiredToExtract = (int)this.ReadLEUshort();
				int num11 = (int)this.ReadLEUshort();
				int method = (int)this.ReadLEUshort();
				uint num12 = this.ReadLEUint();
				uint num13 = this.ReadLEUint();
				long num14 = (long)((ulong)this.ReadLEUint());
				long num15 = (long)((ulong)this.ReadLEUint());
				int num16 = (int)this.ReadLEUshort();
				int num17 = (int)this.ReadLEUshort();
				int num18 = (int)this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUshort();
				uint externalFileAttributes = this.ReadLEUint();
				long offset = (long)((ulong)this.ReadLEUint());
				byte[] array2 = new byte[Math.Max(num16, num18)];
				StreamUtils.ReadFully(this.baseStream_, array2, 0, num16);
				ZipEntry zipEntry = new ZipEntry(ZipStrings.ConvertToStringExt(num11, array2, num16), versionRequiredToExtract, madeByInfo, (CompressionMethod)method)
				{
					Crc = (long)((ulong)num13 & (ulong)-1),
					Size = (num15 & (long)((ulong)-1)),
					CompressedSize = (num14 & (long)((ulong)-1)),
					Flags = num11,
					DosTime = (long)((ulong)num12),
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
					zipEntry.CryptoCheckValue = (byte)(num12 >> 8 & 255U);
				}
				if (num17 > 0)
				{
					byte[] array3 = new byte[num17];
					StreamUtils.ReadFully(this.baseStream_, array3);
					zipEntry.ExtraData = array3;
				}
				zipEntry.ProcessExtraData(false);
				if (num18 > 0)
				{
					StreamUtils.ReadFully(this.baseStream_, array2, 0, num18);
					zipEntry.Comment = ZipStrings.ConvertToStringExt(num11, array2, num18);
				}
				this.entries_[(int)(checked((IntPtr)num10))] = zipEntry;
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x00145CFB File Offset: 0x00143EFB
		private long LocateEntry(ZipEntry entry)
		{
			return this.TestLocalHeader(entry, ZipFile.HeaderTest.Extract);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x00145D08 File Offset: 0x00143F08
		private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream;
			if (entry.CompressionMethodForHeader == CompressionMethod.WinZipAES)
			{
				if (entry.Version < 51)
				{
					throw new ZipException("Decryption method not supported");
				}
				this.OnKeysRequired(entry.Name);
				if (this.rawPassword_ == null)
				{
					throw new ZipException("No password available for AES encrypted stream");
				}
				int aessaltLen = entry.AESSaltLen;
				byte[] array = new byte[aessaltLen];
				int num = StreamUtils.ReadRequestedBytes(baseStream, array, 0, aessaltLen);
				if (num != aessaltLen)
				{
					throw new ZipException(string.Concat(new object[]
					{
						"AES Salt expected ",
						aessaltLen,
						" got ",
						num
					}));
				}
				byte[] array2 = new byte[2];
				StreamUtils.ReadFully(baseStream, array2);
				int blockSize = entry.AESKeySize / 8;
				ZipAESTransform zipAESTransform = new ZipAESTransform(this.rawPassword_, array, blockSize, false);
				byte[] pwdVerifier = zipAESTransform.PwdVerifier;
				if (pwdVerifier[0] != array2[0] || pwdVerifier[1] != array2[1])
				{
					throw new ZipException("Invalid password for AES");
				}
				cryptoStream = new ZipAESStream(baseStream, zipAESTransform, CryptoStreamMode.Read);
			}
			else
			{
				if (entry.Version >= 50 && (entry.Flags & 64) != 0)
				{
					throw new ZipException("Decryption method not supported");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(baseStream, pkzipClassicManaged.CreateDecryptor(this.key, null), CryptoStreamMode.Read);
				ZipFile.CheckClassicPassword(cryptoStream, entry);
			}
			return cryptoStream;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x00145E70 File Offset: 0x00144070
		private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream = null;
			if (entry.Version < 50 || (entry.Flags & 64) == 0)
			{
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(new ZipFile.UncompressedStream(baseStream), pkzipClassicManaged.CreateEncryptor(this.key, null), CryptoStreamMode.Write);
				if (entry.Crc < 0L || (entry.Flags & 8) != 0)
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
				}
				else
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.Crc);
				}
			}
			return cryptoStream;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00145F08 File Offset: 0x00144108
		private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
		{
			byte[] array = new byte[12];
			StreamUtils.ReadFully(classicCryptoStream, array);
			if (array[11] != entry.CryptoCheckValue)
			{
				throw new ZipException("Invalid password");
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00145F3C File Offset: 0x0014413C
		private static void WriteEncryptionHeader(Stream stream, long crcValue)
		{
			byte[] array = new byte[12];
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				rngcryptoServiceProvider.GetBytes(array);
			}
			array[11] = (byte)(crcValue >> 24);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x04002713 RID: 10003
		public ZipFile.KeysRequiredEventHandler KeysRequired;

		// Token: 0x04002714 RID: 10004
		private const int DefaultBufferSize = 4096;

		// Token: 0x04002715 RID: 10005
		private bool isDisposed_;

		// Token: 0x04002716 RID: 10006
		private string name_;

		// Token: 0x04002717 RID: 10007
		private string comment_;

		// Token: 0x04002718 RID: 10008
		private string rawPassword_;

		// Token: 0x04002719 RID: 10009
		private Stream baseStream_;

		// Token: 0x0400271A RID: 10010
		private bool isStreamOwner;

		// Token: 0x0400271B RID: 10011
		private long offsetOfFirstEntry;

		// Token: 0x0400271C RID: 10012
		private ZipEntry[] entries_;

		// Token: 0x0400271D RID: 10013
		private byte[] key;

		// Token: 0x0400271E RID: 10014
		private bool isNewArchive_;

		// Token: 0x0400271F RID: 10015
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x04002720 RID: 10016
		private List<ZipFile.ZipUpdate> updates_;

		// Token: 0x04002721 RID: 10017
		private long updateCount_;

		// Token: 0x04002722 RID: 10018
		private Dictionary<string, int> updateIndex_;

		// Token: 0x04002723 RID: 10019
		private IArchiveStorage archiveStorage_;

		// Token: 0x04002724 RID: 10020
		private IDynamicDataSource updateDataSource_;

		// Token: 0x04002725 RID: 10021
		private bool contentsEdited_;

		// Token: 0x04002726 RID: 10022
		private int bufferSize_ = 4096;

		// Token: 0x04002727 RID: 10023
		private byte[] copyBuffer_;

		// Token: 0x04002728 RID: 10024
		private ZipFile.ZipString newComment_;

		// Token: 0x04002729 RID: 10025
		private bool commentEdited_;

		// Token: 0x0400272A RID: 10026
		private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

		// Token: 0x02001483 RID: 5251
		// (Invoke) Token: 0x060080F4 RID: 33012
		public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

		// Token: 0x02001484 RID: 5252
		[Flags]
		private enum HeaderTest
		{
			// Token: 0x04006C4A RID: 27722
			Extract = 1,
			// Token: 0x04006C4B RID: 27723
			Header = 2
		}

		// Token: 0x02001485 RID: 5253
		private enum UpdateCommand
		{
			// Token: 0x04006C4D RID: 27725
			Copy,
			// Token: 0x04006C4E RID: 27726
			Modify,
			// Token: 0x04006C4F RID: 27727
			Add
		}

		// Token: 0x02001486 RID: 5254
		private class UpdateComparer : IComparer<ZipFile.ZipUpdate>
		{
			// Token: 0x060080F7 RID: 33015 RVA: 0x002D797C File Offset: 0x002D5B7C
			public int Compare(ZipFile.ZipUpdate x, ZipFile.ZipUpdate y)
			{
				int num;
				if (x == null)
				{
					if (y == null)
					{
						num = 0;
					}
					else
					{
						num = -1;
					}
				}
				else if (y == null)
				{
					num = 1;
				}
				else
				{
					int num2 = (x.Command == ZipFile.UpdateCommand.Copy || x.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1;
					int num3 = (y.Command == ZipFile.UpdateCommand.Copy || y.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1;
					num = num2 - num3;
					if (num == 0)
					{
						long num4 = x.Entry.Offset - y.Entry.Offset;
						if (num4 < 0L)
						{
							num = -1;
						}
						else if (num4 == 0L)
						{
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x02001487 RID: 5255
		private class ZipUpdate
		{
			// Token: 0x060080F9 RID: 33017 RVA: 0x002D79FA File Offset: 0x002D5BFA
			public ZipUpdate(string fileName, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.filename_ = fileName;
			}

			// Token: 0x060080FA RID: 33018 RVA: 0x002D7A30 File Offset: 0x002D5C30
			[Obsolete]
			public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName)
				{
					CompressionMethod = compressionMethod
				};
				this.filename_ = fileName;
			}

			// Token: 0x060080FB RID: 33019 RVA: 0x002D7A7C File Offset: 0x002D5C7C
			[Obsolete]
			public ZipUpdate(string fileName, string entryName) : this(fileName, entryName, CompressionMethod.Deflated)
			{
			}

			// Token: 0x060080FC RID: 33020 RVA: 0x002D7A88 File Offset: 0x002D5C88
			[Obsolete]
			public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName)
				{
					CompressionMethod = compressionMethod
				};
				this.dataSource_ = dataSource;
			}

			// Token: 0x060080FD RID: 33021 RVA: 0x002D7AD4 File Offset: 0x002D5CD4
			public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.dataSource_ = dataSource;
			}

			// Token: 0x060080FE RID: 33022 RVA: 0x002D7B09 File Offset: 0x002D5D09
			public ZipUpdate(ZipEntry original, ZipEntry updated)
			{
				throw new ZipException("Modify not currently supported");
			}

			// Token: 0x060080FF RID: 33023 RVA: 0x002D7B33 File Offset: 0x002D5D33
			public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
			{
				this.command_ = command;
				this.entry_ = (ZipEntry)entry.Clone();
			}

			// Token: 0x06008100 RID: 33024 RVA: 0x002D7B6B File Offset: 0x002D5D6B
			public ZipUpdate(ZipEntry entry) : this(ZipFile.UpdateCommand.Copy, entry)
			{
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06008101 RID: 33025 RVA: 0x002D7B75 File Offset: 0x002D5D75
			public ZipEntry Entry
			{
				get
				{
					return this.entry_;
				}
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06008102 RID: 33026 RVA: 0x002D7B7D File Offset: 0x002D5D7D
			public ZipEntry OutEntry
			{
				get
				{
					if (this.outEntry_ == null)
					{
						this.outEntry_ = (ZipEntry)this.entry_.Clone();
					}
					return this.outEntry_;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06008103 RID: 33027 RVA: 0x002D7BA3 File Offset: 0x002D5DA3
			public ZipFile.UpdateCommand Command
			{
				get
				{
					return this.command_;
				}
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06008104 RID: 33028 RVA: 0x002D7BAB File Offset: 0x002D5DAB
			public string Filename
			{
				get
				{
					return this.filename_;
				}
			}

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x06008105 RID: 33029 RVA: 0x002D7BB3 File Offset: 0x002D5DB3
			// (set) Token: 0x06008106 RID: 33030 RVA: 0x002D7BBB File Offset: 0x002D5DBB
			public long SizePatchOffset
			{
				get
				{
					return this.sizePatchOffset_;
				}
				set
				{
					this.sizePatchOffset_ = value;
				}
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06008107 RID: 33031 RVA: 0x002D7BC4 File Offset: 0x002D5DC4
			// (set) Token: 0x06008108 RID: 33032 RVA: 0x002D7BCC File Offset: 0x002D5DCC
			public long CrcPatchOffset
			{
				get
				{
					return this.crcPatchOffset_;
				}
				set
				{
					this.crcPatchOffset_ = value;
				}
			}

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x06008109 RID: 33033 RVA: 0x002D7BD5 File Offset: 0x002D5DD5
			// (set) Token: 0x0600810A RID: 33034 RVA: 0x002D7BDD File Offset: 0x002D5DDD
			public long OffsetBasedSize
			{
				get
				{
					return this._offsetBasedSize;
				}
				set
				{
					this._offsetBasedSize = value;
				}
			}

			// Token: 0x0600810B RID: 33035 RVA: 0x002D7BE8 File Offset: 0x002D5DE8
			public Stream GetSource()
			{
				Stream result = null;
				if (this.dataSource_ != null)
				{
					result = this.dataSource_.GetSource();
				}
				return result;
			}

			// Token: 0x04006C50 RID: 27728
			private ZipEntry entry_;

			// Token: 0x04006C51 RID: 27729
			private ZipEntry outEntry_;

			// Token: 0x04006C52 RID: 27730
			private readonly ZipFile.UpdateCommand command_;

			// Token: 0x04006C53 RID: 27731
			private IStaticDataSource dataSource_;

			// Token: 0x04006C54 RID: 27732
			private readonly string filename_;

			// Token: 0x04006C55 RID: 27733
			private long sizePatchOffset_ = -1L;

			// Token: 0x04006C56 RID: 27734
			private long crcPatchOffset_ = -1L;

			// Token: 0x04006C57 RID: 27735
			private long _offsetBasedSize = -1L;
		}

		// Token: 0x02001488 RID: 5256
		private class ZipString
		{
			// Token: 0x0600810C RID: 33036 RVA: 0x002D7C0C File Offset: 0x002D5E0C
			public ZipString(string comment)
			{
				this.comment_ = comment;
				this.isSourceString_ = true;
			}

			// Token: 0x0600810D RID: 33037 RVA: 0x002D7C22 File Offset: 0x002D5E22
			public ZipString(byte[] rawString)
			{
				this.rawComment_ = rawString;
			}

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x0600810E RID: 33038 RVA: 0x002D7C31 File Offset: 0x002D5E31
			public bool IsSourceString
			{
				get
				{
					return this.isSourceString_;
				}
			}

			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x0600810F RID: 33039 RVA: 0x002D7C39 File Offset: 0x002D5E39
			public int RawLength
			{
				get
				{
					this.MakeBytesAvailable();
					return this.rawComment_.Length;
				}
			}

			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x06008110 RID: 33040 RVA: 0x002D7C49 File Offset: 0x002D5E49
			public byte[] RawComment
			{
				get
				{
					this.MakeBytesAvailable();
					return (byte[])this.rawComment_.Clone();
				}
			}

			// Token: 0x06008111 RID: 33041 RVA: 0x002D7C61 File Offset: 0x002D5E61
			public void Reset()
			{
				if (this.isSourceString_)
				{
					this.rawComment_ = null;
					return;
				}
				this.comment_ = null;
			}

			// Token: 0x06008112 RID: 33042 RVA: 0x002D7C7A File Offset: 0x002D5E7A
			private void MakeTextAvailable()
			{
				if (this.comment_ == null)
				{
					this.comment_ = ZipStrings.ConvertToString(this.rawComment_);
				}
			}

			// Token: 0x06008113 RID: 33043 RVA: 0x002D7C95 File Offset: 0x002D5E95
			private void MakeBytesAvailable()
			{
				if (this.rawComment_ == null)
				{
					this.rawComment_ = ZipStrings.ConvertToArray(this.comment_);
				}
			}

			// Token: 0x06008114 RID: 33044 RVA: 0x002D7CB0 File Offset: 0x002D5EB0
			public static implicit operator string(ZipFile.ZipString zipString)
			{
				zipString.MakeTextAvailable();
				return zipString.comment_;
			}

			// Token: 0x04006C58 RID: 27736
			private string comment_;

			// Token: 0x04006C59 RID: 27737
			private byte[] rawComment_;

			// Token: 0x04006C5A RID: 27738
			private readonly bool isSourceString_;
		}

		// Token: 0x02001489 RID: 5257
		private class ZipEntryEnumerator : IEnumerator
		{
			// Token: 0x06008115 RID: 33045 RVA: 0x002D7CBE File Offset: 0x002D5EBE
			public ZipEntryEnumerator(ZipEntry[] entries)
			{
				this.array = entries;
			}

			// Token: 0x17000AB0 RID: 2736
			// (get) Token: 0x06008116 RID: 33046 RVA: 0x002D7CD4 File Offset: 0x002D5ED4
			public object Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x06008117 RID: 33047 RVA: 0x002D7CE3 File Offset: 0x002D5EE3
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x06008118 RID: 33048 RVA: 0x002D7CEC File Offset: 0x002D5EEC
			public bool MoveNext()
			{
				int num = this.index + 1;
				this.index = num;
				return num < this.array.Length;
			}

			// Token: 0x04006C5B RID: 27739
			private ZipEntry[] array;

			// Token: 0x04006C5C RID: 27740
			private int index = -1;
		}

		// Token: 0x0200148A RID: 5258
		private class UncompressedStream : Stream
		{
			// Token: 0x06008119 RID: 33049 RVA: 0x002D7D14 File Offset: 0x002D5F14
			public UncompressedStream(Stream baseStream)
			{
				this.baseStream_ = baseStream;
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x0600811A RID: 33050 RVA: 0x0000280F File Offset: 0x00000A0F
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600811B RID: 33051 RVA: 0x002D7D23 File Offset: 0x002D5F23
			public override void Flush()
			{
				this.baseStream_.Flush();
			}

			// Token: 0x17000AB2 RID: 2738
			// (get) Token: 0x0600811C RID: 33052 RVA: 0x002D7D30 File Offset: 0x002D5F30
			public override bool CanWrite
			{
				get
				{
					return this.baseStream_.CanWrite;
				}
			}

			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x0600811D RID: 33053 RVA: 0x0000280F File Offset: 0x00000A0F
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x0600811E RID: 33054 RVA: 0x0023395F File Offset: 0x00231B5F
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x0600811F RID: 33055 RVA: 0x002D7D3D File Offset: 0x002D5F3D
			// (set) Token: 0x06008120 RID: 33056 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
			public override long Position
			{
				get
				{
					return this.baseStream_.Position;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06008121 RID: 33057 RVA: 0x0000280F File Offset: 0x00000A0F
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06008122 RID: 33058 RVA: 0x0023395F File Offset: 0x00231B5F
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06008123 RID: 33059 RVA: 0x00004095 File Offset: 0x00002295
			public override void SetLength(long value)
			{
			}

			// Token: 0x06008124 RID: 33060 RVA: 0x002D7D4A File Offset: 0x002D5F4A
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.baseStream_.Write(buffer, offset, count);
			}

			// Token: 0x04006C5D RID: 27741
			private readonly Stream baseStream_;
		}

		// Token: 0x0200148B RID: 5259
		private class PartialInputStream : Stream
		{
			// Token: 0x06008125 RID: 33061 RVA: 0x002D7D5A File Offset: 0x002D5F5A
			public PartialInputStream(ZipFile zipFile, long start, long length)
			{
				this.start_ = start;
				this.length_ = length;
				this.zipFile_ = zipFile;
				this.baseStream_ = this.zipFile_.baseStream_;
				this.readPos_ = start;
				this.end_ = start + length;
			}

			// Token: 0x06008126 RID: 33062 RVA: 0x002D7D98 File Offset: 0x002D5F98
			public override int ReadByte()
			{
				if (this.readPos_ >= this.end_)
				{
					return -1;
				}
				Stream obj = this.baseStream_;
				int result;
				lock (obj)
				{
					Stream stream = this.baseStream_;
					long num = this.readPos_;
					this.readPos_ = num + 1L;
					stream.Seek(num, SeekOrigin.Begin);
					result = this.baseStream_.ReadByte();
				}
				return result;
			}

			// Token: 0x06008127 RID: 33063 RVA: 0x002D7E10 File Offset: 0x002D6010
			public override int Read(byte[] buffer, int offset, int count)
			{
				Stream obj = this.baseStream_;
				int result;
				lock (obj)
				{
					if ((long)count > this.end_ - this.readPos_)
					{
						count = (int)(this.end_ - this.readPos_);
						if (count == 0)
						{
							return 0;
						}
					}
					if (this.baseStream_.Position != this.readPos_)
					{
						this.baseStream_.Seek(this.readPos_, SeekOrigin.Begin);
					}
					int num = this.baseStream_.Read(buffer, offset, count);
					if (num > 0)
					{
						this.readPos_ += (long)num;
					}
					result = num;
				}
				return result;
			}

			// Token: 0x06008128 RID: 33064 RVA: 0x002B974C File Offset: 0x002B794C
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06008129 RID: 33065 RVA: 0x002B974C File Offset: 0x002B794C
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600812A RID: 33066 RVA: 0x002D7EC0 File Offset: 0x002D60C0
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = this.readPos_;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = this.start_ + offset;
					break;
				case SeekOrigin.Current:
					num = this.readPos_ + offset;
					break;
				case SeekOrigin.End:
					num = this.end_ + offset;
					break;
				}
				if (num < this.start_)
				{
					throw new ArgumentException("Negative position is invalid");
				}
				if (num > this.end_)
				{
					throw new IOException("Cannot seek past end");
				}
				this.readPos_ = num;
				return this.readPos_;
			}

			// Token: 0x0600812B RID: 33067 RVA: 0x00004095 File Offset: 0x00002295
			public override void Flush()
			{
			}

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x0600812C RID: 33068 RVA: 0x002D7F3C File Offset: 0x002D613C
			// (set) Token: 0x0600812D RID: 33069 RVA: 0x002D7F4C File Offset: 0x002D614C
			public override long Position
			{
				get
				{
					return this.readPos_ - this.start_;
				}
				set
				{
					long num = this.start_ + value;
					if (num < this.start_)
					{
						throw new ArgumentException("Negative position is invalid");
					}
					if (num > this.end_)
					{
						throw new InvalidOperationException("Cannot seek past end");
					}
					this.readPos_ = num;
				}
			}

			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x0600812E RID: 33070 RVA: 0x002D7F91 File Offset: 0x002D6191
			public override long Length
			{
				get
				{
					return this.length_;
				}
			}

			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x0600812F RID: 33071 RVA: 0x0000280F File Offset: 0x00000A0F
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x06008130 RID: 33072 RVA: 0x00024C5F File Offset: 0x00022E5F
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06008131 RID: 33073 RVA: 0x00024C5F File Offset: 0x00022E5F
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06008132 RID: 33074 RVA: 0x002D7F99 File Offset: 0x002D6199
			public override bool CanTimeout
			{
				get
				{
					return this.baseStream_.CanTimeout;
				}
			}

			// Token: 0x04006C5E RID: 27742
			private ZipFile zipFile_;

			// Token: 0x04006C5F RID: 27743
			private Stream baseStream_;

			// Token: 0x04006C60 RID: 27744
			private readonly long start_;

			// Token: 0x04006C61 RID: 27745
			private readonly long length_;

			// Token: 0x04006C62 RID: 27746
			private long readPos_;

			// Token: 0x04006C63 RID: 27747
			private readonly long end_;
		}
	}
}
