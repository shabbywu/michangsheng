using System;

namespace KBEngine
{
	// Token: 0x02000EF2 RID: 3826
	public class EntityCellEntityCall_CDKBase : EntityCall
	{
		// Token: 0x06005C41 RID: 23617 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_CDKBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
