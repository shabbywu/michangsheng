using System;

namespace KBEngine
{
	// Token: 0x02000EFE RID: 3838
	public class EntityCellEntityCall_MonsterBase : EntityCall
	{
		// Token: 0x06005C50 RID: 23632 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_MonsterBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
