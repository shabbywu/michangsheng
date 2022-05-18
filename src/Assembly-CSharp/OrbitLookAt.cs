using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class OrbitLookAt : Orbit
{
	// Token: 0x06000C43 RID: 3139 RVA: 0x0000E45F File Offset: 0x0000C65F
	private void Start()
	{
		this.Data.Zenith = -0.3f;
		this.Data.Length = -6f;
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0000E494 File Offset: 0x0000C694
	protected override void Update()
	{
		base.Update();
		base.gameObject.transform.position += this.LookAt;
		base.gameObject.transform.LookAt(this.LookAt);
	}

	// Token: 0x04000961 RID: 2401
	public Vector3 LookAt = Vector3.zero;
}
