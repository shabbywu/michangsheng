using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onEntityLeaveSpace : Message
{
	public Message_Client_onEntityLeaveSpace(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		int eid = msgstream.readInt32();
		KBEngineApp.app.Client_onEntityLeaveSpace(eid);
	}
}
