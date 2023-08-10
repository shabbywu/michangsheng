using UnityEngine;
using UnityEngine.UI;

public class AutoSVContentHeight : MonoBehaviour
{
	private VerticalLayoutGroup ver;

	private RectTransform rt;

	private int lastChildCount;

	[Header("额外高度")]
	public int ExHeigt;

	[Header("是否现在滑动")]
	[Tooltip("当子物体高度小于SV高度时，禁止滑动")]
	public bool IsLimitSliding;

	public ScrollRect SR;

	[Tooltip("根据物体数量的变动进行高度刷新。关闭的话会一直检测")]
	public bool ChildCountMode = true;

	private void Start()
	{
		ref RectTransform reference = ref rt;
		Transform transform = ((Component)this).transform;
		reference = (RectTransform)(object)((transform is RectTransform) ? transform : null);
		ver = ((Component)this).GetComponent<VerticalLayoutGroup>();
	}

	private void Update()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		int childCount = ((Transform)rt).childCount;
		if (ChildCountMode && lastChildCount == childCount)
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < childCount; i++)
		{
			float num2 = num;
			Transform child = ((Transform)rt).GetChild(i);
			num = num2 + ((RectTransform)((child is RectTransform) ? child : null)).sizeDelta.y;
			if (i < childCount - 1 && (Object)(object)ver != (Object)null)
			{
				num += ((HorizontalOrVerticalLayoutGroup)ver).spacing;
			}
		}
		lastChildCount = childCount;
		if (rt.sizeDelta.y == num + (float)ExHeigt)
		{
			return;
		}
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, num + (float)ExHeigt);
		rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0f);
		if ((Object)(object)ver != (Object)null && IsLimitSliding)
		{
			float num3 = num;
			Transform transform = ((Component)SR).transform;
			if (num3 < ((RectTransform)((transform is RectTransform) ? transform : null)).sizeDelta.y)
			{
				SR.vertical = false;
			}
			else
			{
				SR.vertical = true;
			}
		}
	}
}
