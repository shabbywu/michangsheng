using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020011AE RID: 4526
	internal abstract class StreamFileUserDataBase : FileUserDataBase
	{
		// Token: 0x06006EC7 RID: 28359 RVA: 0x0004B58B File Offset: 0x0004978B
		protected void Initialize(Stream stream, StreamReader reader, StreamWriter writer)
		{
			this.m_Stream = stream;
			this.m_Reader = reader;
			this.m_Writer = writer;
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x0004B5A2 File Offset: 0x000497A2
		private void CheckFileIsNotClosed()
		{
			if (this.m_Closed)
			{
				throw new ScriptRuntimeException("attempt to use a closed file");
			}
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x0004B5B7 File Offset: 0x000497B7
		protected override bool Eof()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader != null && this.m_Reader.EndOfStream;
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x0004B5D4 File Offset: 0x000497D4
		protected override string ReadLine()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadLine();
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x0004B5E7 File Offset: 0x000497E7
		protected override string ReadToEnd()
		{
			this.CheckFileIsNotClosed();
			return this.m_Reader.ReadToEnd();
		}

		// Token: 0x06006ECC RID: 28364 RVA: 0x0029F8AC File Offset: 0x0029DAAC
		protected override string ReadBuffer(int p)
		{
			this.CheckFileIsNotClosed();
			char[] array = new char[p];
			int length = this.m_Reader.ReadBlock(array, 0, p);
			return new string(array, 0, length);
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x0004B5FA File Offset: 0x000497FA
		protected override char Peek()
		{
			this.CheckFileIsNotClosed();
			return (char)this.m_Reader.Peek();
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x0004B60E File Offset: 0x0004980E
		protected override void Write(string value)
		{
			this.CheckFileIsNotClosed();
			this.m_Writer.Write(value);
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x0029F8E0 File Offset: 0x0029DAE0
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

		// Token: 0x06006ED0 RID: 28368 RVA: 0x0004B622 File Offset: 0x00049822
		public override bool flush()
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.Flush();
			}
			return true;
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x0029F92C File Offset: 0x0029DB2C
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

		// Token: 0x06006ED2 RID: 28370 RVA: 0x0004B63E File Offset: 0x0004983E
		public override bool setvbuf(string mode)
		{
			this.CheckFileIsNotClosed();
			if (this.m_Writer != null)
			{
				this.m_Writer.AutoFlush = (mode == "no" || mode == "line");
			}
			return true;
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x0004B675 File Offset: 0x00049875
		protected internal override bool isopen()
		{
			return !this.m_Closed;
		}

		// Token: 0x04006276 RID: 25206
		protected Stream m_Stream;

		// Token: 0x04006277 RID: 25207
		protected StreamReader m_Reader;

		// Token: 0x04006278 RID: 25208
		protected StreamWriter m_Writer;

		// Token: 0x04006279 RID: 25209
		protected bool m_Closed;
	}
}
