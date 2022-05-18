using System;

namespace KBEngine
{
	// Token: 0x02000EFA RID: 3834
	public class EntityCellEntityCall_MailBase : EntityCall
	{
		// Token: 0x06005C4A RID: 23626 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_MailBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
