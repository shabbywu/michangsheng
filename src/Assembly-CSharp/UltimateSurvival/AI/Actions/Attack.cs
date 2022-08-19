using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000677 RID: 1655
	public class Attack : Action
	{
		// Token: 0x06003492 RID: 13458 RVA: 0x0016E278 File Offset: 0x0016C478
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 10;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			this.m_IsInterruptable = false;
			base.Preconditions.Add("Can Attack Player", true);
			base.Effects.Add("Is Player Dead", true);
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x0016E2C7 File Offset: 0x0016C4C7
		public override bool CanActivate(AIBrain brain)
		{
			return brain.Settings.Detection.HasTarget();
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0016E2D9 File Offset: 0x0016C4D9
		public override void Activate(AIBrain brain)
		{
			this.m_Target = brain.Settings.Detection.LastChasedTarget;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0016E2F4 File Offset: 0x0016C4F4
		public override void OnUpdate(AIBrain brain)
		{
			if (Time.time > this.m_NextTimeCanAttack)
			{
				brain.Settings.Animation.SetTrigger("Attack");
				this.m_NextTimeCanAttack = Time.time + this.m_AttackInterval;
			}
			this.RotateTowards(brain.transform, this.m_Target.transform, 5f);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x0016E354 File Offset: 0x0016C554
		public override bool StillValid(AIBrain brain)
		{
			bool flag = Vector3.Distance(this.m_Target.transform.position, brain.transform.position) < this.m_MinAttackDistance;
			bool flag2 = Time.time > this.m_NextTimeCanAttack;
			return (brain.Settings.Detection.HasTarget() && flag) || !flag2;
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x0016E3B2 File Offset: 0x0016C5B2
		public override bool IsDone(AIBrain brain)
		{
			return this.m_HasKilledPlayer && Time.time > this.m_NextTimeCanAttack;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x0016E3CC File Offset: 0x0016C5CC
		private void RotateTowards(Transform transform, Transform target, float rotationSpeed)
		{
			Vector3 normalized = (target.position - transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(normalized.x, 0f, normalized.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);
		}

		// Token: 0x04002E9B RID: 11931
		[SerializeField]
		private float m_MinAttackDistance = 1.5f;

		// Token: 0x04002E9C RID: 11932
		[SerializeField]
		private float m_AttackInterval = 0.8f;

		// Token: 0x04002E9D RID: 11933
		private bool m_HasKilledPlayer;

		// Token: 0x04002E9E RID: 11934
		private GameObject m_Target;

		// Token: 0x04002E9F RID: 11935
		private float m_NextTimeCanAttack;
	}
}
