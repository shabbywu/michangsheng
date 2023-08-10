using System.Collections;
using UnityEngine;

namespace UltimateSurvival;

[RequireComponent(typeof(CharacterController))]
public class CCDrivenController : PlayerBehaviour
{
	public enum JumpedFrom
	{
		Ground,
		Ladder
	}

	[Header("General")]
	[SerializeField]
	[Tooltip("How fast the player will change direction / accelerate.")]
	private float m_Acceleration = 5f;

	[SerializeField]
	[Tooltip("How fast the player will stop if no input is given (applies only when grounded).")]
	private float m_Damping = 8f;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("How well the player can control direction while in air.")]
	private float m_AirControl = 0.15f;

	[SerializeField]
	private float m_ForwardSpeed = 4f;

	[SerializeField]
	private float m_SidewaysSpeed = 3.5f;

	[SerializeField]
	private float m_BackwardSpeed = 3f;

	[SerializeField]
	[Tooltip("Curve for multiplying speed based on slope.")]
	private AnimationCurve m_SlopeMultiplier = new AnimationCurve((Keyframe[])(object)new Keyframe[2]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	[SerializeField]
	[Tooltip("A small number will make the player bump when descending slopes, a larger one will make it stick to the surface.")]
	private float m_AntiBumpFactor = 1f;

	[Header("Running")]
	[SerializeField]
	[Tooltip("Can the player run?")]
	private bool m_EnableRunning = true;

	[SerializeField]
	[Tooltip("The current movement speed will be multiplied by this value, when sprinting.")]
	private float m_RunSpeedMultiplier = 1.8f;

	[Header("Jumping")]
	[SerializeField]
	[Tooltip("Can the player jump?")]
	private bool m_EnableJumping = true;

	[SerializeField]
	[Tooltip("How high do we jump when pressing jump and letting go immediately.")]
	private float m_JumpHeight = 1f;

	[SerializeField]
	private float m_JumpSpeedFromLadder = 5f;

	[Header("Crouching")]
	[SerializeField]
	[Tooltip("Can the player crouch?")]
	private bool m_EnableCrouching = true;

	[SerializeField]
	[Tooltip("The current movement speed will be multiplied by this value, when moving crouched.")]
	private float m_CrouchSpeedMultiplier = 0.7f;

	[SerializeField]
	[Tooltip("The CharacterController's height when fully-crouched.")]
	private float m_CrouchHeight = 1f;

	[SerializeField]
	[Tooltip("How much time it takes to go in and out of crouch-mode.")]
	private float m_CrouchDuration = 0.3f;

	[Header("Ladder Climbing")]
	[SerializeField]
	[Tooltip("How fast the character moves on ladder.")]
	private float m_SpeedOnLadder = 1f;

	[Header("Sliding")]
	[SerializeField]
	[Tooltip("Will the player slide on steep surfaces?")]
	private bool m_EnableSliding = true;

	[SerializeField]
	private float m_SlideLimit = 32f;

	[Tooltip("How fast does the character slide on steep surfaces?")]
	[SerializeField]
	private float m_SlidingSpeed = 15f;

	[Header("Physics")]
	[SerializeField]
	private float m_PushForce = 60f;

	[SerializeField]
	[Tooltip("How fast we accelerate into falling.")]
	private float m_Gravity = 20f;

	private CharacterController m_Controller;

	private float m_DesiredSpeed;

	private Vector3 m_CurrentVelocity;

	private Vector3 m_SlideVelocity;

	private Vector3 m_DesiredVelocity;

	private Vector3 m_LastSurfaceNormal;

	private CollisionFlags m_LastCollisionFlags;

	private float m_UncrouchedHeight;

	private bool m_PreviouslyGrounded;

	private float m_LastTimeToggledCrouching;

	private JumpedFrom m_JumpedFrom;

	private void Start()
	{
		m_Controller = ((Component)this).GetComponent<CharacterController>();
		m_UncrouchedHeight = m_Controller.height;
		base.Player.Jump.AddStartTryer(TryStart_Jump);
		base.Player.Run.AddStartTryer(TryStart_Run);
		base.Player.Crouch.AddStartTryer(TryStart_Crouch);
		base.Player.Crouch.AddStopTryer(TryStop_Crouch);
		base.Player.Sleep.AddStopListener(delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			m_CurrentVelocity = Vector3.zero;
		});
	}

	private void Update()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Invalid comparison between Unknown and I4
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		m_LastCollisionFlags = m_Controller.Move(m_CurrentVelocity * Time.deltaTime);
		if ((m_LastCollisionFlags & 4) == 4 && !m_PreviouslyGrounded)
		{
			if (base.Player.Jump.Active)
			{
				base.Player.Jump.ForceStop();
			}
			base.Player.Land.Send(Mathf.Abs(base.Player.Velocity.Get().y));
		}
		base.Player.IsGrounded.Set(m_Controller.isGrounded);
		base.Player.Velocity.Set(m_Controller.velocity);
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
			if (base.Player.Jump.Active && m_JumpedFrom == JumpedFrom.Ground)
			{
				base.Player.Jump.ForceStop();
			}
			UpdateLadderMovement();
		}
		else if (!m_Controller.isGrounded)
		{
			if (base.Player.Walk.Active)
			{
				base.Player.Walk.ForceStop();
			}
			if (base.Player.Run.Active)
			{
				base.Player.Run.ForceStop();
			}
			UpdateFalling();
		}
		else if (!base.Player.Jump.Active)
		{
			UpdateMovement();
		}
		m_PreviouslyGrounded = m_Controller.isGrounded;
	}

	private void UpdateLadderMovement()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Vector2.ClampMagnitude(base.Player.MovementInput.Get(), 1f);
		if (MonoSingleton<InventoryController>.Instance.IsClosed)
		{
			Transform val2 = base.Player.NearLadders.Peek();
			Vector3 val3 = base.Player.LookDirection.Get();
			bool flag = Vector3.Dot(val2.forward, val3) < 0f;
			bool flag2 = (val.y < 0f && !flag) || (val.y > 0f && flag);
			if (base.Player.IsGrounded.Get() && flag2)
			{
				m_DesiredVelocity = ((Component)this).transform.forward * val.y * 3f;
			}
			else
			{
				Vector3 val4 = val2.right * val.x * m_SpeedOnLadder / 2f;
				if (flag)
				{
					val4 *= -1f;
				}
				Vector3 val5 = val3 * val.y * m_SpeedOnLadder;
				Vector3 val6 = val4 + val5;
				Vector3 val7 = Vector3.ProjectOnPlane(val6, val2.right);
				Vector3 val8 = val2.up * Mathf.Sign(val6.y);
				val6 = Quaternion.FromToRotation(val7, val8) * val6;
				if (((Vector2)(ref val)).sqrMagnitude > 0f)
				{
					val6 += val2.forward;
				}
				m_DesiredVelocity = val6;
			}
		}
		else
		{
			m_DesiredVelocity = Vector3.zero;
		}
		float num = ((((Vector3)(ref m_DesiredVelocity)).sqrMagnitude > 0f) ? m_Acceleration : m_Damping);
		m_CurrentVelocity = Vector3.Lerp(m_CurrentVelocity, m_DesiredVelocity, num * Time.deltaTime);
	}

	private void UpdateMovement()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		CalculateDesiredVelocity();
		Vector3 val = (MonoSingleton<InventoryController>.Instance.IsClosed ? m_DesiredVelocity : Vector3.zero);
		float num = Vector3.Angle(Vector3.up, m_LastSurfaceNormal);
		val *= m_SlopeMultiplier.Evaluate(num / m_Controller.slopeLimit);
		float num2 = ((((Vector3)(ref val)).sqrMagnitude > 0f) ? m_Acceleration : m_Damping);
		m_CurrentVelocity = Vector3.Lerp(m_CurrentVelocity, val, num2 * Time.deltaTime);
		if (!base.Player.Walk.Active && ((Vector3)(ref val)).sqrMagnitude > 0.1f && !base.Player.Run.Active)
		{
			base.Player.Walk.ForceStart();
		}
		else if (base.Player.Walk.Active && (((Vector3)(ref val)).sqrMagnitude < 0.1f || base.Player.Run.Active))
		{
			base.Player.Walk.ForceStop();
		}
		if (base.Player.Run.Active)
		{
			if (base.Player.Stamina.Is(0f))
			{
				base.Player.Run.ForceStop();
			}
			else if (base.Player.MovementInput.Get().y < 0f || m_DesiredSpeed == 0f)
			{
				base.Player.Run.ForceStop();
			}
		}
		if (m_EnableSliding)
		{
			if (num > m_SlideLimit)
			{
				Vector3 val2 = m_LastSurfaceNormal + Vector3.down;
				m_SlideVelocity += val2 * m_SlidingSpeed * Time.deltaTime;
			}
			else
			{
				m_SlideVelocity = Vector3.Lerp(m_SlideVelocity, Vector3.zero, Time.deltaTime * 10f);
			}
			m_CurrentVelocity += m_SlideVelocity;
		}
		if (!base.Player.Jump.Active)
		{
			m_CurrentVelocity.y = 0f - m_AntiBumpFactor;
		}
	}

	private void UpdateFalling()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (m_PreviouslyGrounded && !base.Player.Jump.Active)
		{
			m_CurrentVelocity.y = 0f;
		}
		m_CurrentVelocity += m_DesiredVelocity * m_Acceleration * m_AirControl * Time.deltaTime;
		m_CurrentVelocity.y -= m_Gravity * Time.deltaTime;
	}

	private void CalculateDesiredVelocity()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Vector2.ClampMagnitude(base.Player.MovementInput.Get(), 1f);
		bool num = ((Vector2)(ref val)).sqrMagnitude > 0f;
		Vector3 val2;
		if (!num)
		{
			Vector3 velocity = m_Controller.velocity;
			val2 = ((Vector3)(ref velocity)).normalized;
		}
		else
		{
			val2 = ((Component)this).transform.TransformDirection(new Vector3(val.x, 0f, val.y));
		}
		Vector3 val3 = val2;
		m_DesiredSpeed = 0f;
		if (num)
		{
			m_DesiredSpeed = m_ForwardSpeed * base.Player.MovementSpeedFactor.Get();
			if (Mathf.Abs(val.x) > 0f)
			{
				m_DesiredSpeed = m_SidewaysSpeed;
			}
			if (val.y < 0f)
			{
				m_DesiredSpeed = m_BackwardSpeed;
			}
			if (base.Player.Run.Active && (m_DesiredSpeed == m_ForwardSpeed || m_DesiredSpeed == m_SidewaysSpeed))
			{
				m_DesiredSpeed *= m_RunSpeedMultiplier;
			}
			if (base.Player.Crouch.Active)
			{
				m_DesiredSpeed *= m_CrouchSpeedMultiplier;
			}
		}
		m_DesiredVelocity = val3 * m_DesiredSpeed;
	}

	private bool TryStart_Run()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (!m_EnableRunning)
		{
			return false;
		}
		bool flag = base.Player.MovementInput.Get().y > 0f;
		bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
		bool flag2 = base.Player.Stamina.Get() > 0f;
		if (base.Player.IsGrounded.Get() && flag2 && flag && isClosed && !base.Player.Crouch.Active)
		{
			return !base.Player.Aim.Active;
		}
		return false;
	}

	private bool TryStart_Jump()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (m_EnableJumping && base.Player.Stamina.Get() > 5f && (base.Player.IsGrounded.Get() || base.Player.NearLadders.Count > 0) && (!base.Player.Crouch.Active || base.Player.Crouch.TryStop()))
		{
			Vector3 val = Vector3.up;
			if (base.Player.NearLadders.Count > 0)
			{
				val = -base.Player.NearLadders.Peek().forward;
			}
			m_CurrentVelocity.y = 0f;
			if (base.Player.NearLadders.Count > 0)
			{
				m_CurrentVelocity += val * m_JumpSpeedFromLadder;
				m_JumpedFrom = JumpedFrom.Ladder;
			}
			else
			{
				m_CurrentVelocity += val * CalculateJumpSpeed(m_JumpHeight);
				m_JumpedFrom = JumpedFrom.Ground;
			}
			return true;
		}
		return false;
	}

	private float CalculateJumpSpeed(float heightToReach)
	{
		return Mathf.Sqrt(2f * m_Gravity * heightToReach);
	}

	private bool TryStart_Crouch()
	{
		int num;
		if (m_EnableCrouching && base.Player.NearLadders.Count == 0 && Time.time > m_LastTimeToggledCrouching + m_CrouchDuration && base.Player.IsGrounded.Get())
		{
			num = ((!base.Player.Run.Active) ? 1 : 0);
			if (num != 0)
			{
				((MonoBehaviour)this).StartCoroutine(C_SetHeight(m_CrouchHeight));
				m_LastTimeToggledCrouching = Time.time;
			}
		}
		else
		{
			num = 0;
		}
		return (byte)num != 0;
	}

	private bool TryStop_Crouch()
	{
		bool num = Time.time > m_LastTimeToggledCrouching + m_CrouchDuration;
		bool flag = CheckForObstacles(checkAbove: true, Mathf.Abs(m_CrouchHeight - m_UncrouchedHeight));
		int num2;
		if (num)
		{
			num2 = ((!flag) ? 1 : 0);
			if (num2 != 0)
			{
				((MonoBehaviour)this).StartCoroutine(C_SetHeight(m_UncrouchedHeight));
				m_LastTimeToggledCrouching = Time.time;
			}
		}
		else
		{
			num2 = 0;
		}
		return (byte)num2 != 0;
	}

	private IEnumerator C_SetHeight(float targetHeight)
	{
		float speed = Mathf.Abs(targetHeight - m_Controller.height) / m_CrouchDuration;
		while (Mathf.Abs(targetHeight - m_Controller.height) > Mathf.Epsilon)
		{
			m_Controller.height = Mathf.MoveTowards(m_Controller.height, targetHeight, Time.deltaTime * speed);
			m_Controller.center = Vector3.up * m_Controller.height / 2f;
			yield return null;
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		m_LastSurfaceNormal = hit.normal;
		if (Object.op_Implicit((Object)(object)hit.rigidbody))
		{
			float pushForce = m_PushForce;
			Vector3 velocity = m_Controller.velocity;
			float num = pushForce * ((Vector3)(ref velocity)).magnitude;
			Vector3 val = (hit.moveDirection + Vector3.up * 0.35f) * num;
			hit.rigidbody.AddForceAtPosition(val, hit.point);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (((Component)other).CompareTag("Ladder"))
		{
			base.Player.NearLadders.Enqueue(((Component)other).transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (((Component)other).CompareTag("Ladder"))
		{
			base.Player.NearLadders.Dequeue();
		}
	}

	private bool CheckForObstacles(bool checkAbove, float maxDistance, out RaycastHit hitInfo)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Component)this).transform.position + (checkAbove ? (Vector3.up * m_Controller.height) : Vector3.zero);
		Vector3 val2 = (checkAbove ? Vector3.up : Vector3.down);
		return Physics.SphereCast(new Ray(val, val2), m_Controller.radius, ref hitInfo, maxDistance, -1, (QueryTriggerInteraction)1);
	}

	private bool CheckForObstacles(bool checkAbove, float maxDistance)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Component)this).transform.position + (checkAbove ? (Vector3.up * m_Controller.height) : Vector3.zero);
		Vector3 val2 = (checkAbove ? Vector3.up : Vector3.down);
		return Physics.SphereCast(new Ray(val, val2), m_Controller.radius, maxDistance, -1, (QueryTriggerInteraction)1);
	}
}
