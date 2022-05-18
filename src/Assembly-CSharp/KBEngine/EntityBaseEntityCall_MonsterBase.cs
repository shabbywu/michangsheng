using System;

namespace KBEngine
{
	// Token: 0x02000EFD RID: 3837
	public class EntityBaseEntityCall_MonsterBase : EntityCall
	{
		// Token: 0x06005C4F RID: 23631 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_MonsterBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
