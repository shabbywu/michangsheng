using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x020010E2 RID: 4322
	public class BinDumpBinaryWriter : BinaryWriter
	{
		// Token: 0x06006853 RID: 26707 RVA: 0x000479A5 File Offset: 0x00045BA5
		public BinDumpBinaryWriter(Stream s) : base(s)
		{
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x000479B9 File Offset: 0x00045BB9
		public BinDumpBinaryWriter(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0028B3F4 File Offset: 0x002895F4
		public override void Write(uint value)
		{
			byte b = (byte)value;
			if ((uint)b == value && b != 127 && b != 126)
			{
				base.Write(b);
				return;
			}
			ushort num = (ushort)value;
			if ((uint)num == value)
			{
				base.Write(127);
				base.Write(num);
				return;
			}
			base.Write(126);
			base.Write(value);
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x0028B440 File Offset: 0x00289640
		public override void Write(int value)
		{
			sbyte b = (sbyte)value;
			if ((int)b == value && b != 127 && b != 126)
			{
				base.Write(b);
				return;
			}
			short num = (short)value;
			if ((int)num == value)
			{
				base.Write(sbyte.MaxValue);
				base.Write(num);
				return;
			}
			base.Write(126);
			base.Write(value);
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0028B48C File Offset: 0x0028968C
		public override void Write(string value)
		{
			int count;
			if (this.m_StringMap.TryGetValue(value, out count))
			{
				this.Write(this.m_StringMap[value]);
				return;
			}
			count = this.m_StringMap.Count;
			this.m_StringMap[value] = count;
			this.Write(count);
			base.Write(value);
		}

		// Token: 0x04005FE2 RID: 24546
		private Dictionary<string, int> m_StringMap = new Dictionary<string, int>();
	}
}
