namespace KBEngine;

public class EntityBaseEntityCall_SpawnPointBase : EntityCall
{
	public EntityBaseEntityCall_SpawnPointBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
