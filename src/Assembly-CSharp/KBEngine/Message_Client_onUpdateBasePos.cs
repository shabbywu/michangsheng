using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onUpdateBasePos : Message
{
	public Message_Client_onUpdateBasePos(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		float x = msgstream.readFloat();
		float y = msgstream.readFloat();
		float z = msgstream.readFloat();
		KBEngineApp.app.Client_onUpdateBasePos(x, y, z);
	}
}
