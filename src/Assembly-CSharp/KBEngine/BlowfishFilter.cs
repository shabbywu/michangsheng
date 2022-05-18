using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EE7 RID: 3815
	internal class BlowfishFilter : EncryptionFilter
	{
		// Token: 0x06005BD2 RID: 23506 RVA: 0x0024EF70 File Offset: 0x0024D170
		~BlowfishFilter()
		{
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0004099E File Offset: 0x0003EB9E
		public byte[] key()
		{
			return this._blowfish.key();
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x00251B60 File Offset: 0x0024FD60
		public override void encrypt(MemoryStream stream)
		{
			int num = 0;
			if (stream.length() % 8U != 0U)
			{
				num = (int)(8U - stream.length() % 8U);
				stream.wpos += num;
				if (stream.wpos > 5840)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"BlowfishFilter::encrypt: stream.wpos(",
						stream.wpos,
						") > MemoryStream.BUFFER_MAX(",
						5840,
						")!"
					}));
				}
			}
			this._blowfish.encipher(stream.data(), (int)stream.length());
			ushort v = (ushort)(stream.length() + 1U);
			this._enctyptStrem.writeUint16(v);
			this._enctyptStrem.writeUint8((byte)num);
			this._enctyptStrem.append(stream.data(), (uint)stream.rpos, stream.length());
			stream.swap(this._enctyptStrem);
			this._enctyptStrem.clear();
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x000409AB File Offset: 0x0003EBAB
		public override void decrypt(MemoryStream stream)
		{
			this._blowfish.decipher(stream.data(), stream.rpos, (int)stream.length());
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x000409CA File Offset: 0x0003EBCA
		public override void decrypt(byte[] buffer, int startIndex, int length)
		{
			this._blowfish.decipher(buffer, startIndex, length);
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x000409DA File Offset: 0x0003EBDA
		public override bool send(PacketSenderBase sender, MemoryStream stream)
		{
			if (!this._blowfish.isGood())
			{
				Dbg.ERROR_MSG("BlowfishFilter::send: Dropping packet, due to invalid filter");
				return false;
			}
			this.encrypt(stream);
			return sender.send(stream);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x00251C5C File Offset: 0x0024FE5C
		public override bool recv(MessageReaderBase reader, byte[] buffer, uint rpos, uint len)
		{
			if (!this._blowfish.isGood())
			{
				Dbg.ERROR_MSG("BlowfishFilter::recv: Dropping packet, due to invalid filter");
				return false;
			}
			if (this._packet.length() == 0U && len >= 11U && (long)(BitConverter.ToUInt16(buffer, (int)rpos) - 1) == (long)((ulong)(len - 3U)))
			{
				int num = (int)(BitConverter.ToUInt16(buffer, (int)rpos) - 1);
				int num2 = (int)buffer[(int)(rpos + 2U)];
				this.decrypt(buffer, (int)(rpos + 3U), (int)(len - 3U));
				int length = num - num2;
				if (reader != null)
				{
					reader.process(buffer, rpos + 3U, (uint)length);
				}
				return true;
			}
			this._packet.append(buffer, rpos, len);
			while (this._packet.length() > 0U)
			{
				uint num3 = 0U;
				int wpos = 0;
				if (this._packLen <= 0)
				{
					if (this._packet.length() < 11U)
					{
						return false;
					}
					this._packLen = this._packet.readUint16();
					this._padSize = this._packet.readUint8();
					this._packLen -= 1;
					if (this._packet.length() > (uint)this._packLen)
					{
						num3 = (uint)(this._packet.rpos + (int)this._packLen);
						wpos = this._packet.wpos;
						this._packet.wpos = (int)num3;
					}
					else if (this._packet.length() < (uint)this._packLen)
					{
						return false;
					}
				}
				else if (this._packet.length() > (uint)this._packLen)
				{
					num3 = (uint)(this._packet.rpos + (int)this._packLen);
					wpos = this._packet.wpos;
					this._packet.wpos = (int)num3;
				}
				else if (this._packet.length() < (uint)this._packLen)
				{
					return false;
				}
				this.decrypt(this._packet);
				this._packet.wpos -= (int)this._padSize;
				if (reader != null)
				{
					reader.process(this._packet.data(), (uint)this._packet.rpos, this._packet.length());
				}
				if (num3 > 0U)
				{
					this._packet.rpos = (int)num3;
					this._packet.wpos = wpos;
				}
				else
				{
					this._packet.clear();
				}
				this._packLen = 0;
				this._padSize = 0;
			}
			return true;
		}

		// Token: 0x04005A21 RID: 23073
		private Blowfish _blowfish = new Blowfish(16);

		// Token: 0x04005A22 RID: 23074
		private MemoryStream _packet = new MemoryStream();

		// Token: 0x04005A23 RID: 23075
		private MemoryStream _enctyptStrem = new MemoryStream();

		// Token: 0x04005A24 RID: 23076
		private UINT8 _padSize = 0;

		// Token: 0x04005A25 RID: 23077
		private ushort _packLen;

		// Token: 0x04005A26 RID: 23078
		private const uint BLOCK_SIZE = 8U;

		// Token: 0x04005A27 RID: 23079
		private const uint MIN_PACKET_SIZE = 11U;
	}
}
