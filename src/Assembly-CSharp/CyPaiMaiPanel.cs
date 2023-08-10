using JSONClass;
using UnityEngine;
using UnityEngine.UI;

public class CyPaiMaiPanel : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Text _title;

	[SerializeField]
	private Text _time;

	[SerializeField]
	private Transform _itemList;

	[SerializeField]
	private GameObject _item;

	public void Show(CyPaiMaiInfo paiMaiInfo)
	{
		((Component)this).gameObject.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
		if (paiMaiInfo == null)
		{
			Debug.LogError((object)"传音符查看拍卖物品信息失败,对象paiMaiInfo为空");
			return;
		}
		Tools.ClearChild(_itemList);
		foreach (int item in paiMaiInfo.ItemList)
		{
			UIIconShow component = _item.Inst(_itemList).GetComponent<UIIconShow>();
			component.SetItem(item);
			((Component)component).gameObject.SetActive(true);
		}
		_title.text = PaiMaiBiao.DataDict[paiMaiInfo.PaiMaiId].Name;
		_time.text = $"{paiMaiInfo.StartTime.Year}年{paiMaiInfo.StartTime.Month}月{paiMaiInfo.StartTime.Day}日" + $"至{paiMaiInfo.EndTime.Month}月{paiMaiInfo.EndTime.Day}日";
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
