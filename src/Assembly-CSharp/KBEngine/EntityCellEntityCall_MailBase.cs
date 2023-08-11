namespace KBEngine;

public class EntityCellEntityCall_MailBase : EntityCall
{
	public EntityCellEntityCall_MailBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}
}
