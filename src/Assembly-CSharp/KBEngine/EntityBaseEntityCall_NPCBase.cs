using System;

namespace KBEngine
{
	// Token: 0x02000B81 RID: 2945
	public class EntityBaseEntityCall_NPCBase : EntityCall
	{
		// Token: 0x06005215 RID: 21013 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_NPCBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
