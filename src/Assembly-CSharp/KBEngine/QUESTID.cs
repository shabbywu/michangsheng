using System;

namespace KBEngine
{
	// Token: 0x02000BB6 RID: 2998
	public struct QUESTID
	{
		// Token: 0x060053A8 RID: 21416 RVA: 0x00233DA8 File Offset: 0x00231FA8
		private QUESTID(int value)
		{
			this.value = value;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x00233DB1 File Offset: 0x00231FB1
		public static implicit operator int(QUESTID value)
		{
			return value.value;
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x00233DB9 File Offset: 0x00231FB9
		public static implicit operator QUESTID(int value)
		{
			return new QUESTID(value);
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005042 RID: 20546
		private int value;
	}
}
