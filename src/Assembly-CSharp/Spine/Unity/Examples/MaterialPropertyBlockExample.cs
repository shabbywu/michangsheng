using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE8 RID: 2792
	public class MaterialPropertyBlockExample : MonoBehaviour
	{
		// Token: 0x06004E0A RID: 19978 RVA: 0x002151F0 File Offset: 0x002133F0
		private void Start()
		{
			this.mpb = new MaterialPropertyBlock();
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x00215200 File Offset: 0x00213400
		private void Update()
		{
			if (this.timeToNextColor <= 0f)
			{
				this.timeToNextColor = this.timeInterval;
				Color color = this.randomColors.Evaluate(Random.value);
				this.mpb.SetColor(this.colorPropertyName, color);
				base.GetComponent<MeshRenderer>().SetPropertyBlock(this.mpb);
			}
			this.timeToNextColor -= Time.deltaTime;
		}

		// Token: 0x04004D6A RID: 19818
		public float timeInterval = 1f;

		// Token: 0x04004D6B RID: 19819
		public Gradient randomColors = new Gradient();

		// Token: 0x04004D6C RID: 19820
		public string colorPropertyName = "_FillColor";

		// Token: 0x04004D6D RID: 19821
		private MaterialPropertyBlock mpb;

		// Token: 0x04004D6E RID: 19822
		private float timeToNextColor;
	}
}
