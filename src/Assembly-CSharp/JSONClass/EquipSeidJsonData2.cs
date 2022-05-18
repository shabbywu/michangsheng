using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCA RID: 3018
	public class EquipSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004A90 RID: 19088 RVA: 0x001F8DA0 File Offset: 0x001F6FA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[2].list)
			{
				try
				{
					EquipSeidJsonData2 equipSeidJsonData = new EquipSeidJsonData2();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData2.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData2.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData2.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData2.OnInitFinishAction != null)
			{
				EquipSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004637 RID: 17975
		public static int SEIDID = 2;

		// Token: 0x04004638 RID: 17976
		public static Dictionary<int, EquipSeidJsonData2> DataDict = new Dictionary<int, EquipSeidJsonData2>();

		// Token: 0x04004639 RID: 17977
		public static List<EquipSeidJsonData2> DataList = new List<EquipSeidJsonData2>();

		// Token: 0x0400463A RID: 17978
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData2.OnInitFinish);

		// Token: 0x0400463B RID: 17979
		public int id;

		// Token: 0x0400463C RID: 17980
		public int value1;
	}
}
