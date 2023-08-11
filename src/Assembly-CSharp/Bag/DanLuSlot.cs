using UnityEngine.EventSystems;
using UnityEngine.UI;
using script.NewLianDan;

namespace Bag;

public class DanLuSlot : BaseSlot
{
	public bool IsInBag;

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (IsInBag)
		{
			if (IsNull())
			{
				return;
			}
			LianDanUIMag.Instance.LianDanPanel.PutDanLu(this);
			LianDanUIMag.Instance.DanLuBag.Close();
			LianDanUIMag.Instance.PutDanLuPanel.Hide();
			LianDanUIMag.Instance.LianDanPanel.Show();
		}
		else if ((int)eventData.button == 0)
		{
			LianDanUIMag.Instance.DanLuBag.UpdateItem();
			LianDanUIMag.Instance.DanLuBag.Open();
		}
		_selectPanel.SetActive(false);
	}

	public override void SetSlotData(object data)
	{
		base.SetSlotData(data);
		if (!IsInBag)
		{
			Get<Text>("Bg/耐久").SetText(string.Format("{0}/100", Item.Seid["NaiJiu"].I));
		}
	}

	public void UpdateNaiJiu()
	{
		if (!IsInBag)
		{
			if (IsNull())
			{
				Get<Text>("Bg/耐久").SetText("0/100");
			}
			else
			{
				Get<Text>("Bg/耐久").SetText(string.Format("{0}/100", Item.Seid["NaiJiu"].I));
			}
		}
	}
}
