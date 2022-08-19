using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000579 RID: 1401
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x06002E29 RID: 11817 RVA: 0x0015117E File Offset: 0x0014F37E
		public ProgressEventArgs(string name, long processed, long target)
		{
			this.name_ = name;
			this.processed_ = processed;
			this.target_ = target;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x001511A2 File Offset: 0x0014F3A2
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x001511AA File Offset: 0x0014F3AA
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x001511B2 File Offset: 0x0014F3B2
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x001511BC File Offset: 0x0014F3BC
		public float PercentComplete
		{
			get
			{
				float result;
				if (this.target_ <= 0L)
				{
					result = 0f;
				}
				else
				{
					result = (float)this.processed_ / (float)this.target_ * 100f;
				}
				return result;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x001511F2 File Offset: 0x0014F3F2
		public long Processed
		{
			get
			{
				return this.processed_;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x001511FA File Offset: 0x0014F3FA
		public long Target
		{
			get
			{
				return this.target_;
			}
		}

		// Token: 0x040028D0 RID: 10448
		private string name_;

		// Token: 0x040028D1 RID: 10449
		private long processed_;

		// Token: 0x040028D2 RID: 10450
		private long target_;

		// Token: 0x040028D3 RID: 10451
		private bool continueRunning_ = true;
	}
}
