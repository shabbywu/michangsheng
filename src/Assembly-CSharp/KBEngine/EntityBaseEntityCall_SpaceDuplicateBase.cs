using System;

namespace KBEngine
{
	// Token: 0x02000F09 RID: 3849
	public class EntityBaseEntityCall_SpaceDuplicateBase : EntityCall
	{
		// Token: 0x06005C5E RID: 23646 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_SpaceDuplicateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
