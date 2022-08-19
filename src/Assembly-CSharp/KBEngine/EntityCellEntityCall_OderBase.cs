using System;

namespace KBEngine
{
	// Token: 0x02000B84 RID: 2948
	public class EntityCellEntityCall_OderBase : EntityCall
	{
		// Token: 0x06005218 RID: 21016 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_OderBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
