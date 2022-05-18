using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000823 RID: 2083
	public class DirectoryEventArgs : ScanEventArgs
	{
		// Token: 0x060036A6 RID: 13990 RVA: 0x00027D17 File Offset: 0x00025F17
		public DirectoryEventArgs(string name, bool hasMatchingFiles) : base(name)
		{
			this.hasMatchingFiles_ = hasMatchingFiles;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x00027D27 File Offset: 0x00025F27
		public bool HasMatchingFiles
		{
			get
			{
				return this.hasMatchingFiles_;
			}
		}

		// Token: 0x04003113 RID: 12563
		private readonly bool hasMatchingFiles_;
	}
}
