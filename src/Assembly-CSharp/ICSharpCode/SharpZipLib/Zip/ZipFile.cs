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
	// Token: 0x020007D8 RID: 2008
	public class ZipFile : IEnumerable, IDisposable
	{
		// Token: 0x0600332A RID: 13098 RVA: 0x0018F350 File Offset: 0x0018D550
		private void OnKeysRequired(string fileName)
		{
			if (this.KeysRequired != null)
			{
				KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, this.key);
				this.KeysRequired(this, keysRequiredEventArgs);
				this.key = keysRequiredEventArgs.Key;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x00025553 File Offset: 0x00023753
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x0002555B File Offset: 0x0002375B
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

		// Token: 0x170004AD RID: 1197
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x00025564 File Offset: 0x00023764
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x0002558F File Offset: 0x0002378F
		private bool HaveKeys
		{
			get
			{
				return this.key != null;
			}
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x0018F38C File Offset: 0x0018D58C
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

		// Token: 0x06003330 RID: 13104 RVA: 0x0002559A File Offset: 0x0002379A
		public ZipFile(FileStream file) : this(file, false)
		{
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x0018F40C File Offset: 0x0018D60C
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

		// Token: 0x06003332 RID: 13106 RVA: 0x000255A4 File Offset: 0x000237A4
		public ZipFile(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0018F4A4 File Offset: 0x0018D6A4
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

		// Token: 0x06003334 RID: 13108 RVA: 0x000255AE File Offset: 0x000237AE
		internal ZipFile()
		{
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0018F550 File Offset: 0x0018D750
		~ZipFile()
		{
			this.Dispose(false);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000255E6 File Offset: 0x000237E6
		public void Close()
		{
			this.DisposeInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x0018F580 File Offset: 0x0018D780
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

		// Token: 0x06003338 RID: 13112 RVA: 0x0018F5BC File Offset: 0x0018D7BC
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000255F5 File Offset: 0x000237F5
		// (set) Token: 0x0600333A RID: 13114 RVA: 0x000255FD File Offset: 0x000237FD
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x00025606 File Offset: 0x00023806
		public bool IsEmbeddedArchive
		{
			get
			{
				return this.offsetOfFirstEntry > 0L;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x00025612 File Offset: 0x00023812
		public bool IsNewArchive
		{
			get
			{
				return this.isNewArchive_;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x0002561A File Offset: 0x0002381A
		public string ZipFileComment
		{
			get
			{
				return this.comment_;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x00025622 File Offset: 0x00023822
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x0002562A File Offset: 0x0002382A
		[Obsolete("Use the Count property instead")]
		public int Size
		{
			get
			{
				return this.entries_.Length;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x00025634 File Offset: 0x00023834
		public long Count
		{
			get
			{
				return (long)this.entries_.Length;
			}
		}

		// Token: 0x170004B6 RID: 1206
		[IndexerName("EntryByIndex")]
		public ZipEntry this[int index]
		{
			get
			{
				return (ZipEntry)this.entries_[index].Clone();
			}
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x00025653 File Offset: 0x00023853
		public IEnumerator GetEnumerator()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			return new ZipFile.ZipEntryEnumerator(this.entries_);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0018F614 File Offset: 0x0018D814
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

		// Token: 0x06003344 RID: 13124 RVA: 0x0018F668 File Offset: 0x0018D868
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

		// Token: 0x06003345 RID: 13125 RVA: 0x0018F6AC File Offset: 0x0018D8AC
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

		// Token: 0x06003346 RID: 13126 RVA: 0x0018F734 File Offset: 0x0018D934
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

		// Token: 0x06003347 RID: 13127 RVA: 0x00025673 File Offset: 0x00023873
		public bool TestArchive(bool testData)
		{
			return this.TestArchive(testData, TestStrategy.FindFirstError, null);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0018F7F8 File Offset: 0x0018D9F8
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

		// Token: 0x06003349 RID: 13129 RVA: 0x0018FAF8 File Offset: 0x0018DCF8
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x0002567E File Offset: 0x0002387E
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x0002568B File Offset: 0x0002388B
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x00025699 File Offset: 0x00023899
		// (set) Token: 0x0600334D RID: 13133 RVA: 0x000256A1 File Offset: 0x000238A1
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000256B9 File Offset: 0x000238B9
		// (set) Token: 0x0600334F RID: 13135 RVA: 0x000256C1 File Offset: 0x000238C1
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

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000256F2 File Offset: 0x000238F2
		public bool IsUpdating
		{
			get
			{
				return this.updates_ != null;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000256FD File Offset: 0x000238FD
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x00025705 File Offset: 0x00023905
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

		// Token: 0x06003353 RID: 13139 RVA: 0x0018FFEC File Offset: 0x0018E1EC
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

		// Token: 0x06003354 RID: 13140 RVA: 0x0002570E File Offset: 0x0002390E
		public void BeginUpdate(IArchiveStorage archiveStorage)
		{
			this.BeginUpdate(archiveStorage, new DynamicDiskDataSource());
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x0002571C File Offset: 0x0002391C
		public void BeginUpdate()
		{
			if (this.Name == null)
			{
				this.BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
				return;
			}
			this.BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x00190170 File Offset: 0x0018E370
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

		// Token: 0x06003357 RID: 13143 RVA: 0x00025748 File Offset: 0x00023948
		public void AbortUpdate()
		{
			this.PostUpdateCleanup();
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0019023C File Offset: 0x0018E43C
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

		// Token: 0x06003359 RID: 13145 RVA: 0x0019029C File Offset: 0x0018E49C
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

		// Token: 0x0600335A RID: 13146 RVA: 0x00190330 File Offset: 0x0018E530
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

		// Token: 0x0600335B RID: 13147 RVA: 0x0019039C File Offset: 0x0018E59C
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

		// Token: 0x0600335C RID: 13148 RVA: 0x00025750 File Offset: 0x00023950
		public void Add(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0002577E File Offset: 0x0002397E
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

		// Token: 0x0600335E RID: 13150 RVA: 0x000257BC File Offset: 0x000239BC
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

		// Token: 0x0600335F RID: 13151 RVA: 0x001903EC File Offset: 0x0018E5EC
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

		// Token: 0x06003360 RID: 13152 RVA: 0x00190444 File Offset: 0x0018E644
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

		// Token: 0x06003361 RID: 13153 RVA: 0x000257F9 File Offset: 0x000239F9
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

		// Token: 0x06003362 RID: 13154 RVA: 0x001904A4 File Offset: 0x0018E6A4
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

		// Token: 0x06003363 RID: 13155 RVA: 0x00190500 File Offset: 0x0018E700
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

		// Token: 0x06003364 RID: 13156 RVA: 0x00025837 File Offset: 0x00023A37
		private void CheckSupportedCompressionMethod(CompressionMethod compressionMethod)
		{
			if (compressionMethod != CompressionMethod.Deflated && compressionMethod != CompressionMethod.Stored && compressionMethod != CompressionMethod.BZip2)
			{
				throw new NotImplementedException("Compression method not supported");
			}
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0019053C File Offset: 0x0018E73C
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

		// Token: 0x06003366 RID: 13158 RVA: 0x001905B0 File Offset: 0x0018E7B0
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

		// Token: 0x06003367 RID: 13159 RVA: 0x00025850 File Offset: 0x00023A50
		private void WriteLEShort(int value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8 & 255));
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x0002587A File Offset: 0x00023A7A
		private void WriteLEUshort(ushort value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0002589E File Offset: 0x00023A9E
		private void WriteLEInt(int value)
		{
			this.WriteLEShort(value & 65535);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000258B7 File Offset: 0x00023AB7
		private void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000258D2 File Offset: 0x00023AD2
		private void WriteLeLong(long value)
		{
			this.WriteLEInt((int)(value & (long)((ulong)-1)));
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000258EA File Offset: 0x00023AEA
		private void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)-1));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x0019060C File Offset: 0x0018E80C
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

		// Token: 0x0600336E RID: 13166 RVA: 0x001908A0 File Offset: 0x0018EAA0
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

		// Token: 0x0600336F RID: 13167 RVA: 0x00025902 File Offset: 0x00023B02
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

		// Token: 0x06003370 RID: 13168 RVA: 0x00190B38 File Offset: 0x0018ED38
		private string GetTransformedFileName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformFile(name);
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x00190B58 File Offset: 0x0018ED58
		private string GetTransformedDirectoryName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformDirectory(name);
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x00025933 File Offset: 0x00023B33
		private byte[] GetBuffer()
		{
			if (this.copyBuffer_ == null)
			{
				this.copyBuffer_ = new byte[this.bufferSize_];
			}
			return this.copyBuffer_;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x00190B78 File Offset: 0x0018ED78
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

		// Token: 0x06003374 RID: 13172 RVA: 0x00190BF8 File Offset: 0x0018EDF8
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

		// Token: 0x06003375 RID: 13173 RVA: 0x00190CB4 File Offset: 0x0018EEB4
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

		// Token: 0x06003376 RID: 13174 RVA: 0x00190CFC File Offset: 0x0018EEFC
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

		// Token: 0x06003377 RID: 13175 RVA: 0x00190D88 File Offset: 0x0018EF88
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

		// Token: 0x06003378 RID: 13176 RVA: 0x00190E64 File Offset: 0x0018F064
		private int FindExistingUpdate(ZipEntry entry)
		{
			int result = -1;
			if (this.updateIndex_.ContainsKey(entry.Name))
			{
				result = this.updateIndex_[entry.Name];
			}
			return result;
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x00190E9C File Offset: 0x0018F09C
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

		// Token: 0x0600337A RID: 13178 RVA: 0x00190ED8 File Offset: 0x0018F0D8
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

		// Token: 0x0600337B RID: 13179 RVA: 0x00190F74 File Offset: 0x0018F174
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

		// Token: 0x0600337C RID: 13180 RVA: 0x001910B4 File Offset: 0x0018F2B4
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

		// Token: 0x0600337D RID: 13181 RVA: 0x00191160 File Offset: 0x0018F360
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

		// Token: 0x0600337E RID: 13182 RVA: 0x00191274 File Offset: 0x0018F474
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

		// Token: 0x0600337F RID: 13183 RVA: 0x00025954 File Offset: 0x00023B54
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

		// Token: 0x06003380 RID: 13184 RVA: 0x00025979 File Offset: 0x00023B79
		private void Reopen()
		{
			if (this.Name == null)
			{
				throw new InvalidOperationException("Name is not known cannot Reopen");
			}
			this.Reopen(File.Open(this.Name, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x00191308 File Offset: 0x0018F508
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

		// Token: 0x06003382 RID: 13186 RVA: 0x00191454 File Offset: 0x0018F654
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

		// Token: 0x06003383 RID: 13187 RVA: 0x000259A2 File Offset: 0x00023BA2
		private void CheckUpdating()
		{
			if (this.updates_ == null)
			{
				throw new InvalidOperationException("BeginUpdate has not been called");
			}
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000259B7 File Offset: 0x00023BB7
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00191860 File Offset: 0x0018FA60
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

		// Token: 0x06003386 RID: 13190 RVA: 0x000259BF File Offset: 0x00023BBF
		protected virtual void Dispose(bool disposing)
		{
			this.DisposeInternal(disposing);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x001918D8 File Offset: 0x0018FAD8
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

		// Token: 0x06003388 RID: 13192 RVA: 0x000259C8 File Offset: 0x00023BC8
		private uint ReadLEUint()
		{
			return (uint)((int)this.ReadLEUshort() | (int)this.ReadLEUshort() << 16);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000259DA File Offset: 0x00023BDA
		private ulong ReadLEUlong()
		{
			return (ulong)this.ReadLEUint() | (ulong)this.ReadLEUint() << 32;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00191924 File Offset: 0x0018FB24
		private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long result;
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
			{
				result = zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
			}
			return result;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x00191968 File Offset: 0x0018FB68
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

		// Token: 0x0600338C RID: 13196 RVA: 0x000259EE File Offset: 0x00023BEE
		private long LocateEntry(ZipEntry entry)
		{
			return this.TestLocalHeader(entry, ZipFile.HeaderTest.Extract);
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00191D1C File Offset: 0x0018FF1C
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

		// Token: 0x0600338E RID: 13198 RVA: 0x00191E84 File Offset: 0x00190084
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

		// Token: 0x0600338F RID: 13199 RVA: 0x00191F1C File Offset: 0x0019011C
		private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
		{
			byte[] array = new byte[12];
			StreamUtils.ReadFully(classicCryptoStream, array);
			if (array[11] != entry.CryptoCheckValue)
			{
				throw new ZipException("Invalid password");
			}
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x00191F50 File Offset: 0x00190150
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

		// Token: 0x04002F1A RID: 12058
		public ZipFile.KeysRequiredEventHandler KeysRequired;

		// Token: 0x04002F1B RID: 12059
		private const int DefaultBufferSize = 4096;

		// Token: 0x04002F1C RID: 12060
		private bool isDisposed_;

		// Token: 0x04002F1D RID: 12061
		private string name_;

		// Token: 0x04002F1E RID: 12062
		private string comment_;

		// Token: 0x04002F1F RID: 12063
		private string rawPassword_;

		// Token: 0x04002F20 RID: 12064
		private Stream baseStream_;

		// Token: 0x04002F21 RID: 12065
		private bool isStreamOwner;

		// Token: 0x04002F22 RID: 12066
		private long offsetOfFirstEntry;

		// Token: 0x04002F23 RID: 12067
		private ZipEntry[] entries_;

		// Token: 0x04002F24 RID: 12068
		private byte[] key;

		// Token: 0x04002F25 RID: 12069
		private bool isNewArchive_;

		// Token: 0x04002F26 RID: 12070
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x04002F27 RID: 12071
		private List<ZipFile.ZipUpdate> updates_;

		// Token: 0x04002F28 RID: 12072
		private long updateCount_;

		// Token: 0x04002F29 RID: 12073
		private Dictionary<string, int> updateIndex_;

		// Token: 0x04002F2A RID: 12074
		private IArchiveStorage archiveStorage_;

		// Token: 0x04002F2B RID: 12075
		private IDynamicDataSource updateDataSource_;

		// Token: 0x04002F2C RID: 12076
		private bool contentsEdited_;

		// Token: 0x04002F2D RID: 12077
		private int bufferSize_ = 4096;

		// Token: 0x04002F2E RID: 12078
		private byte[] copyBuffer_;

		// Token: 0x04002F2F RID: 12079
		private ZipFile.ZipString newComment_;

		// Token: 0x04002F30 RID: 12080
		private bool commentEdited_;

		// Token: 0x04002F31 RID: 12081
		private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

		// Token: 0x020007D9 RID: 2009
		// (Invoke) Token: 0x06003392 RID: 13202
		public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

		// Token: 0x020007DA RID: 2010
		[Flags]
		private enum HeaderTest
		{
			// Token: 0x04002F33 RID: 12083
			Extract = 1,
			// Token: 0x04002F34 RID: 12084
			Header = 2
		}

		// Token: 0x020007DB RID: 2011
		private enum UpdateCommand
		{
			// Token: 0x04002F36 RID: 12086
			Copy,
			// Token: 0x04002F37 RID: 12087
			Modify,
			// Token: 0x04002F38 RID: 12088
			Add
		}

		// Token: 0x020007DC RID: 2012
		private class UpdateComparer : IComparer<ZipFile.ZipUpdate>
		{
			// Token: 0x06003395 RID: 13205 RVA: 0x00191FA4 File Offset: 0x001901A4
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

		// Token: 0x020007DD RID: 2013
		private class ZipUpdate
		{
			// Token: 0x06003397 RID: 13207 RVA: 0x000259F8 File Offset: 0x00023BF8
			public ZipUpdate(string fileName, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.filename_ = fileName;
			}

			// Token: 0x06003398 RID: 13208 RVA: 0x00192024 File Offset: 0x00190224
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

			// Token: 0x06003399 RID: 13209 RVA: 0x00025A2D File Offset: 0x00023C2D
			[Obsolete]
			public ZipUpdate(string fileName, string entryName) : this(fileName, entryName, CompressionMethod.Deflated)
			{
			}

			// Token: 0x0600339A RID: 13210 RVA: 0x00192070 File Offset: 0x00190270
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

			// Token: 0x0600339B RID: 13211 RVA: 0x00025A38 File Offset: 0x00023C38
			public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.dataSource_ = dataSource;
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x00025A6D File Offset: 0x00023C6D
			public ZipUpdate(ZipEntry original, ZipEntry updated)
			{
				throw new ZipException("Modify not currently supported");
			}

			// Token: 0x0600339D RID: 13213 RVA: 0x00025A97 File Offset: 0x00023C97
			public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
			{
				this.command_ = command;
				this.entry_ = (ZipEntry)entry.Clone();
			}

			// Token: 0x0600339E RID: 13214 RVA: 0x00025ACF File Offset: 0x00023CCF
			public ZipUpdate(ZipEntry entry) : this(ZipFile.UpdateCommand.Copy, entry)
			{
			}

			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x0600339F RID: 13215 RVA: 0x00025AD9 File Offset: 0x00023CD9
			public ZipEntry Entry
			{
				get
				{
					return this.entry_;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x060033A0 RID: 13216 RVA: 0x00025AE1 File Offset: 0x00023CE1
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

			// Token: 0x170004BE RID: 1214
			// (get) Token: 0x060033A1 RID: 13217 RVA: 0x00025B07 File Offset: 0x00023D07
			public ZipFile.UpdateCommand Command
			{
				get
				{
					return this.command_;
				}
			}

			// Token: 0x170004BF RID: 1215
			// (get) Token: 0x060033A2 RID: 13218 RVA: 0x00025B0F File Offset: 0x00023D0F
			public string Filename
			{
				get
				{
					return this.filename_;
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x060033A3 RID: 13219 RVA: 0x00025B17 File Offset: 0x00023D17
			// (set) Token: 0x060033A4 RID: 13220 RVA: 0x00025B1F File Offset: 0x00023D1F
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

			// Token: 0x170004C1 RID: 1217
			// (get) Token: 0x060033A5 RID: 13221 RVA: 0x00025B28 File Offset: 0x00023D28
			// (set) Token: 0x060033A6 RID: 13222 RVA: 0x00025B30 File Offset: 0x00023D30
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

			// Token: 0x170004C2 RID: 1218
			// (get) Token: 0x060033A7 RID: 13223 RVA: 0x00025B39 File Offset: 0x00023D39
			// (set) Token: 0x060033A8 RID: 13224 RVA: 0x00025B41 File Offset: 0x00023D41
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

			// Token: 0x060033A9 RID: 13225 RVA: 0x001920BC File Offset: 0x001902BC
			public Stream GetSource()
			{
				Stream result = null;
				if (this.dataSource_ != null)
				{
					result = this.dataSource_.GetSource();
				}
				return result;
			}

			// Token: 0x04002F39 RID: 12089
			private ZipEntry entry_;

			// Token: 0x04002F3A RID: 12090
			private ZipEntry outEntry_;

			// Token: 0x04002F3B RID: 12091
			private readonly ZipFile.UpdateCommand command_;

			// Token: 0x04002F3C RID: 12092
			private IStaticDataSource dataSource_;

			// Token: 0x04002F3D RID: 12093
			private readonly string filename_;

			// Token: 0x04002F3E RID: 12094
			private long sizePatchOffset_ = -1L;

			// Token: 0x04002F3F RID: 12095
			private long crcPatchOffset_ = -1L;

			// Token: 0x04002F40 RID: 12096
			private long _offsetBasedSize = -1L;
		}

		// Token: 0x020007DE RID: 2014
		private class ZipString
		{
			// Token: 0x060033AA RID: 13226 RVA: 0x00025B4A File Offset: 0x00023D4A
			public ZipString(string comment)
			{
				this.comment_ = comment;
				this.isSourceString_ = true;
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x00025B60 File Offset: 0x00023D60
			public ZipString(byte[] rawString)
			{
				this.rawComment_ = rawString;
			}

			// Token: 0x170004C3 RID: 1219
			// (get) Token: 0x060033AC RID: 13228 RVA: 0x00025B6F File Offset: 0x00023D6F
			public bool IsSourceString
			{
				get
				{
					return this.isSourceString_;
				}
			}

			// Token: 0x170004C4 RID: 1220
			// (get) Token: 0x060033AD RID: 13229 RVA: 0x00025B77 File Offset: 0x00023D77
			public int RawLength
			{
				get
				{
					this.MakeBytesAvailable();
					return this.rawComment_.Length;
				}
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x060033AE RID: 13230 RVA: 0x00025B87 File Offset: 0x00023D87
			public byte[] RawComment
			{
				get
				{
					this.MakeBytesAvailable();
					return (byte[])this.rawComment_.Clone();
				}
			}

			// Token: 0x060033AF RID: 13231 RVA: 0x00025B9F File Offset: 0x00023D9F
			public void Reset()
			{
				if (this.isSourceString_)
				{
					this.rawComment_ = null;
					return;
				}
				this.comment_ = null;
			}

			// Token: 0x060033B0 RID: 13232 RVA: 0x00025BB8 File Offset: 0x00023DB8
			private void MakeTextAvailable()
			{
				if (this.comment_ == null)
				{
					this.comment_ = ZipStrings.ConvertToString(this.rawComment_);
				}
			}

			// Token: 0x060033B1 RID: 13233 RVA: 0x00025BD3 File Offset: 0x00023DD3
			private void MakeBytesAvailable()
			{
				if (this.rawComment_ == null)
				{
					this.rawComment_ = ZipStrings.ConvertToArray(this.comment_);
				}
			}

			// Token: 0x060033B2 RID: 13234 RVA: 0x00025BEE File Offset: 0x00023DEE
			public static implicit operator string(ZipFile.ZipString zipString)
			{
				zipString.MakeTextAvailable();
				return zipString.comment_;
			}

			// Token: 0x04002F41 RID: 12097
			private string comment_;

			// Token: 0x04002F42 RID: 12098
			private byte[] rawComment_;

			// Token: 0x04002F43 RID: 12099
			private readonly bool isSourceString_;
		}

		// Token: 0x020007DF RID: 2015
		private class ZipEntryEnumerator : IEnumerator
		{
			// Token: 0x060033B3 RID: 13235 RVA: 0x00025BFC File Offset: 0x00023DFC
			public ZipEntryEnumerator(ZipEntry[] entries)
			{
				this.array = entries;
			}

			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x060033B4 RID: 13236 RVA: 0x00025C12 File Offset: 0x00023E12
			public object Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x060033B5 RID: 13237 RVA: 0x00025C21 File Offset: 0x00023E21
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x060033B6 RID: 13238 RVA: 0x001920E0 File Offset: 0x001902E0
			public bool MoveNext()
			{
				int num = this.index + 1;
				this.index = num;
				return num < this.array.Length;
			}

			// Token: 0x04002F44 RID: 12100
			private ZipEntry[] array;

			// Token: 0x04002F45 RID: 12101
			private int index = -1;
		}

		// Token: 0x020007E0 RID: 2016
		private class UncompressedStream : Stream
		{
			// Token: 0x060033B7 RID: 13239 RVA: 0x00025C2A File Offset: 0x00023E2A
			public UncompressedStream(Stream baseStream)
			{
				this.baseStream_ = baseStream;
			}

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x060033B8 RID: 13240 RVA: 0x00004050 File Offset: 0x00002250
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060033B9 RID: 13241 RVA: 0x00025C39 File Offset: 0x00023E39
			public override void Flush()
			{
				this.baseStream_.Flush();
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x060033BA RID: 13242 RVA: 0x00025C46 File Offset: 0x00023E46
			public override bool CanWrite
			{
				get
				{
					return this.baseStream_.CanWrite;
				}
			}

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x060033BB RID: 13243 RVA: 0x00004050 File Offset: 0x00002250
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x060033BC RID: 13244 RVA: 0x00025C53 File Offset: 0x00023E53
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x060033BD RID: 13245 RVA: 0x00025C57 File Offset: 0x00023E57
			// (set) Token: 0x060033BE RID: 13246 RVA: 0x0001C722 File Offset: 0x0001A922
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

			// Token: 0x060033BF RID: 13247 RVA: 0x00004050 File Offset: 0x00002250
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x060033C0 RID: 13248 RVA: 0x00025C53 File Offset: 0x00023E53
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060033C1 RID: 13249 RVA: 0x000042DD File Offset: 0x000024DD
			public override void SetLength(long value)
			{
			}

			// Token: 0x060033C2 RID: 13250 RVA: 0x00025C64 File Offset: 0x00023E64
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.baseStream_.Write(buffer, offset, count);
			}

			// Token: 0x04002F46 RID: 12102
			private readonly Stream baseStream_;
		}

		// Token: 0x020007E1 RID: 2017
		private class PartialInputStream : Stream
		{
			// Token: 0x060033C3 RID: 13251 RVA: 0x00025C74 File Offset: 0x00023E74
			public PartialInputStream(ZipFile zipFile, long start, long length)
			{
				this.start_ = start;
				this.length_ = length;
				this.zipFile_ = zipFile;
				this.baseStream_ = this.zipFile_.baseStream_;
				this.readPos_ = start;
				this.end_ = start + length;
			}

			// Token: 0x060033C4 RID: 13252 RVA: 0x00192108 File Offset: 0x00190308
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

			// Token: 0x060033C5 RID: 13253 RVA: 0x00192180 File Offset: 0x00190380
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

			// Token: 0x060033C6 RID: 13254 RVA: 0x00004412 File Offset: 0x00002612
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060033C7 RID: 13255 RVA: 0x00004412 File Offset: 0x00002612
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060033C8 RID: 13256 RVA: 0x00192230 File Offset: 0x00190430
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

			// Token: 0x060033C9 RID: 13257 RVA: 0x000042DD File Offset: 0x000024DD
			public override void Flush()
			{
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x060033CA RID: 13258 RVA: 0x00025CB2 File Offset: 0x00023EB2
			// (set) Token: 0x060033CB RID: 13259 RVA: 0x001922AC File Offset: 0x001904AC
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

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060033CC RID: 13260 RVA: 0x00025CC1 File Offset: 0x00023EC1
			public override long Length
			{
				get
				{
					return this.length_;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060033CD RID: 13261 RVA: 0x00004050 File Offset: 0x00002250
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060033CE RID: 13262 RVA: 0x0000A093 File Offset: 0x00008293
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x060033CF RID: 13263 RVA: 0x0000A093 File Offset: 0x00008293
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x060033D0 RID: 13264 RVA: 0x00025CC9 File Offset: 0x00023EC9
			public override bool CanTimeout
			{
				get
				{
					return this.baseStream_.CanTimeout;
				}
			}

			// Token: 0x04002F47 RID: 12103
			private ZipFile zipFile_;

			// Token: 0x04002F48 RID: 12104
			private Stream baseStream_;

			// Token: 0x04002F49 RID: 12105
			private readonly long start_;

			// Token: 0x04002F4A RID: 12106
			private readonly long length_;

			// Token: 0x04002F4B RID: 12107
			private long readPos_;

			// Token: 0x04002F4C RID: 12108
			private readonly long end_;
		}
	}
}
