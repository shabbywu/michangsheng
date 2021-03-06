using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCC RID: 3020
	public class EquipSeidJsonData4 : IJSONClass
	{
		// Token: 0x06004A98 RID: 19096 RVA: 0x001F8FF0 File Offset: 0x001F71F0
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

		// Token: 0x06004A99 RID: 19097 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004643 RID: 17987
		public static int SEIDID = 4;

		// Token: 0x04004644 RID: 17988
		public static Dictionary<int, EquipSeidJsonData4> DataDict = new Dictionary<int, EquipSeidJsonData4>();

		// Token: 0x04004645 RID: 17989
		public static List<EquipSeidJsonData4> DataList = new List<EquipSeidJsonData4>();

		// Token: 0x04004646 RID: 17990
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData4.OnInitFinish);

		// Token: 0x04004647 RID: 17991
		public int id;

		// Token: 0x04004648 RID: 17992
		public int value1;
	}
}
