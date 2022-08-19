using System;

namespace KBEngine
{
	// Token: 0x02000B71 RID: 2929
	public class EntityBaseEntityCall_BuildBase : EntityCall
	{
		// Token: 0x06005202 RID: 20994 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_BuildBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
