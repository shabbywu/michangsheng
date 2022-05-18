using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000986 RID: 2438
	public class Eat : Action
	{
		// Token: 0x06003E53 RID: 15955 RVA: 0x001B6DA0 File Offset: 0x001B4FA0
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 8;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Next To Food", true);
			base.Effects.Add("Is Hungry", false);
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0002CDEC File Offset: 0x0002AFEC
		public override void Activate(AIBrain brain)
		{
			this.ResetValues();
			brain.Settings.Animation.ToggleBool("Eat", true);
			this.m_EatStartTime = Time.time;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x001B6DF0 File Offset: 0x001B4FF0
		public override void OnUpdate(AIBrain brain)
		{
			if (Time.time - this.m_EatStartTime > this.m_EatTime)
			{
				brain.Settings.Vitals.LastTimeFed = Time.time;
				brain.Settings.Vitals.IsHungry = false;
				this.m_IsDoneEating = true;
				brain.Settings.Animation.ToggleBool("Eat", false);
			}
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0002CE15 File Offset: 0x0002B015
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Eat", false);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0002CE2D File Offset: 0x0002B02D
		public override bool IsDone(AIBrain brain)
		{
			return this.m_IsDoneEating;
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x0002CE35 File Offset: 0x0002B035
		public override void ResetValues()
		{
			this.m_IsDoneEating = false;
			this.m_EatStartTime = 0f;
		}

		// Token: 0x0400384B RID: 14411
		[SerializeField]
		[Tooltip("Determines the time it will take for the AI to eat.")]
		private float m_EatTime;

		// Token: 0x0400384C RID: 14412
		private bool m_IsDoneEating;

		// Token: 0x0400384D RID: 14413
		private float m_EatStartTime;
	}
}
