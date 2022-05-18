using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000928 RID: 2344
	public class MGInputManager : MonoBehaviour
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x0002B259 File Offset: 0x00029459
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x0002B261 File Offset: 0x00029461
		public InputData InputData
		{
			get
			{
				return this.m_InputData;
			}
			set
			{
				this.m_InputData = value;
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x001AEB9C File Offset: 0x001ACD9C
		public void SetupDefaults(ET.InputType inputType, ET.InputMode inputMode)
		{
			if (inputType != ET.InputType.Standalone)
			{
				if (inputType == ET.InputType.Mobile)
				{
					if (inputMode == ET.InputMode.Axes)
					{
						this.AddAxis(new Axis("Movement Axis", ET.StandaloneAxisType.Custom, new Joystick()));
						return;
					}
					this.AddDefaultButtons();
				}
				return;
			}
			if (inputMode == ET.InputMode.Axes)
			{
				this.AddAxis(new Axis("Horizontal Axis", ET.StandaloneAxisType.Unity, "Horizontal"));
				this.AddAxis(new Axis("Vertical Axis", ET.StandaloneAxisType.Unity, "Vertical"));
				this.AddAxis(new Axis("Mouse X", ET.StandaloneAxisType.Unity, "Mouse X"));
				this.AddAxis(new Axis("Mouse Y", ET.StandaloneAxisType.Unity, "Mouse Y"));
				return;
			}
			this.AddDefaultButtons();
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x001AEC38 File Offset: 0x001ACE38
		private void AddDefaultButtons()
		{
			this.AddButton(new Button("Sprint", 304));
			this.AddButton(new Button("Attack", 323));
			this.AddButton(new Button("Jump", 32));
			this.AddButton(new Button("Crouch", 99));
			this.AddButton(new Button("Reload", 114));
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0002B26A File Offset: 0x0002946A
		public void Clear(ET.InputMode inputMode)
		{
			if (inputMode == ET.InputMode.Axes)
			{
				this.m_InputData.Axes.Clear();
				return;
			}
			if (inputMode == ET.InputMode.Buttons)
			{
				this.m_InputData.Buttons.Clear();
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x0002B294 File Offset: 0x00029494
		public void ClearAll()
		{
			this.m_InputData.Axes.Clear();
			this.m_InputData.Buttons.Clear();
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x001AECA8 File Offset: 0x001ACEA8
		public float GetAxis(string name)
		{
			Axis axis = this.FindAxis(name);
			float num = 0f;
			if (axis != null)
			{
				if (axis.AxisType == ET.StandaloneAxisType.Unity)
				{
					num += Input.GetAxis(axis.UnityAxisName);
				}
				if (axis.AxisType == ET.StandaloneAxisType.Custom)
				{
					num += (float)(-(float)this.GetKeyPress(axis.NegativeKey) + this.GetKeyPress(axis.PositiveKey));
				}
			}
			if (!axis.Normalize || axis.AxisType == ET.StandaloneAxisType.Unity)
			{
				return num;
			}
			return Mathf.Clamp(num, -1f, 1f);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x001AED28 File Offset: 0x001ACF28
		public float GetAxisRaw(string name)
		{
			Axis axis = this.FindAxis(name);
			float num = 0f;
			if (axis != null)
			{
				if (axis.AxisType == ET.StandaloneAxisType.Unity)
				{
					num += Input.GetAxisRaw(axis.UnityAxisName);
				}
				if (axis.AxisType == ET.StandaloneAxisType.Custom)
				{
					num += (float)(-(float)this.GetKeyPress(axis.NegativeKey) + this.GetKeyPress(axis.PositiveKey));
				}
			}
			if (!axis.Normalize || axis.AxisType == ET.StandaloneAxisType.Unity)
			{
				return num;
			}
			return Mathf.Clamp(num, -1f, 1f);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x001AEDA8 File Offset: 0x001ACFA8
		public bool GetButton(string name)
		{
			Button button = this.FindButton(name);
			bool result = false;
			if (button != null)
			{
				result = Input.GetKey(button.Key);
			}
			return result;
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x001AEDD0 File Offset: 0x001ACFD0
		public bool GetButtonDown(string name)
		{
			Button button = this.FindButton(name);
			bool result = false;
			if (button != null)
			{
				result = Input.GetKeyDown(button.Key);
			}
			return result;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x001AEDF8 File Offset: 0x001ACFF8
		public bool GetButtonUp(string name)
		{
			Button button = this.FindButton(name);
			bool result = false;
			if (button != null)
			{
				result = Input.GetKeyUp(button.Key);
			}
			return result;
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x0002B2B6 File Offset: 0x000294B6
		private void AddButton(Button toAdd)
		{
			this.m_InputData.Buttons.Add(toAdd);
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x0002B2C9 File Offset: 0x000294C9
		private void AddAxis(Axis toAdd)
		{
			this.m_InputData.Axes.Add(toAdd);
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x001AEE20 File Offset: 0x001AD020
		private Button FindButton(string name)
		{
			for (int i = 0; i < this.m_InputData.Buttons.Count; i++)
			{
				if (name == this.m_InputData.Buttons[i].Name)
				{
					return this.m_InputData.Buttons[i];
				}
			}
			return null;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x001AEE7C File Offset: 0x001AD07C
		private Axis FindAxis(string name)
		{
			for (int i = 0; i < this.m_InputData.Axes.Count; i++)
			{
				if (name == this.m_InputData.Axes[i].AxisName)
				{
					return this.m_InputData.Axes[i];
				}
			}
			return null;
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x0002B2DC File Offset: 0x000294DC
		private int GetKeyPress(KeyCode key)
		{
			if (Input.GetKey(key))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04003645 RID: 13893
		[SerializeField]
		private InputData m_InputData;
	}
}
