using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x060004AD RID: 1197 RVA: 0x00019B07 File Offset: 0x00017D07
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00019B10 File Offset: 0x00017D10
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00019B2B File Offset: 0x00017D2B
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00019B53 File Offset: 0x00017D53
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00019B7B File Offset: 0x00017D7B
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00019B97 File Offset: 0x00017D97
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00019BAF File Offset: 0x00017DAF
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00019BC8 File Offset: 0x00017DC8
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].gameObject.SendMessage(this.functionName, base.gameObject, 1);
				i++;
			}
			return;
		}
		this.target.SendMessage(this.functionName, base.gameObject, 1);
	}

	// Token: 0x040002D5 RID: 725
	public GameObject target;

	// Token: 0x040002D6 RID: 726
	public string functionName;

	// Token: 0x040002D7 RID: 727
	public UIButtonMessage.Trigger trigger;

	// Token: 0x040002D8 RID: 728
	public bool includeChildren;

	// Token: 0x040002D9 RID: 729
	private bool mStarted;

	// Token: 0x020011DE RID: 4574
	public enum Trigger
	{
		// Token: 0x040063C8 RID: 25544
		OnClick,
		// Token: 0x040063C9 RID: 25545
		OnMouseOver,
		// Token: 0x040063CA RID: 25546
		OnMouseOut,
		// Token: 0x040063CB RID: 25547
		OnPress,
		// Token: 0x040063CC RID: 25548
		OnRelease,
		// Token: 0x040063CD RID: 25549
		OnDoubleClick
	}
}
