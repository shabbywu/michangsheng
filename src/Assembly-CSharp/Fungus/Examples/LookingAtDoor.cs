using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FAF RID: 4015
	public class LookingAtDoor : MonoBehaviour
	{
		// Token: 0x06006FE9 RID: 28649 RVA: 0x002A87F9 File Offset: 0x002A69F9
		public void ActivateNow()
		{
			base.enabled = true;
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x002A8804 File Offset: 0x002A6A04
		private void Update()
		{
			float num = this.gazeCounter;
			RaycastHit raycastHit;
			if (Physics.Raycast(this.eye.position, this.eye.forward, ref raycastHit))
			{
				if (raycastHit.collider == this.doorCol)
				{
					this.gazeCounter += Time.deltaTime;
				}
				else
				{
					this.gazeCounter = 0f;
				}
			}
			else
			{
				this.gazeCounter = 0f;
			}
			if (this.gazeCounter >= this.gazeTime && num <= this.gazeTime)
			{
				this.runBlockWhenGazed.Execute();
				this.fungusBoolHasGazed.Set<bool>(true);
			}
		}

		// Token: 0x04005C59 RID: 23641
		public Collider doorCol;

		// Token: 0x04005C5A RID: 23642
		public float gazeTime = 0.2f;

		// Token: 0x04005C5B RID: 23643
		private float gazeCounter;

		// Token: 0x04005C5C RID: 23644
		public BlockReference runBlockWhenGazed;

		// Token: 0x04005C5D RID: 23645
		public Transform eye;

		// Token: 0x04005C5E RID: 23646
		public VariableReference fungusBoolHasGazed;
	}
}
