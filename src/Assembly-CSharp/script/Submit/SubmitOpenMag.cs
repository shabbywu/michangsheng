using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;

namespace script.Submit
{
	// Token: 0x02000AB9 RID: 2745
	public class SubmitOpenMag
	{
		// Token: 0x06004630 RID: 17968 RVA: 0x001DEEBC File Offset: 0x001DD0BC
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
