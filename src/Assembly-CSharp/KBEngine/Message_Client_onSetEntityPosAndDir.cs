using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onSetEntityPosAndDir : Message
{
	public Message_Client_onSetEntityPosAndDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		KBEngineApp.app.Client_onSetEntityPosAndDir(msgstream);
	}
}
