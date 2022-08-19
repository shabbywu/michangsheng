using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000525 RID: 1317
	public class FastZipEvents
	{
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002A17 RID: 10775 RVA: 0x0014039C File Offset: 0x0013E59C
		// (remove) Token: 0x06002A18 RID: 10776 RVA: 0x001403D4 File Offset: 0x0013E5D4
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x06002A19 RID: 10777 RVA: 0x0014040C File Offset: 0x0013E60C
		public bool OnDirectoryFailure(string directory, Exception e)
		{
			bool result = false;
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			if (directoryFailure != null)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				result = scanFailureEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00140440 File Offset: 0x0013E640
		public bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				fileFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00140474 File Offset: 0x0013E674
		public bool OnProcessFile(string file)
		{
			bool result = true;
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x001404A4 File Offset: 0x0013E6A4
		public bool OnCompletedFile(string file)
		{
			bool result = true;
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x001404D4 File Offset: 0x0013E6D4
		public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			bool result = true;
			EventHandler<DirectoryEventArgs> processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				result = directoryEventArgs.ContinueRunning;
			}
			return result;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x00140505 File Offset: 0x0013E705
		// (set) Token: 0x06002A1F RID: 10783 RVA: 0x0014050D File Offset: 0x0013E70D
		public TimeSpan ProgressInterval
		{
			get
			{
				return this.progressInterval_;
			}
			set
			{
				this.progressInterval_ = value;
			}
		}

		// Token: 0x0400264D RID: 9805
		public ProcessFileHandler ProcessFile;

		// Token: 0x0400264E RID: 9806
		public ProgressHandler Progress;

		// Token: 0x0400264F RID: 9807
		public CompletedFileHandler CompletedFile;

		// Token: 0x04002650 RID: 9808
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x04002651 RID: 9809
		public FileFailureHandler FileFailure;

		// Token: 0x04002652 RID: 9810
		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);
	}
}
