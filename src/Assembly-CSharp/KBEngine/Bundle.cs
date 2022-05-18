using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EAE RID: 3758
	public class Bundle : ObjectPool<Bundle>
	{
		// Token: 0x06005AB6 RID: 23222 RVA: 0x0024FCA4 File Offset: 0x0024DEA4
		public void clear()
		{
			for (int i = 0; i < this.streamList.Count; i++)
			{
				if (this.stream != this.streamList[i])
				{
					this.streamList[i].reclaimObject();
				}
			}
			this.streamList.Clear();
			if (this.stream != null)
			{
				this.stream.clear();
			}
			else
			{
				this.stream = ObjectPool<MemoryStream>.createObject();
			}
			this.numMessage = 0;
			this.messageLength = 0;
			this.msgtype = null;
			this._curMsgStreamIndex = 0;
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x0003FFA0 File Offset: 0x0003E1A0
		public void reclaimObject()
		{
			this.clear();
			ObjectPool<Bundle>.reclaimObject(this);
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0024FD34 File Offset: 0x0024DF34
		public void newMessage(Message mt)
		{
			this.fini(false);
			this.msgtype = mt;
			this.numMessage++;
			this.writeUint16(this.msgtype.id);
			if (this.msgtype.msglen == -1)
			{
				this.writeUint16(0);
				this.messageLength = 0;
			}
			this._curMsgStreamIndex = 0;
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x0024FD94 File Offset: 0x0024DF94
		public void writeMsgLength()
		{
			if (this.msgtype.msglen != -1)
			{
				return;
			}
			MemoryStream memoryStream = this.stream;
			if (this._curMsgStreamIndex > 0)
			{
				memoryStream = this.streamList[this.streamList.Count - this._curMsgStreamIndex];
			}
			memoryStream.data()[2] = (byte)(this.messageLength & 255);
			memoryStream.data()[3] = (byte)(this.messageLength >> 8 & 255);
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x0024FE0C File Offset: 0x0024E00C
		public void fini(bool issend)
		{
			if (this.numMessage > 0)
			{
				this.writeMsgLength();
				this.streamList.Add(this.stream);
				this.stream = ObjectPool<MemoryStream>.createObject();
			}
			if (issend)
			{
				this.numMessage = 0;
				this.msgtype = null;
			}
			this._curMsgStreamIndex = 0;
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0024FE5C File Offset: 0x0024E05C
		public void send(NetworkInterfaceBase networkInterface)
		{
			this.fini(true);
			if (networkInterface.valid())
			{
				for (int i = 0; i < this.streamList.Count; i++)
				{
					MemoryStream memoryStream = this.streamList[i];
					networkInterface.send(memoryStream);
				}
			}
			else
			{
				Dbg.ERROR_MSG("Bundle::send: networkInterface invalid!");
			}
			this.reclaimObject();
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0024FEB8 File Offset: 0x0024E0B8
		public void checkStream(int v)
		{
			if ((long)v > (long)((ulong)this.stream.space()))
			{
				this.streamList.Add(this.stream);
				this.stream = ObjectPool<MemoryStream>.createObject();
				this._curMsgStreamIndex++;
			}
			this.messageLength += v;
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x0003FFAE File Offset: 0x0003E1AE
		public void writeInt8(sbyte v)
		{
			this.checkStream(1);
			this.stream.writeInt8(v);
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x0003FFC3 File Offset: 0x0003E1C3
		public void writeInt16(short v)
		{
			this.checkStream(2);
			this.stream.writeInt16(v);
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
		public void writeInt32(int v)
		{
			this.checkStream(4);
			this.stream.writeInt32(v);
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x0003FFED File Offset: 0x0003E1ED
		public void writeInt64(long v)
		{
			this.checkStream(8);
			this.stream.writeInt64(v);
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x00040002 File Offset: 0x0003E202
		public void writeUint8(byte v)
		{
			this.checkStream(1);
			this.stream.writeUint8(v);
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x00040017 File Offset: 0x0003E217
		public void writeUint16(ushort v)
		{
			this.checkStream(2);
			this.stream.writeUint16(v);
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x0004002C File Offset: 0x0003E22C
		public void writeUint32(uint v)
		{
			this.checkStream(4);
			this.stream.writeUint32(v);
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x00040041 File Offset: 0x0003E241
		public void writeUint64(ulong v)
		{
			this.checkStream(8);
			this.stream.writeUint64(v);
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x00040056 File Offset: 0x0003E256
		public void writeFloat(float v)
		{
			this.checkStream(4);
			this.stream.writeFloat(v);
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0004006B File Offset: 0x0003E26B
		public void writeDouble(double v)
		{
			this.checkStream(8);
			this.stream.writeDouble(v);
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x00040080 File Offset: 0x0003E280
		public void writeString(string v)
		{
			this.checkStream(v.Length + 1);
			this.stream.writeString(v);
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x0004009C File Offset: 0x0003E29C
		public void writeUnicode(string v)
		{
			this.writeBlob(Encoding.UTF8.GetBytes(v));
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x000400AF File Offset: 0x0003E2AF
		public void writeBlob(byte[] v)
		{
			this.checkStream(v.Length + 4);
			this.stream.writeBlob(v);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x000400C8 File Offset: 0x0003E2C8
		public void writePython(byte[] v)
		{
			this.writeBlob(v);
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x000400D1 File Offset: 0x0003E2D1
		public void writeVector2(Vector2 v)
		{
			this.checkStream(8);
			this.stream.writeVector2(v);
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x000400E6 File Offset: 0x0003E2E6
		public void writeVector3(Vector3 v)
		{
			this.checkStream(12);
			this.stream.writeVector3(v);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x000400FC File Offset: 0x0003E2FC
		public void writeVector4(Vector4 v)
		{
			this.checkStream(16);
			this.stream.writeVector4(v);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x0024FF10 File Offset: 0x0024E110
		public void writeEntitycall(byte[] v)
		{
			this.checkStream(16);
			ulong v2 = 0UL;
			int v3 = 0;
			ushort v4 = 0;
			ushort v5 = 0;
			this.stream.writeUint64(v2);
			this.stream.writeInt32(v3);
			this.stream.writeUint16(v4);
			this.stream.writeUint16(v5);
		}

		// Token: 0x040059F6 RID: 23030
		public MemoryStream stream = new MemoryStream();

		// Token: 0x040059F7 RID: 23031
		public List<MemoryStream> streamList = new List<MemoryStream>();

		// Token: 0x040059F8 RID: 23032
		public int numMessage;

		// Token: 0x040059F9 RID: 23033
		public int messageLength;

		// Token: 0x040059FA RID: 23034
		public Message msgtype;

		// Token: 0x040059FB RID: 23035
		private int _curMsgStreamIndex;
	}
}
