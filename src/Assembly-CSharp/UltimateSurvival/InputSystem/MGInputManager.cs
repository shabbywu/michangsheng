using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem
{
	// Token: 0x02000636 RID: 1590
	public class MGInputManager : MonoBehaviour
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x00165611 File Offset: 0x00163811
		// (set) Token: 0x0600326E RID: 12910 RVA: 0x00165619 File Offset: 0x00163819
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

		// Token: 0x0600326F RID: 12911 RVA: 0x00165624 File Offset: 0x00163824
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

		// Token: 0x06003270 RID: 12912 RVA: 0x001656C0 File Offset: 0x001638C0
		private void AddDefaultButtons()
		{
			this.AddButton(new Button("Sprint", 304));
			this.AddButton(new Button("Attack", 323));
			this.AddButton(new Button("Jump", 32));
			this.AddButton(new Button("Crouch", 99));
			this.AddButton(new Button("Reload", 114));
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x0016572D File Offset: 0x0016392D
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

		// Token: 0x06003272 RID: 12914 RVA: 0x00165757 File Offset: 0x00163957
		public void ClearAll()
		{
			this.m_InputData.Axes.Clear();
			this.m_InputData.Buttons.Clear();
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0016577C File Offset: 0x0016397C
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

		// Token: 0x06003274 RID: 12916 RVA: 0x001657FC File Offset: 0x001639FC
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

		// Token: 0x06003275 RID: 12917 RVA: 0x0016587C File Offset: 0x00163A7C
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

		// Token: 0x06003276 RID: 12918 RVA: 0x001658A4 File Offset: 0x00163AA4
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

		// Token: 0x06003277 RID: 12919 RVA: 0x001658CC File Offset: 0x00163ACC
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

		// Token: 0x06003278 RID: 12920 RVA: 0x001658F3 File Offset: 0x00163AF3
		private void AddButton(Button toAdd)
		{
			this.m_InputData.Buttons.Add(toAdd);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x00165906 File Offset: 0x00163B06
		private void AddAxis(Axis toAdd)
		{
			this.m_InputData.Axes.Add(toAdd);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x0016591C File Offset: 0x00163B1C
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

		// Token: 0x0600327B RID: 12923 RVA: 0x00165978 File Offset: 0x00163B78
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

		// Token: 0x0600327C RID: 12924 RVA: 0x001659D1 File Offset: 0x00163BD1
		private int GetKeyPress(KeyCode key)
		{
			if (Input.GetKey(key))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04002CF4 RID: 11508
		[SerializeField]
		private InputData m_InputData;
	}
}
