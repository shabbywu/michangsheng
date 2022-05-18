using System;
using System.Net.Sockets;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000FE1 RID: 4065
	public class PacketReceiverTCP : PacketReceiverBase
	{
		// Token: 0x06006026 RID: 24614 RVA: 0x00042DBC File Offset: 0x00040FBC
		public PacketReceiverTCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this._buffer = new byte[KBEngineApp.app.getInitArgs().TCP_RECV_BUFFER_MAX];
			this._messageReader = new MessageReaderTCP();
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x00267364 File Offset: 0x00265564
		~PacketReceiverTCP()
		{
			Dbg.DEBUG_MSG("PacketReceiverTCP::~PacketReceiverTCP(), destroyed!");
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x00267394 File Offset: 0x00265594
		public override void process()
		{
			int num = Interlocked.Add(ref this._wpos, 0);
			if (this._rpos < num)
			{
				if (this._networkInterface.fileter() != null)
				{
					this._networkInterface.fileter().recv(this._messageReader, this._buffer, (uint)this._rpos, (uint)(num - this._rpos));
				}
				else
				{
					this._messageReader.process(this._buffer, (uint)this._rpos, (uint)(num - this._rpos));
				}
				Interlocked.Exchange(ref this._rpos, num);
				return;
			}
			if (num < this._rpos)
			{
				if (this._networkInterface.fileter() != null)
				{
					this._networkInterface.fileter().recv(this._messageReader, this._buffer, (uint)this._rpos, (uint)(this._buffer.Length - this._rpos));
					this._networkInterface.fileter().recv(this._messageReader, this._buffer, 0U, (uint)num);
				}
				else
				{
					this._messageReader.process(this._buffer, (uint)this._rpos, (uint)(this._buffer.Length - this._rpos));
					this._messageReader.process(this._buffer, 0U, (uint)num);
				}
				Interlocked.Exchange(ref this._rpos, num);
			}
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x002674D0 File Offset: 0x002656D0
		private int _free()
		{
			int num = Interlocked.Add(ref this._rpos, 0);
			if (this._wpos == this._buffer.Length)
			{
				if (num == 0)
				{
					return 0;
				}
				Interlocked.Exchange(ref this._wpos, 0);
			}
			if (num <= this._wpos)
			{
				return this._buffer.Length - this._wpos;
			}
			return num - this._wpos - 1;
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x00267530 File Offset: 0x00265730
		protected override void _asyncReceive()
		{
			if (this._networkInterface == null || !this._networkInterface.valid())
			{
				Dbg.WARNING_MSG("PacketReceiverTCP::_asyncReceive(): network interface invalid!");
				return;
			}
			Socket socket = this._networkInterface.sock();
			for (;;)
			{
				int num = 0;
				int num2;
				for (num2 = this._free(); num2 == 0; num2 = this._free())
				{
					if (num > 0)
					{
						if (num > 1000)
						{
							goto Block_3;
						}
						Dbg.WARNING_MSG("PacketReceiverTCP::_asyncReceive(): waiting for space, Please adjust 'RECV_BUFFER_MAX'! retries=" + num);
						Thread.Sleep(5);
					}
					num++;
				}
				int num3 = 0;
				try
				{
					num3 = socket.Receive(this._buffer, this._wpos, num2, SocketFlags.None);
				}
				catch (SocketException arg)
				{
					Dbg.ERROR_MSG(string.Format("PacketReceiverTCP::_asyncReceive(): receive error, disconnect from '{0}'! error = '{1}'", socket.RemoteEndPoint, arg));
					Event.fireIn("_closeNetwork", new object[]
					{
						this._networkInterface
					});
					return;
				}
				if (num3 <= 0)
				{
					goto IL_F3;
				}
				Interlocked.Add(ref this._wpos, num3);
			}
			Block_3:
			Dbg.ERROR_MSG("PacketReceiverTCP::_asyncReceive(): no space!");
			Event.fireIn("_closeNetwork", new object[]
			{
				this._networkInterface
			});
			return;
			IL_F3:
			Dbg.WARNING_MSG(string.Format("PacketReceiverTCP::_asyncReceive(): receive 0 bytes, disconnect from '{0}'!", socket.RemoteEndPoint));
			Event.fireIn("_closeNetwork", new object[]
			{
				this._networkInterface
			});
		}

		// Token: 0x04005B7C RID: 23420
		private byte[] _buffer;

		// Token: 0x04005B7D RID: 23421
		private int _wpos;

		// Token: 0x04005B7E RID: 23422
		private int _rpos;
	}
}
