namespace KBEngine;

public class EntityBaseEntityCall_MonsterBase : EntityCall
{
	public EntityBaseEntityCall_MonsterBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
