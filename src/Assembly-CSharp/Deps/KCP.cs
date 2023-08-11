using System;
using System.Collections.Generic;

namespace Deps;

public class KCP
{
	public class TimeUtils
	{
		private static readonly DateTime epoch = new DateTime(1970, 1, 1);

		private static readonly DateTime twepoch = new DateTime(2000, 1, 1);

		public static uint iclock()
		{
			return (uint)(Convert.ToInt64(DateTime.Now.Subtract(twepoch).TotalMilliseconds) & 0xFFFFFFFFu);
		}

		public static long LocalUnixTime()
		{
			return Convert.ToInt64(DateTime.Now.Subtract(epoch).TotalMilliseconds);
		}

		public static long ToUnixTimestamp(DateTime t)
		{
			return (long)Math.Truncate(t.ToUniversalTime().Subtract(epoch).TotalSeconds);
		}
	}

	internal class Segment
	{
		internal uint conv;

		internal uint cmd;

		internal uint frg;

		internal uint wnd;

		internal uint ts;

		internal uint sn;

		internal uint una;

		internal uint resendts;

		internal uint rto;

		internal uint faskack;

		internal uint xmit;

		internal byte[] data;

		internal Segment(int size = 0)
		{
			data = new byte[size];
		}

		internal void Encode(byte[] ptr, ref int offset)
		{
			uint l = (uint)data.Length;
			ikcp_encode32u(ptr, offset, conv);
			ikcp_encode8u(ptr, offset + 4, (byte)cmd);
			ikcp_encode8u(ptr, offset + 5, (byte)frg);
			ikcp_encode16u(ptr, offset + 6, (ushort)wnd);
			ikcp_encode32u(ptr, offset + 8, ts);
			ikcp_encode32u(ptr, offset + 12, sn);
			ikcp_encode32u(ptr, offset + 16, una);
			ikcp_encode32u(ptr, offset + 20, l);
			offset += 24;
		}
	}

	public delegate void OutputDelegate(byte[] data, int size, object user);

	public const int IKCP_RTO_NDL = 30;

	public const int IKCP_RTO_MIN = 100;

	public const int IKCP_RTO_DEF = 200;

	public const int IKCP_RTO_MAX = 60000;

	public const int IKCP_CMD_PUSH = 81;

	public const int IKCP_CMD_ACK = 82;

	public const int IKCP_CMD_WASK = 83;

	public const int IKCP_CMD_WINS = 84;

	public const int IKCP_ASK_SEND = 1;

	public const int IKCP_ASK_TELL = 2;

	public const int IKCP_WND_SND = 32;

	public const int IKCP_WND_RCV = 32;

	public const int IKCP_MTU_DEF = 1400;

	public const int IKCP_ACK_FAST = 3;

	public const int IKCP_INTERVAL = 100;

	public const int IKCP_OVERHEAD = 24;

	public const int IKCP_DEADLINK = 20;

	public const int IKCP_THRESH_INIT = 2;

	public const int IKCP_THRESH_MIN = 2;

	public const int IKCP_PROBE_INIT = 7000;

	public const int IKCP_PROBE_LIMIT = 120000;

	public const int IKCP_LOG_OUTPUT = 1;

	public const int IKCP_LOG_INPUT = 2;

	public const int IKCP_LOG_SEND = 4;

	public const int IKCP_LOG_RECV = 8;

	public const int IKCP_LOG_IN_DATA = 16;

	public const int IKCP_LOG_IN_ACK = 32;

	public const int IKCP_LOG_IN_PROBE = 64;

	public const int IKCP_LOG_IN_WINS = 128;

	public const int IKCP_LOG_OUT_DATA = 256;

	public const int IKCP_LOG_OUT_ACK = 512;

	public const int IKCP_LOG_OUT_PROBE = 1024;

	public const int IKCP_LOG_OUT_WINS = 2048;

	private uint conv_;

	private uint mtu_;

	private uint mss_;

	private uint state_;

	private uint snd_una_;

	private uint snd_nxt_;

	private uint rcv_nxt_;

	private uint ssthresh_;

	private int rx_rttval_;

	private int rx_srtt_;

	private int rx_rto_;

	private int rx_minrto_;

