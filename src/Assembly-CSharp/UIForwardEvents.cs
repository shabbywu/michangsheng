using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000513 RID: 1299 RVA: 0x0001BF3D File Offset: 0x0001A13D
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, 1);
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0001BF6C File Offset: 0x0001A16C
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, 1);
		}
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0001BF9B File Offset: 0x0001A19B
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", 1);
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", 1);
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0001BFED File Offset: 0x0001A1ED
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, 1);
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0001C01C File Offset: 0x0001A21C
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, 1);
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0001C04B File Offset: 0x0001A24B
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, 1);
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0001C075 File Offset: 0x0001A275
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", 1);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0001C09E File Offset: 0x0001A29E
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, 1);
		}
	}

	// Token: 0x0400033F RID: 831
	public GameObject target;

	// Token: 0x04000340 RID: 832
	public bool onHover;

	// Token: 0x04000341 RID: 833
	public bool onPress;

	// Token: 0x04000342 RID: 834
	public bool onClick;

	// Token: 0x04000343 RID: 835
	public bool onDoubleClick;

	// Token: 0x04000344 RID: 836
	public bool onSelect;

	// Token: 0x04000345 RID: 837
	public bool onDrag;

	// Token: 0x04000346 RID: 838
	public bool onDrop;

	// Token: 0x04000347 RID: 839
	public bool onSubmit;

	// Token: 0x04000348 RID: 840
	public bool onScroll;
}
