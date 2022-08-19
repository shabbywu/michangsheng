using System;

namespace KBEngine
{
	// Token: 0x02000B7B RID: 2939
	public class EntityBaseEntityCall_MailBase : EntityCall
	{
		// Token: 0x0600520D RID: 21005 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_MailBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
