using System;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class FlowmapAnimator : MonoBehaviour
{
	// Token: 0x06000BD6 RID: 3030 RVA: 0x0000DF23 File Offset: 0x0000C123
	private void Reset()
	{
		this.flowSpeed = 0.25f;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00093EC4 File Offset: 0x000920C4
	private void Start()
	{
		this.currentMaterial = base.GetComponent<Renderer>().material;
		this.cycle = 6f;
		this.halfCycle = this.cycle * 0.5f;
		this.flowMapOffset0 = 0f;
		this.flowMapOffset1 = this.halfCycle;
		this.currentMaterial.SetFloat("halfCycle", this.halfCycle);
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00093F2C File Offset: 0x0009212C
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

	// Token: 0x040008AF RID: 2223
	public float flowSpeed;

	// Token: 0x040008B0 RID: 2224
	private Material currentMaterial;

	// Token: 0x040008B1 RID: 2225
	private float cycle;

	// Token: 0x040008B2 RID: 2226
	private float halfCycle;

	// Token: 0x040008B3 RID: 2227
	private float flowMapOffset0;

	// Token: 0x040008B4 RID: 2228
	private float flowMapOffset1;

	// Token: 0x040008B5 RID: 2229
	private bool hasTide;
}
