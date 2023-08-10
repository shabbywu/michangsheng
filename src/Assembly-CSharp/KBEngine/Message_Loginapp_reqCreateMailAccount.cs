using System.Collections.Generic;

namespace KBEngine;

public class Message_Loginapp_reqCreateMailAccount : Message
{
	public Message_Loginapp_reqCreateMailAccount(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
