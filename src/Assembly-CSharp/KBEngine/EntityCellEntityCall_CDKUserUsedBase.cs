using System;

namespace KBEngine
{
	// Token: 0x02000B76 RID: 2934
	public class EntityCellEntityCall_CDKUserUsedBase : EntityCall
	{
		// Token: 0x06005207 RID: 20999 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_CDKUserUsedBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
