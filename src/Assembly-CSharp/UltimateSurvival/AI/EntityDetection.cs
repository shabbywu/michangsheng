using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI
{
	// Token: 0x02000977 RID: 2423
	[Serializable]
	public class EntityDetection
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x0002C9E2 File Offset: 0x0002ABE2
		public int ViewRadius
		{
			get
			{
				return this.m_ViewRadius;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x0002C9EA File Offset: 0x0002ABEA
		public int ViewAngle
		{
			get
			{
				return this.m_ViewAngle;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x0002C9F2 File Offset: 0x0002ABF2
		public int HearRange
		{
			get
			{
				return this.m_HearRange;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x0002C9FA File Offset: 0x0002ABFA
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x0002CA02 File Offset: 0x0002AC02
		public GameObject LastChasedTarget { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x0002CA0B File Offset: 0x0002AC0B
		public List<GameObject> VisibleTargets
		{
			get
			{
				return this.m_VisibleTargets;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x0002CA13 File Offset: 0x0002AC13
		public List<GameObject> StillInRangeTargets
		{
			get
			{
				return this.m_StillInRangeTargets;
			}
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x0002CA1B File Offset: 0x0002AC1B
		public void Initialize(Transform transform)
		{
			this.m_Transform = transform;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x001B63BC File Offset: 0x001B45BC
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

		// Token: 0x06003DFC RID: 15868 RVA: 0x0002CA24 File Offset: 0x0002AC24
		public bool HasTarget()
		{
			return this.m_StillInRangeTargets.Count > 0;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x0002CA34 File Offset: 0x0002AC34
		public bool HasVisibleTarget()
		{
			return this.m_VisibleTargets.Count > 0;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x0002CA44 File Offset: 0x0002AC44
		public Transform GetRandomTarget()
		{
			return this.m_StillInRangeTargets[Random.Range(0, this.m_StillInRangeTargets.Count)].transform;
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x001B6424 File Offset: 0x001B4624
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

		// Token: 0x06003E00 RID: 15872 RVA: 0x001B64E8 File Offset: 0x001B46E8
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

		// Token: 0x06003E01 RID: 15873 RVA: 0x0002CA67 File Offset: 0x0002AC67
		private Collider[] GetCollidersInRange()
		{
			return Physics.OverlapSphere(this.m_Transform.position, (float)this.m_ViewRadius, this.m_SpotMask);
		}

		// Token: 0x04003818 RID: 14360
		[SerializeField]
		[Tooltip("Time it takes to look for targets again.")]
		private float m_TargetSearchDelay;

		// Token: 0x04003819 RID: 14361
		[SerializeField]
		private Transform m_Eyes;

		// Token: 0x0400381A RID: 14362
		[SerializeField]
		[Range(0f, 360f)]
		[Tooltip("Angle at which it can spot a player.")]
		private int m_ViewAngle = 120;

		// Token: 0x0400381B RID: 14363
		[SerializeField]
		[Tooltip("Radius around the AI at which it can spot a player")]
		private int m_ViewRadius = 3;

		// Token: 0x0400381C RID: 14364
		[SerializeField]
		[Clamp(0f, 300f)]
		private int m_HearRange = 50;

		// Token: 0x0400381D RID: 14365
		[SerializeField]
		[Tooltip("Used for finding only specific types of targets.")]
		private LayerMask m_SpotMask;

		// Token: 0x0400381E RID: 14366
		[SerializeField]
		[Tooltip("Used to know what objects can be blocking our view from the AI.")]
		private LayerMask m_ObstacleMask;

		// Token: 0x0400381F RID: 14367
		private List<GameObject> m_VisibleTargets = new List<GameObject>();

		// Token: 0x04003820 RID: 14368
		private List<GameObject> m_StillInRangeTargets = new List<GameObject>();

		// Token: 0x04003821 RID: 14369
		private Transform m_Transform;

		// Token: 0x04003822 RID: 14370
		private float m_LastTargetFindTime;
	}
}
