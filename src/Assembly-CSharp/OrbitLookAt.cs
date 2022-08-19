using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class OrbitLookAt : Orbit
{
	// Token: 0x06000B54 RID: 2900 RVA: 0x00044FFA File Offset: 0x000431FA
	private void Start()
	{
		this.Data.Zenith = -0.3f;
		this.Data.Length = -6f;
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x000450B7 File Offset: 0x000432B7
	protected override void Update()
	{
		base.Update();
		base.gameObject.transform.position += this.LookAt;
		base.gameObject.transform.LookAt(this.LookAt);
	}

	// Token: 0x04000786 RID: 1926
	public Vector3 LookAt = Vector3.zero;
}
