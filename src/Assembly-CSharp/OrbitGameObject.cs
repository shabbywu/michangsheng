using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class OrbitGameObject : Orbit
{
	// Token: 0x06000C40 RID: 3136 RVA: 0x0000E45F File Offset: 0x0000C65F
	private void Start()
	{
		this.Data.Zenith = -0.3f;
		this.Data.Length = -6f;
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00096C34 File Offset: 0x00094E34
	protected override void Update()
	{
		Time.timeScale += (1f - Time.timeScale) / 10f;
		Vector3 vector = this.ArmOffset;
		if (this.Target != null)
		{
			vector += this.Target.transform.position;
		}
		base.Update();
		base.gameObject.transform.position += vector;
		base.gameObject.transform.LookAt(vector);
	}

	// Token: 0x0400095F RID: 2399
	public GameObject Target;

	// Token: 0x04000960 RID: 2400
	public Vector3 ArmOffset = Vector3.zero;
}
