using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000526 RID: 1318
	public class FastZip
	{
		// Token: 0x06002A21 RID: 10785 RVA: 0x00140532 File Offset: 0x0013E732
		public FastZip()
		{
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0014055A File Offset: 0x0013E75A
		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x00140589 File Offset: 0x0013E789
		// (set) Token: 0x06002A24 RID: 10788 RVA: 0x00140591 File Offset: 0x0013E791
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

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x0014059A File Offset: 0x0013E79A
		// (set) Token: 0x06002A26 RID: 10790 RVA: 0x001405A2 File Offset: 0x0013E7A2
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

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x001405AB File Offset: 0x0013E7AB
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x001405B3 File Offset: 0x0013E7B3
		public ZipEncryptionMethod EntryEncryptionMethod { get; set; } = ZipEncryptionMethod.ZipCrypto;

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x001405BC File Offset: 0x0013E7BC
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x001405C9 File Offset: 0x0013E7C9
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

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x001405D7 File Offset: 0x0013E7D7
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x001405DF File Offset: 0x0013E7DF
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x001405F7 File Offset: 0x0013E7F7
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x001405FF File Offset: 0x0013E7FF
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

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00140608 File Offset: 0x0013E808
		// (set) Token: 0x06002A30 RID: 10800 RVA: 0x00140610 File Offset: 0x0013E810
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

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00140619 File Offset: 0x0013E819
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x00140621 File Offset: 0x0013E821
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

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x0014062A File Offset: 0x0013E82A
		// (set) Token: 0x06002A34 RID: 10804 RVA: 0x00140632 File Offset: 0x0013E832
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

		// Token: 0x06002A35 RID: 10805 RVA: 0x0014063B File Offset: 0x0013E83B
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0014064F File Offset: 0x0013E84F
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00140662 File Offset: 0x0013E862
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(outputStream, sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00140674 File Offset: 0x0013E874
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter, bool leaveOpen)
		{
			FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00140697 File Offset: 0x0013E897
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x001406AC File Offset: 0x0013E8AC
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter, bool leaveOpen = false)
		{
			FileSystemScanner scanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, scanner, leaveOpen);
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x001406D0 File Offset: 0x0013E8D0
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

		// Token: 0x06002A3C RID: 10812 RVA: 0x0014083C File Offset: 0x0013EA3C
		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_, false);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0014085C File Offset: 0x0013EA5C
		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool allowParentTraversal = false)
		{
			Stream inputStream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.ExtractZip(inputStream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true, allowParentTraversal);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00140888 File Offset: 0x0013EA88
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

		// Token: 0x06002A3F RID: 10815 RVA: 0x001409CC File Offset: 0x0013EBCC
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

		// Token: 0x06002A40 RID: 10816 RVA: 0x00140A44 File Offset: 0x0013EC44
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

		// Token: 0x06002A41 RID: 10817 RVA: 0x00140B1C File Offset: 0x0013ED1C
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

		// Token: 0x06002A42 RID: 10818 RVA: 0x00140B68 File Offset: 0x0013ED68
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

		// Token: 0x06002A43 RID: 10819 RVA: 0x00140C08 File Offset: 0x0013EE08
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

		// Token: 0x06002A44 RID: 10820 RVA: 0x00140DC8 File Offset: 0x0013EFC8
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

		// Token: 0x06002A45 RID: 10821 RVA: 0x00140F18 File Offset: 0x0013F118
		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00140F20 File Offset: 0x0013F120
		private static bool NameIsValid(string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x04002654 RID: 9812
		private bool continueRunning_;

		// Token: 0x04002655 RID: 9813
		private byte[] buffer_;

		// Token: 0x04002656 RID: 9814
		private ZipOutputStream outputStream_;

		// Token: 0x04002657 RID: 9815
		private ZipFile zipFile_;

		// Token: 0x04002658 RID: 9816
		private string sourceDirectory_;

		// Token: 0x04002659 RID: 9817
		private NameFilter fileFilter_;

		// Token: 0x0400265A RID: 9818
		private NameFilter directoryFilter_;

		// Token: 0x0400265B RID: 9819
		private FastZip.Overwrite overwrite_;

		// Token: 0x0400265C RID: 9820
		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		// Token: 0x0400265D RID: 9821
		private bool restoreDateTimeOnExtract_;

		// Token: 0x0400265E RID: 9822
		private bool restoreAttributesOnExtract_;

		// Token: 0x0400265F RID: 9823
		private bool createEmptyDirectories_;

		// Token: 0x04002660 RID: 9824
		private FastZipEvents events_;

		// Token: 0x04002661 RID: 9825
		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		// Token: 0x04002662 RID: 9826
		private INameTransform extractNameTransform_;

		// Token: 0x04002663 RID: 9827
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x04002664 RID: 9828
		private Deflater.CompressionLevel compressionLevel_ = Deflater.CompressionLevel.DEFAULT_COMPRESSION;

		// Token: 0x04002665 RID: 9829
		private string password_;

		// Token: 0x0200147E RID: 5246
		public enum Overwrite
		{
			// Token: 0x04006C33 RID: 27699
			Prompt,
			// Token: 0x04006C34 RID: 27700
			Never,
			// Token: 0x04006C35 RID: 27701
			Always
		}

		// Token: 0x0200147F RID: 5247
		// (Invoke) Token: 0x060080F0 RID: 33008
		public delegate bool ConfirmOverwriteDelegate(string fileName);
	}
}
