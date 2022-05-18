using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000146 RID: 326
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Character/Character Motor")]
public class CharacterMotor : MonoBehaviour
{
	// Token: 0x06000BF4 RID: 3060 RVA: 0x0000E03F File Offset: 0x0000C23F
	private void Awake()
	{
		this.controller = base.gameObject.GetComponent<CharacterController>();
		this.tr = base.transform;
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x000950E0 File Offset: 0x000932E0
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

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000955F0 File Offset: 0x000937F0
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

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0000E05E File Offset: 0x0000C25E
	private void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.UpdateFunction();
		}
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x000956D0 File Offset: 0x000938D0
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

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0009585C File Offset: 0x00093A5C
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

	// Token: 0x06000BFA RID: 3066 RVA: 0x00095B40 File Offset: 0x00093D40
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

	// Token: 0x06000BFB RID: 3067 RVA: 0x0000E06E File Offset: 0x0000C26E
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

	// Token: 0x06000BFC RID: 3068 RVA: 0x0000E07E File Offset: 0x0000C27E
	private bool MoveWithPlatform()
	{
		return this.movingPlatform.enabled && (this.grounded || this.movingPlatform.movementTransfer == CharacterMotor.MovementTransferOnJump.PermaLocked) && this.movingPlatform.activePlatform != null;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00095C20 File Offset: 0x00093E20
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

	// Token: 0x06000BFE RID: 3070 RVA: 0x00095C98 File Offset: 0x00093E98
	private Vector3 AdjustGroundVelocityToNormal(Vector3 hVelocity, Vector3 groundNormal)
	{
		return Vector3.Cross(Vector3.Cross(Vector3.up, hVelocity), groundNormal).normalized * hVelocity.magnitude;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
	private bool IsGroundedTest()
	{
		return (double)this.groundNormal.y > 0.01;
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0000E0CF File Offset: 0x0000C2CF
	private float GetMaxAcceleration(bool grounded)
	{
		if (!grounded)
		{
			return this.movement.maxAirAcceleration;
		}
		return this.movement.maxGroundAcceleration;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0000E0EB File Offset: 0x0000C2EB
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.movement.gravity);
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0000E105 File Offset: 0x0000C305
	private bool IsJumping()
	{
		return this.jumping.jumping;
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x0000E112 File Offset: 0x0000C312
	private bool IsSliding()
	{
		return this.grounded && this.sliding.enabled && this.TooSteep();
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x0000E131 File Offset: 0x0000C331
	private bool IsTouchingCeiling()
	{
		return (this.movement.collisionFlags & 2) > 0;
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0000E143 File Offset: 0x0000C343
	private bool IsGrounded()
	{
		return this.grounded;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x0000E14B File Offset: 0x0000C34B
	private bool TooSteep()
	{
		return this.groundNormal.y <= Mathf.Cos(this.controller.slopeLimit * 0.017453292f);
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0000E173 File Offset: 0x0000C373
	private Vector3 GetDirection()
	{
		return this.inputMoveDirection;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0000E17B File Offset: 0x0000C37B
	private void SetControllable(bool controllable)
	{
		this.canControl = controllable;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00095CCC File Offset: 0x00093ECC
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

	// Token: 0x06000C0A RID: 3082 RVA: 0x0000E184 File Offset: 0x0000C384
	private void SetVelocity(Vector3 velocity)
	{
		this.grounded = false;
		this.movement.velocity = velocity;
		this.movement.frameVelocity = Vector3.zero;
		base.SendMessage("OnExternalVelocity");
	}

	// Token: 0x040008E4 RID: 2276
	private bool canControl = true;

	// Token: 0x040008E5 RID: 2277
	private bool useFixedUpdate = true;

	// Token: 0x040008E6 RID: 2278
	[NonSerialized]
	public Vector3 inputMoveDirection = Vector3.zero;

	// Token: 0x040008E7 RID: 2279
	[NonSerialized]
	public bool inputJump;

	// Token: 0x040008E8 RID: 2280
	private CharacterMotor.CharacterMotorMovement movement = new CharacterMotor.CharacterMotorMovement();

	// Token: 0x040008E9 RID: 2281
	private CharacterMotor.CharacterMotorJumping jumping = new CharacterMotor.CharacterMotorJumping();

	// Token: 0x040008EA RID: 2282
	private CharacterMotor.CharacterMotorMovingPlatform movingPlatform = new CharacterMotor.CharacterMotorMovingPlatform();

	// Token: 0x040008EB RID: 2283
	private CharacterMotor.CharacterMotorSliding sliding = new CharacterMotor.CharacterMotorSliding();

	// Token: 0x040008EC RID: 2284
	[NonSerialized]
	public bool grounded = true;

	// Token: 0x040008ED RID: 2285
	[NonSerialized]
	public Vector3 groundNormal = Vector3.zero;

	// Token: 0x040008EE RID: 2286
	private Vector3 lastGroundNormal = Vector3.zero;

	// Token: 0x040008EF RID: 2287
	private Transform tr;

	// Token: 0x040008F0 RID: 2288
	private CharacterController controller;

	// Token: 0x02000147 RID: 327
	private class CharacterMotorMovement
	{
		// Token: 0x040008F1 RID: 2289
		public float maxForwardSpeed = 10f;

		// Token: 0x040008F2 RID: 2290
		public float maxSidewaysSpeed = 10f;

		// Token: 0x040008F3 RID: 2291
		public float maxBackwardsSpeed = 10f;

		// Token: 0x040008F4 RID: 2292
		public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(-90f, 1f),
			new Keyframe(0f, 1f),
			new Keyframe(90f, 0f)
		});

		// Token: 0x040008F5 RID: 2293
		public float maxGroundAcceleration = 30f;

		// Token: 0x040008F6 RID: 2294
		public float maxAirAcceleration = 20f;

		// Token: 0x040008F7 RID: 2295
		public float gravity = 10f;

		// Token: 0x040008F8 RID: 2296
		public float maxFallSpeed = 20f;

		// Token: 0x040008F9 RID: 2297
		[NonSerialized]
		public CollisionFlags collisionFlags;

		// Token: 0x040008FA RID: 2298
		[NonSerialized]
		public Vector3 velocity;

		// Token: 0x040008FB RID: 2299
		[NonSerialized]
		public Vector3 frameVelocity = Vector3.zero;

		// Token: 0x040008FC RID: 2300
		[NonSerialized]
		public Vector3 hitPoint = Vector3.zero;

		// Token: 0x040008FD RID: 2301
		[NonSerialized]
		public Vector3 lastHitPoint = new Vector3(float.PositiveInfinity, 0f, 0f);
	}

	// Token: 0x02000148 RID: 328
	private enum MovementTransferOnJump
	{
		// Token: 0x040008FF RID: 2303
		None,
		// Token: 0x04000900 RID: 2304
		InitTransfer,
		// Token: 0x04000901 RID: 2305
		PermaTransfer,
		// Token: 0x04000902 RID: 2306
		PermaLocked
	}

	// Token: 0x02000149 RID: 329
	private class CharacterMotorJumping
	{
		// Token: 0x04000903 RID: 2307
		public bool enabled = true;

		// Token: 0x04000904 RID: 2308
		public float baseHeight = 1.6f;

		// Token: 0x04000905 RID: 2309
		public float extraHeight = 1.6f;

		// Token: 0x04000906 RID: 2310
		public float perpAmount = 2f;

		// Token: 0x04000907 RID: 2311
		public float steepPerpAmount = 1.5f;

		// Token: 0x04000908 RID: 2312
		[NonSerialized]
		public bool jumping;

		// Token: 0x04000909 RID: 2313
		[NonSerialized]
		public bool holdingJumpButton;

		// Token: 0x0400090A RID: 2314
		[NonSerialized]
		public float lastStartTime;

		// Token: 0x0400090B RID: 2315
		[NonSerialized]
		public float lastButtonDownTime = -100f;

		// Token: 0x0400090C RID: 2316
		[NonSerialized]
		public Vector3 jumpDir = Vector3.up;
	}

	// Token: 0x0200014A RID: 330
	private class CharacterMotorMovingPlatform
	{
		// Token: 0x0400090D RID: 2317
		public bool enabled = true;

		// Token: 0x0400090E RID: 2318
		public CharacterMotor.MovementTransferOnJump movementTransfer = CharacterMotor.MovementTransferOnJump.PermaTransfer;

		// Token: 0x0400090F RID: 2319
		[NonSerialized]
		public Transform hitPlatform;

		// Token: 0x04000910 RID: 2320
		[NonSerialized]
		public Transform activePlatform;

		// Token: 0x04000911 RID: 2321
		[NonSerialized]
		public Vector3 activeLocalPoint;

		// Token: 0x04000912 RID: 2322
		[NonSerialized]
		public Vector3 activeGlobalPoint;

		// Token: 0x04000913 RID: 2323
		[NonSerialized]
		public Quaternion activeLocalRotation;

		// Token: 0x04000914 RID: 2324
		[NonSerialized]
		public Quaternion activeGlobalRotation;

		// Token: 0x04000915 RID: 2325
		[NonSerialized]
		public Matrix4x4 lastMatrix;

		// Token: 0x04000916 RID: 2326
		[NonSerialized]
		public Vector3 platformVelocity;

		// Token: 0x04000917 RID: 2327
		[NonSerialized]
		public bool newPlatform;
	}

	// Token: 0x0200014B RID: 331
	private class CharacterMotorSliding
	{
		// Token: 0x04000918 RID: 2328
		public bool enabled = true;

		// Token: 0x04000919 RID: 2329
		public float slidingSpeed = 15f;

		// Token: 0x0400091A RID: 2330
		public float sidewaysControl = 1f;

		// Token: 0x0400091B RID: 2331
		public float speedControl = 0.4f;
	}
}
