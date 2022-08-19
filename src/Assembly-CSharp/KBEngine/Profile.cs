using System;

namespace KBEngine
{
	// Token: 0x02000C5D RID: 3165
	public class Profile
	{
		// Token: 0x06005616 RID: 22038 RVA: 0x0023C00E File Offset: 0x0023A20E
		public Profile(string name)
		{
			this._name = name;
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0023C028 File Offset: 0x0023A228
		~Profile()
		{
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x0023C050 File Offset: 0x0023A250
		public void start()
		{
			this.startTime = DateTime.Now;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x0023C060 File Offset: 0x0023A260
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

		// Token: 0x040050F1 RID: 20721
		private DateTime startTime;

		// Token: 0x040050F2 RID: 20722
		private string _name = "";
	}
}
