using System;

namespace KBEngine
{
	// Token: 0x02000B8B RID: 2955
	public class EntityBaseEntityCall_SpaceDuplicateBase : EntityCall
	{
		// Token: 0x06005222 RID: 21026 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_SpaceDuplicateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
