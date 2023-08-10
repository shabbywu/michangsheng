using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.UI.Base;

namespace script.Steam.UI;

public class WorkShopUI : BasePanel
{
	private bool isInit;

	public readonly WorkShopCtr Ctr;

	public ModUI CurSelect;

	public Text CurPage;

	public List<Toggle> Toggles;

	public GameObject Loading;

	public WorkShopUI(GameObject gameObject)
	{
		_go = gameObject;
		Ctr = new WorkShopCtr();
	}

	private void Init()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		CreateTags();
		Get<FpBtn>("刷新按钮").mouseUpEvent.AddListener((UnityAction)delegate
		{
			Ctr.UpdateList(isWithOutIsQuerying: true);
		});
		CurPage = Get<Text>("翻页/CurPage/Value");
		Loading = Get("加载中");
		Get<FpBtn>("翻页/下一页").mouseUpEvent.AddListener(new UnityAction(Ctr.AddPage));
		Get<FpBtn>("翻页/上一页").mouseUpEvent.AddListener(new UnityAction(Ctr.ReducePage));
		((UnityEvent<int>)(object)Get<Dropdown>("选项").onValueChanged).AddListener((UnityAction<int>)Ctr.SetQueryType);
	}

	public override void Show()
	{
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		Ctr.UpdateList();
		WorkShopMag.Inst.ModPoolUI.Show();
		base.Show();
	}

	public void Select(ModUI modUI)
	{
		if (CurSelect != null)
		{
			CurSelect.CancelSelect();
		}
		CurSelect = modUI;
		WorkShopMag.Inst.MoreModInfoUI.Show(CurSelect.Info);
	}

	public override void Hide()
	{
		if (CurSelect != null)
		{
			CurSelect.CancelSelect();
		}
		WorkShopMag.Inst.MoreModInfoUI.Hide();
		CurSelect = null;
		Ctr.Clear();
		base.Hide();
	}

	private void CreateTags()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Toggles = new List<Toggle>();
		GameObject val = Get("标签/0");
		Transform parent = val.transform.parent;
		float x = val.transform.localPosition.x;
		float num = val.transform.localPosition.y;
		foreach (string tag in WorkShopMag.Tags)
		{
			GameObject obj = val.Inst(parent);
			obj.transform.localPosition = Vector2.op_Implicit(new Vector2(x, num));
			Toggle component = obj.GetComponent<Toggle>();
			((Object)obj).name = tag;
			((UnityEvent<bool>)(object)component.onValueChanged).AddListener((UnityAction<bool>)delegate(bool arg0)
			{
				int index = WorkShopMag.Tags.IndexOf(tag);
				string tag2 = WorkShopMag.EnTags[index];
				if (arg0)
				{
					Ctr.AddTag(tag2);
				}
				else
				{
					Ctr.RemoveTag(tag2);
				}
			});
			Toggles.Add(component);
			((Component)obj.transform.GetChild(1)).GetComponent<Text>().SetText(tag);
			obj.SetActive(true);
			num -= 50f;
		}
	}
}
