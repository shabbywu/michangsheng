using System;
using UnityEngine;

// Token: 0x02000660 RID: 1632
public class testrotaion : MonoBehaviour
{
	// Token: 0x060028C3 RID: 10435 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x0001FC91 File Offset: 0x0001DE91
	private void Update()
	{
		base.transform.localEulerAngles = new Vector3(this.x, this.y, this.z);
	}

	// Token: 0x04002265 RID: 8805
	public float x;

	// Token: 0x04002266 RID: 8806
	public float y;

	// Token: 0x04002267 RID: 8807
	public float z;
}
