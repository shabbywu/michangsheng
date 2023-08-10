namespace KBEngine;

public class EntityCellEntityCall_OderBase : EntityCall
{
	public EntityCellEntityCall_OderBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
