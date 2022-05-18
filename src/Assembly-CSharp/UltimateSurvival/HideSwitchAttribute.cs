using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000866 RID: 2150
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class HideSwitchAttribute : PropertyAttribute
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x001A0D4C File Offset: 0x0019EF4C
		public HideSwitchAttribute(string basedOn, bool showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnBool = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x001A0DAC File Offset: 0x0019EFAC
		public HideSwitchAttribute(string basedOn, string showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnString = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x001A0E0C File Offset: 0x0019F00C
		public HideSwitchAttribute(string basedOn, int showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnInt = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x001A0E6C File Offset: 0x0019F06C
		public HideSwitchAttribute(string basedOn, float showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnFloat = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x001A0ECC File Offset: 0x0019F0CC
		public HideSwitchAttribute(string basedOn, Vector3 showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnVector3 = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x040031F9 RID: 12793
		public readonly string m_BasedOnValue;

		// Token: 0x040031FA RID: 12794
		public readonly float m_IndentAmount;

		// Token: 0x040031FB RID: 12795
		public readonly bool m_ShowOnBool;

		// Token: 0x040031FC RID: 12796
		public readonly string m_ShowOnString = "s";

		// Token: 0x040031FD RID: 12797
		public readonly int m_ShowOnInt = -1;

		// Token: 0x040031FE RID: 12798
		public readonly float m_ShowOnFloat = -1f;

		// Token: 0x040031FF RID: 12799
		public readonly Vector3 m_ShowOnVector3 = new Vector3(1f, 1f, 1f);
	}
}
