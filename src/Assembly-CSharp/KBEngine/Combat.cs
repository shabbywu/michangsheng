using System;

namespace KBEngine
{
	// Token: 0x0200101B RID: 4123
	public class Combat
	{
		// Token: 0x06006289 RID: 25225 RVA: 0x00044365 File Offset: 0x00042565
		public Combat(Entity e)
		{
			this.entity = e;
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x000042DD File Offset: 0x000024DD
		public void __init__()
		{
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x00043B90 File Offset: 0x00041D90
		public void recvSkill(int attacker, int skillID)
		{
			Event.fireOut("recvSkill", new object[]
			{
				attacker,
				skillID
			});
		}

		// Token: 0x04005CED RID: 23789
		public Entity entity;
	}
}
