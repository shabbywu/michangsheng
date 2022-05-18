using System;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI
{
	// Token: 0x02000978 RID: 2424
	[Serializable]
	public class EntityMovement
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x0002CAC0 File Offset: 0x0002ACC0
		public Vector3 CurrentDestination
		{
			get
			{
				return this.m_CurrentDestination;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		public ET.AIMovementState MovementState
		{
			get
			{
				return this.m_MovementState;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		public NavMeshAgent Agent
		{
			get
			{
				return this.m_Agent;
			}
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		public void Initialize(AIBrain brain)
		{
			this.m_Brain = brain;
			this.m_Agent = this.m_Brain.GetComponent<NavMeshAgent>();
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x001B65C0 File Offset: 0x001B47C0
		public void Update(Transform transform)
		{
			Vector3 vector = this.m_Agent.nextPosition - transform.position;
			if (vector.magnitude > this.m_Agent.radius)
			{
				this.m_Agent.nextPosition = transform.position + 0.9f * vector;
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x001B661C File Offset: 0x001B481C
		public NavMeshPath MoveTo(Vector3 position, bool fastMove = false)
		{
			NavMeshPath result = new NavMeshPath();
			this.m_Agent.SetDestination(position);
			this.m_CurrentDestination = position;
			bool flag = fastMove && this.m_Brain.Settings.Animation.ParameterExists("Run");
			bool flag2 = this.m_Brain.Settings.Animation.ParameterExists("Walk");
			if (flag)
			{
				this.ChangeMovementState(this.m_RunSpeed, "Run", true, ET.AIMovementState.Running);
				return result;
			}
			if (flag2)
			{
				this.ChangeMovementState(this.m_WalkSpeed, "Walk", true, ET.AIMovementState.Walking);
			}
			return result;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x0002CAF2 File Offset: 0x0002ACF2
		private void ChangeMovementState(float speed, string animName, bool animValue, ET.AIMovementState newState)
		{
			this.m_Agent.speed = speed;
			this.m_Brain.Settings.Animation.ToggleBool(animName, animValue);
			this.m_MovementState = newState;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x001B66AC File Offset: 0x001B48AC
		public bool ReachedDestination(bool isStop = true)
		{
			if (this.m_Agent.remainingDistance <= this.m_Agent.stoppingDistance)
			{
				if (isStop)
				{
					string paramName = (this.m_MovementState == ET.AIMovementState.Running) ? "Run" : "Walk";
					this.m_Brain.Settings.Animation.ToggleBool(paramName, false);
					this.m_MovementState = ET.AIMovementState.Idle;
				}
				return true;
			}
			return false;
		}

		// Token: 0x04003823 RID: 14371
		[SerializeField]
		[Tooltip("Normal speed the agent will use.")]
		private float m_WalkSpeed;

		// Token: 0x04003824 RID: 14372
		[Tooltip("Speed the agent will only use whenever an action requires it to hurry.")]
		[SerializeField]
		private float m_RunSpeed;

		// Token: 0x04003825 RID: 14373
		private Vector3 m_CurrentDestination;

		// Token: 0x04003826 RID: 14374
		private ET.AIMovementState m_MovementState;

		// Token: 0x04003827 RID: 14375
		private AIBrain m_Brain;

		// Token: 0x04003828 RID: 14376
		private NavMeshAgent m_Agent;
	}
}
