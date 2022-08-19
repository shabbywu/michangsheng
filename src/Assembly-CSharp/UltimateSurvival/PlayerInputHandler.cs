using System;
using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000595 RID: 1429
	public class PlayerInputHandler : PlayerBehaviour
	{
		// Token: 0x06002F08 RID: 12040 RVA: 0x00155BAC File Offset: 0x00153DAC
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

		// Token: 0x06002F09 RID: 12041 RVA: 0x00155BEC File Offset: 0x00153DEC
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

		// Token: 0x06002F0A RID: 12042 RVA: 0x00155D74 File Offset: 0x00153F74
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

		// Token: 0x06002F0B RID: 12043 RVA: 0x00156129 File Offset: 0x00154329
		private void OnSucceded_PlaceObject()
		{
			base.Player.CanShowObjectPreview.Set(false);
		}

		// Token: 0x04002956 RID: 10582
		[SerializeField]
		private bool m_ShowControls;

		// Token: 0x04002957 RID: 10583
		private MGInputManager m_Input;
	}
}
