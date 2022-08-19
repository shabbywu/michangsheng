using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADF RID: 2783
	public class SpineboyBeginnerInput : MonoBehaviour
	{
		// Token: 0x06004DE4 RID: 19940 RVA: 0x00214949 File Offset: 0x00212B49
		private void OnValidate()
		{
			if (this.model == null)
			{
				this.model = base.GetComponent<SpineboyBeginnerModel>();
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x00214968 File Offset: 0x00212B68
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

		// Token: 0x04004D38 RID: 19768
		public string horizontalAxis = "Horizontal";

		// Token: 0x04004D39 RID: 19769
		public string attackButton = "Fire1";

		// Token: 0x04004D3A RID: 19770
		public string jumpButton = "Jump";

		// Token: 0x04004D3B RID: 19771
		public SpineboyBeginnerModel model;
	}
}
