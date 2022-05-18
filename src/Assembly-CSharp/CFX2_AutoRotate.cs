using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class CFX2_AutoRotate : MonoBehaviour
{
	// Token: 0x06000B8B RID: 2955 RVA: 0x0000D951 File Offset: 0x0000BB51
	private void Update()
	{
		base.transform.Rotate(this.speed * Time.deltaTime);
	}

	// Token: 0x04000858 RID: 2136
	public Vector3 speed = new Vector3(0f, 40f, 0f);
}
