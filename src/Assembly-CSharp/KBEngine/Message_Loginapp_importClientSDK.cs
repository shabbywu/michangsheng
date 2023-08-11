using System.Collections.Generic;

namespace KBEngine;

public class Message_Loginapp_importClientSDK : Message
{
	public Message_Loginapp_importClientSDK(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
