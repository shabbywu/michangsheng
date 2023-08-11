using System;
using System.Collections.Generic;
using Bag;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;

namespace script.Submit;

public class SubmitOpenMag
{
	public static void OpenLianQiSub(int taskId)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		int i = Tools.instance.getPlayer().NomelTaskJson[taskId.ToString()]["TaskChild"][0].I;
		NTaskSuiJI nTaskSuiJI = NTaskSuiJI.DataDict[i];
		int quality = nTaskSuiJI.Value % 10 + 1;
		int type = nTaskSuiJI.Value / 10 - 1;
		int i2 = Tools.instance.getPlayer().nomelTaskMag.GetNTaskXiangXiList(taskId)[0]["num"].I;
		ResManager.inst.LoadPrefab("SubmitPanel").Inst();
		Func<BaseItem, bool> canPut = delegate(BaseItem item)
		{
			EquipItem equipItem = (EquipItem)item;
			string name = equipItem.GetName();
			if (name == "金影剑")
			{
				Debug.Log((object)name);
			}
			if (equipItem.GetEquipQualityType() != EquipQuality.上品)
			{
				return false;
			}
			List<int> shuXingList = equipItem.GetShuXingList();
			if (equipItem.GetImgQuality() != quality)
			{
				return false;
			}
			if (shuXingList == null || shuXingList.Count != 1)
			{
				return false;
			}
			return (shuXingList[0] == type) ? true : false;
		};
		SubmitUIMag.Inst.OpenLianQi(canPut, (UnityAction)delegate
		{
			Tools.instance.getPlayer().nomelTaskMag.EndNTask(taskId);
		}, $"需提交{i2}件{nTaskSuiJI.name}", i2);
	}
}
