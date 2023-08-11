using UnityEngine;

public class cardEffect : MonoBehaviour
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
	}
}
