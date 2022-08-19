using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class UIMapNingZhou : MonoBehaviour
{
	// Token: 0x06001C75 RID: 7285 RVA: 0x000CBB94 File Offset: 0x000C9D94
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

	// Token: 0x06001C76 RID: 7286 RVA: 0x000CBC40 File Offset: 0x000C9E40
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

	// Token: 0x06001C77 RID: 7287 RVA: 0x000CBE64 File Offset: 0x000CA064
	public void Hide()
	{
		this.HighlightBlockRoot.gameObject.SetActive(false);
		this.NodesRoot.gameObject.SetActive(false);
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x000CBE88 File Offset: 0x000CA088
	public void CloseAllHighlight()
	{
		foreach (UIMapHighlight uimapHighlight in this.highLights.Values)
		{
			uimapHighlight.SetLight(false);
		}
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x000CBEE0 File Offset: 0x000CA0E0
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

	// Token: 0x06001C7A RID: 7290 RVA: 0x000CBF30 File Offset: 0x000CA130
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

	// Token: 0x06001C7B RID: 7291 RVA: 0x000CBF84 File Offset: 0x000CA184
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

	// Token: 0x06001C7C RID: 7292 RVA: 0x000CBFB8 File Offset: 0x000CA1B8
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

	// Token: 0x040016ED RID: 5869
	public RectTransform HighlightBlockRoot;

	// Token: 0x040016EE RID: 5870
	public RectTransform NodesRoot;

	// Token: 0x040016EF RID: 5871
	public UIMapNingZhouNode DongFuNode;

	// Token: 0x040016F0 RID: 5872
	public Sprite BGSprite;

	// Token: 0x040016F1 RID: 5873
	public string FungusNowScene;

	// Token: 0x040016F2 RID: 5874
	private Dictionary<int, UIMapHighlight> highLights = new Dictionary<int, UIMapHighlight>();

	// Token: 0x040016F3 RID: 5875
	private List<UIMapNingZhouNode> nodes = new List<UIMapNingZhouNode>();

	// Token: 0x040016F4 RID: 5876
	private List<UIMapNingZhouNode> warpNodes = new List<UIMapNingZhouNode>();
}
