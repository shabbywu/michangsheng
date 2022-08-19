using System;

namespace KBEngine
{
	// Token: 0x02000B86 RID: 2950
	public class EntityCellEntityCall_PetBase : EntityCall
	{
		// Token: 0x0600521A RID: 21018 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_PetBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
