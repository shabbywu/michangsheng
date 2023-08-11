using UnityEngine;
using UnityEngine.UI;

public class UINPCEventSVItem : MonoBehaviour
{
	public Text TimeText;

	public Text DescText;

	private RectTransform RT;

	private void Awake()
	{
		ref RectTransform rT = ref RT;
		Transform transform = ((Component)this).transform;
		rT = (RectTransform)(object)((transform is RectTransform) ? transform : null);
	}

	private void Update()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (RT.sizeDelta.y != DescText.preferredHeight)
		{
			RT.sizeDelta = new Vector2(RT.sizeDelta.x, DescText.preferredHeight);
		}
	}

	public void SetEvent(string time, string desc)
	{
		TimeText.text = time;
		DescText.text = desc;
	}
}
