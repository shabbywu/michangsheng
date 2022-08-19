using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class AttachCamera : MonoBehaviour
{
	// Token: 0x06000AEC RID: 2796 RVA: 0x00041F5F File Offset: 0x0004015F
	private void Start()
	{
		this.myTransform = base.transform;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00041F70 File Offset: 0x00040170
	private void FixedUpdate()
	{
		if (this.target != null)
		{
			this.myTransform.position = this.target.position + this.offset;
			this.myTransform.LookAt(this.target.position, Vector3.up);
		}
	}

	// Token: 0x040006FA RID: 1786
	private Transform myTransform;

	// Token: 0x040006FB RID: 1787
	public Transform target;

	// Token: 0x040006FC RID: 1788
	public Vector3 offset = new Vector3(0f, 5f, -5f);
}
