using System;

namespace KBEngine
{
	// Token: 0x02000B74 RID: 2932
	public class EntityCellEntityCall_CDKBase : EntityCall
	{
		// Token: 0x06005205 RID: 20997 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_CDKBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
