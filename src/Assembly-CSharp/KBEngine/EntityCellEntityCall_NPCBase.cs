namespace KBEngine;

public class EntityCellEntityCall_NPCBase : EntityCall
{
	public EntityCellEntityCall_NPCBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
