using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A80 RID: 2688
	public class SkillEffect : MonoBehaviour
	{
		// Token: 0x06004B78 RID: 19320 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0020064E File Offset: 0x001FE84E
		public void createSkillEffect()
		{
			Object.Instantiate<GameObject>(this.Prefabs[this.Prefab], this.FirePoint.transform.position, this.FirePoint.transform.rotation);
		}

		// Token: 0x04004A87 RID: 19079
		public GameObject FirePoint;

		// Token: 0x04004A88 RID: 19080
		public Camera Cam;

		// Token: 0x04004A89 RID: 19081
		public float MaxLength;

		// Token: 0x04004A8A RID: 19082
		public GameObject[] Prefabs;

		// Token: 0x04004A8B RID: 19083
		private Ray RayMouse;

		// Token: 0x04004A8C RID: 19084
		private Vector3 direction;

		// Token: 0x04004A8D RID: 19085
		private Quaternion rotation;

		// Token: 0x04004A8E RID: 19086
		[Header("GUI")]
		private float windowDpi;

		// Token: 0x04004A8F RID: 19087
		private int Prefab;

		// Token: 0x04004A90 RID: 19088
		private GameObject Instance;

		// Token: 0x04004A91 RID: 19089
		private float hSliderValue = 0.1f;

		// Token: 0x04004A92 RID: 19090
		private float fireCountdown;

		// Token: 0x04004A93 RID: 19091
		private float buttonSaver;
	}
}
