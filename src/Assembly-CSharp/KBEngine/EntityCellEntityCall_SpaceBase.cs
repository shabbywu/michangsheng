using System;

namespace KBEngine
{
	// Token: 0x02000F08 RID: 3848
	public class EntityCellEntityCall_SpaceBase : EntityCall
	{
		// Token: 0x06005C5D RID: 23645 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_SpaceBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
