using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000822 RID: 2082
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x0600369F RID: 13983 RVA: 0x00027CCA File Offset: 0x00025ECA
		public ProgressEventArgs(string name, long processed, long target)
		{
			this.name_ = name;
			this.processed_ = processed;
			this.target_ = target;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x00027CEE File Offset: 0x00025EEE
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x00027CF6 File Offset: 0x00025EF6
		// (set) Token: 0x060036A2 RID: 13986 RVA: 0x00027CFE File Offset: 0x00025EFE
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x0019C12C File Offset: 0x0019A32C
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

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x00027D07 File Offset: 0x00025F07
		public long Processed
		{
			get
			{
				return this.processed_;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x00027D0F File Offset: 0x00025F0F
		public long Target
		{
			get
			{
				return this.target_;
			}
		}

		// Token: 0x0400310F RID: 12559
		private string name_;

		// Token: 0x04003110 RID: 12560
		private long processed_;

		// Token: 0x04003111 RID: 12561
		private long target_;

		// Token: 0x04003112 RID: 12562
		private bool continueRunning_ = true;
	}
}
