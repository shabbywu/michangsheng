using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class TuJianTab : MonoBehaviour, ITuJianHyperlink
{
	[HideInInspector]
	public TuJianTabType TabType;

	public Button TabButton;

	private Image _TabButtonImage;

	private Text _TabButtonText;

	private Transform _ChildRoot;

	private static Color _TabButtonTextShowColor = new Color(0.99215686f, 0.88235295f, 28f / 51f);

	private static Color _TabButtonTextHideColor = new Color(24f / 85f, 58f / 85f, 0.7019608f);

	public virtual void Awake()
	{
		_TabButtonImage = ((Component)TabButton).GetComponent<Image>();
		_TabButtonText = ((Component)TabButton).GetComponentInChildren<Text>();
		_ChildRoot = ((Component)this).transform.Find("Root");
		TuJianManager.TabDict.Add(TabType, this);
	}

	public virtual void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		((UnityEvent)TabButton.onClick).AddListener((UnityAction)delegate
		{
			TuJianManager.Inst.NowTuJianTab = TabType;
		});
	}

	public virtual void Show()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		_TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_light.png"];
		((Graphic)_TabButtonText).color = _TabButtonTextShowColor;
		((Component)_ChildRoot).gameObject.SetActive(true);
	}

	public virtual void Hide()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		_TabButtonImage.sprite = TuJianDB.TuJianUISprite["MCS_TJ_title_gn.png"];
		((Graphic)_TabButtonText).color = _TabButtonTextHideColor;
		((Component)_ChildRoot).gameObject.SetActive(false);
	}

	public virtual void OnHyperlink(int[] args)
	{
	}

	public virtual void OnButtonClick()
	{
	}

	public virtual void RefreshPanel(bool isHyperLink = false)
	{
	}
}
