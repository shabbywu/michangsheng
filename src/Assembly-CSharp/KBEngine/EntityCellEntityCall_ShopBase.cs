using System;

namespace KBEngine
{
	// Token: 0x02000F06 RID: 3846
	public class EntityCellEntityCall_ShopBase : EntityCall
	{
		// Token: 0x06005C5B RID: 23643 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_ShopBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
