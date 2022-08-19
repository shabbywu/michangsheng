using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B6A RID: 2922
	internal class BlowfishFilter : EncryptionFilter
	{
		// Token: 0x06005196 RID: 20886 RVA: 0x002229B0 File Offset: 0x00220BB0
		~BlowfishFilter()
		{
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x002229D8 File Offset: 0x00220BD8
		public byte[] key()
		{
			return this._blowfish.key();
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x002229E8 File Offset: 0x00220BE8
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

		// Token: 0x06005199 RID: 20889 RVA: 0x00222AE2 File Offset: 0x00220CE2
		public override void decrypt(MemoryStream stream)
		{
			this._blowfish.decipher(stream.data(), stream.rpos, (int)stream.length());
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x00222B01 File Offset: 0x00220D01
		public override void decrypt(byte[] buffer, int startIndex, int length)
		{
			this._blowfish.decipher(buffer, startIndex, length);
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00222B11 File Offset: 0x00220D11
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

		// Token: 0x0600519C RID: 20892 RVA: 0x00222B3C File Offset: 0x00220D3C
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

		// Token: 0x04004F96 RID: 20374
		private Blowfish _blowfish = new Blowfish(16);

		// Token: 0x04004F97 RID: 20375
		private MemoryStream _packet = new MemoryStream();

		// Token: 0x04004F98 RID: 20376
		private MemoryStream _enctyptStrem = new MemoryStream();

		// Token: 0x04004F99 RID: 20377
		private UINT8 _padSize = 0;

		// Token: 0x04004F9A RID: 20378
		private ushort _packLen;

		// Token: 0x04004F9B RID: 20379
		private const uint BLOCK_SIZE = 8U;

		// Token: 0x04004F9C RID: 20380
		private const uint MIN_PACKET_SIZE = 11U;
	}
}
