using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02001467 RID: 5223
	public class LookingAtDoor : MonoBehaviour
	{
		// Token: 0x06007DE3 RID: 32227 RVA: 0x0005519E File Offset: 0x0005339E
		public void ActivateNow()
		{
			base.enabled = true;
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x002C7FFC File Offset: 0x002C61FC
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

		// Token: 0x04006B51 RID: 27473
		public Collider doorCol;

		// Token: 0x04006B52 RID: 27474
		public float gazeTime = 0.2f;

		// Token: 0x04006B53 RID: 27475
		private float gazeCounter;

		// Token: 0x04006B54 RID: 27476
		public BlockReference runBlockWhenGazed;

		// Token: 0x04006B55 RID: 27477
		public Transform eye;

		// Token: 0x04006B56 RID: 27478
		public VariableReference fungusBoolHasGazed;
	}
}
