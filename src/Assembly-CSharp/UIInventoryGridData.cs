using System;
using GUIPackage;
using UnityEngine;

public class UIInventoryGridData
{
	public Vector2 Pos;

	public int Index;

	public item Item;

	public int ItemCount;

	public Action<UIIconShow> IconShowInitAction;

	public UIIconShow BindShow;

	public void IconShowInit(UIIconShow iconShow)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (IconShowInitAction != null)
		{
			BindShow = iconShow;
			Transform transform = ((Component)BindShow).transform;
			((RectTransform)((transform is RectTransform) ? transform : null)).anchoredPosition = Pos;
			IconShowInitAction(iconShow);
		}
	}
}
