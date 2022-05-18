using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x020010E1 RID: 4321
	public class BinDumpBinaryReader : BinaryReader
	{
		// Token: 0x0600684E RID: 26702 RVA: 0x0004797C File Offset: 0x00045B7C
		public BinDumpBinaryReader(Stream s) : base(s)
		{
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x00047990 File Offset: 0x00045B90
		public BinDumpBinaryReader(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x0028B338 File Offset: 0x00289538
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

		// Token: 0x06006851 RID: 26705 RVA: 0x0028B368 File Offset: 0x00289568
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

		// Token: 0x06006852 RID: 26706 RVA: 0x0028B398 File Offset: 0x00289598
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

		// Token: 0x04005FE1 RID: 24545
		private List<string> m_Strings = new List<string>();
	}
}
