using System;

namespace KBEngine
{
	// Token: 0x02000F01 RID: 3841
	public class EntityBaseEntityCall_OderBase : EntityCall
	{
		// Token: 0x06005C53 RID: 23635 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_OderBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
