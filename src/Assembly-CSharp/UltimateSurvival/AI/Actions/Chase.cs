using System;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000985 RID: 2437
	public class Chase : Action
	{
		// Token: 0x06003E4A RID: 15946 RVA: 0x001B6ADC File Offset: 0x001B4CDC
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 1;
			this.m_IsInterruptable = false;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Player in sight", true);
			base.Effects.Add("Can Attack Player", true);
			this.ResetValues();
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x001B6B30 File Offset: 0x001B4D30
		public override bool CanActivate(AIBrain brain)
		{
			if (!brain.Settings.Detection.HasTarget())
			{
				return false;
			}
			this.m_Target = brain.Settings.Detection.GetRandomTarget();
			brain.Settings.Detection.LastChasedTarget = this.m_Target.gameObject;
			return true;
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001B6B84 File Offset: 0x001B4D84
		public override void Activate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_Target.position, true);
			if (Vector3.Distance(brain.transform.position, this.m_Target.position) > 3.5f)
			{
				this.m_InitialScreamAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource, 1f);
			}
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x001B6BEC File Offset: 0x001B4DEC
		public override void OnUpdate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_Target.position, true);
			NavMeshAgent agent = brain.Settings.Movement.Agent;
			if (agent.velocity.sqrMagnitude < 0.01f)
			{
				brain.Settings.Animation.ToggleBool("Run", false);
				agent.updateRotation = false;
				this.RotateTowards(agent.transform, this.m_Target, 5f);
				return;
			}
			agent.updateRotation = true;
			brain.Settings.Animation.ToggleBool("Run", true);
			if (Time.time > this.m_NextScreamTime)
			{
				this.m_ScreamingAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource, 1f);
				this.m_NextScreamTime = Time.time + Random.Range(this.m_ScreamInterval.x, this.m_ScreamInterval.y);
			}
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x001B6CE0 File Offset: 0x001B4EE0
		public override bool StillValid(AIBrain brain)
		{
			bool flag = brain.Settings.Detection.HasTarget();
			if (!flag)
			{
				brain.Settings.Animation.ToggleBool("Run", false);
				brain.Settings.Movement.Agent.updateRotation = true;
			}
			return flag;
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x001B6D2C File Offset: 0x001B4F2C
		public override bool IsDone(AIBrain brain)
		{
			return brain.Settings.Movement.ReachedDestination(true) && (brain.transform.position - this.m_Target.position).sqrMagnitude < brain.Settings.Movement.Agent.stoppingDistance * brain.Settings.Movement.Agent.stoppingDistance;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x0002CDC6 File Offset: 0x0002AFC6
		public override void ResetValues()
		{
			this.m_Target = null;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x001B6A80 File Offset: 0x001B4C80
		private void RotateTowards(Transform transform, Transform target, float rotationSpeed)
		{
			Vector3 normalized = (target.position - transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(normalized.x, 0f, normalized.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);
		}

		// Token: 0x04003846 RID: 14406
		[SerializeField]
		private SoundPlayer m_InitialScreamAudio;

		// Token: 0x04003847 RID: 14407
		[SerializeField]
		private SoundPlayer m_ScreamingAudio;

		// Token: 0x04003848 RID: 14408
		[SerializeField]
		private Vector2 m_ScreamInterval = new Vector2(0.7f, 1.2f);

		// Token: 0x04003849 RID: 14409
		private Transform m_Target;

		// Token: 0x0400384A RID: 14410
		private float m_NextScreamTime;
	}
}
