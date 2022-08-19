using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x060007A1 RID: 1953 RVA: 0x0002EF3B File Offset: 0x0002D13B
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0002EF56 File Offset: 0x0002D156
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0002EF71 File Offset: 0x0002D171
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0002EF8C File Offset: 0x0002D18C
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0002EFFC File Offset: 0x0002D1FC
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0002F018 File Offset: 0x0002D218
	private void OnDragOver()
	{
		if (this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0002F033 File Offset: 0x0002D233
	private void OnDragOut()
	{
		if (this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0002F04E File Offset: 0x0002D24E
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002F06A File Offset: 0x0002D26A
	private void OnKey(KeyCode key)
	{
		if (this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0002F088 File Offset: 0x0002D288
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x040004BE RID: 1214
	public object parameter;

	// Token: 0x040004BF RID: 1215
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x040004C0 RID: 1216
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x040004C1 RID: 1217
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x040004C2 RID: 1218
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x040004C3 RID: 1219
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x040004C4 RID: 1220
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x040004C5 RID: 1221
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x040004C6 RID: 1222
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x040004C7 RID: 1223
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x040004C8 RID: 1224
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x040004C9 RID: 1225
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x040004CA RID: 1226
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x02001207 RID: 4615
	// (Invoke) Token: 0x0600783E RID: 30782
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02001208 RID: 4616
	// (Invoke) Token: 0x06007842 RID: 30786
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02001209 RID: 4617
	// (Invoke) Token: 0x06007846 RID: 30790
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x0200120A RID: 4618
	// (Invoke) Token: 0x0600784A RID: 30794
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x0200120B RID: 4619
	// (Invoke) Token: 0x0600784E RID: 30798
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	// Token: 0x0200120C RID: 4620
	// (Invoke) Token: 0x06007852 RID: 30802
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
