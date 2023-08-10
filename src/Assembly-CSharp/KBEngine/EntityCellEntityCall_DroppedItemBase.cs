namespace KBEngine;

public class EntityCellEntityCall_DroppedItemBase : EntityCall
{
	public EntityCellEntityCall_DroppedItemBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}

	public void pickUpRequest()
	{
		if (newCall("pickUpRequest", 0) != null)
		{
			sendCall(null);
		}
	}
}
