using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200057B RID: 1403
	public class ScanFailureEventArgs : EventArgs
	{
		// Token: 0x06002E32 RID: 11826 RVA: 0x0015121A File Offset: 0x0014F41A
		public ScanFailureEventArgs(string name, Exception e)
		{
			this.name_ = name;
			this.exception_ = e;
			this.continueRunning_ = true;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x00151237 File Offset: 0x0014F437
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x0015123F File Offset: 0x0014F43F
		public Exception Exception
		{
			get
			{
				return this.exception_;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x00151247 File Offset: 0x0014F447
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x0015124F File Offset: 0x0014F44F
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

		// Token: 0x040028D5 RID: 10453
		private string name_;

		// Token: 0x040028D6 RID: 10454
		private Exception exception_;

		// Token: 0x040028D7 RID: 10455
		private bool continueRunning_;
	}
}
