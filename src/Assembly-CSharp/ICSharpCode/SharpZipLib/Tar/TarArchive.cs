using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000561 RID: 1377
	public class TarArchive : IDisposable
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002CD7 RID: 11479 RVA: 0x0014C8C0 File Offset: 0x0014AAC0
		// (remove) Token: 0x06002CD8 RID: 11480 RVA: 0x0014C8F8 File Offset: 0x0014AAF8
		public event ProgressMessageHandler ProgressMessageEvent;

		// Token: 0x06002CD9 RID: 11481 RVA: 0x0014C930 File Offset: 0x0014AB30
		protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
		{
			ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
			if (progressMessageEvent != null)
			{
				progressMessageEvent(this, entry, message);
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0014C950 File Offset: 0x0014AB50
		protected TarArchive()
		{
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x0014C96E File Offset: 0x0014AB6E
		protected TarArchive(TarInputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarIn = stream;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0014C9A1 File Offset: 0x0014ABA1
		protected TarArchive(TarOutputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarOut = stream;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x0014C9D4 File Offset: 0x0014ABD4
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream)
		{
			return TarArchive.CreateInputTarArchive(inputStream, null);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x0014C9E0 File Offset: 0x0014ABE0
		public static TarArchive CreateInputTarArchive(Stream inputStream, Encoding nameEncoding)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			TarInputStream tarInputStream = inputStream as TarInputStream;
			TarArchive result;
			if (tarInputStream != null)
			{
				result = new TarArchive(tarInputStream);
			}
			else
			{
				result = TarArchive.CreateInputTarArchive(inputStream, 20, nameEncoding);
			}
			return result;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x0014CA19 File Offset: 0x0014AC19
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
		{
			return TarArchive.CreateInputTarArchive(inputStream, blockFactor, null);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x0014CA23 File Offset: 0x0014AC23
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor, Encoding nameEncoding)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (inputStream is TarInputStream)
			{
				throw new ArgumentException("TarInputStream not valid");
			}
			return new TarArchive(new TarInputStream(inputStream, blockFactor, nameEncoding));
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x0014CA54 File Offset: 0x0014AC54
		public static TarArchive CreateOutputTarArchive(Stream outputStream, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			TarOutputStream tarOutputStream = outputStream as TarOutputStream;
			TarArchive result;
			if (tarOutputStream != null)
			{
				result = new TarArchive(tarOutputStream);
			}
			else
			{
				result = TarArchive.CreateOutputTarArchive(outputStream, 20, nameEncoding);
			}
			return result;
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0014CA8D File Offset: 0x0014AC8D
		public static TarArchive CreateOutputTarArchive(Stream outputStream)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, null);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0014CA96 File Offset: 0x0014AC96
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, blockFactor, null);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0014CAA0 File Offset: 0x0014ACA0
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (outputStream is TarOutputStream)
			{
				throw new ArgumentException("TarOutputStream is not valid");
			}
			return new TarArchive(new TarOutputStream(outputStream, blockFactor, nameEncoding));
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x0014CAD0 File Offset: 0x0014ACD0
		public void SetKeepOldFiles(bool keepExistingFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.keepOldFiles = keepExistingFiles;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x0014CAEC File Offset: 0x0014ACEC
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x0014CB07 File Offset: 0x0014AD07
		public bool AsciiTranslate
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.asciiTranslate;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.asciiTranslate = value;
			}
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0014CB07 File Offset: 0x0014AD07
		[Obsolete("Use the AsciiTranslate property")]
		public void SetAsciiTranslation(bool translateAsciiFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.asciiTranslate = translateAsciiFiles;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0014CB23 File Offset: 0x0014AD23
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x0014CB3E File Offset: 0x0014AD3E
		public string PathPrefix
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.pathPrefix;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.pathPrefix = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x0014CB5A File Offset: 0x0014AD5A
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x0014CB75 File Offset: 0x0014AD75
		public string RootPath
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.rootPath;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.rootPath = value.Replace('\\', '/').TrimEnd(new char[]
				{
					'/'
				});
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x0014CBAA File Offset: 0x0014ADAA
		public void SetUserInfo(int userId, string userName, int groupId, string groupName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.userId = userId;
			this.userName = userName;
			this.groupId = groupId;
			this.groupName = groupName;
			this.applyUserInfoOverrides = true;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0014CBE3 File Offset: 0x0014ADE3
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x0014CBFE File Offset: 0x0014ADFE
		public bool ApplyUserInfoOverrides
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.applyUserInfoOverrides;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.applyUserInfoOverrides = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x0014CC1A File Offset: 0x0014AE1A
		public int UserId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userId;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x0014CC35 File Offset: 0x0014AE35
		public string UserName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userName;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x0014CC50 File Offset: 0x0014AE50
		public int GroupId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupId;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x0014CC6B File Offset: 0x0014AE6B
		public string GroupName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupName;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x0014CC88 File Offset: 0x0014AE88
		public int RecordSize
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				if (this.tarIn != null)
				{
					return this.tarIn.RecordSize;
				}
				if (this.tarOut != null)
				{
					return this.tarOut.RecordSize;
				}
				return 10240;
			}
		}

		// Token: 0x1700036B RID: 875
		// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x0014CCD5 File Offset: 0x0014AED5
		public bool IsStreamOwner
		{
			set
			{
				if (this.tarIn != null)
				{
					this.tarIn.IsStreamOwner = value;
					return;
				}
				this.tarOut.IsStreamOwner = value;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0014CCF8 File Offset: 0x0014AEF8
		[Obsolete("Use Close instead")]
		public void CloseArchive()
		{
			this.Close();
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0014CD00 File Offset: 0x0014AF00
		public void ListContents()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				this.OnProgressMessageEvent(nextEntry, null);
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0014CD39 File Offset: 0x0014AF39
		public void ExtractContents(string destinationDirectory)
		{
			this.ExtractContents(destinationDirectory, false);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0014CD44 File Offset: 0x0014AF44
		public void ExtractContents(string destinationDirectory, bool allowParentTraversal)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			string fullPath = Path.GetFullPath(destinationDirectory);
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				if (nextEntry.TarHeader.TypeFlag != 49 && nextEntry.TarHeader.TypeFlag != 50)
				{
					this.ExtractEntry(fullPath, nextEntry, allowParentTraversal);
				}
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0014CDA4 File Offset: 0x0014AFA4
		private void ExtractEntry(string destDir, TarEntry entry, bool allowParentTraversal)
		{
			this.OnProgressMessageEvent(entry, null);
			string text = entry.Name;
			if (Path.IsPathRooted(text))
			{
				text = text.Substring(Path.GetPathRoot(text).Length);
			}
			text = text.Replace('/', Path.DirectorySeparatorChar);
			string text2 = Path.Combine(destDir, text);
			if (!allowParentTraversal && !Path.GetFullPath(text2).StartsWith(destDir, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new InvalidNameException("Parent traversal in paths is not allowed");
			}
			if (entry.IsDirectory)
			{
				TarArchive.EnsureDirectoryExists(text2);
				return;
			}
			TarArchive.EnsureDirectoryExists(Path.GetDirectoryName(text2));
			bool flag = true;
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Exists)
			{
				if (this.keepOldFiles)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists");
					flag = false;
				}
				else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
					flag = false;
				}
			}
			if (flag)
			{
				using (FileStream fileStream = File.Create(text2))
				{
					if (this.asciiTranslate)
					{
						this.ExtractAndTranslateEntry(text2, fileStream);
					}
					else
					{
						this.tarIn.CopyEntryContents(fileStream);
					}
				}
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0014CEB4 File Offset: 0x0014B0B4
		private void ExtractAndTranslateEntry(string destFile, Stream outputStream)
		{
			if (!TarArchive.IsBinary(destFile))
			{
				using (StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(false), 1024, true))
				{
					byte[] array = new byte[32768];
					for (;;)
					{
						int num = this.tarIn.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							if (array[i] == 10)
							{
								string @string = Encoding.ASCII.GetString(array, num2, i - num2);
								streamWriter.WriteLine(@string);
								num2 = i + 1;
							}
						}
					}
					return;
				}
			}
			this.tarIn.CopyEntryContents(outputStream);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0014CF64 File Offset: 0x0014B164
		public void WriteEntry(TarEntry sourceEntry, bool recurse)
		{
			if (sourceEntry == null)
			{
				throw new ArgumentNullException("sourceEntry");
			}
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			try
			{
				if (recurse)
				{
					TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
				}
				this.WriteEntryCore(sourceEntry, recurse);
			}
			finally
			{
				if (recurse)
				{
					TarHeader.RestoreSetValues();
				}
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0014CFD8 File Offset: 0x0014B1D8
		private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
		{
			string text = null;
			string text2 = sourceEntry.File;
			TarEntry tarEntry = (TarEntry)sourceEntry.Clone();
			if (this.applyUserInfoOverrides)
			{
				tarEntry.GroupId = this.groupId;
				tarEntry.GroupName = this.groupName;
				tarEntry.UserId = this.userId;
				tarEntry.UserName = this.userName;
			}
			this.OnProgressMessageEvent(tarEntry, null);
			if (this.asciiTranslate && !tarEntry.IsDirectory && !TarArchive.IsBinary(text2))
			{
				text = Path.GetTempFileName();
				using (StreamReader streamReader = File.OpenText(text2))
				{
					using (Stream stream = File.Create(text))
					{
						for (;;)
						{
							string text3 = streamReader.ReadLine();
							if (text3 == null)
							{
								break;
							}
							byte[] bytes = Encoding.ASCII.GetBytes(text3);
							stream.Write(bytes, 0, bytes.Length);
							stream.WriteByte(10);
						}
						stream.Flush();
					}
				}
				tarEntry.Size = new FileInfo(text).Length;
				text2 = text;
			}
			string text4 = null;
			if (!string.IsNullOrEmpty(this.rootPath) && tarEntry.Name.StartsWith(this.rootPath, StringComparison.OrdinalIgnoreCase))
			{
				text4 = tarEntry.Name.Substring(this.rootPath.Length + 1);
			}
			if (this.pathPrefix != null)
			{
				text4 = ((text4 == null) ? (this.pathPrefix + "/" + tarEntry.Name) : (this.pathPrefix + "/" + text4));
			}
			if (text4 != null)
			{
				tarEntry.Name = text4;
			}
			this.tarOut.PutNextEntry(tarEntry);
			if (tarEntry.IsDirectory)
			{
				if (recurse)
				{
					TarEntry[] directoryEntries = tarEntry.GetDirectoryEntries();
					for (int i = 0; i < directoryEntries.Length; i++)
					{
						this.WriteEntryCore(directoryEntries[i], recurse);
					}
					return;
				}
			}
			else
			{
				using (Stream stream2 = File.OpenRead(text2))
				{
					byte[] array = new byte[32768];
					for (;;)
					{
						int num = stream2.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						this.tarOut.Write(array, 0, num);
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					File.Delete(text);
				}
				this.tarOut.CloseEntry();
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0014D220 File Offset: 0x0014B420
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0014D230 File Offset: 0x0014B430
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (disposing)
				{
					if (this.tarOut != null)
					{
						this.tarOut.Flush();
						this.tarOut.Dispose();
					}
					if (this.tarIn != null)
					{
						this.tarIn.Dispose();
					}
				}
			}
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0014D280 File Offset: 0x0014B480
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0014D28C File Offset: 0x0014B48C
		~TarArchive()
		{
			this.Dispose(false);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0014D2BC File Offset: 0x0014B4BC
		private static void EnsureDirectoryExists(string directoryName)
		{
			if (!Directory.Exists(directoryName))
			{
				try
				{
					Directory.CreateDirectory(directoryName);
				}
				catch (Exception ex)
				{
					throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
				}
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0014D308 File Offset: 0x0014B508
		private static bool IsBinary(string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				int num = Math.Min(4096, (int)fileStream.Length);
				byte[] array = new byte[num];
				int num2 = fileStream.Read(array, 0, num);
				for (int i = 0; i < num2; i++)
				{
					byte b = array[i];
					if (b < 8 || (b > 13 && b < 32) || b == 255)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04002810 RID: 10256
		private bool keepOldFiles;

		// Token: 0x04002811 RID: 10257
		private bool asciiTranslate;

		// Token: 0x04002812 RID: 10258
		private int userId;

		// Token: 0x04002813 RID: 10259
		private string userName = string.Empty;

		// Token: 0x04002814 RID: 10260
		private int groupId;

		// Token: 0x04002815 RID: 10261
		private string groupName = string.Empty;

		// Token: 0x04002816 RID: 10262
		private string rootPath;

		// Token: 0x04002817 RID: 10263
		private string pathPrefix;

		// Token: 0x04002818 RID: 10264
		private bool applyUserInfoOverrides;

		// Token: 0x04002819 RID: 10265
		private TarInputStream tarIn;

		// Token: 0x0400281A RID: 10266
		private TarOutputStream tarOut;

		// Token: 0x0400281B RID: 10267
		private bool isDisposed;
	}
}
