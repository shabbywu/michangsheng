using System;

namespace KBEngine
{
	// Token: 0x02000F03 RID: 3843
	public class EntityBaseEntityCall_PetBase : EntityCall
	{
		// Token: 0x06005C55 RID: 23637 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_PetBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
