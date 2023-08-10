using UnityEngine;
using UnityEngine.UI;

public class TaskIndexCell : MonoBehaviour
{
	[SerializeField]
	private Text content;

	[SerializeField]
	private Image image;

	[SerializeField]
	private Sprite sprite;

	public void setContent(string str, bool isFinsh = false)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (isFinsh)
		{
			((Graphic)content).color = new Color(0.6784314f, 0.52156866f, 33f / 85f);
			image.sprite = sprite;
		}
		content.text = str.STVarReplace().ToCN();
	}
}
