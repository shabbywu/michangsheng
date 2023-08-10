namespace KBEngine;

public class EntityCellEntityCall_AccountBase : EntityCall
{
	public EntityCellEntityCall_AccountBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
