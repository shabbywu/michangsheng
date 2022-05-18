using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000988 RID: 2440
	public class Flee : Action
	{
		// Token: 0x06003E5D RID: 15965 RVA: 0x001B6EB0 File Offset: 0x001B50B0
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 12;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Player in sight", true);
			base.Effects.Add("Player in sight", false);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x001B6F00 File Offset: 0x001B5100
		public override bool CanActivate(AIBrain brain)
		{
			if (brain.Settings.Detection.HasTarget())
			{
				Vector3 position = brain.transform.position;
				Vector3 position2 = brain.Settings.Detection.GetRandomTarget().transform.position;
				return (position - position2).magnitude < this.m_MinFleeDistance;
			}
			return false;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000042DD File Offset: 0x000024DD
		public override void Activate(AIBrain brain)
		{
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x001B6F60 File Offset: 0x001B5160
		public override void OnUpdate(AIBrain brain)
		{
			Vector3 position = brain.transform.position;
			Vector3 position2 = brain.Settings.Detection.GetRandomTarget().transform.position;
			this.m_FleePosition = position - position2;
			this.m_FleePosition += brain.transform.position;
			brain.Settings.Movement.MoveTo(this.m_FleePosition, true);
			if (Time.time > this.m_LastTimeScreamed + this.m_ScreamInterval)
			{
				this.m_ScreamAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource, 1f);
				this.m_LastTimeScreamed = Time.time;
			}
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0002CE76 File Offset: 0x0002B076
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Run", false);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0002CD65 File Offset: 0x0002AF65
		public override bool StillValid(AIBrain brain)
		{
			return brain.Settings.Detection.HasTarget();
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0002CE8E File Offset: 0x0002B08E
		public override bool IsDone(AIBrain brain)
		{
			return !brain.Settings.Detection.HasTarget();
		}

		// Token: 0x0400384E RID: 14414
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_MinFleeDistance = 3f;

		// Token: 0x0400384F RID: 14415
		[SerializeField]
		private SoundPlayer m_ScreamAudio;

		// Token: 0x04003850 RID: 14416
		[SerializeField]
		[Clamp(0f, 15f)]
		private float m_ScreamInterval = 3f;

		// Token: 0x04003851 RID: 14417
		private Vector3 m_FleePosition;

		// Token: 0x04003852 RID: 14418
		private float m_LastTimeScreamed;
	}
}
