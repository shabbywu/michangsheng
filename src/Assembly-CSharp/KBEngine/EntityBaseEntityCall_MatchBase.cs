using System;

namespace KBEngine
{
	// Token: 0x02000EFB RID: 3835
	public class EntityBaseEntityCall_MatchBase : EntityCall
	{
		// Token: 0x06005C4B RID: 23627 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_MatchBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x000412D9 File Offset: 0x0003F4D9
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

		// Token: 0x06005C4D RID: 23629 RVA: 0x002527D0 File Offset: 0x002509D0
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
