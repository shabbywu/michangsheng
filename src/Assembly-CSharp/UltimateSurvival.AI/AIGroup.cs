using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI;

public class AIGroup : MonoBehaviour
{
	public enum SpawnMode
	{
		AtNight,
		AtDaytime,
		AllDay
	}

	[SerializeField]
	private Color m_GroupColor;

	[SerializeField]
	[Space]
	private bool m_EnableSpawning = true;

	[SerializeField]
	private bool m_MakeAgentsChildren = true;

	[SerializeField]
	private GameObject[] m_Prefabs;

	[SerializeField]
	[Space]
	[Clamp(0f, 30f)]
	private float m_GroupRadius = 10f;

	[SerializeField]
	[Clamp(0f, 5f)]
	private int m_MaxCount = 3;

	[SerializeField]
	[Space]
	private SpawnMode m_SpawnMode;

	[SerializeField]
	[Clamp(3f, 120f)]
	private float m_SpawnInterval = 30f;

	private float m_LastUpdateTime;

	private List<Vector3> m_SpawnPoints = new List<Vector3>();

	private List<EntityEventHandler> m_AliveAgents = new List<EntityEventHandler>();

	private Transform m_Player;

	private void Start()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		NavMeshHit val2 = default(NavMeshHit);
		for (int i = 0; i < m_MaxCount; i++)
		{
			Vector3 val = ((Component)this).transform.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * m_GroupRadius;
			if (NavMesh.SamplePosition(val, ref val2, 10f, -1))
			{
				val = ((NavMeshHit)(ref val2)).position;
			}
			m_SpawnPoints.Add(val);
			TrySpawn();
		}
		m_Player = ((Component)GameController.LocalPlayer).transform;
	}

	private void Update()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (m_EnableSpawning && Time.time > m_LastUpdateTime && m_AliveAgents.Count < m_MaxCount && Vector3.Dot(m_Player.forward, ((Component)this).transform.position - m_Player.position) < 0f)
		{
			TrySpawn();
		}
	}

	private void TrySpawn()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		bool num = m_SpawnMode == SpawnMode.AllDay;
		bool flag = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && m_SpawnMode == SpawnMode.AtNight;
		bool flag2 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Day && m_SpawnMode == SpawnMode.AtDaytime;
		if (!(num || flag || flag2))
		{
			return;
		}
		m_LastUpdateTime = Time.time + m_SpawnInterval;
		Vector3 val = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)];
		GameObject val2 = Object.Instantiate<GameObject>(m_Prefabs[Random.Range(0, m_Prefabs.Length)], val, Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f)));
		EntityEventHandler agent = val2.GetComponent<EntityEventHandler>();
		if (m_MakeAgentsChildren)
		{
			val2.transform.SetParent(((Component)this).transform, true);
		}
		if ((Object)(object)agent != (Object)null)
		{
			m_AliveAgents.Add(agent);
			agent.Death.AddListener(delegate
			{
				On_AgentDeath(agent);
			});
		}
	}

	private void On_AgentDeath(EntityEventHandler agent)
	{
		m_AliveAgents.Remove(agent);
	}

	private void OnDrawGizmosSelected()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Color color = Gizmos.color;
		Gizmos.color = m_GroupColor;
		Gizmos.DrawSphere(((Component)this).transform.position, m_GroupRadius);
		Gizmos.color = color;
	}
}
