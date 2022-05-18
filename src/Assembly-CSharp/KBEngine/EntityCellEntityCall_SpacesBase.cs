using System;

namespace KBEngine
{
	// Token: 0x02000F0C RID: 3852
	public class EntityCellEntityCall_SpacesBase : EntityCall
	{
		// Token: 0x06005C61 RID: 23649 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_SpacesBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
