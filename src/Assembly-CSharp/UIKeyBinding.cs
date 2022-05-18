using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x0600058E RID: 1422 RVA: 0x00072D24 File Offset: 0x00070F24
	private void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0000906C File Offset: 0x0000726C
	private void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00072D68 File Offset: 0x00070F68
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

	// Token: 0x06000591 RID: 1425 RVA: 0x00072DEC File Offset: 0x00070FEC
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

	// Token: 0x040003EF RID: 1007
	public KeyCode keyCode;

	// Token: 0x040003F0 RID: 1008
	public UIKeyBinding.Modifier modifier;

	// Token: 0x040003F1 RID: 1009
	public UIKeyBinding.Action action;

	// Token: 0x040003F2 RID: 1010
	private bool mIgnoreUp;

	// Token: 0x040003F3 RID: 1011
	private bool mIsInput;

	// Token: 0x040003F4 RID: 1012
	private bool mPress;

	// Token: 0x0200008C RID: 140
	public enum Action
	{
		// Token: 0x040003F6 RID: 1014
		PressAndClick,
		// Token: 0x040003F7 RID: 1015
		Select
	}

	// Token: 0x0200008D RID: 141
	public enum Modifier
	{
		// Token: 0x040003F9 RID: 1017
		None,
		// Token: 0x040003FA RID: 1018
		Shift,
		// Token: 0x040003FB RID: 1019
		Control,
		// Token: 0x040003FC RID: 1020
		Alt
	}
}
