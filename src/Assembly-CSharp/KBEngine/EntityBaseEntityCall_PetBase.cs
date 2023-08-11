namespace KBEngine;

public class EntityBaseEntityCall_PetBase : EntityCall
{
	public EntityBaseEntityCall_PetBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
