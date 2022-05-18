using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADE RID: 2782
	public class _LastWomenNameJsonData : IJSONClass
	{
		// Token: 0x060046E2 RID: 18146 RVA: 0x001E538C File Offset: 0x001E358C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._LastWomenNameJsonData.list)
			{
				try
				{
					_LastWomenNameJsonData lastWomenNameJsonData = new _LastWomenNameJsonData();
					lastWomenNameJsonData.id = jsonobject["id"].I;
					lastWomenNameJsonData.Name = jsonobject["Name"].Str;
					if (_LastWomenNameJsonData.DataDict.ContainsKey(lastWomenNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lastWomenNameJsonData.id));
					}
					else
					{
						_LastWomenNameJsonData.DataDict.Add(lastWomenNameJsonData.id, lastWomenNameJsonData);
						_LastWomenNameJsonData.DataList.Add(lastWomenNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_LastWomenNameJsonData.OnInitFinishAction != null)
			{
				_LastWomenNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F2D RID: 16173
		public static Dictionary<int, _LastWomenNameJsonData> DataDict = new Dictionary<int, _LastWomenNameJsonData>();

		// Token: 0x04003F2E RID: 16174
		public static List<_LastWomenNameJsonData> DataList = new List<_LastWomenNameJsonData>();

		// Token: 0x04003F2F RID: 16175
		public static Action OnInitFinishAction = new Action(_LastWomenNameJsonData.OnInitFinish);

		// Token: 0x04003F30 RID: 16176
		public int id;

		// Token: 0x04003F31 RID: 16177
		public string Name;
	}
}
