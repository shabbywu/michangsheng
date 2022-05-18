using System;

namespace KBEngine
{
	// Token: 0x02000F0B RID: 3851
	public class EntityBaseEntityCall_SpacesBase : EntityCall
	{
		// Token: 0x06005C60 RID: 23648 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_SpacesBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}
	}
}
