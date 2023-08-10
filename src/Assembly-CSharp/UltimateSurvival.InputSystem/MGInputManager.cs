using UnityEngine;

namespace UltimateSurvival.InputSystem;

public class MGInputManager : MonoBehaviour
{
	[SerializeField]
	private InputData m_InputData;

	public InputData InputData
	{
		get
		{
			return m_InputData;
		}
		set
		{
			m_InputData = value;
		}
	}

	public void SetupDefaults(ET.InputType inputType, ET.InputMode inputMode)
	{
		switch (inputType)
		{
		case ET.InputType.Standalone:
			if (inputMode == ET.InputMode.Axes)
			{
				AddAxis(new Axis("Horizontal Axis", ET.StandaloneAxisType.Unity, "Horizontal"));
				AddAxis(new Axis("Vertical Axis", ET.StandaloneAxisType.Unity, "Vertical"));
				AddAxis(new Axis("Mouse X", ET.StandaloneAxisType.Unity, "Mouse X"));
				AddAxis(new Axis("Mouse Y", ET.StandaloneAxisType.Unity, "Mouse Y"));
			}
			else
			{
				AddDefaultButtons();
			}
			break;
		case ET.InputType.Mobile:
			if (inputMode == ET.InputMode.Axes)
			{
				AddAxis(new Axis("Movement Axis", ET.StandaloneAxisType.Custom, new Joystick()));
			}
			else
			{
				AddDefaultButtons();
			}
			break;
		}
	}

	private void AddDefaultButtons()
	{
		AddButton(new Button("Sprint", (KeyCode)304));
		AddButton(new Button("Attack", (KeyCode)323));
		AddButton(new Button("Jump", (KeyCode)32));
		AddButton(new Button("Crouch", (KeyCode)99));
		AddButton(new Button("Reload", (KeyCode)114));
	}

	public void Clear(ET.InputMode inputMode)
	{
		switch (inputMode)
		{
		case ET.InputMode.Axes:
			m_InputData.Axes.Clear();
			break;
		case ET.InputMode.Buttons:
			m_InputData.Buttons.Clear();
			break;
		}
	}

	public void ClearAll()
	{
		m_InputData.Axes.Clear();
		m_InputData.Buttons.Clear();
	}

	public float GetAxis(string name)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Axis axis = FindAxis(name);
		float num = 0f;
		if (axis != null)
		{
			if (axis.AxisType == ET.StandaloneAxisType.Unity)
			{
				num += Input.GetAxis(axis.UnityAxisName);
			}
			if (axis.AxisType == ET.StandaloneAxisType.Custom)
			{
				num += (float)(-GetKeyPress(axis.NegativeKey) + GetKeyPress(axis.PositiveKey));
			}
		}
		if (!axis.Normalize || axis.AxisType == ET.StandaloneAxisType.Unity)
		{
			return num;
		}
		return Mathf.Clamp(num, -1f, 1f);
	}

	public float GetAxisRaw(string name)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Axis axis = FindAxis(name);
		float num = 0f;
		if (axis != null)
		{
			if (axis.AxisType == ET.StandaloneAxisType.Unity)
			{
				num += Input.GetAxisRaw(axis.UnityAxisName);
			}
			if (axis.AxisType == ET.StandaloneAxisType.Custom)
			{
				num += (float)(-GetKeyPress(axis.NegativeKey) + GetKeyPress(axis.PositiveKey));
			}
		}
		if (!axis.Normalize || axis.AxisType == ET.StandaloneAxisType.Unity)
		{
			return num;
		}
		return Mathf.Clamp(num, -1f, 1f);
	}

	public bool GetButton(string name)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Button button = FindButton(name);
		bool result = false;
		if (button != null)
		{
			result = Input.GetKey(button.Key);
		}
		return result;
	}

	public bool GetButtonDown(string name)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Button button = FindButton(name);
		bool result = false;
		if (button != null)
		{
			result = Input.GetKeyDown(button.Key);
		}
		return result;
	}

	public bool GetButtonUp(string name)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		Button button = FindButton(name);
		bool result = false;
		if (button != null)
		{
			result = Input.GetKeyUp(button.Key);
		}
		return result;
	}

	private void AddButton(Button toAdd)
	{
		m_InputData.Buttons.Add(toAdd);
	}

	private void AddAxis(Axis toAdd)
	{
		m_InputData.Axes.Add(toAdd);
	}

	private Button FindButton(string name)
	{
		for (int i = 0; i < m_InputData.Buttons.Count; i++)
		{
			if (name == m_InputData.Buttons[i].Name)
			{
				return m_InputData.Buttons[i];
			}
		}
		return null;
	}

	private Axis FindAxis(string name)
	{
		for (int i = 0; i < m_InputData.Axes.Count; i++)
		{
			if (name == m_InputData.Axes[i].AxisName)
			{
				return m_InputData.Axes[i];
			}
		}
		return null;
	}

	private int GetKeyPress(KeyCode key)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKey(key))
		{
			return 1;
		}
		return 0;
	}
}
