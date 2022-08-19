using System;

namespace KBEngine
{
	// Token: 0x02000C75 RID: 3189
	public class Combat
	{
		// Token: 0x06005809 RID: 22537 RVA: 0x00248CB8 File Offset: 0x00246EB8
		public Combat(Entity e)
		{
			this.entity = e;
		}

		// Token: 0x0600580A RID: 22538 RVA: 0x00004095 File Offset: 0x00002295
		public void __init__()
		{
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x0024431B File Offset: 0x0024251B
		public void recvSkill(int attacker, int skillID)
		{
			Event.fireOut("recvSkill", new object[]
			{
				attacker,
				skillID
			});
		}

		// Token: 0x040051FA RID: 20986
		public Entity entity;
	}
}
