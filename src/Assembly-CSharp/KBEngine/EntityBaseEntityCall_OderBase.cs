using System;

namespace KBEngine
{
	// Token: 0x02000B83 RID: 2947
	public class EntityBaseEntityCall_OderBase : EntityCall
	{
		// Token: 0x06005217 RID: 21015 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_OderBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
