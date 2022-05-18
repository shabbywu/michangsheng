using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000084 RID: 132
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x0600055D RID: 1373 RVA: 0x00008C49 File Offset: 0x00006E49
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

	// Token: 0x0600055E RID: 1374 RVA: 0x00008C80 File Offset: 0x00006E80
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

	// Token: 0x0600055F RID: 1375 RVA: 0x00008CB7 File Offset: 0x00006EB7
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

	// Token: 0x06000560 RID: 1376 RVA: 0x00008CEE File Offset: 0x00006EEE
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

	// Token: 0x06000561 RID: 1377 RVA: 0x00008D15 File Offset: 0x00006F15
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

	// Token: 0x06000562 RID: 1378 RVA: 0x00008D3C File Offset: 0x00006F3C
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

	// Token: 0x06000563 RID: 1379 RVA: 0x00008D63 File Offset: 0x00006F63
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

	// Token: 0x040003BC RID: 956
	public static UIEventTrigger current;

	// Token: 0x040003BD RID: 957
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x040003BE RID: 958
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x040003BF RID: 959
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x040003C0 RID: 960
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x040003C1 RID: 961
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x040003C2 RID: 962
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x040003C3 RID: 963
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x040003C4 RID: 964
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x040003C5 RID: 965
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x040003C6 RID: 966
	public List<EventDelegate> onDragOut = new List<EventDelegate>();
}
