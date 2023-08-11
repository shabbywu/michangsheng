using UnityEngine;
using UnityEngine.UI;

public class MyScrollRect : MonoBehaviour
{
	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private int contentChildCount;

	[SerializeField]
	private float childHeight;

	[SerializeField]
	private float speceY;

	[SerializeField]
	private float contentWidth;

	private void Start()
	{
	}

	public void setContentChild(int count)
	{
		contentChildCount = count;
		setContentHeight();
	}

	private void setContentHeight()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		scrollRect.content.sizeDelta = new Vector2(contentWidth, (childHeight + speceY) * (float)contentChildCount - speceY);
	}
}
