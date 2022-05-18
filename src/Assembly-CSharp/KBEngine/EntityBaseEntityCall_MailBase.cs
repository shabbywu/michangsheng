using System;

namespace KBEngine
{
	// Token: 0x02000EF9 RID: 3833
	public class EntityBaseEntityCall_MailBase : EntityCall
	{
		// Token: 0x06005C49 RID: 23625 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_MailBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
