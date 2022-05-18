using System;

namespace KBEngine
{
	// Token: 0x02000EF3 RID: 3827
	public class EntityBaseEntityCall_CDKUserUsedBase : EntityCall
	{
		// Token: 0x06005C42 RID: 23618 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_CDKUserUsedBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
