using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000565 RID: 1381 RVA: 0x00008D8A File Offset: 0x00006F8A
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, 1);
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00008DB9 File Offset: 0x00006FB9
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, 1);
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00008DE8 File Offset: 0x00006FE8
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", 1);
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00008E11 File Offset: 0x00007011
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", 1);
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00008E3A File Offset: 0x0000703A
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, 1);
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00008E69 File Offset: 0x00007069
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, 1);
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00008E98 File Offset: 0x00007098
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, 1);
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00008EC2 File Offset: 0x000070C2
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", 1);
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00008EEB File Offset: 0x000070EB
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, 1);
		}
	}

	// Token: 0x040003C7 RID: 967
	public GameObject target;

	// Token: 0x040003C8 RID: 968
	public bool onHover;

	// Token: 0x040003C9 RID: 969
	public bool onPress;

	// Token: 0x040003CA RID: 970
	public bool onClick;

	// Token: 0x040003CB RID: 971
	public bool onDoubleClick;

	// Token: 0x040003CC RID: 972
	public bool onSelect;

	// Token: 0x040003CD RID: 973
	public bool onDrag;

	// Token: 0x040003CE RID: 974
	public bool onDrop;

	// Token: 0x040003CF RID: 975
	public bool onSubmit;

	// Token: 0x040003D0 RID: 976
	public bool onScroll;
}
