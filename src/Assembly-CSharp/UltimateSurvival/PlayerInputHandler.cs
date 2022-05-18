using System;
using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200083F RID: 2111
	public class PlayerInputHandler : PlayerBehaviour
	{
		// Token: 0x0600377E RID: 14206 RVA: 0x00028481 File Offset: 0x00026681
		private void Awake()
		{
			if (GameController.InputManager)
			{
				this.m_Input = GameController.InputManager;
			}
			else
			{
				base.enabled = false;
			}
			base.Player.PlaceObject.AddListener(new Action(this.OnSucceded_PlaceObject));
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x001A03A4 File Offset: 0x0019E5A4
		private void OnGUI()
		{
			if (!this.m_ShowControls)
			{
				return;
			}
			Rect rect;
			rect..ctor((float)Screen.width - 8f - 150f, 32f, 150f, 20f);
			GUI.Label(rect, "Unlock cursor - Escape");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Inventory - TAB");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Move - WASD");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Attack - Left Click");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Aim - Right Click");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Crouch - C");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Run - Left Shift");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Interact - E");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Rotate - Arrows");
			rect.y = rect.yMax + 4f;
			GUI.Label(rect, "Wheel - Right Click");
			rect.y = rect.yMax + 4f;
			if (GUI.Button(rect, "Quit!"))
			{
				Application.Quit();
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x001A052C File Offset: 0x0019E72C
		private void Update()
		{
			if (!MonoSingleton<InventoryController>.Instance.IsClosed && this.m_Input.GetButtonDown("Close Inventory"))
			{
				MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Closed);
			}
			if (MonoSingleton<InventoryController>.Instance.IsClosed && this.m_Input.GetButtonDown("Open Inventory"))
			{
				MonoSingleton<InventoryController>.Instance.SetState.Try(ET.InventoryState.Normal);
			}
			float axisRaw = this.m_Input.GetAxisRaw("Rotate Object");
			if (Mathf.Abs(axisRaw) > 0f)
			{
				base.Player.RotateObject.Try(axisRaw);
			}
			if (this.m_Input.GetButtonDown("Select Buildable"))
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
			if (MonoSingleton<InventoryController>.Instance.IsClosed && base.Player.ViewLocked.Is(false))
			{
				Vector2 value;
				value..ctor(this.m_Input.GetAxis("Horizontal"), this.m_Input.GetAxis("Vertical"));
				base.Player.MovementInput.Set(value);
				base.Player.LookInput.Set(new Vector2(this.m_Input.GetAxisRaw("Mouse X"), this.m_Input.GetAxisRaw("Mouse Y")));
				if (this.m_Input.GetButtonDown("Interact"))
				{
					base.Player.InteractOnce.Try();
				}
				base.Player.InteractContinuously.Set(this.m_Input.GetButton("Interact"));
				if (this.m_Input.GetButtonDown("Jump"))
				{
					base.Player.Jump.TryStart();
				}
				bool button = this.m_Input.GetButton("Run");
				bool flag = base.Player.IsGrounded.Get() && base.Player.MovementInput.Get().y > 0f;
				if (!base.Player.Run.Active && button && flag)
				{
					base.Player.Run.TryStart();
				}
				if (base.Player.Run.Active && !button)
				{
					base.Player.Run.ForceStop();
				}
				if (this.m_Input.GetButtonDown("Crouch"))
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
				if (this.m_Input.GetButton("Attack"))
				{
					base.Player.AttackContinuously.Try();
				}
				if (this.m_Input.GetButtonDown("Attack"))
				{
					base.Player.AttackOnce.Try();
				}
				if (this.m_Input.GetButtonDown("Aim"))
				{
					base.Player.Aim.TryStart();
				}
				else if (this.m_Input.GetButtonUp("Aim"))
				{
					base.Player.Aim.ForceStop();
				}
				if (!this.m_Input.GetButtonDown("Place Object"))
				{
					base.Player.CanShowObjectPreview.Set(true);
				}
			}
			else
			{
				base.Player.MovementInput.Set(Vector2.zero);
				base.Player.LookInput.Set(Vector2.zero);
			}
			float axis = this.m_Input.GetAxis("Mouse ScrollWheel");
			base.Player.ScrollValue.Set(axis);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000284BF File Offset: 0x000266BF
		private void OnSucceded_PlaceObject()
		{
			base.Player.CanShowObjectPreview.Set(false);
		}

		// Token: 0x04003198 RID: 12696
		[SerializeField]
		private bool m_ShowControls;

		// Token: 0x04003199 RID: 12697
		private MGInputManager m_Input;
	}
}
