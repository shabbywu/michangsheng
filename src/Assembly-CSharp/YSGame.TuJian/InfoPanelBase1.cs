using UnityEngine;
using UnityEngine.UI;
using WXB;

namespace YSGame.TuJian;

public class InfoPanelBase1 : InfoPanelBase
{
	protected Image _QualityImage;

	protected Image _QualityUpImage;

	protected Image _ItemIconImage;

	protected RectTransform _HyContentTransform;

	protected SymbolText _HyText;

	public Color HyTextColor = new Color(0.4627451f, 4f / 15f, 0.02745098f);

	public Color HyTextHoverColor = new Color(32f / 85f, 0.21960784f, 0.02745098f);

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
		_QualityUpImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon/QualityUp")).GetComponent<Image>();
		_ItemIconImage = ((Component)((Component)this).transform.Find("ItemBG/QualityBg/ItemIcon")).GetComponent<Image>();
		ref RectTransform hyContentTransform = ref _HyContentTransform;
		Transform obj = ((Component)this).transform.Find("HyTextSV/Viewport/Content");
		hyContentTransform = (RectTransform)(object)((obj is RectTransform) ? obj : null);
		_HyText = ((Component)((Component)this).transform.Find("HyTextSV/Viewport/Content/Text")).GetComponent<SymbolText>();
	}

	public void RefreshSVHeight()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)_HyContentTransform != (Object)null && _HyContentTransform.sizeDelta.y != ((Text)_HyText).preferredHeight + 34f)
		{
			_HyContentTransform.sizeDelta = new Vector2(_HyContentTransform.sizeDelta.x, ((Text)_HyText).preferredHeight + 34f);
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
}
