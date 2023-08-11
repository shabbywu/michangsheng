using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.InputSystem;

public class InputData : ScriptableObject
{
	[SerializeField]
	private ET.InputType m_InputType;

	[SerializeField]
	private List<Button> m_Buttons = new List<Button>();

	[SerializeField]
	private List<Axis> m_Axes = new List<Axis>();

	public ET.InputType InputType => m_InputType;

	public List<Button> Buttons => m_Buttons;

	public List<Axis> Axes => m_Axes;
}
