using System;
using System.Collections.Generic;

namespace Deps
{
	// Token: 0x02000B32 RID: 2866
	public class KCP
	{
		// Token: 0x06004FC1 RID: 20417 RVA: 0x0021AC10 File Offset: 0x00218E10
		public static void ikcp_encode8u(byte[] p, int offset, byte c)
		{
			p[offset] = c;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0021AC18 File Offset: 0x00218E18
		public static byte ikcp_decode8u(byte[] p, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			return p[num];
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0021AC30 File Offset: 0x00218E30
		public static void ikcp_encode16u(byte[] p, int offset, ushort v)
		{
			p[offset] = (byte)(v & 255);
			p[offset + 1] = (byte)(v >> 8);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0021AC48 File Offset: 0x00218E48
		public static ushort ikcp_decode16u(byte[] p, ref int offset)
		{
			int num = offset;
			offset += 2;
			return (ushort)p[num] | (ushort)(p[num + 1] << 8);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0021AC6B File Offset: 0x00218E6B
		public static void ikcp_encode32u(byte[] p, int offset, uint l)
		{
			p[offset] = (byte)(l & 255U);
			p[offset + 1] = (byte)(l >> 8);
			p[offset + 2] = (byte)(l >> 16);
			p[offset + 3] = (byte)(l >> 24);
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0021AC98 File Offset: 0x00218E98
		public static uint ikcp_decode32u(byte[] p, ref int offset)
		{
			int num = offset;
			offset += 4;
			return (uint)((int)p[num] | (int)p[num + 1] << 8 | (int)p[num + 2] << 16 | (int)p[num + 3] << 24);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0021ACCB File Offset: 0x00218ECB
		public static uint _imin_(uint a, uint b)
		{
			if (a > b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0021ACD4 File Offset: 0x00218ED4
		public static uint _imax_(uint a, uint b)
		{
			if (a < b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0021ACDD File Offset: 0x00218EDD
		public static uint _ibound_(uint lower, uint middle, uint upper)
		{
			return KCP._imin_(KCP._imax_(lower, middle), upper);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0021ACEC File Offset: 0x00218EEC
		public static int _itimediff(uint later, uint earlier)
		{
			return (int)(later - earlier);
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0021ACF4 File Offset: 0x00218EF4
		public KCP(uint conv, object user)
		{
			this.user_ = user;
			this.conv_ = conv;
			this.snd_wnd_ = 32U;
			this.rcv_wnd_ = 32U;
			this.rmt_wnd_ = 32U;
			this.mtu_ = 1400U;
			this.mss_ = this.mtu_ - 24U;
			this.rx_rto_ = 200;
			this.rx_minrto_ = 100;
			this.interval_ = 100U;
			this.ts_flush_ = 100U;
			this.ssthresh_ = 2U;
			this.dead_link_ = 20U;
			this.buffer_ = new byte[(this.mtu_ + 24U) * 3U];
			this.snd_queue_ = new LinkedList<KCP.Segment>();
			this.rcv_queue_ = new LinkedList<KCP.Segment>();
			this.snd_buf_ = new LinkedList<KCP.Segment>();
			this.rcv_buf_ = new LinkedList<KCP.Segment>();
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0021ADBC File Offset: 0x00218FBC
		public void Release()
		{
			this.snd_buf_.Clear();
			this.rcv_buf_.Clear();
			this.snd_queue_.Clear();
			this.rcv_queue_.Clear();
			this.nrcv_buf_ = 0U;
			this.nsnd_buf_ = 0U;
			this.nrcv_que_ = 0U;
			this.nsnd_que_ = 0U;
			this.ackblock_ = 0U;
			this.ackcount_ = 0U;
			this.buffer_ = null;
			this.acklist_ = null;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0021AE2D File Offset: 0x0021902D
		public void SetOutput(KCP.OutputDelegate output)
		{
			this.output_ = output;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0021AE38 File Offset: 0x00219038
		public int Recv(byte[] buffer, int offset, int len)
		{
			int num = (len < 0) ? 1 : 0;
			int num2 = 0;
			if (this.rcv_queue_.Count == 0)
			{
				return -1;
			}
			if (len < 0)
			{
				len = -len;
			}
			int num3 = this.PeekSize();
			if (num3 < 0)
			{
				return -2;
			}
			if (num3 > len)
			{
				return -3;
			}
			if (this.nrcv_que_ >= this.rcv_wnd_)
			{
				num2 = 1;
			}
			len = 0;
			LinkedListNode<KCP.Segment> next;
			for (LinkedListNode<KCP.Segment> linkedListNode = this.rcv_queue_.First; linkedListNode != null; linkedListNode = next)
			{
				KCP.Segment value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (buffer != null)
				{
					Buffer.BlockCopy(value.data, 0, buffer, offset, value.data.Length);
					offset += value.data.Length;
				}
				len += value.data.Length;
				int frg = (int)value.frg;
				this.Log(8, "recv sn={0}", new object[]
				{
					value.sn
				});
				if (num == 0)
				{
					this.rcv_queue_.Remove(linkedListNode);
					this.nrcv_que_ -= 1U;
				}
				if (frg == 0)
				{
					IL_172:
					while (this.rcv_buf_.Count > 0)
					{
						LinkedListNode<KCP.Segment> first = this.rcv_buf_.First;
						if (first.Value.sn != this.rcv_nxt_ || this.nrcv_que_ >= this.rcv_wnd_)
						{
							break;
						}
						this.rcv_buf_.Remove(first);
						this.nrcv_buf_ -= 1U;
						this.rcv_queue_.AddLast(first);
						this.nrcv_que_ += 1U;
						this.rcv_nxt_ += 1U;
					}
					if (this.nrcv_que_ < this.rcv_wnd_ && num2 != 0)
					{
						this.probe_ |= 2U;
					}
					return len;
				}
			}
			goto IL_172;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0021AFE8 File Offset: 0x002191E8
		public int PeekSize()
		{
			if (this.rcv_queue_.Count == 0)
			{
				return -1;
			}
			LinkedListNode<KCP.Segment> linkedListNode = this.rcv_queue_.First;
			KCP.Segment value = linkedListNode.Value;
			if (value.frg == 0U)
			{
				return value.data.Length;
			}
			if (this.nrcv_que_ < value.frg + 1U)
			{
				return -1;
			}
			int num = 0;
			for (linkedListNode = this.rcv_queue_.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				value = linkedListNode.Value;
				num += value.data.Length;
				if (value.frg == 0U)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0021B070 File Offset: 0x00219270
		public int Send(byte[] buffer, int offset, int len)
		{
			if (len < 0)
			{
				return -1;
			}
			int num;
			if (len <= (int)this.mss_)
			{
				num = 1;
			}
			else
			{
				num = (len + (int)this.mss_ - 1) / (int)this.mss_;
			}
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
				int num2 = (int)((len > (int)this.mss_) ? this.mss_ : ((uint)len));
				KCP.Segment segment = new KCP.Segment(num2);
				if (buffer != null && len > 0)
				{
					Buffer.BlockCopy(buffer, offset, segment.data, 0, num2);
					offset += num2;
				}
				segment.frg = (uint)(num - i - 1);
				this.snd_queue_.AddLast(segment);
				this.nsnd_que_ += 1U;
				len -= num2;
			}
			return 0;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0021B124 File Offset: 0x00219324
		private void UpdateACK(int rtt)
		{
			if (this.rx_srtt_ == 0)
			{
				this.rx_srtt_ = rtt;
				this.rx_rttval_ = rtt / 2;
			}
			else
			{
				int num = rtt - this.rx_srtt_;
				if (num < 0)
				{
					num = -num;
				}
				this.rx_rttval_ = (3 * this.rx_rttval_ + num) / 4;
				this.rx_srtt_ = (7 * this.rx_srtt_ + rtt) / 8;
				if (this.rx_srtt_ < 1)
				{
					this.rx_srtt_ = 1;
				}
			}
			long num2 = (long)this.rx_srtt_ + (long)((ulong)KCP._imax_(this.interval_, (uint)(4 * this.rx_rttval_)));
			this.rx_rto_ = (int)KCP._ibound_((uint)this.rx_minrto_, (uint)num2, 60000U);
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0021B1C4 File Offset: 0x002193C4
		private void ShrinkBuf()
		{
			LinkedListNode<KCP.Segment> first = this.snd_buf_.First;
			if (first != null)
			{
				KCP.Segment value = first.Value;
				this.snd_una_ = value.sn;
				return;
			}
			this.snd_una_ = this.snd_nxt_;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0021B200 File Offset: 0x00219400
		private void ParseACK(uint sn)
		{
			if (KCP._itimediff(sn, this.snd_una_) < 0 || KCP._itimediff(sn, this.snd_nxt_) >= 0)
			{
				return;
			}
			LinkedListNode<KCP.Segment> next;
			for (LinkedListNode<KCP.Segment> linkedListNode = this.snd_buf_.First; linkedListNode != null; linkedListNode = next)
			{
				KCP.Segment value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (sn == value.sn)
				{
					this.snd_buf_.Remove(linkedListNode);
					this.nsnd_buf_ -= 1U;
					return;
				}
				if (KCP._itimediff(sn, value.sn) < 0)
				{
					break;
				}
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0021B284 File Offset: 0x00219484
		private void ParseUNA(uint una)
		{
			LinkedListNode<KCP.Segment> next;
			for (LinkedListNode<KCP.Segment> linkedListNode = this.snd_buf_.First; linkedListNode != null; linkedListNode = next)
			{
				KCP.Segment value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (KCP._itimediff(una, value.sn) <= 0)
				{
					break;
				}
				this.snd_buf_.Remove(linkedListNode);
				this.nsnd_buf_ -= 1U;
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0021B2E0 File Offset: 0x002194E0
		private void ParseFastACK(uint sn)
		{
			if (KCP._itimediff(sn, this.snd_una_) < 0 || KCP._itimediff(sn, this.snd_nxt_) >= 0)
			{
				return;
			}
			LinkedListNode<KCP.Segment> next;
			for (LinkedListNode<KCP.Segment> linkedListNode = this.snd_buf_.First; linkedListNode != null; linkedListNode = next)
			{
				KCP.Segment value = linkedListNode.Value;
				next = linkedListNode.Next;
				if (KCP._itimediff(sn, value.sn) < 0)
				{
					break;
				}
				if (sn != value.sn)
				{
					value.faskack += 1U;
				}
			}
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0021B358 File Offset: 0x00219558
		private void ACKPush(uint sn, uint ts)
		{
			uint num = this.ackcount_ + 1U;
			if (num > this.ackblock_)
			{
				uint num2;
				for (num2 = 8U; num2 < num; num2 <<= 1)
				{
				}
				uint[] array = new uint[num2 * 2U];
				if (this.acklist_ != null)
				{
					int num3 = 0;
					while ((long)num3 < (long)((ulong)this.ackcount_))
					{
						array[num3 * 2] = this.acklist_[num3 * 2];
						array[num3 * 2 + 1] = this.acklist_[num3 * 2 + 1];
						num3++;
					}
				}
				this.acklist_ = array;
				this.ackblock_ = num2;
			}
			this.acklist_[(int)(this.ackcount_ * 2U)] = sn;
			this.acklist_[(int)(this.ackcount_ * 2U + 1U)] = ts;
			this.ackcount_ += 1U;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0021B407 File Offset: 0x00219607
		private void ACKGet(int pos, ref uint sn, ref uint ts)
		{
			sn = this.acklist_[pos * 2];
			ts = this.acklist_[pos * 2 + 1];
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0021B424 File Offset: 0x00219624
		private void ParseData(KCP.Segment newseg)
		{
			uint sn = newseg.sn;
			int num = 0;
			if (KCP._itimediff(sn, this.rcv_nxt_ + this.rcv_wnd_) >= 0 || KCP._itimediff(sn, this.rcv_nxt_) < 0)
			{
				return;
			}
			LinkedListNode<KCP.Segment> linkedListNode;
			LinkedListNode<KCP.Segment> previous;
			for (linkedListNode = this.rcv_buf_.Last; linkedListNode != null; linkedListNode = previous)
			{
				KCP.Segment value = linkedListNode.Value;
				previous = linkedListNode.Previous;
				if (value.sn == sn)
				{
					num = 1;
					break;
				}
				if (KCP._itimediff(sn, value.sn) > 0)
				{
					break;
				}
			}
			if (num == 0)
			{
				if (linkedListNode != null)
				{
					this.rcv_buf_.AddAfter(linkedListNode, newseg);
				}
				else
				{
					this.rcv_buf_.AddFirst(newseg);
				}
				this.nrcv_buf_ += 1U;
			}
			while (this.rcv_buf_.Count > 0)
			{
				linkedListNode = this.rcv_buf_.First;
				if (linkedListNode.Value.sn != this.rcv_nxt_ || this.nrcv_que_ >= this.rcv_wnd_)
				{
					break;
				}
				this.rcv_buf_.Remove(linkedListNode);
				this.nrcv_buf_ -= 1U;
				this.rcv_queue_.AddLast(linkedListNode);
				this.nrcv_que_ += 1U;
				this.rcv_nxt_ += 1U;
			}
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0021B558 File Offset: 0x00219758
		public int Input(byte[] data, int offset, int size)
		{
			uint num = 0U;
			int num2 = 0;
			this.Log(2, "[RI] {0} bytes", new object[]
			{
				size
			});
			if (data == null || size < 24)
			{
				return -1;
			}
			while (size >= 24)
			{
				uint num3 = KCP.ikcp_decode32u(data, ref offset);
				if (this.conv_ != num3)
				{
					return -1;
				}
				uint num4 = (uint)KCP.ikcp_decode8u(data, ref offset);
				uint frg = (uint)KCP.ikcp_decode8u(data, ref offset);
				uint num5 = (uint)KCP.ikcp_decode16u(data, ref offset);
				uint num6 = KCP.ikcp_decode32u(data, ref offset);
				uint num7 = KCP.ikcp_decode32u(data, ref offset);
				uint una = KCP.ikcp_decode32u(data, ref offset);
				uint num8 = KCP.ikcp_decode32u(data, ref offset);
				size -= 24;
				if ((long)size < (long)((ulong)num8))
				{
					return -2;
				}
				if (num4 != 81U && num4 != 82U && num4 != 83U && num4 != 84U)
				{
					return -3;
				}
				this.rmt_wnd_ = num5;
				this.ParseUNA(una);
				this.ShrinkBuf();
				if (num4 == 82U)
				{
					if (KCP._itimediff(this.current_, num6) >= 0)
					{
						this.UpdateACK(KCP._itimediff(this.current_, num6));
					}
					this.ParseACK(num7);
					this.ShrinkBuf();
					if (num2 == 0)
					{
						num2 = 1;
						num = num7;
					}
					else if (KCP._itimediff(num7, num) > 0)
					{
						num = num7;
					}
					this.Log(16, "input ack: sn={0} rtt={1} rto={2}", new object[]
					{
						num7,
						KCP._itimediff(this.current_, num6),
						this.rx_rto_
					});
				}
				else if (num4 == 81U)
				{
					this.Log(16, "input psh: sn={0} ts={1}", new object[]
					{
						num7,
						num6
					});
					if (KCP._itimediff(num7, this.rcv_nxt_ + this.rcv_wnd_) < 0)
					{
						this.ACKPush(num7, num6);
						if (KCP._itimediff(num7, this.rcv_nxt_) >= 0)
						{
							KCP.Segment segment = new KCP.Segment((int)num8);
							segment.conv = num3;
							segment.cmd = num4;
							segment.frg = frg;
							segment.wnd = num5;
							segment.ts = num6;
							segment.sn = num7;
							segment.una = una;
							if (num8 > 0U)
							{
								Buffer.BlockCopy(data, offset, segment.data, 0, (int)num8);
							}
							this.ParseData(segment);
						}
					}
				}
				else if (num4 == 83U)
				{
					this.probe_ |= 2U;
					this.Log(64, "input probe", Array.Empty<object>());
				}
				else
				{
					if (num4 != 84U)
					{
						return -3;
					}
					this.Log(128, "input wins: {0}", new object[]
					{
						num5
					});
				}
				offset += (int)num8;
				size -= (int)num8;
			}
			if (num2 != 0)
			{
				this.ParseFastACK(num);
			}
			uint earlier = this.snd_una_;
			if (KCP._itimediff(this.snd_una_, earlier) > 0 && this.cwnd_ < this.rmt_wnd_)
			{
				if (this.cwnd_ < this.ssthresh_)
				{
					this.cwnd_ += 1U;
					this.incr_ += this.mss_;
				}
				else
				{
					if (this.incr_ < this.mss_)
					{
						this.incr_ = this.mss_;
					}
					this.incr_ += this.mss_ * this.mss_ / this.incr_ + this.mss_ / 16U;
					if ((this.cwnd_ + 1U) * this.mss_ <= this.incr_)
					{
						this.cwnd_ += 1U;
					}
				}
				if (this.cwnd_ > this.rmt_wnd_)
				{
					this.cwnd_ = this.rmt_wnd_;
					this.incr_ = this.rmt_wnd_ * this.mss_;
				}
			}
			return 0;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0021B8F7 File Offset: 0x00219AF7
		private int WndUnused()
		{
			if (this.nrcv_que_ < this.rcv_wnd_)
			{
				return (int)(this.rcv_wnd_ - this.nrcv_que_);
			}
			return 0;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0021B918 File Offset: 0x00219B18
		private void Flush()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (this.updated_ == 0U)
			{
				return;
			}
			KCP.Segment segment = new KCP.Segment(0)
			{
				conv = this.conv_,
				cmd = 82U,
				wnd = (uint)this.WndUnused(),
				una = this.rcv_nxt_
			};
			int num4 = (int)this.ackcount_;
			for (int i = 0; i < num4; i++)
			{
				if ((long)(num3 + 24) > (long)((ulong)this.mtu_))
				{
					this.output_(this.buffer_, num3, this.user_);
					num3 = 0;
				}
				this.ACKGet(i, ref segment.sn, ref segment.ts);
				segment.Encode(this.buffer_, ref num3);
			}
			this.ackcount_ = 0U;
			if (this.rmt_wnd_ == 0U)
			{
				if (this.probe_wait_ == 0U)
				{
					this.probe_wait_ = 7000U;
					this.ts_probe_ = this.current_ + this.probe_wait_;
				}
				else if (KCP._itimediff(this.current_, this.ts_probe_) >= 0)
				{
					if (this.probe_wait_ < 7000U)
					{
						this.probe_wait_ = 7000U;
					}
					this.probe_wait_ += this.probe_wait_ / 2U;
					if (this.probe_wait_ > 120000U)
					{
						this.probe_wait_ = 120000U;
					}
					this.ts_probe_ = this.current_ + this.probe_wait_;
					this.probe_ |= 1U;
				}
			}
			else
			{
				this.ts_probe_ = 0U;
				this.probe_wait_ = 0U;
			}
			if ((this.probe_ & 1U) > 0U)
			{
				segment.cmd = 83U;
				if ((long)(num3 + 24) > (long)((ulong)this.mtu_))
				{
					this.output_(this.buffer_, num3, this.user_);
					num3 = 0;
				}
				segment.Encode(this.buffer_, ref num3);
			}
			if ((this.probe_ & 2U) > 0U)
			{
				segment.cmd = 84U;
				if ((long)(num3 + 24) > (long)((ulong)this.mtu_))
				{
					this.output_(this.buffer_, num3, this.user_);
					num3 = 0;
				}
				segment.Encode(this.buffer_, ref num3);
			}
			this.probe_ = 0U;
			uint num5 = KCP._imin_(this.snd_wnd_, this.rmt_wnd_);
			if (this.nocwnd_ == 0)
			{
				num5 = KCP._imin_(this.cwnd_, num5);
			}
			while (KCP._itimediff(this.snd_nxt_, this.snd_una_ + num5) < 0 && this.snd_queue_.Count != 0)
			{
				LinkedListNode<KCP.Segment> first = this.snd_queue_.First;
				KCP.Segment value = first.Value;
				this.snd_queue_.Remove(first);
				this.snd_buf_.AddLast(first);
				this.nsnd_que_ -= 1U;
				this.nsnd_buf_ += 1U;
				value.conv = this.conv_;
				value.cmd = 81U;
				value.wnd = segment.wnd;
				value.ts = this.current_;
				uint num6 = this.snd_nxt_;
				this.snd_nxt_ = num6 + 1U;
				value.sn = num6;
				value.una = this.rcv_nxt_;
				value.resendts = this.current_;
				value.rto = (uint)this.rx_rto_;
				value.faskack = 0U;
				value.xmit = 0U;
			}
			uint num7 = (uint)((this.fastresend_ > 0) ? this.fastresend_ : -1);
			uint num8 = (uint)((this.nodelay_ == 0U) ? (this.rx_rto_ >> 3) : 0);
			for (LinkedListNode<KCP.Segment> linkedListNode = this.snd_buf_.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				KCP.Segment value2 = linkedListNode.Value;
				int num9 = 0;
				if (value2.xmit == 0U)
				{
					num9 = 1;
					value2.xmit += 1U;
					value2.rto = (uint)this.rx_rto_;
					value2.resendts = this.current_ + value2.rto + num8;
				}
				else if (KCP._itimediff(this.current_, value2.resendts) >= 0)
				{
					num9 = 1;
					value2.xmit += 1U;
					this.xmit_ += 1U;
					if (this.nodelay_ == 0U)
					{
						value2.rto += (uint)this.rx_rto_;
					}
					else
					{
						value2.rto += (uint)(this.rx_rto_ / 2);
					}
					value2.resendts = this.current_ + value2.rto;
					num2 = 1;
				}
				else if (value2.faskack >= num7)
				{
					num9 = 1;
					value2.xmit += 1U;
					value2.faskack = 0U;
					value2.resendts = this.current_ + value2.rto;
					num++;
				}
				if (num9 > 0)
				{
					value2.ts = this.current_;
					value2.wnd = segment.wnd;
					value2.una = this.rcv_nxt_;
					int num10 = 24;
					if (value2.data != null)
					{
						num10 += value2.data.Length;
					}
					if ((long)(num3 + num10) > (long)((ulong)this.mtu_))
					{
						this.output_(this.buffer_, num3, this.user_);
						num3 = 0;
					}
					value2.Encode(this.buffer_, ref num3);
					if (value2.data.Length != 0)
					{
						Buffer.BlockCopy(value2.data, 0, this.buffer_, num3, value2.data.Length);
						num3 += value2.data.Length;
					}
					if (value2.xmit >= this.dead_link_)
					{
						this.state_ = uint.MaxValue;
					}
				}
			}
			if (num3 > 0)
			{
				this.output_(this.buffer_, num3, this.user_);
				num3 = 0;
			}
			if (num > 0)
			{
				uint num11 = this.snd_nxt_ - this.snd_una_;
				this.ssthresh_ = num11 / 2U;
				if (this.ssthresh_ < 2U)
				{
					this.ssthresh_ = 2U;
				}
				this.cwnd_ = this.ssthresh_ + num7;
				this.incr_ = this.cwnd_ * this.mss_;
			}
			if (num2 > 0)
			{
				this.ssthresh_ = num5 / 2U;
				if (this.ssthresh_ < 2U)
				{
					this.ssthresh_ = 2U;
				}
				this.cwnd_ = 1U;
				this.incr_ = this.mss_;
			}
			if (this.cwnd_ < 1U)
			{
				this.cwnd_ = 1U;
				this.incr_ = this.mss_;
			}
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0021BF28 File Offset: 0x0021A128
		public void Update(uint current)
		{
			this.current_ = current;
			if (this.updated_ == 0U)
			{
				this.updated_ = 1U;
				this.ts_flush_ = current;
			}
			int num = KCP._itimediff(this.current_, this.ts_flush_);
			if (num >= 10000 || num < -10000)
			{
				this.ts_flush_ = current;
				num = 0;
			}
			if (num >= 0)
			{
				this.ts_flush_ += this.interval_;
				if (KCP._itimediff(this.current_, this.ts_flush_) >= 0)
				{
					this.ts_flush_ = this.current_ + this.interval_;
				}
				this.Flush();
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0021BFC4 File Offset: 0x0021A1C4
		public uint Check(uint current)
		{
			uint num = this.ts_flush_;
			int num2 = int.MaxValue;
			if (this.updated_ == 0U)
			{
				return current;
			}
			if (KCP._itimediff(current, num) >= 10000 || KCP._itimediff(current, num) < -10000)
			{
				num = current;
			}
			if (KCP._itimediff(current, num) >= 0)
			{
				return current;
			}
			int num3 = KCP._itimediff(num, current);
			for (LinkedListNode<KCP.Segment> linkedListNode = this.snd_buf_.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				int num4 = KCP._itimediff(linkedListNode.Value.resendts, current);
				if (num4 <= 0)
				{
					return current;
				}
				if (num4 < num2)
				{
					num2 = num4;
				}
			}
			uint num5 = (uint)((num2 < num3) ? num2 : num3);
			if (num5 >= this.interval_)
			{
				num5 = this.interval_;
			}
			return current + num5;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0021C07C File Offset: 0x0021A27C
		public int SetMTU(int mtu)
		{
			if (mtu < 50 || mtu < 24)
			{
				return -1;
			}
			byte[] array = new byte[(mtu + 24) * 3];
			this.mtu_ = (uint)mtu;
			this.mss_ = this.mtu_ - 24U;
			this.buffer_ = array;
			return 0;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0021C0BF File Offset: 0x0021A2BF
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
			this.interval_ = (uint)interval;
			return 0;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0021C0E4 File Offset: 0x0021A2E4
		public int NoDelay(int nodelay, int interval, int resend, int nc)
		{
			if (nodelay >= 0)
			{
				this.nodelay_ = (uint)nodelay;
				if (nodelay > 0)
				{
					this.rx_minrto_ = 30;
				}
				else
				{
					this.rx_minrto_ = 100;
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
				this.interval_ = (uint)interval;
			}
			if (resend >= 0)
			{
				this.fastresend_ = resend;
			}
			if (nc >= 0)
			{
				this.nocwnd_ = nc;
			}
			return 0;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0021C150 File Offset: 0x0021A350
		public int WndSize(int sndwnd, int rcvwnd)
		{
			if (sndwnd > 0)
			{
				this.snd_wnd_ = (uint)sndwnd;
			}
			if (rcvwnd > 0)
			{
				this.rcv_wnd_ = (uint)rcvwnd;
			}
			return 0;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0021C169 File Offset: 0x0021A369
		public int WaitSnd()
		{
			return (int)(this.nsnd_buf_ + this.nsnd_que_);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0021C178 File Offset: 0x0021A378
		public uint GetConv()
		{
			return this.conv_;
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0021C180 File Offset: 0x0021A380
		public uint GetState()
		{
			return this.state_;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0021C188 File Offset: 0x0021A388
		public void SetMinRTO(int minrto)
		{
			this.rx_minrto_ = minrto;
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0021C191 File Offset: 0x0021A391
		public void SetFastResend(int resend)
		{
			this.fastresend_ = resend;
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x00004095 File Offset: 0x00002295
		private void Log(int mask, string format, params object[] args)
		{
		}

		// Token: 0x04004ED3 RID: 20179
		public const int IKCP_RTO_NDL = 30;

		// Token: 0x04004ED4 RID: 20180
		public const int IKCP_RTO_MIN = 100;

		// Token: 0x04004ED5 RID: 20181
		public const int IKCP_RTO_DEF = 200;

		// Token: 0x04004ED6 RID: 20182
		public const int IKCP_RTO_MAX = 60000;

		// Token: 0x04004ED7 RID: 20183
		public const int IKCP_CMD_PUSH = 81;

		// Token: 0x04004ED8 RID: 20184
		public const int IKCP_CMD_ACK = 82;

		// Token: 0x04004ED9 RID: 20185
		public const int IKCP_CMD_WASK = 83;

		// Token: 0x04004EDA RID: 20186
		public const int IKCP_CMD_WINS = 84;

		// Token: 0x04004EDB RID: 20187
		public const int IKCP_ASK_SEND = 1;

		// Token: 0x04004EDC RID: 20188
		public const int IKCP_ASK_TELL = 2;

		// Token: 0x04004EDD RID: 20189
		public const int IKCP_WND_SND = 32;

		// Token: 0x04004EDE RID: 20190
		public const int IKCP_WND_RCV = 32;

		// Token: 0x04004EDF RID: 20191
		public const int IKCP_MTU_DEF = 1400;

		// Token: 0x04004EE0 RID: 20192
		public const int IKCP_ACK_FAST = 3;

		// Token: 0x04004EE1 RID: 20193
		public const int IKCP_INTERVAL = 100;

		// Token: 0x04004EE2 RID: 20194
		public const int IKCP_OVERHEAD = 24;

		// Token: 0x04004EE3 RID: 20195
		public const int IKCP_DEADLINK = 20;

		// Token: 0x04004EE4 RID: 20196
		public const int IKCP_THRESH_INIT = 2;

		// Token: 0x04004EE5 RID: 20197
		public const int IKCP_THRESH_MIN = 2;

		// Token: 0x04004EE6 RID: 20198
		public const int IKCP_PROBE_INIT = 7000;

		// Token: 0x04004EE7 RID: 20199
		public const int IKCP_PROBE_LIMIT = 120000;

		// Token: 0x04004EE8 RID: 20200
		public const int IKCP_LOG_OUTPUT = 1;

		// Token: 0x04004EE9 RID: 20201
		public const int IKCP_LOG_INPUT = 2;

		// Token: 0x04004EEA RID: 20202
		public const int IKCP_LOG_SEND = 4;

		// Token: 0x04004EEB RID: 20203
		public const int IKCP_LOG_RECV = 8;

		// Token: 0x04004EEC RID: 20204
		public const int IKCP_LOG_IN_DATA = 16;

		// Token: 0x04004EED RID: 20205
		public const int IKCP_LOG_IN_ACK = 32;

		// Token: 0x04004EEE RID: 20206
		public const int IKCP_LOG_IN_PROBE = 64;

		// Token: 0x04004EEF RID: 20207
		public const int IKCP_LOG_IN_WINS = 128;

		// Token: 0x04004EF0 RID: 20208
		public const int IKCP_LOG_OUT_DATA = 256;

		// Token: 0x04004EF1 RID: 20209
		public const int IKCP_LOG_OUT_ACK = 512;

		// Token: 0x04004EF2 RID: 20210
		public const int IKCP_LOG_OUT_PROBE = 1024;

		// Token: 0x04004EF3 RID: 20211
		public const int IKCP_LOG_OUT_WINS = 2048;

		// Token: 0x04004EF4 RID: 20212
		private uint conv_;

		// Token: 0x04004EF5 RID: 20213
		private uint mtu_;

		// Token: 0x04004EF6 RID: 20214
		private uint mss_;

		// Token: 0x04004EF7 RID: 20215
		private uint state_;

		// Token: 0x04004EF8 RID: 20216
		private uint snd_una_;

		// Token: 0x04004EF9 RID: 20217
		private uint snd_nxt_;

		// Token: 0x04004EFA RID: 20218
		private uint rcv_nxt_;

		// Token: 0x04004EFB RID: 20219
		private uint ssthresh_;

		// Token: 0x04004EFC RID: 20220
		private int rx_rttval_;

		// Token: 0x04004EFD RID: 20221
		private int rx_srtt_;

		// Token: 0x04004EFE RID: 20222
		private int rx_rto_;

		// Token: 0x04004EFF RID: 20223
		private int rx_minrto_;

		// Token: 0x04004F00 RID: 20224
		private uint snd_wnd_;

		// Token: 0x04004F01 RID: 20225
		private uint rcv_wnd_;

		// Token: 0x04004F02 RID: 20226
		private uint rmt_wnd_;

		// Token: 0x04004F03 RID: 20227
		private uint cwnd_;

		// Token: 0x04004F04 RID: 20228
		private uint probe_;

		// Token: 0x04004F05 RID: 20229
		private uint current_;

		// Token: 0x04004F06 RID: 20230
		private uint interval_;

		// Token: 0x04004F07 RID: 20231
		private uint ts_flush_;

		// Token: 0x04004F08 RID: 20232
		private uint xmit_;

		// Token: 0x04004F09 RID: 20233
		private uint nrcv_buf_;

		// Token: 0x04004F0A RID: 20234
		private uint nsnd_buf_;

		// Token: 0x04004F0B RID: 20235
		private uint nrcv_que_;

		// Token: 0x04004F0C RID: 20236
		private uint nsnd_que_;

		// Token: 0x04004F0D RID: 20237
		private uint nodelay_;

		// Token: 0x04004F0E RID: 20238
		private uint updated_;

		// Token: 0x04004F0F RID: 20239
		private uint ts_probe_;

		// Token: 0x04004F10 RID: 20240
		private uint probe_wait_;

		// Token: 0x04004F11 RID: 20241
		private uint dead_link_;

		// Token: 0x04004F12 RID: 20242
		private uint incr_;

		// Token: 0x04004F13 RID: 20243
		private LinkedList<KCP.Segment> snd_queue_;

		// Token: 0x04004F14 RID: 20244
		private LinkedList<KCP.Segment> rcv_queue_;

		// Token: 0x04004F15 RID: 20245
		private LinkedList<KCP.Segment> snd_buf_;

		// Token: 0x04004F16 RID: 20246
		private LinkedList<KCP.Segment> rcv_buf_;

		// Token: 0x04004F17 RID: 20247
		private uint[] acklist_;

		// Token: 0x04004F18 RID: 20248
		private uint ackcount_;

		// Token: 0x04004F19 RID: 20249
		private uint ackblock_;

		// Token: 0x04004F1A RID: 20250
		private byte[] buffer_;

		// Token: 0x04004F1B RID: 20251
		private object user_;

		// Token: 0x04004F1C RID: 20252
		private int fastresend_;

		// Token: 0x04004F1D RID: 20253
		private int nocwnd_;

		// Token: 0x04004F1E RID: 20254
		private KCP.OutputDelegate output_;

		// Token: 0x020015EA RID: 5610
		public class TimeUtils
		{
			// Token: 0x06008585 RID: 34181 RVA: 0x002E4690 File Offset: 0x002E2890
			public static uint iclock()
			{
				return (uint)(Convert.ToInt64(DateTime.Now.Subtract(KCP.TimeUtils.twepoch).TotalMilliseconds) & (long)((ulong)-1));
			}

			// Token: 0x06008586 RID: 34182 RVA: 0x002E46C0 File Offset: 0x002E28C0
			public static long LocalUnixTime()
			{
				return Convert.ToInt64(DateTime.Now.Subtract(KCP.TimeUtils.epoch).TotalMilliseconds);
			}

			// Token: 0x06008587 RID: 34183 RVA: 0x002E46EC File Offset: 0x002E28EC
			public static long ToUnixTimestamp(DateTime t)
			{
				return (long)Math.Truncate(t.ToUniversalTime().Subtract(KCP.TimeUtils.epoch).TotalSeconds);
			}

			// Token: 0x040070CE RID: 28878
			private static readonly DateTime epoch = new DateTime(1970, 1, 1);

			// Token: 0x040070CF RID: 28879
			private static readonly DateTime twepoch = new DateTime(2000, 1, 1);
		}

		// Token: 0x020015EB RID: 5611
		internal class Segment
		{
			// Token: 0x0600858A RID: 34186 RVA: 0x002E473F File Offset: 0x002E293F
			internal Segment(int size = 0)
			{
				this.data = new byte[size];
			}

			// Token: 0x0600858B RID: 34187 RVA: 0x002E4754 File Offset: 0x002E2954
			internal void Encode(byte[] ptr, ref int offset)
			{
				uint l = (uint)this.data.Length;
				KCP.ikcp_encode32u(ptr, offset, this.conv);
				KCP.ikcp_encode8u(ptr, offset + 4, (byte)this.cmd);
				KCP.ikcp_encode8u(ptr, offset + 5, (byte)this.frg);
				KCP.ikcp_encode16u(ptr, offset + 6, (ushort)this.wnd);
				KCP.ikcp_encode32u(ptr, offset + 8, this.ts);
				KCP.ikcp_encode32u(ptr, offset + 12, this.sn);
				KCP.ikcp_encode32u(ptr, offset + 16, this.una);
				KCP.ikcp_encode32u(ptr, offset + 20, l);
				offset += 24;
			}

			// Token: 0x040070D0 RID: 28880
			internal uint conv;

			// Token: 0x040070D1 RID: 28881
			internal uint cmd;

			// Token: 0x040070D2 RID: 28882
			internal uint frg;

			// Token: 0x040070D3 RID: 28883
			internal uint wnd;

			// Token: 0x040070D4 RID: 28884
			internal uint ts;

			// Token: 0x040070D5 RID: 28885
			internal uint sn;

			// Token: 0x040070D6 RID: 28886
			internal uint una;

			// Token: 0x040070D7 RID: 28887
			internal uint resendts;

			// Token: 0x040070D8 RID: 28888
			internal uint rto;

			// Token: 0x040070D9 RID: 28889
			internal uint faskack;

			// Token: 0x040070DA RID: 28890
			internal uint xmit;

			// Token: 0x040070DB RID: 28891
			internal byte[] data;
		}

		// Token: 0x020015EC RID: 5612
		// (Invoke) Token: 0x0600858D RID: 34189
		public delegate void OutputDelegate(byte[] data, int size, object user);
	}
}
