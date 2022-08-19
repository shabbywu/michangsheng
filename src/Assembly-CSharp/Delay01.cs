using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class Delay01 : MonoBehaviour
{
	// Token: 0x06000050 RID: 80 RVA: 0x00003B6D File Offset: 0x00001D6D
	private void Start()
	{
		base.gameObject.SetActiveRecursively(false);
		base.Invoke("DelayFunc", this.delayTime);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003B8C File Offset: 0x00001D8C
	private void DelayFunc()
	{
		base.gameObject.SetActiveRecursively(true);
	}

	// Token: 0x04000053 RID: 83
	public float delayTime = 1f;
}
