using System;

namespace KBEngine
{
	// Token: 0x02000F07 RID: 3847
	public class EntityBaseEntityCall_SpaceBase : EntityCall
	{
		// Token: 0x06005C5C RID: 23644 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_SpaceBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
