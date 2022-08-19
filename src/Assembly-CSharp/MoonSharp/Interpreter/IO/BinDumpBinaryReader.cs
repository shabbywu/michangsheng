using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x02000D02 RID: 3330
	public class BinDumpBinaryReader : BinaryReader
	{
		// Token: 0x06005D30 RID: 23856 RVA: 0x00262443 File Offset: 0x00260643
		public BinDumpBinaryReader(Stream s) : base(s)
		{
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00262457 File Offset: 0x00260657
		public BinDumpBinaryReader(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0026246C File Offset: 0x0026066C
		public override int ReadInt32()
		{
			sbyte b = base.ReadSByte();
			if (b == 127)
			{
				return (int)base.ReadInt16();
			}
			if (b == 126)
			{
				return base.ReadInt32();
			}
			return (int)b;
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x0026249C File Offset: 0x0026069C
		public override uint ReadUInt32()
		{
			byte b = base.ReadByte();
			if (b == 127)
			{
				return (uint)base.ReadUInt16();
			}
			if (b == 126)
			{
				return base.ReadUInt32();
			}
			return (uint)b;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x002624CC File Offset: 0x002606CC
		public override string ReadString()
		{
			int num = this.ReadInt32();
			if (num < this.m_Strings.Count)
			{
				return this.m_Strings[num];
			}
			if (num == this.m_Strings.Count)
			{
				string text = base.ReadString();
				this.m_Strings.Add(text);
				return text;
			}
			throw new IOException("string map failure");
		}

		// Token: 0x040053D7 RID: 21463
		private List<string> m_Strings = new List<string>();
	}
}
