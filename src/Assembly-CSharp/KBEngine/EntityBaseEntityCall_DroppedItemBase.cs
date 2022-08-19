using System;

namespace KBEngine
{
	// Token: 0x02000B77 RID: 2935
	public class EntityBaseEntityCall_DroppedItemBase : EntityCall
	{
		// Token: 0x06005208 RID: 21000 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_DroppedItemBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
