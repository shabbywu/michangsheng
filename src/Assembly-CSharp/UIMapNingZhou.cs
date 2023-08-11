using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

public class UIMapNingZhou : MonoBehaviour
{
	public RectTransform HighlightBlockRoot;

	public RectTransform NodesRoot;

	public UIMapNingZhouNode DongFuNode;

	public Sprite BGSprite;

	public string FungusNowScene;

	private Dictionary<int, UIMapHighlight> highLights = new Dictionary<int, UIMapHighlight>();

	private List<UIMapNingZhouNode> nodes = new List<UIMapNingZhouNode>();

	private List<UIMapNingZhouNode> warpNodes = new List<UIMapNingZhouNode>();

	public void Init()
	{
		for (int i = 0; i < ((Transform)HighlightBlockRoot).childCount; i++)
		{
			UIMapHighlight component = ((Component)((Transform)HighlightBlockRoot).GetChild(i)).GetComponent<UIMapHighlight>();
			if ((Object)(object)component != (Object)null)
			{
				highLights[component.ID] = component;
			}
		}
		for (int j = 0; j < ((Transform)NodesRoot).childCount; j++)
		{
			UIMapNingZhouNode component2 = ((Component)((Transform)NodesRoot).GetChild(j)).GetComponent<UIMapNingZhouNode>();
			if ((Object)(object)component2 != (Object)null)
			{
				component2.Init();
				nodes.Add(component2);
				if (!string.IsNullOrWhiteSpace(component2.WarpSceneName))
				{
					warpNodes.Add(component2);
				}
			}
		}
	}

	public void Show()
	{
		((Component)HighlightBlockRoot).gameObject.SetActive(true);
		((Component)NodesRoot).gameObject.SetActive(true);
		CloseAllHighlight();
		foreach (UIMapNingZhouNode node in nodes)
		{
			node.SetCanJiaoHu(can: false);
		}
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Warp:
			foreach (UIMapNingZhouNode node2 in nodes)
			{
				node2.SetNodeAlpha(alpha: true);
			}
			foreach (UIMapNingZhouNode warpNode in warpNodes)
			{
				if (warpNode.WarpSceneName == FungusNowScene)
				{
					warpNode.SetCanJiaoHu(can: false);
				}
				else
				{
					warpNode.SetCanJiaoHu(can: true);
				}
				warpNode.SetNodeAlpha(alpha: false);
			}
			break;
		case UIMapState.Highlight:
			foreach (UIMapNingZhouNode node3 in nodes)
			{
				node3.SetNodeAlpha(alpha: false);
				node3.SetCanJiaoHu(can: false);
			}
			if (highLights.ContainsKey(UIMapPanel.Inst.NeedHighlightBlock))
			{
				highLights[UIMapPanel.Inst.NeedHighlightBlock].SetLight(light: true);
			}
			else
			{
				Debug.LogError((object)$"宁州地图高亮模式出错，没有ID为{UIMapPanel.Inst.NeedHighlightBlock}的高亮区域");
			}
			break;
		default:
			foreach (UIMapNingZhouNode node4 in nodes)
			{
				node4.SetNodeAlpha(alpha: false);
				node4.SetCanJiaoHu(can: false);
			}
			break;
		}
		RefreshDongFuNode();
	}

	public void Hide()
	{
		((Component)HighlightBlockRoot).gameObject.SetActive(false);
		((Component)NodesRoot).gameObject.SetActive(false);
	}

	public void CloseAllHighlight()
	{
		foreach (UIMapHighlight value in highLights.Values)
		{
			value.SetLight(light: false);
		}
	}

	public void RefreshDongFuNode()
	{
		if (PlayerEx.Player != null && DongFuManager.PlayerHasDongFu(1))
		{
			DongFuNode.SetNodeName(DongFuManager.GetDongFuName(1));
			((Component)DongFuNode).gameObject.SetActive(true);
		}
		else
		{
			((Component)DongFuNode).gameObject.SetActive(false);
		}
	}

	public void OnNodeClick(UIMapNingZhouNode node)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		if (UIMapPanel.Inst.NowState != UIMapState.Warp)
		{
			return;
		}
		USelectBox.Show("是否花费5灵石传送至" + node.NodeName + "？", (UnityAction)delegate
		{
			Avatar player = PlayerEx.Player;
			if (player.money >= 5)
			{
				player.AddMoney(-5);
				AvatarTransfer.Do(int.Parse(node.WarpSceneName.Replace("S", "")));
				Tools.instance.loadMapScenes(node.WarpSceneName);
				UIMapPanel.Inst.HidePanel();
			}
			else
			{
				UIPopTip.Inst.Pop("灵石不足");
			}
		});
	}

	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(light: true);
			break;
		case UIMapState.Highlight:
		case UIMapState.Warp:
			break;
		}
	}

	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(light: false);
			break;
		case UIMapState.Highlight:
		case UIMapState.Warp:
			break;
		}
	}
}
