using UnityEngine;
using UnityEngine.UI;

public class MainUITooltip : MonoBehaviour
{
	[SerializeField]
	private Text desc;

	[SerializeField]
	private RectTransform rectTransform;

	[SerializeField]
	private ContentSizeFitter childSizeFitter;

	[SerializeField]
	private ContentSizeFitter sizeFitter;

	public void Show(string content, Vector3 vector3)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).gameObject.SetActive(true);
		desc.text = content;
		((Component)this).transform.position = vector3;
		childSizeFitter.SetLayoutHorizontal();
		sizeFitter.SetLayoutHorizontal();
		sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		Transform transform = ((Component)this).transform;
		float x = ((Component)this).transform.localPosition.x;
		Rect rect = rectTransform.rect;
		transform.localPosition = new Vector3(x - ((Rect)(ref rect)).width / 2f, ((Component)this).transform.localPosition.y, ((Component)this).transform.localPosition.z);
	}

	public void Hide()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
