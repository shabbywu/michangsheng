using System;

namespace KBEngine
{
	// Token: 0x02000EF6 RID: 3830
	public class EntityCellEntityCall_DroppedItemBase : EntityCall
	{
		// Token: 0x06005C45 RID: 23621 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_DroppedItemBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x000412C1 File Offset: 0x0003F4C1
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
