using System;

namespace KBEngine
{
	// Token: 0x02000B73 RID: 2931
	public class EntityBaseEntityCall_CDKBase : EntityCall
	{
		// Token: 0x06005204 RID: 20996 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_CDKBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
