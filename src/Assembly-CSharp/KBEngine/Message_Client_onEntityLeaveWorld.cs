using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onEntityLeaveWorld : Message
{
	public Message_Client_onEntityLeaveWorld(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		int eid = msgstream.readInt32();
		KBEngineApp.app.Client_onEntityLeaveWorld(eid);
	}
}
