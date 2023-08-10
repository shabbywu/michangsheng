namespace KBEngine;

public class EntityCellEntityCall_BuildBase : EntityCall
{
	public EntityCellEntityCall_BuildBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
