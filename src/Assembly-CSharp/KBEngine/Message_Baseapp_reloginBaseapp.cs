using System.Collections.Generic;

namespace KBEngine;

public class Message_Baseapp_reloginBaseapp : Message
{
	public Message_Baseapp_reloginBaseapp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
	}
}
