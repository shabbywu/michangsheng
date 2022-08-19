using System;

namespace KBEngine
{
	// Token: 0x02000B78 RID: 2936
	public class EntityCellEntityCall_DroppedItemBase : EntityCall
	{
		// Token: 0x06005209 RID: 21001 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_DroppedItemBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00223F6C File Offset: 0x0022216C
		public void pickUpRequest()
		{
			if (base.newCall("pickUpRequest", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}
	}
}
