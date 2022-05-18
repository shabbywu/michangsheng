using System;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E22 RID: 3618
	[RequireComponent(typeof(CharacterController))]
	public class BasicPlatformerController : MonoBehaviour
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06005735 RID: 22325 RVA: 0x002441BC File Offset: 0x002423BC
		// (remove) Token: 0x06005736 RID: 22326 RVA: 0x002441F4 File Offset: 0x002423F4
		public event UnityAction OnJump;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06005737 RID: 22327 RVA: 0x0024422C File Offset: 0x0024242C
		// (remove) Token: 0x06005738 RID: 22328 RVA: 0x00244264 File Offset: 0x00242464
		public event UnityAction OnLand;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06005739 RID: 22329 RVA: 0x0024429C File Offset: 0x0024249C
		// (remove) Token: 0x0600573A RID: 22330 RVA: 0x002442D4 File Offset: 0x002424D4
		public event UnityAction OnHardLand;

		// Token: 0x0600573B RID: 22331 RVA: 0x0024430C File Offset: 0x0024250C
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

		// Token: 0x0600573C RID: 22332 RVA: 0x00244624 File Offset: 0x00242824
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

		// Token: 0x040056FB RID: 22267
		[Header("Components")]
		public CharacterController controller;

		// Token: 0x040056FC RID: 22268
		[Header("Controls")]
		public string XAxis = "Horizontal";

		// Token: 0x040056FD RID: 22269
		public string YAxis = "Vertical";

		// Token: 0x040056FE RID: 22270
		public string JumpButton = "Jump";

		// Token: 0x040056FF RID: 22271
		[Header("Moving")]
		public float walkSpeed = 1.5f;

		// Token: 0x04005700 RID: 22272
		public float runSpeed = 7f;

		// Token: 0x04005701 RID: 22273
		public float gravityScale = 6.6f;

		// Token: 0x04005702 RID: 22274
		[Header("Jumping")]
		public float jumpSpeed = 25f;

		// Token: 0x04005703 RID: 22275
		public float minimumJumpDuration = 0.5f;

		// Token: 0x04005704 RID: 22276
		public float jumpInterruptFactor = 0.5f;

		// Token: 0x04005705 RID: 22277
		public float forceCrouchVelocity = 25f;

		// Token: 0x04005706 RID: 22278
		public float forceCrouchDuration = 0.5f;

		// Token: 0x04005707 RID: 22279
		[Header("Animation")]
		public SkeletonAnimationHandleExample animationHandle;

		// Token: 0x0400570B RID: 22283
		private Vector2 input;

		// Token: 0x0400570C RID: 22284
		private Vector3 velocity;

		// Token: 0x0400570D RID: 22285
		private float minimumJumpEndTime;

		// Token: 0x0400570E RID: 22286
		private float forceCrouchEndTime;

		// Token: 0x0400570F RID: 22287
		private bool wasGrounded;

		// Token: 0x04005710 RID: 22288
		private BasicPlatformerController.CharacterState previousState;

		// Token: 0x04005711 RID: 22289
		private BasicPlatformerController.CharacterState currentState;

		// Token: 0x02000E23 RID: 3619
		public enum CharacterState
		{
			// Token: 0x04005713 RID: 22291
			None,
			// Token: 0x04005714 RID: 22292
			Idle,
			// Token: 0x04005715 RID: 22293
			Walk,
			// Token: 0x04005716 RID: 22294
			Run,
			// Token: 0x04005717 RID: 22295
			Crouch,
			// Token: 0x04005718 RID: 22296
			Rise,
			// Token: 0x04005719 RID: 22297
			Fall,
			// Token: 0x0400571A RID: 22298
			Attack
		}
	}
}
