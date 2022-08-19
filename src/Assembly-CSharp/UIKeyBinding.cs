using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x06000538 RID: 1336 RVA: 0x0001C81C File Offset: 0x0001AA1C
	private void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001C85E File Offset: 0x0001AA5E
	private void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0001C87C File Offset: 0x0001AA7C
	private bool IsModifierActive()
	{
		if (this.modifier == UIKeyBinding.Modifier.None)
		{
			return true;
		}
		if (this.modifier == UIKeyBinding.Modifier.Alt)
		{
			if (Input.GetKey(308) || Input.GetKey(307))
			{
				return true;
			}
		}
		else if (this.modifier == UIKeyBinding.Modifier.Control)
		{
			if (Input.GetKey(306) || Input.GetKey(305))
			{
				return true;
			}
		}
		else if (this.modifier == UIKeyBinding.Modifier.Shift && (Input.GetKey(304) || Input.GetKey(303)))
		{
			return true;
		}
		return false;
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001C900 File Offset: 0x0001AB00
	private void Update()
	{
		if (this.keyCode == null || !this.IsModifierActive())
		{
			return;
		}
		if (this.action != UIKeyBinding.Action.PressAndClick)
		{
			if (this.action == UIKeyBinding.Action.Select && Input.GetKeyUp(this.keyCode))
			{
				if (this.mIsInput)
				{
					if (!this.mIgnoreUp && !UICamera.inputHasFocus)
					{
						UICamera.selectedObject = base.gameObject;
					}
					this.mIgnoreUp = false;
					return;
				}
				UICamera.selectedObject = base.gameObject;
			}
			return;
		}
		if (UICamera.inputHasFocus)
		{
			return;
		}
		UICamera.currentTouch = UICamera.controller;
		UICamera.currentScheme = UICamera.ControlScheme.Mouse;
		UICamera.currentTouch.current = base.gameObject;
		if (Input.GetKeyDown(this.keyCode))
		{
			this.mPress = true;
			UICamera.Notify(base.gameObject, "OnPress", true);
		}
		if (Input.GetKeyUp(this.keyCode))
		{
			UICamera.Notify(base.gameObject, "OnPress", false);
			if (this.mPress)
			{
				UICamera.Notify(base.gameObject, "OnClick", null);
				this.mPress = false;
			}
		}
		UICamera.currentTouch.current = null;
	}

	// Token: 0x0400035E RID: 862
	public KeyCode keyCode;

	// Token: 0x0400035F RID: 863
	public UIKeyBinding.Modifier modifier;

	// Token: 0x04000360 RID: 864
	public UIKeyBinding.Action action;

	// Token: 0x04000361 RID: 865
	private bool mIgnoreUp;

	// Token: 0x04000362 RID: 866
	private bool mIsInput;

	// Token: 0x04000363 RID: 867
	private bool mPress;

	// Token: 0x020011E5 RID: 4581
	public enum Action
	{
		// Token: 0x040063E1 RID: 25569
		PressAndClick,
		// Token: 0x040063E2 RID: 25570
		Select
	}

	// Token: 0x020011E6 RID: 4582
	public enum Modifier
	{
		// Token: 0x040063E4 RID: 25572
		None,
		// Token: 0x040063E5 RID: 25573
		Shift,
		// Token: 0x040063E6 RID: 25574
		Control,
		// Token: 0x040063E7 RID: 25575
		Alt
	}
}
