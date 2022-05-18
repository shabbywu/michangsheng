using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class Orbit : MonoBehaviour
{
	// Token: 0x06000C3E RID: 3134 RVA: 0x0000E420 File Offset: 0x0000C620
	protected virtual void Update()
	{
		base.gameObject.transform.position = this.Data.Position;
	}

	// Token: 0x0400095E RID: 2398
	public SphericalVector Data = new SphericalVector(0f, 0f, 1f);
}
