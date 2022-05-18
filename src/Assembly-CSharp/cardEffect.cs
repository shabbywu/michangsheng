using System;
using UnityEngine;

// Token: 0x0200060C RID: 1548
public class cardEffect : MonoBehaviour
{
	// Token: 0x06002699 RID: 9881 RVA: 0x0012EEDC File Offset: 0x0012D0DC
	public void Start()
	{
		this.m_RelaCoor = (this.ToPos - this.FromPos).normalized;
		this.m_fStartRockon = Time.time;
		this.m_fDistance = Vector3.Distance(this.ToPos, this.FromPos);
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x000042DD File Offset: 0x000024DD
	public void Update()
	{
	}

	// Token: 0x040020DF RID: 8415
	public Vector3 FromPos = Vector3.zero;

	// Token: 0x040020E0 RID: 8416
	public Vector3 ToPos = Vector3.zero;

	// Token: 0x040020E1 RID: 8417
	public float MaxDistance = 30f;

	// Token: 0x040020E2 RID: 8418
	public float Speed = 30f;

	// Token: 0x040020E3 RID: 8419
	public float HWRate;

	// Token: 0x040020E4 RID: 8420
	private Vector3 m_RelaCoor = Vector3.up;

	// Token: 0x040020E5 RID: 8421
	private float m_fStartRockon;

	// Token: 0x040020E6 RID: 8422
	private float m_fDistance;
}
