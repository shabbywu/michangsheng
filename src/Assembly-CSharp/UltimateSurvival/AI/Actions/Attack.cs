using System;
using UnityEngine;

namespace UltimateSurvival.AI.Actions
{
	// Token: 0x02000984 RID: 2436
	public class Attack : Action
	{
		// Token: 0x06003E42 RID: 15938 RVA: 0x001B6970 File Offset: 0x001B4B70
		public override void OnStart(AIBrain brain)
		{
			this.m_Priority = 10;
			this.m_RepeatType = ET.ActionRepeatType.Single;
			this.m_IsInterruptable = false;
			base.Preconditions.Add("Can Attack Player", true);
			base.Effects.Add("Is Player Dead", true);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0002CD65 File Offset: 0x0002AF65
		public override bool CanActivate(AIBrain brain)
		{
			return brain.Settings.Detection.HasTarget();
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0002CD77 File Offset: 0x0002AF77
		public override void Activate(AIBrain brain)
		{
			this.m_Target = brain.Settings.Detection.LastChasedTarget;
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x001B69C0 File Offset: 0x001B4BC0
		public override void OnUpdate(AIBrain brain)
		{
			if (Time.time > this.m_NextTimeCanAttack)
			{
				brain.Settings.Animation.SetTrigger("Attack");
				this.m_NextTimeCanAttack = Time.time + this.m_AttackInterval;
			}
			this.RotateTowards(brain.transform, this.m_Target.transform, 5f);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x001B6A20 File Offset: 0x001B4C20
		public override bool StillValid(AIBrain brain)
		{
			bool flag = Vector3.Distance(this.m_Target.transform.position, brain.transform.position) < this.m_MinAttackDistance;
			bool flag2 = Time.time > this.m_NextTimeCanAttack;
			return (brain.Settings.Detection.HasTarget() && flag) || !flag2;
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x0002CD8F File Offset: 0x0002AF8F
		public override bool IsDone(AIBrain brain)
		{
			return this.m_HasKilledPlayer && Time.time > this.m_NextTimeCanAttack;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x001B6A80 File Offset: 0x001B4C80
		private void RotateTowards(Transform transform, Transform target, float rotationSpeed)
		{
			Vector3 normalized = (target.position - transform.position).normalized;
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(normalized.x, 0f, normalized.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);
		}

		// Token: 0x04003841 RID: 14401
		[SerializeField]
		private float m_MinAttackDistance = 1.5f;

		// Token: 0x04003842 RID: 14402
		[SerializeField]
		private float m_AttackInterval = 0.8f;

		// Token: 0x04003843 RID: 14403
		private bool m_HasKilledPlayer;

		// Token: 0x04003844 RID: 14404
		private GameObject m_Target;

		// Token: 0x04003845 RID: 14405
		private float m_NextTimeCanAttack;
	}
}
