namespace KBEngine;

public class EntityCellEntityCall_MatchBase : EntityCall
{
	public EntityCellEntityCall_MatchBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
