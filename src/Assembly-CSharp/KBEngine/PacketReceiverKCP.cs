using System;
using System.Net.Sockets;
using Deps;

namespace KBEngine
{
	// Token: 0x02000C57 RID: 3159
	public class PacketReceiverKCP : PacketReceiverBase
	{
		// Token: 0x060055D6 RID: 21974 RVA: 0x0023A406 File Offset: 0x00238606
		public PacketReceiverKCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this._buffer = new byte[65583];
			this._messageReader = new MessageReaderKCP();
			this.kcp_ = ((NetworkInterfaceKCP)networkInterface).kcp();
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0023A43C File Offset: 0x0023863C
		~PacketReceiverKCP()
		{
			this.kcp_ = null;
			Dbg.DEBUG_MSG("PacketReceiverKCP::~PacketReceiverKCP(), destroyed!");
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x0023A474 File Offset: 0x00238674
		public override void process()
		{
			Socket socket = this._networkInterface.sock();
			while (socket.Available > 0)
			{
				int num = 0;
				try
				{
					num = socket.Receive(this._buffer);
				}
				catch (Exception ex)
				{
					Dbg.ERROR_MSG("PacketReceiverKCP::process: " + ex.ToString());
					Event.fireIn("_closeNetwork", new object[]
					{
						this._networkInterface
					});
					break;
				}
				if (num <= 0)
				{
					Dbg.WARNING_MSG("PacketReceiverKCP::_asyncReceive(): KCP Receive <= 0!");
					return;
				}
				((NetworkInterfaceKCP)this._networkInterface).nextTickKcpUpdate = 0U;
				if (this.kcp_.Input(this._buffer, 0, num) < 0)
				{
					Dbg.WARNING_MSG(string.Format("PacketReceiverKCP::_asyncReceive(): KCP Input get {0}!", num));
					return;
				}
				for (;;)
				{
					num = this.kcp_.Recv(this._buffer, 0, this._buffer.Length);
					if (num < 0)
					{
						break;
					}
					if (this._networkInterface.fileter() != null)
					{
						this._networkInterface.fileter().recv(this._messageReader, this._buffer, 0U, (uint)num);
					}
					else
					{
						this._messageReader.process(this._buffer, 0U, (uint)num);
					}
				}
			}
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x00004095 File Offset: 0x00002295
		public override void startRecv()
		{
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x00004095 File Offset: 0x00002295
		protected override void _asyncReceive()
		{
		}

		// Token: 0x040050CA RID: 20682
		private byte[] _buffer;

		// Token: 0x040050CB RID: 20683
		private KCP kcp_;
	}
}
