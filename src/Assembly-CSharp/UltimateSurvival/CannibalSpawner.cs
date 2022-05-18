using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000843 RID: 2115
	public class CannibalSpawner : MonoBehaviour
	{
		// Token: 0x0600378F RID: 14223 RVA: 0x0002856D File Offset: 0x0002676D
		private void Start()
		{
			this.m_SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x001A0A00 File Offset: 0x0019EC00
		private void OnGUI()
		{
			if (!this.m_ShowGUI)
			{
				return;
			}
			Rect rect;
			rect..ctor(8f, 64f, 256f, 20f);
			GUI.Label(rect, "# CANNIBAL SPAWNER");
			rect.width = 128f;
			rect.y = rect.yMax + 4f;
			this.m_EnableSpawning = GUI.Toggle(rect, this.m_EnableSpawning, "Enable Spawning");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Spawn Interval: " + this.m_SpawnInterval);
			rect.y = rect.yMax + 4f;
			this.m_SpawnInterval = (float)((int)GUI.HorizontalSlider(rect, this.m_SpawnInterval, 3f, 120f));
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Max Count: " + this.m_MaxCount);
			rect.y = rect.yMax + 4f;
			this.m_MaxCount = (int)GUI.HorizontalSlider(rect, (float)this.m_MaxCount, 0f, 99f);
			rect.y = rect.yMax + 4f;
			if (GUI.Button(rect, "Remove All"))
			{
				foreach (AIEventHandler aieventHandler in Object.FindObjectsOfType<AIEventHandler>())
				{
					if (aieventHandler.name.Contains("Cannibal"))
					{
						Object.Destroy(aieventHandler.gameObject);
					}
				}
				this.m_AliveCannibals.Clear();
			}
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x001A0B98 File Offset: 0x0019ED98
		private void Update()
		{
			if (this.m_EnableSpawning && Time.time > this.m_LastUpdateTime && this.m_AliveCannibals.Count < this.m_MaxCount)
			{
				bool flag = this.m_SpawnMode == CannibalSpawner.SpawnMode.AllDay;
				bool flag2 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && this.m_SpawnMode == CannibalSpawner.SpawnMode.AtNight;
				bool flag3 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Day && this.m_SpawnMode == CannibalSpawner.SpawnMode.AtDaytime;
				if (!flag && !flag2 && !flag3)
				{
					return;
				}
				this.m_LastUpdateTime = Time.time + this.m_SpawnInterval;
				this.Spawn();
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x001A0C38 File Offset: 0x0019EE38
		private void Spawn()
		{
			Transform transform = this.m_SpawnPoints[Random.Range(0, this.m_SpawnPoints.Length)].transform;
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_Prefabs[Random.Range(0, this.m_Prefabs.Length)], transform.position, Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f)));
			EntityEventHandler cannibal = gameObject.GetComponent<EntityEventHandler>();
			if (cannibal != null)
			{
				this.m_AliveCannibals.Add(cannibal);
				cannibal.Death.AddListener(delegate
				{
					this.On_CannibalDeath(cannibal);
				});
			}
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0002857F File Offset: 0x0002677F
		private void On_CannibalDeath(EntityEventHandler cannibal)
		{
			this.m_AliveCannibals.Remove(cannibal);
		}

		// Token: 0x040031A5 RID: 12709
		[SerializeField]
		private bool m_ShowGUI = true;

		// Token: 0x040031A6 RID: 12710
		[SerializeField]
		private bool m_EnableSpawning = true;

		// Token: 0x040031A7 RID: 12711
		[SerializeField]
		[Space]
		[Clamp(0f, 99f)]
		private int m_MaxCount = 20;

		// Token: 0x040031A8 RID: 12712
		[SerializeField]
		private CannibalSpawner.SpawnMode m_SpawnMode;

		// Token: 0x040031A9 RID: 12713
		[SerializeField]
		[Clamp(3f, 120f)]
		private float m_SpawnInterval = 30f;

		// Token: 0x040031AA RID: 12714
		[SerializeField]
		private GameObject[] m_Prefabs;

		// Token: 0x040031AB RID: 12715
		private float m_LastUpdateTime;

		// Token: 0x040031AC RID: 12716
		private GameObject[] m_SpawnPoints;

		// Token: 0x040031AD RID: 12717
		private List<EntityEventHandler> m_AliveCannibals = new List<EntityEventHandler>();

		// Token: 0x02000844 RID: 2116
		public enum SpawnMode
		{
			// Token: 0x040031AF RID: 12719
			AtNight,
			// Token: 0x040031B0 RID: 12720
			AtDaytime,
			// Token: 0x040031B1 RID: 12721
			AllDay
		}
	}
}
