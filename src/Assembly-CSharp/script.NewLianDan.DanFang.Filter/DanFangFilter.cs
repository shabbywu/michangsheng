using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;

namespace script.NewLianDan.DanFang.Filter;

public class DanFangFilter : BasePanel
{
	public List<QualityFilter> QualityFilterList;

	public GameObject TempFilter;

	private bool _init;

	public DanFangFilter(GameObject go)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		_go = go;
		TempFilter = Get("Mask/Temp");
		((UnityEvent)Get<Button>("Bg").onClick).AddListener(new UnityAction(Hide));
		_init = false;
	}

	public override void Show()
	{
		if (!_init)
		{
			Init();
		}
		base.Show();
	}

	private void Init()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		QualityFilterList = new List<QualityFilter>();
		Dictionary<int, string> filterDataDict = LianDanUIMag.Instance.DanFangPanel.FilterDataDict;
		float num = TempFilter.transform.localPosition.x;
		float y = TempFilter.transform.localPosition.y;
		foreach (int key in filterDataDict.Keys)
		{
			QualityFilter qualityFilter = new QualityFilter(TempFilter.Inst(TempFilter.transform.parent), filterDataDict[key], key, num, y);
			qualityFilter.Action = LianDanUIMag.Instance.DanFangPanel.UpdateFilter;
			QualityFilterList.Add(qualityFilter);
			num -= 64f;
		}
		_init = true;
	}
}
