using System;

namespace KBEngine
{
	// Token: 0x02000B8D RID: 2957
	public class EntityBaseEntityCall_SpacesBase : EntityCall
	{
		// Token: 0x06005224 RID: 21028 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_SpacesBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
