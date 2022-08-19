using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CA6 RID: 3238
	public struct TablePair
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x00258E8A File Offset: 0x0025708A
		// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x00258E92 File Offset: 0x00257092
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

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x00258EA0 File Offset: 0x002570A0
		// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x00258EA8 File Offset: 0x002570A8
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

		// Token: 0x06005AB5 RID: 23221 RVA: 0x00258EBE File Offset: 0x002570BE
		public TablePair(DynValue key, DynValue val)
		{
			this.key = key;
			this.value = val;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x00258ECE File Offset: 0x002570CE
		public static TablePair Nil
		{
			get
			{
				return TablePair.s_NilNode;
			}
		}

		// Token: 0x04005293 RID: 21139
		private static TablePair s_NilNode = new TablePair(DynValue.Nil, DynValue.Nil);

		// Token: 0x04005294 RID: 21140
		private DynValue key;

		// Token: 0x04005295 RID: 21141
		private DynValue value;
	}
}
