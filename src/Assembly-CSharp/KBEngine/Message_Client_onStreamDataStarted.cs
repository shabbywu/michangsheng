using System.Collections.Generic;

namespace KBEngine;

public class Message_Client_onStreamDataStarted : Message
{
	public Message_Client_onStreamDataStarted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		: base(msgid, msgname, length, argstype, msgargtypes)
	{
	}

	public override void handleMessage(MemoryStream msgstream)
	{
		short num = msgstream.readInt16();
		uint datasize = msgstream.readUint32();
		string descr = msgstream.readString();
		KBEngineApp.app.Client_onStreamDataStarted(num, datasize, descr);
	}
}
