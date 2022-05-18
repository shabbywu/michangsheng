using System;

namespace KBEngine
{
	// Token: 0x02000F00 RID: 3840
	public class EntityCellEntityCall_NPCBase : EntityCall
	{
		// Token: 0x06005C52 RID: 23634 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_NPCBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
