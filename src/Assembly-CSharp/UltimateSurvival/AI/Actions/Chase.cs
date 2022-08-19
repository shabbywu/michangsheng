using System;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000678 RID: 1656
	public class Chase : Action
	{
		// Token: 0x0600349A RID: 13466 RVA: 0x0016E448 File Offset: 0x0016C648
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 1;
			this.m_IsInterruptable = false;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			base.Preconditions.Add("Player in sight", true);
			base.Effects.Add("Can Attack Player", true);
			this.ResetValues();
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0016E49C File Offset: 0x0016C69C
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

		// Token: 0x0600349C RID: 13468 RVA: 0x0016E4F0 File Offset: 0x0016C6F0
		public override void Activate(AIBrain brain)
		{
			brain.Settings.Movement.MoveTo(this.m_Target.position, true);
			if (Vector3.Distance(brain.transform.position, this.m_Target.position) > 3.5f)
			{
				this.m_InitialScreamAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource, 1f);
			}
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x0016E558 File Offset: 0x0016C758
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

		// Token: 0x0600349E RID: 13470 RVA: 0x0016E64C File Offset: 0x0016C84C
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

		// Token: 0x0600349F RID: 13471 RVA: 0x0016E698 File Offset: 0x0016C898
		public override bool IsDone(AIBrain brain)
		{
			return brain.Settings.Movement.ReachedDestination(true) && (brain.transform.position - this.m_Target.position).sqrMagnitude < brain.Settings.Movement.Agent.stoppingDistance * brain.Settings.Movement.Agent.stoppingDistance;
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x0016E70A File Offset: 0x0016C90A
		public override void ResetValues()
		{
			this.m_Target = null;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x0016E714 File Offset: 0x0016C914
		private void RotateTowards(Transform transform, Transform target, float rotationSpeed)
		{
			Vector3 normalized = (target.position - transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(normalized.x, 0f, normalized.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);
		}

		// Token: 0x04002EA0 RID: 11936
		[SerializeField]
		private SoundPlayer m_InitialScreamAudio;

		// Token: 0x04002EA1 RID: 11937
		[SerializeField]
		private SoundPlayer m_ScreamingAudio;

		// Token: 0x04002EA2 RID: 11938
		[SerializeField]
		private Vector2 m_ScreamInterval = new Vector2(0.7f, 1.2f);

		// Token: 0x04002EA3 RID: 11939
		private Transform m_Target;

		// Token: 0x04002EA4 RID: 11940
		private float m_NextScreamTime;
	}
}
