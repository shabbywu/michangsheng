using System;

namespace KBEngine
{
	// Token: 0x02000EF1 RID: 3825
	public class EntityBaseEntityCall_CDKBase : EntityCall
	{
		// Token: 0x06005C40 RID: 23616 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_CDKBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
