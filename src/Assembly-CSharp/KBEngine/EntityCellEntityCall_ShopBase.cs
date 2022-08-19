using System;

namespace KBEngine
{
	// Token: 0x02000B88 RID: 2952
	public class EntityCellEntityCall_ShopBase : EntityCall
	{
		// Token: 0x0600521F RID: 21023 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_ShopBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
