using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E36 RID: 3638
	public class MaterialPropertyBlockExample : MonoBehaviour
	{
		// Token: 0x06005786 RID: 22406 RVA: 0x0003E92A File Offset: 0x0003CB2A
		private void Start()
		{
			this.mpb = new MaterialPropertyBlock();
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x002452CC File Offset: 0x002434CC
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

		// Token: 0x04005776 RID: 22390
		public float timeInterval = 1f;

		// Token: 0x04005777 RID: 22391
		public Gradient randomColors = new Gradient();

		// Token: 0x04005778 RID: 22392
		public string colorPropertyName = "_FillColor";

		// Token: 0x04005779 RID: 22393
		private MaterialPropertyBlock mpb;

		// Token: 0x0400577A RID: 22394
		private float timeToNextColor;
	}
}
