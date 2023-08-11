using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine;

public abstract class PacketReceiverBase
{
	protected delegate void AsyncReceiveMethod();

	protected MessageReaderBase _messageReader;

	protected NetworkInterfaceBase _networkInterface;

	public PacketReceiverBase(NetworkInterfaceBase networkInterface)
	{
		_networkInterface = networkInterface;
	}

	~PacketReceiverBase()
	{
	}

	public NetworkInterfaceBase networkInterface()
	{
		return _networkInterface;
	}

	public abstract void process();

	public virtual void startRecv()
	{
		new AsyncReceiveMethod(_asyncReceive).BeginInvoke(_onRecv, null);
	}

	protected abstract void _asyncReceive();

	private void _onRecv(IAsyncResult ar)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		((AsyncReceiveMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
	}
}
