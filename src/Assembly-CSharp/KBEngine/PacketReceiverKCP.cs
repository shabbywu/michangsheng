using System;
using System.Net.Sockets;
using Deps;

namespace KBEngine
{
	// Token: 0x02000FE0 RID: 4064
	public class PacketReceiverKCP : PacketReceiverBase
	{
		// Token: 0x06006021 RID: 24609 RVA: 0x00042D87 File Offset: 0x00040F87
		public PacketReceiverKCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this._buffer = new byte[65583];
			this._messageReader = new MessageReaderKCP();
			this.kcp_ = ((NetworkInterfaceKCP)networkInterface).kcp();
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x002671FC File Offset: 0x002653FC
		~PacketReceiverKCP()
		{
			this.kcp_ = null;
			Dbg.DEBUG_MSG("PacketReceiverKCP::~PacketReceiverKCP(), destroyed!");
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x00267234 File Offset: 0x00265434
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

		// Token: 0x06006024 RID: 24612 RVA: 0x000042DD File Offset: 0x000024DD
		public override void startRecv()
		{
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x000042DD File Offset: 0x000024DD
		protected override void _asyncReceive()
		{
		}

		// Token: 0x04005B7A RID: 23418
		private byte[] _buffer;

		// Token: 0x04005B7B RID: 23419
		private KCP kcp_;
	}
}
