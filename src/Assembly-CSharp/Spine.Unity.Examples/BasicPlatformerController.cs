using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Examples;

[RequireComponent(typeof(CharacterController))]
public class BasicPlatformerController : MonoBehaviour
{
	public enum CharacterState
	{
		None,
		Idle,
		Walk,
		Run,
		Crouch,
		Rise,
		Fall,
		Attack
	}

	[Header("Components")]
	public CharacterController controller;

	[Header("Controls")]
	public string XAxis = "Horizontal";

	public string YAxis = "Vertical";

	public string JumpButton = "Jump";

	[Header("Moving")]
	public float walkSpeed = 1.5f;

	public float runSpeed = 7f;

	public float gravityScale = 6.6f;

	[Header("Jumping")]
	public float jumpSpeed = 25f;

	public float minimumJumpDuration = 0.5f;

	public float jumpInterruptFactor = 0.5f;

	public float forceCrouchVelocity = 25f;

	public float forceCrouchDuration = 0.5f;

	[Header("Animation")]
	public SkeletonAnimationHandleExample animationHandle;

	[CompilerGenerated]
	private UnityAction m_OnJump;

	[CompilerGenerated]
	private UnityAction m_OnLand;

	[CompilerGenerated]
	private UnityAction m_OnHardLand;

	private Vector2 input;

	private Vector3 velocity;

	private float minimumJumpEndTime;

	private float forceCrouchEndTime;

	private bool wasGrounded;

	private CharacterState previousState;

	private CharacterState currentState;

	public event UnityAction OnJump
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnJump;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Combine((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnJump, value2, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnJump;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Remove((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnJump, value2, val2);
			}
			while (val != val2);
		}
	}

	public event UnityAction OnLand
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnLand;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Combine((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnLand, value2, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnLand;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Remove((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnLand, value2, val2);
			}
			while (val != val2);
		}
	}

	public event UnityAction OnHardLand
	{
		[CompilerGenerated]
		add
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnHardLand;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Combine((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnHardLand, value2, val2);
			}
			while (val != val2);
		}
		[CompilerGenerated]
		remove
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			UnityAction val = this.m_OnHardLand;
			UnityAction val2;
			do
			{
				val2 = val;
				UnityAction value2 = (UnityAction)Delegate.Remove((Delegate?)(object)val2, (Delegate?)(object)value);
				val = Interlocked.CompareExchange(ref this.m_OnHardLand, value2, val2);
			}
			while (val != val2);
		}
	}

	private void Update()
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = Time.deltaTime;
		bool isGrounded = controller.isGrounded;
		bool num = !wasGrounded && isGrounded;
		input.x = Input.GetAxis(XAxis);
		input.y = Input.GetAxis(YAxis);
		bool buttonUp = Input.GetButtonUp(JumpButton);
		bool buttonDown = Input.GetButtonDown(JumpButton);
		bool flag = (isGrounded && input.y < -0.5f) || forceCrouchEndTime > Time.time;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (num && 0f - velocity.y > forceCrouchVelocity)
		{
			flag4 = true;
			flag = true;
			forceCrouchEndTime = Time.time + forceCrouchDuration;
		}
		if (!flag)
		{
			if (isGrounded)
			{
				if (buttonDown)
				{
					flag3 = true;
				}
			}
			else
			{
				flag2 = buttonUp && Time.time < minimumJumpEndTime;
			}
		}
		Vector3 val = Physics.gravity * gravityScale * deltaTime;
		if (flag3)
		{
			velocity.y = jumpSpeed;
			minimumJumpEndTime = Time.time + minimumJumpDuration;
		}
		else if (flag2 && velocity.y > 0f)
		{
			velocity.y *= jumpInterruptFactor;
		}
		velocity.x = 0f;
		if (!flag && input.x != 0f)
		{
			velocity.x = ((Mathf.Abs(input.x) > 0.6f) ? runSpeed : walkSpeed);
			velocity.x *= Mathf.Sign(input.x);
		}
		if (!isGrounded)
		{
			if (wasGrounded)
			{
				if (velocity.y < 0f)
				{
					velocity.y = 0f;
				}
			}
			else
			{
				velocity += val;
			}
		}
		controller.Move(velocity * deltaTime);
		wasGrounded = isGrounded;
		if (isGrounded)
		{
			if (flag)
			{
				currentState = CharacterState.Crouch;
			}
			else if (input.x == 0f)
			{
				currentState = CharacterState.Idle;
			}
			else
			{
				currentState = ((Mathf.Abs(input.x) > 0.6f) ? CharacterState.Run : CharacterState.Walk);
			}
		}
		else
		{
			currentState = ((velocity.y > 0f) ? CharacterState.Rise : CharacterState.Fall);
		}
		bool num2 = previousState != currentState;
		previousState = currentState;
		if (num2)
		{
			HandleStateChanged();
		}
		if (input.x != 0f)
		{
			animationHandle.SetFlip(input.x);
		}
		if (flag3)
		{
			this.OnJump.Invoke();
		}
		if (num)
		{
			if (flag4)
			{
				this.OnHardLand.Invoke();
			}
			else
			{
				this.OnLand.Invoke();
			}
		}
	}

	private void HandleStateChanged()
	{
		string stateShortName = null;
		switch (currentState)
		{
		case CharacterState.Idle:
			stateShortName = "idle";
			break;
		case CharacterState.Walk:
			stateShortName = "walk";
			break;
		case CharacterState.Run:
			stateShortName = "run";
			break;
		case CharacterState.Crouch:
			stateShortName = "crouch";
			break;
		case CharacterState.Rise:
			stateShortName = "rise";
			break;
		case CharacterState.Fall:
			stateShortName = "fall";
			break;
		case CharacterState.Attack:
			stateShortName = "attack";
			break;
		}
		animationHandle.PlayAnimationForState(stateShortName, 0);
	}
}
