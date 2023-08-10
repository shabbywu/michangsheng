using System.Collections.Generic;
using Tab;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.Steam.Utils;

namespace script.Steam.UI.Base;

public class ModUI : UIBase
{
	private int type;

	private Text modName;

	private Text modTag;

	private Text modNum;

	private Text modLv;

	private Toggle toggle;

	private GameObject select;

	private UIListener listener;

	private bool isSelect;

	public bool IsUsed { get; private set; }

	public ModInfo Info { get; private set; }

	public ModUI(GameObject go)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		IsUsed = false;
		_go = go;
		modName = Get<Text>("Mod名称");
		modTag = Get<Text>("类型");
		modNum = Get<Text>("订阅数");
		modLv = Get<Text>("好评率");
		toggle = Get<Toggle>("订阅");
		select = Get("选择UI/已选中");
		listener = Get<UIListener>("选择UI");
		listener.mouseUpEvent.AddListener(new UnityAction(Click));
		listener.mouseEnterEvent.AddListener((UnityAction)delegate
		{
			select.SetActive(true);
		});
		listener.mouseOutEvent.AddListener((UnityAction)delegate
		{
			if (!isSelect)
			{
				select.SetActive(false);
			}
		});
	}

	public void SetType(int type)
	{
		if (type == 1)
		{
			((Component)modTag).gameObject.SetActive(true);
			((Component)modNum).gameObject.SetActive(true);
			((Component)modLv).gameObject.SetActive(true);
		}
		else
		{
			((Component)modTag).gameObject.SetActive(false);
			((Component)modNum).gameObject.SetActive(false);
			((Component)modLv).gameObject.SetActive(false);
		}
		this.type = type;
	}

	public void BindingInfo(ModInfo modInfo)
	{
		Info = modInfo;
		IsUsed = true;
		((UnityEventBase)toggle.onValueChanged).RemoveAllListeners();
		modName.SetTextWithEllipsis(Info.Name);
		modTag.SetTextWithEllipsis(Info.Tags);
		modNum.SetText(Info.Subscriptions);
		modLv.SetText(Info.GetLv());
		if (type == 0)
		{
			toggle.isOn = WorkShopMag.Inst.ModMagUI.Ctr.IsOpen(Info.Id);
		}
		else
		{
			toggle.isOn = WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(Info.Id);
		}
		((UnityEvent<bool>)(object)toggle.onValueChanged).AddListener((UnityAction<bool>)UpdateToggleState);
		_go.SetActive(true);
	}

	private void UpdateToggleState(bool isOn)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		if (type == 0)
		{
			if (isOn)
			{
				WorkShopMag.Inst.ModMagUI.Ctr.OpenMod(Info.Id);
				WorkShopMag.Inst.IsChange = true;
			}
			else
			{
				WorkShopMag.Inst.ModMagUI.Ctr.CloseMod(Info.Id);
				WorkShopMag.Inst.IsChange = true;
			}
		}
		else if (isOn)
		{
			List<ulong> list = WorkShopMag.Inst.WorkShopUI.Ctr.GetNoSubscriptDependency(Info);
			if (list.Count > 0)
			{
				USelectBox.Show("检测到前置Mod未订阅,该Mod无法生效，是否要订阅前置Mod", (UnityAction)delegate
				{
					foreach (ulong item in list)
					{
						WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(item, isShow: false);
					}
					WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(Info.Id);
					WorkShopMag.Inst.WorkShopUI.Ctr.UpdateList();
					WorkShopMag.Inst.IsChange = true;
				});
			}
			else
			{
				WorkShopMag.Inst.WorkShopUI.Ctr.SubscriptionMod(Info.Id);
				WorkShopMag.Inst.IsChange = true;
			}
		}
		else
		{
			WorkShopMag.Inst.WorkShopUI.Ctr.UnSubscriptionMod(Info.Id);
			WorkShopMag.Inst.IsChange = true;
		}
	}

	public void UnBindingInfo()
	{
		Info = null;
		IsUsed = false;
		_go.SetActive(false);
	}

	private void Click()
	{
		if (type == 0)
		{
			WorkShopMag.Inst.ModMagUI.Select(this);
		}
		else
		{
			WorkShopMag.Inst.WorkShopUI.Select(this);
		}
		select.SetActive(true);
		isSelect = true;
	}

	public void CancelSelect()
	{
		select.SetActive(false);
		isSelect = false;
		WorkShopMag.Inst.MoreModInfoUI.Hide();
	}
}
