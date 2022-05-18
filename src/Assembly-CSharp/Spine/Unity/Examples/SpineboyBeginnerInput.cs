using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E2B RID: 3627
	public class SpineboyBeginnerInput : MonoBehaviour
	{
		// Token: 0x0600575A RID: 22362 RVA: 0x0003E6B5 File Offset: 0x0003C8B5
		private void OnValidate()
		{
			if (this.model == null)
			{
				this.model = base.GetComponent<SpineboyBeginnerModel>();
			}
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00244B28 File Offset: 0x00242D28
		private void Update()
		{
			if (this.model == null)
			{
				return;
			}
			float axisRaw = Input.GetAxisRaw(this.horizontalAxis);
			this.model.TryMove(axisRaw);
			if (Input.GetButton(this.attackButton))
			{
				this.model.TryShoot();
			}
			if (Input.GetButtonDown(this.jumpButton))
			{
				this.model.TryJump();
			}
		}

		// Token: 0x0400573C RID: 22332
		public string horizontalAxis = "Horizontal";

		// Token: 0x0400573D RID: 22333
		public string attackButton = "Fire1";

		// Token: 0x0400573E RID: 22334
		public string jumpButton = "Jump";

		// Token: 0x0400573F RID: 22335
		public SpineboyBeginnerModel model;
	}
}
