using UnityEngine;
using UnityEngine.UI;

namespace PaiMai;

public class PaiMaiItem : MonoBehaviour
{
	[SerializeField]
	private UIIconShow _curItem;

	[SerializeField]
	private Text _curPrice;

	[SerializeField]
	private Text _curAvatarName;

	[SerializeField]
	private Text _mayPrice;

	public PaiMaiAvatar Owner { get; private set; }

	public void UpdateItem()
	{
		if (SingletonMono<PaiMaiUiMag>.Instance.CurShop == null)
		{
			Debug.LogError((object)"当前商品为");
			return;
		}
		if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid.HasField("quality"))
		{
			_curItem.SetItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid);
		}
		else
		{
			_curItem.SetItem(SingletonMono<PaiMaiUiMag>.Instance.CurShop.ShopId);
			if (SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid.HasField("NaiJiu"))
			{
				_curItem.tmpItem.Seid = SingletonMono<PaiMaiUiMag>.Instance.CurShop.Seid;
			}
		}
		_curItem.CanDrag = false;
		_curItem.Count = SingletonMono<PaiMaiUiMag>.Instance.CurShop.Count;
		_mayPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.MayPrice.ToString();
		_curAvatarName.text = "";
		_curPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice.ToString();
		Owner = null;
	}

	public void UpdateUI()
	{
		Owner = SingletonMono<PaiMaiUiMag>.Instance.CurAvatar;
		_curPrice.text = SingletonMono<PaiMaiUiMag>.Instance.CurShop.CurPrice.ToString();
		if (Owner != null)
		{
			_curAvatarName.text = Owner.Name;
		}
		else
		{
			_curAvatarName.text = "";
		}
	}
}
