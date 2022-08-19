using System;

namespace KBEngine
{
	// Token: 0x02000B7E RID: 2942
	public class EntityCellEntityCall_MatchBase : EntityCall
	{
		// Token: 0x06005212 RID: 21010 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_MatchBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
