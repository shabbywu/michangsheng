using System.Collections.Generic;
using Bag;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;

namespace script.NewLianDan.Result;

public class LianDanResult : BasePanel, IESCClose
{
	public List<BaseSlot> SlotList = new List<BaseSlot>();

	public Text Desc;

	public override void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.Show();
	}

	public override void Hide()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Clear();
		base.Hide();
	}

	public LianDanResult(GameObject go)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		_go = go;
		Desc = Get<Text>("描述");
		Get<FpBtn>("OkBtn").mouseUpEvent.AddListener(new UnityAction(Hide));
		for (int i = 1; i <= 6; i++)
		{
			SlotList.Add(Get<BaseSlot>($"ItemList/{i}"));
		}
		Clear();
	}

	public void Success(int index, int num)
	{
		Show();
		LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
		LianDanUIMag.Instance.CaoYaoBag.UpdateItem();
		Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
		LianDanUIMag.Instance.LianDanCallBack();
	}

	public void Fail(int index, int num)
	{
		Show();
		LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
		LianDanUIMag.Instance.CaoYaoBag.UpdateItem();
		Desc.SetText(Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString()));
		LianDanUIMag.Instance.LianDanCallBack();
	}

	public void ZhaLuLianDan()
	{
		Show();
		Desc.text = Tools.Code64("  " + jsonData.instance.LianDanSuccessItemLeiXin[16.ToString()]["desc"].str);
		LianDanUIMag.Instance.CaoYaoBag.CreateTempList();
		LianDanUIMag.Instance.CaoYaoBag.UpdateItem();
		LianDanUIMag.Instance.LianDanCallBack();
		LianDanUIMag.Instance.LianDanPanel.ZhaLuCallBack();
	}

	private void Clear()
	{
		foreach (BaseSlot slot in SlotList)
		{
			slot.SetNull();
		}
	}

	public bool TryEscClose()
	{
		Hide();
		return true;
	}
}
