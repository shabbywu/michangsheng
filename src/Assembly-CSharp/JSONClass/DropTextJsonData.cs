using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC8 RID: 3016
	public class DropTextJsonData : IJSONClass
	{
		// Token: 0x06004A88 RID: 19080 RVA: 0x001F8B54 File Offset: 0x001F6D54
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

		// Token: 0x06004A89 RID: 19081 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400462C RID: 17964
		public static Dictionary<int, DropTextJsonData> DataDict = new Dictionary<int, DropTextJsonData>();

		// Token: 0x0400462D RID: 17965
		public static List<DropTextJsonData> DataList = new List<DropTextJsonData>();

		// Token: 0x0400462E RID: 17966
		public static Action OnInitFinishAction = new Action(DropTextJsonData.OnInitFinish);

		// Token: 0x0400462F RID: 17967
		public int id;

		// Token: 0x04004630 RID: 17968
		public string Text;
	}
}
