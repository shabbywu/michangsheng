using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x0200098A RID: 2442
	[Serializable]
	public class PointBased : Action
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x0002CF49 File Offset: 0x0002B149
		public List<Vector3> PointPositions
		{
			get
			{
				return this.m_PointPositions;
			}
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x001B72D0 File Offset: 0x001B54D0
		public override void OnStart(AIBrain brain)
		{
			if (this.m_UsePredefinedPoints)
			{
				if (!ScriptUtilities.GetTransformsPositionsByTag(this.m_PointTag, out this.m_PointPositions))
				{
					Debug.LogError("No waypoints with tag " + this.m_PointTag + " have been setup");
					return;
				}
			}
			else
			{
				this.m_PointPositions = ScriptUtilities.GetRandomPositionsAroundTransform(brain.Settings.transform, this.m_PointAmount, this.m_PointMaxRadius, 5f);
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x0002CF51 File Offset: 0x0002B151
		public override bool CanActivate(AIBrain brain)
		{
			return this.m_PointPositions.Count > 0;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0002CF64 File Offset: 0x0002B164
		public override void Activate(AIBrain brain)
		{
			this.ChangePatrolPoint();
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], this.m_IsUrgent);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0002CF94 File Offset: 0x0002B194
		public override void OnUpdate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], this.m_IsUrgent);
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x001B733C File Offset: 0x001B553C
		public override void OnDeactivation(AIBrain brain)
		{
			string paramName = this.m_IsUrgent ? "Run" : "Walk";
			brain.Settings.Animation.ToggleBool(paramName, false);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0002CFBE File Offset: 0x0002B1BE
		public override bool IsDone(AIBrain brain)
		{
			return brain.Settings.Movement.ReachedDestination(true);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x001B7370 File Offset: 0x001B5570
		public void ChangePatrolPoint()
		{
			int num = 0;
			if (this.m_PointSequence == ET.PointOrder.Sequenced)
			{
				num = (this.m_CurrentIndex + 1) % this.m_PointPositions.Count;
			}
			else if (this.m_PointSequence == ET.PointOrder.Random)
			{
				num = Random.Range(0, this.m_PointPositions.Count);
			}
			if (num == this.m_CurrentIndex)
			{
				this.ChangePatrolPoint();
				return;
			}
			this.m_CurrentIndex = num;
		}

		// Token: 0x0400385B RID: 14427
		[SerializeField]
		[Tooltip("Determines if the AI will run to the waypoints or just walk.")]
		protected bool m_IsUrgent;

		// Token: 0x0400385C RID: 14428
		[SerializeField]
		[Tooltip("Determines whether the AI will go to the points at random or in order.")]
		private ET.PointOrder m_PointSequence;

		// Token: 0x0400385D RID: 14429
		[SerializeField]
		[Tooltip("Use predefined points? If not, we will create some at the start.")]
		private bool m_UsePredefinedPoints;

		// Token: 0x0400385E RID: 14430
		[SerializeField]
		[Tooltip("This is the tag the predefined points need to have in order to be detected.")]
		private string m_PointTag;

		// Token: 0x0400385F RID: 14431
		[Header("Procedural Point Creation")]
		[SerializeField]
		[Tooltip("Determines the amount of points that will be randomly created if we don't want to use predefined ones.")]
		private int m_PointAmount;

		// Token: 0x04003860 RID: 14432
		[SerializeField]
		[Tooltip("Determines the max distance from the AI at which the points will be created.")]
		private int m_PointMaxRadius;

		// Token: 0x04003861 RID: 14433
		protected int m_CurrentIndex;

		// Token: 0x04003862 RID: 14434
		protected List<Vector3> m_PointPositions;
	}
}
