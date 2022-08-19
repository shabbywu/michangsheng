using System;

namespace KBEngine
{
	// Token: 0x02000B80 RID: 2944
	public class EntityCellEntityCall_MonsterBase : EntityCall
	{
		// Token: 0x06005214 RID: 21012 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_MonsterBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
