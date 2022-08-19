using System;
using PaiMai;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000340 RID: 832
public class UIMapPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001C86 RID: 7302 RVA: 0x000CC185 File Offset: 0x000CA385
	private void Awake()
	{
		UIMapPanel.Inst = this;
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x000CC18D File Offset: 0x000CA38D
	private void Start()
	{
		this.Init();
		this.HidePanel();
	}

	// Token: 0x06001C88 RID: 7304 RVA: 0x000CC19C File Offset: 0x000CA39C
	private void Update()
	{
		if (Input.GetKeyDown(109))
		{
			if (this.IsShow)
			{
				this.HidePanel();
				return;
			}
			if (PlayerEx.Player == null)
			{
				return;
			}
			if (RoundManager.instance != null)
			{
				return;
			}
			if (SingletonMono<PaiMaiUiMag>.Instance != null)
			{
				return;
			}
			if (!Tools.instance.canClick(false, true))
			{
				return;
			}
			this.OpenDefaultMap();
		}
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x000CC1FC File Offset: 0x000CA3FC
	private void Init()
	{
		this.NingZhouTab.mouseUpEvent.AddListener(new UnityAction(this.OnNingZhouTabClick));
		this.SeaTab.mouseUpEvent.AddListener(new UnityAction(this.OnSeaTabClick));
		this.CloseBtn.onClick.AddListener(new UnityAction(this.OnCloseBtnClick));
		this.NingZhou.Init();
		this.Sea.Init();
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x000CC274 File Offset: 0x000CA474
	public void OpenDefaultMap()
	{
		MapArea nowMapArea = SceneEx.GetNowMapArea();
		if (nowMapArea != MapArea.Unknow)
		{
			this.NowArea = nowMapArea;
		}
		this.NowState = UIMapState.Normal;
		if (SceneEx.NowSceneName.StartsWith("Sea"))
		{
			this.NowState = UIMapState.Warp;
		}
		this.SetTabShow(true);
		this.ShowPanel();
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x000CC2BE File Offset: 0x000CA4BE
	public void OpenMap(MapArea area, UIMapState state)
	{
		this.NowArea = area;
		this.NowState = state;
		this.SetTabShow(false);
		this.ShowPanel();
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x000CC2DC File Offset: 0x000CA4DC
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
		UIMapPanel.Inst.NeedHighlightBlock = id;
		UIMapPanel.Inst.OpenMap(area, UIMapState.Highlight);
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x000CC31C File Offset: 0x000CA51C
	private void SetTabShow(bool show)
	{
		this.TabRoot.SetActive(show);
		if (show)
		{
			if (this.NowArea == MapArea.NingZhou)
			{
				this.NingZhouTabHighlight.SetActive(true);
				this.SeaTabHighlight.SetActive(false);
				return;
			}
			this.NingZhouTabHighlight.SetActive(false);
			this.SeaTabHighlight.SetActive(true);
		}
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000CC374 File Offset: 0x000CA574
	private void ShowPanel()
	{
		this.IsShow = true;
		this.NingZhou.Hide();
		this.Sea.Hide();
		MapArea nowArea = this.NowArea;
		if (nowArea != MapArea.NingZhou)
		{
			if (nowArea == MapArea.Sea)
			{
				this.MapBG.sprite = this.Sea.BGSprite;
				this.Sea.Show();
			}
		}
		else
		{
			this.MapBG.sprite = this.NingZhou.BGSprite;
			this.NingZhou.Show();
		}
		this.PanelObj.SetActive(true);
		if (!this.isRegisteredESC)
		{
			this.isRegisteredESC = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}
		MusicMag.instance.PlayEffectMusic(13, 1f);
		base.transform.SetAsLastSibling();
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x000CC434 File Offset: 0x000CA634
	public void HidePanel()
	{
		this.IsShow = false;
		this.NingZhou.Hide();
		this.Sea.Hide();
		this.PanelObj.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
		this.isRegisteredESC = false;
		if (this.CloseAction != null)
		{
			this.CloseAction.Invoke();
		}
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x000CC48F File Offset: 0x000CA68F
	public bool TryEscClose()
	{
		this.HidePanel();
		return true;
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x000CC498 File Offset: 0x000CA698
	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		MapArea nowArea = this.NowArea;
		if (nowArea == MapArea.NingZhou)
		{
			this.NingZhou.OnMouseEnterHighlightBlock(highLight);
			return;
		}
		if (nowArea != MapArea.Sea)
		{
			return;
		}
		this.Sea.OnMouseEnterHighlightBlock(highLight);
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x000CC4D0 File Offset: 0x000CA6D0
	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		MapArea nowArea = this.NowArea;
		if (nowArea == MapArea.NingZhou)
		{
			this.NingZhou.OnMouseExitHighlightBlock(highLight);
			return;
		}
		if (nowArea != MapArea.Sea)
		{
			return;
		}
		this.Sea.OnMouseExitHighlightBlock(highLight);
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000CC505 File Offset: 0x000CA705
	private void OnNingZhouTabClick()
	{
		this.NowArea = MapArea.NingZhou;
		this.SetTabShow(true);
		this.NowState = UIMapState.Normal;
		this.ShowPanel();
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x000CC522 File Offset: 0x000CA722
	private void OnSeaTabClick()
	{
		this.NowArea = MapArea.Sea;
		this.SetTabShow(true);
		if (SceneEx.NowSceneName.StartsWith("Sea"))
		{
			this.NowState = UIMapState.Warp;
		}
		else
		{
			this.NowState = UIMapState.Normal;
		}
		this.ShowPanel();
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x000CC559 File Offset: 0x000CA759
	private void OnCloseBtnClick()
	{
		this.HidePanel();
	}

	// Token: 0x040016FC RID: 5884
	public static UIMapPanel Inst;

	// Token: 0x040016FD RID: 5885
	public MapArea NowArea;

	// Token: 0x040016FE RID: 5886
	public UIMapState NowState;

	// Token: 0x040016FF RID: 5887
	public bool IsShow;

	// Token: 0x04001700 RID: 5888
	public GameObject PanelObj;

	// Token: 0x04001701 RID: 5889
	public Image MapBG;

	// Token: 0x04001702 RID: 5890
	public UIMapNingZhou NingZhou;

	// Token: 0x04001703 RID: 5891
	public UIMapSea Sea;

	// Token: 0x04001704 RID: 5892
	public GameObject TabRoot;

	// Token: 0x04001705 RID: 5893
	public FpBtn NingZhouTab;

	// Token: 0x04001706 RID: 5894
	public GameObject NingZhouTabHighlight;

	// Token: 0x04001707 RID: 5895
	public FpBtn SeaTab;

	// Token: 0x04001708 RID: 5896
	public GameObject SeaTabHighlight;

	// Token: 0x04001709 RID: 5897
	public Button CloseBtn;

	// Token: 0x0400170A RID: 5898
	public UnityAction CloseAction;

	// Token: 0x0400170B RID: 5899
	private bool isRegisteredESC;

	// Token: 0x0400170C RID: 5900
	public int NeedHighlightBlock;
}
