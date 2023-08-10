using System;

namespace KBEngine;

public class MessageReaderTCP : MessageReaderBase
{
	private enum READ_STATE
	{
		READ_STATE_MSGID,
		READ_STATE_MSGLEN,
		READ_STATE_MSGLEN_EX,
		READ_STATE_BODY
	}

	private ushort msgid;

	private ushort msglen;

	private uint expectSize = 2u;

	private READ_STATE state;

	private MemoryStream stream = new MemoryStream();

	public override void process(byte[] datas, uint offset, uint length)
	{
		uint num = offset;
		while (length != 0 && expectSize != 0)
		{
			if (state == READ_STATE.READ_STATE_MSGID)
			{
				if (length < expectSize)
				{
					Array.Copy(datas, num, stream.data(), stream.wpos, length);
					stream.wpos += (int)length;
					expectSize -= length;
					break;
				}
				Array.Copy(datas, num, stream.data(), stream.wpos, expectSize);
				num += expectSize;
				stream.wpos += (int)expectSize;
				length -= expectSize;
				msgid = stream.readUint16();
				stream.clear();
				Message message = Messages.clientMessages[msgid];
				if (message.msglen == -1)
				{
					state = READ_STATE.READ_STATE_MSGLEN;
					expectSize = 2u;
				}
				else if (message.msglen == 0)
				{
					message.handleMessage(stream);
					state = READ_STATE.READ_STATE_MSGID;
					expectSize = 2u;
				}
				else
				{
					expectSize = (uint)message.msglen;
					state = READ_STATE.READ_STATE_BODY;
				}
			}
			else if (state == READ_STATE.READ_STATE_MSGLEN)
			{
				if (length < expectSize)
				{
					Array.Copy(datas, num, stream.data(), stream.wpos, length);
					stream.wpos += (int)length;
					expectSize -= length;
					break;
				}
				Array.Copy(datas, num, stream.data(), stream.wpos, expectSize);
				num += expectSize;
				stream.wpos += (int)expectSize;
				length -= expectSize;
				msglen = stream.readUint16();
				stream.clear();
				if (msglen >= ushort.MaxValue)
				{
					state = READ_STATE.READ_STATE_MSGLEN_EX;
					expectSize = 4u;
				}
				else
				{
					state = READ_STATE.READ_STATE_BODY;
					expectSize = msglen;
				}
			}
			else if (state == READ_STATE.READ_STATE_MSGLEN_EX)
			{
				if (length < expectSize)
				{
					Array.Copy(datas, num, stream.data(), stream.wpos, length);
					stream.wpos += (int)length;
					expectSize -= length;
					break;
				}
				Array.Copy(datas, num, stream.data(), stream.wpos, expectSize);
				num += expectSize;
				stream.wpos += (int)expectSize;
				length -= expectSize;
				expectSize = stream.readUint32();
				stream.clear();
				state = READ_STATE.READ_STATE_BODY;
			}
			else if (state == READ_STATE.READ_STATE_BODY)
			{
				if (length < expectSize)
				{
					stream.append(datas, num, length);
					expectSize -= length;
					break;
				}
				stream.append(datas, num, expectSize);
				num += expectSize;
				length -= expectSize;
				Messages.clientMessages[msgid].handleMessage(stream);
				stream.clear();
				state = READ_STATE.READ_STATE_MSGID;
				expectSize = 2u;
			}
		}
	}
}
