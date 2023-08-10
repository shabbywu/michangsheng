namespace KBEngine;

public class EntityBaseEntityCall_DroppedItemBase : EntityCall
{
	public EntityBaseEntityCall_DroppedItemBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
