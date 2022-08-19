using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000581 RID: 1409
	public class FileSystemScanner
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x00151258 File Offset: 0x0014F458
		public FileSystemScanner(string filter)
		{
			this.fileFilter_ = new PathFilter(filter);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0015126C File Offset: 0x0014F46C
		public FileSystemScanner(string fileFilter, string directoryFilter)
		{
			this.fileFilter_ = new PathFilter(fileFilter);
			this.directoryFilter_ = new PathFilter(directoryFilter);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x0015128C File Offset: 0x0014F48C
		public FileSystemScanner(IScanFilter fileFilter)
		{
			this.fileFilter_ = fileFilter;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x0015129B File Offset: 0x0014F49B
		public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.fileFilter_ = fileFilter;
			this.directoryFilter_ = directoryFilter;
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06002E4F RID: 11855 RVA: 0x001512B4 File Offset: 0x0014F4B4
		// (remove) Token: 0x06002E50 RID: 11856 RVA: 0x001512EC File Offset: 0x0014F4EC
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x06002E51 RID: 11857 RVA: 0x00151324 File Offset: 0x0014F524
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

		// Token: 0x06002E52 RID: 11858 RVA: 0x0015135C File Offset: 0x0014F55C
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

		// Token: 0x06002E53 RID: 11859 RVA: 0x00151398 File Offset: 0x0014F598
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

		// Token: 0x06002E54 RID: 11860 RVA: 0x001513CC File Offset: 0x0014F5CC
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

		// Token: 0x06002E55 RID: 11861 RVA: 0x00151400 File Offset: 0x0014F600
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

		// Token: 0x06002E56 RID: 11862 RVA: 0x00151433 File Offset: 0x0014F633
		public void Scan(string directory, bool recurse)
		{
			this.alive_ = true;
			this.ScanDir(directory, recurse);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x00151444 File Offset: 0x0014F644
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

		// Token: 0x040028D9 RID: 10457
		public ProcessFileHandler ProcessFile;

		// Token: 0x040028DA RID: 10458
		public CompletedFileHandler CompletedFile;

		// Token: 0x040028DB RID: 10459
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x040028DC RID: 10460
		public FileFailureHandler FileFailure;

		// Token: 0x040028DD RID: 10461
		private IScanFilter fileFilter_;

		// Token: 0x040028DE RID: 10462
		private IScanFilter directoryFilter_;

		// Token: 0x040028DF RID: 10463
		private bool alive_;
	}
}
