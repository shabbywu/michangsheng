using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;

namespace script.Submit
{
	// Token: 0x020009D1 RID: 2513
	public class SubmitOpenMag
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x001D91A8 File Offset: 0x001D73A8
		public static void OpenLianQiSub(int taskId)
		{
			int i = Tools.instance.getPlayer().NomelTaskJson[taskId.ToString()]["TaskChild"][0].I;
			NTaskSuiJI ntaskSuiJI = NTaskSuiJI.DataDict[i];
			int quality = ntaskSuiJI.Value % 10 + 1;
			int type = ntaskSuiJI.Value / 10 - 1;
			int i2 = Tools.instance.getPlayer().nomelTaskMag.GetNTaskXiangXiList(taskId)[0]["num"].I;
			ResManager.inst.LoadPrefab("SubmitPanel").Inst(null);
			Func<BaseItem, bool> canPut = delegate(BaseItem item)
			{
				EquipItem equipItem = (EquipItem)item;
				string name = equipItem.GetName();
				if (name == "金影剑")
				{
					Debug.Log(name);
				}
				if (equipItem.GetEquipQualityType() != EquipQuality.上品)
				{
					return false;
				}
				List<int> shuXingList = equipItem.GetShuXingList();
				return equipItem.GetImgQuality() == quality && shuXingList != null && shuXingList.Count == 1 && shuXingList[0] == type;
			};
			SubmitUIMag.Inst.OpenLianQi(canPut, delegate
			{
				Tools.instance.getPlayer().nomelTaskMag.EndNTask(taskId);
			}, string.Format("需提交{0}件{1}", i2, ntaskSuiJI.name), i2);
		}
	}
}
