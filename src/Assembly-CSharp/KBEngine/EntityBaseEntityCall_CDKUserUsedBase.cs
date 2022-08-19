using System;

namespace KBEngine
{
	// Token: 0x02000B75 RID: 2933
	public class EntityBaseEntityCall_CDKUserUsedBase : EntityCall
	{
		// Token: 0x06005206 RID: 20998 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_CDKUserUsedBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
