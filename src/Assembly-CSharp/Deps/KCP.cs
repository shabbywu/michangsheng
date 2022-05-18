using System;
using System.Collections.Generic;

namespace Deps
{
	// Token: 0x02000EA6 RID: 3750
	public class KCP
	{
		// Token: 0x060059E0 RID: 23008 RVA: 0x0003FC6A File Offset: 0x0003DE6A
		public static void ikcp_encode8u(byte[] p, int offset, byte c)
		{
			p[offset] = c;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0024A840 File Offset: 0x00248A40
		public static byte ikcp_decode8u(byte[] p, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			return p[num];
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0003FC70 File Offset: 0x0003DE70
		public static void ikcp_encode16u(byte[] p, int offset, ushort v)
		{
			p[offset] = (byte)(v & 255);
			p[offset + 1] = (byte)(v >> 8);
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x0024A858 File Offset: 0x00248A58
		public static ushort ikcp_decode16u(byte[] p, ref int offset)
		{
			int num = offset;
			offset += 2;
			return (ushort)p[num] | (ushort)(p[num + 1] << 8);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x0003FC86 File Offset: 0x0003DE86
		public static void ikcp_encode32u(byte[] p, int offset, uint l)
		{
			p[offset] = (byte)(l & 255U);
			p[offset + 1] = (byte)(l >> 8);
			p[offset + 2] = (byte)(l >> 16);
			p[offset + 3] = (byte)(l >> 24);
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x0024A87C File Offset: 0x00248A7C
		public static uint ikcp_decode32u(byte[] p, ref int offset)
		{
			int num = offset;
			offset += 4;
			return (uint)((int)p[num] | (int)p[num + 1] << 8 | (int)p[num + 2] << 16 | (int)p[num + 3] << 24);
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
		public static uint _imin_(uint a, uint b)
		{
			if (a > b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x0003FCB9 File Offset: 0x0003DEB9
		public static uint _imax_(uint a, uint b)
		{
			if (a < b)
			{
				return b;
			}
			return a;
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x0003FCC2 File Offset: 0x0003DEC2
		public static uint _ibound_(uint lower, uint middle, uint upper)
		{
			return KCP._imin_(KCP._imax_(lower, middle), upper);
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x0003FCD1 File Offset: 0x0003DED1
		public static int _itimediff(uint later, uint earlier)
		{
			return (int)(later - earlier);
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0024A8B0 File Offset: 0x00248AB0
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

		// Token: 0x060059EB RID: 23019 RVA: 0x0024A978 File Offset: 0x00248B78
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

		// Token: 0x060059EC RID: 23020 RVA: 0x0003FCD6 File Offset: 0x0003DED6
		public void SetOutput(KCP.OutputDelegate output)
		{
			this.output_ = output;
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x0024A9EC File Offset: 0x00248BEC
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

		// Token: 0x060059EE RID: 23022 RVA: 0x0024AB9C File Offset: 0x00248D9C
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

		// Token: 0x060059EF RID: 23023 RVA: 0x0024AC24 File Offset: 0x00248E24
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

		// Token: 0x060059F0 RID: 23024 RVA: 0x0024ACD8 File Offset: 0x00248ED8
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

		// Token: 0x060059F1 RID: 23025 RVA: 0x0024AD78 File Offset: 0x00248F78
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

		// Token: 0x060059F2 RID: 23026 RVA: 0x0024ADB4 File Offset: 0x00248FB4
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

		// Token: 0x060059F3 RID: 23027 RVA: 0x0024AE38 File Offset: 0x00249038
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

		// Token: 0x060059F4 RID: 23028 RVA: 0x0024AE94 File Offset: 0x00249094
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

		// Token: 0x060059F5 RID: 23029 RVA: 0x0024AF0C File Offset: 0x0024910C
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

		// Token: 0x060059F6 RID: 23030 RVA: 0x0003FCDF File Offset: 0x0003DEDF
		private void ACKGet(int pos, ref uint sn, ref uint ts)
		{
			sn = this.acklist_[pos * 2];
			ts = this.acklist_[pos * 2 + 1];
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x0024AFBC File Offset: 0x002491BC
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

		// Token: 0x060059F8 RID: 23032 RVA: 0x0024B0F0 File Offset: 0x002492F0
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

		// Token: 0x060059F9 RID: 23033 RVA: 0x0003FCFB File Offset: 0x0003DEFB
		private int WndUnused()
		{
			if (this.nrcv_que_ < this.rcv_wnd_)
			{
				return (int)(this.rcv_wnd_ - this.nrcv_que_);
			}
			return 0;
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x0024B490 File Offset: 0x00249690
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

		// Token: 0x060059FB RID: 23035 RVA: 0x0024BAA0 File Offset: 0x00249CA0
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

		// Token: 0x060059FC RID: 23036 RVA: 0x0024BB3C File Offset: 0x00249D3C
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

		// Token: 0x060059FD RID: 23037 RVA: 0x0024BBF4 File Offset: 0x00249DF4
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

		// Token: 0x060059FE RID: 23038 RVA: 0x0003FD1A File Offset: 0x0003DF1A
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

		// Token: 0x060059FF RID: 23039 RVA: 0x0024BC38 File Offset: 0x00249E38
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

		// Token: 0x06005A00 RID: 23040 RVA: 0x0003FD3E File Offset: 0x0003DF3E
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

		// Token: 0x06005A01 RID: 23041 RVA: 0x0003FD57 File Offset: 0x0003DF57
		public int WaitSnd()
		{
			return (int)(this.nsnd_buf_ + this.nsnd_que_);
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x0003FD66 File Offset: 0x0003DF66
		public uint GetConv()
		{
			return this.conv_;
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0003FD6E File Offset: 0x0003DF6E
		public uint GetState()
		{
			return this.state_;
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x0003FD76 File Offset: 0x0003DF76
		public void SetMinRTO(int minrto)
		{
			this.rx_minrto_ = minrto;
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x0003FD7F File Offset: 0x0003DF7F
		public void SetFastResend(int resend)
		{
			this.fastresend_ = resend;
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x000042DD File Offset: 0x000024DD
		private void Log(int mask, string format, params object[] args)
		{
		}

		// Token: 0x0400594A RID: 22858
		public const int IKCP_RTO_NDL = 30;

		// Token: 0x0400594B RID: 22859
		public const int IKCP_RTO_MIN = 100;

		// Token: 0x0400594C RID: 22860
		public const int IKCP_RTO_DEF = 200;

		// Token: 0x0400594D RID: 22861
		public const int IKCP_RTO_MAX = 60000;

		// Token: 0x0400594E RID: 22862
		public const int IKCP_CMD_PUSH = 81;

		// Token: 0x0400594F RID: 22863
		public const int IKCP_CMD_ACK = 82;

		// Token: 0x04005950 RID: 22864
		public const int IKCP_CMD_WASK = 83;

		// Token: 0x04005951 RID: 22865
		public const int IKCP_CMD_WINS = 84;

		// Token: 0x04005952 RID: 22866
		public const int IKCP_ASK_SEND = 1;

		// Token: 0x04005953 RID: 22867
		public const int IKCP_ASK_TELL = 2;

		// Token: 0x04005954 RID: 22868
		public const int IKCP_WND_SND = 32;

		// Token: 0x04005955 RID: 22869
		public const int IKCP_WND_RCV = 32;

		// Token: 0x04005956 RID: 22870
		public const int IKCP_MTU_DEF = 1400;

		// Token: 0x04005957 RID: 22871
		public const int IKCP_ACK_FAST = 3;

		// Token: 0x04005958 RID: 22872
		public const int IKCP_INTERVAL = 100;

		// Token: 0x04005959 RID: 22873
		public const int IKCP_OVERHEAD = 24;

		// Token: 0x0400595A RID: 22874
		public const int IKCP_DEADLINK = 20;

		// Token: 0x0400595B RID: 22875
		public const int IKCP_THRESH_INIT = 2;

		// Token: 0x0400595C RID: 22876
		public const int IKCP_THRESH_MIN = 2;

		// Token: 0x0400595D RID: 22877
		public const int IKCP_PROBE_INIT = 7000;

		// Token: 0x0400595E RID: 22878
		public const int IKCP_PROBE_LIMIT = 120000;

		// Token: 0x0400595F RID: 22879
		public const int IKCP_LOG_OUTPUT = 1;

		// Token: 0x04005960 RID: 22880
		public const int IKCP_LOG_INPUT = 2;

		// Token: 0x04005961 RID: 22881
		public const int IKCP_LOG_SEND = 4;

		// Token: 0x04005962 RID: 22882
		public const int IKCP_LOG_RECV = 8;

		// Token: 0x04005963 RID: 22883
		public const int IKCP_LOG_IN_DATA = 16;

		// Token: 0x04005964 RID: 22884
		public const int IKCP_LOG_IN_ACK = 32;

		// Token: 0x04005965 RID: 22885
		public const int IKCP_LOG_IN_PROBE = 64;

		// Token: 0x04005966 RID: 22886
		public const int IKCP_LOG_IN_WINS = 128;

		// Token: 0x04005967 RID: 22887
		public const int IKCP_LOG_OUT_DATA = 256;

		// Token: 0x04005968 RID: 22888
		public const int IKCP_LOG_OUT_ACK = 512;

		// Token: 0x04005969 RID: 22889
		public const int IKCP_LOG_OUT_PROBE = 1024;

		// Token: 0x0400596A RID: 22890
		public const int IKCP_LOG_OUT_WINS = 2048;

		// Token: 0x0400596B RID: 22891
		private uint conv_;

		// Token: 0x0400596C RID: 22892
		private uint mtu_;

		// Token: 0x0400596D RID: 22893
		private uint mss_;

		// Token: 0x0400596E RID: 22894
		private uint state_;

		// Token: 0x0400596F RID: 22895
		private uint snd_una_;

		// Token: 0x04005970 RID: 22896
		private uint snd_nxt_;

		// Token: 0x04005971 RID: 22897
		private uint rcv_nxt_;

		// Token: 0x04005972 RID: 22898
		private uint ssthresh_;

		// Token: 0x04005973 RID: 22899
		private int rx_rttval_;

		// Token: 0x04005974 RID: 22900
		private int rx_srtt_;

		// Token: 0x04005975 RID: 22901
		private int rx_rto_;

		// Token: 0x04005976 RID: 22902
		private int rx_minrto_;

		// Token: 0x04005977 RID: 22903
		private uint snd_wnd_;

		// Token: 0x04005978 RID: 22904
		private uint rcv_wnd_;

		// Token: 0x04005979 RID: 22905
		private uint rmt_wnd_;

		// Token: 0x0400597A RID: 22906
		private uint cwnd_;

		// Token: 0x0400597B RID: 22907
		private uint probe_;

		// Token: 0x0400597C RID: 22908
		private uint current_;

		// Token: 0x0400597D RID: 22909
		private uint interval_;

		// Token: 0x0400597E RID: 22910
		private uint ts_flush_;

		// Token: 0x0400597F RID: 22911
		private uint xmit_;

		// Token: 0x04005980 RID: 22912
		private uint nrcv_buf_;

		// Token: 0x04005981 RID: 22913
		private uint nsnd_buf_;

		// Token: 0x04005982 RID: 22914
		private uint nrcv_que_;

		// Token: 0x04005983 RID: 22915
		private uint nsnd_que_;

		// Token: 0x04005984 RID: 22916
		private uint nodelay_;

		// Token: 0x04005985 RID: 22917
		private uint updated_;

		// Token: 0x04005986 RID: 22918
		private uint ts_probe_;

		// Token: 0x04005987 RID: 22919
		private uint probe_wait_;

		// Token: 0x04005988 RID: 22920
		private uint dead_link_;

		// Token: 0x04005989 RID: 22921
		private uint incr_;

		// Token: 0x0400598A RID: 22922
		private LinkedList<KCP.Segment> snd_queue_;

		// Token: 0x0400598B RID: 22923
		private LinkedList<KCP.Segment> rcv_queue_;

		// Token: 0x0400598C RID: 22924
		private LinkedList<KCP.Segment> snd_buf_;

		// Token: 0x0400598D RID: 22925
		private LinkedList<KCP.Segment> rcv_buf_;

		// Token: 0x0400598E RID: 22926
		private uint[] acklist_;

		// Token: 0x0400598F RID: 22927
		private uint ackcount_;

		// Token: 0x04005990 RID: 22928
		private uint ackblock_;

		// Token: 0x04005991 RID: 22929
		private byte[] buffer_;

		// Token: 0x04005992 RID: 22930
		private object user_;

		// Token: 0x04005993 RID: 22931
		private int fastresend_;

		// Token: 0x04005994 RID: 22932
		private int nocwnd_;

		// Token: 0x04005995 RID: 22933
		private KCP.OutputDelegate output_;

		// Token: 0x02000EA7 RID: 3751
		public class TimeUtils
		{
			// Token: 0x06005A07 RID: 23047 RVA: 0x0024BCA4 File Offset: 0x00249EA4
			public static uint iclock()
			{
				return (uint)(Convert.ToInt64(DateTime.Now.Subtract(KCP.TimeUtils.twepoch).TotalMilliseconds) & (long)((ulong)-1));
			}

			// Token: 0x06005A08 RID: 23048 RVA: 0x0024BCD4 File Offset: 0x00249ED4
			public static long LocalUnixTime()
			{
				return Convert.ToInt64(DateTime.Now.Subtract(KCP.TimeUtils.epoch).TotalMilliseconds);
			}

			// Token: 0x06005A09 RID: 23049 RVA: 0x0024BD00 File Offset: 0x00249F00
			public static long ToUnixTimestamp(DateTime t)
			{
				return (long)Math.Truncate(t.ToUniversalTime().Subtract(KCP.TimeUtils.epoch).TotalSeconds);
			}

			// Token: 0x04005996 RID: 22934
			private static readonly DateTime epoch = new DateTime(1970, 1, 1);

			// Token: 0x04005997 RID: 22935
			private static readonly DateTime twepoch = new DateTime(2000, 1, 1);
		}

		// Token: 0x02000EA8 RID: 3752
		internal class Segment
		{
			// Token: 0x06005A0C RID: 23052 RVA: 0x0003FDAC File Offset: 0x0003DFAC
			internal Segment(int size = 0)
			{
				this.data = new byte[size];
			}

			// Token: 0x06005A0D RID: 23053 RVA: 0x0024BD30 File Offset: 0x00249F30
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

			// Token: 0x04005998 RID: 22936
			internal uint conv;

			// Token: 0x04005999 RID: 22937
			internal uint cmd;

			// Token: 0x0400599A RID: 22938
			internal uint frg;

			// Token: 0x0400599B RID: 22939
			internal uint wnd;

			// Token: 0x0400599C RID: 22940
			internal uint ts;

			// Token: 0x0400599D RID: 22941
			internal uint sn;

			// Token: 0x0400599E RID: 22942
			internal uint una;

			// Token: 0x0400599F RID: 22943
			internal uint resendts;

			// Token: 0x040059A0 RID: 22944
			internal uint rto;

			// Token: 0x040059A1 RID: 22945
			internal uint faskack;

			// Token: 0x040059A2 RID: 22946
			internal uint xmit;

			// Token: 0x040059A3 RID: 22947
			internal byte[] data;
		}

		// Token: 0x02000EA9 RID: 3753
		// (Invoke) Token: 0x06005A0F RID: 23055
		public delegate void OutputDelegate(byte[] data, int size, object user);
	}
}
