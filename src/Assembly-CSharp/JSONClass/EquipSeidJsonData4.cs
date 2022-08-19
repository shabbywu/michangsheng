using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083B RID: 2107
	public class EquipSeidJsonData4 : IJSONClass
	{
		// Token: 0x06003EFE RID: 16126 RVA: 0x001AE6B4 File Offset: 0x001AC8B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[4].list)
			{
				try
				{
					EquipSeidJsonData4 equipSeidJsonData = new EquipSeidJsonData4();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData4.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData4.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData4.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData4.OnInitFinishAction != null)
			{
				EquipSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AD5 RID: 15061
		public static int SEIDID = 4;

		// Token: 0x04003AD6 RID: 15062
		public static Dictionary<int, EquipSeidJsonData4> DataDict = new Dictionary<int, EquipSeidJsonData4>();

		// Token: 0x04003AD7 RID: 15063
		public static List<EquipSeidJsonData4> DataList = new List<EquipSeidJsonData4>();

		// Token: 0x04003AD8 RID: 15064
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData4.OnInitFinish);

		// Token: 0x04003AD9 RID: 15065
		public int id;

		// Token: 0x04003ADA RID: 15066
		public int value1;
	}
}
