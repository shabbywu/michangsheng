using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace KBEngine
{
	// Token: 0x02000FD5 RID: 4053
	public abstract class NetworkInterfaceBase
	{
		// Token: 0x06005FBC RID: 24508 RVA: 0x00042A5B File Offset: 0x00040C5B
		public NetworkInterfaceBase()
		{
			this.reset();
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x00265EC8 File Offset: 0x002640C8
		~NetworkInterfaceBase()
		{
			Dbg.DEBUG_MSG("NetworkInterfaceBase::~NetworkInterfaceBase(), destructed!!!");
			this.reset();
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x00042A69 File Offset: 0x00040C69
		public virtual Socket sock()
		{
			return this._socket;
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x00265F00 File Offset: 0x00264100
		public virtual void reset()
		{
			this._packetReceiver = null;
			this._packetSender = null;
			this._filter = null;
			this.connected = false;
			if (this._socket != null)
			{
				try
				{
					if (this._socket.RemoteEndPoint != null)
					{
						Dbg.DEBUG_MSG(string.Format("NetworkInterfaceBase::reset(), close socket from '{0}'", this._socket.RemoteEndPoint.ToString()));
					}
				}
				catch (Exception)
				{
				}
				this._socket.Close(0);
				this._socket = null;
			}
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x00042A71 File Offset: 0x00040C71
		public virtual void close()
		{
			if (this._socket != null)
			{
				this._socket.Close(0);
				this._socket = null;
				Event.fireAll("onDisconnected", Array.Empty<object>());
			}
			this._socket = null;
			this.connected = false;
		}

		// Token: 0x06005FC1 RID: 24513
		protected abstract PacketReceiverBase createPacketReceiver();

		// Token: 0x06005FC2 RID: 24514
		protected abstract PacketSenderBase createPacketSender();

		// Token: 0x06005FC3 RID: 24515
		protected abstract Socket createSocket();

		// Token: 0x06005FC4 RID: 24516
		protected abstract void onAsyncConnect(NetworkInterfaceBase.ConnectState state);

		// Token: 0x06005FC5 RID: 24517 RVA: 0x00042AAB File Offset: 0x00040CAB
		public virtual PacketReceiverBase packetReceiver()
		{
			return this._packetReceiver;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x00042AB3 File Offset: 0x00040CB3
		public virtual PacketSenderBase PacketSender()
		{
			return this._packetSender;
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x00042ABB File Offset: 0x00040CBB
		public virtual bool valid()
		{
			return this._socket != null && this._socket.Connected;
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x00265F88 File Offset: 0x00264188
		public void _onConnectionState(NetworkInterfaceBase.ConnectState state)
		{
			Event.deregisterIn(this);
			bool flag = state.error == "" && this.valid();
			if (flag)
			{
				Dbg.DEBUG_MSG(string.Format("NetworkInterfaceBase::_onConnectionState(), connect to {0}:{1} is success!", state.connectIP, state.connectPort));
				this._packetReceiver = this.createPacketReceiver();
				this._packetReceiver.startRecv();
				this.connected = true;
			}
			else
			{
				this.reset();
				Dbg.ERROR_MSG(string.Format("NetworkInterfaceBase::_onConnectionState(), connect error! ip: {0}:{1}, err: {2}", state.connectIP, state.connectPort, state.error));
			}
			Event.fireAll("onConnectionState", new object[]
			{
				flag
			});
			if (state.connectCB != null)
			{
				state.connectCB(state.connectIP, state.connectPort, flag, state.userData);
			}
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x00266068 File Offset: 0x00264268
		private static void connectCB(IAsyncResult ar)
		{
			NetworkInterfaceBase.ConnectState connectState = null;
			try
			{
				connectState = (NetworkInterfaceBase.ConnectState)ar.AsyncState;
				connectState.socket.EndConnect(ar);
				Event.fireIn("_onConnectionState", new object[]
				{
					connectState
				});
			}
			catch (Exception ex)
			{
				connectState.error = ex.ToString();
				Event.fireIn("_onConnectionState", new object[]
				{
					connectState
				});
			}
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x00042AD2 File Offset: 0x00040CD2
		private void _asyncConnect(NetworkInterfaceBase.ConnectState state)
		{
			Dbg.DEBUG_MSG(string.Format("NetworkInterfaceBase::_asyncConnect(), will connect to '{0}:{1}' ...", state.connectIP, state.connectPort));
			this.onAsyncConnect(state);
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void onAsyncConnectCB(NetworkInterfaceBase.ConnectState state)
		{
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x002660D8 File Offset: 0x002642D8
		private void _asyncConnectCB(IAsyncResult ar)
		{
			NetworkInterfaceBase.ConnectState connectState = (NetworkInterfaceBase.ConnectState)ar.AsyncState;
			NetworkInterfaceBase.AsyncConnectMethod asyncConnectMethod = (NetworkInterfaceBase.AsyncConnectMethod)((AsyncResult)ar).AsyncDelegate;
			this.onAsyncConnectCB(connectState);
			Dbg.DEBUG_MSG(string.Format("NetworkInterfaceBase::_asyncConnectCB(), connect to '{0}:{1}' finish. error = '{2}'", connectState.connectIP, connectState.connectPort, connectState.error));
			asyncConnectMethod.EndInvoke(ar);
			Event.fireIn("_onConnectionState", new object[]
			{
				connectState
			});
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x00266148 File Offset: 0x00264348
		public void connectTo(string ip, int port, NetworkInterfaceBase.ConnectCallback callback, object userData)
		{
			if (this.valid())
			{
				throw new InvalidOperationException("Have already connected!");
			}
			if (!new Regex("((?:(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d))))").IsMatch(ip))
			{
				ip = Dns.GetHostEntry(ip).AddressList[0].ToString();
			}
			this._socket = this.createSocket();
			NetworkInterfaceBase.ConnectState connectState = new NetworkInterfaceBase.ConnectState();
			connectState.connectIP = ip;
			connectState.connectPort = port;
			connectState.connectCB = callback;
			connectState.userData = userData;
			connectState.socket = this._socket;
			connectState.networkInterface = this;
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				"connect to ",
				ip,
				":",
				port,
				" ..."
			}));
			this.connected = false;
			Event.registerIn("_onConnectionState", this, "_onConnectionState");
			new NetworkInterfaceBase.AsyncConnectMethod(this._asyncConnect).BeginInvoke(connectState, new AsyncCallback(this._asyncConnectCB), connectState);
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00266240 File Offset: 0x00264440
		public virtual bool send(MemoryStream stream)
		{
			if (!this.valid())
			{
				throw new ArgumentException("invalid socket!");
			}
			if (this._packetSender == null)
			{
				this._packetSender = this.createPacketSender();
			}
			if (this._filter != null)
			{
				return this._filter.send(this._packetSender, stream);
			}
			return this._packetSender.send(stream);
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00042AFB File Offset: 0x00040CFB
		public virtual void process()
		{
			if (!this.valid())
			{
				return;
			}
			if (this._packetReceiver != null)
			{
				this._packetReceiver.process();
			}
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00042B19 File Offset: 0x00040D19
		public EncryptionFilter fileter()
		{
			return this._filter;
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x00042B21 File Offset: 0x00040D21
		public void setFilter(EncryptionFilter filter)
		{
			this._filter = filter;
		}

		// Token: 0x04005B56 RID: 23382
		public const int TCP_PACKET_MAX = 1460;

		// Token: 0x04005B57 RID: 23383
		public const int UDP_PACKET_MAX = 1472;

		// Token: 0x04005B58 RID: 23384
		public const string UDP_HELLO = "62a559f3fa7748bc22f8e0766019d498";

		// Token: 0x04005B59 RID: 23385
		public const string UDP_HELLO_ACK = "1432ad7c829170a76dd31982c3501eca";

		// Token: 0x04005B5A RID: 23386
		protected Socket _socket;

		// Token: 0x04005B5B RID: 23387
		protected PacketReceiverBase _packetReceiver;

		// Token: 0x04005B5C RID: 23388
		protected PacketSenderBase _packetSender;

		// Token: 0x04005B5D RID: 23389
		protected EncryptionFilter _filter;

		// Token: 0x04005B5E RID: 23390
		public bool connected;

		// Token: 0x02000FD6 RID: 4054
		// (Invoke) Token: 0x06005FD3 RID: 24531
		public delegate void AsyncConnectMethod(NetworkInterfaceBase.ConnectState state);

		// Token: 0x02000FD7 RID: 4055
		// (Invoke) Token: 0x06005FD7 RID: 24535
		public delegate void ConnectCallback(string ip, int port, bool success, object userData);

		// Token: 0x02000FD8 RID: 4056
		public class ConnectState
		{
			// Token: 0x04005B5F RID: 23391
			public string connectIP = "";

			// Token: 0x04005B60 RID: 23392
			public int connectPort;

			// Token: 0x04005B61 RID: 23393
			public NetworkInterfaceBase.ConnectCallback connectCB;

			// Token: 0x04005B62 RID: 23394
			public object userData;

			// Token: 0x04005B63 RID: 23395
			public Socket socket;

			// Token: 0x04005B64 RID: 23396
			public NetworkInterfaceBase networkInterface;

			// Token: 0x04005B65 RID: 23397
			public string error = "";
		}
	}
}
