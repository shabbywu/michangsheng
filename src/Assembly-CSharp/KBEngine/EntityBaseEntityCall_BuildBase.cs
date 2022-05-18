using System;

namespace KBEngine
{
	// Token: 0x02000EEF RID: 3823
	public class EntityBaseEntityCall_BuildBase : EntityCall
	{
		// Token: 0x06005C3E RID: 23614 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_BuildBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
