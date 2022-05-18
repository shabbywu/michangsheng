using System;

namespace KBEngine
{
	// Token: 0x02000EEC RID: 3820
	public class EntityCellEntityCall_AccountBase : EntityCall
	{
		// Token: 0x06005C1E RID: 23582 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_AccountBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
