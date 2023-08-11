using UnityEngine;
using UnityEngine.UI;

public class AutoGridSVContentHeight : MonoBehaviour
{
	private GridLayoutGroup grid;

	private RectTransform rt;

	private int lastChildCount;

	[Header("每行个数")]
	public int ColCount;

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
		grid = ((Component)this).GetComponent<GridLayoutGroup>();
	}

	private void Update()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		int childCount = ((Transform)rt).childCount;
		if (ChildCountMode && lastChildCount == childCount)
		{
			return;
		}
		float num = 0f;
		int num2 = childCount / ColCount;
		if (childCount % ColCount > 0)
		{
			num2++;
		}
		num = (float)num2 * (grid.cellSize.y + grid.spacing.y);
		lastChildCount = childCount;
		if (rt.sizeDelta.y == num + (float)ExHeigt)
		{
			return;
		}
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, num + (float)ExHeigt);
		rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0f);
		if ((Object)(object)grid != (Object)null && IsLimitSliding)
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
