using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class CFX2_AutoRotate : MonoBehaviour
{
	// Token: 0x06000AA8 RID: 2728 RVA: 0x0004092E File Offset: 0x0003EB2E
	private void Update()
	{
		base.transform.Rotate(this.speed * Time.deltaTime);
	}

	// Token: 0x040006B1 RID: 1713
	public Vector3 speed = new Vector3(0f, 40f, 0f);
}
