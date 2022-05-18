using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class AttachCamera : MonoBehaviour
{
	// Token: 0x06000BCF RID: 3023 RVA: 0x0000DEBF File Offset: 0x0000C0BF
	private void Start()
	{
		this.myTransform = base.transform;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00093E1C File Offset: 0x0009201C
	private void FixedUpdate()
	{
		if (this.target != null)
		{
			this.myTransform.position = this.target.position + this.offset;
			this.myTransform.LookAt(this.target.position, Vector3.up);
		}
	}

	// Token: 0x040008A5 RID: 2213
	private Transform myTransform;

	// Token: 0x040008A6 RID: 2214
	public Transform target;

	// Token: 0x040008A7 RID: 2215
	public Vector3 offset = new Vector3(0f, 5f, -5f);
}
