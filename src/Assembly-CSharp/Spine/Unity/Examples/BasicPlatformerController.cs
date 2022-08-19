using System;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADA RID: 2778
	[RequireComponent(typeof(CharacterController))]
	public class BasicPlatformerController : MonoBehaviour
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06004DD1 RID: 19921 RVA: 0x00214248 File Offset: 0x00212448
		// (remove) Token: 0x06004DD2 RID: 19922 RVA: 0x00214280 File Offset: 0x00212480
		public event UnityAction OnJump;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06004DD3 RID: 19923 RVA: 0x002142B8 File Offset: 0x002124B8
		// (remove) Token: 0x06004DD4 RID: 19924 RVA: 0x002142F0 File Offset: 0x002124F0
		public event UnityAction OnLand;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06004DD5 RID: 19925 RVA: 0x00214328 File Offset: 0x00212528
		// (remove) Token: 0x06004DD6 RID: 19926 RVA: 0x00214360 File Offset: 0x00212560
		public event UnityAction OnHardLand;

		// Token: 0x06004DD7 RID: 19927 RVA: 0x00214398 File Offset: 0x00212598
		private void Update()
		{
			float deltaTime = Time.deltaTime;
			bool isGrounded = this.controller.isGrounded;
			bool flag = !this.wasGrounded && isGrounded;
			this.input.x = Input.GetAxis(this.XAxis);
			this.input.y = Input.GetAxis(this.YAxis);
			bool buttonUp = Input.GetButtonUp(this.JumpButton);
			bool buttonDown = Input.GetButtonDown(this.JumpButton);
			bool flag2 = (isGrounded && this.input.y < -0.5f) || this.forceCrouchEndTime > Time.time;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (flag && -this.velocity.y > this.forceCrouchVelocity)
			{
				flag5 = true;
				flag2 = true;
				this.forceCrouchEndTime = Time.time + this.forceCrouchDuration;
			}
			if (!flag2)
			{
				if (isGrounded)
				{
					if (buttonDown)
					{
						flag4 = true;
					}
				}
				else
				{
					flag3 = (buttonUp && Time.time < this.minimumJumpEndTime);
				}
			}
			Vector3 vector = Physics.gravity * this.gravityScale * deltaTime;
			if (flag4)
			{
				this.velocity.y = this.jumpSpeed;
				this.minimumJumpEndTime = Time.time + this.minimumJumpDuration;
			}
			else if (flag3 && this.velocity.y > 0f)
			{
				this.velocity.y = this.velocity.y * this.jumpInterruptFactor;
			}
			this.velocity.x = 0f;
			if (!flag2 && this.input.x != 0f)
			{
				this.velocity.x = ((Mathf.Abs(this.input.x) > 0.6f) ? this.runSpeed : this.walkSpeed);
				this.velocity.x = this.velocity.x * Mathf.Sign(this.input.x);
			}
			if (!isGrounded)
			{
				if (this.wasGrounded)
				{
					if (this.velocity.y < 0f)
					{
						this.velocity.y = 0f;
					}
				}
				else
				{
					this.velocity += vector;
				}
			}
			this.controller.Move(this.velocity * deltaTime);
			this.wasGrounded = isGrounded;
			if (isGrounded)
			{
				if (flag2)
				{
					this.currentState = BasicPlatformerController.CharacterState.Crouch;
				}
				else if (this.input.x == 0f)
				{
					this.currentState = BasicPlatformerController.CharacterState.Idle;
				}
				else
				{
					this.currentState = ((Mathf.Abs(this.input.x) > 0.6f) ? BasicPlatformerController.CharacterState.Run : BasicPlatformerController.CharacterState.Walk);
				}
			}
			else
			{
				this.currentState = ((this.velocity.y > 0f) ? BasicPlatformerController.CharacterState.Rise : BasicPlatformerController.CharacterState.Fall);
			}
			bool flag6 = this.previousState != this.currentState;
			this.previousState = this.currentState;
			if (flag6)
			{
				this.HandleStateChanged();
			}
			if (this.input.x != 0f)
			{
				this.animationHandle.SetFlip(this.input.x);
			}
			if (flag4)
			{
				this.OnJump.Invoke();
			}
			if (flag)
			{
				if (flag5)
				{
					this.OnHardLand.Invoke();
					return;
				}
				this.OnLand.Invoke();
			}
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x002146B0 File Offset: 0x002128B0
		private void HandleStateChanged()
		{
			string stateShortName = null;
			switch (this.currentState)
			{
			case BasicPlatformerController.CharacterState.Idle:
				stateShortName = "idle";
				break;
			case BasicPlatformerController.CharacterState.Walk:
				stateShortName = "walk";
				break;
			case BasicPlatformerController.CharacterState.Run:
				stateShortName = "run";
				break;
			case BasicPlatformerController.CharacterState.Crouch:
				stateShortName = "crouch";
				break;
			case BasicPlatformerController.CharacterState.Rise:
				stateShortName = "rise";
				break;
			case BasicPlatformerController.CharacterState.Fall:
				stateShortName = "fall";
				break;
			case BasicPlatformerController.CharacterState.Attack:
				stateShortName = "attack";
				break;
			}
			this.animationHandle.PlayAnimationForState(stateShortName, 0);
		}

		// Token: 0x04004D0A RID: 19722
		[Header("Components")]
		public CharacterController controller;

		// Token: 0x04004D0B RID: 19723
		[Header("Controls")]
		public string XAxis = "Horizontal";

		// Token: 0x04004D0C RID: 19724
		public string YAxis = "Vertical";

		// Token: 0x04004D0D RID: 19725
		public string JumpButton = "Jump";

		// Token: 0x04004D0E RID: 19726
		[Header("Moving")]
		public float walkSpeed = 1.5f;

		// Token: 0x04004D0F RID: 19727
		public float runSpeed = 7f;

		// Token: 0x04004D10 RID: 19728
		public float gravityScale = 6.6f;

		// Token: 0x04004D11 RID: 19729
		[Header("Jumping")]
		public float jumpSpeed = 25f;

		// Token: 0x04004D12 RID: 19730
		public float minimumJumpDuration = 0.5f;

		// Token: 0x04004D13 RID: 19731
		public float jumpInterruptFactor = 0.5f;

		// Token: 0x04004D14 RID: 19732
		public float forceCrouchVelocity = 25f;

		// Token: 0x04004D15 RID: 19733
		public float forceCrouchDuration = 0.5f;

		// Token: 0x04004D16 RID: 19734
		[Header("Animation")]
		public SkeletonAnimationHandleExample animationHandle;

		// Token: 0x04004D1A RID: 19738
		private Vector2 input;

		// Token: 0x04004D1B RID: 19739
		private Vector3 velocity;

		// Token: 0x04004D1C RID: 19740
		private float minimumJumpEndTime;

		// Token: 0x04004D1D RID: 19741
		private float forceCrouchEndTime;

		// Token: 0x04004D1E RID: 19742
		private bool wasGrounded;

		// Token: 0x04004D1F RID: 19743
		private BasicPlatformerController.CharacterState previousState;

		// Token: 0x04004D20 RID: 19744
		private BasicPlatformerController.CharacterState currentState;

		// Token: 0x020015BE RID: 5566
		public enum CharacterState
		{
			// Token: 0x04007049 RID: 28745
			None,
			// Token: 0x0400704A RID: 28746
			Idle,
			// Token: 0x0400704B RID: 28747
			Walk,
			// Token: 0x0400704C RID: 28748
			Run,
			// Token: 0x0400704D RID: 28749
			Crouch,
			// Token: 0x0400704E RID: 28750
			Rise,
			// Token: 0x0400704F RID: 28751
			Fall,
			// Token: 0x04007050 RID: 28752
			Attack
		}
	}
}
