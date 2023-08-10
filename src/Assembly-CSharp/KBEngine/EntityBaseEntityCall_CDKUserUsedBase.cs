namespace KBEngine;

public class EntityBaseEntityCall_CDKUserUsedBase : EntityCall
{
	public EntityBaseEntityCall_CDKUserUsedBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
