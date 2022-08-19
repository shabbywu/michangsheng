using System;
using System.Collections.Generic;
using System.IO;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA3 RID: 3235
	public class SymbolRef
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06005A76 RID: 23158 RVA: 0x002583AF File Offset: 0x002565AF
		public SymbolRefType Type
		{
			get
			{
				return this.i_Type;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x002583B7 File Offset: 0x002565B7
		public int Index
		{
			get
			{
				return this.i_Index;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06005A78 RID: 23160 RVA: 0x002583BF File Offset: 0x002565BF
		public string Name
		{
			get
			{
				return this.i_Name;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x002583C7 File Offset: 0x002565C7
		public SymbolRef Environment
		{
			get
			{
				return this.i_Env;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x002583CF File Offset: 0x002565CF
		public static SymbolRef DefaultEnv
		{
			get
			{
				return SymbolRef.s_DefaultEnv;
			}
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x002583D6 File Offset: 0x002565D6
		public static SymbolRef Global(string name, SymbolRef envSymbol)
		{
			return new SymbolRef
			{
				i_Index = -1,
				i_Type = SymbolRefType.Global,
				i_Env = envSymbol,
				i_Name = name
			};
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x002583F9 File Offset: 0x002565F9
		internal static SymbolRef Local(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Local,
				i_Name = name
			};
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x00258415 File Offset: 0x00256615
		internal static SymbolRef Upvalue(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Upvalue,
				i_Name = name
			};
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x00258434 File Offset: 0x00256634
		public override string ToString()
		{
			if (this.i_Type == SymbolRefType.DefaultEnv)
			{
				return "(default _ENV)";
			}
			if (this.i_Type == SymbolRefType.Global)
			{
				return string.Format("{2} : {0} / {1}", this.i_Type, this.i_Env, this.i_Name);
			}
			return string.Format("{2} : {0}[{1}]", this.i_Type, this.i_Index, this.i_Name);
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x002584A1 File Offset: 0x002566A1
		internal void WriteBinary(BinaryWriter bw)
		{
			bw.Write((byte)this.i_Type);
			bw.Write(this.i_Index);
			bw.Write(this.i_Name);
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x002584C8 File Offset: 0x002566C8
		internal static SymbolRef ReadBinary(BinaryReader br)
		{
			return new SymbolRef
			{
				i_Type = (SymbolRefType)br.ReadByte(),
				i_Index = br.ReadInt32(),
				i_Name = br.ReadString()
			};
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x002584F3 File Offset: 0x002566F3
		internal void WriteBinaryEnv(BinaryWriter bw, Dictionary<SymbolRef, int> symbolMap)
		{
			if (this.i_Env != null)
			{
				bw.Write(symbolMap[this.i_Env]);
				return;
			}
			bw.Write(-1);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x00258518 File Offset: 0x00256718
		internal void ReadBinaryEnv(BinaryReader br, SymbolRef[] symbolRefs)
		{
			int num = br.ReadInt32();
			if (num >= 0)
			{
				this.i_Env = symbolRefs[num];
			}
		}

		// Token: 0x04005280 RID: 21120
		private static SymbolRef s_DefaultEnv = new SymbolRef
		{
			i_Type = SymbolRefType.DefaultEnv
		};

		// Token: 0x04005281 RID: 21121
		internal SymbolRefType i_Type;

		// Token: 0x04005282 RID: 21122
		internal SymbolRef i_Env;

		// Token: 0x04005283 RID: 21123
		internal int i_Index;

		// Token: 0x04005284 RID: 21124
		internal string i_Name;
	}
}
