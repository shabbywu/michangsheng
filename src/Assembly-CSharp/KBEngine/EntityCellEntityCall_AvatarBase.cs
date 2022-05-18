using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EEE RID: 3822
	public class EntityCellEntityCall_AvatarBase : EntityCall
	{
		// Token: 0x06005C30 RID: 23600 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_AvatarBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x000410C9 File Offset: 0x0003F2C9
		public void BuildNotify(ulong arg1, Vector3 arg2, Vector3 arg3)
		{
			if (base.newCall("BuildNotify", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeVector3(arg2);
			this.bundle.writeVector3(arg3);
			base.sendCall(null);
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x00041105 File Offset: 0x0003F305
		public void DayZombie()
		{
			if (base.newCall("DayZombie", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0004111D File Offset: 0x0003F31D
		public void SkillDamage(int arg1, int arg2)
		{
			if (base.newCall("SkillDamage", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			this.bundle.writeInt32(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x0004114D File Offset: 0x0003F34D
		public void addSkill(int arg1)
		{
			if (base.newCall("addSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x00041171 File Offset: 0x0003F371
		public void changeAvaterType(uint arg1, uint arg2)
		{
			if (base.newCall("changeAvaterType", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			this.bundle.writeUint32(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x000411A1 File Offset: 0x0003F3A1
		public void dialog(int arg1, uint arg2)
		{
			if (base.newCall("dialog", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			this.bundle.writeUint32(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x000411D1 File Offset: 0x0003F3D1
		public void gameFinsh()
		{
			if (base.newCall("gameFinsh", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x000411E9 File Offset: 0x0003F3E9
		public void relive(byte arg1)
		{
			if (base.newCall("relive", 0) == null)
			{
				return;
			}
			this.bundle.writeUint8(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x0004120D File Offset: 0x0003F40D
		public void removeSkill(int arg1)
		{
			if (base.newCall("removeSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00041231 File Offset: 0x0003F431
		public void requestPull()
		{
			if (base.newCall("requestPull", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00041249 File Offset: 0x0003F449
		public void usePostionSkill(int arg1)
		{
			if (base.newCall("usePostionSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x0004126D File Offset: 0x0003F46D
		public void useSelfSkill(int arg1)
		{
			if (base.newCall("useSelfSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x00041291 File Offset: 0x0003F491
		public void useTargetSkill(int arg1, int arg2)
		{
			if (base.newCall("useTargetSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			this.bundle.writeInt32(arg2);
			base.sendCall(null);
		}
	}
}
