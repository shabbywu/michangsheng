using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onStreamDataCompleted : Message
{
	public Message_Client_onStreamDataCompleted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		short num = msgstream.readInt16();
		KBEngineApp.app.Client_onStreamDataCompleted(num);
	}
}
