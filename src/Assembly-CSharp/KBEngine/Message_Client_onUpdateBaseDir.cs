using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onUpdateBaseDir : Message
{
	public Message_Client_onUpdateBaseDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		KBEngineApp.app.Client_onUpdateBaseDir(msgstream);
	}
}
