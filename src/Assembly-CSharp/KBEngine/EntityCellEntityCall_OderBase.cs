using System;

namespace KBEngine
{
	// Token: 0x02000F02 RID: 3842
	public class EntityCellEntityCall_OderBase : EntityCall
	{
		// Token: 0x06005C54 RID: 23636 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_OderBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
