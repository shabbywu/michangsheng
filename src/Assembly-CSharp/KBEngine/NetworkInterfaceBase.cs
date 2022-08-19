using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace KBEngine
{
	// Token: 0x02000C50 RID: 3152
	public abstract class NetworkInterfaceBase
	{
		// Token: 0x0600557E RID: 21886 RVA: 0x00238D92 File Offset: 0x00236F92
		public NetworkInterfaceBase()
		{
			this.reset();
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00238DA0 File Offset: 0x00236FA0
		~NetworkInterfaceBase()
		{
			Dbg.DEBUG_MSG("NetworkInterfaceBase::~NetworkInterfaceBase(), destructed!!!");
			this.reset();
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00238DD8 File Offset: 0x00236FD8
		public virtual Socket sock()
		{
			return this._socket;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x00238DE0 File Offset: 0x00236FE0
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

		// Token: 0x06005582 RID: 21890 RVA: 0x00238E68 File Offset: 0x00237068
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

		// Token: 0x06005583 RID: 21891
		protected abstract PacketReceiverBase createPacketReceiver();

		// Token: 0x06005584 RID: 21892
		protected abstract PacketSenderBase createPacketSender();

		// Token: 0x06005585 RID: 21893
		protected abstract Socket createSocket();

		// Token: 0x06005586 RID: 21894
		protected abstract void onAsyncConnect(NetworkInterfaceBase.ConnectState state);

		// Token: 0x06005587 RID: 21895 RVA: 0x00238EA2 File Offset: 0x002370A2
		public virtual PacketReceiverBase packetReceiver()
		{
			return this._packetReceiver;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00238EAA File Offset: 0x002370AA
		public virtual PacketSenderBase PacketSender()
		{
			return this._packetSender;
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x00238EB2 File Offset: 0x002370B2
		public virtual bool valid()
		{
			return this._socket != null && this._socket.Connected;
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00238ECC File Offset: 0x002370CC
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

		// Token: 0x0600558B RID: 21899 RVA: 0x00238FAC File Offset: 0x002371AC
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

		// Token: 0x0600558C RID: 21900 RVA: 0x0023901C File Offset: 0x0023721C
		private void _asyncConnect(NetworkInterfaceBase.ConnectState state)
		{
			Dbg.DEBUG_MSG(string.Format("NetworkInterfaceBase::_asyncConnect(), will connect to '{0}:{1}' ...", state.connectIP, state.connectPort));
			this.onAsyncConnect(state);
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void onAsyncConnectCB(NetworkInterfaceBase.ConnectState state)
		{
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00239048 File Offset: 0x00237248
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

		// Token: 0x0600558F RID: 21903 RVA: 0x002390B8 File Offset: 0x002372B8
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

		// Token: 0x06005590 RID: 21904 RVA: 0x002391B0 File Offset: 0x002373B0
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

		// Token: 0x06005591 RID: 21905 RVA: 0x0023920B File Offset: 0x0023740B
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

		// Token: 0x06005592 RID: 21906 RVA: 0x00239229 File Offset: 0x00237429
		public EncryptionFilter fileter()
		{
			return this._filter;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x00239231 File Offset: 0x00237431
		public void setFilter(EncryptionFilter filter)
		{
			this._filter = filter;
		}

		// Token: 0x040050AD RID: 20653
		public const int TCP_PACKET_MAX = 1460;

		// Token: 0x040050AE RID: 20654
		public const int UDP_PACKET_MAX = 1472;

		// Token: 0x040050AF RID: 20655
		public const string UDP_HELLO = "62a559f3fa7748bc22f8e0766019d498";

		// Token: 0x040050B0 RID: 20656
		public const string UDP_HELLO_ACK = "1432ad7c829170a76dd31982c3501eca";

		// Token: 0x040050B1 RID: 20657
		protected Socket _socket;

		// Token: 0x040050B2 RID: 20658
		protected PacketReceiverBase _packetReceiver;

		// Token: 0x040050B3 RID: 20659
		protected PacketSenderBase _packetSender;

		// Token: 0x040050B4 RID: 20660
		protected EncryptionFilter _filter;

		// Token: 0x040050B5 RID: 20661
		public bool connected;

		// Token: 0x020015FB RID: 5627
		// (Invoke) Token: 0x060085A5 RID: 34213
		public delegate void AsyncConnectMethod(NetworkInterfaceBase.ConnectState state);

		// Token: 0x020015FC RID: 5628
		// (Invoke) Token: 0x060085A9 RID: 34217
		public delegate void ConnectCallback(string ip, int port, bool success, object userData);

		// Token: 0x020015FD RID: 5629
		public class ConnectState
		{
			// Token: 0x04007100 RID: 28928
			public string connectIP = "";

			// Token: 0x04007101 RID: 28929
			public int connectPort;

			// Token: 0x04007102 RID: 28930
			public NetworkInterfaceBase.ConnectCallback connectCB;

			// Token: 0x04007103 RID: 28931
			public object userData;

			// Token: 0x04007104 RID: 28932
			public Socket socket;

			// Token: 0x04007105 RID: 28933
			public NetworkInterfaceBase networkInterface;

			// Token: 0x04007106 RID: 28934
			public string error = "";
		}
	}
}
