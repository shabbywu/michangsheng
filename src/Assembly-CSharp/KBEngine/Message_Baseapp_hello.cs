using System.Collections.Generic;

namespace KBEngine;

public class Message_Baseapp_hello : Message
{
	public Message_Baseapp_hello(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
