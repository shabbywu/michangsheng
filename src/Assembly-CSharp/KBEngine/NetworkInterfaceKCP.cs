using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Deps;

namespace KBEngine
{
	// Token: 0x02000FD9 RID: 4057
	public class NetworkInterfaceKCP : NetworkInterfaceBase
	{
		// Token: 0x06005FDB RID: 24539 RVA: 0x00042B48 File Offset: 0x00040D48
		protected override Socket createSocket()
		{
			return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x00042B53 File Offset: 0x00040D53
		protected override PacketReceiverBase createPacketReceiver()
		{
			return new PacketReceiverKCP(this);
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x00042B5B File Offset: 0x00040D5B
		protected override PacketSenderBase createPacketSender()
		{
			return new PacketSenderKCP(this);
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00042B63 File Offset: 0x00040D63
		public override void reset()
		{
			this.finiKCP();
			base.reset();
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x00042B72 File Offset: 0x00040D72
		public override void close()
		{
			this.finiKCP();
			base.close();
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x00042B81 File Offset: 0x00040D81
		public override bool valid()
		{
			return this.kcp_ != null && this._socket != null && this.connected;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x00042B9B File Offset: 0x00040D9B
		protected void outputKCP(byte[] data, int size, object userData)
		{
			if (!this.valid())
			{
				throw new ArgumentException("invalid socket!");
			}
			if (this._packetSender == null)
			{
				this._packetSender = this.createPacketSender();
			}
			((PacketSenderKCP)this._packetSender).sendto(data, size);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0026629C File Offset: 0x0026449C
		private bool initKCP()
		{
			this.kcp_ = new KCP(this.connID, this);
			this.kcp_.SetOutput(new KCP.OutputDelegate(this.outputKCP));
			this.kcp_.SetMTU(1400);
			this.kcp_.WndSize(KBEngineApp.app.getInitArgs().getUDPSendBufferSize(), KBEngineApp.app.getInitArgs().getUDPRecvBufferSize());
			this.kcp_.NoDelay(1, 10, 2, 1);
			this.kcp_.SetMinRTO(10);
			this.nextTickKcpUpdate = 0U;
			return true;
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x00042BD7 File Offset: 0x00040DD7
		private bool finiKCP()
		{
			if (this.kcp_ != null)
			{
				this.kcp_.SetOutput(null);
				this.kcp_.Release();
				this.kcp_ = null;
			}
			this.remoteEndPint = null;
			this.connID = 0U;
			this.nextTickKcpUpdate = 0U;
			return true;
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x00042C15 File Offset: 0x00040E15
		public KCP kcp()
		{
			return this.kcp_;
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x00266334 File Offset: 0x00264534
		public override bool send(MemoryStream stream)
		{
			if (!this.valid())
			{
				throw new ArgumentException("invalid socket!");
			}
			if (this._filter != null)
			{
				this._filter.encrypt(stream);
			}
			this.nextTickKcpUpdate = 0U;
			return this.kcp_.Send(stream.data(), stream.rpos, (int)stream.length()) >= 0;
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x00266394 File Offset: 0x00264594
		public override void process()
		{
			if (!this.valid())
			{
				return;
			}
			uint num = KCP.TimeUtils.iclock();
			if (num >= this.nextTickKcpUpdate)
			{
				this.kcp_.Update(num);
				this.nextTickKcpUpdate = this.kcp_.Check(num);
			}
			if (this._packetReceiver != null)
			{
				this._packetReceiver.process();
			}
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00042C1D File Offset: 0x00040E1D
		protected override void onAsyncConnectCB(NetworkInterfaceBase.ConnectState state)
		{
			if (state.error.Length > 0 || !this.initKCP())
			{
				return;
			}
			this.connected = true;
			this.remoteEndPint = new IPEndPoint(IPAddress.Parse(state.connectIP), state.connectPort);
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x002663EC File Offset: 0x002645EC
		protected override void onAsyncConnect(NetworkInterfaceBase.ConnectState state)
		{
			try
			{
				byte[] bytes = Encoding.ASCII.GetBytes("62a559f3fa7748bc22f8e0766019d498");
				state.socket.SendTo(bytes, bytes.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(state.connectIP), state.connectPort));
				ArrayList arrayList = new ArrayList();
				arrayList.Add(state.socket);
				Socket.Select(arrayList, null, null, 3000000);
				if (arrayList.Count > 0)
				{
					byte[] array = new byte[1472];
					int num = state.socket.Receive(array);
					if (num <= 0)
					{
						Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{0}:{1}'! receive hello-ack error!", state.connectIP, state.connectPort));
						state.error = "receive hello-ack error!";
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream();
						Array.Copy(array, 0, memoryStream.data(), memoryStream.wpos, num);
						memoryStream.wpos = num;
						string text = memoryStream.readString();
						string text2 = memoryStream.readString();
						uint num2 = memoryStream.readUint32();
						if (text != "1432ad7c829170a76dd31982c3501eca")
						{
							Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{0}:{1}'! receive hello-ack({2}!={3}) mismatch!", new object[]
							{
								state.connectIP,
								state.connectPort,
								text,
								"1432ad7c829170a76dd31982c3501eca"
							}));
							state.error = "hello-ack mismatch!";
						}
						else if (KBEngineApp.app.serverVersion != text2)
						{
							Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{0}:{1}'! version({2}!={3}) mismatch!", new object[]
							{
								state.connectIP,
								state.connectPort,
								text2,
								KBEngineApp.app.serverVersion
							}));
							state.error = "version mismatch!";
						}
						else if (num2 == 0U)
						{
							Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{0}:{1}'! conv is 0!", state.connectIP, state.connectPort));
							state.error = "kcp conv error!";
						}
						((NetworkInterfaceKCP)state.networkInterface).connID = num2;
					}
				}
				else
				{
					Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), connect to '{0}:{1}' timeout!'", state.connectIP, state.connectPort));
					state.error = "timeout!";
				}
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), connect to '{0}:{1}' fault! error = '{2}'", state.connectIP, state.connectPort, ex));
				state.error = ex.ToString();
			}
		}

		// Token: 0x04005B66 RID: 23398
		private KCP kcp_;

		// Token: 0x04005B67 RID: 23399
		public uint connID;

		// Token: 0x04005B68 RID: 23400
		public uint nextTickKcpUpdate;

		// Token: 0x04005B69 RID: 23401
		public EndPoint remoteEndPint;
	}
}
