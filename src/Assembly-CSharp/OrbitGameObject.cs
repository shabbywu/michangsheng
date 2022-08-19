using System;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class OrbitGameObject : Orbit
{
	// Token: 0x06000B51 RID: 2897 RVA: 0x00044FFA File Offset: 0x000431FA
	private void Start()
	{
		this.Data.Zenith = -0.3f;
		this.Data.Length = -6f;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0004501C File Offset: 0x0004321C
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

	// Token: 0x04000784 RID: 1924
	public GameObject Target;

	// Token: 0x04000785 RID: 1925
	public Vector3 ArmOffset = Vector3.zero;
}
