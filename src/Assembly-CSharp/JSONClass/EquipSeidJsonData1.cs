using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000838 RID: 2104
	public class EquipSeidJsonData1 : IJSONClass
	{
		// Token: 0x06003EF2 RID: 16114 RVA: 0x001AE2AC File Offset: 0x001AC4AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[1].list)
			{
				try
				{
					EquipSeidJsonData1 equipSeidJsonData = new EquipSeidJsonData1();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData1.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData1.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData1.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData1.OnInitFinishAction != null)
			{
				EquipSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AC3 RID: 15043
		public static int SEIDID = 1;

		// Token: 0x04003AC4 RID: 15044
		public static Dictionary<int, EquipSeidJsonData1> DataDict = new Dictionary<int, EquipSeidJsonData1>();

		// Token: 0x04003AC5 RID: 15045
		public static List<EquipSeidJsonData1> DataList = new List<EquipSeidJsonData1>();

		// Token: 0x04003AC6 RID: 15046
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData1.OnInitFinish);

		// Token: 0x04003AC7 RID: 15047
		public int id;

		// Token: 0x04003AC8 RID: 15048
		public int value1;
	}
}
