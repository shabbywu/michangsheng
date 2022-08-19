using System;

namespace KBEngine
{
	// Token: 0x02000B7C RID: 2940
	public class EntityCellEntityCall_MailBase : EntityCall
	{
		// Token: 0x0600520E RID: 21006 RVA: 0x00223AFA File Offset: 0x00221CFA
		public EntityCellEntityCall_MailBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
