using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class Delay01 : MonoBehaviour
{
	// Token: 0x06000050 RID: 80 RVA: 0x00004217 File Offset: 0x00002417
	private void Start()
	{
		base.gameObject.SetActiveRecursively(false);
		base.Invoke("DelayFunc", this.delayTime);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004236 File Offset: 0x00002436
	private void DelayFunc()
	{
		base.gameObject.SetActiveRecursively(true);
	}

	// Token: 0x04000059 RID: 89
	public float delayTime = 1f;
}
