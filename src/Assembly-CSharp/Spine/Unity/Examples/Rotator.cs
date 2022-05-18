using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E4D RID: 3661
	public class Rotator : MonoBehaviour
	{
		// Token: 0x060057E6 RID: 22502 RVA: 0x0003ED76 File Offset: 0x0003CF76
		private void Update()
		{
			base.transform.Rotate(this.direction * (this.speed * Time.deltaTime * 100f));
		}

		// Token: 0x040057E4 RID: 22500
		public Vector3 direction = new Vector3(0f, 0f, 1f);

		// Token: 0x040057E5 RID: 22501
		public float speed = 1f;
	}
}
