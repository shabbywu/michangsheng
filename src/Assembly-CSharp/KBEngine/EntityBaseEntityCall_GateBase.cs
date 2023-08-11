namespace KBEngine;

public class EntityBaseEntityCall_GateBase : EntityCall
{
	public EntityBaseEntityCall_GateBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
