namespace KBEngine;

public class EntityCellEntityCall_MonsterBase : EntityCall
{
	public EntityCellEntityCall_MonsterBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
