using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007B9 RID: 1977
	public class FastZipEvents
	{
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600322A RID: 12842 RVA: 0x0018D46C File Offset: 0x0018B66C
		// (remove) Token: 0x0600322B RID: 12843 RVA: 0x0018D4A4 File Offset: 0x0018B6A4
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x0600322C RID: 12844 RVA: 0x0018D4DC File Offset: 0x0018B6DC
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

		// Token: 0x0600322D RID: 12845 RVA: 0x0018D510 File Offset: 0x0018B710
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

		// Token: 0x0600322E RID: 12846 RVA: 0x0018D544 File Offset: 0x0018B744
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

		// Token: 0x0600322F RID: 12847 RVA: 0x0018D574 File Offset: 0x0018B774
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

		// Token: 0x06003230 RID: 12848 RVA: 0x0018D5A4 File Offset: 0x0018B7A4
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x00024957 File Offset: 0x00022B57
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x0002495F File Offset: 0x00022B5F
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

		// Token: 0x04002E3D RID: 11837
		public ProcessFileHandler ProcessFile;

		// Token: 0x04002E3E RID: 11838
		public ProgressHandler Progress;

		// Token: 0x04002E3F RID: 11839
		public CompletedFileHandler CompletedFile;

		// Token: 0x04002E40 RID: 11840
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x04002E41 RID: 11841
		public FileFailureHandler FileFailure;

		// Token: 0x04002E42 RID: 11842
		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);
	}
}
