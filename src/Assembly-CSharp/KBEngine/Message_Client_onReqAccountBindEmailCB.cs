using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onReqAccountBindEmailCB : Message
{
	public Message_Client_onReqAccountBindEmailCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		ushort failcode = msgstream.readUint16();
		KBEngineApp.app.Client_onReqAccountBindEmailCB(failcode);
	}
}
