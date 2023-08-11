using UnityEngine;

namespace GUIPackage;

public class ScrollBar : MonoBehaviour
{
	public UIScrollView scrollView;

	private void OnScroll(float delta)
	{
		MonoBehaviour.print((object)"sb");
		scrollView.Scroll(delta);
		((Component)((Component)this).transform.parent.parent).GetComponentInChildren<UIScrollBar>().value -= delta;
	}
}
