using UnityEngine;
using UnityEngine.Events;

namespace script.Steam.Utils;

public class UIToggleA : UIBase
{
	private GameObject 已选中;

	private FpBtn 未选中;

	private readonly UnityAction select;

	private readonly UnityAction unSelect;

	public UIToggleA(GameObject gameObject, UIToggleGroup uiToggleGroup, UnityAction selectAction, UnityAction unSelectAction)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		UIToggleA toggle = this;
		_go = gameObject;
		已选中 = Get("已选中");
		未选中 = Get<FpBtn>("未选中");
		select = selectAction;
		unSelect = unSelectAction;
		未选中.mouseUpEvent.AddListener((UnityAction)delegate
		{
			uiToggleGroup.Select(toggle);
		});
	}

	public void SetCanClick(bool flag)
	{
		未选中.SetCanClick(flag);
	}

	public void Select()
	{
		已选中.SetActive(true);
		((Component)未选中).gameObject.SetActive(false);
		UnityAction obj = select;
		if (obj != null)
		{
			obj.Invoke();
		}
	}

	public void CanCelSelect()
	{
		已选中.SetActive(false);
		((Component)未选中).gameObject.SetActive(true);
		UnityAction obj = unSelect;
		if (obj != null)
		{
			obj.Invoke();
		}
	}
}
