using System;

namespace KBEngine
{
	// Token: 0x02000B90 RID: 2960
	public class EntityCellEntityCall_SpawnPointBase : EntityCall
	{
		// Token: 0x06005227 RID: 21031 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_SpawnPointBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
