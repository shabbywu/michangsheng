using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI
{
	// Token: 0x02000668 RID: 1640
	public class AIGroup : MonoBehaviour
	{
		// Token: 0x06003423 RID: 13347 RVA: 0x0016CE04 File Offset: 0x0016B004
		private void Start()
		{
			for (int i = 0; i < this.m_MaxCount; i++)
			{
				Vector3 vector = base.transform.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * this.m_GroupRadius;
				NavMeshHit navMeshHit;
				if (NavMesh.SamplePosition(vector, ref navMeshHit, 10f, -1))
				{
					vector = navMeshHit.position;
				}
				this.m_SpawnPoints.Add(vector);
				this.TrySpawn();
			}
			this.m_Player = GameController.LocalPlayer.transform;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x0016CE98 File Offset: 0x0016B098
		private void Update()
		{
			if (this.m_EnableSpawning && Time.time > this.m_LastUpdateTime && this.m_AliveAgents.Count < this.m_MaxCount && Vector3.Dot(this.m_Player.forward, base.transform.position - this.m_Player.position) < 0f)
			{
				this.TrySpawn();
			}
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x0016CF0C File Offset: 0x0016B10C
		private void TrySpawn()
		{
			bool flag = this.m_SpawnMode == AIGroup.SpawnMode.AllDay;
			bool flag2 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Night && this.m_SpawnMode == AIGroup.SpawnMode.AtNight;
			bool flag3 = MonoSingleton<TimeOfDay>.Instance.State.Get() == ET.TimeOfDay.Day && this.m_SpawnMode == AIGroup.SpawnMode.AtDaytime;
			if (!flag && !flag2 && !flag3)
			{
				return;
			}
			this.m_LastUpdateTime = Time.time + this.m_SpawnInterval;
			Vector3 vector = this.m_SpawnPoints[Random.Range(0, this.m_SpawnPoints.Count)];
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_Prefabs[Random.Range(0, this.m_Prefabs.Length)], vector, Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f)));
			EntityEventHandler agent = gameObject.GetComponent<EntityEventHandler>();
			if (this.m_MakeAgentsChildren)
			{
				gameObject.transform.SetParent(base.transform, true);
			}
			if (agent != null)
			{
				this.m_AliveAgents.Add(agent);
				agent.Death.AddListener(delegate
				{
					this.On_AgentDeath(agent);
				});
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x0016D040 File Offset: 0x0016B240
		private void On_AgentDeath(EntityEventHandler agent)
		{
			this.m_AliveAgents.Remove(agent);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x0016D04F File Offset: 0x0016B24F
		private void OnDrawGizmosSelected()
		{
			Color color = Gizmos.color;
			Gizmos.color = this.m_GroupColor;
			Gizmos.DrawSphere(base.transform.position, this.m_GroupRadius);
			Gizmos.color = color;
		}

		// Token: 0x04002E59 RID: 11865
		[SerializeField]
		private Color m_GroupColor;

		// Token: 0x04002E5A RID: 11866
		[SerializeField]
		[Space]
		private bool m_EnableSpawning = true;

		// Token: 0x04002E5B RID: 11867
		[SerializeField]
		private bool m_MakeAgentsChildren = true;

		// Token: 0x04002E5C RID: 11868
		[SerializeField]
		private GameObject[] m_Prefabs;

		// Token: 0x04002E5D RID: 11869
		[SerializeField]
		[Space]
		[Clamp(0f, 30f)]
		private float m_GroupRadius = 10f;

		// Token: 0x04002E5E RID: 11870
		[SerializeField]
		[Clamp(0f, 5f)]
		private int m_MaxCount = 3;

		// Token: 0x04002E5F RID: 11871
		[SerializeField]
		[Space]
		private AIGroup.SpawnMode m_SpawnMode;

		// Token: 0x04002E60 RID: 11872
		[SerializeField]
		[Clamp(3f, 120f)]
		private float m_SpawnInterval = 30f;

		// Token: 0x04002E61 RID: 11873
		private float m_LastUpdateTime;

		// Token: 0x04002E62 RID: 11874
		private List<Vector3> m_SpawnPoints = new List<Vector3>();

		// Token: 0x04002E63 RID: 11875
		private List<EntityEventHandler> m_AliveAgents = new List<EntityEventHandler>();

		// Token: 0x04002E64 RID: 11876
		private Transform m_Player;

		// Token: 0x020014F2 RID: 5362
		public enum SpawnMode
		{
			// Token: 0x04006DD6 RID: 28118
			AtNight,
			// Token: 0x04006DD7 RID: 28119
			AtDaytime,
			// Token: 0x04006DD8 RID: 28120
			AllDay
		}
	}
}
