using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.UI.Base;

namespace script.Steam.UI;

public class ModMagUI : BasePanel
{
	public ModMagCtr Ctr;

	private bool isInit;

	public Text CurPage;

	public ModUI CurSelect;

	public GameObject Loading;

	public ModMagUI(GameObject gameObject)
	{
		_go = gameObject;
		Ctr = new ModMagCtr();
	}

	public override void Show()
	{
		if (!isInit)
		{
			Init();
			isInit = true;
		}
		CurPage.SetText($"{Ctr.CurPage}/{Ctr.MaxPage}");
		Ctr.UpdateList();
		WorkShopMag.Inst.ModPoolUI.Show();
		base.Show();
	}

	private void Init()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		Get<FpBtn>("刷新按钮").mouseUpEvent.AddListener((UnityAction)delegate
		{
			Ctr.UpdateList(isWithOutIsQuerying: true);
		});
		CurPage = Get<Text>("翻页/CurPage/Value");
		Loading = Get("加载中");
		Get<FpBtn>("翻页/下一页").mouseUpEvent.AddListener(new UnityAction(Ctr.AddPage));
		Get<FpBtn>("翻页/上一页").mouseUpEvent.AddListener(new UnityAction(Ctr.ReducePage));
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
}
