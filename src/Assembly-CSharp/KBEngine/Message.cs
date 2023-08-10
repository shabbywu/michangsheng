using System.Collections.Generic;

namespace KBEngine;

public class Message
{
	public ushort id;

	public string name;

	public short msglen = -1;

	public List<byte> argtypes;

	public sbyte argsType;

	public Message(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
	{
		id = msgid;
		name = msgname;
		msglen = length;
		argsType = argstype;
		argtypes = msgargtypes;
	}

	public virtual void handleMessage(MemoryStream msgstream)
	{
	}
}
