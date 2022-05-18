using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000824 RID: 2084
	public class ScanFailureEventArgs : EventArgs
	{
		// Token: 0x060036A8 RID: 13992 RVA: 0x00027D2F File Offset: 0x00025F2F
		public ScanFailureEventArgs(string name, Exception e)
		{
			this.name_ = name;
			this.exception_ = e;
			this.continueRunning_ = true;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x00027D4C File Offset: 0x00025F4C
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x00027D54 File Offset: 0x00025F54
		public Exception Exception
		{
			get
			{
				return this.exception_;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x00027D5C File Offset: 0x00025F5C
		// (set) Token: 0x060036AC RID: 13996 RVA: 0x00027D64 File Offset: 0x00025F64
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

		// Token: 0x04003114 RID: 12564
		private string name_;

		// Token: 0x04003115 RID: 12565
		private Exception exception_;

		// Token: 0x04003116 RID: 12566
		private bool continueRunning_;
	}
}