	private uint snd_wnd_;

	private uint rcv_wnd_;

	private uint rmt_wnd_;

	private uint cwnd_;

	private uint probe_;

	private uint current_;

	private uint interval_;

	private uint ts_flush_;

	private uint xmit_;

	private uint nrcv_buf_;

	private uint nsnd_buf_;

	private uint nrcv_que_;

	private uint nsnd_que_;

	private uint nodelay_;

	private uint updated_;

	private uint ts_probe_;

	private uint probe_wait_;

	private uint dead_link_;

	private uint incr_;

	private LinkedList<Segment> snd_queue_;

	private LinkedList<Segment> rcv_queue_;

	private LinkedList<Segment> snd_buf_;

	private LinkedList<Segment> rcv_buf_;

	private uint[] acklist_;

	private uint ackcount_;

	private uint ackblock_;

	private byte[] buffer_;

	private object user_;

	private int fastresend_;

	private int nocwnd_;

	private OutputDelegate output_;

	public static void ikcp_encode8u(byte[] p, int offset, byte c)
	{
		p[offset] = c;
	}

	public static byte ikcp_decode8u(byte[] p, ref int offset)
	{
		return p[offset++];
	}

	public static void ikcp_encode16u(byte[] p, int offset, ushort v)
	{
		p[offset] = (byte)(v & 0xFFu);
		p[offset + 1] = (byte)(v >> 8);
	}

	public static ushort ikcp_decode16u(byte[] p, ref int offset)
	{
		int num = offset;
		offset += 2;
		return (ushort)(p[num] | (ushort)(p[num + 1] << 8));
	}

	public static void ikcp_encode32u(byte[] p, int offset, uint l)
	{
		p[offset] = (byte)(l & 0xFFu);
		p[offset + 1] = (byte)(l >> 8);
		p[offset + 2] = (byte)(l >> 16);
		p[offset + 3] = (byte)(l >> 24);
	}

	public static uint ikcp_decode32u(byte[] p, ref int offset)
	{
		int num = offset;
		offset += 4;
		return (uint)(p[num] | (p[num + 1] << 8) | (p[num + 2] << 16) | (p[num + 3] << 24));
	}

	public static uint _imin_(uint a, uint b)
	{
		if (a > b)
		{
			return b;
		}
		return a;
	}

	public static uint _imax_(uint a, uint b)
	{
		if (a < b)
		{
			return b;
		}
		return a;
	}

	public static uint _ibound_(uint lower, uint middle, uint upper)
	{
		return _imin_(_imax_(lower, middle), upper);
	}

	public static int _itimediff(uint later, uint earlier)
	{
		return (int)(later - earlier);
	}

	public KCP(uint conv, object user)
	{
		user_ = user;
		conv_ = conv;
		snd_wnd_ = 32u;
		rcv_wnd_ = 32u;
		rmt_wnd_ = 32u;
		mtu_ = 1400u;
		mss_ = mtu_ - 24;
		rx_rto_ = 200;
		rx_minrto_ = 100;
		interval_ = 100u;
		ts_flush_ = 100u;
		ssthresh_ = 2u;
		dead_link_ = 20u;
		buffer_ = new byte[(mtu_ + 24) * 3];
		snd_queue_ = new LinkedList<Segment>();
		rcv_queue_ = new LinkedList<Segment>();
		snd_buf_ = new LinkedList<Segment>();
		rcv_buf_ = new LinkedList<Segment>();
	}

	public void Release()
	{
		snd_buf_.Clear();
		rcv_buf_.Clear();
		snd_queue_.Clear();
		rcv_queue_.Clear();
		nrcv_buf_ = 0u;
		nsnd_buf_ = 0u;
		nrcv_que_ = 0u;
		nsnd_que_ = 0u;
		ackblock_ = 0u;
		ackcount_ = 0u;
		buffer_ = null;
		acklist_ = null;
	}

	public void SetOutput(OutputDelegate output)
	{
		output_ = output;
	}

