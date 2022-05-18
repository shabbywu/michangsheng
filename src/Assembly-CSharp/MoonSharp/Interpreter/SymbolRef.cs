using System;
using System.Collections.Generic;
using System.IO;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001070 RID: 4208
	public class SymbolRef
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x00045BC4 File Offset: 0x00043DC4
		public SymbolRefType Type
		{
			get
			{
				return this.i_Type;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06006561 RID: 25953 RVA: 0x00045BCC File Offset: 0x00043DCC
		public int Index
		{
			get
			{
				return this.i_Index;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x00045BD4 File Offset: 0x00043DD4
		public string Name
		{
			get
			{
				return this.i_Name;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x00045BDC File Offset: 0x00043DDC
		public SymbolRef Environment
		{
			get
			{
				return this.i_Env;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x00045BE4 File Offset: 0x00043DE4
		public static SymbolRef DefaultEnv
		{
			get
			{
				return SymbolRef.s_DefaultEnv;
			}
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x00045BEB File Offset: 0x00043DEB
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

		// Token: 0x06006566 RID: 25958 RVA: 0x00045C0E File Offset: 0x00043E0E
		internal static SymbolRef Local(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Local,
				i_Name = name
			};
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x00045C2A File Offset: 0x00043E2A
		internal static SymbolRef Upvalue(string name, int index)
		{
			return new SymbolRef
			{
				i_Index = index,
				i_Type = SymbolRefType.Upvalue,
				i_Name = name
			};
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x00282E98 File Offset: 0x00281098
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

		// Token: 0x06006569 RID: 25961 RVA: 0x00045C46 File Offset: 0x00043E46
		internal void WriteBinary(BinaryWriter bw)
		{
			bw.Write((byte)this.i_Type);
			bw.Write(this.i_Index);
			bw.Write(this.i_Name);
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x00045C6D File Offset: 0x00043E6D
		internal static SymbolRef ReadBinary(BinaryReader br)
		{
			return new SymbolRef
			{
				i_Type = (SymbolRefType)br.ReadByte(),
				i_Index = br.ReadInt32(),
				i_Name = br.ReadString()
			};
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x00045C98 File Offset: 0x00043E98
		internal void WriteBinaryEnv(BinaryWriter bw, Dictionary<SymbolRef, int> symbolMap)
		{
			if (this.i_Env != null)
			{
				bw.Write(symbolMap[this.i_Env]);
				return;
			}
			bw.Write(-1);
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x00282F08 File Offset: 0x00281108
		internal void ReadBinaryEnv(BinaryReader br, SymbolRef[] symbolRefs)
		{
			int num = br.ReadInt32();
			if (num >= 0)
			{
				this.i_Env = symbolRefs[num];
			}
		}

		// Token: 0x04005E4F RID: 24143
		private static SymbolRef s_DefaultEnv = new SymbolRef
		{
			i_Type = SymbolRefType.DefaultEnv
		};

		// Token: 0x04005E50 RID: 24144
		internal SymbolRefType i_Type;

		// Token: 0x04005E51 RID: 24145
		internal SymbolRef i_Env;

		// Token: 0x04005E52 RID: 24146
		internal int i_Index;

		// Token: 0x04005E53 RID: 24147
		internal string i_Name;
	}
}
