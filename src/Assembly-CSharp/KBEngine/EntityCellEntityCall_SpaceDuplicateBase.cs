using System;

namespace KBEngine
{
	// Token: 0x02000B8C RID: 2956
	public class EntityCellEntityCall_SpaceDuplicateBase : EntityCall
	{
		// Token: 0x06005223 RID: 21027 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_SpaceDuplicateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