	public int Recv(byte[] buffer, int offset, int len)
	{
		int num = ((len < 0) ? 1 : 0);
		int num2 = 0;
		if (rcv_queue_.Count == 0)
		{
			return -1;
		}
		if (len < 0)
		{
			len = -len;
		}
		int num3 = PeekSize();
		if (num3 < 0)
		{
			return -2;
		}
		if (num3 > len)
		{
			return -3;
		}
		if (nrcv_que_ >= rcv_wnd_)
		{
			num2 = 1;
		}
		len = 0;
		LinkedListNode<Segment> linkedListNode = null;
		for (LinkedListNode<Segment> linkedListNode2 = rcv_queue_.First; linkedListNode2 != null; linkedListNode2 = linkedListNode)
		{
			int num4 = 0;
			Segment value = linkedListNode2.Value;
			linkedListNode = linkedListNode2.Next;
			if (buffer != null)
			{
				Buffer.BlockCopy(value.data, 0, buffer, offset, value.data.Length);
				offset += value.data.Length;
			}
			len += value.data.Length;
			num4 = (int)value.frg;
			Log(8, "recv sn={0}", value.sn);
			if (num == 0)
			{
				rcv_queue_.Remove(linkedListNode2);
				nrcv_que_--;
			}
			if (num4 == 0)
			{
				break;
			}
		}
		while (rcv_buf_.Count > 0)
		{
			LinkedListNode<Segment> first = rcv_buf_.First;
			if (first.Value.sn != rcv_nxt_ || nrcv_que_ >= rcv_wnd_)
			{
				break;
			}
			rcv_buf_.Remove(first);
			nrcv_buf_--;
			rcv_queue_.AddLast(first);
			nrcv_que_++;
			rcv_nxt_++;
		}
		if (nrcv_que_ < rcv_wnd_ && num2 != 0)
		{
			probe_ |= 2u;
		}
		return len;
	}

	public int PeekSize()
	{
		if (rcv_queue_.Count == 0)
		{
			return -1;
		}
		LinkedListNode<Segment> first = rcv_queue_.First;
		Segment value = first.Value;
		if (value.frg == 0)
		{
			return value.data.Length;
		}
		if (nrcv_que_ < value.frg + 1)
		{
			return -1;
		}
		int num = 0;
		for (first = rcv_queue_.First; first != null; first = first.Next)
		{
			value = first.Value;
			num += value.data.Length;
			if (value.frg == 0)
			{
				break;
			}
		}
		return num;
	}

	public int Send(byte[] buffer, int offset, int len)
	{
		if (len < 0)
		{
			return -1;
		}
		int num = 0;
		num = ((len <= (int)mss_) ? 1 : ((len + (int)mss_ - 1) / (int)mss_));
		if (num > 255)
		{
			return -2;
		}
		if (num == 0)
		{
			num = 1;
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = ((len > (int)mss_) ? ((int)mss_) : len);
			Segment segment = new Segment(num2);
			if (buffer != null && len > 0)
			{
				Buffer.BlockCopy(buffer, offset, segment.data, 0, num2);
				offset += num2;
			}
			segment.frg = (uint)(num - i - 1);
			snd_queue_.AddLast(segment);
			nsnd_que_++;
			len -= num2;
		}
		return 0;
	}

	private void UpdateACK(int rtt)
	{
		if (rx_srtt_ == 0)
		{
			rx_srtt_ = rtt;
			rx_rttval_ = rtt / 2;
		}
		else
		{
			int num = rtt - rx_srtt_;
			if (num < 0)
			{
				num = -num;
			}
			rx_rttval_ = (3 * rx_rttval_ + num) / 4;
			rx_srtt_ = (7 * rx_srtt_ + rtt) / 8;
			if (rx_srtt_ < 1)
			{
				rx_srtt_ = 1;
			}
		}
		long num2 = rx_srtt_ + _imax_(interval_, (uint)(4 * rx_rttval_));
		rx_rto_ = (int)_ibound_((uint)rx_minrto_, (uint)num2, 60000u);
	}

	private void ShrinkBuf()
	{
		LinkedListNode<Segment> first = snd_buf_.First;
		if (first != null)
		{
			Segment value = first.Value;
			snd_una_ = value.sn;
		}
		else
		{
			snd_una_ = snd_nxt_;
		}
	}

