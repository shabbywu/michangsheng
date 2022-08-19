using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000B26 RID: 2854
	public class ETFXRotation : MonoBehaviour
	{
		// Token: 0x06004F96 RID: 20374 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00219EB0 File Offset: 0x002180B0
		private void Update()
		{
			if (this.rotateSpace == ETFXRotation.spaceEnum.Local)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime);
			}
			if (this.rotateSpace == ETFXRotation.spaceEnum.World)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime, 0);
			}
		}

		// Token: 0x04004E93 RID: 20115
		[Header("Rotate axises by degrees per second")]
		public Vector3 rotateVector = Vector3.zero;

		// Token: 0x04004E94 RID: 20116
		public ETFXRotation.spaceEnum rotateSpace;

		// Token: 0x020015E9 RID: 5609
		public enum spaceEnum
		{
			// Token: 0x040070CC RID: 28876
			Local,
			// Token: 0x040070CD RID: 28877
			World
		}
	}
}
