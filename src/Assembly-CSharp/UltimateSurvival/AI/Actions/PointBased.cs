using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x0200067D RID: 1661
	[Serializable]
	public class PointBased : Action
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x0016EE1A File Offset: 0x0016D01A
		public List<Vector3> PointPositions
		{
			get
			{
				return this.m_PointPositions;
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x0016EE24 File Offset: 0x0016D024
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

		// Token: 0x060034BF RID: 13503 RVA: 0x0016EE8E File Offset: 0x0016D08E
		public override bool CanActivate(AIBrain brain)
		{
			return this.m_PointPositions.Count > 0;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0016EEA1 File Offset: 0x0016D0A1
		public override void Activate(AIBrain brain)
		{
			this.ChangePatrolPoint();
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], this.m_IsUrgent);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x0016EED1 File Offset: 0x0016D0D1
		public override void OnUpdate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], this.m_IsUrgent);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x0016EEFC File Offset: 0x0016D0FC
		public override void OnDeactivation(AIBrain brain)
		{
			string paramName = this.m_IsUrgent ? "Run" : "Walk";
			brain.Settings.Animation.ToggleBool(paramName, false);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x0016EF30 File Offset: 0x0016D130
		public override bool IsDone(AIBrain brain)
		{
			return brain.Settings.Movement.ReachedDestination(true);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x0016EF44 File Offset: 0x0016D144
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

		// Token: 0x04002EB5 RID: 11957
		[SerializeField]
		[Tooltip("Determines if the AI will run to the waypoints or just walk.")]
		protected bool m_IsUrgent;

		// Token: 0x04002EB6 RID: 11958
		[SerializeField]
		[Tooltip("Determines whether the AI will go to the points at random or in order.")]
		private ET.PointOrder m_PointSequence;

		// Token: 0x04002EB7 RID: 11959
		[SerializeField]
		[Tooltip("Use predefined points? If not, we will create some at the start.")]
		private bool m_UsePredefinedPoints;

		// Token: 0x04002EB8 RID: 11960
		[SerializeField]
		[Tooltip("This is the tag the predefined points need to have in order to be detected.")]
		private string m_PointTag;

		// Token: 0x04002EB9 RID: 11961
		[Header("Procedural Point Creation")]
		[SerializeField]
		[Tooltip("Determines the amount of points that will be randomly created if we don't want to use predefined ones.")]
		private int m_PointAmount;

		// Token: 0x04002EBA RID: 11962
		[SerializeField]
		[Tooltip("Determines the max distance from the AI at which the points will be created.")]
		private int m_PointMaxRadius;

		// Token: 0x04002EBB RID: 11963
		protected int m_CurrentIndex;

		// Token: 0x04002EBC RID: 11964
		protected List<Vector3> m_PointPositions;
	}
}
