using System.Net.Sockets;
using System.Threading;

namespace KBEngine;

public class PacketReceiverTCP : PacketReceiverBase
{
	private byte[] _buffer;

	private int _wpos;

	private int _rpos;

	public PacketReceiverTCP(NetworkInterfaceBase networkInterface)
		: base(networkInterface)
	{
		_buffer = new byte[KBEngineApp.app.getInitArgs().TCP_RECV_BUFFER_MAX];
		_messageReader = new MessageReaderTCP();
	}

	~PacketReceiverTCP()
	{
		Dbg.DEBUG_MSG("PacketReceiverTCP::~PacketReceiverTCP(), destroyed!");
	}

	public override void process()
	{
		int num = Interlocked.Add(ref _wpos, 0);
		if (_rpos < num)
		{
			if (_networkInterface.fileter() != null)
			{
				_networkInterface.fileter().recv(_messageReader, _buffer, (uint)_rpos, (uint)(num - _rpos));
			}
			else
			{
				_messageReader.process(_buffer, (uint)_rpos, (uint)(num - _rpos));
			}
			Interlocked.Exchange(ref _rpos, num);
		}
		else if (num < _rpos)
		{
			if (_networkInterface.fileter() != null)
			{
				_networkInterface.fileter().recv(_messageReader, _buffer, (uint)_rpos, (uint)(_buffer.Length - _rpos));
				_networkInterface.fileter().recv(_messageReader, _buffer, 0u, (uint)num);
			}
			else
			{
				_messageReader.process(_buffer, (uint)_rpos, (uint)(_buffer.Length - _rpos));
				_messageReader.process(_buffer, 0u, (uint)num);
			}
			Interlocked.Exchange(ref _rpos, num);
		}
	}

	private int _free()
	{
		int num = Interlocked.Add(ref _rpos, 0);
		if (_wpos == _buffer.Length)
		{
			if (num == 0)
			{
				return 0;
			}
			Interlocked.Exchange(ref _wpos, 0);
		}
		if (num <= _wpos)
		{
			return _buffer.Length - _wpos;
		}
		return num - _wpos - 1;
	}

	protected override void _asyncReceive()
	{
		if (_networkInterface == null || !_networkInterface.valid())
		{
			Dbg.WARNING_MSG("PacketReceiverTCP::_asyncReceive(): network interface invalid!");
			return;
		}
		Socket socket = _networkInterface.sock();
		while (true)
		{
			int num = 0;
			int num2;
			for (num2 = _free(); num2 == 0; num2 = _free())
			{
				if (num > 0)
				{
					if (num > 1000)
					{
						Dbg.ERROR_MSG("PacketReceiverTCP::_asyncReceive(): no space!");
						Event.fireIn("_closeNetwork", _networkInterface);
						return;
					}
					Dbg.WARNING_MSG("PacketReceiverTCP::_asyncReceive(): waiting for space, Please adjust 'RECV_BUFFER_MAX'! retries=" + num);
					Thread.Sleep(5);
				}
				num++;
			}
			int num3 = 0;
			try
			{
				num3 = socket.Receive(_buffer, _wpos, num2, SocketFlags.None);
			}
			catch (SocketException arg)
			{
				Dbg.ERROR_MSG($"PacketReceiverTCP::_asyncReceive(): receive error, disconnect from '{socket.RemoteEndPoint}'! error = '{arg}'");
				Event.fireIn("_closeNetwork", _networkInterface);
				return;
			}
			if (num3 <= 0)
			{
				break;
			}
			Interlocked.Add(ref _wpos, num3);
		}
		Dbg.WARNING_MSG($"PacketReceiverTCP::_asyncReceive(): receive 0 bytes, disconnect from '{socket.RemoteEndPoint}'!");
		Event.fireIn("_closeNetwork", _networkInterface);
	}
}
