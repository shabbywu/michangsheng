using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000807 RID: 2055
	public class TarArchive : IDisposable
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06003545 RID: 13637 RVA: 0x00198774 File Offset: 0x00196974
		// (remove) Token: 0x06003546 RID: 13638 RVA: 0x001987AC File Offset: 0x001969AC
		public event ProgressMessageHandler ProgressMessageEvent;

		// Token: 0x06003547 RID: 13639 RVA: 0x001987E4 File Offset: 0x001969E4
		protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
		{
			ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
			if (progressMessageEvent != null)
			{
				progressMessageEvent(this, entry, message);
			}
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x00026DA1 File Offset: 0x00024FA1
		protected TarArchive()
		{
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x00026DBF File Offset: 0x00024FBF
		protected TarArchive(TarInputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarIn = stream;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x00026DF2 File Offset: 0x00024FF2
		protected TarArchive(TarOutputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarOut = stream;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x00026E25 File Offset: 0x00025025
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream)
		{
			return TarArchive.CreateInputTarArchive(inputStream, null);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x00198804 File Offset: 0x00196A04
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

		// Token: 0x0600354D RID: 13645 RVA: 0x00026E2E File Offset: 0x0002502E
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
		{
			return TarArchive.CreateInputTarArchive(inputStream, blockFactor, null);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x00026E38 File Offset: 0x00025038
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

		// Token: 0x0600354F RID: 13647 RVA: 0x00198840 File Offset: 0x00196A40
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

		// Token: 0x06003550 RID: 13648 RVA: 0x00026E68 File Offset: 0x00025068
		public static TarArchive CreateOutputTarArchive(Stream outputStream)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, null);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x00026E71 File Offset: 0x00025071
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, blockFactor, null);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x00026E7B File Offset: 0x0002507B
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

		// Token: 0x06003553 RID: 13651 RVA: 0x00026EAB File Offset: 0x000250AB
		public void SetKeepOldFiles(bool keepExistingFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.keepOldFiles = keepExistingFiles;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x00026EC7 File Offset: 0x000250C7
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x00026EE2 File Offset: 0x000250E2
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

		// Token: 0x06003556 RID: 13654 RVA: 0x00026EE2 File Offset: 0x000250E2
		[Obsolete("Use the AsciiTranslate property")]
		public void SetAsciiTranslation(bool translateAsciiFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.asciiTranslate = translateAsciiFiles;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x00026EFE File Offset: 0x000250FE
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x00026F19 File Offset: 0x00025119
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x00026F35 File Offset: 0x00025135
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x00026F50 File Offset: 0x00025150
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

		// Token: 0x0600355B RID: 13659 RVA: 0x00026F85 File Offset: 0x00025185
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x00026FBE File Offset: 0x000251BE
		// (set) Token: 0x0600355D RID: 13661 RVA: 0x00026FD9 File Offset: 0x000251D9
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

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x00026FF5 File Offset: 0x000251F5
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x00027010 File Offset: 0x00025210
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

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x0002702B File Offset: 0x0002522B
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x00027046 File Offset: 0x00025246
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

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x0019887C File Offset: 0x00196A7C
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

		// Token: 0x17000522 RID: 1314
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x00027061 File Offset: 0x00025261
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

		// Token: 0x06003564 RID: 13668 RVA: 0x00027084 File Offset: 0x00025284
		[Obsolete("Use Close instead")]
		public void CloseArchive()
		{
			this.Close();
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x001988CC File Offset: 0x00196ACC
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

		// Token: 0x06003566 RID: 13670 RVA: 0x0002708C File Offset: 0x0002528C
		public void ExtractContents(string destinationDirectory)
		{
			this.ExtractContents(destinationDirectory, false);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x00198908 File Offset: 0x00196B08
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

		// Token: 0x06003568 RID: 13672 RVA: 0x00198968 File Offset: 0x00196B68
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

		// Token: 0x06003569 RID: 13673 RVA: 0x00198A78 File Offset: 0x00196C78
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

		// Token: 0x0600356A RID: 13674 RVA: 0x00198B28 File Offset: 0x00196D28
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

		// Token: 0x0600356B RID: 13675 RVA: 0x00198B9C File Offset: 0x00196D9C
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

		// Token: 0x0600356C RID: 13676 RVA: 0x00027096 File Offset: 0x00025296
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x00198DE4 File Offset: 0x00196FE4
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

		// Token: 0x0600356E RID: 13678 RVA: 0x000270A5 File Offset: 0x000252A5
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x00198E34 File Offset: 0x00197034
		~TarArchive()
		{
			this.Dispose(false);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x00198E64 File Offset: 0x00197064
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

		// Token: 0x06003571 RID: 13681 RVA: 0x00198EB0 File Offset: 0x001970B0
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

		// Token: 0x04003049 RID: 12361
		private bool keepOldFiles;

		// Token: 0x0400304A RID: 12362
		private bool asciiTranslate;

		// Token: 0x0400304B RID: 12363
		private int userId;

		// Token: 0x0400304C RID: 12364
		private string userName = string.Empty;

		// Token: 0x0400304D RID: 12365
		private int groupId;

		// Token: 0x0400304E RID: 12366
		private string groupName = string.Empty;

		// Token: 0x0400304F RID: 12367
		private string rootPath;

		// Token: 0x04003050 RID: 12368
		private string pathPrefix;

		// Token: 0x04003051 RID: 12369
		private bool applyUserInfoOverrides;

		// Token: 0x04003052 RID: 12370
		private TarInputStream tarIn;

		// Token: 0x04003053 RID: 12371
		private TarOutputStream tarOut;

		// Token: 0x04003054 RID: 12372
		private bool isDisposed;
	}
}
