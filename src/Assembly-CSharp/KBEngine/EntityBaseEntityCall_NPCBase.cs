using System;

namespace KBEngine
{
	// Token: 0x02000EFF RID: 3839
	public class EntityBaseEntityCall_NPCBase : EntityCall
	{
		// Token: 0x06005C51 RID: 23633 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_NPCBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
