using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000679 RID: 1657
	public class Eat : Action
	{
		// Token: 0x060034A3 RID: 13475 RVA: 0x0016E790 File Offset: 0x0016C990
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 8;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Next To Food", true);
			base.Effects.Add("Is Hungry", false);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x0016E7DE File Offset: 0x0016C9DE
		public override void Activate(AIBrain brain)
		{
			this.ResetValues();
			brain.Settings.Animation.ToggleBool("Eat", true);
			this.m_EatStartTime = Time.time;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x0016E808 File Offset: 0x0016CA08
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

		// Token: 0x060034A6 RID: 13478 RVA: 0x0016E86C File Offset: 0x0016CA6C
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Eat", false);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x0016E884 File Offset: 0x0016CA84
		public override bool IsDone(AIBrain brain)
		{
			return this.m_IsDoneEating;
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x0016E88C File Offset: 0x0016CA8C
		public override void ResetValues()
		{
			this.m_IsDoneEating = false;
			this.m_EatStartTime = 0f;
		}

		// Token: 0x04002EA5 RID: 11941
		[SerializeField]
		[Tooltip("Determines the time it will take for the AI to eat.")]
		private float m_EatTime;

		// Token: 0x04002EA6 RID: 11942
		private bool m_IsDoneEating;

		// Token: 0x04002EA7 RID: 11943
		private float m_EatStartTime;
	}
}
