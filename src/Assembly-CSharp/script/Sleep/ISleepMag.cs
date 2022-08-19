using System;

namespace script.Sleep
{
	// Token: 0x020009EC RID: 2540
	public abstract class ISleepMag
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x001DD132 File Offset: 0x001DB332
		public static ISleepMag Inst
		{
			get
			{
				if (ISleepMag._inst == null)
				{
					ISleepMag._inst = new SleepMag();
				}
				return ISleepMag._inst;
			}
		}

		// Token: 0x06004682 RID: 18050
		public abstract void Sleep();

		// Token: 0x040047EB RID: 18411
		private static ISleepMag _inst;
	}
}
