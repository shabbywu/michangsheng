using System.Collections.Generic;

namespace KBEngine;

public class Message_Entity_forwardEntityMessageToCellappFromClient : Message
{
	public Message_Entity_forwardEntityMessageToCellappFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
