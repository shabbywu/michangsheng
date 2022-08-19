using System;
using EIU;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class SimpleCube : MonoBehaviour
{
	// Token: 0x06000C3A RID: 3130 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00049DA8 File Offset: 0x00047FA8
	private void Update()
	{
		if (EasyInputUtility.instance)
		{
			base.transform.Translate(this.moveSpeed * EasyInputUtility.instance.GetAxis("Horizontal") * Time.deltaTime, 0f, this.moveSpeed * EasyInputUtility.instance.GetAxis("Vertical") * Time.deltaTime);
			return;
		}
		base.transform.Translate(this.moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, this.moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
	}

	// Token: 0x04000888 RID: 2184
	public float moveSpeed = 5f;

	// Token: 0x04000889 RID: 2185
	public float jumpLimit = 5f;
}
