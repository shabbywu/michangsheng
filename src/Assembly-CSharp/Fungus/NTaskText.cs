using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTask", "NTaskText", "", 0)]
[AddComponentMenu("")]
public class NTaskText : Command
{
	[Tooltip("需要获取描述的任务ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable NTaskID;

	[Tooltip("需要到的值存放位置")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable Desc;

	public override void OnEnter()
	{
		Desc.Value = GetNTaskDesc(NTaskID.Value);
		Continue();
	}

	public static string GetNTaskDesc(int NTaskID)
	{
		Avatar player = Tools.instance.getPlayer();
		try
		{
			if (!player.nomelTaskMag.HasNTask(NTaskID))
			{
				player.nomelTaskMag.DeDaiSetWhereNode(NTaskID);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		NTaskXiangXi nTaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(NTaskID);
		List<JSONObject> nTaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(NTaskID);
		string text = nTaskXiangXiData.SayMiaoShu ?? "";
		int num = 0;
		foreach (JSONObject item in nTaskXiangXiList)
		{
			int chilidID = player.nomelTaskMag.getChilidID(NTaskID, num);
			NTaskSuiJI nTaskSuiJI = NTaskSuiJI.DataDict[chilidID];
			string str = item["TaskID"].str;
			text = text.Replace(str, nTaskSuiJI.name);
			if (item["Place"].str != "0" && text.Contains(item["Place"].str))
			{
				int whereChilidID = player.nomelTaskMag.getWhereChilidID(NTaskID, num);
				text = text.Replace(item["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
			}
			if (item["type"].I == 6)
			{
				text = text.Replace("{yiwunum}", string.Concat(player.nomelTaskMag.GetTaskSeid6AddItemNum(NTaskID, num)));
			}
			num++;
		}
		JSONObject whereTaskChildTypeList = player.nomelTaskMag.getWhereTaskChildTypeList(NTaskID);
		if (whereTaskChildTypeList != null && whereTaskChildTypeList.Count > 0)
		{
			text = text.Replace("{whereType}", (string)jsonData.instance.RandomMapType[whereTaskChildTypeList[0].I.ToString()][(object)"name"]);
		}
		return text.Replace("{lingshi}", string.Concat(player.nomelTaskMag.GetTaskMoney(NTaskID)));
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
