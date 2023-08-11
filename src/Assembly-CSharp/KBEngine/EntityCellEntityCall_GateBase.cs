namespace KBEngine;

public class EntityCellEntityCall_GateBase : EntityCall
{
	public EntityCellEntityCall_GateBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
