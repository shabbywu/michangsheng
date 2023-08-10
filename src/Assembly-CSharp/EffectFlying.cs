using UnityEngine;

public class EffectFlying : MonoBehaviour
{
	public Vector3 FromPos = Vector3.zero;

	public Vector3 ToPos = Vector3.zero;

	public float MaxDistance = 30f;

	public float Speed = 30f;

	public float HWRate;

	private Vector3 m_RelaCoor = Vector3.up;

	private float m_fStartRockon;

	private float m_fDistance;

	public void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ToPos - FromPos;
		m_RelaCoor = ((Vector3)(ref val)).normalized;
		m_fStartRockon = Time.time;
		m_fDistance = Vector3.Distance(ToPos, FromPos);
	}

	public void Update()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		float num = Time.time - m_fStartRockon;
		if (num > m_fDistance / Speed)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		Vector3 val = m_RelaCoor * Speed * num;
		float num2 = 4f * (Speed * num * HWRate - Mathf.Pow(Speed * num, 2f) * HWRate / m_fDistance);
		val.y += num2;
		Vector3 val2 = FromPos + m_RelaCoor + val;
		((Component)this).transform.LookAt(val2);
		((Component)this).transform.position = val2;
	}
}
