using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x06000828 RID: 2088 RVA: 0x0000ABB6 File Offset: 0x00008DB6
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0000ABD1 File Offset: 0x00008DD1
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0000ABEC File Offset: 0x00008DEC
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0000AC07 File Offset: 0x00008E07
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0000AC23 File Offset: 0x00008E23
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0000AC3F File Offset: 0x00008E3F
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0000AC5B File Offset: 0x00008E5B
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0000AC77 File Offset: 0x00008E77
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0000AC93 File Offset: 0x00008E93
	private void OnDragOver()
	{
		if (this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0000ACAE File Offset: 0x00008EAE
	private void OnDragOut()
	{
		if (this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0000ACC9 File Offset: 0x00008EC9
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0000ACE5 File Offset: 0x00008EE5
	private void OnKey(KeyCode key)
	{
		if (this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00083B5C File Offset: 0x00081D5C
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x040005CB RID: 1483
	public object parameter;

	// Token: 0x040005CC RID: 1484
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x040005CD RID: 1485
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x040005CE RID: 1486
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x040005CF RID: 1487
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x040005D0 RID: 1488
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x040005D1 RID: 1489
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x040005D2 RID: 1490
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x040005D3 RID: 1491
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x040005D4 RID: 1492
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x040005D5 RID: 1493
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x040005D6 RID: 1494
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x040005D7 RID: 1495
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x020000D2 RID: 210
	// (Invoke) Token: 0x06000837 RID: 2103
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x020000D3 RID: 211
	// (Invoke) Token: 0x0600083B RID: 2107
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x020000D4 RID: 212
	// (Invoke) Token: 0x0600083F RID: 2111
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x020000D5 RID: 213
	// (Invoke) Token: 0x06000843 RID: 2115
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x020000D6 RID: 214
	// (Invoke) Token: 0x06000847 RID: 2119
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	// Token: 0x020000D7 RID: 215
	// (Invoke) Token: 0x0600084B RID: 2123
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
