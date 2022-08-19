using System;

namespace KBEngine
{
	// Token: 0x02000B79 RID: 2937
	public class EntityBaseEntityCall_GateBase : EntityCall
	{
		// Token: 0x0600520B RID: 21003 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_GateBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
