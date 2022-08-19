using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Deps;

namespace KBEngine
{
	// Token: 0x02000C51 RID: 3153
	public class NetworkInterfaceKCP : NetworkInterfaceBase
	{
		// Token: 0x06005594 RID: 21908 RVA: 0x0023923A File Offset: 0x0023743A
		protected override Socket createSocket()
		{
			return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x00239245 File Offset: 0x00237445
		protected override PacketReceiverBase createPacketReceiver()
		{
			return new PacketReceiverKCP(this);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0023924D File Offset: 0x0023744D
		protected override PacketSenderBase createPacketSender()
		{
			return new PacketSenderKCP(this);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x00239255 File Offset: 0x00237455
		public override void reset()
		{
			this.finiKCP();
			base.reset();
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00239264 File Offset: 0x00237464
		public override void close()
		{
			this.finiKCP();
			base.close();
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x00239273 File Offset: 0x00237473
		public override bool valid()
		{
			return this.kcp_ != null && this._socket != null && this.connected;
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x0023928D File Offset: 0x0023748D
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

		// Token: 0x0600559B RID: 21915 RVA: 0x002392CC File Offset: 0x002374CC
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

		// Token: 0x0600559C RID: 21916 RVA: 0x00239363 File Offset: 0x00237563
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

		// Token: 0x0600559D RID: 21917 RVA: 0x002393A1 File Offset: 0x002375A1
		public KCP kcp()
		{
			return this.kcp_;
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x002393AC File Offset: 0x002375AC
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

		// Token: 0x0600559F RID: 21919 RVA: 0x0023940C File Offset: 0x0023760C
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

		// Token: 0x060055A0 RID: 21920 RVA: 0x00239462 File Offset: 0x00237662
		protected override void onAsyncConnectCB(NetworkInterfaceBase.ConnectState state)
		{
			if (state.error.Length > 0 || !this.initKCP())
			{
				return;
			}
			this.connected = true;
			this.remoteEndPint = new IPEndPoint(IPAddress.Parse(state.connectIP), state.connectPort);
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x002394A0 File Offset: 0x002376A0
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

		// Token: 0x040050B6 RID: 20662
		private KCP kcp_;

		// Token: 0x040050B7 RID: 20663
		public uint connID;

		// Token: 0x040050B8 RID: 20664
		public uint nextTickKcpUpdate;

		// Token: 0x040050B9 RID: 20665
		public EndPoint remoteEndPint;
	}
}
