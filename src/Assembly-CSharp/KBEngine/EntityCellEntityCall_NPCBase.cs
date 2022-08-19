using System;

namespace KBEngine
{
	// Token: 0x02000B82 RID: 2946
	public class EntityCellEntityCall_NPCBase : EntityCall
	{
		// Token: 0x06005216 RID: 21014 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_NPCBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
