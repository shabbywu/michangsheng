using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x02000D8B RID: 3467
	internal abstract class StreamFileUserDataBase : FileUserDataBase
	{
		// Token: 0x06006296 RID: 25238 RVA: 0x0027993B File Offset: 0x00277B3B
		protected void Initialize(Stream stream, StreamReader reader, StreamWriter writer)
		{
			this.m_Stream = stream;
			this.m_Reader = reader;
			this.m_Writer = writer;
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x00279952 File Offset: 0x00277B52
		private void CheckFileIsNotClosed()
		{
			if (this.m_Closed)
			{
				throw new ScriptRuntimeException("attempt to use a closed file");
			}
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x00279967 File Offset: 0x00277B67
		protected override bool Eof()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader != null && this.m_Reader.EndOfStream;
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x00279984 File Offset: 0x00277B84
		protected override string ReadLine()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadLine();
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00279997 File Offset: 0x00277B97
		protected override string ReadToEnd()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadToEnd();
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x002799AC File Offset: 0x00277BAC
		protected override string ReadBuffer(int p)
		{
			this.CheckFileIsNotClosed();
			char[] array = new char[p];
			int length = this.m_Reader.ReadBlock(array, 0, p);
			return new string(array, 0, length);
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x002799DD File Offset: 0x00277BDD
		protected override char Peek()
		{
			this.CheckFileIsNotClosed();
			return (char)this.m_Reader.Peek();
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x002799F1 File Offset: 0x00277BF1
		protected override void Write(string value)
		{
			this.CheckFileIsNotClosed();
			this.m_Writer.Write(value);
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x00279A08 File Offset: 0x00277C08
		protected override string Close()
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.Dispose();
			}
			if (this.m_Reader != null)
			{
				this.m_Reader.Dispose();
			}
			this.m_Stream.Dispose();
			this.m_Closed = true;
			return null;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x00279A54 File Offset: 0x00277C54
		public override bool flush()
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.Flush();
			}
			return true;
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x00279A70 File Offset: 0x00277C70
		public override long seek(string whence, long offset)
		{
			this.CheckFileIsNotClosed();
			if (whence != null)
			{
				if (whence == "set")
				{
					this.m_Stream.Seek(offset, SeekOrigin.Begin);
				}
				else if (whence == "cur")
				{
					this.m_Stream.Seek(offset, SeekOrigin.Current);
				}
				else
				{
					if (!(whence == "end"))
					{
						throw ScriptRuntimeException.BadArgument(0, "seek", "invalid option '" + whence + "'");
					}
					this.m_Stream.Seek(offset, SeekOrigin.End);
				}
			}
			return this.m_Stream.Position;
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x00279B04 File Offset: 0x00277D04
		public override bool setvbuf(string mode)
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.AutoFlush = (mode == "no" || mode == "line");
			}
			return true;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x00279B3B File Offset: 0x00277D3B
		protected internal override bool isopen()
		{
			return !this.m_Closed;
		}

		// Token: 0x0400559F RID: 21919
		protected Stream m_Stream;

		// Token: 0x040055A0 RID: 21920
		protected StreamReader m_Reader;

		// Token: 0x040055A1 RID: 21921
		protected StreamWriter m_Writer;

		// Token: 0x040055A2 RID: 21922
		protected bool m_Closed;
	}
}
