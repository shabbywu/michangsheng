using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival;

public class PlayerInputHandler : PlayerBehaviour
{
	[SerializeField]
	private bool m_ShowControls;

	private MGInputManager m_Input;

	private void Awake()
	{
		if (Object.op_Implicit((Object)(object)GameController.InputManager))
		{
			m_Input = GameController.InputManager;
		}
		else
		{
			((Behaviour)this).enabled = false;
		}
		base.Player.PlaceObject.AddListener(OnSucceded_PlaceObject);
	}

	private void OnGUI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		if (m_ShowControls)
		{
			Rect val = default(Rect);
			((Rect)(ref val))._002Ector((float)Screen.width - 8f - 150f, 32f, 150f, 20f);
			GUI.Label(val, "Unlock cursor - Escape");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Inventory - TAB");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Move - WASD");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Attack - Left Click");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Aim - Right Click");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Crouch - C");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Run - Left Shift");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Interact - E");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Rotate - Arrows");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			GUI.Label(val, "Wheel - Right Click");
			((Rect)(ref val)).y = ((Rect)(ref val)).yMax + 4f;
			if (GUI.Button(val, "Quit!"))
			{
				Application.Quit();
			}
		}
	}

	private void Update()
	{
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoSingleton<InventoryController>.Instance.IsClosed && m_Input.GetButtonDown("Close Inventory"))
		{
			MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Closed);
		}
		if (MonoSingleton<InventoryController>.Instance.IsClosed && m_Input.GetButtonDown("Open Inventory"))
		{
			MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Normal);
		}
		float axisRaw = m_Input.GetAxisRaw("Rotate Object");
		if (Mathf.Abs(axisRaw) > 0f)
		{
			base.Player.RotateObject.Try(axisRaw);
		}
		if (m_Input.GetButtonDown("Select Buildable"))
		{
			if (!base.Player.SelectBuildable.Active)
			{
				base.Player.SelectBuildable.TryStart();
			}
			else
			{
				base.Player.SelectBuildable.TryStop();
			}
		}
		if (MonoSingleton<InventoryController>.Instance.IsClosed && base.Player.ViewLocked.Is(value: false))
		{
			Vector2 value = default(Vector2);
			((Vector2)(ref value))._002Ector(m_Input.GetAxis("Horizontal"), m_Input.GetAxis("Vertical"));
			base.Player.MovementInput.Set(value);
			base.Player.LookInput.Set(new Vector2(m_Input.GetAxisRaw("Mouse X"), m_Input.GetAxisRaw("Mouse Y")));
			if (m_Input.GetButtonDown("Interact"))
			{
				base.Player.InteractOnce.Try();
			}
			base.Player.InteractContinuously.Set(m_Input.GetButton("Interact"));
			if (m_Input.GetButtonDown("Jump"))
			{
				base.Player.Jump.TryStart();
			}
			bool button = m_Input.GetButton("Run");
			bool flag = base.Player.IsGrounded.Get() && base.Player.MovementInput.Get().y > 0f;
			if (!base.Player.Run.Active && button && flag)
			{
				base.Player.Run.TryStart();
			}
			if (base.Player.Run.Active && !button)
			{
				base.Player.Run.ForceStop();
			}
			if (m_Input.GetButtonDown("Crouch"))
			{
				if (!base.Player.Crouch.Active)
				{
					base.Player.Crouch.TryStart();
				}
				else
				{
					base.Player.Crouch.TryStop();
				}
			}
			if (m_Input.GetButton("Attack"))
			{
				base.Player.AttackContinuously.Try();
			}
			if (m_Input.GetButtonDown("Attack"))
			{
				base.Player.AttackOnce.Try();
			}
			if (m_Input.GetButtonDown("Aim"))
			{
				base.Player.Aim.TryStart();
			}
			else if (m_Input.GetButtonUp("Aim"))
			{
				base.Player.Aim.ForceStop();
			}
			if (!m_Input.GetButtonDown("Place Object"))
			{
				base.Player.CanShowObjectPreview.Set(value: true);
			}
		}
		else
		{
			base.Player.MovementInput.Set(Vector2.zero);
			base.Player.LookInput.Set(Vector2.zero);
		}
		float axis = m_Input.GetAxis("Mouse ScrollWheel");
		base.Player.ScrollValue.Set(axis);
	}

	private void OnSucceded_PlaceObject()
	{
		base.Player.CanShowObjectPreview.Set(value: false);
	}
}
