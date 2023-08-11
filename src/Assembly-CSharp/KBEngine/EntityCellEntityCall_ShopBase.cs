namespace KBEngine;

public class EntityCellEntityCall_ShopBase : EntityCall
{
	public EntityCellEntityCall_ShopBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
