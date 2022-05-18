using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADA RID: 2778
	public class _FaBaoLastNameJsonData : IJSONClass
	{
		// Token: 0x060046D2 RID: 18130 RVA: 0x001E4C7C File Offset: 0x001E2E7C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._FaBaoLastNameJsonData.list)
			{
				try
				{
					_FaBaoLastNameJsonData faBaoLastNameJsonData = new _FaBaoLastNameJsonData();
					faBaoLastNameJsonData.id = jsonobject["id"].I;
					faBaoLastNameJsonData.LastName = jsonobject["LastName"].Str;
					faBaoLastNameJsonData.Type = jsonobject["Type"].ToList();
					faBaoLastNameJsonData.quality = jsonobject["quality"].ToList();
					if (_FaBaoLastNameJsonData.DataDict.ContainsKey(faBaoLastNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", faBaoLastNameJsonData.id));
					}
					else
					{
						_FaBaoLastNameJsonData.DataDict.Add(faBaoLastNameJsonData.id, faBaoLastNameJsonData);
						_FaBaoLastNameJsonData.DataList.Add(faBaoLastNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_FaBaoLastNameJsonData.OnInitFinishAction != null)
			{
				_FaBaoLastNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EFD RID: 16125
		public static Dictionary<int, _FaBaoLastNameJsonData> DataDict = new Dictionary<int, _FaBaoLastNameJsonData>();

		// Token: 0x04003EFE RID: 16126
		public static List<_FaBaoLastNameJsonData> DataList = new List<_FaBaoLastNameJsonData>();

		// Token: 0x04003EFF RID: 16127
		public static Action OnInitFinishAction = new Action(_FaBaoLastNameJsonData.OnInitFinish);

		// Token: 0x04003F00 RID: 16128
		public int id;

		// Token: 0x04003F01 RID: 16129
		public string LastName;

		// Token: 0x04003F02 RID: 16130
		public List<int> Type = new List<int>();

		// Token: 0x04003F03 RID: 16131
		public List<int> quality = new List<int>();
	}
}