	private void ParseACK(uint sn)
	{
		if (_itimediff(sn, snd_una_) < 0 || _itimediff(sn, snd_nxt_) >= 0)
		{
			return;
		}
		LinkedListNode<Segment> linkedListNode = null;
		LinkedListNode<Segment> linkedListNode2 = snd_buf_.First;
		while (linkedListNode2 != null)
		{
			Segment value = linkedListNode2.Value;
			linkedListNode = linkedListNode2.Next;
			if (sn == value.sn)
			{
				snd_buf_.Remove(linkedListNode2);
				nsnd_buf_--;
				break;
			}
			if (_itimediff(sn, value.sn) >= 0)
			{
				linkedListNode2 = linkedListNode;
				continue;
			}
			break;
		}
	}

	private void ParseUNA(uint una)
	{
		LinkedListNode<Segment> linkedListNode = null;
		LinkedListNode<Segment> linkedListNode2 = snd_buf_.First;
		while (linkedListNode2 != null)
		{
			Segment value = linkedListNode2.Value;
			linkedListNode = linkedListNode2.Next;
			if (_itimediff(una, value.sn) > 0)
			{
				snd_buf_.Remove(linkedListNode2);
				nsnd_buf_--;
				linkedListNode2 = linkedListNode;
				continue;
			}
			break;
		}
	}

	private void ParseFastACK(uint sn)
	{
		if (_itimediff(sn, snd_una_) < 0 || _itimediff(sn, snd_nxt_) >= 0)
		{
			return;
		}
		LinkedListNode<Segment> linkedListNode = null;
		LinkedListNode<Segment> linkedListNode2 = snd_buf_.First;
		while (linkedListNode2 != null)
		{
			Segment value = linkedListNode2.Value;
			linkedListNode = linkedListNode2.Next;
			if (_itimediff(sn, value.sn) >= 0)
			{
				if (sn != value.sn)
				{
					value.faskack++;
				}
				linkedListNode2 = linkedListNode;
				continue;
			}
			break;
		}
	}

	private void ACKPush(uint sn, uint ts)
	{
		uint num = ackcount_ + 1;
		if (num > ackblock_)
		{
			uint num2;
			for (num2 = 8u; num2 < num; num2 <<= 1)
			{
			}
			uint[] array = new uint[num2 * 2];
			if (acklist_ != null)
			{
				for (int i = 0; i < ackcount_; i++)
				{
					array[i * 2] = acklist_[i * 2];
					array[i * 2 + 1] = acklist_[i * 2 + 1];
				}
			}
			acklist_ = array;
			ackblock_ = num2;
		}
		acklist_[ackcount_ * 2] = sn;
		acklist_[ackcount_ * 2 + 1] = ts;
		ackcount_++;
	}

	private void ACKGet(int pos, ref uint sn, ref uint ts)
	{
		sn = acklist_[pos * 2];
		ts = acklist_[pos * 2 + 1];
	}

	private void ParseData(Segment newseg)
	{
		uint sn = newseg.sn;
		int num = 0;
		if (_itimediff(sn, rcv_nxt_ + rcv_wnd_) >= 0 || _itimediff(sn, rcv_nxt_) < 0)
		{
			return;
		}
		LinkedListNode<Segment> linkedListNode = null;
		LinkedListNode<Segment> linkedListNode2 = null;
		for (linkedListNode = rcv_buf_.Last; linkedListNode != null; linkedListNode = linkedListNode2)
		{
			Segment value = linkedListNode.Value;
			linkedListNode2 = linkedListNode.Previous;
			if (value.sn == sn)
			{
				num = 1;
				break;
			}
			if (_itimediff(sn, value.sn) > 0)
			{
				break;
			}
		}
		if (num == 0)
		{
			if (linkedListNode != null)
			{
				rcv_buf_.AddAfter(linkedListNode, newseg);
			}
			else
			{
				rcv_buf_.AddFirst(newseg);
			}
			nrcv_buf_++;
		}
		while (rcv_buf_.Count > 0)
		{
			linkedListNode = rcv_buf_.First;
			if (linkedListNode.Value.sn == rcv_nxt_ && nrcv_que_ < rcv_wnd_)
			{
				rcv_buf_.Remove(linkedListNode);
				nrcv_buf_--;
				rcv_queue_.AddLast(linkedListNode);
				nrcv_que_++;
				rcv_nxt_++;
				continue;
			}
			break;
		}
	}

