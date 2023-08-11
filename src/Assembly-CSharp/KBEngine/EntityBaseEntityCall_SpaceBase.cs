namespace KBEngine;

public class EntityBaseEntityCall_SpaceBase : EntityCall
{
	public EntityBaseEntityCall_SpaceBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
