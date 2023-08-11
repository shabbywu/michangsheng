using System;
using System.Net.Sockets;
using System.Threading;

namespace KBEngine;

public class PacketSenderTCP : PacketSenderBase
{
	private byte[] _buffer;

	private int _wpos;

	private int _spos;

	private int _sending;

	public PacketSenderTCP(NetworkInterfaceBase networkInterface)
		: base(networkInterface)
	{
		_buffer = new byte[KBEngineApp.app.getInitArgs().TCP_SEND_BUFFER_MAX];
		_wpos = 0;
		_spos = 0;
		_sending = 0;
	}

	~PacketSenderTCP()
	{
		Dbg.DEBUG_MSG("PacketSenderTCP::~PacketSenderTCP(), destroyed!");
	}

	public override bool send(MemoryStream stream)
	{
		int num = (int)stream.length();
		if (num <= 0)
		{
			return true;
		}
		if (Interlocked.Add(ref _sending, 0) == 0 && _wpos == _spos)
		{
			_wpos = 0;
			_spos = 0;
		}
		int num2 = Interlocked.Add(ref _spos, 0);
		int num3 = 0;
		int num4 = _wpos % _buffer.Length;
		int num5 = num2 % _buffer.Length;
		num3 = ((num4 < num5) ? (num5 - num4 - 1) : (_buffer.Length - num4 + num5 - 1));
		if (num > num3)
		{
			Dbg.ERROR_MSG("PacketSenderTCP::send(): no space, Please adjust 'SEND_BUFFER_MAX'! data(" + num + ") > space(" + num3 + "), wpos=" + _wpos + ", spos=" + num2);
			return false;
		}
		int num6 = num4 + num;
		if (num6 <= _buffer.Length)
		{
			Array.Copy(stream.data(), stream.rpos, _buffer, num4, num);
		}
		else
		{
			int num7 = _buffer.Length - num4;
			Array.Copy(stream.data(), stream.rpos, _buffer, num4, num7);
			Array.Copy(stream.data(), stream.rpos + num7, _buffer, 0, num6 - _buffer.Length);
		}
		Interlocked.Add(ref _wpos, num);
		if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
		{
			_startSend();
		}
		return true;
	}

	protected override void _asyncSend()
	{
		if (_networkInterface == null || !_networkInterface.valid())
		{
			Dbg.WARNING_MSG("PacketSenderTCP::_asyncSend(): network interface invalid!");
			return;
		}
		Socket socket = _networkInterface.sock();
		int num3;
		do
		{
			int num = Interlocked.Add(ref _wpos, 0) - _spos;
			int num2 = _spos % _buffer.Length;
			if (num2 == 0)
			{
				num2 = num;
			}
			if (num > _buffer.Length - num2)
			{
				num = _buffer.Length - num2;
			}
			num3 = 0;
			try
			{
				num3 = socket.Send(_buffer, _spos % _buffer.Length, num, SocketFlags.None);
			}
			catch (SocketException arg)
			{
				Dbg.ERROR_MSG($"PacketSenderTCP::_asyncSend(): send data error, disconnect from '{socket.RemoteEndPoint}'! error = '{arg}'");
				Event.fireIn("_closeNetwork", _networkInterface);
				return;
			}
		}
		while (Interlocked.Add(ref _spos, num3) != Interlocked.Add(ref _wpos, 0));
		Interlocked.Exchange(ref _sending, 0);
	}
}
