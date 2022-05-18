using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DB2 RID: 3506
	public class SkillEffect : MonoBehaviour
	{
		// Token: 0x06005498 RID: 21656 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0003C8BB File Offset: 0x0003AABB
		public void createSkillEffect()
		{
			Object.Instantiate<GameObject>(this.Prefabs[this.Prefab], this.FirePoint.transform.position, this.FirePoint.transform.rotation);
		}

		// Token: 0x04005446 RID: 21574
		public GameObject FirePoint;

		// Token: 0x04005447 RID: 21575
		public Camera Cam;

		// Token: 0x04005448 RID: 21576
		public float MaxLength;

		// Token: 0x04005449 RID: 21577
		public GameObject[] Prefabs;

		// Token: 0x0400544A RID: 21578
		private Ray RayMouse;

		// Token: 0x0400544B RID: 21579
		private Vector3 direction;

		// Token: 0x0400544C RID: 21580
		private Quaternion rotation;

		// Token: 0x0400544D RID: 21581
		[Header("GUI")]
		private float windowDpi;

		// Token: 0x0400544E RID: 21582
		private int Prefab;

		// Token: 0x0400544F RID: 21583
		private GameObject Instance;

		// Token: 0x04005450 RID: 21584
		private float hSliderValue = 0.1f;

		// Token: 0x04005451 RID: 21585
		private float fireCountdown;

		// Token: 0x04005452 RID: 21586
		private float buttonSaver;
	}
}
