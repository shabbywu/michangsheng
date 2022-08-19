using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F75 RID: 3957
	[CommandInfo("YSTask", "NTaskText", "", 0)]
	[AddComponentMenu("")]
	public class NTaskText : Command
	{
		// Token: 0x06006F0B RID: 28427 RVA: 0x002A6192 File Offset: 0x002A4392
		public override void OnEnter()
		{
			this.Desc.Value = NTaskText.GetNTaskDesc(this.NTaskID.Value);
			this.Continue();
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x002A61B8 File Offset: 0x002A43B8
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

		// Token: 0x06006F0D RID: 28429 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BE5 RID: 23525
		[Tooltip("需要获取描述的任务ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable NTaskID;

		// Token: 0x04005BE6 RID: 23526
		[Tooltip("需要到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable Desc;
	}
}
