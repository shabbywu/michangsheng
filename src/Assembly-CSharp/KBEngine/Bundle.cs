using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B37 RID: 2871
	public class Bundle : ObjectPool<Bundle>
	{
		// Token: 0x0600508C RID: 20620 RVA: 0x00220254 File Offset: 0x0021E454
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

		// Token: 0x0600508D RID: 20621 RVA: 0x002202E3 File Offset: 0x0021E4E3
		public void reclaimObject()
		{
			this.clear();
			ObjectPool<Bundle>.reclaimObject(this);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x002202F4 File Offset: 0x0021E4F4
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

		// Token: 0x0600508F RID: 20623 RVA: 0x00220354 File Offset: 0x0021E554
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

		// Token: 0x06005090 RID: 20624 RVA: 0x002203CC File Offset: 0x0021E5CC
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

		// Token: 0x06005091 RID: 20625 RVA: 0x0022041C File Offset: 0x0021E61C
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

		// Token: 0x06005092 RID: 20626 RVA: 0x00220478 File Offset: 0x0021E678
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

		// Token: 0x06005093 RID: 20627 RVA: 0x002204CD File Offset: 0x0021E6CD
		public void writeInt8(sbyte v)
		{
			this.checkStream(1);
			this.stream.writeInt8(v);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x002204E2 File Offset: 0x0021E6E2
		public void writeInt16(short v)
		{
			this.checkStream(2);
			this.stream.writeInt16(v);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x002204F7 File Offset: 0x0021E6F7
		public void writeInt32(int v)
		{
			this.checkStream(4);
			this.stream.writeInt32(v);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x0022050C File Offset: 0x0021E70C
		public void writeInt64(long v)
		{
			this.checkStream(8);
			this.stream.writeInt64(v);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00220521 File Offset: 0x0021E721
		public void writeUint8(byte v)
		{
			this.checkStream(1);
			this.stream.writeUint8(v);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00220536 File Offset: 0x0021E736
		public void writeUint16(ushort v)
		{
			this.checkStream(2);
			this.stream.writeUint16(v);
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x0022054B File Offset: 0x0021E74B
		public void writeUint32(uint v)
		{
			this.checkStream(4);
			this.stream.writeUint32(v);
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x00220560 File Offset: 0x0021E760
		public void writeUint64(ulong v)
		{
			this.checkStream(8);
			this.stream.writeUint64(v);
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x00220575 File Offset: 0x0021E775
		public void writeFloat(float v)
		{
			this.checkStream(4);
			this.stream.writeFloat(v);
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0022058A File Offset: 0x0021E78A
		public void writeDouble(double v)
		{
			this.checkStream(8);
			this.stream.writeDouble(v);
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0022059F File Offset: 0x0021E79F
		public void writeString(string v)
		{
			this.checkStream(v.Length + 1);
			this.stream.writeString(v);
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x002205BB File Offset: 0x0021E7BB
		public void writeUnicode(string v)
		{
			this.writeBlob(Encoding.UTF8.GetBytes(v));
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x002205CE File Offset: 0x0021E7CE
		public void writeBlob(byte[] v)
		{
			this.checkStream(v.Length + 4);
			this.stream.writeBlob(v);
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x002205E7 File Offset: 0x0021E7E7
		public void writePython(byte[] v)
		{
			this.writeBlob(v);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x002205F0 File Offset: 0x0021E7F0
		public void writeVector2(Vector2 v)
		{
			this.checkStream(8);
			this.stream.writeVector2(v);
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x00220605 File Offset: 0x0021E805
		public void writeVector3(Vector3 v)
		{
			this.checkStream(12);
			this.stream.writeVector3(v);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0022061B File Offset: 0x0021E81B
		public void writeVector4(Vector4 v)
		{
			this.checkStream(16);
			this.stream.writeVector4(v);
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x00220634 File Offset: 0x0021E834
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

		// Token: 0x04004F71 RID: 20337
		public MemoryStream stream = new MemoryStream();

		// Token: 0x04004F72 RID: 20338
		public List<MemoryStream> streamList = new List<MemoryStream>();

		// Token: 0x04004F73 RID: 20339
		public int numMessage;

		// Token: 0x04004F74 RID: 20340
		public int messageLength;

		// Token: 0x04004F75 RID: 20341
		public Message msgtype;

		// Token: 0x04004F76 RID: 20342
		private int _curMsgStreamIndex;
	}
}
