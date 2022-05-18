using System;

namespace KBEngine
{
	// Token: 0x02000EF5 RID: 3829
	public class EntityBaseEntityCall_DroppedItemBase : EntityCall
	{
		// Token: 0x06005C44 RID: 23620 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_DroppedItemBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
