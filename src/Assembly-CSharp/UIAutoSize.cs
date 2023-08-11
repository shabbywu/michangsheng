using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIAutoSize : MonoBehaviour
{
	public bool AutoWidth;

	public bool AutoHeight;

	private ILayoutElement layoutElement;

	private RectTransform rectTransform;

	private void Awake()
	{
		layoutElement = ((Component)this).GetComponent<ILayoutElement>();
		rectTransform = ((Component)this).GetComponent<RectTransform>();
	}

	private void Update()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (layoutElement != null)
		{
			if (AutoWidth && rectTransform.sizeDelta.x != layoutElement.preferredWidth)
			{
				rectTransform.SetSizeWithCurrentAnchors((Axis)0, layoutElement.preferredWidth);
			}
			if (AutoHeight && rectTransform.sizeDelta.y != layoutElement.preferredHeight)
			{
				rectTransform.SetSizeWithCurrentAnchors((Axis)1, layoutElement.preferredHeight);
			}
		}
	}
}