	public int Input(byte[] data, int offset, int size)
	{
		uint num = 0u;
		int num2 = 0;
		Log(2, "[RI] {0} bytes", size);
		if (data == null || size < 24)
		{
			return -1;
		}
		while (size >= 24)
		{
			uint num3 = ikcp_decode32u(data, ref offset);
			if (conv_ != num3)
			{
				return -1;
			}
			uint num4 = ikcp_decode8u(data, ref offset);
			uint frg = ikcp_decode8u(data, ref offset);
			uint num5 = ikcp_decode16u(data, ref offset);
			uint num6 = ikcp_decode32u(data, ref offset);
			uint num7 = ikcp_decode32u(data, ref offset);
			uint una = ikcp_decode32u(data, ref offset);
			uint num8 = ikcp_decode32u(data, ref offset);
			size -= 24;
			if (size < num8)
			{
				return -2;
			}
			if (num4 != 81 && num4 != 82 && num4 != 83 && num4 != 84)
			{
				return -3;
			}
			rmt_wnd_ = num5;
			ParseUNA(una);
			ShrinkBuf();
			switch (num4)
			{
			case 82u:
				if (_itimediff(current_, num6) >= 0)
				{
					UpdateACK(_itimediff(current_, num6));
				}
				ParseACK(num7);
				ShrinkBuf();
				if (num2 == 0)
				{
					num2 = 1;
					num = num7;
				}
				else if (_itimediff(num7, num) > 0)
				{
					num = num7;
				}
				Log(16, "input ack: sn={0} rtt={1} rto={2}", num7, _itimediff(current_, num6), rx_rto_);
				break;
			case 81u:
				Log(16, "input psh: sn={0} ts={1}", num7, num6);
				if (_itimediff(num7, rcv_nxt_ + rcv_wnd_) >= 0)
				{
					break;
				}
				ACKPush(num7, num6);
				if (_itimediff(num7, rcv_nxt_) >= 0)
				{
					Segment segment = new Segment((int)num8);
					segment.conv = num3;
					segment.cmd = num4;
					segment.frg = frg;
					segment.wnd = num5;
					segment.ts = num6;
					segment.sn = num7;
					segment.una = una;
					if (num8 != 0)
					{
						Buffer.BlockCopy(data, offset, segment.data, 0, (int)num8);
					}
					ParseData(segment);
				}
				break;
			case 83u:
				probe_ |= 2u;
				Log(64, "input probe");
				break;
			case 84u:
				Log(128, "input wins: {0}", num5);
				break;
			default:
				return -3;
			}
			offset += (int)num8;
			size -= (int)num8;
		}
		if (num2 != 0)
		{
			ParseFastACK(num);
		}
		uint earlier = snd_una_;
		if (_itimediff(snd_una_, earlier) > 0 && cwnd_ < rmt_wnd_)
		{
			if (cwnd_ < ssthresh_)
			{
				cwnd_++;
				incr_ += mss_;
			}
			else
			{
				if (incr_ < mss_)
				{
					incr_ = mss_;
				}
				incr_ += mss_ * mss_ / incr_ + mss_ / 16;
				if ((cwnd_ + 1) * mss_ <= incr_)
				{
					cwnd_++;
				}
			}
			if (cwnd_ > rmt_wnd_)
			{
				cwnd_ = rmt_wnd_;
				incr_ = rmt_wnd_ * mss_;
			}
		}
		return 0;
	}

	private int WndUnused()
	{
		if (nrcv_que_ < rcv_wnd_)
		{
			return (int)(rcv_wnd_ - nrcv_que_);
		}
		return 0;
	}

