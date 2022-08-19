using System;

namespace KBEngine
{
	// Token: 0x02000B89 RID: 2953
	public class EntityBaseEntityCall_SpaceBase : EntityCall
	{
		// Token: 0x06005220 RID: 21024 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_SpaceBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
