namespace KBEngine;

public class EntityBaseEntityCall_BuildBase : EntityCall
{
	public EntityBaseEntityCall_BuildBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
