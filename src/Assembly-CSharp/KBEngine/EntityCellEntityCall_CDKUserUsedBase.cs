namespace KBEngine;

public class EntityCellEntityCall_CDKUserUsedBase : EntityCall
{
	public EntityCellEntityCall_CDKUserUsedBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
