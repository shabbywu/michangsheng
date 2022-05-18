using System;

namespace KBEngine
{
	// Token: 0x02000F0A RID: 3850
	public class EntityCellEntityCall_SpaceDuplicateBase : EntityCall
	{
		// Token: 0x06005C5F RID: 23647 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_SpaceDuplicateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
