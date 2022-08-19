using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000598 RID: 1432
	public class CannibalSpawner : MonoBehaviour
	{
		// Token: 0x06002F13 RID: 12051 RVA: 0x001561C0 File Offset: 0x001543C0
		private void Start()
		{
			this.m_SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x001561D4 File Offset: 0x001543D4
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

		// Token: 0x06002F15 RID: 12053 RVA: 0x0015636C File Offset: 0x0015456C
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

		// Token: 0x06002F16 RID: 12054 RVA: 0x0015640C File Offset: 0x0015460C
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

		// Token: 0x06002F17 RID: 12055 RVA: 0x001564C7 File Offset: 0x001546C7
		private void On_CannibalDeath(EntityEventHandler cannibal)
		{
			this.m_AliveCannibals.Remove(cannibal);
		}

		// Token: 0x0400295E RID: 10590
		[SerializeField]
		private bool m_ShowGUI = true;

		// Token: 0x0400295F RID: 10591
		[SerializeField]
		private bool m_EnableSpawning = true;

		// Token: 0x04002960 RID: 10592
		[SerializeField]
		[Space]
		[Clamp(0f, 99f)]
		private int m_MaxCount = 20;

		// Token: 0x04002961 RID: 10593
		[SerializeField]
		private CannibalSpawner.SpawnMode m_SpawnMode;

		// Token: 0x04002962 RID: 10594
		[SerializeField]
		[Clamp(3f, 120f)]
		private float m_SpawnInterval = 30f;

		// Token: 0x04002963 RID: 10595
		[SerializeField]
		private GameObject[] m_Prefabs;

		// Token: 0x04002964 RID: 10596
		private float m_LastUpdateTime;

		// Token: 0x04002965 RID: 10597
		private GameObject[] m_SpawnPoints;

		// Token: 0x04002966 RID: 10598
		private List<EntityEventHandler> m_AliveCannibals = new List<EntityEventHandler>();

		// Token: 0x02001495 RID: 5269
		public enum SpawnMode
		{
			// Token: 0x04006C8A RID: 27786
			AtNight,
			// Token: 0x04006C8B RID: 27787
			AtDaytime,
			// Token: 0x04006C8C RID: 27788
			AllDay
		}
	}
}
