using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B1 RID: 1457
	public class MouseLook : PlayerBehaviour
	{
		// Token: 0x06002F61 RID: 12129 RVA: 0x00157080 File Offset: 0x00155280
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

		// Token: 0x06002F62 RID: 12130 RVA: 0x001570E2 File Offset: 0x001552E2
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

		// Token: 0x06002F63 RID: 12131 RVA: 0x00157118 File Offset: 0x00155318
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

		// Token: 0x06002F64 RID: 12132 RVA: 0x001571C4 File Offset: 0x001553C4
		private void Update()
		{
			if (base.Player.ViewLocked.Is(false) && Cursor.lockState == 1 && !base.Player.Sleep.Active && base.Player.Health.Get() > 0f)
			{
				this.LookAround();
			}
			base.Player.ViewLocked.Set(Cursor.lockState != 1 || base.Player.SelectBuildable.Active);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x00157248 File Offset: 0x00155448
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

		// Token: 0x06002F66 RID: 12134 RVA: 0x00157388 File Offset: 0x00155588
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

		// Token: 0x06002F67 RID: 12135 RVA: 0x001573B8 File Offset: 0x001555B8
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

		// Token: 0x0400299B RID: 10651
		[Header("General")]
		[SerializeField]
		[Tooltip("The camera root which will be rotated up & down (on the X axis).")]
		private Transform m_LookRoot;

		// Token: 0x0400299C RID: 10652
		[SerializeField]
		private Transform m_PlayerRoot;

		// Token: 0x0400299D RID: 10653
		[SerializeField]
		[Tooltip("The up & down rotation will be inverted, if checked.")]
		private bool m_Invert;

		// Token: 0x0400299E RID: 10654
		[SerializeField]
		[Tooltip("If checked, a button will show up which can lock the cursor.")]
		private bool m_ShowLockButton = true;

		// Token: 0x0400299F RID: 10655
		[SerializeField]
		[Tooltip("If checked, you can unlock the cursor by pressing the Escape / Esc key.")]
		private bool m_CanUnlock = true;

		// Token: 0x040029A0 RID: 10656
		[Header("Motion")]
		[SerializeField]
		[Tooltip("The higher it is, the faster the camera will rotate.")]
		private float m_Sensitivity = 5f;

		// Token: 0x040029A1 RID: 10657
		[SerializeField]
		[Range(0f, 20f)]
		private int m_SmoothSteps = 10;

		// Token: 0x040029A2 RID: 10658
		[SerializeField]
		[Range(0f, 1f)]
		private float m_SmoothWeight = 0.4f;

		// Token: 0x040029A3 RID: 10659
		[SerializeField]
		private float m_RollAngle = 10f;

		// Token: 0x040029A4 RID: 10660
		[SerializeField]
		private float m_RollSpeed = 3f;

		// Token: 0x040029A5 RID: 10661
		[Header("Rotation Limits")]
		[SerializeField]
		private Vector2 m_DefaultLookLimits = new Vector2(-60f, 90f);

		// Token: 0x040029A6 RID: 10662
		private float m_CurrentRollAngle;

		// Token: 0x040029A7 RID: 10663
		private bool m_InventoryIsOpen;

		// Token: 0x040029A8 RID: 10664
		private Vector2 m_LookAngles;

		// Token: 0x040029A9 RID: 10665
		private int m_LastLookFrame;

		// Token: 0x040029AA RID: 10666
		private Vector2 m_CurrentMouseLook;

		// Token: 0x040029AB RID: 10667
		private Vector2 m_SmoothMove;

		// Token: 0x040029AC RID: 10668
		private List<Vector2> m_SmoothBuffer = new List<Vector2>();
	}
}
