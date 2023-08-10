namespace KBEngine;

public class EntityBaseEntityCall_NPCBase : EntityCall
{
	public EntityBaseEntityCall_NPCBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
