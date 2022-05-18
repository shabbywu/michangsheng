using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI
{
	// Token: 0x02000970 RID: 2416
	public class AIGroup : MonoBehaviour
	{
		// Token: 0x06003DCA RID: 15818 RVA: 0x001B59FC File Offset: 0x001B3BFC
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

		// Token: 0x06003DCB RID: 15819 RVA: 0x001B5A90 File Offset: 0x001B3C90
		private void Update()
		{
			if (this.m_EnableSpawning && Time.time > this.m_LastUpdateTime && this.m_AliveAgents.Count < this.m_MaxCount && Vector3.Dot(this.m_Player.forward, base.transform.position - this.m_Player.position) < 0f)
			{
				this.TrySpawn();
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x001B5B04 File Offset: 0x001B3D04
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

		// Token: 0x06003DCD RID: 15821 RVA: 0x0002C83A File Offset: 0x0002AA3A
		private void On_AgentDeath(EntityEventHandler agent)
		{
			this.m_AliveAgents.Remove(agent);
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x0002C849 File Offset: 0x0002AA49
		private void OnDrawGizmosSelected()
		{
			Color color = Gizmos.color;
			Gizmos.color = this.m_GroupColor;
			Gizmos.DrawSphere(base.transform.position, this.m_GroupRadius);
			Gizmos.color = color;
		}

		// Token: 0x040037EC RID: 14316
		[SerializeField]
		private Color m_GroupColor;

		// Token: 0x040037ED RID: 14317
		[SerializeField]
		[Space]
		private bool m_EnableSpawning = true;

		// Token: 0x040037EE RID: 14318
		[SerializeField]
		private bool m_MakeAgentsChildren = true;

		// Token: 0x040037EF RID: 14319
		[SerializeField]
		private GameObject[] m_Prefabs;

		// Token: 0x040037F0 RID: 14320
		[SerializeField]
		[Space]
		[Clamp(0f, 30f)]
		private float m_GroupRadius = 10f;

		// Token: 0x040037F1 RID: 14321
		[SerializeField]
		[Clamp(0f, 5f)]
		private int m_MaxCount = 3;

		// Token: 0x040037F2 RID: 14322
		[SerializeField]
		[Space]
		private AIGroup.SpawnMode m_SpawnMode;

		// Token: 0x040037F3 RID: 14323
		[SerializeField]
		[Clamp(3f, 120f)]
		private float m_SpawnInterval = 30f;

		// Token: 0x040037F4 RID: 14324
		private float m_LastUpdateTime;

		// Token: 0x040037F5 RID: 14325
		private List<Vector3> m_SpawnPoints = new List<Vector3>();

		// Token: 0x040037F6 RID: 14326
		private List<EntityEventHandler> m_AliveAgents = new List<EntityEventHandler>();

		// Token: 0x040037F7 RID: 14327
		private Transform m_Player;

		// Token: 0x02000971 RID: 2417
		public enum SpawnMode
		{
			// Token: 0x040037F9 RID: 14329
			AtNight,
			// Token: 0x040037FA RID: 14330
			AtDaytime,
			// Token: 0x040037FB RID: 14331
			AllDay
		}
	}
}
