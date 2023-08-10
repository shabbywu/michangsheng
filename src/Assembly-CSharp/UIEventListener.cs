using UnityEngine;

[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	public delegate void VoidDelegate(GameObject go);

	public delegate void BoolDelegate(GameObject go, bool state);

	public delegate void FloatDelegate(GameObject go, float delta);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	public object parameter;

	public VoidDelegate onSubmit;

	public VoidDelegate onClick;

	public VoidDelegate onDoubleClick;

	public BoolDelegate onHover;

	public BoolDelegate onPress;

	public BoolDelegate onSelect;

	public FloatDelegate onScroll;

	public VectorDelegate onDrag;

	public VoidDelegate onDragOver;

	public VoidDelegate onDragOut;

	public ObjectDelegate onDrop;

	public KeyCodeDelegate onKey;

	private void OnSubmit()
	{
		if (onSubmit != null)
		{
			onSubmit(((Component)this).gameObject);
		}
	}

	private void OnClick()
	{
		if (onClick != null)
		{
			onClick(((Component)this).gameObject);
		}
	}

	private void OnDoubleClick()
	{
		if (onDoubleClick != null)
		{
			onDoubleClick(((Component)this).gameObject);
		}
	}

	private void OnHover(bool isOver)
	{
		if (onHover != null)
		{
			onHover(((Component)this).gameObject, isOver);
		}
	}

	private void OnPress(bool isPressed)
	{
		if (onPress != null)
		{
			onPress(((Component)this).gameObject, isPressed);
		}
	}

	private void OnSelect(bool selected)
	{
		if (onSelect != null)
		{
			onSelect(((Component)this).gameObject, selected);
		}
	}

	private void OnScroll(float delta)
	{
		if (onScroll != null)
		{
			onScroll(((Component)this).gameObject, delta);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (onDrag != null)
		{
			onDrag(((Component)this).gameObject, delta);
		}
	}

	private void OnDragOver()
	{
		if (onDragOver != null)
		{
			onDragOver(((Component)this).gameObject);
		}
	}

	private void OnDragOut()
	{
		if (onDragOut != null)
		{
			onDragOut(((Component)this).gameObject);
		}
	}

	private void OnDrop(GameObject go)
	{
		if (onDrop != null)
		{
			onDrop(((Component)this).gameObject, go);
		}
	}

	private void OnKey(KeyCode key)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (onKey != null)
		{
			onKey(((Component)this).gameObject, key);
		}
	}

	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uIEventListener = go.GetComponent<UIEventListener>();
		if ((Object)(object)uIEventListener == (Object)null)
		{
			uIEventListener = go.AddComponent<UIEventListener>();
		}
		return uIEventListener;
	}
}
