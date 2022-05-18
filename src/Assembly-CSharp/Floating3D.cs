using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class Floating3D : MonoBehaviour
{
	// Token: 0x06000C37 RID: 3127 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
	private void Start()
	{
		this.positionTemp = base.transform.position;
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x000969D4 File Offset: 0x00094BD4
	private void Update()
	{
		this.positionTemp += this.PositionDirection * Time.deltaTime;
		this.PositionDirection += this.PositionMult * Time.deltaTime;
		base.transform.position = Vector3.Lerp(base.transform.position, this.positionTemp, 0.5f);
	}

	// Token: 0x04000951 RID: 2385
	public Vector3 PositionMult;

	// Token: 0x04000952 RID: 2386
	public Vector3 PositionDirection;

	// Token: 0x04000953 RID: 2387
	private Vector3 positionTemp;

	// Token: 0x04000954 RID: 2388
	private FloatingText floatingtext;
}
