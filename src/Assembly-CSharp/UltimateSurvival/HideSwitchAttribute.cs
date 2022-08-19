using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005AA RID: 1450
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class HideSwitchAttribute : PropertyAttribute
	{
		// Token: 0x06002F40 RID: 12096 RVA: 0x001566DC File Offset: 0x001548DC
		public HideSwitchAttribute(string basedOn, bool showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnBool = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0015673C File Offset: 0x0015493C
		public HideSwitchAttribute(string basedOn, string showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnString = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0015679C File Offset: 0x0015499C
		public HideSwitchAttribute(string basedOn, int showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnInt = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x001567FC File Offset: 0x001549FC
		public HideSwitchAttribute(string basedOn, float showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnFloat = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0015685C File Offset: 0x00154A5C
		public HideSwitchAttribute(string basedOn, Vector3 showOnValue, float indentAmount = 20f)
		{
			this.m_BasedOnValue = basedOn;
			this.m_ShowOnVector3 = showOnValue;
			this.m_IndentAmount = indentAmount;
		}

		// Token: 0x04002978 RID: 10616
		public readonly string m_BasedOnValue;

		// Token: 0x04002979 RID: 10617
		public readonly float m_IndentAmount;

		// Token: 0x0400297A RID: 10618
		public readonly bool m_ShowOnBool;

		// Token: 0x0400297B RID: 10619
		public readonly string m_ShowOnString = "s";

		// Token: 0x0400297C RID: 10620
		public readonly int m_ShowOnInt = -1;

		// Token: 0x0400297D RID: 10621
		public readonly float m_ShowOnFloat = -1f;

		// Token: 0x0400297E RID: 10622
		public readonly Vector3 m_ShowOnVector3 = new Vector3(1f, 1f, 1f);
	}
}
