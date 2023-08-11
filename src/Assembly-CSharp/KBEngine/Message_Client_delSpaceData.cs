using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_delSpaceData : Message
{
	public Message_Client_delSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		uint spaceID = msgstream.readUint32();
		string key = msgstream.readString();
		KBEngineApp.app.Client_delSpaceData(spaceID, key);
	}
}
