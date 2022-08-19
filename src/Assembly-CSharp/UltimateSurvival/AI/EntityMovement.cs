using System;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI
{
	// Token: 0x0200066D RID: 1645
	[Serializable]
	public class EntityMovement
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x0016DC29 File Offset: 0x0016BE29
		public Vector3 CurrentDestination
		{
			get
			{
				return this.m_CurrentDestination;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x0016DC31 File Offset: 0x0016BE31
		public ET.AIMovementState MovementState
		{
			get
			{
				return this.m_MovementState;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x0016DC39 File Offset: 0x0016BE39
		public NavMeshAgent Agent
		{
			get
			{
				return this.m_Agent;
			}
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x0016DC41 File Offset: 0x0016BE41
		public void Initialize(AIBrain brain)
		{
			this.m_Brain = brain;
			this.m_Agent = this.m_Brain.GetComponent<NavMeshAgent>();
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x0016DC5C File Offset: 0x0016BE5C
		public void Update(Transform transform)
		{
			Vector3 vector = this.m_Agent.nextPosition - transform.position;
			if (vector.magnitude > this.m_Agent.radius)
			{
				this.m_Agent.nextPosition = transform.position + 0.9f * vector;
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x0016DCB8 File Offset: 0x0016BEB8
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

		// Token: 0x0600345D RID: 13405 RVA: 0x0016DD45 File Offset: 0x0016BF45
		private void ChangeMovementState(float speed, string animName, bool animValue, ET.AIMovementState newState)
		{
			this.m_Agent.speed = speed;
			this.m_Brain.Settings.Animation.ToggleBool(animName, animValue);
			this.m_MovementState = newState;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x0016DD74 File Offset: 0x0016BF74
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

		// Token: 0x04002E88 RID: 11912
		[SerializeField]
		[Tooltip("Normal speed the agent will use.")]
		private float m_WalkSpeed;

		// Token: 0x04002E89 RID: 11913
		[Tooltip("Speed the agent will only use whenever an action requires it to hurry.")]
		[SerializeField]
		private float m_RunSpeed;

		// Token: 0x04002E8A RID: 11914
		private Vector3 m_CurrentDestination;

		// Token: 0x04002E8B RID: 11915
		private ET.AIMovementState m_MovementState;

		// Token: 0x04002E8C RID: 11916
		private AIBrain m_Brain;

		// Token: 0x04002E8D RID: 11917
		private NavMeshAgent m_Agent;
	}
}
