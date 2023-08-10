namespace KBEngine;

public class EntityCellEntityCall_SpacesBase : EntityCall
{
	public EntityCellEntityCall_SpacesBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
