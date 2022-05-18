using System;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000E9C RID: 3740
	[Serializable]
	public class EIU_AxisBase
	{
		// Token: 0x0400590F RID: 22799
		public string axisName;

		// Token: 0x04005910 RID: 22800
		public KeyCode positiveKey;

		// Token: 0x04005911 RID: 22801
		public KeyCode negativeKey;

		// Token: 0x04005912 RID: 22802
		[HideInInspector]
		public bool positive;

		// Token: 0x04005913 RID: 22803
		[HideInInspector]
		public bool negative;

		// Token: 0x04005914 RID: 22804
		[HideInInspector]
		public float axis;

		// Token: 0x04005915 RID: 22805
		[HideInInspector]
		public float targetAxis;

		// Token: 0x04005916 RID: 22806
		public float sensitivity = 3f;

		// Token: 0x04005917 RID: 22807
		public string pKeyDescription;

		// Token: 0x04005918 RID: 22808
		public string nKeyDescription;

		// Token: 0x04005919 RID: 22809
		[HideInInspector]
		public EIU_AxisButton pUIButton;

		// Token: 0x0400591A RID: 22810
		[HideInInspector]
		public EIU_AxisButton nUIButton;
	}
}
