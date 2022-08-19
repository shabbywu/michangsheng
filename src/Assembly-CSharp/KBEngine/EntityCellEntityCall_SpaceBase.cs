using System;

namespace KBEngine
{
	// Token: 0x02000B8A RID: 2954
	public class EntityCellEntityCall_SpaceBase : EntityCall
	{
		// Token: 0x06005221 RID: 21025 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_SpaceBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