	private void Flush()
	{
		int num = 0;
		int num2 = 0;
		int offset = 0;
		if (updated_ == 0)
		{
			return;
		}
		Segment segment = new Segment
		{
			conv = conv_,
			cmd = 82u,
			wnd = (uint)WndUnused(),
			una = rcv_nxt_
		};
		int num3 = (int)ackcount_;
		for (int i = 0; i < num3; i++)
		{
			if (offset + 24 > mtu_)
			{
				output_(buffer_, offset, user_);
				offset = 0;
			}
			ACKGet(i, ref segment.sn, ref segment.ts);
			segment.Encode(buffer_, ref offset);
		}
		ackcount_ = 0u;
		if (rmt_wnd_ == 0)
		{
			if (probe_wait_ == 0)
			{
				probe_wait_ = 7000u;
				ts_probe_ = current_ + probe_wait_;
			}
			else if (_itimediff(current_, ts_probe_) >= 0)
			{
				if (probe_wait_ < 7000)
				{
					probe_wait_ = 7000u;
				}
				probe_wait_ += probe_wait_ / 2;
				if (probe_wait_ > 120000)
				{
					probe_wait_ = 120000u;
				}
				ts_probe_ = current_ + probe_wait_;
				probe_ |= 1u;
			}
		}
		else
		{
			ts_probe_ = 0u;
			probe_wait_ = 0u;
		}
		if ((probe_ & (true ? 1u : 0u)) != 0)
		{
			segment.cmd = 83u;
			if (offset + 24 > mtu_)
			{
				output_(buffer_, offset, user_);
				offset = 0;
			}
			segment.Encode(buffer_, ref offset);
		}
		if ((probe_ & 2u) != 0)
		{
			segment.cmd = 84u;
			if (offset + 24 > mtu_)
			{
				output_(buffer_, offset, user_);
				offset = 0;
			}
			segment.Encode(buffer_, ref offset);
		}
		probe_ = 0u;
		uint num4 = _imin_(snd_wnd_, rmt_wnd_);
		if (nocwnd_ == 0)
		{
			num4 = _imin_(cwnd_, num4);
		}
		while (_itimediff(snd_nxt_, snd_una_ + num4) < 0 && snd_queue_.Count != 0)
		{
			LinkedListNode<Segment> first = snd_queue_.First;
			Segment value = first.Value;
			snd_queue_.Remove(first);
			snd_buf_.AddLast(first);
			nsnd_que_--;
			nsnd_buf_++;
			value.conv = conv_;
			value.cmd = 81u;
			value.wnd = segment.wnd;
			value.ts = current_;
			value.sn = snd_nxt_++;
			value.una = rcv_nxt_;
			value.resendts = current_;
			value.rto = (uint)rx_rto_;
			value.faskack = 0u;
			value.xmit = 0u;
		}
		uint num5 = ((fastresend_ > 0) ? ((uint)fastresend_) : uint.MaxValue);
		uint num6 = ((nodelay_ == 0) ? ((uint)(rx_rto_ >> 3)) : 0u);
		for (LinkedListNode<Segment> linkedListNode = snd_buf_.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			Segment value2 = linkedListNode.Value;
			int num7 = 0;
			if (value2.xmit == 0)
			{
				num7 = 1;
				value2.xmit++;
				value2.rto = (uint)rx_rto_;
				value2.resendts = current_ + value2.rto + num6;
			}
			else if (_itimediff(current_, value2.resendts) >= 0)
			{
				num7 = 1;
				value2.xmit++;
				xmit_++;
				if (nodelay_ == 0)
				{
					value2.rto += (uint)rx_rto_;
				}
				else
				{
					value2.rto += (uint)rx_rto_ / 2u;
				}
				value2.resendts = current_ + value2.rto;
				num2 = 1;
			}
			else if (value2.faskack >= num5)
			{
				num7 = 1;
				value2.xmit++;
				value2.faskack = 0u;
				value2.resendts = current_ + value2.rto;
				num++;
			}
			if (num7 > 0)
			{
				value2.ts = current_;
				value2.wnd = segment.wnd;
				value2.una = rcv_nxt_;
				int num8 = 24;
				if (value2.data != null)
				{
					num8 += value2.data.Length;
				}
				if (offset + num8 > mtu_)
				{
					output_(buffer_, offset, user_);
					offset = 0;
				}
				value2.Encode(buffer_, ref offset);
				if (value2.data.Length != 0)
				{
					Buffer.BlockCopy(value2.data, 0, buffer_, offset, value2.data.Length);
					offset += value2.data.Length;
				}
				if (value2.xmit >= dead_link_)
				{
					state_ = uint.MaxValue;
				}
			}
		}
		if (offset > 0)
		{
			output_(buffer_, offset, user_);
			offset = 0;
		}
		if (num > 0)
		{
			uint num9 = snd_nxt_ - snd_una_;
			ssthresh_ = num9 / 2;
			if (ssthresh_ < 2)
			{
				ssthresh_ = 2u;
			}
			cwnd_ = ssthresh_ + num5;
			incr_ = cwnd_ * mss_;
		}
		if (num2 > 0)
		{
			ssthresh_ = num4 / 2;
			if (ssthresh_ < 2)
			{
				ssthresh_ = 2u;
			}
			cwnd_ = 1u;
			incr_ = mss_;
		}
		if (cwnd_ < 1)
		{
			cwnd_ = 1u;
			incr_ = mss_;
		}
	}

