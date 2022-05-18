using System;

namespace KBEngine
{
	// Token: 0x02000F0D RID: 3853
	public class EntityBaseEntityCall_SpawnPointBase : EntityCall
	{
		// Token: 0x06005C62 RID: 23650 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_SpawnPointBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
