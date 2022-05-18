using System;

namespace KBEngine
{
	// Token: 0x02000EF8 RID: 3832
	public class EntityCellEntityCall_GateBase : EntityCall
	{
		// Token: 0x06005C48 RID: 23624 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_GateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
