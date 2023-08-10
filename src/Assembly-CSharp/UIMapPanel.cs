using PaiMai;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class UIMapPanel : MonoBehaviour, IESCClose
{
	public static UIMapPanel Inst;

	public MapArea NowArea;

	public UIMapState NowState;

	public bool IsShow;

	public GameObject PanelObj;

	public Image MapBG;

	public UIMapNingZhou NingZhou;

	public UIMapSea Sea;

	public GameObject TabRoot;

	public FpBtn NingZhouTab;

	public GameObject NingZhouTabHighlight;

	public FpBtn SeaTab;

	public GameObject SeaTabHighlight;

	public Button CloseBtn;

	public UnityAction CloseAction;

	private bool isRegisteredESC;

	public int NeedHighlightBlock;

	private void Awake()
	{
		Inst = this;
	}

	private void Start()
	{
		Init();
		HidePanel();
	}

	private void Update()
	{
		if (Input.GetKeyDown((KeyCode)109))
		{
			if (IsShow)
			{
				HidePanel();
			}
			else if (PlayerEx.Player != null && !((Object)(object)RoundManager.instance != (Object)null) && !((Object)(object)SingletonMono<PaiMaiUiMag>.Instance != (Object)null) && Tools.instance.canClick())
			{
				OpenDefaultMap();
			}
		}
	}

	private void Init()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		NingZhouTab.mouseUpEvent.AddListener(new UnityAction(OnNingZhouTabClick));
		SeaTab.mouseUpEvent.AddListener(new UnityAction(OnSeaTabClick));
		((UnityEvent)CloseBtn.onClick).AddListener(new UnityAction(OnCloseBtnClick));
		NingZhou.Init();
		Sea.Init();
	}

	public void OpenDefaultMap()
	{
		MapArea nowMapArea = SceneEx.GetNowMapArea();
		if (nowMapArea != MapArea.Unknow)
		{
			NowArea = nowMapArea;
		}
		NowState = UIMapState.Normal;
		if (SceneEx.NowSceneName.StartsWith("Sea"))
		{
			NowState = UIMapState.Warp;
		}
		SetTabShow(show: true);
		ShowPanel();
	}

	public void OpenMap(MapArea area, UIMapState state)
	{
		NowArea = area;
		NowState = state;
		SetTabShow(show: false);
		ShowPanel();
	}

	public void OpenHighlight(int id)
	{
		MapArea area;
		if (id > 100)
		{
			id -= 100;
			area = MapArea.Sea;
		}
		else
		{
			area = MapArea.NingZhou;
		}
		ESCCloseManager.Inst.CloseAll();
		Inst.NeedHighlightBlock = id;
		Inst.OpenMap(area, UIMapState.Highlight);
	}

	private void SetTabShow(bool show)
	{
		TabRoot.SetActive(show);
		if (show)
		{
			if (NowArea == MapArea.NingZhou)
			{
				NingZhouTabHighlight.SetActive(true);
				SeaTabHighlight.SetActive(false);
			}
			else
			{
				NingZhouTabHighlight.SetActive(false);
				SeaTabHighlight.SetActive(true);
			}
		}
	}

	private void ShowPanel()
	{
		IsShow = true;
		NingZhou.Hide();
		Sea.Hide();
		switch (NowArea)
		{
		case MapArea.NingZhou:
			MapBG.sprite = NingZhou.BGSprite;
			NingZhou.Show();
			break;
		case MapArea.Sea:
			MapBG.sprite = Sea.BGSprite;
			Sea.Show();
			break;
		}
		PanelObj.SetActive(true);
		if (!isRegisteredESC)
		{
			isRegisteredESC = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}
		MusicMag.instance.PlayEffectMusic(13);
		((Component)this).transform.SetAsLastSibling();
	}

	public void HidePanel()
	{
		IsShow = false;
		NingZhou.Hide();
		Sea.Hide();
		PanelObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
		isRegisteredESC = false;
		if (CloseAction != null)
		{
			CloseAction.Invoke();
		}
	}

	public bool TryEscClose()
	{
		HidePanel();
		return true;
	}

	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		switch (NowArea)
		{
		case MapArea.NingZhou:
			NingZhou.OnMouseEnterHighlightBlock(highLight);
			break;
		case MapArea.Sea:
			Sea.OnMouseEnterHighlightBlock(highLight);
			break;
		}
	}

	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		switch (NowArea)
		{
		case MapArea.NingZhou:
			NingZhou.OnMouseExitHighlightBlock(highLight);
			break;
		case MapArea.Sea:
			Sea.OnMouseExitHighlightBlock(highLight);
			break;
		}
	}

	private void OnNingZhouTabClick()
	{
		NowArea = MapArea.NingZhou;
		SetTabShow(show: true);
		NowState = UIMapState.Normal;
		ShowPanel();
	}

	private void OnSeaTabClick()
	{
		NowArea = MapArea.Sea;
		SetTabShow(show: true);
		if (SceneEx.NowSceneName.StartsWith("Sea"))
		{
			NowState = UIMapState.Warp;
		}
		else
		{
			NowState = UIMapState.Normal;
		}
		ShowPanel();
	}

	private void OnCloseBtnClick()
	{
		HidePanel();
	}
}
