using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class AutoSVContentWidth : MonoBehaviour
{
	private HorizontalLayoutGroup hor;

	private RectTransform rt;

	private int lastChildCount;

	[Header("额外宽度")]
	public int ExWidth;

	[Header("是否现在滑动")]
	[Tooltip("当子物体宽度小于SV宽度时，禁止滑动")]
	public bool IsLimitSliding;

	public ScrollRect SR;

	private void Start()
	{
		ref RectTransform reference = ref rt;
		Transform transform = ((Component)this).transform;
		reference = (RectTransform)(object)((transform is RectTransform) ? transform : null);
		hor = ((Component)this).GetComponent<HorizontalLayoutGroup>();
	}

	private void Update()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		int childCount = ((Transform)rt).childCount;
		if (lastChildCount == childCount)
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < childCount; i++)
		{
			float num2 = num;
			Transform child = ((Transform)rt).GetChild(i);
			num = num2 + ((RectTransform)((child is RectTransform) ? child : null)).sizeDelta.x;
			if (i < childCount - 1)
			{
				num += ((HorizontalOrVerticalLayoutGroup)hor).spacing;
			}
		}
		lastChildCount = childCount;
		rt.sizeDelta = new Vector2(num + (float)ExWidth, rt.sizeDelta.y);
		rt.anchoredPosition = new Vector2(0f, rt.anchoredPosition.y);
		if (IsLimitSliding)
		{
			float num3 = num;
			Transform transform = ((Component)SR).transform;
			if (num3 < ((RectTransform)((transform is RectTransform) ? transform : null)).sizeDelta.x)
			{
				SR.horizontal = false;
			}
			else
			{
				SR.horizontal = true;
			}
		}
	}
}
