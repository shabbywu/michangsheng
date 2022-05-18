using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200142D RID: 5165
	[CommandInfo("YSTask", "NTaskText", "", 0)]
	[AddComponentMenu("")]
	public class NTaskText : Command
	{
		// Token: 0x06007CFD RID: 31997 RVA: 0x0005496F File Offset: 0x00052B6F
		public override void OnEnter()
		{
			this.Desc.Value = NTaskText.GetNTaskDesc(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x002C5E8C File Offset: 0x002C408C
		public static string GetNTaskDesc(int NTaskID)
		{
			Avatar player = Tools.instance.getPlayer();
			try
			{
				if (!player.nomelTaskMag.HasNTask(NTaskID))
				{
					player.nomelTaskMag.DeDaiSetWhereNode(NTaskID, false);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			NTaskXiangXi ntaskXiangXiData = player.nomelTaskMag.GetNTaskXiangXiData(NTaskID);
			List<JSONObject> ntaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(NTaskID);
			string text = ntaskXiangXiData.SayMiaoShu ?? "";
			int num = 0;
			foreach (JSONObject jsonobject in ntaskXiangXiList)
			{
				int chilidID = player.nomelTaskMag.getChilidID(NTaskID, num);
				NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[chilidID];
				string str = jsonobject["TaskID"].str;
				text = text.Replace(str, ntaskSuiJI.name);
				if (jsonobject["Place"].str != "0" && text.Contains(jsonobject["Place"].str))
				{
					int whereChilidID = player.nomelTaskMag.getWhereChilidID(NTaskID, num);
					text = text.Replace(jsonobject["Place"].str, NTaskSuiJI.DataDict[whereChilidID].name);
				}
				if (jsonobject["type"].I == 6)
				{
					text = text.Replace("{yiwunum}", string.Concat(player.nomelTaskMag.GetTaskSeid6AddItemNum(NTaskID, num)));
				}
				num++;
			}
			JSONObject whereTaskChildTypeList = player.nomelTaskMag.getWhereTaskChildTypeList(NTaskID);
			if (whereTaskChildTypeList != null && whereTaskChildTypeList.Count > 0)
			{
				text = text.Replace("{whereType}", (string)jsonData.instance.RandomMapType[whereTaskChildTypeList[0].I.ToString()]["name"]);
			}
			text = text.Replace("{lingshi}", string.Concat(player.nomelTaskMag.GetTaskMoney(NTaskID)));
			return text;
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ABB RID: 27323
		[Tooltip("需要获取描述的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04006ABC RID: 27324
		[Tooltip("需要到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Desc;
	}
}
