using UnityEngine;
using UnityEngine.UI;

namespace Tab;

public abstract class ITabTips : UIBase
{
	protected RectTransform _rect;

	protected ContentSizeFitter _childSizeFitter;

	protected ContentSizeFitter _sizeFitter;

	protected Text _text;

	public void Show(string msg, Vector3 position)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		_text.text = Replace(msg);
		_go.SetActive(true);
		UpdateSize();
		_go.transform.position = Vector2.op_Implicit(new Vector2(position.x, position.y));
	}

	public void Show(string msg)
	{
		_text.text = Replace(msg);
		_go.SetActive(true);
		UpdateSize();
	}

	public void Hide()
	{
		_go.SetActive(false);
	}

	protected void UpdateSize()
	{
		if ((Object)(object)_childSizeFitter != (Object)null)
		{
			_childSizeFitter.SetLayoutVertical();
		}
		if ((Object)(object)_sizeFitter != (Object)null)
		{
			_sizeFitter.SetLayoutVertical();
		}
		if ((Object)(object)_rect != (Object)null)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
		}
	}

	protected abstract string Replace(string msg);
}
