using System;

namespace KBEngine
{
	// Token: 0x02000F0E RID: 3854
	public class EntityCellEntityCall_SpawnPointBase : EntityCall
	{
		// Token: 0x06005C63 RID: 23651 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_SpawnPointBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
