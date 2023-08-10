using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI;

[Serializable]
public class EntityDetection
{
	[SerializeField]
	[Tooltip("Time it takes to look for targets again.")]
	private float m_TargetSearchDelay;

	[SerializeField]
	private Transform m_Eyes;

	[SerializeField]
	[Range(0f, 360f)]
	[Tooltip("Angle at which it can spot a player.")]
	private int m_ViewAngle = 120;

	[SerializeField]
	[Tooltip("Radius around the AI at which it can spot a player")]
	private int m_ViewRadius = 3;

	[SerializeField]
	[Clamp(0f, 300f)]
	private int m_HearRange = 50;

	[SerializeField]
	[Tooltip("Used for finding only specific types of targets.")]
	private LayerMask m_SpotMask;

	[SerializeField]
	[Tooltip("Used to know what objects can be blocking our view from the AI.")]
	private LayerMask m_ObstacleMask;

	private List<GameObject> m_VisibleTargets = new List<GameObject>();

	private List<GameObject> m_StillInRangeTargets = new List<GameObject>();

	private Transform m_Transform;

	private float m_LastTargetFindTime;

	public int ViewRadius => m_ViewRadius;

	public int ViewAngle => m_ViewAngle;

	public int HearRange => m_HearRange;

	public GameObject LastChasedTarget { get; set; }

	public List<GameObject> VisibleTargets => m_VisibleTargets;

	public List<GameObject> StillInRangeTargets => m_StillInRangeTargets;

	public void Initialize(Transform transform)
	{
		m_Transform = transform;
	}

	public void Update(AIBrain brain)
	{
		if (m_TargetSearchDelay != 0f && Time.time - m_LastTargetFindTime >= m_TargetSearchDelay)
		{
			m_VisibleTargets = GetVisibleTargets();
			GetTargetsStillInRange();
			m_LastTargetFindTime = Time.time;
			StateData.OverrideValue("Player in sight", HasTarget(), brain.WorldState);
		}
	}

	public bool HasTarget()
	{
		return m_StillInRangeTargets.Count > 0;
	}

	public bool HasVisibleTarget()
	{
		return m_VisibleTargets.Count > 0;
	}

	public Transform GetRandomTarget()
	{
		return m_StillInRangeTargets[Random.Range(0, m_StillInRangeTargets.Count)].transform;
	}

	private void GetTargetsStillInRange()
	{
		Collider[] collidersInRange = GetCollidersInRange();
		for (int i = 0; i < collidersInRange.Length; i++)
		{
			if (m_VisibleTargets.Contains(((Component)collidersInRange[i]).gameObject) && !m_StillInRangeTargets.Contains(((Component)collidersInRange[i]).gameObject))
			{
				m_StillInRangeTargets.Add(((Component)collidersInRange[i]).gameObject);
			}
		}
		for (int j = 0; j < m_StillInRangeTargets.Count; j++)
		{
			bool flag = false;
			for (int k = 0; k < collidersInRange.Length; k++)
			{
				if ((Object)(object)((Component)collidersInRange[k]).gameObject == (Object)(object)m_StillInRangeTargets[j])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				m_StillInRangeTargets.Remove(m_StillInRangeTargets[j]);
			}
		}
	}

	private List<GameObject> GetVisibleTargets()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		List<GameObject> list = new List<GameObject>();
		Collider[] collidersInRange = GetCollidersInRange();
		for (int i = 0; i < collidersInRange.Length; i++)
		{
			if ((Object)(object)((Component)collidersInRange[i]).GetComponent<PlayerEventHandler>() == (Object)null)
			{
				continue;
			}
			Transform transform = ((Component)collidersInRange[i]).transform;
			Vector3 val = transform.position + Vector3.up;
			Vector3 val2 = val - m_Eyes.position;
			Vector3 normalized = ((Vector3)(ref val2)).normalized;
			if (Vector3.Angle(m_Eyes.forward, normalized) < (float)(m_ViewAngle / 2))
			{
				float num = Vector3.Distance(m_Eyes.position, val);
				if (!Physics.Raycast(m_Eyes.position, normalized, num, LayerMask.op_Implicit(m_ObstacleMask)))
				{
					list.Add(((Component)transform).gameObject);
				}
			}
		}
		return list;
	}

	private Collider[] GetCollidersInRange()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return Physics.OverlapSphere(m_Transform.position, (float)m_ViewRadius, LayerMask.op_Implicit(m_SpotMask));
	}
}
