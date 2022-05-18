using System;
using PaiMai;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x020004B2 RID: 1202
public class UIMapPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001FD3 RID: 8147 RVA: 0x0001A333 File Offset: 0x00018533
	private void Awake()
	{
		UIMapPanel.Inst = this;
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x0001A33B File Offset: 0x0001853B
	private void Start()
	{
		this.Init();
		this.HidePanel();
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x00110BDC File Offset: 0x0010EDDC
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

	// Token: 0x06001FD6 RID: 8150 RVA: 0x00110C3C File Offset: 0x0010EE3C
	private void Init()
	{
		this.NingZhouTab.mouseUpEvent.AddListener(new UnityAction(this.OnNingZhouTabClick));
		this.SeaTab.mouseUpEvent.AddListener(new UnityAction(this.OnSeaTabClick));
		this.CloseBtn.onClick.AddListener(new UnityAction(this.OnCloseBtnClick));
		this.NingZhou.Init();
		this.Sea.Init();
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x00110CB4 File Offset: 0x0010EEB4
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

	// Token: 0x06001FD8 RID: 8152 RVA: 0x0001A349 File Offset: 0x00018549
	public void OpenMap(MapArea area, UIMapState state)
	{
		this.NowArea = area;
		this.NowState = state;
		this.SetTabShow(false);
		this.ShowPanel();
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x00110D00 File Offset: 0x0010EF00
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

	// Token: 0x06001FDA RID: 8154 RVA: 0x00110D40 File Offset: 0x0010EF40
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

	// Token: 0x06001FDB RID: 8155 RVA: 0x00110D98 File Offset: 0x0010EF98
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

	// Token: 0x06001FDC RID: 8156 RVA: 0x00110E58 File Offset: 0x0010F058
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

	// Token: 0x06001FDD RID: 8157 RVA: 0x0001A366 File Offset: 0x00018566
	public bool TryEscClose()
	{
		this.HidePanel();
		return true;
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x00110EB4 File Offset: 0x0010F0B4
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

	// Token: 0x06001FDF RID: 8159 RVA: 0x00110EEC File Offset: 0x0010F0EC
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

	// Token: 0x06001FE0 RID: 8160 RVA: 0x0001A36F File Offset: 0x0001856F
	private void OnNingZhouTabClick()
	{
		this.NowArea = MapArea.NingZhou;
		this.SetTabShow(true);
		this.NowState = UIMapState.Normal;
		this.ShowPanel();
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x0001A38C File Offset: 0x0001858C
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

	// Token: 0x06001FE2 RID: 8162 RVA: 0x0001A3C3 File Offset: 0x000185C3
	private void OnCloseBtnClick()
	{
		this.HidePanel();
	}

	// Token: 0x04001B38 RID: 6968
	public static UIMapPanel Inst;

	// Token: 0x04001B39 RID: 6969
	public MapArea NowArea;

	// Token: 0x04001B3A RID: 6970
	public UIMapState NowState;

	// Token: 0x04001B3B RID: 6971
	public bool IsShow;

	// Token: 0x04001B3C RID: 6972
	public GameObject PanelObj;

	// Token: 0x04001B3D RID: 6973
	public Image MapBG;

	// Token: 0x04001B3E RID: 6974
	public UIMapNingZhou NingZhou;

	// Token: 0x04001B3F RID: 6975
	public UIMapSea Sea;

	// Token: 0x04001B40 RID: 6976
	public GameObject TabRoot;

	// Token: 0x04001B41 RID: 6977
	public FpBtn NingZhouTab;

	// Token: 0x04001B42 RID: 6978
	public GameObject NingZhouTabHighlight;

	// Token: 0x04001B43 RID: 6979
	public FpBtn SeaTab;

	// Token: 0x04001B44 RID: 6980
	public GameObject SeaTabHighlight;

	// Token: 0x04001B45 RID: 6981
	public Button CloseBtn;

	// Token: 0x04001B46 RID: 6982
	public UnityAction CloseAction;

	// Token: 0x04001B47 RID: 6983
	private bool isRegisteredESC;

	// Token: 0x04001B48 RID: 6984
	public int NeedHighlightBlock;
}
