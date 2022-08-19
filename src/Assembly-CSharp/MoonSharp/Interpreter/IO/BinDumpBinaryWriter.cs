using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoonSharp.Interpreter.IO
{
	// Token: 0x02000D03 RID: 3331
	public class BinDumpBinaryWriter : BinaryWriter
	{
		// Token: 0x06005D35 RID: 23861 RVA: 0x00262528 File Offset: 0x00260728
		public BinDumpBinaryWriter(Stream s) : base(s)
		{
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0026253C File Offset: 0x0026073C
		public BinDumpBinaryWriter(Stream s, Encoding e) : base(s, e)
		{
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00262554 File Offset: 0x00260754
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

		// Token: 0x06005D38 RID: 23864 RVA: 0x002625A0 File Offset: 0x002607A0
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

		// Token: 0x06005D39 RID: 23865 RVA: 0x002625EC File Offset: 0x002607EC
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

		// Token: 0x040053D8 RID: 21464
		private Dictionary<string, int> m_StringMap = new Dictionary<string, int>();
	}
}
