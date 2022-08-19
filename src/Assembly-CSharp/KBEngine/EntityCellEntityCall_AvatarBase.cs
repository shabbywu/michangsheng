using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B70 RID: 2928
	public class EntityCellEntityCall_AvatarBase : EntityCall
	{
		// Token: 0x060051F4 RID: 20980 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_AvatarBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00223D74 File Offset: 0x00221F74
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

		// Token: 0x060051F6 RID: 20982 RVA: 0x00223DB0 File Offset: 0x00221FB0
		public void DayZombie()
		{
			if (base.newCall("DayZombie", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00223DC8 File Offset: 0x00221FC8
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

		// Token: 0x060051F8 RID: 20984 RVA: 0x00223DF8 File Offset: 0x00221FF8
		public void addSkill(int arg1)
		{
			if (base.newCall("addSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00223E1C File Offset: 0x0022201C
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

		// Token: 0x060051FA RID: 20986 RVA: 0x00223E4C File Offset: 0x0022204C
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

		// Token: 0x060051FB RID: 20987 RVA: 0x00223E7C File Offset: 0x0022207C
		public void gameFinsh()
		{
			if (base.newCall("gameFinsh", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00223E94 File Offset: 0x00222094
		public void relive(byte arg1)
		{
			if (base.newCall("relive", 0) == null)
			{
				return;
			}
			this.bundle.writeUint8(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00223EB8 File Offset: 0x002220B8
		public void removeSkill(int arg1)
		{
			if (base.newCall("removeSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x00223EDC File Offset: 0x002220DC
		public void requestPull()
		{
			if (base.newCall("requestPull", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00223EF4 File Offset: 0x002220F4
		public void usePostionSkill(int arg1)
		{
			if (base.newCall("usePostionSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00223F18 File Offset: 0x00222118
		public void useSelfSkill(int arg1)
		{
			if (base.newCall("useSelfSkill", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00223F3C File Offset: 0x0022213C
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
