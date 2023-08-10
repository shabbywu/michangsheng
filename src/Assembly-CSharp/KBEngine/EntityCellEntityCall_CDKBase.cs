namespace KBEngine;

public class EntityCellEntityCall_CDKBase : EntityCall
{
	public EntityCellEntityCall_CDKBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
