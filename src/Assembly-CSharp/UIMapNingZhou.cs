using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x020004AF RID: 1199
public class UIMapNingZhou : MonoBehaviour
{
	// Token: 0x06001FC0 RID: 8128 RVA: 0x00110660 File Offset: 0x0010E860
	public void Init()
	{
		for (int i = 0; i < this.HighlightBlockRoot.childCount; i++)
		{
			UIMapHighlight component = this.HighlightBlockRoot.GetChild(i).GetComponent<UIMapHighlight>();
			if (component != null)
			{
				this.highLights[component.ID] = component;
			}
		}
		for (int j = 0; j < this.NodesRoot.childCount; j++)
		{
			UIMapNingZhouNode component2 = this.NodesRoot.GetChild(j).GetComponent<UIMapNingZhouNode>();
			if (component2 != null)
			{
				component2.Init();
				this.nodes.Add(component2);
				if (!string.IsNullOrWhiteSpace(component2.WarpSceneName))
				{
					this.warpNodes.Add(component2);
				}
			}
		}
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x0011070C File Offset: 0x0010E90C
	public void Show()
	{
		this.HighlightBlockRoot.gameObject.SetActive(true);
		this.NodesRoot.gameObject.SetActive(true);
		this.CloseAllHighlight();
		foreach (UIMapNingZhouNode uimapNingZhouNode in this.nodes)
		{
			uimapNingZhouNode.SetCanJiaoHu(false);
		}
		UIMapState nowState = UIMapPanel.Inst.NowState;
		if (nowState != UIMapState.Highlight)
		{
			if (nowState != UIMapState.Warp)
			{
				foreach (UIMapNingZhouNode uimapNingZhouNode2 in this.nodes)
				{
					uimapNingZhouNode2.SetNodeAlpha(false);
					uimapNingZhouNode2.SetCanJiaoHu(false);
				}
				goto IL_1CF;
			}
			foreach (UIMapNingZhouNode uimapNingZhouNode3 in this.nodes)
			{
				uimapNingZhouNode3.SetNodeAlpha(true);
			}
			using (List<UIMapNingZhouNode>.Enumerator enumerator = this.warpNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UIMapNingZhouNode uimapNingZhouNode4 = enumerator.Current;
					if (uimapNingZhouNode4.WarpSceneName == this.FungusNowScene)
					{
						uimapNingZhouNode4.SetCanJiaoHu(false);
					}
					else
					{
						uimapNingZhouNode4.SetCanJiaoHu(true);
					}
					uimapNingZhouNode4.SetNodeAlpha(false);
				}
				goto IL_1CF;
			}
		}
		foreach (UIMapNingZhouNode uimapNingZhouNode5 in this.nodes)
		{
			uimapNingZhouNode5.SetNodeAlpha(false);
			uimapNingZhouNode5.SetCanJiaoHu(false);
		}
		if (this.highLights.ContainsKey(UIMapPanel.Inst.NeedHighlightBlock))
		{
			this.highLights[UIMapPanel.Inst.NeedHighlightBlock].SetLight(true);
		}
		else
		{
			Debug.LogError(string.Format("宁州地图高亮模式出错，没有ID为{0}的高亮区域", UIMapPanel.Inst.NeedHighlightBlock));
		}
		IL_1CF:
		this.RefreshDongFuNode();
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x0001A23F File Offset: 0x0001843F
	public void Hide()
	{
		this.HighlightBlockRoot.gameObject.SetActive(false);
		this.NodesRoot.gameObject.SetActive(false);
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00110930 File Offset: 0x0010EB30
	public void CloseAllHighlight()
	{
		foreach (UIMapHighlight uimapHighlight in this.highLights.Values)
		{
			uimapHighlight.SetLight(false);
		}
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00110988 File Offset: 0x0010EB88
	public void RefreshDongFuNode()
	{
		if (PlayerEx.Player != null && DongFuManager.PlayerHasDongFu(1))
		{
			this.DongFuNode.SetNodeName(DongFuManager.GetDongFuName(1));
			this.DongFuNode.gameObject.SetActive(true);
			return;
		}
		this.DongFuNode.gameObject.SetActive(false);
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x001109D8 File Offset: 0x0010EBD8
	public void OnNodeClick(UIMapNingZhouNode node)
	{
		if (UIMapPanel.Inst.NowState == UIMapState.Warp)
		{
			USelectBox.Show("是否花费5灵石传送至" + node.NodeName + "？", delegate
			{
				Avatar player = PlayerEx.Player;
				if (player.money >= 5UL)
				{
					player.AddMoney(-5);
					AvatarTransfer.Do(int.Parse(node.WarpSceneName.Replace("S", "")));
					Tools.instance.loadMapScenes(node.WarpSceneName, true);
					UIMapPanel.Inst.HidePanel();
					return;
				}
				UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
			}, null);
		}
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x00110A2C File Offset: 0x0010EC2C
	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(true);
			break;
		case UIMapState.Highlight:
		case UIMapState.Warp:
			break;
		default:
			return;
		}
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x00110A60 File Offset: 0x0010EC60
	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(false);
			break;
		case UIMapState.Highlight:
		case UIMapState.Warp:
			break;
		default:
			return;
		}
	}

	// Token: 0x04001B28 RID: 6952
	public RectTransform HighlightBlockRoot;

	// Token: 0x04001B29 RID: 6953
	public RectTransform NodesRoot;

	// Token: 0x04001B2A RID: 6954
	public UIMapNingZhouNode DongFuNode;

	// Token: 0x04001B2B RID: 6955
	public Sprite BGSprite;

	// Token: 0x04001B2C RID: 6956
	public string FungusNowScene;

	// Token: 0x04001B2D RID: 6957
	private Dictionary<int, UIMapHighlight> highLights = new Dictionary<int, UIMapHighlight>();

	// Token: 0x04001B2E RID: 6958
	private List<UIMapNingZhouNode> nodes = new List<UIMapNingZhouNode>();

	// Token: 0x04001B2F RID: 6959
	private List<UIMapNingZhouNode> warpNodes = new List<UIMapNingZhouNode>();
}
