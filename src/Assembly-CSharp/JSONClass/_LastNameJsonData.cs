using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000745 RID: 1861
	public class _LastNameJsonData : IJSONClass
	{
		// Token: 0x06003B28 RID: 15144 RVA: 0x00196F3C File Offset: 0x0019513C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._LastNameJsonData.list)
			{
				try
				{
					_LastNameJsonData lastNameJsonData = new _LastNameJsonData();
					lastNameJsonData.id = jsonobject["id"].I;
					lastNameJsonData.Name = jsonobject["Name"].Str;
					if (_LastNameJsonData.DataDict.ContainsKey(lastNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lastNameJsonData.id));
					}
					else
					{
						_LastNameJsonData.DataDict.Add(lastNameJsonData.id, lastNameJsonData);
						_LastNameJsonData.DataList.Add(lastNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_LastNameJsonData.OnInitFinishAction != null)
			{
				_LastNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400338F RID: 13199
		public static Dictionary<int, _LastNameJsonData> DataDict = new Dictionary<int, _LastNameJsonData>();

		// Token: 0x04003390 RID: 13200
		public static List<_LastNameJsonData> DataList = new List<_LastNameJsonData>();

		// Token: 0x04003391 RID: 13201
		public static Action OnInitFinishAction = new Action(_LastNameJsonData.OnInitFinish);

		// Token: 0x04003392 RID: 13202
		public int id;

		// Token: 0x04003393 RID: 13203
		public string Name;
	}
}
