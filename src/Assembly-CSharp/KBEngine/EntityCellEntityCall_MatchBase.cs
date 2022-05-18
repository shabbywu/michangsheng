using System;

namespace KBEngine
{
	// Token: 0x02000EFC RID: 3836
	public class EntityCellEntityCall_MatchBase : EntityCall
	{
		// Token: 0x06005C4E RID: 23630 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_MatchBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
