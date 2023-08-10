namespace KBEngine;

public class EntityBaseEntityCall_SpacesBase : EntityCall
{
	public EntityBaseEntityCall_SpacesBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
