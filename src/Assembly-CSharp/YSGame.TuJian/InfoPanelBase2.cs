using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class InfoPanelBase2 : InfoPanelBase
{
	protected Image _QualityImage;

	protected Image _ItemIconImage;

	protected Image _QualityUpImage;

	protected RectTransform _HyContentTransform;

	protected RectTransform _Hy2ContentTransform;

	protected SymbolText _HyText;

	protected SymbolText _HyText2;

	public Color HyTextColor = new Color(0.4627451f, 4f / 15f, 0.02745098f);

	public Color HyTextHoverColor = new Color(32f / 85f, 0.21960784f, 0.02745098f);

	public Color HyText2Color = new Color(0.003921569f, 0.4745098f, 37f / 85f);

	public Color HyText2HoverColor = new Color(0.015686275f, 33f / 85f, 0.35686275f);

	public virtual void Start()
	{
		Init();
	}

	public override void Update()
	{
		base.Update();
		RefreshSVHeight();
	}

	public void Init()
	{
		_QualityImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg")).GetComponent<Image>();
		_ItemIconImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon")).GetComponent<Image>();
		_QualityUpImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp")).GetComponent<Image>();
		ref RectTransform hyContentTransform = ref _HyContentTransform;
		Transform obj = ((Component)this).transform.Find("HyTextSV/Viewport/Content");
		hyContentTransform = (RectTransform)(object)((obj is RectTransform) ? obj : null);
		_HyText = ((Component)((Component)this).transform.Find("HyTextSV/Viewport/Content/Text")).GetComponent<SymbolText>();
		ref RectTransform hy2ContentTransform = ref _Hy2ContentTransform;
		Transform obj2 = ((Component)this).transform.Find("HyTextSV2/Viewport/Content");
		hy2ContentTransform = (RectTransform)(object)((obj2 is RectTransform) ? obj2 : null);
		_HyText2 = ((Component)((Component)this).transform.Find("HyTextSV2/Viewport/Content/Text")).GetComponent<SymbolText>();
	}

	public void RefreshSVHeight()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_HyContentTransform != (Object)null && _HyContentTransform.sizeDelta.y != ((Text)_HyText).preferredHeight + 34f)
		{
			_HyContentTransform.sizeDelta = new Vector2(_HyContentTransform.sizeDelta.x, ((Text)_HyText).preferredHeight + 34f);
		}
		if ((Object)(object)_Hy2ContentTransform != (Object)null && _Hy2ContentTransform.sizeDelta.y != ((Text)_HyText2).preferredHeight + 34f)
		{
			_Hy2ContentTransform.sizeDelta = new Vector2(_Hy2ContentTransform.sizeDelta.x, ((Text)_HyText2).preferredHeight + 34f);
		}
	}

	public override void OnHyperlink(int[] args)
	{
		base.OnHyperlink(args);
	}

	public void SetItemIcon(int id)
	{
		_ItemIconImage.sprite = TuJianDB.GetItemIconSprite(id);
		_QualityImage.sprite = TuJianDB.GetItemQualitySprite(id);
		_QualityUpImage.sprite = TuJianDB.GetItemQualityUpSprite(id);
	}

	public void SetSkillIcon(int id, int quality)
	{
		_ItemIconImage.sprite = TuJianDB.GetShenTongMiShuSprite(id);
		_QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
	}

	public void SetGongFaIcon(int id, int quality)
	{
		_ItemIconImage.sprite = TuJianDB.GetGongFaSprite(id);
		_QualityUpImage.sprite = TuJianDB.GetSkillQualitySprite(quality);
	}
}