	public void Update(uint current)
	{
		current_ = current;
		if (updated_ == 0)
		{
			updated_ = 1u;
			ts_flush_ = current;
		}
		int num = _itimediff(current_, ts_flush_);
		if (num >= 10000 || num < -10000)
		{
			ts_flush_ = current;
			num = 0;
		}
		if (num >= 0)
		{
			ts_flush_ += interval_;
			if (_itimediff(current_, ts_flush_) >= 0)
			{
				ts_flush_ = current_ + interval_;
			}
			Flush();
		}
	}

	public uint Check(uint current)
	{
		uint num = ts_flush_;
		int num2 = int.MaxValue;
		int num3 = int.MaxValue;
		if (updated_ == 0)
		{
			return current;
		}
		if (_itimediff(current, num) >= 10000 || _itimediff(current, num) < -10000)
		{
			num = current;
		}
		if (_itimediff(current, num) >= 0)
		{
			return current;
		}
		num2 = _itimediff(num, current);
		for (LinkedListNode<Segment> linkedListNode = snd_buf_.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			int num4 = _itimediff(linkedListNode.Value.resendts, current);
			if (num4 <= 0)
			{
				return current;
			}
			if (num4 < num3)
			{
				num3 = num4;
			}
		}
		uint num5 = (uint)((num3 < num2) ? num3 : num2);
		if (num5 >= interval_)
		{
			num5 = interval_;
		}
		return current + num5;
	}

	public int SetMTU(int mtu)
	{
		if (mtu < 50 || mtu < 24)
		{
			return -1;
		}
		byte[] array = new byte[(mtu + 24) * 3];
		mtu_ = (uint)mtu;
		mss_ = mtu_ - 24;
		buffer_ = array;
		return 0;
	}

	public int Interval(int interval)
	{
		if (interval > 5000)
		{
			interval = 5000;
		}
		else if (interval < 10)
		{
			interval = 10;
		}
		interval_ = (uint)interval;
		return 0;
	}

	public int NoDelay(int nodelay, int interval, int resend, int nc)
	{
		if (nodelay >= 0)
		{
			nodelay_ = (uint)nodelay;
			if (nodelay > 0)
			{
				rx_minrto_ = 30;
			}
			else
			{
				rx_minrto_ = 100;
			}
		}
		if (interval >= 0)
		{
			if (interval > 5000)
			{
				interval = 5000;
			}
			else if (interval < 10)
			{
				interval = 10;
			}
			interval_ = (uint)interval;
		}
		if (resend >= 0)
		{
			fastresend_ = resend;
		}
		if (nc >= 0)
		{
			nocwnd_ = nc;
		}
		return 0;
	}

	public int WndSize(int sndwnd, int rcvwnd)
	{
		if (sndwnd > 0)
		{
			snd_wnd_ = (uint)sndwnd;
		}
		if (rcvwnd > 0)
		{
			rcv_wnd_ = (uint)rcvwnd;
		}
		return 0;
	}

	public int WaitSnd()
	{
		return (int)(nsnd_buf_ + nsnd_que_);
	}

	public uint GetConv()
	{
		return conv_;
	}

	public uint GetState()
	{
		return state_;
	}

	public void SetMinRTO(int minrto)
	{
		rx_minrto_ = minrto;
	}

	public void SetFastResend(int resend)
	{
		fastresend_ = resend;
	}

	private void Log(int mask, string format, params object[] args)
	{
	}
}
