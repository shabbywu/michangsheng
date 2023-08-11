namespace KBEngine;

public class EntityBaseEntityCall_MailBase : EntityCall
{
	public EntityBaseEntityCall_MailBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}
}
