using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x0600050B RID: 1291 RVA: 0x0001BD7A File Offset: 0x00019F7A
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001BDB1 File Offset: 0x00019FB1
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0001BDE8 File Offset: 0x00019FE8
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001BE1F File Offset: 0x0001A01F
	private void OnClick()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0001BE46 File Offset: 0x0001A046
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0001BE6D File Offset: 0x0001A06D
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0001BE94 File Offset: 0x0001A094
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x04000334 RID: 820
	public static UIEventTrigger current;

	// Token: 0x04000335 RID: 821
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x04000336 RID: 822
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x04000337 RID: 823
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x04000338 RID: 824
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x04000339 RID: 825
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x0400033A RID: 826
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x0400033B RID: 827
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x0400033C RID: 828
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x0400033D RID: 829
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x0400033E RID: 830
	public List<EventDelegate> onDragOut = new List<EventDelegate>();
}
