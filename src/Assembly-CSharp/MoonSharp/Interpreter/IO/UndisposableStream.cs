using System;
using System.IO;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x020010E3 RID: 4323
	public class UndisposableStream : Stream
	{
		// Token: 0x06006858 RID: 26712 RVA: 0x000479CE File Offset: 0x00045BCE
		public UndisposableStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x000042DD File Offset: 0x000024DD
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x000042DD File Offset: 0x000024DD
		public override void Close()
		{
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600685B RID: 26715 RVA: 0x000479DD File Offset: 0x00045BDD
		public override bool CanRead
		{
			get
			{
				return this.m_Stream.CanRead;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600685C RID: 26716 RVA: 0x000479EA File Offset: 0x00045BEA
		public override bool CanSeek
		{
			get
			{
				return this.m_Stream.CanSeek;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600685D RID: 26717 RVA: 0x000479F7 File Offset: 0x00045BF7
		public override bool CanWrite
		{
			get
			{
				return this.m_Stream.CanWrite;
			}
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x00047A04 File Offset: 0x00045C04
		public override void Flush()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600685F RID: 26719 RVA: 0x00047A11 File Offset: 0x00045C11
		public override long Length
		{
			get
			{
				return this.m_Stream.Length;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06006860 RID: 26720 RVA: 0x00047A1E File Offset: 0x00045C1E
		// (set) Token: 0x06006861 RID: 26721 RVA: 0x00047A2B File Offset: 0x00045C2B
		public override long Position
		{
			get
			{
				return this.m_Stream.Position;
			}
			set
			{
				this.m_Stream.Position = value;
			}
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x00047A39 File Offset: 0x00045C39
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_Stream.Read(buffer, offset, count);
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x00047A49 File Offset: 0x00045C49
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_Stream.Seek(offset, origin);
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x00047A58 File Offset: 0x00045C58
		public override void SetLength(long value)
		{
			this.m_Stream.SetLength(value);
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x00047A66 File Offset: 0x00045C66
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_Stream.Write(buffer, offset, count);
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x00047A76 File Offset: 0x00045C76
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x00047A8A File Offset: 0x00045C8A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x00047A9E File Offset: 0x00045C9E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_Stream.EndWrite(asyncResult);
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x00047AAC File Offset: 0x00045CAC
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_Stream.EndRead(asyncResult);
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600686A RID: 26730 RVA: 0x00047ABA File Offset: 0x00045CBA
		public override bool CanTimeout
		{
			get
			{
				return this.m_Stream.CanTimeout;
			}
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x00047AC7 File Offset: 0x00045CC7
		public override bool Equals(object obj)
		{
			return this.m_Stream.Equals(obj);
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x00047AD5 File Offset: 0x00045CD5
		public override int GetHashCode()
		{
			return this.m_Stream.GetHashCode();
		}

		// Token: 0x0600686D RID: 26733 RVA: 0x00047AE2 File Offset: 0x00045CE2
		public override int ReadByte()
		{
			return this.m_Stream.ReadByte();
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600686E RID: 26734 RVA: 0x00047AEF File Offset: 0x00045CEF
		// (set) Token: 0x0600686F RID: 26735 RVA: 0x00047AFC File Offset: 0x00045CFC
		public override int ReadTimeout
		{
			get
			{
				return this.m_Stream.ReadTimeout;
			}
			set
			{
				this.m_Stream.ReadTimeout = value;
			}
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x00047B0A File Offset: 0x00045D0A
		public override string ToString()
		{
			return this.m_Stream.ToString();
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x00047B17 File Offset: 0x00045D17
		public override void WriteByte(byte value)
		{
			this.m_Stream.WriteByte(value);
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06006872 RID: 26738 RVA: 0x00047B25 File Offset: 0x00045D25
		// (set) Token: 0x06006873 RID: 26739 RVA: 0x00047B32 File Offset: 0x00045D32
		public override int WriteTimeout
		{
			get
			{
				return this.m_Stream.WriteTimeout;
			}
			set
			{
				this.m_Stream.WriteTimeout = value;
			}
		}

		// Token: 0x04005FE3 RID: 24547
		private Stream m_Stream;
	}
}
