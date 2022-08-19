using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class FlowmapAnimator : MonoBehaviour
{
	// Token: 0x06000AF3 RID: 2803 RVA: 0x0004206E File Offset: 0x0004026E
	private void Reset()
	{
		this.flowSpeed = 0.25f;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0004207C File Offset: 0x0004027C
	private void Start()
	{
		this.currentMaterial = base.GetComponent<Renderer>().material;
		this.cycle = 6f;
		this.halfCycle = this.cycle * 0.5f;
		this.flowMapOffset0 = 0f;
		this.flowMapOffset1 = this.halfCycle;
		this.currentMaterial.SetFloat("halfCycle", this.halfCycle);
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x000420E4 File Offset: 0x000402E4
	private void Update()
	{
		this.flowMapOffset0 += this.flowSpeed * Time.deltaTime;
		this.flowMapOffset1 += this.flowSpeed * Time.deltaTime;
		while (this.flowMapOffset0 >= this.cycle)
		{
			this.flowMapOffset0 -= this.cycle;
		}
		while (this.flowMapOffset1 >= this.cycle)
		{
			this.flowMapOffset1 -= this.cycle;
		}
		this.currentMaterial.SetFloat("flowMapOffset0", this.flowMapOffset0);
		this.currentMaterial.SetFloat("flowMapOffset1", this.flowMapOffset1);
	}

	// Token: 0x04000704 RID: 1796
	public float flowSpeed;

	// Token: 0x04000705 RID: 1797
	private Material currentMaterial;

	// Token: 0x04000706 RID: 1798
	private float cycle;

	// Token: 0x04000707 RID: 1799
	private float halfCycle;

	// Token: 0x04000708 RID: 1800
	private float flowMapOffset0;

	// Token: 0x04000709 RID: 1801
	private float flowMapOffset1;

	// Token: 0x0400070A RID: 1802
	private bool hasTide;
}
