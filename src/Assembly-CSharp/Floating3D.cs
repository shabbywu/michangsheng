using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class Floating3D : MonoBehaviour
{
	// Token: 0x06000B48 RID: 2888 RVA: 0x00044D15 File Offset: 0x00042F15
	private void Start()
	{
		this.positionTemp = base.transform.position;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00044D28 File Offset: 0x00042F28
	private void Update()
	{
		this.positionTemp += this.PositionDirection * Time.deltaTime;
		this.PositionDirection += this.PositionMult * Time.deltaTime;
		base.transform.position = Vector3.Lerp(base.transform.position, this.positionTemp, 0.5f);
	}

	// Token: 0x04000776 RID: 1910
	public Vector3 PositionMult;

	// Token: 0x04000777 RID: 1911
	public Vector3 PositionDirection;

	// Token: 0x04000778 RID: 1912
	private Vector3 positionTemp;

	// Token: 0x04000779 RID: 1913
	private FloatingText floatingtext;
}
