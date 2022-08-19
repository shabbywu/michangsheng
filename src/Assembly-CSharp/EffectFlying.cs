using System;
using UnityEngine;

// Token: 0x02000457 RID: 1111
public class EffectFlying : MonoBehaviour
{
	// Token: 0x060022EC RID: 8940 RVA: 0x000EE894 File Offset: 0x000ECA94
	public void Start()
	{
		this.m_RelaCoor = (this.ToPos - this.FromPos).normalized;
		this.m_fStartRockon = Time.time;
		this.m_fDistance = Vector3.Distance(this.ToPos, this.FromPos);
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x000EE8E4 File Offset: 0x000ECAE4
	public void Update()
	{
		float num = Time.time - this.m_fStartRockon;
		if (num > this.m_fDistance / this.Speed)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		Vector3 vector = this.m_RelaCoor * this.Speed * num;
		float num2 = 4f * (this.Speed * num * this.HWRate - Mathf.Pow(this.Speed * num, 2f) * this.HWRate / this.m_fDistance);
		vector.y += num2;
		Vector3 vector2 = this.FromPos + this.m_RelaCoor + vector;
		base.transform.LookAt(vector2);
		base.transform.position = vector2;
	}

	// Token: 0x04001C1E RID: 7198
	public Vector3 FromPos = Vector3.zero;

	// Token: 0x04001C1F RID: 7199
	public Vector3 ToPos = Vector3.zero;

	// Token: 0x04001C20 RID: 7200
	public float MaxDistance = 30f;

	// Token: 0x04001C21 RID: 7201
	public float Speed = 30f;

	// Token: 0x04001C22 RID: 7202
	public float HWRate;

	// Token: 0x04001C23 RID: 7203
	private Vector3 m_RelaCoor = Vector3.up;

	// Token: 0x04001C24 RID: 7204
	private float m_fStartRockon;

	// Token: 0x04001C25 RID: 7205
	private float m_fDistance;
}
