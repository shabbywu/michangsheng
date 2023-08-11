using System.Collections.Generic;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UIMapSea : MonoBehaviour
{
	public RectTransform HighlightBlockRoot;

	public RectTransform NamesRoot;

	public RectTransform NodesRoot;

	public Sprite BGSprite;

	public UTooltipTrigger TanSuoDuInfoTrigger;

	private Dictionary<int, UIMapHighlight> highLights = new Dictionary<int, UIMapHighlight>();

	private Dictionary<int, UIMapSeaName> seaNames = new Dictionary<int, UIMapSeaName>();

	private List<UIMapSeaNode> nodes = new List<UIMapSeaNode>();

	private float noLingZhouCostMul = 2f;

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
		for (int j = 0; j < ((Transform)NamesRoot).childCount; j++)
		{
			UIMapSeaName component2 = ((Component)((Transform)NamesRoot).GetChild(j)).GetComponent<UIMapSeaName>();
			if ((Object)(object)component2 != (Object)null)
			{
				seaNames[component2.SeaID] = component2;
			}
		}
		for (int k = 0; k < ((Transform)NodesRoot).childCount; k++)
		{
			UIMapSeaNode component3 = ((Component)((Transform)NodesRoot).GetChild(k)).GetComponent<UIMapSeaNode>();
			if ((Object)(object)component3 != (Object)null)
			{
				component3.Init();
				nodes.Add(component3);
			}
		}
		((Component)TanSuoDuInfoTrigger).gameObject.SetActive(false);
	}

	public void Show()
	{
		((Component)HighlightBlockRoot).gameObject.SetActive(true);
		((Component)NamesRoot).gameObject.SetActive(true);
		((Component)NodesRoot).gameObject.SetActive(true);
		if (PlayerEx.Player != null)
		{
			TanSuoDuInfoTrigger.Tooltip = "<size=18><color=#e0ddb4>";
			foreach (KeyValuePair<int, UIMapSeaName> seaName in seaNames)
			{
				seaName.Value.RefreshUI();
				if (seaName.Value.HasTanSuoDu)
				{
					TanSuoDuInfoTrigger.Tooltip += $"{seaName.Value.SeaNameText.text}  {seaName.Value.TanSuoDu}%\n";
				}
			}
			TanSuoDuInfoTrigger.Tooltip = TanSuoDuInfoTrigger.Tooltip.Substring(0, TanSuoDuInfoTrigger.Tooltip.Length - 1);
			TanSuoDuInfoTrigger.Tooltip += "</color></size>";
			((Component)TanSuoDuInfoTrigger).gameObject.SetActive(true);
		}
		else
		{
			((Component)TanSuoDuInfoTrigger).gameObject.SetActive(false);
			foreach (KeyValuePair<int, UIMapSeaName> seaName2 in seaNames)
			{
				seaName2.Value.HasTanSuoDu = false;
				seaName2.Value.SetTanSuoDuShow(show: false);
			}
		}
		CloseAllHighlight();
		foreach (UIMapSeaNode node in nodes)
		{
			node.SetCanJiaoHu(can: false);
			node.RefreshUI();
		}
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Warp:
		{
			int nowIndex = PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
			{
				foreach (UIMapSeaNode node2 in nodes)
				{
					if (node2.IsAccessed && nowIndex != node2.NodeIndex)
					{
						node2.SetCanJiaoHu(can: true);
					}
					else
					{
						node2.SetCanJiaoHu(can: false);
					}
				}
				return;
			}
		}
		case UIMapState.Highlight:
			foreach (UIMapSeaNode node3 in nodes)
			{
				node3.SetCanJiaoHu(can: false);
			}
			if (highLights.ContainsKey(UIMapPanel.Inst.NeedHighlightBlock))
			{
				highLights[UIMapPanel.Inst.NeedHighlightBlock].SetLight(light: true);
			}
			else
			{
				Debug.LogError((object)$"海上地图高亮模式出错，没有ID为{UIMapPanel.Inst.NeedHighlightBlock}的高亮区域");
			}
			return;
		}
		foreach (UIMapSeaNode node4 in nodes)
		{
			node4.SetCanJiaoHu(can: true);
		}
	}

	public void Hide()
	{
		((Component)HighlightBlockRoot).gameObject.SetActive(false);
		((Component)NamesRoot).gameObject.SetActive(false);
		((Component)NodesRoot).gameObject.SetActive(false);
		((Component)TanSuoDuInfoTrigger).gameObject.SetActive(false);
	}

	public void CloseAllHighlight()
	{
		foreach (UIMapHighlight value in highLights.Values)
		{
			value.SetLight(light: false);
		}
	}

	public void OnNodeClick(UIMapSeaNode node)
	{
		Debug.Log((object)("点击了" + node.NodeName));
		UIMapState nowState = UIMapPanel.Inst.NowState;
		if (nowState == UIMapState.Warp)
		{
			QuickMoveToNode(node);
		}
		else
		{
			UIPopTip.Inst.Pop("仅能在海域时快速移动");
		}
	}

	public void OnMouseEnterHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(light: true);
			SetTanSuoDuShow(highLight, show: true);
			break;
		case UIMapState.Warp:
			highLight.SetLight(light: true);
			SetTanSuoDuShow(highLight, show: true);
			break;
		case UIMapState.Highlight:
			break;
		}
	}

	public void OnMouseExitHighlightBlock(UIMapHighlight highLight)
	{
		switch (UIMapPanel.Inst.NowState)
		{
		case UIMapState.Normal:
			highLight.SetLight(light: false);
			SetTanSuoDuShow(highLight, show: false);
			break;
		case UIMapState.Warp:
			highLight.SetLight(light: false);
			SetTanSuoDuShow(highLight, show: false);
			break;
		case UIMapState.Highlight:
			break;
		}
	}

	private void SetTanSuoDuShow(UIMapHighlight highLight, bool show)
	{
		foreach (UIMapSeaName value in seaNames.Values)
		{
			if (value.BindHighlightID == highLight.ID)
			{
				value.SetTanSuoDuShow(show);
			}
			else
			{
				value.SetTanSuoDuShow(show: false);
			}
		}
	}

	public SeaQuickMoveData CalcQuickMove(int startIndex, int endIndex, int lingZhouQuality, Avatar avatar)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		SeaQuickMoveData seaQuickMoveData = new SeaQuickMoveData();
		Vector2Int nodePosByNodeIndex = SeaEx.GetNodePosByNodeIndex(startIndex);
		Vector2Int nodePosByNodeIndex2 = SeaEx.GetNodePosByNodeIndex(endIndex);
		if (lingZhouQuality > 0)
		{
			seaQuickMoveData.MoveType = 0;
			List<Vector2Int> list = CalcPathForQuickMove(nodePosByNodeIndex, nodePosByNodeIndex2);
			List<SeaNodeData> list2 = new List<SeaNodeData>();
			foreach (Vector2Int item in list)
			{
				list2.Add(SeaEx.GetSeaNodeDataByNodePos(item));
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (SeaNodeData item2 in list2)
			{
				if (!dictionary.ContainsKey(item2.SmallSeaDangerLevel))
				{
					dictionary.Add(item2.SmallSeaDangerLevel, 0);
				}
				dictionary[item2.SmallSeaDangerLevel]++;
			}
			JToken val = jsonData.instance.LingZhouPinJie[lingZhouQuality.ToString()];
			int num = 30 - (int)val[(object)"speed"];
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			int num2 = 1;
			foreach (JToken item3 in (IEnumerable<JToken>)val[(object)"YiDongCost"])
			{
				dictionary2[num2] = (int)item3;
				num2++;
			}
			int num3 = 0;
			string text = "修理费详细:\n";
			foreach (KeyValuePair<int, int> item4 in dictionary)
			{
				num3 += dictionary2[item4.Key] * item4.Value;
				text += $"格子危险等级{item4.Key}: {dictionary2[item4.Key]}灵石*{item4.Value}格={dictionary2[item4.Key] * item4.Value}灵石\n";
			}
			Debug.Log((object)text);
			int costDaySum = list2.Count * num;
			seaQuickMoveData.CostDaySum = costDaySum;
			seaQuickMoveData.XiuLiCostSum = num3;
			seaQuickMoveData.PathLen = list.Count;
			seaQuickMoveData.Path = list;
		}
		else
		{
			int num4 = Mathf.Abs(((Vector2Int)(ref nodePosByNodeIndex2)).x - ((Vector2Int)(ref nodePosByNodeIndex)).x) + Mathf.Abs(((Vector2Int)(ref nodePosByNodeIndex2)).y - ((Vector2Int)(ref nodePosByNodeIndex)).y);
			Debug.Log((object)$"快速移动长度:{num4}");
			int dunSu = avatar.dunSu;
			int num5 = 10;
			if (avatar.HasDunShuSkill())
			{
				seaQuickMoveData.MoveType = 1;
				foreach (SeaCastTimeJsonData data in SeaCastTimeJsonData.DataList)
				{
					if (data.dunSu >= dunSu)
					{
						num5 = data.XiaoHao;
						break;
					}
				}
			}
			else
			{
				seaQuickMoveData.MoveType = 2;
				num5 = 60;
			}
			int costDaySum2 = (int)((float)(num4 * num5) * noLingZhouCostMul);
			seaQuickMoveData.PathLen = num4;
			seaQuickMoveData.CostDaySum = costDaySum2;
		}
		return seaQuickMoveData;
	}

	private void QuickMoveToNode(UIMapSeaNode node)
	{
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		Avatar player = PlayerEx.Player;
		_ItemJsonData equipLingZhouData = player.GetEquipLingZhouData();
		int nowIndex = player.fubenContorl[Tools.getScreenName()].NowIndex;
		int lingZhouQuality = 0;
		if (equipLingZhouData != null)
		{
			lingZhouQuality = equipLingZhouData.quality;
		}
		SeaQuickMoveData result = CalcQuickMove(nowIndex, node.NodeIndex, lingZhouQuality, player);
		if (result.MoveType == 0)
		{
			USelectBox.Show($"是否快速移动到{node.NodeName}？\n需要花费{result.CostDaySum.DayToDesc()}和{result.XiuLiCostSum}灵石维修灵舟", (UnityAction)delegate
			{
				if ((int)player.money >= result.XiuLiCostSum)
				{
					UIMapPanel.Inst.HidePanel();
					if (result.XiuLiCostSum != 0)
					{
						player.AddMoney(-result.XiuLiCostSum);
					}
					QuickMoveSlider(result.CostDaySum, node.WarpSceneName);
				}
				else
				{
					UIPopTip.Inst.Pop("灵石不足");
				}
			});
			return;
		}
		USelectBox.Show("是否快速移动到" + node.NodeName + "？\n需要消耗" + result.CostDaySum.DayToDesc() + "\n（没有灵舟保护需躲避灵气乱流会消耗更多时间）", (UnityAction)delegate
		{
			UIMapPanel.Inst.HidePanel();
			QuickMoveSlider(result.CostDaySum, node.WarpSceneName);
		});
	}

	public void QuickMoveSlider(int costDaySum, string warpSceneName)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		PlayerEx.Player.AddTime(costDaySum);
		Tools.instance.playFader("正在快速移动...", (UnityAction)delegate
		{
			Tools.instance.loadMapScenes(warpSceneName);
		});
	}

	private List<Vector2Int> CalcPathForQuickMove(Vector2Int start, Vector2Int end)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2Int> list = new List<Vector2Int>();
		int num = Mathf.Abs(((Vector2Int)(ref end)).y - ((Vector2Int)(ref start)).y);
		if (num > 0)
		{
			int num2 = ((((Vector2Int)(ref end)).y - ((Vector2Int)(ref start)).y > 0) ? 1 : (-1));
			for (int i = 1; i <= num; i++)
			{
				int num3 = ((Vector2Int)(ref start)).y + num2 * i;
				list.Add(new Vector2Int(((Vector2Int)(ref start)).x, num3));
			}
		}
		int num4 = Mathf.Abs(((Vector2Int)(ref end)).x - ((Vector2Int)(ref start)).x);
		if (num4 > 0)
		{
			int num5 = ((((Vector2Int)(ref end)).x - ((Vector2Int)(ref start)).x > 0) ? 1 : (-1));
			for (int j = 1; j <= num4; j++)
			{
				int num6 = ((Vector2Int)(ref start)).x + num5 * j;
				list.Add(new Vector2Int(num6, ((Vector2Int)(ref end)).y));
			}
		}
		return list;
	}
}
