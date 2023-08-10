using System.Collections.Generic;

namespace KBEngine;

public class Message_Loginapp_reqAccountResetPassword : Message
{
	public Message_Loginapp_reqAccountResetPassword(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
