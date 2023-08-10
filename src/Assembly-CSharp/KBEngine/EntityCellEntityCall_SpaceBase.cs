namespace KBEngine;

public class EntityCellEntityCall_SpaceBase : EntityCall
{
	public EntityCellEntityCall_SpaceBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
