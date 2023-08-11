namespace KBEngine;

public class EntityCellEntityCall_SpaceDuplicateBase : EntityCall
{
	public EntityCellEntityCall_SpaceDuplicateBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
