using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF4 RID: 2804
	public class Rotator : MonoBehaviour
	{
		// Token: 0x06004E45 RID: 20037 RVA: 0x00215FDD File Offset: 0x002141DD
		private void Update()
		{
			base.transform.Rotate(this.direction * (this.speed * Time.deltaTime * 100f));
		}

		// Token: 0x04004DB8 RID: 19896
		public Vector3 direction = new Vector3(0f, 0f, 1f);

		// Token: 0x04004DB9 RID: 19897
		public float speed = 1f;
	}
}
