using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x0200067C RID: 1660
	[Serializable]
	public class Patrol : PointBased
	{
		// Token: 0x060034B5 RID: 13493 RVA: 0x0016EAD3 File Offset: 0x0016CCD3
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 0;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Repetitive;
			base.OnStart(brain);
			this.m_Brain = brain;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x0016EAF8 File Offset: 0x0016CCF8
		public override void Activate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], false);
			brain.AI.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSucceded_ChangeHealth));
			GameController.Audio.LastGunshot.AddChangeListener(new Action(this.OnChanged_LastGunshot));
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x0016EB60 File Offset: 0x0016CD60
		public override void OnUpdate(AIBrain brain)
		{
			if (this.m_SuspectedTarget != null)
			{
				if (brain.Settings.Movement.CurrentDestination != this.m_SuspectedTarget.Value)
				{
					brain.Settings.Movement.MoveTo(this.m_SuspectedTarget.Value, false);
				}
				if (brain.Settings.Movement.ReachedDestination(true))
				{
					this.m_SuspectedTarget = null;
				}
			}
			else if (brain.Settings.Movement.ReachedDestination(true))
			{
				if (!this.m_Waiting)
				{
					this.m_Waiting = true;
					this.m_NextMoveTime = Time.time + Random.Range(this.m_WaitTime.x, this.m_WaitTime.y);
				}
				if (Time.time > this.m_NextMoveTime)
				{
					base.ChangePatrolPoint();
					brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], this.m_IsUrgent);
					this.m_Waiting = false;
				}
			}
			if (Time.time > this.m_LastAudioPlayTime + this.m_AudioPlayInterval)
			{
				this.m_LastAudioPlayTime = Time.time;
				this.m_Audio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource, 1f);
			}
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x0016ECA8 File Offset: 0x0016CEA8
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Walk", false);
			brain.Settings.Animation.ToggleBool("Run", false);
			brain.AI.ChangeHealth.RemoveListener(new Action<HealthEventData>(this.OnSucceded_ChangeHealth));
			GameController.Audio.LastGunshot.RemoveChangeListener(new Action(this.OnChanged_LastGunshot));
			brain.Settings.AudioSource.Stop();
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool IsDone(AIBrain brain)
		{
			return false;
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x0016ED28 File Offset: 0x0016CF28
		private void OnSucceded_ChangeHealth(HealthEventData data)
		{
			if (data.Damager != null)
			{
				this.m_SuspectedTarget = new Vector3?(data.Damager.transform.position);
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x0016ED54 File Offset: 0x0016CF54
		private void OnChanged_LastGunshot()
		{
			if (this.m_Brain == null)
			{
				return;
			}
			Gunshot gunshot = GameController.Audio.LastGunshot.Get();
			if ((gunshot.Position - this.m_Brain.transform.position).sqrMagnitude < (float)(this.m_Brain.Settings.Detection.HearRange * this.m_Brain.Settings.Detection.HearRange))
			{
				this.m_SuspectedTarget = new Vector3?(gunshot.Position);
			}
		}

		// Token: 0x04002EAD RID: 11949
		[SerializeField]
		private Vector2 m_WaitTime = new Vector2(8f, 15f);

		// Token: 0x04002EAE RID: 11950
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_Audio;

		// Token: 0x04002EAF RID: 11951
		[SerializeField]
		[Clamp(0f, 15f)]
		private float m_AudioPlayInterval = 1f;

		// Token: 0x04002EB0 RID: 11952
		private float m_NextMoveTime;

		// Token: 0x04002EB1 RID: 11953
		private bool m_Waiting;

		// Token: 0x04002EB2 RID: 11954
		private Vector3? m_SuspectedTarget = new Vector3?(Vector3.zero);

		// Token: 0x04002EB3 RID: 11955
		private AIBrain m_Brain;

		// Token: 0x04002EB4 RID: 11956
		private float m_LastAudioPlayTime;
	}
}
