using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KBEngine;

public class Bundle : ObjectPool<Bundle>
{
	public MemoryStream stream = new MemoryStream();

	public List<MemoryStream> streamList = new List<MemoryStream>();

	public int numMessage;

	public int messageLength;

	public Message msgtype;

	private int _curMsgStreamIndex;

	public void clear()
	{
		for (int i = 0; i < streamList.Count; i++)
		{
			if (stream != streamList[i])
			{
				streamList[i].reclaimObject();
			}
		}
		streamList.Clear();
		if (stream != null)
		{
			stream.clear();
		}
		else
		{
			stream = ObjectPool<MemoryStream>.createObject();
		}
		numMessage = 0;
		messageLength = 0;
		msgtype = null;
		_curMsgStreamIndex = 0;
	}

	public void reclaimObject()
	{
		clear();
		ObjectPool<Bundle>.reclaimObject(this);
	}

	public void newMessage(Message mt)
	{
		fini(issend: false);
		msgtype = mt;
		numMessage++;
		writeUint16(msgtype.id);
		if (msgtype.msglen == -1)
		{
			writeUint16(0);
			messageLength = 0;
		}
		_curMsgStreamIndex = 0;
	}

	public void writeMsgLength()
	{
		if (msgtype.msglen == -1)
		{
			MemoryStream memoryStream = stream;
			if (_curMsgStreamIndex > 0)
			{
				memoryStream = streamList[streamList.Count - _curMsgStreamIndex];
			}
			memoryStream.data()[2] = (byte)((uint)messageLength & 0xFFu);
			memoryStream.data()[3] = (byte)((uint)(messageLength >> 8) & 0xFFu);
		}
	}

	public void fini(bool issend)
	{
		if (numMessage > 0)
		{
			writeMsgLength();
			streamList.Add(stream);
			stream = ObjectPool<MemoryStream>.createObject();
		}
		if (issend)
		{
			numMessage = 0;
			msgtype = null;
		}
		_curMsgStreamIndex = 0;
	}

	public void send(NetworkInterfaceBase networkInterface)
	{
		fini(issend: true);
		if (networkInterface.valid())
		{
			for (int i = 0; i < streamList.Count; i++)
			{
				MemoryStream memoryStream = streamList[i];
				networkInterface.send(memoryStream);
			}
		}
		else
		{
			Dbg.ERROR_MSG("Bundle::send: networkInterface invalid!");
		}
		reclaimObject();
	}

	public void checkStream(int v)
	{
		if (v > stream.space())
		{
			streamList.Add(stream);
			stream = ObjectPool<MemoryStream>.createObject();
			_curMsgStreamIndex++;
		}
		messageLength += v;
	}

	public void writeInt8(sbyte v)
	{
		checkStream(1);
		stream.writeInt8(v);
	}

	public void writeInt16(short v)
	{
		checkStream(2);
		stream.writeInt16(v);
	}

	public void writeInt32(int v)
	{
		checkStream(4);
		stream.writeInt32(v);
	}

	public void writeInt64(long v)
	{
		checkStream(8);
		stream.writeInt64(v);
	}

	public void writeUint8(byte v)
	{
		checkStream(1);
		stream.writeUint8(v);
	}

	public void writeUint16(ushort v)
	{
		checkStream(2);
		stream.writeUint16(v);
	}

	public void writeUint32(uint v)
	{
		checkStream(4);
		stream.writeUint32(v);
	}

	public void writeUint64(ulong v)
	{
		checkStream(8);
		stream.writeUint64(v);
	}

	public void writeFloat(float v)
	{
		checkStream(4);
		stream.writeFloat(v);
	}

	public void writeDouble(double v)
	{
		checkStream(8);
		stream.writeDouble(v);
	}

	public void writeString(string v)
	{
		checkStream(v.Length + 1);
		stream.writeString(v);
	}

	public void writeUnicode(string v)
	{
		writeBlob(Encoding.UTF8.GetBytes(v));
	}

	public void writeBlob(byte[] v)
	{
		checkStream(v.Length + 4);
		stream.writeBlob(v);
	}

	public void writePython(byte[] v)
	{
		writeBlob(v);
	}

	public void writeVector2(Vector2 v)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		checkStream(8);
		stream.writeVector2(v);
	}

	public void writeVector3(Vector3 v)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		checkStream(12);
		stream.writeVector3(v);
	}

	public void writeVector4(Vector4 v)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		checkStream(16);
		stream.writeVector4(v);
	}

	public void writeEntitycall(byte[] v)
	{
		checkStream(16);
		ulong num = 0uL;
		int num2 = 0;
		ushort num3 = 0;
		ushort num4 = 0;
		stream.writeUint64(num);
		stream.writeInt32(num2);
		stream.writeUint16(num3);
		stream.writeUint16(num4);
	}
}
