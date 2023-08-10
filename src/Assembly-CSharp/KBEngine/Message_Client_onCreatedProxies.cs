using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onCreatedProxies : Message
{
	public Message_Client_onCreatedProxies(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		ulong rndUUID = msgstream.readUint64();
		int eid = msgstream.readInt32();
		string entityType = msgstream.readString();
		KBEngineApp.app.Client_onCreatedProxies(rndUUID, eid, entityType);
	}
}
