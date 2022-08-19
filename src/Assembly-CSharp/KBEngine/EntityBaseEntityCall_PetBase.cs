using System;

namespace KBEngine
{
	// Token: 0x02000B85 RID: 2949
	public class EntityBaseEntityCall_PetBase : EntityCall
	{
		// Token: 0x06005219 RID: 21017 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_PetBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
