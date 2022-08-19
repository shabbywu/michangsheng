using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A5F RID: 2655
	public class ItemDatebase : MonoBehaviour
	{
		// Token: 0x06004A84 RID: 19076 RVA: 0x001FA83E File Offset: 0x001F8A3E
		private void Awake()
		{
			ItemDatebase.Inst = this;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x001FA848 File Offset: 0x001F8A48
		public void Preload(int taskID)
		{
			this.LoadSync(taskID);
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x001FA888 File Offset: 0x001F8A88
		public void LoadSync(int taskID)
		{
			try
			{
				for (int i = 1; i <= 6; i++)
				{
					this.PingZhi.Add(ResManager.inst.LoadTexture2D("Ui Icon/tab/item" + i));
					this.PingZhiUp.Add(ResManager.inst.LoadSprite("Ui Icon/tab/itemUP" + i));
				}
			}
			catch (Exception arg)
			{
				PreloadManager.IsException = true;
				PreloadManager.ExceptionData += string.Format("{0}\n", arg);
				PreloadManager.Inst.TaskDone(taskID);
			}
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x001FA92C File Offset: 0x001F8B2C
		public void LoadAsync(int taskID)
		{
			try
			{
				foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
				{
					this.items.Add(jsonobject["id"].I, new item(jsonobject["id"].I));
				}
				PreloadManager.Inst.TaskDone(taskID);
			}
			catch (Exception arg)
			{
				PreloadManager.IsException = true;
				PreloadManager.ExceptionData += string.Format("{0}\n", arg);
				PreloadManager.Inst.TaskDone(taskID);
			}
		}

		// Token: 0x040049B1 RID: 18865
		public static ItemDatebase Inst;

		// Token: 0x040049B2 RID: 18866
		public Dictionary<int, item> items = new Dictionary<int, item>();

		// Token: 0x040049B3 RID: 18867
		public List<Texture2D> PingZhi;

		// Token: 0x040049B4 RID: 18868
		public List<Sprite> PingZhiUp;
	}
}
