using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class cardEffect : MonoBehaviour
{
	// Token: 0x060022E2 RID: 8930 RVA: 0x000EE6A8 File Offset: 0x000EC8A8
	public void Start()
	{
		this.m_RelaCoor = (this.ToPos - this.FromPos).normalized;
		this.m_fStartRockon = Time.time;
		this.m_fDistance = Vector3.Distance(this.ToPos, this.FromPos);
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x00004095 File Offset: 0x00002295
	public void Update()
	{
	}

	// Token: 0x04001C0F RID: 7183
	public Vector3 FromPos = Vector3.zero;

	// Token: 0x04001C10 RID: 7184
	public Vector3 ToPos = Vector3.zero;

	// Token: 0x04001C11 RID: 7185
	public float MaxDistance = 30f;

	// Token: 0x04001C12 RID: 7186
	public float Speed = 30f;

	// Token: 0x04001C13 RID: 7187
	public float HWRate;

	// Token: 0x04001C14 RID: 7188
	private Vector3 m_RelaCoor = Vector3.up;

	// Token: 0x04001C15 RID: 7189
	private float m_fStartRockon;

	// Token: 0x04001C16 RID: 7190
	private float m_fDistance;
}
