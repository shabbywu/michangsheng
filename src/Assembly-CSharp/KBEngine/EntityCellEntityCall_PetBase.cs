using System;

namespace KBEngine
{
	// Token: 0x02000F04 RID: 3844
	public class EntityCellEntityCall_PetBase : EntityCall
	{
		// Token: 0x06005C56 RID: 23638 RVA: 0x00040E9C File Offset: 0x0003F09C
		public EntityCellEntityCall_PetBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}
	}
}
