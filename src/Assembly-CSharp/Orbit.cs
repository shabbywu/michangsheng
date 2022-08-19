using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class Orbit : MonoBehaviour
{
	// Token: 0x06000B4F RID: 2895 RVA: 0x00044FBB File Offset: 0x000431BB
	protected virtual void Update()
	{
		base.gameObject.transform.position = this.Data.Position;
	}

	// Token: 0x04000783 RID: 1923
	public SphericalVector Data = new SphericalVector(0f, 0f, 1f);
}
