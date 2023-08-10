namespace KBEngine;

public class EntityBaseEntityCall_MatchBase : EntityCall
{
	public EntityBaseEntityCall_MatchBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}

	public void createTeam(byte[] arg1, ulong arg2)
	{
		if (newCall("createTeam", 0) != null)
		{
			bundle.writeEntitycall(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void joinTeam(ulong arg1, byte[] arg2, string arg3, uint arg4, ulong arg5)
	{
		if (newCall("joinTeam", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeEntitycall(arg2);
			bundle.writeUnicode(arg3);
			bundle.writeUint32(arg4);
			bundle.writeUint64(arg5);
			sendCall(null);
		}
	}
}
