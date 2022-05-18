using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007BA RID: 1978
	public class FastZip
	{
		// Token: 0x06003234 RID: 12852 RVA: 0x00024984 File Offset: 0x00022B84
		public FastZip()
		{
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000249AC File Offset: 0x00022BAC
		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000249DB File Offset: 0x00022BDB
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x000249E3 File Offset: 0x00022BE3
		public bool CreateEmptyDirectories
		{
			get
			{
				return this.createEmptyDirectories_;
			}
			set
			{
				this.createEmptyDirectories_ = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000249EC File Offset: 0x00022BEC
		// (set) Token: 0x06003239 RID: 12857 RVA: 0x000249F4 File Offset: 0x00022BF4
		public string Password
		{
			get
			{
				return this.password_;
			}
			set
			{
				this.password_ = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000249FD File Offset: 0x00022BFD
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x00024A05 File Offset: 0x00022C05
		public ZipEncryptionMethod EntryEncryptionMethod { get; set; } = ZipEncryptionMethod.ZipCrypto;

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x00024A0E File Offset: 0x00022C0E
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x00024A1B File Offset: 0x00022C1B
		public INameTransform NameTransform
		{
			get
			{
				return this.entryFactory_.NameTransform;
			}
			set
			{
				this.entryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x00024A29 File Offset: 0x00022C29
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x00024A31 File Offset: 0x00022C31
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.entryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.entryFactory_ = new ZipEntryFactory();
					return;
				}
				this.entryFactory_ = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x00024A49 File Offset: 0x00022C49
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x00024A51 File Offset: 0x00022C51
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x00024A5A File Offset: 0x00022C5A
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x00024A62 File Offset: 0x00022C62
		public bool RestoreDateTimeOnExtract
		{
			get
			{
				return this.restoreDateTimeOnExtract_;
			}
			set
			{
				this.restoreDateTimeOnExtract_ = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x00024A6B File Offset: 0x00022C6B
		// (set) Token: 0x06003245 RID: 12869 RVA: 0x00024A73 File Offset: 0x00022C73
		public bool RestoreAttributesOnExtract
		{
			get
			{
				return this.restoreAttributesOnExtract_;
			}
			set
			{
				this.restoreAttributesOnExtract_ = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x00024A7C File Offset: 0x00022C7C
		// (set) Token: 0x06003247 RID: 12871 RVA: 0x00024A84 File Offset: 0x00022C84
		public Deflater.CompressionLevel CompressionLevel
		{
			get
			{
				return this.compressionLevel_;
			}
			set
			{
				this.compressionLevel_ = value;
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x00024A8D File Offset: 0x00022C8D
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x00024AA1 File Offset: 0x00022CA1
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x00024AB4 File Offset: 0x00022CB4
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(outputStream, sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0018D5D8 File Offset: 0x0018B7D8
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter, bool leaveOpen)
		{
			FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x00024AC4 File Offset: 0x00022CC4
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0018D5FC File Offset: 0x0018B7FC
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter, bool leaveOpen = false)
		{
			FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x0018D620 File Offset: 0x0018B820
		private void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, FileSystemScanner scanner, bool leaveOpen)
		{
			this.NameTransform = new ZipNameTransform(sourceDirectory);
			this.sourceDirectory_ = sourceDirectory;
			using (this.outputStream_ = new ZipOutputStream(outputStream))
			{
				this.outputStream_.SetLevel((int)this.CompressionLevel);
				this.outputStream_.IsStreamOwner = !leaveOpen;
				this.outputStream_.NameTransform = null;
				if (!string.IsNullOrEmpty(this.password_) && this.EntryEncryptionMethod != ZipEncryptionMethod.None)
				{
					this.outputStream_.Password = this.password_;
				}
				this.outputStream_.UseZip64 = this.UseZip64;
				scanner.ProcessFile = (ProcessFileHandler)Delegate.Combine(scanner.ProcessFile, new ProcessFileHandler(this.ProcessFile));
				if (this.CreateEmptyDirectories)
				{
					scanner.ProcessDirectory += this.ProcessDirectory;
				}
				if (this.events_ != null)
				{
					if (this.events_.FileFailure != null)
					{
						scanner.FileFailure = (FileFailureHandler)Delegate.Combine(scanner.FileFailure, this.events_.FileFailure);
					}
					if (this.events_.DirectoryFailure != null)
					{
						scanner.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(scanner.DirectoryFailure, this.events_.DirectoryFailure);
					}
				}
				scanner.Scan(sourceDirectory, recurse);
			}
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x0018D78C File Offset: 0x0018B98C
		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_, false);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x0018D7AC File Offset: 0x0018B9AC
		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool allowParentTraversal = false)
		{
			Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true, allowParentTraversal);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0018D7D8 File Offset: 0x0018B9D8
		public void ExtractZip(Stream inputStream, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner, bool allowParentTraversal = false)
		{
			if (overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null)
			{
				throw new ArgumentNullException("confirmDelegate");
			}
			this.continueRunning_ = true;
			this.overwrite_ = overwrite;
			this.confirmDelegate_ = confirmDelegate;
			this.extractNameTransform_ = new WindowsNameTransform(targetDirectory, allowParentTraversal);
			this.fileFilter_ = new NameFilter(fileFilter);
			this.directoryFilter_ = new NameFilter(directoryFilter);
			this.restoreDateTimeOnExtract_ = restoreDateTime;
			using (this.zipFile_ = new ZipFile(inputStream, !isStreamOwner))
			{
				if (this.password_ != null)
				{
					this.zipFile_.Password = this.password_;
				}
				IEnumerator enumerator = this.zipFile_.GetEnumerator();
				while (this.continueRunning_ && enumerator.MoveNext())
				{
					ZipEntry zipEntry = (ZipEntry)enumerator.Current;
					if (zipEntry.IsFile)
					{
						if (this.directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && this.fileFilter_.IsMatch(zipEntry.Name))
						{
							this.ExtractEntry(zipEntry);
						}
					}
					else if (zipEntry.IsDirectory && this.directoryFilter_.IsMatch(zipEntry.Name) && this.CreateEmptyDirectories)
					{
						this.ExtractEntry(zipEntry);
					}
				}
			}
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0018D91C File Offset: 0x0018BB1C
		private void ProcessDirectory(object sender, DirectoryEventArgs e)
		{
			if (!e.HasMatchingFiles && this.CreateEmptyDirectories)
			{
				if (this.events_ != null)
				{
					this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
				}
				if (e.ContinueRunning && e.Name != this.sourceDirectory_)
				{
					ZipEntry entry = this.entryFactory_.MakeDirectoryEntry(e.Name);
					this.outputStream_.PutNextEntry(entry);
				}
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0018D994 File Offset: 0x0018BB94
		private void ProcessFile(object sender, ScanEventArgs e)
		{
			if (this.events_ != null && this.events_.ProcessFile != null)
			{
				this.events_.ProcessFile(sender, e);
			}
			if (e.ContinueRunning)
			{
				try
				{
					using (FileStream fileStream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						ZipEntry entry = this.entryFactory_.MakeFileEntry(e.Name);
						this.ConfigureEntryEncryption(entry);
						this.outputStream_.PutNextEntry(entry);
						this.AddFileContents(e.Name, fileStream);
					}
				}
				catch (Exception e2)
				{
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					this.continueRunning_ = this.events_.OnFileFailure(e.Name, e2);
				}
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x0018DA6C File Offset: 0x0018BC6C
		private void ConfigureEntryEncryption(ZipEntry entry)
		{
			if (!string.IsNullOrEmpty(this.Password) && entry.AESEncryptionStrength == 0)
			{
				ZipEncryptionMethod entryEncryptionMethod = this.EntryEncryptionMethod;
				if (entryEncryptionMethod == ZipEncryptionMethod.AES128)
				{
					entry.AESKeySize = 128;
					return;
				}
				if (entryEncryptionMethod != ZipEncryptionMethod.AES256)
				{
					return;
				}
				entry.AESKeySize = 256;
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x0018DAB8 File Offset: 0x0018BCB8
		private void AddFileContents(string name, Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.buffer_ == null)
			{
				this.buffer_ = new byte[4096];
			}
			if (this.events_ != null && this.events_.Progress != null)
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, name);
			}
			else
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_);
			}
			if (this.events_ != null)
			{
				this.continueRunning_ = this.events_.OnCompletedFile(name);
			}
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x0018DB58 File Offset: 0x0018BD58
		private void ExtractFileEntry(ZipEntry entry, string targetName)
		{
			bool flag = true;
			if (this.overwrite_ != FastZip.Overwrite.Always && File.Exists(targetName))
			{
				flag = (this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null && this.confirmDelegate_(targetName));
			}
			if (flag)
			{
				if (this.events_ != null)
				{
					this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
				}
				if (this.continueRunning_)
				{
					try
					{
						using (FileStream fileStream = File.Create(targetName))
						{
							if (this.buffer_ == null)
							{
								this.buffer_ = new byte[4096];
							}
							using (Stream inputStream = this.zipFile_.GetInputStream(entry))
							{
								if (this.events_ != null && this.events_.Progress != null)
								{
									StreamUtils.Copy(inputStream, fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, entry.Name, entry.Size);
								}
								else
								{
									StreamUtils.Copy(inputStream, fileStream, this.buffer_);
								}
							}
							if (this.events_ != null)
							{
								this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
							}
						}
						if (this.restoreDateTimeOnExtract_)
						{
							File.SetLastWriteTime(targetName, entry.DateTime);
						}
						if (this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
						{
							FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
							fileAttributes &= (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal);
							File.SetAttributes(targetName, fileAttributes);
						}
					}
					catch (Exception e)
					{
						if (this.events_ == null)
						{
							this.continueRunning_ = false;
							throw;
						}
						this.continueRunning_ = this.events_.OnFileFailure(targetName, e);
					}
				}
			}
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0018DD18 File Offset: 0x0018BF18
		private void ExtractEntry(ZipEntry entry)
		{
			bool flag = entry.IsCompressionMethodSupported();
			string text = entry.Name;
			if (flag)
			{
				if (entry.IsFile)
				{
					text = this.extractNameTransform_.TransformFile(text);
				}
				else if (entry.IsDirectory)
				{
					text = this.extractNameTransform_.TransformDirectory(text);
				}
				flag = !string.IsNullOrEmpty(text);
			}
			string text2 = string.Empty;
			if (flag)
			{
				if (entry.IsDirectory)
				{
					text2 = text;
				}
				else
				{
					text2 = Path.GetDirectoryName(Path.GetFullPath(text));
				}
			}
			if (flag && !Directory.Exists(text2) && (!entry.IsDirectory || this.CreateEmptyDirectories))
			{
				try
				{
					FastZipEvents fastZipEvents = this.events_;
					this.continueRunning_ = (fastZipEvents == null || fastZipEvents.OnProcessDirectory(text2, true));
					if (this.continueRunning_)
					{
						Directory.CreateDirectory(text2);
						if (entry.IsDirectory && this.restoreDateTimeOnExtract_)
						{
							Directory.SetLastWriteTime(text2, entry.DateTime);
						}
					}
					else
					{
						flag = false;
					}
				}
				catch (Exception e)
				{
					flag = false;
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					if (entry.IsDirectory)
					{
						this.continueRunning_ = this.events_.OnDirectoryFailure(text, e);
					}
					else
					{
						this.continueRunning_ = this.events_.OnFileFailure(text, e);
					}
				}
			}
			if (flag && entry.IsFile)
			{
				this.ExtractFileEntry(entry, text);
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x00024AD9 File Offset: 0x00022CD9
		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00024AE1 File Offset: 0x00022CE1
		private static bool NameIsValid(string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x04002E44 RID: 11844
		private bool continueRunning_;

		// Token: 0x04002E45 RID: 11845
		private byte[] buffer_;

		// Token: 0x04002E46 RID: 11846
		private ZipOutputStream outputStream_;

		// Token: 0x04002E47 RID: 11847
		private ZipFile zipFile_;

		// Token: 0x04002E48 RID: 11848
		private string sourceDirectory_;

		// Token: 0x04002E49 RID: 11849
		private NameFilter fileFilter_;

		// Token: 0x04002E4A RID: 11850
		private NameFilter directoryFilter_;

		// Token: 0x04002E4B RID: 11851
		private FastZip.Overwrite overwrite_;

		// Token: 0x04002E4C RID: 11852
		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		// Token: 0x04002E4D RID: 11853
		private bool restoreDateTimeOnExtract_;

		// Token: 0x04002E4E RID: 11854
		private bool restoreAttributesOnExtract_;

		// Token: 0x04002E4F RID: 11855
		private bool createEmptyDirectories_;

		// Token: 0x04002E50 RID: 11856
		private FastZipEvents events_;

		// Token: 0x04002E51 RID: 11857
		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		// Token: 0x04002E52 RID: 11858
		private INameTransform extractNameTransform_;

		// Token: 0x04002E53 RID: 11859
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x04002E54 RID: 11860
		private Deflater.CompressionLevel compressionLevel_ = Deflater.CompressionLevel.DEFAULT_COMPRESSION;

		// Token: 0x04002E55 RID: 11861
		private string password_;

		// Token: 0x020007BB RID: 1979
		public enum Overwrite
		{
			// Token: 0x04002E57 RID: 11863
			Prompt,
			// Token: 0x04002E58 RID: 11864
			Never,
			// Token: 0x04002E59 RID: 11865
			Always
		}

		// Token: 0x020007BC RID: 1980
		// (Invoke) Token: 0x0600325B RID: 12891
		public delegate bool ConfirmOverwriteDelegate(string fileName);
	}
}
