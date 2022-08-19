using System;

namespace KBEngine
{
	// Token: 0x02000B8E RID: 2958
	public class EntityCellEntityCall_SpacesBase : EntityCall
	{
		// Token: 0x06005225 RID: 21029 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_SpacesBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
