using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001074 RID: 4212
	public struct TablePair
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060065A0 RID: 26016 RVA: 0x00045F96 File Offset: 0x00044196
		// (set) Token: 0x060065A1 RID: 26017 RVA: 0x00045F9E File Offset: 0x0004419E
		public DynValue Key
		{
			get
			{
				return this.key;
			}
			private set
			{
				this.Key = this.key;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x00045FAC File Offset: 0x000441AC
		// (set) Token: 0x060065A3 RID: 26019 RVA: 0x00045FB4 File Offset: 0x000441B4
		public DynValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.key.IsNotNil())
				{
					this.Value = value;
				}
			}
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x00045FCA File Offset: 0x000441CA
		public TablePair(DynValue key, DynValue val)
		{
			this.key = key;
			this.value = val;
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x00045FDA File Offset: 0x000441DA
		public static TablePair Nil
		{
			get
			{
				return TablePair.s_NilNode;
			}
		}

		// Token: 0x04005E66 RID: 24166
		private static TablePair s_NilNode = new TablePair(DynValue.Nil, DynValue.Nil);

		// Token: 0x04005E67 RID: 24167
		private DynValue key;

		// Token: 0x04005E68 RID: 24168
		private DynValue value;
	}
}
