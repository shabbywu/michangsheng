using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine;

public abstract class PacketSenderBase
{
	public delegate void AsyncSendMethod();

	protected NetworkInterfaceBase _networkInterface;

	private AsyncCallback _asyncCallback;

	private AsyncSendMethod _asyncSendMethod;

	public PacketSenderBase(NetworkInterfaceBase networkInterface)
	{
		_networkInterface = networkInterface;
		_asyncSendMethod = _asyncSend;
		_asyncCallback = _onSent;
	}

	~PacketSenderBase()
	{
	}

	public NetworkInterfaceBase networkInterface()
	{
		return _networkInterface;
	}

	public abstract bool send(MemoryStream stream);

	protected void _startSend()
	{
		_asyncSendMethod.BeginInvoke(_asyncCallback, null);
	}

	protected abstract void _asyncSend();

	protected static void _onSent(IAsyncResult ar)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		((AsyncSendMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
	}
}
