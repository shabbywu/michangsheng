using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200086D RID: 2157
	public class MouseLook : PlayerBehaviour
	{
		// Token: 0x060037DF RID: 14303 RVA: 0x001A14E8 File Offset: 0x0019F6E8
		private void Start()
		{
			if (!this.m_LookRoot)
			{
				Debug.LogErrorFormat(this, "Assign the look root in the inspector!", new object[]
				{
					base.name
				});
				base.enabled = false;
			}
			MonoSingleton<InventoryController>.Instance.State.AddChangeListener(new Action(this.OnChanged_InventoryState));
			Cursor.visible = false;
			Cursor.lockState = 1;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x00028956 File Offset: 0x00026B56
		private void OnChanged_InventoryState()
		{
			this.m_InventoryIsOpen = !MonoSingleton<InventoryController>.Instance.IsClosed;
			if (this.m_InventoryIsOpen)
			{
				Cursor.visible = true;
				Cursor.lockState = 0;
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = 1;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x001A154C File Offset: 0x0019F74C
		private void OnGUI()
		{
			if (!this.m_ShowLockButton)
			{
				return;
			}
			Vector2 vector;
			vector..ctor(256f, 24f);
			if (Event.current.type == 4 && this.m_CanUnlock && Event.current.keyCode == 27)
			{
				Cursor.visible = true;
				Cursor.lockState = 0;
			}
			if (Cursor.lockState == null && !this.m_InventoryIsOpen && GUI.Button(new Rect((float)Screen.width * 0.5f - vector.x / 2f, 16f, vector.x, vector.y), "Lock Cursor (Hit 'Esc' to unlock)"))
			{
				Cursor.visible = false;
				Cursor.lockState = 1;
			}
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x001A15F8 File Offset: 0x0019F7F8
		private void Update()
		{
			if (base.Player.ViewLocked.Is(false) && Cursor.lockState == 1 && !base.Player.Sleep.Active && base.Player.Health.Get() > 0f)
			{
				this.LookAround();
			}
			base.Player.ViewLocked.Set(Cursor.lockState != 1 || base.Player.SelectBuildable.Active);
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x001A167C File Offset: 0x0019F87C
		private void LookAround()
		{
			this.CalculateMouseInput(Time.deltaTime);
			this.m_LookAngles.x = this.m_LookAngles.x + this.m_CurrentMouseLook.x * this.m_Sensitivity * (this.m_Invert ? 1f : -1f);
			this.m_LookAngles.y = this.m_LookAngles.y + this.m_CurrentMouseLook.y * this.m_Sensitivity;
			this.m_LookAngles.x = this.ClampAngle(this.m_LookAngles.x, this.m_DefaultLookLimits.x, this.m_DefaultLookLimits.y);
			this.m_CurrentRollAngle = Mathf.Lerp(this.m_CurrentRollAngle, base.Player.LookInput.Get().x * this.m_RollAngle, Time.deltaTime * this.m_RollSpeed);
			this.m_LookRoot.localRotation = Quaternion.Euler(this.m_LookAngles.x, 0f, this.m_CurrentRollAngle);
			this.m_PlayerRoot.localRotation = Quaternion.Euler(0f, this.m_LookAngles.y, 0f);
			base.Player.LookDirection.Set(this.m_LookRoot.forward);
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0002898C File Offset: 0x00026B8C
		private float ClampAngle(float angle, float min, float max)
		{
			if (angle > 360f)
			{
				angle -= 360f;
			}
			else if (angle < -360f)
			{
				angle += 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x001A17BC File Offset: 0x0019F9BC
		private void CalculateMouseInput(float deltaTime)
		{
			if (this.m_LastLookFrame == Time.frameCount)
			{
				return;
			}
			this.m_LastLookFrame = Time.frameCount;
			this.m_SmoothMove = new Vector2(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
			this.m_SmoothSteps = Mathf.Clamp(this.m_SmoothSteps, 1, 20);
			this.m_SmoothWeight = Mathf.Clamp01(this.m_SmoothWeight);
			while (this.m_SmoothBuffer.Count > this.m_SmoothSteps)
			{
				this.m_SmoothBuffer.RemoveAt(0);
			}
			this.m_SmoothBuffer.Add(this.m_SmoothMove);
			float num = 1f;
			Vector2 vector = Vector2.zero;
			float num2 = 0f;
			for (int i = this.m_SmoothBuffer.Count - 1; i > 0; i--)
			{
				vector += this.m_SmoothBuffer[i] * num;
				num2 += num;
				num *= this.m_SmoothWeight / (deltaTime * 60f);
			}
			num2 = Mathf.Max(1f, num2);
			this.m_CurrentMouseLook = vector / num2;
		}

		// Token: 0x0400321C RID: 12828
		[Header("General")]
		[SerializeField]
		[Tooltip("The camera root which will be rotated up & down (on the X axis).")]
		private Transform m_LookRoot;

		// Token: 0x0400321D RID: 12829
		[SerializeField]
		private Transform m_PlayerRoot;

		// Token: 0x0400321E RID: 12830
		[SerializeField]
		[Tooltip("The up & down rotation will be inverted, if checked.")]
		private bool m_Invert;

		// Token: 0x0400321F RID: 12831
		[SerializeField]
		[Tooltip("If checked, a button will show up which can lock the cursor.")]
		private bool m_ShowLockButton = true;

		// Token: 0x04003220 RID: 12832
		[SerializeField]
		[Tooltip("If checked, you can unlock the cursor by pressing the Escape / Esc key.")]
		private bool m_CanUnlock = true;

		// Token: 0x04003221 RID: 12833
		[Header("Motion")]
		[SerializeField]
		[Tooltip("The higher it is, the faster the camera will rotate.")]
		private float m_Sensitivity = 5f;

		// Token: 0x04003222 RID: 12834
		[SerializeField]
		[Range(0f, 20f)]
		private int m_SmoothSteps = 10;

		// Token: 0x04003223 RID: 12835
		[SerializeField]
		[Range(0f, 1f)]
		private float m_SmoothWeight = 0.4f;

		// Token: 0x04003224 RID: 12836
		[SerializeField]
		private float m_RollAngle = 10f;

		// Token: 0x04003225 RID: 12837
		[SerializeField]
		private float m_RollSpeed = 3f;

		// Token: 0x04003226 RID: 12838
		[Header("Rotation Limits")]
		[SerializeField]
		private Vector2 m_DefaultLookLimits = new Vector2(-60f, 90f);

		// Token: 0x04003227 RID: 12839
		private float m_CurrentRollAngle;

		// Token: 0x04003228 RID: 12840
		private bool m_InventoryIsOpen;

		// Token: 0x04003229 RID: 12841
		private Vector2 m_LookAngles;

		// Token: 0x0400322A RID: 12842
		private int m_LastLookFrame;

		// Token: 0x0400322B RID: 12843
		private Vector2 m_CurrentMouseLook;

		// Token: 0x0400322C RID: 12844
		private Vector2 m_SmoothMove;

		// Token: 0x0400322D RID: 12845
		private List<Vector2> m_SmoothBuffer = new List<Vector2>();
	}
}
