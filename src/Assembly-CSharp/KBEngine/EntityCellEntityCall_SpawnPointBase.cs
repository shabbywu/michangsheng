namespace KBEngine;

public class EntityCellEntityCall_SpawnPointBase : EntityCall
{
	public EntityCellEntityCall_SpawnPointBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
