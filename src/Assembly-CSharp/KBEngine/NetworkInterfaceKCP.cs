using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Deps;

namespace KBEngine;

public class NetworkInterfaceKCP : NetworkInterfaceBase
{
	private KCP kcp_;

	public uint connID;

	public uint nextTickKcpUpdate;

	public EndPoint remoteEndPint;

	protected override Socket createSocket()
	{
		return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
	}

	protected override PacketReceiverBase createPacketReceiver()
	{
		return new PacketReceiverKCP(this);
	}

	protected override PacketSenderBase createPacketSender()
	{
		return new PacketSenderKCP(this);
	}

	public override void reset()
	{
		finiKCP();
		base.reset();
	}

	public override void close()
	{
		finiKCP();
		base.close();
	}

	public override bool valid()
	{
		if (kcp_ != null && _socket != null)
		{
			return connected;
		}
		return false;
	}

	protected void outputKCP(byte[] data, int size, object userData)
	{
		if (!valid())
		{
			throw new ArgumentException("invalid socket!");
		}
		if (_packetSender == null)
		{
			_packetSender = createPacketSender();
		}
		((PacketSenderKCP)_packetSender).sendto(data, size);
	}

	private bool initKCP()
	{
		kcp_ = new KCP(connID, this);
		kcp_.SetOutput(outputKCP);
		kcp_.SetMTU(1400);
		kcp_.WndSize(KBEngineApp.app.getInitArgs().getUDPSendBufferSize(), KBEngineApp.app.getInitArgs().getUDPRecvBufferSize());
		kcp_.NoDelay(1, 10, 2, 1);
		kcp_.SetMinRTO(10);
		nextTickKcpUpdate = 0u;
		return true;
	}

	private bool finiKCP()
	{
		if (kcp_ != null)
		{
			kcp_.SetOutput(null);
			kcp_.Release();
			kcp_ = null;
		}
		remoteEndPint = null;
		connID = 0u;
		nextTickKcpUpdate = 0u;
		return true;
	}

	public KCP kcp()
	{
		return kcp_;
	}

	public override bool send(MemoryStream stream)
	{
		if (!valid())
		{
			throw new ArgumentException("invalid socket!");
		}
		if (_filter != null)
		{
			_filter.encrypt(stream);
		}
		nextTickKcpUpdate = 0u;
		return kcp_.Send(stream.data(), stream.rpos, (int)stream.length()) >= 0;
	}

	public override void process()
	{
		if (valid())
		{
			uint num = KCP.TimeUtils.iclock();
			if (num >= nextTickKcpUpdate)
			{
				kcp_.Update(num);
				nextTickKcpUpdate = kcp_.Check(num);
			}
			if (_packetReceiver != null)
			{
				_packetReceiver.process();
			}
		}
	}

	protected override void onAsyncConnectCB(ConnectState state)
	{
		if (state.error.Length <= 0 && initKCP())
		{
			connected = true;
			remoteEndPint = new IPEndPoint(IPAddress.Parse(state.connectIP), state.connectPort);
		}
	}

	protected override void onAsyncConnect(ConnectState state)
	{
		try
		{
			byte[] bytes = Encoding.ASCII.GetBytes("62a559f3fa7748bc22f8e0766019d498");
			state.socket.SendTo(bytes, bytes.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(state.connectIP), state.connectPort));
			ArrayList obj = new ArrayList { state.socket };
			Socket.Select(obj, null, null, 3000000);
			if (obj.Count > 0)
			{
				byte[] array = new byte[1472];
				int num = state.socket.Receive(array);
				if (num <= 0)
				{
					Dbg.ERROR_MSG($"NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{state.connectIP}:{state.connectPort}'! receive hello-ack error!");
					state.error = "receive hello-ack error!";
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				Array.Copy(array, 0, memoryStream.data(), memoryStream.wpos, num);
				memoryStream.wpos = num;
				string text = memoryStream.readString();
				string text2 = memoryStream.readString();
				uint num2 = memoryStream.readUint32();
				if (text != "1432ad7c829170a76dd31982c3501eca")
				{
					Dbg.ERROR_MSG(string.Format("NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{0}:{1}'! receive hello-ack({2}!={3}) mismatch!", state.connectIP, state.connectPort, text, "1432ad7c829170a76dd31982c3501eca"));
					state.error = "hello-ack mismatch!";
				}
				else if (KBEngineApp.app.serverVersion != text2)
				{
					Dbg.ERROR_MSG($"NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{state.connectIP}:{state.connectPort}'! version({text2}!={KBEngineApp.app.serverVersion}) mismatch!");
					state.error = "version mismatch!";
				}
				else if (num2 == 0)
				{
					Dbg.ERROR_MSG($"NetworkInterfaceKCP::_asyncConnect(), failed to connect to '{state.connectIP}:{state.connectPort}'! conv is 0!");
					state.error = "kcp conv error!";
				}
				((NetworkInterfaceKCP)state.networkInterface).connID = num2;
			}
			else
			{
				Dbg.ERROR_MSG($"NetworkInterfaceKCP::_asyncConnect(), connect to '{state.connectIP}:{state.connectPort}' timeout!'");
				state.error = "timeout!";
			}
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG($"NetworkInterfaceKCP::_asyncConnect(), connect to '{state.connectIP}:{state.connectPort}' fault! error = '{ex}'");
			state.error = ex.ToString();
		}
	}
}
