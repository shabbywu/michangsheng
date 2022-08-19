using System;

namespace KBEngine
{
	// Token: 0x02000B7F RID: 2943
	public class EntityBaseEntityCall_MonsterBase : EntityCall
	{
		// Token: 0x06005213 RID: 21011 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_MonsterBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
