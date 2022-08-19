using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class testrotaion : MonoBehaviour
{
	// Token: 0x060024D7 RID: 9431 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x000FFB7E File Offset: 0x000FDD7E
	private void Update()
	{
		base.transform.localEulerAngles = new Vector3(this.x, this.y, this.z);
	}

	// Token: 0x04001D78 RID: 7544
	public float x;

	// Token: 0x04001D79 RID: 7545
	public float y;

	// Token: 0x04001D7A RID: 7546
	public float z;
}
