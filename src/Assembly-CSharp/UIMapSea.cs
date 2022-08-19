﻿using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class UIMapSea : MonoBehaviour
{
	// Token: 0x06001C97 RID: 7319 RVA: 0x000CC564 File Offset: 0x000CA764
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
		for (int j = 0; j < this.NamesRoot.childCount; j++)
		{
			UIMapSeaName component2 = this.NamesRoot.GetChild(j).GetComponent<UIMapSeaName>();
			if (component2 != null)
			{
				this.seaNames[component2.SeaID] = component2;
			}
		}
		for (int k = 0; k < this.NodesRoot.childCount; k++)
		{
			UIMapSeaNode component3 = this.NodesRoot.GetChild(k).GetComponent<UIMapSeaNode>();
			if (component3 != null)
			{
				component3.Init();
				this.nodes.Add(component3);
			}
		}
		this.TanSuoDuInfoTrigger.gameObject.SetActive(false);
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x000CC654 File Offset: 0x000CA854
	public void Show()
	{
		this.HighlightBlockRoot.gameObject.SetActive(true);
		this.NamesRoot.gameObject.SetActive(true);
		this.NodesRoot.gameObject.SetActive(true);
		if (PlayerEx.Player != null)
		{
			this.TanSuoDuInfoTrigger.Tooltip = "<size=18><color=#e0ddb4>";
			foreach (KeyValuePair<int, UIMapSeaName> keyValuePair in this.seaNames)
			{
				keyValuePair.Value.RefreshUI();
				if (keyValuePair.Value.HasTanSuoDu)
				{
					UTooltipTrigger tanSuoDuInfoTrigger = this.TanSuoDuInfoTrigger;
					tanSuoDuInfoTrigger.Tooltip += string.Format("{0}  {1}%\n", keyValuePair.Value.SeaNameText.text, keyValuePair.Value.TanSuoDu);
				}
			}
			this.TanSuoDuInfoTrigger.Tooltip = this.TanSuoDuInfoTrigger.Tooltip.Substring(0, this.TanSuoDuInfoTrigger.Tooltip.Length - 1);
			UTooltipTrigger tanSuoDuInfoTrigger2 = this.TanSuoDuInfoTrigger;
			tanSuoDuInfoTrigger2.Tooltip += "</color></size>";
			this.TanSuoDuInfoTrigger.gameObject.SetActive(true);
		}
		else
		{
			this.TanSuoDuInfoTrigger.gameObject.SetActive(false);
			foreach (KeyValuePair<int, UIMapSeaName> keyValuePair2 in this.seaNames)
			{
				keyValuePair2.Value.HasTanSuoDu = false;
				keyValuePair2.Value.SetTanSuoDuShow(false);
			}
		}
		this.CloseAllHighlight();
		foreach (UIMapSeaNode uimapSeaNode in this.nodes)
		{
			uimapSeaNode.SetCanJiaoHu(false);
			uimapSeaNode.RefreshUI();
		}
		UIMapState nowState = UIMapPanel.Inst.NowState;
		if (nowState != UIMapState.Highlight)
		{
			if (nowState == UIMapState.Warp)
			{
				int nowIndex = PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
				using (List<UIMapSeaNode>.Enumerator enumerator2 = this.nodes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						UIMapSeaNode uimapSeaNode2 = enumerator2.Current;
						if (uimapSeaNode2.IsAccessed && nowIndex != uimapSeaNode2.NodeIndex)
						{
							uimapSeaNode2.SetCanJiaoHu(true);
						}
						else
						{
							uimapSeaNode2.SetCanJiaoHu(false);
						}
					}
					return;
				}
				goto IL_25B;
			}
			foreach (UIMapSeaNode uimapSeaNode3 in this.nodes)
			{
				uimapSeaNode3.SetCanJiaoHu(true);
			}
			return;
		}
		IL_25B:
		foreach (UIMapSeaNode uimapSeaNode4 in this.nodes)
		{
			uimapSeaNode4.SetCanJiaoHu(false);
		}
		if (this.highLights.ContainsKey(UIMapPanel.Inst.NeedHighlightBlock))
		{
			this.highLights[UIMapPanel.Inst.NeedHighlightBlock].SetLight(true);
			return;
		}
		Debug.LogError(string.Format("海上地图高亮模式出错，没有ID为{0}的高亮区域", UIMapPanel.Inst.NeedHighlightBlock));
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x000CC9C4 File Offset: 0x000CABC4
	public void Hide()
	{
		this.HighlightBlockRoot.gameObject.SetActive(false);
		this.NamesRoot.gameObject.SetActive(false);
		this.NodesRoot.gameObject.SetActive(false);
		this.TanSuoDuInfoTrigger.gameObject.SetActive(false);
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x000CCA18 File Offset: 0x000CAC18
	public void CloseAllHighlight()
	{
		foreach (UIMapHighlight uimapHighlight in this.highLights.Values)
		{
			uimapHighlight.SetLight(false);
		}
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x000CCA70 File Offset: 0x000CAC70
	public void OnNodeClick(UIMapSeaNode node)
	{
		Debug.Log("点击了" + node.NodeName);
		UIMapState nowState = UIMapPanel.Inst.NowState;
		if (nowState == UIMapState.Warp)
		{
			this.QuickMoveToNode(node);
			return;
		}
		UIPopTip.Inst.Pop("仅能在海域时快速移动", PopTipIconType.叹号);
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x000CCABC File Offset: 0x000CACBC
	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(true);
			this.SetTanSuoDuShow(highLight, true);
			return;
		case UIMapState.Highlight:
			break;
		case UIMapState.Warp:
			highLight.SetLight(true);
			this.SetTanSuoDuShow(highLight, true);
			break;
		default:
			return;
		}
	}

	// Token: 0x06001C9D RID: 7325 RVA: 0x000CCB08 File Offset: 0x000CAD08
	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(false);
			this.SetTanSuoDuShow(highLight, false);
			return;
		case UIMapState.Highlight:
			break;
		case UIMapState.Warp:
			highLight.SetLight(false);
			this.SetTanSuoDuShow(highLight, false);
			break;
		default:
			return;
		}
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x000CCB54 File Offset: 0x000CAD54
	private void SetTanSuoDuShow(UIMapHighlight highLight, bool show)
	{
		foreach (UIMapSeaName uimapSeaName in this.seaNames.Values)
		{
			if (uimapSeaName.BindHighlightID == highLight.ID)
			{
				uimapSeaName.SetTanSuoDuShow(show);
			}
			else
			{
				uimapSeaName.SetTanSuoDuShow(false);
			}
		}
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x000CCBC4 File Offset: 0x000CADC4
	public SeaQuickMoveData CalcQuickMove(int startIndex, int endIndex, int lingZhouQuality, Avatar avatar)
	{
		SeaQuickMoveData seaQuickMoveData = new SeaQuickMoveData();
		Vector2Int nodePosByNodeIndex = SeaEx.GetNodePosByNodeIndex(startIndex);
		Vector2Int nodePosByNodeIndex2 = SeaEx.GetNodePosByNodeIndex(endIndex);
		if (lingZhouQuality > 0)
		{
			seaQuickMoveData.MoveType = 0;
			List<Vector2Int> list = this.CalcPathForQuickMove(nodePosByNodeIndex, nodePosByNodeIndex2);
			List<SeaNodeData> list2 = new List<SeaNodeData>();
			foreach (Vector2Int nodePos in list)
			{
				list2.Add(SeaEx.GetSeaNodeDataByNodePos(nodePos));
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (SeaNodeData seaNodeData in list2)
			{
				if (!dictionary.ContainsKey(seaNodeData.SmallSeaDangerLevel))
				{
					dictionary.Add(seaNodeData.SmallSeaDangerLevel, 0);
				}
				Dictionary<int, int> dictionary2 = dictionary;
				int smallSeaDangerLevel = seaNodeData.SmallSeaDangerLevel;
				int num = dictionary2[smallSeaDangerLevel];
				dictionary2[smallSeaDangerLevel] = num + 1;
			}
			JToken jtoken = jsonData.instance.LingZhouPinJie[lingZhouQuality.ToString()];
			int num2 = 30 - (int)jtoken["speed"];
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
			int num3 = 1;
			foreach (JToken jtoken2 in jtoken["YiDongCost"])
			{
				dictionary3[num3] = (int)jtoken2;
				num3++;
			}
			int num4 = 0;
			string text = "修理费详细:\n";
			foreach (KeyValuePair<int, int> keyValuePair in dictionary)
			{
				num4 += dictionary3[keyValuePair.Key] * keyValuePair.Value;
				text += string.Format("格子危险等级{0}: {1}灵石*{2}格={3}灵石\n", new object[]
				{
					keyValuePair.Key,
					dictionary3[keyValuePair.Key],
					keyValuePair.Value,
					dictionary3[keyValuePair.Key] * keyValuePair.Value
				});
			}
			Debug.Log(text);
			int costDaySum = list2.Count * num2;
			seaQuickMoveData.CostDaySum = costDaySum;
			seaQuickMoveData.XiuLiCostSum = num4;
			seaQuickMoveData.PathLen = list.Count;
			seaQuickMoveData.Path = list;
		}
		else
		{
			int num5 = Mathf.Abs(nodePosByNodeIndex2.x - nodePosByNodeIndex.x) + Mathf.Abs(nodePosByNodeIndex2.y - nodePosByNodeIndex.y);
			Debug.Log(string.Format("快速移动长度:{0}", num5));
			int dunSu = avatar.dunSu;
			int num6 = 10;
			if (avatar.HasDunShuSkill())
			{
				seaQuickMoveData.MoveType = 1;
				using (List<SeaCastTimeJsonData>.Enumerator enumerator5 = SeaCastTimeJsonData.DataList.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						SeaCastTimeJsonData seaCastTimeJsonData = enumerator5.Current;
						if (seaCastTimeJsonData.dunSu >= dunSu)
						{
							num6 = seaCastTimeJsonData.XiaoHao;
							break;
						}
					}
					goto IL_30F;
				}
			}
			seaQuickMoveData.MoveType = 2;
			num6 = 60;
			IL_30F:
			int costDaySum2 = (int)((float)(num5 * num6) * this.noLingZhouCostMul);
			seaQuickMoveData.PathLen = num5;
			seaQuickMoveData.CostDaySum = costDaySum2;
		}
		return seaQuickMoveData;
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x000CCF44 File Offset: 0x000CB144
	private void QuickMoveToNode(UIMapSeaNode node)
	{
		Avatar player = PlayerEx.Player;
		_ItemJsonData equipLingZhouData = player.GetEquipLingZhouData();
		int nowIndex = player.fubenContorl[Tools.getScreenName()].NowIndex;
		int lingZhouQuality = 0;
		if (equipLingZhouData != null)
		{
			lingZhouQuality = equipLingZhouData.quality;
		}
		SeaQuickMoveData result = this.CalcQuickMove(nowIndex, node.NodeIndex, lingZhouQuality, player);
		if (result.MoveType == 0)
		{
			USelectBox.Show(string.Format("是否快速移动到{0}？\n需要花费{1}和{2}灵石维修灵舟", node.NodeName, result.CostDaySum.DayToDesc(), result.XiuLiCostSum), delegate
			{
				if ((int)player.money >= result.XiuLiCostSum)
				{
					UIMapPanel.Inst.HidePanel();
					if (result.XiuLiCostSum != 0)
					{
						player.AddMoney(-result.XiuLiCostSum);
					}
					this.QuickMoveSlider(result.CostDaySum, node.WarpSceneName);
					return;
				}
				UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
			}, null);
			return;
		}
		USelectBox.Show(string.Concat(new string[]
		{
			"是否快速移动到",
			node.NodeName,
			"？\n需要消耗",
			result.CostDaySum.DayToDesc(),
			"\n（没有灵舟保护需躲避灵气乱流会消耗更多时间）"
		}), delegate
		{
			UIMapPanel.Inst.HidePanel();
			this.QuickMoveSlider(result.CostDaySum, node.WarpSceneName);
		}, null);
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x000CD070 File Offset: 0x000CB270
	public void QuickMoveSlider(int costDaySum, string warpSceneName)
	{
		PlayerEx.Player.AddTime(costDaySum, 0, 0);
		Tools.instance.playFader("正在快速移动...", delegate
		{
			Tools.instance.loadMapScenes(warpSceneName, true);
		});
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x000CD0B4 File Offset: 0x000CB2B4
	private List<Vector2Int> CalcPathForQuickMove(Vector2Int start, Vector2Int end)
	{
		List<Vector2Int> list = new List<Vector2Int>();
		int num = Mathf.Abs(end.y - start.y);
		if (num > 0)
		{
			int num2 = (end.y - start.y > 0) ? 1 : -1;
			for (int i = 1; i <= num; i++)
			{
				int num3 = start.y + num2 * i;
				list.Add(new Vector2Int(start.x, num3));
			}
		}
		int num4 = Mathf.Abs(end.x - start.x);
		if (num4 > 0)
		{
			int num5 = (end.x - start.x > 0) ? 1 : -1;
			for (int j = 1; j <= num4; j++)
			{
				int num6 = start.x + num5 * j;
				list.Add(new Vector2Int(num6, end.y));
			}
		}
		return list;
	}

	// Token: 0x04001715 RID: 5909
	public RectTransform HighlightBlockRoot;

	// Token: 0x04001716 RID: 5910
	public RectTransform NamesRoot;

	// Token: 0x04001717 RID: 5911
	public RectTransform NodesRoot;

	// Token: 0x04001718 RID: 5912
	public Sprite BGSprite;

	// Token: 0x04001719 RID: 5913
	public UTooltipTrigger TanSuoDuInfoTrigger;

	// Token: 0x0400171A RID: 5914
	private Dictionary<int, UIMapHighlight> highLights = new Dictionary<int, UIMapHighlight>();

	// Token: 0x0400171B RID: 5915
	private Dictionary<int, UIMapSeaName> seaNames = new Dictionary<int, UIMapSeaName>();

	// Token: 0x0400171C RID: 5916
	private List<UIMapSeaNode> nodes = new List<UIMapSeaNode>();

	// Token: 0x0400171D RID: 5917
	private float noLingZhouCostMul = 2f;
}
