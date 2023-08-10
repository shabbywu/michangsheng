using UnityEngine;
using UnityEngine.EventSystems;

public class DragInventory : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
	private Vector2 pointerOffset;

	private RectTransform canvasRectTransform;

	private RectTransform panelRectTransform;

	private void Awake()
	{
		Canvas componentInParent = ((Component)this).GetComponentInParent<Canvas>();
		if ((Object)(object)componentInParent != (Object)null)
		{
			ref RectTransform reference = ref canvasRectTransform;
			Transform transform = ((Component)componentInParent).transform;
			reference = (RectTransform)(object)((transform is RectTransform) ? transform : null);
			ref RectTransform reference2 = ref panelRectTransform;
			Transform parent = ((Component)this).transform.parent;
			reference2 = (RectTransform)(object)((parent is RectTransform) ? parent : null);
		}
	}

	public void OnPointerDown(PointerEventData data)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, ref pointerOffset);
	}

	public void OnDrag(PointerEventData data)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		if (!((Object)(object)panelRectTransform == (Object)null) && RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Vector2.op_Implicit(Input.mousePosition), data.pressEventCamera, ref val))
		{
			((Transform)panelRectTransform).localPosition = Vector2.op_Implicit(val - pointerOffset);
		}
	}
}
