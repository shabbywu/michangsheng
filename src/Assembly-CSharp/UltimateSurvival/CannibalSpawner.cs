using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class CannibalSpawner : MonoBehaviour
{
	public enum SpawnMode
	{
		AtNight,
		AtDaytime,
		AllDay
	}

	[SerializeField]
	private bool m_ShowGUI = true;

	[SerializeField]
	private bool m_EnableSpawning = true;

	[SerializeField]
	[Space]
	[Clamp(0f, 99f)]
	private int m_MaxCount = 20;

	[SerializeField]
	private SpawnMode m_SpawnMode;

	[SerializeField]
	[Clamp(3f, 120f)]
	private float m_SpawnInterval = 30f;

	[SerializeField]
	private GameObject[] m_Prefabs;

	private float m_LastUpdateTime;

	private GameObject[] m_SpawnPoints;

	private List<EntityEventHandler> m_AliveCannibals = new List<EntityEventHandler>();

	private void Start()
	{
		m_SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
	}

	private void OnGUI()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		if (!m_ShowGUI)
		{
			return;
		}
		Rect val = default(Rect);
		((Rect)(ref val))._002Ector(8f, 64f, 256f, 20f);
		GUI.Label(val, "# CANNIBAL SPAWNER");
		((Rect)(ref val)).width = 128f;
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		m_EnableSpawning = GUI.Toggle(val, m_EnableSpawning, "Enable Spawning");
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		GUI.Label(val, "Spawn Interval: " + m_SpawnInterval);
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		m_SpawnInterval = (int)GUI.HorizontalSlider(val, m_SpawnInterval, 3f, 120f);
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		GUI.Label(val, "Max Count: " + m_MaxCount);
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		m_MaxCount = (int)GUI.HorizontalSlider(val, (float)m_MaxCount, 0f, 99f);
		((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
		if (!GUI.Button(val, "Remove All"))
		{
			return;
		}
		AIEventHandler[] array = Object.FindObjectsOfType<AIEventHandler>();
		foreach (AIEventHandler aIEventHandler in array)
		{
			if (((Object)aIEventHandler).name.Contains("Cannibal"))
			{
				Object.Destroy((Object)(object)((Component)aIEventHandler).gameObject);
			}
		}
		m_AliveCannibals.Clear();
	}

	private void Update()
	{
		if (m_EnableSpawning && Time.time > m_LastUpdateTime && m_AliveCannibals.Count < m_MaxCount)
		{
			bool num = m_SpawnMode == SpawnMode.AllDay;
			bool flag = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && m_SpawnMode == SpawnMode.AtNight;
			bool flag2 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Day && m_SpawnMode == SpawnMode.AtDaytime;
			if (num || flag || flag2)
			{
				m_LastUpdateTime = Time.time + m_SpawnInterval;
				Spawn();
			}
		}
	}

	private void Spawn()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform;
		GameObject val = Object.Instantiate<GameObject>(m_Prefabs[Random.Range(0, m_Prefabs.Length)], transform.position, Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f)));
		EntityEventHandler cannibal = val.GetComponent<EntityEventHandler>();
		if ((Object)(object)cannibal != (Object)null)
		{
			m_AliveCannibals.Add(cannibal);
			cannibal.Death.AddListener(delegate
			{
				On_CannibalDeath(cannibal);
			});
		}
	}

	private void On_CannibalDeath(EntityEventHandler cannibal)
	{
		m_AliveCannibals.Remove(cannibal);
	}
}
