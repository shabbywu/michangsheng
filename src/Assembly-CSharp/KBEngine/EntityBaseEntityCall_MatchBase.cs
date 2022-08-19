using System;

namespace KBEngine
{
	// Token: 0x02000B7D RID: 2941
	public class EntityBaseEntityCall_MatchBase : EntityCall
	{
		// Token: 0x0600520F RID: 21007 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_MatchBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00223F84 File Offset: 0x00222184
		public void createTeam(byte[] arg1, ulong arg2)
		{
			if (base.newCall("createTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00223FB4 File Offset: 0x002221B4
		public void joinTeam(ulong arg1, byte[] arg2, string arg3, uint arg4, ulong arg5)
		{
			if (base.newCall("joinTeam", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeEntitycall(arg2);
			this.bundle.writeUnicode(arg3);
			this.bundle.writeUint32(arg4);
			this.bundle.writeUint64(arg5);
			base.sendCall(null);
		}
	}
}
