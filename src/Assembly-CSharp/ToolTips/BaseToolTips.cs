using UnityEngine;

namespace ToolTips;

public abstract class BaseToolTips : MonoBehaviour
{
	private RectTransform _rectTransform;

	public abstract void Show(object Data);

	public void Hide()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void PCSetPosition()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_rectTransform == (Object)null)
		{
			_rectTransform = ((Component)this).GetComponent<RectTransform>();
		}
		Rect rect = _rectTransform.rect;
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(GetMousePosition().x, GetMousePosition().y, GetMousePosition().z);
		val.x += ((Rect)(ref rect)).width / 2f;
		val.y -= ((Rect)(ref rect)).height / 2f;
		if (Input.mousePosition.x > (float)Screen.width / 2f)
		{
			val.x -= ((Rect)(ref rect)).width;
		}
		if (Input.mousePosition.y < (float)Screen.height / 2f)
		{
			val.y += ((Rect)(ref rect)).height;
		}
		((Component)this).transform.position = NewUICanvas.Inst.Camera.ScreenToWorldPoint(val);
	}

	private Vector3 GetMousePosition()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Input.mousePosition;
	}
}
