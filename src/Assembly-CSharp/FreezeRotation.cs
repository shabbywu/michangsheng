using System;
using UnityEngine;

// Token: 0x020006A2 RID: 1698
public class FreezeRotation : MonoBehaviour
{
	// Token: 0x06002A6A RID: 10858 RVA: 0x00020F99 File Offset: 0x0001F199
	private void Start()
	{
		this.shieldPosition = base.transform.position;
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x00147078 File Offset: 0x00145278
	private void Update()
	{
		base.transform.position = new Vector3(this.tr.position.x, this.tr.position.y, base.transform.position.z);
	}

	// Token: 0x0400242C RID: 9260
	public Transform tr;

	// Token: 0x0400242D RID: 9261
	private Transform myTransform;

	// Token: 0x0400242E RID: 9262
	private Vector3 shieldPosition;

	// Token: 0x0400242F RID: 9263
	private float x;

	// Token: 0x04002430 RID: 9264
	private float y;
}
