using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000E99 RID: 3737
	public class ETFXRotation : MonoBehaviour
	{
		// Token: 0x060059B5 RID: 22965 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00249C78 File Offset: 0x00247E78
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

		// Token: 0x04005907 RID: 22791
		[Header("Rotate axises by degrees per second")]
		public Vector3 rotateVector = Vector3.zero;

		// Token: 0x04005908 RID: 22792
		public ETFXRotation.spaceEnum rotateSpace;

		// Token: 0x02000E9A RID: 3738
		public enum spaceEnum
		{
			// Token: 0x0400590A RID: 22794
			Local,
			// Token: 0x0400590B RID: 22795
			World
		}
	}
}
