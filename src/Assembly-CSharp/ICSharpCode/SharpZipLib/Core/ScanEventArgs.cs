using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000821 RID: 2081
	public class ScanEventArgs : EventArgs
	{
		// Token: 0x0600369B RID: 13979 RVA: 0x00027C9B File Offset: 0x00025E9B
		public ScanEventArgs(string name)
		{
			this.name_ = name;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x00027CB1 File Offset: 0x00025EB1
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x00027CB9 File Offset: 0x00025EB9
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x00027CC1 File Offset: 0x00025EC1
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x0400310D RID: 12557
		private string name_;

		// Token: 0x0400310E RID: 12558
		private bool continueRunning_ = true;
	}
}
