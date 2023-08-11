namespace KBEngine;

public class EntityBaseEntityCall_OderBase : EntityCall
{
	public EntityBaseEntityCall_OderBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
