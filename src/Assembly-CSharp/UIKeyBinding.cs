using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	public enum Action
	{
		PressAndClick,
		Select
	}

	public enum Modifier
	{
		None,
		Shift,
		Control,
		Alt
	}

	public KeyCode keyCode;

	public Modifier modifier;

	public Action action;

	private bool mIgnoreUp;

	private bool mIsInput;

	private bool mPress;

	private void Start()
	{
		UIInput component = ((Component)this).GetComponent<UIInput>();
		mIsInput = (Object)(object)component != (Object)null;
		if ((Object)(object)component != (Object)null)
		{
			EventDelegate.Add(component.onSubmit, OnSubmit);
		}
	}

	private void OnSubmit()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentKey == keyCode && IsModifierActive())
		{
			mIgnoreUp = true;
		}
	}

	private bool IsModifierActive()
	{
		if (modifier == Modifier.None)
		{
			return true;
		}
		if (modifier == Modifier.Alt)
		{
			if (Input.GetKey((KeyCode)308) || Input.GetKey((KeyCode)307))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Control)
		{
			if (Input.GetKey((KeyCode)306) || Input.GetKey((KeyCode)305))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Shift && (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303)))
		{
			return true;
		}
		return false;
	}

	private void Update()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if ((int)keyCode == 0 || !IsModifierActive())
		{
			return;
		}
		if (action == Action.PressAndClick)
		{
			if (UICamera.inputHasFocus)
			{
				return;
			}
			UICamera.currentTouch = UICamera.controller;
			UICamera.currentScheme = UICamera.ControlScheme.Mouse;
			UICamera.currentTouch.current = ((Component)this).gameObject;
			if (Input.GetKeyDown(keyCode))
			{
				mPress = true;
				UICamera.Notify(((Component)this).gameObject, "OnPress", true);
			}
			if (Input.GetKeyUp(keyCode))
			{
				UICamera.Notify(((Component)this).gameObject, "OnPress", false);
				if (mPress)
				{
					UICamera.Notify(((Component)this).gameObject, "OnClick", null);
					mPress = false;
				}
			}
			UICamera.currentTouch.current = null;
		}
		else
		{
			if (action != Action.Select || !Input.GetKeyUp(keyCode))
			{
				return;
			}
			if (mIsInput)
			{
				if (!mIgnoreUp && !UICamera.inputHasFocus)
				{
					UICamera.selectedObject = ((Component)this).gameObject;
				}
				mIgnoreUp = false;
			}
			else
			{
				UICamera.selectedObject = ((Component)this).gameObject;
			}
		}
	}
}
