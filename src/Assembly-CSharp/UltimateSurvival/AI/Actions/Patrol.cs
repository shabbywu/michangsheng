using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000989 RID: 2441
	[Serializable]
	public class Patrol : PointBased
	{
		// Token: 0x06003E65 RID: 15973 RVA: 0x0002CEC1 File Offset: 0x0002B0C1
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 0;
			this.m_IsInterruptable = true;
			this.m_RepeatType = ET.ActionRepeatType.Repetitive;
			base.OnStart(brain);
			this.m_Brain = brain;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x001B7010 File Offset: 0x001B5210
		public override void Activate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_PointPositions[this.m_CurrentIndex], false);
			brain.AI.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSucceded_ChangeHealth));
			GameController.Audio.LastGunshot.AddChangeListener(new Action(this.OnChanged_LastGunshot));
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x001B7078 File Offset: 0x001B5278
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

		// Token: 0x06003E68 RID: 15976 RVA: 0x001B71C0 File Offset: 0x001B53C0
		public override void OnDeactivation(AIBrain brain)
		{
			brain.Settings.Animation.ToggleBool("Walk", false);
			brain.Settings.Animation.ToggleBool("Run", false);
			brain.AI.ChangeHealth.RemoveListener(new Action<HealthEventData>(this.OnSucceded_ChangeHealth));
			GameController.Audio.LastGunshot.RemoveChangeListener(new Action(this.OnChanged_LastGunshot));
			brain.Settings.AudioSource.Stop();
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00004050 File Offset: 0x00002250
		public override bool IsDone(AIBrain brain)
		{
			return false;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0002CEE6 File Offset: 0x0002B0E6
		private void OnSucceded_ChangeHealth(HealthEventData data)
		{
			if (data.Damager != null)
			{
				this.m_SuspectedTarget = new Vector3?(data.Damager.transform.position);
			}
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x001B7240 File Offset: 0x001B5440
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

		// Token: 0x04003853 RID: 14419
		[SerializeField]
		private Vector2 m_WaitTime = new Vector2(8f, 15f);

		// Token: 0x04003854 RID: 14420
		[Header("Audio")]
		[SerializeField]
		private SoundPlayer m_Audio;

		// Token: 0x04003855 RID: 14421
		[SerializeField]
		[Clamp(0f, 15f)]
		private float m_AudioPlayInterval = 1f;

		// Token: 0x04003856 RID: 14422
		private float m_NextMoveTime;

		// Token: 0x04003857 RID: 14423
		private bool m_Waiting;

		// Token: 0x04003858 RID: 14424
		private Vector3? m_SuspectedTarget = new Vector3?(Vector3.zero);

		// Token: 0x04003859 RID: 14425
		private AIBrain m_Brain;

		// Token: 0x0400385A RID: 14426
		private float m_LastAudioPlayTime;
	}
}
