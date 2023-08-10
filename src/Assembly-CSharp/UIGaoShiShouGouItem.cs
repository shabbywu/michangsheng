using UnityEngine;
using UnityEngine.UI;

public class UIGaoShiShouGouItem : MonoBehaviour
{
	public GameObject JiaJi;

	public Text Desc;

	public Text LingShiTitle;

	public Text LingShi;

	public Image LingShiIcon;

	public Text ShengWang;

	public UIIconShow Item;

	public FpBtn TiJiaoBtn;

	public RectMask2D YinZhangMask;

	public RectTransform YinZhang;

	public GameObject XuYao;

	public Material GreyMat;

	private static Color _normalColor = new Color(1f, 79f / 85f, 0.76862746f);

	private static Color _greyColor = new Color(46f / 85f, 46f / 85f, 46f / 85f);

	private void Awake()
	{
	}

	public void SetYiShouGou(bool yiShouGou, JSONObject pos, bool anim = false)
	{
		if (yiShouGou)
		{
			XuYao.SetActive(false);
			((Component)Item).gameObject.SetActive(false);
			((Component)TiJiaoBtn).gameObject.SetActive(false);
			GaoShiManager.SetYinZhangShow(YinZhangMask, YinZhang, pos, anim);
		}
		else
		{
			XuYao.SetActive(true);
			((Component)Item).gameObject.SetActive(true);
			((Component)TiJiaoBtn).gameObject.SetActive(true);
			((Component)YinZhang).gameObject.SetActive(false);
		}
	}

	public void SetButtonCanClick(bool canClick)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		((Behaviour)TiJiaoBtn).enabled = canClick;
		if (canClick)
		{
			((Graphic)((Component)TiJiaoBtn).GetComponent<Image>()).material = null;
			((Graphic)((Component)TiJiaoBtn).GetComponentInChildren<Text>()).color = _normalColor;
		}
		else
		{
			((Graphic)((Component)TiJiaoBtn).GetComponent<Image>()).material = GreyMat;
			((Graphic)((Component)TiJiaoBtn).GetComponentInChildren<Text>()).color = _greyColor;
		}
	}
}
