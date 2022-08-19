using System;
using System.IO;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x02000D04 RID: 3332
	public class UndisposableStream : Stream
	{
		// Token: 0x06005D3A RID: 23866 RVA: 0x00262643 File Offset: 0x00260843
		public UndisposableStream(Stream stream)
		{
			this.m_Stream = stream;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x00004095 File Offset: 0x00002295
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x00004095 File Offset: 0x00002295
		public override void Close()
		{
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x00262652 File Offset: 0x00260852
		public override bool CanRead
		{
			get
			{
				return this.m_Stream.CanRead;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06005D3E RID: 23870 RVA: 0x0026265F File Offset: 0x0026085F
		public override bool CanSeek
		{
			get
			{
				return this.m_Stream.CanSeek;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x0026266C File Offset: 0x0026086C
		public override bool CanWrite
		{
			get
			{
				return this.m_Stream.CanWrite;
			}
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x00262679 File Offset: 0x00260879
		public override void Flush()
		{
			this.m_Stream.Flush();
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x00262686 File Offset: 0x00260886
		public override long Length
		{
			get
			{
				return this.m_Stream.Length;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06005D42 RID: 23874 RVA: 0x00262693 File Offset: 0x00260893
		// (set) Token: 0x06005D43 RID: 23875 RVA: 0x002626A0 File Offset: 0x002608A0
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

		// Token: 0x06005D44 RID: 23876 RVA: 0x002626AE File Offset: 0x002608AE
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_Stream.Read(buffer, offset, count);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x002626BE File Offset: 0x002608BE
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_Stream.Seek(offset, origin);
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x002626CD File Offset: 0x002608CD
		public override void SetLength(long value)
		{
			this.m_Stream.SetLength(value);
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x002626DB File Offset: 0x002608DB
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_Stream.Write(buffer, offset, count);
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x002626EB File Offset: 0x002608EB
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x002626FF File Offset: 0x002608FF
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_Stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x00262713 File Offset: 0x00260913
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_Stream.EndWrite(asyncResult);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00262721 File Offset: 0x00260921
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_Stream.EndRead(asyncResult);
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x0026272F File Offset: 0x0026092F
		public override bool CanTimeout
		{
			get
			{
				return this.m_Stream.CanTimeout;
			}
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x0026273C File Offset: 0x0026093C
		public override bool Equals(object obj)
		{
			return this.m_Stream.Equals(obj);
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x0026274A File Offset: 0x0026094A
		public override int GetHashCode()
		{
			return this.m_Stream.GetHashCode();
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00262757 File Offset: 0x00260957
		public override int ReadByte()
		{
			return this.m_Stream.ReadByte();
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06005D50 RID: 23888 RVA: 0x00262764 File Offset: 0x00260964
		// (set) Token: 0x06005D51 RID: 23889 RVA: 0x00262771 File Offset: 0x00260971
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

		// Token: 0x06005D52 RID: 23890 RVA: 0x0026277F File Offset: 0x0026097F
		public override string ToString()
		{
			return this.m_Stream.ToString();
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0026278C File Offset: 0x0026098C
		public override void WriteByte(byte value)
		{
			this.m_Stream.WriteByte(value);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06005D54 RID: 23892 RVA: 0x0026279A File Offset: 0x0026099A
		// (set) Token: 0x06005D55 RID: 23893 RVA: 0x002627A7 File Offset: 0x002609A7
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

		// Token: 0x040053D9 RID: 21465
		private Stream m_Stream;
	}
}
