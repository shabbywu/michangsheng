using System;

namespace KBEngine
{
	// Token: 0x02000B72 RID: 2930
	public class EntityCellEntityCall_BuildBase : EntityCall
	{
		// Token: 0x06005203 RID: 20995 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_BuildBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
