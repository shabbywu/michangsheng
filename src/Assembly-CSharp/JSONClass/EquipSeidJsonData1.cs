using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC9 RID: 3017
	public class EquipSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004A8C RID: 19084 RVA: 0x001F8C78 File Offset: 0x001F6E78
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

		// Token: 0x06004A8D RID: 19085 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004631 RID: 17969
		public static int SEIDID = 1;

		// Token: 0x04004632 RID: 17970
		public static Dictionary<int, EquipSeidJsonData1> DataDict = new Dictionary<int, EquipSeidJsonData1>();

		// Token: 0x04004633 RID: 17971
		public static List<EquipSeidJsonData1> DataList = new List<EquipSeidJsonData1>();

		// Token: 0x04004634 RID: 17972
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData1.OnInitFinish);

		// Token: 0x04004635 RID: 17973
		public int id;

		// Token: 0x04004636 RID: 17974
		public int value1;
	}
}
