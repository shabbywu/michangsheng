using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using script.NewLianDan;

namespace Bag;

public class LianDanSlot : BaseSlot
{
	public bool IsInBag;

	public bool IsLock;

	private Text _yaoXin;

	private GameObject _lock;

	private GameObject _unLock;

	private void Awake()
	{
		if (!IsInBag)
		{
			Get<FpBtn>("Null/UnLock").MouseUp = OnPointerUp;
		}
	}

	public override void SetSlotData(object data)
	{
		base.SetSlotData(data);
		UpdateYaoXin();
	}

	public void UpdateYaoXin()
	{
		if (Item == null)
		{
			if (((Object)this).name.Contains("主药") || ((Object)this).name.Contains("辅药"))
			{
				_yaoXin.SetText("无");
			}
			return;
		}
		CaoYaoItem caoYaoItem = (CaoYaoItem)Item;
		if (((Object)this).name.Contains("主药"))
		{
			_yaoXin.SetText(caoYaoItem.GetZhuYao());
		}
		else if (((Object)this).name.Contains("辅药"))
		{
			_yaoXin.SetText(caoYaoItem.GetFuYao());
		}
	}

	public override void InitUI()
	{
		base.InitUI();
		if (!IsInBag)
		{
			if (((Object)this).name.Contains("主药") || ((Object)this).name.Contains("辅药"))
			{
				_yaoXin = Get<Text>("Bg/药性");
			}
			_lock = Get("Null/Lock");
			_unLock = Get("Null/UnLock");
		}
	}

	public void SetIsLock(bool value)
	{
		IsLock = value;
		if (IsLock)
		{
			_lock.SetActive(true);
			_unLock.SetActive(false);
			SetNull();
		}
		else
		{
			_lock.SetActive(false);
			_unLock.SetActive(true);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Invalid comparison between Unknown and I4
		if (IsLock)
		{
			return;
		}
		if (IsInBag)
		{
			if (IsNull())
			{
				return;
			}
			LianDanUIMag.Instance.LianDanPanel.PutCaoYao(this);
			LianDanUIMag.Instance.CaoYaoBag.Close();
			LianDanUIMag.Instance.LianDanPanel.CheckCanMade();
		}
		else if ((int)eventData.button == 0)
		{
			LianDanUIMag.Instance.CaoYaoBag.ToSlot = this;
			if (((Object)this).name.Contains("主药"))
			{
				LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(1);
			}
			else if (((Object)this).name.Contains("辅药"))
			{
				LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(2);
			}
			else if (((Object)this).name.Contains("药引"))
			{
				LianDanUIMag.Instance.CaoYaoBag.SelectWeiZhi(3);
			}
			LianDanUIMag.Instance.CaoYaoBag.Open();
		}
		else if ((int)eventData.button == 1 && !IsNull())
		{
			LianDanUIMag.Instance.LianDanPanel.BackCaoYao(this);
		}
		_selectPanel.SetActive(false);
	}

	public override void SetNull()
	{
		base.SetNull();
		if (((Object)this).name.Contains("主药") || ((Object)this).name.Contains("辅药"))
		{
			_yaoXin.SetText("无");
		}
	}
}
