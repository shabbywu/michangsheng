using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200057A RID: 1402
	public class DirectoryEventArgs : ScanEventArgs
	{
		// Token: 0x06002E30 RID: 11824 RVA: 0x00151202 File Offset: 0x0014F402
		public DirectoryEventArgs(string name, bool hasMatchingFiles) : base(name)
		{
			this.hasMatchingFiles_ = hasMatchingFiles;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x00151212 File Offset: 0x0014F412
		public bool HasMatchingFiles
		{
			get
			{
				return this.hasMatchingFiles_;
			}
		}

		// Token: 0x040028D4 RID: 10452
		private readonly bool hasMatchingFiles_;
	}
}
