namespace KBEngine;

public class EntityBaseEntityCall_CDKBase : EntityCall
{
	public EntityBaseEntityCall_CDKBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
