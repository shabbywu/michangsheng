using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x060004FB RID: 1275 RVA: 0x0000861C File Offset: 0x0000681C
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00008625 File Offset: 0x00006825
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00008640 File Offset: 0x00006840
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00008668 File Offset: 0x00006868
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x00008690 File Offset: 0x00006890
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000086AC File Offset: 0x000068AC
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000086C4 File Offset: 0x000068C4
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00070A58 File Offset: 0x0006EC58
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

	// Token: 0x0400034D RID: 845
	public GameObject target;

	// Token: 0x0400034E RID: 846
	public string functionName;

	// Token: 0x0400034F RID: 847
	public UIButtonMessage.Trigger trigger;

	// Token: 0x04000350 RID: 848
	public bool includeChildren;

	// Token: 0x04000351 RID: 849
	private bool mStarted;

	// Token: 0x02000073 RID: 115
	public enum Trigger
	{
		// Token: 0x04000353 RID: 851
		OnClick,
		// Token: 0x04000354 RID: 852
		OnMouseOver,
		// Token: 0x04000355 RID: 853
		OnMouseOut,
		// Token: 0x04000356 RID: 854
		OnPress,
		// Token: 0x04000357 RID: 855
		OnRelease,
		// Token: 0x04000358 RID: 856
		OnDoubleClick
	}
}
