using System;
using EIU;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class SimpleCube : MonoBehaviour
{
	// Token: 0x06000D5B RID: 3419 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0009B644 File Offset: 0x00099844
	private void Update()
	{
		if (EasyInputUtility.instance)
		{
			base.transform.Translate(this.moveSpeed * EasyInputUtility.instance.GetAxis("Horizontal") * Time.deltaTime, 0f, this.moveSpeed * EasyInputUtility.instance.GetAxis("Vertical") * Time.deltaTime);
			return;
		}
		base.transform.Translate(this.moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, this.moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
	}

	// Token: 0x04000A84 RID: 2692
	public float moveSpeed = 5f;

	// Token: 0x04000A85 RID: 2693
	public float jumpLimit = 5f;
}
