using System;
using UnityEngine;

// Token: 0x02000610 RID: 1552
public class EffectFlying : MonoBehaviour
{
	// Token: 0x060026A3 RID: 9891 RVA: 0x0012F030 File Offset: 0x0012D230
	public void Start()
	{
		this.m_RelaCoor = (this.ToPos - this.FromPos).normalized;
		this.m_fStartRockon = Time.time;
		this.m_fDistance = Vector3.Distance(this.ToPos, this.FromPos);
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x0012F080 File Offset: 0x0012D280
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

	// Token: 0x040020EE RID: 8430
	public Vector3 FromPos = Vector3.zero;

	// Token: 0x040020EF RID: 8431
	public Vector3 ToPos = Vector3.zero;

	// Token: 0x040020F0 RID: 8432
	public float MaxDistance = 30f;

	// Token: 0x040020F1 RID: 8433
	public float Speed = 30f;

	// Token: 0x040020F2 RID: 8434
	public float HWRate;

	// Token: 0x040020F3 RID: 8435
	private Vector3 m_RelaCoor = Vector3.up;

	// Token: 0x040020F4 RID: 8436
	private float m_fStartRockon;

	// Token: 0x040020F5 RID: 8437
	private float m_fDistance;
}
