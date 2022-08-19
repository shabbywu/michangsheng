using System;

namespace KBEngine
{
	// Token: 0x02000B7A RID: 2938
	public class EntityCellEntityCall_GateBase : EntityCall
	{
		// Token: 0x0600520C RID: 21004 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_GateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
