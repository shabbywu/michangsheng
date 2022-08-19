using System;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000B28 RID: 2856
	[Serializable]
	public class EIU_AxisBase
	{
		// Token: 0x04004E98 RID: 20120
		public string axisName;

		// Token: 0x04004E99 RID: 20121
		public KeyCode positiveKey;

		// Token: 0x04004E9A RID: 20122
		public KeyCode negativeKey;

		// Token: 0x04004E9B RID: 20123
		[HideInInspector]
		public bool positive;

		// Token: 0x04004E9C RID: 20124
		[HideInInspector]
		public bool negative;

		// Token: 0x04004E9D RID: 20125
		[HideInInspector]
		public float axis;

		// Token: 0x04004E9E RID: 20126
		[HideInInspector]
		public float targetAxis;

		// Token: 0x04004E9F RID: 20127
		public float sensitivity = 3f;

		// Token: 0x04004EA0 RID: 20128
		public string pKeyDescription;

		// Token: 0x04004EA1 RID: 20129
		public string nKeyDescription;

		// Token: 0x04004EA2 RID: 20130
		[HideInInspector]
		public EIU_AxisButton pUIButton;

		// Token: 0x04004EA3 RID: 20131
		[HideInInspector]
		public EIU_AxisButton nUIButton;
	}
}
