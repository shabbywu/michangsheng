using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200082A RID: 2090
	public class FileSystemScanner
	{
		// Token: 0x060036C1 RID: 14017 RVA: 0x00027D6D File Offset: 0x00025F6D
		public FileSystemScanner(string filter)
		{
			this.fileFilter_ = new PathFilter(filter);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x00027D81 File Offset: 0x00025F81
		public FileSystemScanner(string fileFilter, string directoryFilter)
		{
			this.fileFilter_ = new PathFilter(fileFilter);
			this.directoryFilter_ = new PathFilter(directoryFilter);
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x00027DA1 File Offset: 0x00025FA1
		public FileSystemScanner(IScanFilter fileFilter)
		{
			this.fileFilter_ = fileFilter;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x00027DB0 File Offset: 0x00025FB0
		public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.fileFilter_ = fileFilter;
			this.directoryFilter_ = directoryFilter;
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060036C5 RID: 14021 RVA: 0x0019C164 File Offset: 0x0019A364
		// (remove) Token: 0x060036C6 RID: 14022 RVA: 0x0019C19C File Offset: 0x0019A39C
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x060036C7 RID: 14023 RVA: 0x0019C1D4 File Offset: 0x0019A3D4
		private bool OnDirectoryFailure(string directory, Exception e)
		{
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			bool flag = directoryFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x0019C20C File Offset: 0x0019A40C
		private bool OnFileFailure(string file, Exception e)
		{
			bool flag = this.FileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				this.FileFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x0019C248 File Offset: 0x0019A448
		private void OnProcessFile(string file)
		{
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x0019C27C File Offset: 0x0019A47C
		private void OnCompleteFile(string file)
		{
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x0019C2B0 File Offset: 0x0019A4B0
		private void OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			EventHandler<DirectoryEventArgs> processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				this.alive_ = directoryEventArgs.ContinueRunning;
			}
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x00027DC6 File Offset: 0x00025FC6
		public void Scan(string directory, bool recurse)
		{
			this.alive_ = true;
			this.ScanDir(directory, recurse);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0019C2E4 File Offset: 0x0019A4E4
		private void ScanDir(string directory, bool recurse)
		{
			try
			{
				string[] files = Directory.GetFiles(directory);
				bool flag = false;
				for (int i = 0; i < files.Length; i++)
				{
					if (!this.fileFilter_.IsMatch(files[i]))
					{
						files[i] = null;
					}
					else
					{
						flag = true;
					}
				}
				this.OnProcessDirectory(directory, flag);
				if (this.alive_ && flag)
				{
					foreach (string text in files)
					{
						try
						{
							if (text != null)
							{
								this.OnProcessFile(text);
								if (!this.alive_)
								{
									break;
								}
							}
						}
						catch (Exception e)
						{
							if (!this.OnFileFailure(text, e))
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				if (!this.OnDirectoryFailure(directory, e2))
				{
					throw;
				}
			}
			if (this.alive_ && recurse)
			{
				try
				{
					foreach (string text2 in Directory.GetDirectories(directory))
					{
						if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(text2))
						{
							this.ScanDir(text2, true);
							if (!this.alive_)
							{
								break;
							}
						}
					}
				}
				catch (Exception e3)
				{
					if (!this.OnDirectoryFailure(directory, e3))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04003118 RID: 12568
		public ProcessFileHandler ProcessFile;

		// Token: 0x04003119 RID: 12569
		public CompletedFileHandler CompletedFile;

		// Token: 0x0400311A RID: 12570
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x0400311B RID: 12571
		public FileFailureHandler FileFailure;

		// Token: 0x0400311C RID: 12572
		private IScanFilter fileFilter_;

		// Token: 0x0400311D RID: 12573
		private IScanFilter directoryFilter_;

		// Token: 0x0400311E RID: 12574
		private bool alive_;
	}
}
