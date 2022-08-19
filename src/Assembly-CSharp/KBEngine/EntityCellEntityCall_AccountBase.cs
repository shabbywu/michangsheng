using System;

namespace KBEngine
{
	// Token: 0x02000B6E RID: 2926
	public class EntityCellEntityCall_AccountBase : EntityCall
	{
		// Token: 0x060051E2 RID: 20962 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_AccountBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
