using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x0200066C RID: 1644
	[Serializable]
	public class EntityDetection
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x0016D946 File Offset: 0x0016BB46
		public int ViewRadius
		{
			get
			{
				return this.m_ViewRadius;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x0016D94E File Offset: 0x0016BB4E
		public int ViewAngle
		{
			get
			{
				return this.m_ViewAngle;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x0016D956 File Offset: 0x0016BB56
		public int HearRange
		{
			get
			{
				return this.m_HearRange;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x0016D95E File Offset: 0x0016BB5E
		// (set) Token: 0x0600344B RID: 13387 RVA: 0x0016D966 File Offset: 0x0016BB66
		public GameObject LastChasedTarget { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x0016D96F File Offset: 0x0016BB6F
		public List<GameObject> VisibleTargets
		{
			get
			{
				return this.m_VisibleTargets;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x0016D977 File Offset: 0x0016BB77
		public List<GameObject> StillInRangeTargets
		{
			get
			{
				return this.m_StillInRangeTargets;
			}
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x0016D97F File Offset: 0x0016BB7F
		public void Initialize(Transform transform)
		{
			this.m_Transform = transform;
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x0016D988 File Offset: 0x0016BB88
		public void Update(AIBrain brain)
		{
			if (this.m_TargetSearchDelay == 0f)
			{
				return;
			}
			if (Time.time - this.m_LastTargetFindTime >= this.m_TargetSearchDelay)
			{
				this.m_VisibleTargets = this.GetVisibleTargets();
				this.GetTargetsStillInRange();
				this.m_LastTargetFindTime = Time.time;
				StateData.OverrideValue("Player in sight", this.HasTarget(), brain.WorldState);
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x0016D9EF File Offset: 0x0016BBEF
		public bool HasTarget()
		{
			return this.m_StillInRangeTargets.Count > 0;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x0016D9FF File Offset: 0x0016BBFF
		public bool HasVisibleTarget()
		{
			return this.m_VisibleTargets.Count > 0;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x0016DA0F File Offset: 0x0016BC0F
		public Transform GetRandomTarget()
		{
			return this.m_StillInRangeTargets[Random.Range(0, this.m_StillInRangeTargets.Count)].transform;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x0016DA34 File Offset: 0x0016BC34
		private void GetTargetsStillInRange()
		{
			Collider[] collidersInRange = this.GetCollidersInRange();
			for (int i = 0; i < collidersInRange.Length; i++)
			{
				if (this.m_VisibleTargets.Contains(collidersInRange[i].gameObject) && !this.m_StillInRangeTargets.Contains(collidersInRange[i].gameObject))
				{
					this.m_StillInRangeTargets.Add(collidersInRange[i].gameObject);
				}
			}
			for (int j = 0; j < this.m_StillInRangeTargets.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < collidersInRange.Length; k++)
				{
					if (collidersInRange[k].gameObject == this.m_StillInRangeTargets[j])
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this.m_StillInRangeTargets.Remove(this.m_StillInRangeTargets[j]);
				}
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x0016DAF8 File Offset: 0x0016BCF8
		private List<GameObject> GetVisibleTargets()
		{
			List<GameObject> list = new List<GameObject>();
			Collider[] collidersInRange = this.GetCollidersInRange();
			for (int i = 0; i < collidersInRange.Length; i++)
			{
				if (!(collidersInRange[i].GetComponent<PlayerEventHandler>() == null))
				{
					Transform transform = collidersInRange[i].transform;
					Vector3 vector = transform.position + Vector3.up;
					Vector3 normalized = (vector - this.m_Eyes.position).normalized;
					if (Vector3.Angle(this.m_Eyes.forward, normalized) < (float)(this.m_ViewAngle / 2))
					{
						float num = Vector3.Distance(this.m_Eyes.position, vector);
						if (!Physics.Raycast(this.m_Eyes.position, normalized, num, this.m_ObstacleMask))
						{
							list.Add(transform.gameObject);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x0016DBD0 File Offset: 0x0016BDD0
		private Collider[] GetCollidersInRange()
		{
			return Physics.OverlapSphere(this.m_Transform.position, (float)this.m_ViewRadius, this.m_SpotMask);
		}

		// Token: 0x04002E7D RID: 11901
		[SerializeField]
		[Tooltip("Time it takes to look for targets again.")]
		private float m_TargetSearchDelay;

		// Token: 0x04002E7E RID: 11902
		[SerializeField]
		private Transform m_Eyes;

		// Token: 0x04002E7F RID: 11903
		[SerializeField]
		[Range(0f, 360f)]
		[Tooltip("Angle at which it can spot a player.")]
		private int m_ViewAngle = 120;

		// Token: 0x04002E80 RID: 11904
		[SerializeField]
		[Tooltip("Radius around the AI at which it can spot a player")]
		private int m_ViewRadius = 3;

		// Token: 0x04002E81 RID: 11905
		[SerializeField]
		[Clamp(0f, 300f)]
		private int m_HearRange = 50;

		// Token: 0x04002E82 RID: 11906
		[SerializeField]
		[Tooltip("Used for finding only specific types of targets.")]
		private LayerMask m_SpotMask;

		// Token: 0x04002E83 RID: 11907
		[SerializeField]
		[Tooltip("Used to know what objects can be blocking our view from the AI.")]
		private LayerMask m_ObstacleMask;

		// Token: 0x04002E84 RID: 11908
		private List<GameObject> m_VisibleTargets = new List<GameObject>();

		// Token: 0x04002E85 RID: 11909
		private List<GameObject> m_StillInRangeTargets = new List<GameObject>();

		// Token: 0x04002E86 RID: 11910
		private Transform m_Transform;

		// Token: 0x04002E87 RID: 11911
		private float m_LastTargetFindTime;
	}
}
