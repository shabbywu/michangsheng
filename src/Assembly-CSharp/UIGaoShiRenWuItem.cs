using UnityEngine;
using UnityEngine.UI;

public class UIGaoShiRenWuItem : MonoBehaviour
{
	public Text Desc;

	public Text LingShiTitle;

	public Text LingShi;

	public Image LingShiIcon;

	public Text ShengWang;

	public FpBtn TiJiaoBtn;

	public RectMask2D YinZhangMask;

	public RectTransform YinZhang;

	public void SetYiLingQu(bool yiLingQu, JSONObject pos, bool anim = false)
	{
		if (yiLingQu)
		{
			((Component)TiJiaoBtn).gameObject.SetActive(false);
			GaoShiManager.SetYinZhangShow(YinZhangMask, YinZhang, pos, anim);
		}
		else
		{
			((Component)TiJiaoBtn).gameObject.SetActive(true);
			((Component)YinZhang).gameObject.SetActive(false);
		}
	}
}
