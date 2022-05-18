using System;

namespace KBEngine
{
	// Token: 0x02000EF4 RID: 3828
	public class EntityCellEntityCall_CDKUserUsedBase : EntityCall
	{
		// Token: 0x06005C43 RID: 23619 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_CDKUserUsedBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
