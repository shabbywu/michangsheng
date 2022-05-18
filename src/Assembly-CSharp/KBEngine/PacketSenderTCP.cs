using System;
using System.Net.Sockets;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000FE5 RID: 4069
	public class PacketSenderTCP : PacketSenderBase
	{
		// Token: 0x0600603B RID: 24635 RVA: 0x00042E83 File Offset: 0x00041083
		public PacketSenderTCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this._buffer = new byte[KBEngineApp.app.getInitArgs().TCP_SEND_BUFFER_MAX];
			this._wpos = 0;
			this._spos = 0;
			this._sending = 0;
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x00267710 File Offset: 0x00265910
		~PacketSenderTCP()
		{
			Dbg.DEBUG_MSG("PacketSenderTCP::~PacketSenderTCP(), destroyed!");
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x00267740 File Offset: 0x00265940
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

		// Token: 0x0600603E RID: 24638 RVA: 0x002678C8 File Offset: 0x00265AC8
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

		// Token: 0x04005B84 RID: 23428
		private byte[] _buffer;

		// Token: 0x04005B85 RID: 23429
		private int _wpos;

		// Token: 0x04005B86 RID: 23430
		private int _spos;

		// Token: 0x04005B87 RID: 23431
		private int _sending;
	}
}
