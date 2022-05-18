using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCD RID: 3021
	public class EquipSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004A9C RID: 19100 RVA: 0x001F9118 File Offset: 0x001F7318
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[8].list)
			{
				try
				{
					EquipSeidJsonData8 equipSeidJsonData = new EquipSeidJsonData8();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData8.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData8.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData8.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData8.OnInitFinishAction != null)
			{
				EquipSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004649 RID: 17993
		public static int SEIDID = 8;

		// Token: 0x0400464A RID: 17994
		public static Dictionary<int, EquipSeidJsonData8> DataDict = new Dictionary<int, EquipSeidJsonData8>();

		// Token: 0x0400464B RID: 17995
		public static List<EquipSeidJsonData8> DataList = new List<EquipSeidJsonData8>();

		// Token: 0x0400464C RID: 17996
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData8.OnInitFinish);

		// Token: 0x0400464D RID: 17997
		public int id;

		// Token: 0x0400464E RID: 17998
		public int value1;
	}
}
