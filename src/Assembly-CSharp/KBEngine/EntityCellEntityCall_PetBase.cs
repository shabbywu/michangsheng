namespace KBEngine;

public class EntityCellEntityCall_PetBase : EntityCall
{
	public EntityCellEntityCall_PetBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
