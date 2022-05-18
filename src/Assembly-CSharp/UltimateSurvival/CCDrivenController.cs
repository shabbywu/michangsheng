using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008EE RID: 2286
	[RequireComponent(typeof(CharacterController))]
	public class CCDrivenController : PlayerBehaviour
	{
		// Token: 0x06003A9F RID: 15007 RVA: 0x001A8ED0 File Offset: 0x001A70D0
		private void Start()
		{
			this.m_Controller = base.GetComponent<CharacterController>();
			this.m_UncrouchedHeight = this.m_Controller.height;
			base.Player.Jump.AddStartTryer(new TryerDelegate(this.TryStart_Jump));
			base.Player.Run.AddStartTryer(new TryerDelegate(this.TryStart_Run));
			base.Player.Crouch.AddStartTryer(new TryerDelegate(this.TryStart_Crouch));
			base.Player.Crouch.AddStopTryer(new TryerDelegate(this.TryStop_Crouch));
			base.Player.Sleep.AddStopListener(delegate
			{
				this.m_CurrentVelocity = Vector3.zero;
			});
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x001A8F88 File Offset: 0x001A7188
		private void Update()
		{
			this.m_LastCollisionFlags = this.m_Controller.Move(this.m_CurrentVelocity * Time.deltaTime);
			if ((this.m_LastCollisionFlags & 4) == 4 && !this.m_PreviouslyGrounded)
			{
				if (base.Player.Jump.Active)
				{
					base.Player.Jump.ForceStop();
				}
				base.Player.Land.Send(Mathf.Abs(base.Player.Velocity.Get().y));
			}
			base.Player.IsGrounded.Set(this.m_Controller.isGrounded);
			base.Player.Velocity.Set(this.m_Controller.velocity);
			if (base.Player.NearLadders.Count > 0)
			{
				if (base.Player.Walk.Active)
				{
					base.Player.Walk.ForceStop();
				}
				if (base.Player.Run.Active)
				{
					base.Player.Run.ForceStop();
				}
				if (base.Player.Crouch.Active)
				{
					base.Player.Crouch.TryStop();
				}
				if (base.Player.Jump.Active && this.m_JumpedFrom == CCDrivenController.JumpedFrom.Ground)
				{
					base.Player.Jump.ForceStop();
				}
				this.UpdateLadderMovement();
			}
			else if (!this.m_Controller.isGrounded)
			{
				if (base.Player.Walk.Active)
				{
					base.Player.Walk.ForceStop();
				}
				if (base.Player.Run.Active)
				{
					base.Player.Run.ForceStop();
				}
				this.UpdateFalling();
			}
			else if (!base.Player.Jump.Active)
			{
				this.UpdateMovement();
			}
			this.m_PreviouslyGrounded = this.m_Controller.isGrounded;
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x001A917C File Offset: 0x001A737C
		private void UpdateLadderMovement()
		{
			Vector2 vector = Vector2.ClampMagnitude(base.Player.MovementInput.Get(), 1f);
			if (MonoSingleton<InventoryController>.Instance.IsClosed)
			{
				Transform transform = base.Player.NearLadders.Peek();
				Vector3 vector2 = base.Player.LookDirection.Get();
				bool flag = Vector3.Dot(transform.forward, vector2) < 0f;
				bool flag2 = (vector.y < 0f && !flag) || (vector.y > 0f && flag);
				if (base.Player.IsGrounded.Get() && flag2)
				{
					this.m_DesiredVelocity = base.transform.forward * vector.y * 3f;
				}
				else
				{
					Vector3 vector3 = transform.right * vector.x * this.m_SpeedOnLadder / 2f;
					if (flag)
					{
						vector3 *= -1f;
					}
					Vector3 vector4 = vector2 * vector.y * this.m_SpeedOnLadder;
					Vector3 vector5 = vector3 + vector4;
					Vector3 vector6 = Vector3.ProjectOnPlane(vector5, transform.right);
					Vector3 vector7 = transform.up * Mathf.Sign(vector5.y);
					vector5 = Quaternion.FromToRotation(vector6, vector7) * vector5;
					if (vector.sqrMagnitude > 0f)
					{
						vector5 += transform.forward;
					}
					this.m_DesiredVelocity = vector5;
				}
			}
			else
			{
				this.m_DesiredVelocity = Vector3.zero;
			}
			float num = (this.m_DesiredVelocity.sqrMagnitude > 0f) ? this.m_Acceleration : this.m_Damping;
			this.m_CurrentVelocity = Vector3.Lerp(this.m_CurrentVelocity, this.m_DesiredVelocity, num * Time.deltaTime);
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x001A9358 File Offset: 0x001A7558
		private void UpdateMovement()
		{
			this.CalculateDesiredVelocity();
			Vector3 vector = MonoSingleton<InventoryController>.Instance.IsClosed ? this.m_DesiredVelocity : Vector3.zero;
			float num = Vector3.Angle(Vector3.up, this.m_LastSurfaceNormal);
			vector *= this.m_SlopeMultiplier.Evaluate(num / this.m_Controller.slopeLimit);
			float num2 = (vector.sqrMagnitude > 0f) ? this.m_Acceleration : this.m_Damping;
			this.m_CurrentVelocity = Vector3.Lerp(this.m_CurrentVelocity, vector, num2 * Time.deltaTime);
			if (!base.Player.Walk.Active && vector.sqrMagnitude > 0.1f && !base.Player.Run.Active)
			{
				base.Player.Walk.ForceStart();
			}
			else if (base.Player.Walk.Active && (vector.sqrMagnitude < 0.1f || base.Player.Run.Active))
			{
				base.Player.Walk.ForceStop();
			}
			if (base.Player.Run.Active)
			{
				if (base.Player.Stamina.Is(0f))
				{
					base.Player.Run.ForceStop();
				}
				else if (base.Player.MovementInput.Get().y < 0f || this.m_DesiredSpeed == 0f)
				{
					base.Player.Run.ForceStop();
				}
			}
			if (this.m_EnableSliding)
			{
				if (num > this.m_SlideLimit)
				{
					Vector3 vector2 = this.m_LastSurfaceNormal + Vector3.down;
					this.m_SlideVelocity += vector2 * this.m_SlidingSpeed * Time.deltaTime;
				}
				else
				{
					this.m_SlideVelocity = Vector3.Lerp(this.m_SlideVelocity, Vector3.zero, Time.deltaTime * 10f);
				}
				this.m_CurrentVelocity += this.m_SlideVelocity;
			}
			if (!base.Player.Jump.Active)
			{
				this.m_CurrentVelocity.y = -this.m_AntiBumpFactor;
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x001A9594 File Offset: 0x001A7794
		private void UpdateFalling()
		{
			if (this.m_PreviouslyGrounded && !base.Player.Jump.Active)
			{
				this.m_CurrentVelocity.y = 0f;
			}
			this.m_CurrentVelocity += this.m_DesiredVelocity * this.m_Acceleration * this.m_AirControl * Time.deltaTime;
			this.m_CurrentVelocity.y = this.m_CurrentVelocity.y - this.m_Gravity * Time.deltaTime;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x001A9620 File Offset: 0x001A7820
		private void CalculateDesiredVelocity()
		{
			Vector2 vector = Vector2.ClampMagnitude(base.Player.MovementInput.Get(), 1f);
			bool flag = vector.sqrMagnitude > 0f;
			Vector3 vector2 = flag ? base.transform.TransformDirection(new Vector3(vector.x, 0f, vector.y)) : this.m_Controller.velocity.normalized;
			this.m_DesiredSpeed = 0f;
			if (flag)
			{
				this.m_DesiredSpeed = this.m_ForwardSpeed * base.Player.MovementSpeedFactor.Get();
				if (Mathf.Abs(vector.x) > 0f)
				{
					this.m_DesiredSpeed = this.m_SidewaysSpeed;
				}
				if (vector.y < 0f)
				{
					this.m_DesiredSpeed = this.m_BackwardSpeed;
				}
				if (base.Player.Run.Active && (this.m_DesiredSpeed == this.m_ForwardSpeed || this.m_DesiredSpeed == this.m_SidewaysSpeed))
				{
					this.m_DesiredSpeed *= this.m_RunSpeedMultiplier;
				}
				if (base.Player.Crouch.Active)
				{
					this.m_DesiredSpeed *= this.m_CrouchSpeedMultiplier;
				}
			}
			this.m_DesiredVelocity = vector2 * this.m_DesiredSpeed;
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x001A976C File Offset: 0x001A796C
		private bool TryStart_Run()
		{
			if (!this.m_EnableRunning)
			{
				return false;
			}
			bool flag = base.Player.MovementInput.Get().y > 0f;
			bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
			bool flag2 = base.Player.Stamina.Get() > 0f;
			return base.Player.IsGrounded.Get() && flag2 && flag && isClosed && !base.Player.Crouch.Active && !base.Player.Aim.Active;
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x001A9804 File Offset: 0x001A7A04
		private bool TryStart_Jump()
		{
			if (this.m_EnableJumping && base.Player.Stamina.Get() > 5f && (base.Player.IsGrounded.Get() || base.Player.NearLadders.Count > 0) && (!base.Player.Crouch.Active || base.Player.Crouch.TryStop()))
			{
				Vector3 vector = Vector3.up;
				if (base.Player.NearLadders.Count > 0)
				{
					vector = -base.Player.NearLadders.Peek().forward;
				}
				this.m_CurrentVelocity.y = 0f;
				if (base.Player.NearLadders.Count > 0)
				{
					this.m_CurrentVelocity += vector * this.m_JumpSpeedFromLadder;
					this.m_JumpedFrom = CCDrivenController.JumpedFrom.Ladder;
				}
				else
				{
					this.m_CurrentVelocity += vector * this.CalculateJumpSpeed(this.m_JumpHeight);
					this.m_JumpedFrom = CCDrivenController.JumpedFrom.Ground;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0002A989 File Offset: 0x00028B89
		private float CalculateJumpSpeed(float heightToReach)
		{
			return Mathf.Sqrt(2f * this.m_Gravity * heightToReach);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x001A992C File Offset: 0x001A7B2C
		private bool TryStart_Crouch()
		{
			bool flag = this.m_EnableCrouching && base.Player.NearLadders.Count == 0 && Time.time > this.m_LastTimeToggledCrouching + this.m_CrouchDuration && base.Player.IsGrounded.Get() && !base.Player.Run.Active;
			if (flag)
			{
				base.StartCoroutine(this.C_SetHeight(this.m_CrouchHeight));
				this.m_LastTimeToggledCrouching = Time.time;
			}
			return flag;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x001A99B0 File Offset: 0x001A7BB0
		private bool TryStop_Crouch()
		{
			bool flag = Time.time > this.m_LastTimeToggledCrouching + this.m_CrouchDuration;
			bool flag2 = this.CheckForObstacles(true, Mathf.Abs(this.m_CrouchHeight - this.m_UncrouchedHeight));
			bool flag3 = flag && !flag2;
			if (flag3)
			{
				base.StartCoroutine(this.C_SetHeight(this.m_UncrouchedHeight));
				this.m_LastTimeToggledCrouching = Time.time;
			}
			return flag3;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0002A99E File Offset: 0x00028B9E
		private IEnumerator C_SetHeight(float targetHeight)
		{
			float speed = Mathf.Abs(targetHeight - this.m_Controller.height) / this.m_CrouchDuration;
			while (Mathf.Abs(targetHeight - this.m_Controller.height) > Mathf.Epsilon)
			{
				this.m_Controller.height = Mathf.MoveTowards(this.m_Controller.height, targetHeight, Time.deltaTime * speed);
				this.m_Controller.center = Vector3.up * this.m_Controller.height / 2f;
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x001A9A18 File Offset: 0x001A7C18
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			this.m_LastSurfaceNormal = hit.normal;
			if (hit.rigidbody)
			{
				float num = this.m_PushForce * this.m_Controller.velocity.magnitude;
				Vector3 vector = (hit.moveDirection + Vector3.up * 0.35f) * num;
				hit.rigidbody.AddForceAtPosition(vector, hit.point);
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x0002A9B4 File Offset: 0x00028BB4
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Ladder"))
			{
				base.Player.NearLadders.Enqueue(other.transform);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x0002A9D9 File Offset: 0x00028BD9
		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Ladder"))
			{
				base.Player.NearLadders.Dequeue();
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x001A9A8C File Offset: 0x001A7C8C
		private bool CheckForObstacles(bool checkAbove, float maxDistance, out RaycastHit hitInfo)
		{
			Vector3 vector = base.transform.position + (checkAbove ? (Vector3.up * this.m_Controller.height) : Vector3.zero);
			Vector3 vector2 = checkAbove ? Vector3.up : Vector3.down;
			return Physics.SphereCast(new Ray(vector, vector2), this.m_Controller.radius, ref hitInfo, maxDistance, -1, 1);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x001A9AF4 File Offset: 0x001A7CF4
		private bool CheckForObstacles(bool checkAbove, float maxDistance)
		{
			Vector3 vector = base.transform.position + (checkAbove ? (Vector3.up * this.m_Controller.height) : Vector3.zero);
			Vector3 vector2 = checkAbove ? Vector3.up : Vector3.down;
			return Physics.SphereCast(new Ray(vector, vector2), this.m_Controller.radius, maxDistance, -1, 1);
		}

		// Token: 0x040034C9 RID: 13513
		[Header("General")]
		[SerializeField]
		[Tooltip("How fast the player will change direction / accelerate.")]
		private float m_Acceleration = 5f;

		// Token: 0x040034CA RID: 13514
		[SerializeField]
		[Tooltip("How fast the player will stop if no input is given (applies only when grounded).")]
		private float m_Damping = 8f;

		// Token: 0x040034CB RID: 13515
		[SerializeField]
		[Range(0f, 1f)]
		[Tooltip("How well the player can control direction while in air.")]
		private float m_AirControl = 0.15f;

		// Token: 0x040034CC RID: 13516
		[SerializeField]
		private float m_ForwardSpeed = 4f;

		// Token: 0x040034CD RID: 13517
		[SerializeField]
		private float m_SidewaysSpeed = 3.5f;

		// Token: 0x040034CE RID: 13518
		[SerializeField]
		private float m_BackwardSpeed = 3f;

		// Token: 0x040034CF RID: 13519
		[SerializeField]
		[Tooltip("Curve for multiplying speed based on slope.")]
		private AnimationCurve m_SlopeMultiplier = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x040034D0 RID: 13520
		[SerializeField]
		[Tooltip("A small number will make the player bump when descending slopes, a larger one will make it stick to the surface.")]
		private float m_AntiBumpFactor = 1f;

		// Token: 0x040034D1 RID: 13521
		[Header("Running")]
		[SerializeField]
		[Tooltip("Can the player run?")]
		private bool m_EnableRunning = true;

		// Token: 0x040034D2 RID: 13522
		[SerializeField]
		[Tooltip("The current movement speed will be multiplied by this value, when sprinting.")]
		private float m_RunSpeedMultiplier = 1.8f;

		// Token: 0x040034D3 RID: 13523
		[Header("Jumping")]
		[SerializeField]
		[Tooltip("Can the player jump?")]
		private bool m_EnableJumping = true;

		// Token: 0x040034D4 RID: 13524
		[SerializeField]
		[Tooltip("How high do we jump when pressing jump and letting go immediately.")]
		private float m_JumpHeight = 1f;

		// Token: 0x040034D5 RID: 13525
		[SerializeField]
		private float m_JumpSpeedFromLadder = 5f;

		// Token: 0x040034D6 RID: 13526
		[Header("Crouching")]
		[SerializeField]
		[Tooltip("Can the player crouch?")]
		private bool m_EnableCrouching = true;

		// Token: 0x040034D7 RID: 13527
		[SerializeField]
		[Tooltip("The current movement speed will be multiplied by this value, when moving crouched.")]
		private float m_CrouchSpeedMultiplier = 0.7f;

		// Token: 0x040034D8 RID: 13528
		[SerializeField]
		[Tooltip("The CharacterController's height when fully-crouched.")]
		private float m_CrouchHeight = 1f;

		// Token: 0x040034D9 RID: 13529
		[SerializeField]
		[Tooltip("How much time it takes to go in and out of crouch-mode.")]
		private float m_CrouchDuration = 0.3f;

		// Token: 0x040034DA RID: 13530
		[Header("Ladder Climbing")]
		[SerializeField]
		[Tooltip("How fast the character moves on ladder.")]
		private float m_SpeedOnLadder = 1f;

		// Token: 0x040034DB RID: 13531
		[Header("Sliding")]
		[SerializeField]
		[Tooltip("Will the player slide on steep surfaces?")]
		private bool m_EnableSliding = true;

		// Token: 0x040034DC RID: 13532
		[SerializeField]
		private float m_SlideLimit = 32f;

		// Token: 0x040034DD RID: 13533
		[Tooltip("How fast does the character slide on steep surfaces?")]
		[SerializeField]
		private float m_SlidingSpeed = 15f;

		// Token: 0x040034DE RID: 13534
		[Header("Physics")]
		[SerializeField]
		private float m_PushForce = 60f;

		// Token: 0x040034DF RID: 13535
		[SerializeField]
		[Tooltip("How fast we accelerate into falling.")]
		private float m_Gravity = 20f;

		// Token: 0x040034E0 RID: 13536
		private CharacterController m_Controller;

		// Token: 0x040034E1 RID: 13537
		private float m_DesiredSpeed;

		// Token: 0x040034E2 RID: 13538
		private Vector3 m_CurrentVelocity;

		// Token: 0x040034E3 RID: 13539
		private Vector3 m_SlideVelocity;

		// Token: 0x040034E4 RID: 13540
		private Vector3 m_DesiredVelocity;

		// Token: 0x040034E5 RID: 13541
		private Vector3 m_LastSurfaceNormal;

		// Token: 0x040034E6 RID: 13542
		private CollisionFlags m_LastCollisionFlags;

		// Token: 0x040034E7 RID: 13543
		private float m_UncrouchedHeight;

		// Token: 0x040034E8 RID: 13544
		private bool m_PreviouslyGrounded;

		// Token: 0x040034E9 RID: 13545
		private float m_LastTimeToggledCrouching;

		// Token: 0x040034EA RID: 13546
		private CCDrivenController.JumpedFrom m_JumpedFrom;

		// Token: 0x020008EF RID: 2287
		public enum JumpedFrom
		{
			// Token: 0x040034EC RID: 13548
			Ground,
			// Token: 0x040034ED RID: 13549
			Ladder
		}
	}
}
