using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Character/Character Motor")]
public class CharacterMotor : MonoBehaviour
{
	// Token: 0x06000B11 RID: 2833 RVA: 0x000433A7 File Offset: 0x000415A7
	private void Awake()
	{
		this.controller = base.gameObject.GetComponent<CharacterController>();
		this.tr = base.transform;
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x000433C8 File Offset: 0x000415C8
	private void UpdateFunction()
	{
		Vector3 vector = this.movement.velocity;
		vector = this.ApplyInputVelocityChange(vector);
		vector = this.ApplyGravityAndJumping(vector);
		Vector3 vector2 = Vector3.zero;
		if (this.MoveWithPlatform())
		{
			vector2 = this.movingPlatform.activePlatform.TransformPoint(this.movingPlatform.activeLocalPoint) - this.movingPlatform.activeGlobalPoint;
			if (vector2 != Vector3.zero)
			{
				this.controller.Move(vector2);
			}
			float y = (this.movingPlatform.activePlatform.rotation * this.movingPlatform.activeLocalRotation * Quaternion.Inverse(this.movingPlatform.activeGlobalRotation)).eulerAngles.y;
			if (y != 0f)
			{
				this.tr.Rotate(0f, y, 0f);
			}
		}
		Vector3 position = this.tr.position;
		Vector3 vector3 = vector * Time.deltaTime;
		float num = Mathf.Max(this.controller.stepOffset, new Vector3(vector3.x, 0f, vector3.z).magnitude);
		if (this.grounded)
		{
			vector3 -= num * Vector3.up;
		}
		this.movingPlatform.hitPlatform = null;
		this.groundNormal = Vector3.zero;
		this.movement.collisionFlags = this.controller.Move(vector3);
		this.movement.lastHitPoint = this.movement.hitPoint;
		this.lastGroundNormal = this.groundNormal;
		if (this.movingPlatform.enabled && this.movingPlatform.activePlatform != this.movingPlatform.hitPlatform && this.movingPlatform.hitPlatform != null)
		{
			this.movingPlatform.activePlatform = this.movingPlatform.hitPlatform;
			this.movingPlatform.lastMatrix = this.movingPlatform.hitPlatform.localToWorldMatrix;
			this.movingPlatform.newPlatform = true;
		}
		Vector3 vector4;
		vector4..ctor(vector.x, 0f, vector.z);
		this.movement.velocity = (this.tr.position - position) / Time.deltaTime;
		Vector3 vector5;
		vector5..ctor(this.movement.velocity.x, 0f, this.movement.velocity.z);
		if (vector4 == Vector3.zero)
		{
			this.movement.velocity = new Vector3(0f, this.movement.velocity.y, 0f);
		}
		else
		{
			float num2 = Vector3.Dot(vector5, vector4) / vector4.sqrMagnitude;
			this.movement.velocity = vector4 * Mathf.Clamp01(num2) + this.movement.velocity.y * Vector3.up;
		}
		if ((double)this.movement.velocity.y < (double)vector.y - 0.001)
		{
			if (this.movement.velocity.y < 0f)
			{
				this.movement.velocity.y = vector.y;
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		if (this.grounded && !this.IsGroundedTest())
		{
			this.grounded = false;
			if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaTransfer))
			{
				this.movement.frameVelocity = this.movingPlatform.platformVelocity;
				this.movement.velocity += this.movingPlatform.platformVelocity;
			}
			base.SendMessage("OnFall", 1);
			this.tr.position += num * Vector3.up;
		}
		else if (!this.grounded && this.IsGroundedTest())
		{
			this.grounded = true;
			this.jumping.jumping = false;
			this.SubtractNewPlatformVelocity();
			base.SendMessage("OnLand", 1);
		}
		if (this.MoveWithPlatform())
		{
			this.movingPlatform.activeGlobalPoint = this.tr.position + Vector3.up * (this.controller.center.y - this.controller.height * 0.5f + this.controller.radius);
			this.movingPlatform.activeLocalPoint = this.movingPlatform.activePlatform.InverseTransformPoint(this.movingPlatform.activeGlobalPoint);
			this.movingPlatform.activeGlobalRotation = this.tr.rotation;
			this.movingPlatform.activeLocalRotation = Quaternion.Inverse(this.movingPlatform.activePlatform.rotation) * this.movingPlatform.activeGlobalRotation;
		}
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x000438D8 File Offset: 0x00041AD8
	private void FixedUpdate()
	{
		if (this.movingPlatform.enabled)
		{
			if (this.movingPlatform.activePlatform != null)
			{
				if (!this.movingPlatform.newPlatform)
				{
					this.movingPlatform.platformVelocity = (this.movingPlatform.activePlatform.localToWorldMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint) - this.movingPlatform.lastMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint)) / Time.deltaTime;
				}
				this.movingPlatform.lastMatrix = this.movingPlatform.activePlatform.localToWorldMatrix;
				this.movingPlatform.newPlatform = false;
			}
			else
			{
				this.movingPlatform.platformVelocity = Vector3.zero;
			}
		}
		if (this.useFixedUpdate)
		{
			this.UpdateFunction();
		}
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000439B7 File Offset: 0x00041BB7
	private void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.UpdateFunction();
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x000439C8 File Offset: 0x00041BC8
	private Vector3 ApplyInputVelocityChange(Vector3 velocity)
	{
		if (!this.canControl)
		{
			this.inputMoveDirection = Vector3.zero;
		}
		Vector3 vector;
		if (this.grounded && this.TooSteep())
		{
			vector = new Vector3(this.groundNormal.x, 0f, this.groundNormal.z).normalized;
			Vector3 vector2 = Vector3.Project(this.inputMoveDirection, vector);
			vector = vector + vector2 * this.sliding.speedControl + (this.inputMoveDirection - vector2) * this.sliding.sidewaysControl;
			vector *= this.sliding.slidingSpeed;
		}
		else
		{
			vector = this.GetDesiredHorizontalVelocity();
		}
		if (this.movingPlatform.enabled && this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaTransfer)
		{
			vector += this.movement.frameVelocity;
			vector.y = 0f;
		}
		if (this.grounded)
		{
			vector = this.AdjustGroundVelocityToNormal(vector, this.groundNormal);
		}
		else
		{
			velocity.y = 0f;
		}
		float num = this.GetMaxAcceleration(this.grounded) * Time.deltaTime;
		Vector3 vector3 = vector - velocity;
		if (vector3.sqrMagnitude > num * num)
		{
			vector3 = vector3.normalized * num;
		}
		if (this.grounded || this.canControl)
		{
			velocity += vector3;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min(velocity.y, 0f);
		}
		return velocity;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00043B54 File Offset: 0x00041D54
	private Vector3 ApplyGravityAndJumping(Vector3 velocity)
	{
		if (!this.inputJump || !this.canControl)
		{
			this.jumping.holdingJumpButton = false;
			this.jumping.lastButtonDownTime = -100f;
		}
		if (this.inputJump && this.jumping.lastButtonDownTime < 0f && this.canControl)
		{
			this.jumping.lastButtonDownTime = Time.time;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min(0f, velocity.y) - this.movement.gravity * Time.deltaTime;
		}
		else
		{
			velocity.y = this.movement.velocity.y - this.movement.gravity * Time.deltaTime * 2f;
			if (this.jumping.jumping && this.jumping.holdingJumpButton && Time.time < this.jumping.lastStartTime + this.jumping.extraHeight / this.CalculateJumpVerticalSpeed(this.jumping.baseHeight))
			{
				velocity += this.jumping.jumpDir * this.movement.gravity * Time.deltaTime;
			}
			velocity.y = Mathf.Max(velocity.y, -this.movement.maxFallSpeed);
		}
		if (this.grounded)
		{
			if (this.jumping.enabled && this.canControl && (double)(Time.time - this.jumping.lastButtonDownTime) < 0.2)
			{
				this.grounded = false;
				this.jumping.jumping = true;
				this.jumping.lastStartTime = Time.time;
				this.jumping.lastButtonDownTime = -100f;
				this.jumping.holdingJumpButton = true;
				if (this.TooSteep())
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.steepPerpAmount);
				}
				else
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.perpAmount);
				}
				velocity.y = 0f;
				velocity += this.jumping.jumpDir * this.CalculateJumpVerticalSpeed(this.jumping.baseHeight);
				if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaTransfer))
				{
					this.movement.frameVelocity = this.movingPlatform.platformVelocity;
					velocity += this.movingPlatform.platformVelocity;
				}
				base.SendMessage("OnJump", 1);
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		return velocity;
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00043E38 File Offset: 0x00042038
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.normal.y > 0f && hit.normal.y > this.groundNormal.y && hit.moveDirection.y < 0f)
		{
			if ((double)(hit.point - this.movement.lastHitPoint).sqrMagnitude > 0.001 || this.lastGroundNormal == Vector3.zero)
			{
				this.groundNormal = hit.normal;
			}
			else
			{
				this.groundNormal = this.lastGroundNormal;
			}
			this.movingPlatform.hitPlatform = hit.collider.transform;
			this.movement.hitPoint = hit.point;
			this.movement.frameVelocity = Vector3.zero;
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00043F17 File Offset: 0x00042117
	private IEnumerable SubtractNewPlatformVelocity()
	{
		if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaTransfer))
		{
			if (this.movingPlatform.newPlatform)
			{
				Transform platform = this.movingPlatform.activePlatform;
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				if (this.grounded && platform == this.movingPlatform.activePlatform)
				{
					yield return 1;
				}
				platform = null;
			}
			this.movement.velocity -= this.movingPlatform.platformVelocity;
		}
		yield break;
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00043F27 File Offset: 0x00042127
	private bool MoveWithPlatform()
	{
		return this.movingPlatform.enabled && (this.grounded || this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaLocked) && this.movingPlatform.activePlatform != null;
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00043F60 File Offset: 0x00042160
	private Vector3 GetDesiredHorizontalVelocity()
	{
		Vector3 vector = this.tr.InverseTransformDirection(this.inputMoveDirection);
		float num = this.MaxSpeedInDirection(vector);
		if (this.grounded)
		{
			float num2 = Mathf.Asin(this.movement.velocity.normalized.y) * 57.29578f;
			num *= this.movement.slopeSpeedMultiplier.Evaluate(num2);
		}
		return this.tr.TransformDirection(vector * num);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00043FD8 File Offset: 0x000421D8
	private Vector3 AdjustGroundVelocityToNormal(Vector3 hVelocity, Vector3 groundNormal)
	{
		return Vector3.Cross(Vector3.Cross(Vector3.up, hVelocity), groundNormal).normalized * hVelocity.magnitude;
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0004400A File Offset: 0x0004220A
	private bool IsGroundedTest()
	{
		return (double)this.groundNormal.y > 0.01;
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00044023 File Offset: 0x00042223
	private float GetMaxAcceleration(bool grounded)
	{
		if (!grounded)
		{
			return this.movement.maxAirAcceleration;
		}
		return this.movement.maxGroundAcceleration;
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0004403F File Offset: 0x0004223F
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.movement.gravity);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00044059 File Offset: 0x00042259
	private bool IsJumping()
	{
		return this.jumping.jumping;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00044066 File Offset: 0x00042266
	private bool IsSliding()
	{
		return this.grounded && this.sliding.enabled && this.TooSteep();
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00044085 File Offset: 0x00042285
	private bool IsTouchingCeiling()
	{
		return (this.movement.collisionFlags & 2) > 0;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00044097 File Offset: 0x00042297
	private bool IsGrounded()
	{
		return this.grounded;
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0004409F File Offset: 0x0004229F
	private bool TooSteep()
	{
		return this.groundNormal.y <= Mathf.Cos(this.controller.slopeLimit * 0.017453292f);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x000440C7 File Offset: 0x000422C7
	private Vector3 GetDirection()
	{
		return this.inputMoveDirection;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000440CF File Offset: 0x000422CF
	private void SetControllable(bool controllable)
	{
		this.canControl = controllable;
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x000440D8 File Offset: 0x000422D8
	private float MaxSpeedInDirection(Vector3 desiredMovementDirection)
	{
		if (desiredMovementDirection == Vector3.zero)
		{
			return 0f;
		}
		float num = ((desiredMovementDirection.z > 0f) ? this.movement.maxForwardSpeed : this.movement.maxBackwardsSpeed) / this.movement.maxSidewaysSpeed;
		Vector3 normalized = new Vector3(desiredMovementDirection.x, 0f, desiredMovementDirection.z / num).normalized;
		return new Vector3(normalized.x, 0f, normalized.z * num).magnitude * this.movement.maxSidewaysSpeed;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00044177 File Offset: 0x00042377
	private void SetVelocity(Vector3 velocity)
	{
		this.grounded = false;
		this.movement.velocity = velocity;
		this.movement.frameVelocity = Vector3.zero;
		base.SendMessage("OnExternalVelocity");
	}

	// Token: 0x04000739 RID: 1849
	private bool canControl = true;

	// Token: 0x0400073A RID: 1850
	private bool useFixedUpdate = true;

	// Token: 0x0400073B RID: 1851
	[NonSerialized]
	public Vector3 inputMoveDirection = Vector3.zero;

	// Token: 0x0400073C RID: 1852
	[NonSerialized]
	public bool inputJump;

	// Token: 0x0400073D RID: 1853
	private CharacterMotor.CharacterMotorMovement movement = new CharacterMotor.CharacterMotorMovement();

	// Token: 0x0400073E RID: 1854
	private CharacterMotor.CharacterMotorJumping jumping = new CharacterMotor.CharacterMotorJumping();

	// Token: 0x0400073F RID: 1855
	private CharacterMotor.CharacterMotorMovingPlatform movingPlatform = new CharacterMotor.CharacterMotorMovingPlatform();

	// Token: 0x04000740 RID: 1856
	private CharacterMotor.CharacterMotorSliding sliding = new CharacterMotor.CharacterMotorSliding();

	// Token: 0x04000741 RID: 1857
	[NonSerialized]
	public bool grounded = true;

	// Token: 0x04000742 RID: 1858
	[NonSerialized]
	public Vector3 groundNormal = Vector3.zero;

	// Token: 0x04000743 RID: 1859
	private Vector3 lastGroundNormal = Vector3.zero;

	// Token: 0x04000744 RID: 1860
	private Transform tr;

	// Token: 0x04000745 RID: 1861
	private CharacterController controller;

	// Token: 0x02001235 RID: 4661
	private class CharacterMotorMovement
	{
		// Token: 0x040064FA RID: 25850
		public float maxForwardSpeed = 10f;

		// Token: 0x040064FB RID: 25851
		public float maxSidewaysSpeed = 10f;

		// Token: 0x040064FC RID: 25852
		public float maxBackwardsSpeed = 10f;

		// Token: 0x040064FD RID: 25853
		public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(-90f, 1f),
			new Keyframe(0f, 1f),
			new Keyframe(90f, 0f)
		});

		// Token: 0x040064FE RID: 25854
		public float maxGroundAcceleration = 30f;

		// Token: 0x040064FF RID: 25855
		public float maxAirAcceleration = 20f;

		// Token: 0x04006500 RID: 25856
		public float gravity = 10f;

		// Token: 0x04006501 RID: 25857
		public float maxFallSpeed = 20f;

		// Token: 0x04006502 RID: 25858
		[NonSerialized]
		public CollisionFlags collisionFlags;

		// Token: 0x04006503 RID: 25859
		[NonSerialized]
		public Vector3 velocity;

		// Token: 0x04006504 RID: 25860
		[NonSerialized]
		public Vector3 frameVelocity = Vector3.zero;

		// Token: 0x04006505 RID: 25861
		[NonSerialized]
		public Vector3 hitPoint = Vector3.zero;

		// Token: 0x04006506 RID: 25862
		[NonSerialized]
		public Vector3 lastHitPoint = new Vector3(float.PositiveInfinity, 0f, 0f);
	}

	// Token: 0x02001236 RID: 4662
	private enum MovementTransferOnJump
	{
		// Token: 0x04006508 RID: 25864
		None,
		// Token: 0x04006509 RID: 25865
		InitTransfer,
		// Token: 0x0400650A RID: 25866
		PermaTransfer,
		// Token: 0x0400650B RID: 25867
		PermaLocked
	}

	// Token: 0x02001237 RID: 4663
	private class CharacterMotorJumping
	{
		// Token: 0x0400650C RID: 25868
		public bool enabled = true;

		// Token: 0x0400650D RID: 25869
		public float baseHeight = 1.6f;

		// Token: 0x0400650E RID: 25870
		public float extraHeight = 1.6f;

		// Token: 0x0400650F RID: 25871
		public float perpAmount = 2f;

		// Token: 0x04006510 RID: 25872
		public float steepPerpAmount = 1.5f;

		// Token: 0x04006511 RID: 25873
		[NonSerialized]
		public bool jumping;

		// Token: 0x04006512 RID: 25874
		[NonSerialized]
		public bool holdingJumpButton;

		// Token: 0x04006513 RID: 25875
		[NonSerialized]
		public float lastStartTime;

		// Token: 0x04006514 RID: 25876
		[NonSerialized]
		public float lastButtonDownTime = -100f;

		// Token: 0x04006515 RID: 25877
		[NonSerialized]
		public Vector3 jumpDir = Vector3.up;
	}

	// Token: 0x02001238 RID: 4664
	private class CharacterMotorMovingPlatform
	{
		// Token: 0x04006516 RID: 25878
		public bool enabled = true;

		// Token: 0x04006517 RID: 25879
		public CharacterMotor.MovementTransferOnJump movementTransfer = CharacterMotor.MovementTransferOnJump.PermaTransfer;

		// Token: 0x04006518 RID: 25880
		[NonSerialized]
		public Transform hitPlatform;

		// Token: 0x04006519 RID: 25881
		[NonSerialized]
		public Transform activePlatform;

		// Token: 0x0400651A RID: 25882
		[NonSerialized]
		public Vector3 activeLocalPoint;

		// Token: 0x0400651B RID: 25883
		[NonSerialized]
		public Vector3 activeGlobalPoint;

		// Token: 0x0400651C RID: 25884
		[NonSerialized]
		public Quaternion activeLocalRotation;

		// Token: 0x0400651D RID: 25885
		[NonSerialized]
		public Quaternion activeGlobalRotation;

		// Token: 0x0400651E RID: 25886
		[NonSerialized]
		public Matrix4x4 lastMatrix;

		// Token: 0x0400651F RID: 25887
		[NonSerialized]
		public Vector3 platformVelocity;

		// Token: 0x04006520 RID: 25888
		[NonSerialized]
		public bool newPlatform;
	}

	// Token: 0x02001239 RID: 4665
	private class CharacterMotorSliding
	{
		// Token: 0x04006521 RID: 25889
		public bool enabled = true;

		// Token: 0x04006522 RID: 25890
		public float slidingSpeed = 15f;

		// Token: 0x04006523 RID: 25891
		public float sidewaysControl = 1f;

		// Token: 0x04006524 RID: 25892
		public float speedControl = 0.4f;
	}
}
