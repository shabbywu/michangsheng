using System;

namespace KBEngine
{
	// Token: 0x02000EF0 RID: 3824
	public class EntityCellEntityCall_BuildBase : EntityCall
	{
		// Token: 0x06005C3F RID: 23615 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_BuildBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
