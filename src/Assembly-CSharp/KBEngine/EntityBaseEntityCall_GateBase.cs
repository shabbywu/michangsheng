using System;

namespace KBEngine
{
	// Token: 0x02000EF7 RID: 3831
	public class EntityBaseEntityCall_GateBase : EntityCall
	{
		// Token: 0x06005C47 RID: 23623 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_GateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
