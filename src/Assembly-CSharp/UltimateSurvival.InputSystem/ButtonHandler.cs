using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem;

public class ButtonHandler : MonoBehaviour
{
	public ET.ButtonState State { get; private set; }

	public event Action OnButtonDown;

	private void Start()
	{
		State = ET.ButtonState.Up;
	}

	public void SetUpState()
	{
		State = ET.ButtonState.Up;
	}

	public void SetDownState()
	{
		State = ET.ButtonState.Down;
		if (this.OnButtonDown != null)
		{
			this.OnButtonDown();
		}
	}
}
