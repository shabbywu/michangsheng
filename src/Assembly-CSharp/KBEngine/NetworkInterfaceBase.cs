using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace KBEngine;

public abstract class NetworkInterfaceBase
{
	public delegate void AsyncConnectMethod(ConnectState state);

	public delegate void ConnectCallback(string ip, int port, bool success, object userData);

	public class ConnectState
	{
		public string connectIP = "";

		public int connectPort;

		public ConnectCallback connectCB;

		public object userData;

		public Socket socket;

		public NetworkInterfaceBase networkInterface;

		public string error = "";
	}

	public const int TCP_PACKET_MAX = 1460;

	public const int UDP_PACKET_MAX = 1472;

	public const string UDP_HELLO = "62a559f3fa7748bc22f8e0766019d498";

	public const string UDP_HELLO_ACK = "1432ad7c829170a76dd31982c3501eca";

	protected Socket _socket;

	protected PacketReceiverBase _packetReceiver;

	protected PacketSenderBase _packetSender;

	protected EncryptionFilter _filter;

	public bool connected;

	public NetworkInterfaceBase()
	{
		reset();
	}

	~NetworkInterfaceBase()
	{
		Dbg.DEBUG_MSG("NetworkInterfaceBase::~NetworkInterfaceBase(), destructed!!!");
		reset();
	}

	public virtual Socket sock()
	{
		return _socket;
	}

	public virtual void reset()
	{
		_packetReceiver = null;
		_packetSender = null;
		_filter = null;
		connected = false;
		if (_socket == null)
		{
			return;
		}
		try
		{
			if (_socket.RemoteEndPoint != null)
			{
				Dbg.DEBUG_MSG($"NetworkInterfaceBase::reset(), close socket from '{_socket.RemoteEndPoint.ToString()}'");
			}
		}
		catch (Exception)
		{
		}
		_socket.Close(0);
		_socket = null;
	}

	public virtual void close()
	{
		if (_socket != null)
		{
			_socket.Close(0);
			_socket = null;
			Event.fireAll("onDisconnected");
		}
		_socket = null;
		connected = false;
	}

	protected abstract PacketReceiverBase createPacketReceiver();

	protected abstract PacketSenderBase createPacketSender();

	protected abstract Socket createSocket();

	protected abstract void onAsyncConnect(ConnectState state);

	public virtual PacketReceiverBase packetReceiver()
	{
		return _packetReceiver;
	}

	public virtual PacketSenderBase PacketSender()
	{
		return _packetSender;
	}

	public virtual bool valid()
	{
		if (_socket != null)
		{
			return _socket.Connected;
		}
		return false;
	}

	public void _onConnectionState(ConnectState state)
	{
		Event.deregisterIn(this);
		bool flag = state.error == "" && valid();
		if (flag)
		{
			Dbg.DEBUG_MSG($"NetworkInterfaceBase::_onConnectionState(), connect to {state.connectIP}:{state.connectPort} is success!");
			_packetReceiver = createPacketReceiver();
			_packetReceiver.startRecv();
			connected = true;
		}
		else
		{
			reset();
			Dbg.ERROR_MSG($"NetworkInterfaceBase::_onConnectionState(), connect error! ip: {state.connectIP}:{state.connectPort}, err: {state.error}");
		}
		Event.fireAll("onConnectionState", flag);
		if (state.connectCB != null)
		{
			state.connectCB(state.connectIP, state.connectPort, flag, state.userData);
		}
	}

	private static void connectCB(IAsyncResult ar)
	{
		ConnectState connectState = null;
		try
		{
			connectState = (ConnectState)ar.AsyncState;
			connectState.socket.EndConnect(ar);
			Event.fireIn("_onConnectionState", connectState);
		}
		catch (Exception ex)
		{
			connectState.error = ex.ToString();
			Event.fireIn("_onConnectionState", connectState);
		}
	}

	private void _asyncConnect(ConnectState state)
	{
		Dbg.DEBUG_MSG($"NetworkInterfaceBase::_asyncConnect(), will connect to '{state.connectIP}:{state.connectPort}' ...");
		onAsyncConnect(state);
	}

	protected virtual void onAsyncConnectCB(ConnectState state)
	{
	}

	private void _asyncConnectCB(IAsyncResult ar)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		ConnectState connectState = (ConnectState)ar.AsyncState;
		AsyncConnectMethod obj = (AsyncConnectMethod)((AsyncResult)ar).AsyncDelegate;
		onAsyncConnectCB(connectState);
		Dbg.DEBUG_MSG($"NetworkInterfaceBase::_asyncConnectCB(), connect to '{connectState.connectIP}:{connectState.connectPort}' finish. error = '{connectState.error}'");
		obj.EndInvoke(ar);
		Event.fireIn("_onConnectionState", connectState);
	}

	public void connectTo(string ip, int port, ConnectCallback callback, object userData)
	{
		if (valid())
		{
			throw new InvalidOperationException("Have already connected!");
		}
		if (!new Regex("((?:(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d))))").IsMatch(ip))
		{
			ip = Dns.GetHostEntry(ip).AddressList[0].ToString();
		}
		_socket = createSocket();
		ConnectState connectState = new ConnectState();
		connectState.connectIP = ip;
		connectState.connectPort = port;
		connectState.connectCB = callback;
		connectState.userData = userData;
		connectState.socket = _socket;
		connectState.networkInterface = this;
		Dbg.DEBUG_MSG("connect to " + ip + ":" + port + " ...");
		connected = false;
		Event.registerIn("_onConnectionState", this, "_onConnectionState");
		new AsyncConnectMethod(_asyncConnect).BeginInvoke(connectState, _asyncConnectCB, connectState);
	}

	public virtual bool send(MemoryStream stream)
	{
		if (!valid())
		{
			throw new ArgumentException("invalid socket!");
		}
		if (_packetSender == null)
		{
			_packetSender = createPacketSender();
		}
		if (_filter != null)
		{
			return _filter.send(_packetSender, stream);
		}
		return _packetSender.send(stream);
	}

	public virtual void process()
	{
		if (valid() && _packetReceiver != null)
		{
			_packetReceiver.process();
		}
	}

	public EncryptionFilter fileter()
	{
		return _filter;
	}

	public void setFilter(EncryptionFilter filter)
	{
		_filter = filter;
	}
}
