using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x0200067B RID: 1659
	public class Flee : Action
	{
		// Token: 0x060034AD RID: 13485 RVA: 0x0016E928 File Offset: 0x0016CB28
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 12;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Player in sight", true);
			base.Effects.Add("Player in sight", false);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x0016E978 File Offset: 0x0016CB78
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

		// Token: 0x060034AF RID: 13487 RVA: 0x00004095 File Offset: 0x00002295
		public override void Activate(AIBrain brain)
		{
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x0016E9D8 File Offset: 0x0016CBD8
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

		// Token: 0x060034B1 RID: 13489 RVA: 0x0016EA88 File Offset: 0x0016CC88
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Run", false);
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0016E2C7 File Offset: 0x0016C4C7
		public override bool StillValid(AIBrain brain)
		{
			return brain.Settings.Detection.HasTarget();
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x0016EAA0 File Offset: 0x0016CCA0
		public override bool IsDone(AIBrain brain)
		{
			return !brain.Settings.Detection.HasTarget();
		}

		// Token: 0x04002EA8 RID: 11944
		[SerializeField]
		[Clamp(0f, 100f)]
		private float m_MinFleeDistance = 3f;

		// Token: 0x04002EA9 RID: 11945
		[SerializeField]
		private SoundPlayer m_ScreamAudio;

		// Token: 0x04002EAA RID: 11946
		[SerializeField]
		[Clamp(0f, 15f)]
		private float m_ScreamInterval = 3f;

		// Token: 0x04002EAB RID: 11947
		private Vector3 m_FleePosition;

		// Token: 0x04002EAC RID: 11948
		private float m_LastTimeScreamed;
	}
}
