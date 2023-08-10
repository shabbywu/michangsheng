using System;
using UnityEngine;

namespace KBEngine;

internal class BlowfishFilter : EncryptionFilter
{
	private Blowfish _blowfish = new Blowfish();

	private MemoryStream _packet = new MemoryStream();

	private MemoryStream _enctyptStrem = new MemoryStream();

	private UINT8 _padSize = (byte)0;

	private ushort _packLen;

	private const uint BLOCK_SIZE = 8u;

	private const uint MIN_PACKET_SIZE = 11u;

	~BlowfishFilter()
	{
	}

	public byte[] key()
	{
		return _blowfish.key();
	}

	public override void encrypt(MemoryStream stream)
	{
		int num = 0;
		if (stream.length() % 8 != 0)
		{
			num = (int)(8 - stream.length() % 8);
			stream.wpos += num;
			if (stream.wpos > 5840)
			{
				Debug.LogError((object)("BlowfishFilter::encrypt: stream.wpos(" + stream.wpos + ") > MemoryStream.BUFFER_MAX(" + 5840 + ")!"));
			}
		}
		_blowfish.encipher(stream.data(), (int)stream.length());
		ushort v = (ushort)(stream.length() + 1);
		_enctyptStrem.writeUint16(v);
		_enctyptStrem.writeUint8((UINT8)(byte)num);
		_enctyptStrem.append(stream.data(), (uint)stream.rpos, stream.length());
		stream.swap(_enctyptStrem);
		_enctyptStrem.clear();
	}

	public override void decrypt(MemoryStream stream)
	{
		_blowfish.decipher(stream.data(), stream.rpos, (int)stream.length());
	}

	public override void decrypt(byte[] buffer, int startIndex, int length)
	{
		_blowfish.decipher(buffer, startIndex, length);
	}

	public override bool send(PacketSenderBase sender, MemoryStream stream)
	{
		if (!_blowfish.isGood())
		{
			Dbg.ERROR_MSG("BlowfishFilter::send: Dropping packet, due to invalid filter");
			return false;
		}
		encrypt(stream);
		return sender.send(stream);
	}

	public override bool recv(MessageReaderBase reader, byte[] buffer, uint rpos, uint len)
	{
		if (!_blowfish.isGood())
		{
			Dbg.ERROR_MSG("BlowfishFilter::recv: Dropping packet, due to invalid filter");
			return false;
		}
		if (_packet.length() == 0 && len >= 11 && BitConverter.ToUInt16(buffer, (int)rpos) - 1 == len - 3)
		{
			int num = BitConverter.ToUInt16(buffer, (int)rpos) - 1;
			int num2 = buffer[rpos + 2];
			decrypt(buffer, (int)(rpos + 3), (int)(len - 3));
			int length = num - num2;
			reader?.process(buffer, rpos + 3, (uint)length);
			return true;
		}
		_packet.append(buffer, rpos, len);
		while (_packet.length() != 0)
		{
			uint num3 = 0u;
			int wpos = 0;
			if (_packLen <= 0)
			{
				if (_packet.length() < 11)
				{
					return false;
				}
				_packLen = _packet.readUint16();
				_padSize = _packet.readUint8();
				_packLen--;
				if (_packet.length() > _packLen)
				{
					num3 = (uint)(_packet.rpos + _packLen);
					wpos = _packet.wpos;
					_packet.wpos = (int)num3;
				}
				else if (_packet.length() < _packLen)
				{
					return false;
				}
			}
			else if (_packet.length() > _packLen)
			{
				num3 = (uint)(_packet.rpos + _packLen);
				wpos = _packet.wpos;
				_packet.wpos = (int)num3;
			}
			else if (_packet.length() < _packLen)
			{
				return false;
			}
			decrypt(_packet);
			_packet.wpos -= (byte)_padSize;
			reader?.process(_packet.data(), (uint)_packet.rpos, _packet.length());
			if (num3 != 0)
			{
				_packet.rpos = (int)num3;
				_packet.wpos = wpos;
			}
			else
			{
				_packet.clear();
			}
			_packLen = 0;
			_padSize = (byte)0;
		}
		return true;
	}
}
