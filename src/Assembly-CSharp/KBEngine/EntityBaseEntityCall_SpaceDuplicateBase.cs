namespace KBEngine;

public class EntityBaseEntityCall_SpaceDuplicateBase : EntityCall
{
	public EntityBaseEntityCall_SpaceDuplicateBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
