using System.Collections.Generic;

namespace KBEngine;

public class Message_Loginapp_importClientMessages : Message
{
	public Message_Loginapp_importClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
