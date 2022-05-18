using System;

namespace KBEngine
{
	// Token: 0x02000FE7 RID: 4071
	public class Profile
	{
		// Token: 0x06006065 RID: 24677 RVA: 0x00042F06 File Offset: 0x00041106
		public Profile(string name)
		{
			this._name = name;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x0024EF70 File Offset: 0x0024D170
		~Profile()
		{
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x00042F20 File Offset: 0x00041120
		public void start()
		{
			this.startTime = DateTime.Now;
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x00268C58 File Offset: 0x00266E58
		public void end()
		{
			TimeSpan timeSpan = DateTime.Now - this.startTime;
			if (timeSpan.TotalMilliseconds >= 100.0)
			{
				Dbg.WARNING_MSG(string.Concat(new object[]
				{
					"Profile::profile(): '",
					this._name,
					"' took ",
					timeSpan.TotalMilliseconds,
					" ms"
				}));
			}
		}

		// Token: 0x04005BA1 RID: 23457
		private DateTime startTime;

		// Token: 0x04005BA2 RID: 23458
		private string _name = "";
	}
}
