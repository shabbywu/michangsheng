using System;

namespace KBEngine
{
	// Token: 0x02000B8F RID: 2959
	public class EntityBaseEntityCall_SpawnPointBase : EntityCall
	{
		// Token: 0x06005226 RID: 21030 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_SpawnPointBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
