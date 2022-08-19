using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000834 RID: 2100
	public class DropTextJsonData : IJSONClass
	{
		// Token: 0x06003EE2 RID: 16098 RVA: 0x001ADD64 File Offset: 0x001ABF64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DropTextJsonData.list)
			{
				try
				{
					DropTextJsonData dropTextJsonData = new DropTextJsonData();
					dropTextJsonData.id = jsonobject["id"].I;
					dropTextJsonData.Text = jsonobject["Text"].Str;
					if (DropTextJsonData.DataDict.ContainsKey(dropTextJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DropTextJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dropTextJsonData.id));
					}
					else
					{
						DropTextJsonData.DataDict.Add(dropTextJsonData.id, dropTextJsonData);
						DropTextJsonData.DataList.Add(dropTextJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DropTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DropTextJsonData.OnInitFinishAction != null)
			{
				DropTextJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AAF RID: 15023
		public static Dictionary<int, DropTextJsonData> DataDict = new Dictionary<int, DropTextJsonData>();

		// Token: 0x04003AB0 RID: 15024
		public static List<DropTextJsonData> DataList = new List<DropTextJsonData>();

		// Token: 0x04003AB1 RID: 15025
		public static Action OnInitFinishAction = new Action(DropTextJsonData.OnInitFinish);

		// Token: 0x04003AB2 RID: 15026
		public int id;

		// Token: 0x04003AB3 RID: 15027
		public string Text;
	}
}
