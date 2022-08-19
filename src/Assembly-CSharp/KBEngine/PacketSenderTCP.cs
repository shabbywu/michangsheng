using System;
using System.Net.Sockets;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000C5B RID: 3163
	public class PacketSenderTCP : PacketSenderBase
	{
		// Token: 0x060055EC RID: 21996 RVA: 0x0023AA44 File Offset: 0x00238C44
		public PacketSenderTCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this._buffer = new byte[KBEngineApp.app.getInitArgs().TCP_SEND_BUFFER_MAX];
			this._wpos = 0;
			this._spos = 0;
			this._sending = 0;
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0023AA7C File Offset: 0x00238C7C
		~PacketSenderTCP()
		{
			Dbg.DEBUG_MSG("PacketSenderTCP::~PacketSenderTCP(), destroyed!");
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0023AAAC File Offset: 0x00238CAC
		public override bool send(MemoryStream stream)
		{
			int num = (int)stream.length();
			if (num <= 0)
			{
				return true;
			}
			if (Interlocked.Add(ref this._sending, 0) == 0 && this._wpos == this._spos)
			{
				this._wpos = 0;
				this._spos = 0;
			}
			int num2 = Interlocked.Add(ref this._spos, 0);
			int num3 = this._wpos % this._buffer.Length;
			int num4 = num2 % this._buffer.Length;
			int num5;
			if (num3 >= num4)
			{
				num5 = this._buffer.Length - num3 + num4 - 1;
			}
			else
			{
				num5 = num4 - num3 - 1;
			}
			if (num > num5)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					"PacketSenderTCP::send(): no space, Please adjust 'SEND_BUFFER_MAX'! data(",
					num,
					") > space(",
					num5,
					"), wpos=",
					this._wpos,
					", spos=",
					num2
				}));
				return false;
			}
			int num6 = num3 + num;
			if (num6 <= this._buffer.Length)
			{
				Array.Copy(stream.data(), stream.rpos, this._buffer, num3, num);
			}
			else
			{
				int num7 = this._buffer.Length - num3;
				Array.Copy(stream.data(), stream.rpos, this._buffer, num3, num7);
				Array.Copy(stream.data(), stream.rpos + num7, this._buffer, 0, num6 - this._buffer.Length);
			}
			Interlocked.Add(ref this._wpos, num);
			if (Interlocked.CompareExchange(ref this._sending, 1, 0) == 0)
			{
				base._startSend();
			}
			return true;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0023AC34 File Offset: 0x00238E34
		protected override void _asyncSend()
		{
			if (this._networkInterface == null || !this._networkInterface.valid())
			{
				Dbg.WARNING_MSG("PacketSenderTCP::_asyncSend(): network interface invalid!");
				return;
			}
			Socket socket = this._networkInterface.sock();
			int value;
			do
			{
				int num = Interlocked.Add(ref this._wpos, 0) - this._spos;
				int num2 = this._spos % this._buffer.Length;
				if (num2 == 0)
				{
					num2 = num;
				}
				if (num > this._buffer.Length - num2)
				{
					num = this._buffer.Length - num2;
				}
				value = 0;
				try
				{
					value = socket.Send(this._buffer, this._spos % this._buffer.Length, num, SocketFlags.None);
				}
				catch (SocketException arg)
				{
					Dbg.ERROR_MSG(string.Format("PacketSenderTCP::_asyncSend(): send data error, disconnect from '{0}'! error = '{1}'", socket.RemoteEndPoint, arg));
					Event.fireIn("_closeNetwork", new object[]
					{
						this._networkInterface
					});
					return;
				}
			}
			while (Interlocked.Add(ref this._spos, value) != Interlocked.Add(ref this._wpos, 0));
			Interlocked.Exchange(ref this._sending, 0);
		}

		// Token: 0x040050D4 RID: 20692
		private byte[] _buffer;

		// Token: 0x040050D5 RID: 20693
		private int _wpos;

		// Token: 0x040050D6 RID: 20694
		private int _spos;

		// Token: 0x040050D7 RID: 20695
		private int _sending;
	}
}
