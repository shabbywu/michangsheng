using System;

namespace KBEngine
{
	// Token: 0x02000F61 RID: 3937
	public class MessageReaderTCP : MessageReaderBase
	{
		// Token: 0x06005EB2 RID: 24242 RVA: 0x00262980 File Offset: 0x00260B80
		public override void process(byte[] datas, uint offset, uint length)
		{
			uint num = offset;
			while (length > 0U && this.expectSize > 0U)
			{
				if (this.state == MessageReaderTCP.READ_STATE.READ_STATE_MSGID)
				{
					if (length < this.expectSize)
					{
						Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)length));
						this.stream.wpos += (int)length;
						this.expectSize -= length;
						return;
					}
					Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)this.expectSize));
					num += this.expectSize;
					this.stream.wpos += (int)this.expectSize;
					length -= this.expectSize;
					this.msgid = this.stream.readUint16();
					this.stream.clear();
					Message message = Messages.clientMessages[this.msgid];
					if (message.msglen == -1)
					{
						this.state = MessageReaderTCP.READ_STATE.READ_STATE_MSGLEN;
						this.expectSize = 2U;
					}
					else if (message.msglen == 0)
					{
						message.handleMessage(this.stream);
						this.state = MessageReaderTCP.READ_STATE.READ_STATE_MSGID;
						this.expectSize = 2U;
					}
					else
					{
						this.expectSize = (uint)message.msglen;
						this.state = MessageReaderTCP.READ_STATE.READ_STATE_BODY;
					}
				}
				else if (this.state == MessageReaderTCP.READ_STATE.READ_STATE_MSGLEN)
				{
					if (length < this.expectSize)
					{
						Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)length));
						this.stream.wpos += (int)length;
						this.expectSize -= length;
						return;
					}
					Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)this.expectSize));
					num += this.expectSize;
					this.stream.wpos += (int)this.expectSize;
					length -= this.expectSize;
					this.msglen = this.stream.readUint16();
					this.stream.clear();
					if (this.msglen >= 65535)
					{
						this.state = MessageReaderTCP.READ_STATE.READ_STATE_MSGLEN_EX;
						this.expectSize = 4U;
					}
					else
					{
						this.state = MessageReaderTCP.READ_STATE.READ_STATE_BODY;
						this.expectSize = (uint)this.msglen;
					}
				}
				else if (this.state == MessageReaderTCP.READ_STATE.READ_STATE_MSGLEN_EX)
				{
					if (length < this.expectSize)
					{
						Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)length));
						this.stream.wpos += (int)length;
						this.expectSize -= length;
						return;
					}
					Array.Copy(datas, (long)((ulong)num), this.stream.data(), (long)this.stream.wpos, (long)((ulong)this.expectSize));
					num += this.expectSize;
					this.stream.wpos += (int)this.expectSize;
					length -= this.expectSize;
					this.expectSize = this.stream.readUint32();
					this.stream.clear();
					this.state = MessageReaderTCP.READ_STATE.READ_STATE_BODY;
				}
				else if (this.state == MessageReaderTCP.READ_STATE.READ_STATE_BODY)
				{
					if (length < this.expectSize)
					{
						this.stream.append(datas, num, length);
						this.expectSize -= length;
						return;
					}
					this.stream.append(datas, num, this.expectSize);
					num += this.expectSize;
					length -= this.expectSize;
					Messages.clientMessages[this.msgid].handleMessage(this.stream);
					this.stream.clear();
					this.state = MessageReaderTCP.READ_STATE.READ_STATE_MSGID;
					this.expectSize = 2U;
				}
			}
		}

		// Token: 0x04005B26 RID: 23334
		private ushort msgid;

		// Token: 0x04005B27 RID: 23335
		private ushort msglen;

		// Token: 0x04005B28 RID: 23336
		private uint expectSize = 2U;

		// Token: 0x04005B29 RID: 23337
		private MessageReaderTCP.READ_STATE state;

		// Token: 0x04005B2A RID: 23338
		private MemoryStream stream = new MemoryStream();

		// Token: 0x02000F62 RID: 3938
		private enum READ_STATE
		{
			// Token: 0x04005B2C RID: 23340
			READ_STATE_MSGID,
			// Token: 0x04005B2D RID: 23341
			READ_STATE_MSGLEN,
			// Token: 0x04005B2E RID: 23342
			READ_STATE_MSGLEN_EX,
			// Token: 0x04005B2F RID: 23343
			READ_STATE_BODY
		}
	}
}
