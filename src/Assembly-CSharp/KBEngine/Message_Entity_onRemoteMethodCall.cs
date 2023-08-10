using System.Collections.Generic;

namespace KBEngine;

public class Message_Entity_onRemoteMethodCall : Message
{
	public Message_Entity_onRemoteMethodCall(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
