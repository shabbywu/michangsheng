using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D87 RID: 3463
	public class ItemDatebase : MonoBehaviour
	{
		// Token: 0x06005384 RID: 21380 RVA: 0x0003BBCA File Offset: 0x00039DCA
		private void Awake()
		{
			ItemDatebase.Inst = this;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0003BBD2 File Offset: 0x00039DD2
		public void Preload(int taskID)
		{
			this.LoadSync();
			Loom.RunAsync(delegate
			{
				this.LoadAsync(taskID);
			});
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0022CC88 File Offset: 0x0022AE88
		public void LoadSync()
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
			}
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0022CD20 File Offset: 0x0022AF20
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
			}
		}

		// Token: 0x04005340 RID: 21312
		public static ItemDatebase Inst;

		// Token: 0x04005341 RID: 21313
		public Dictionary<int, item> items = new Dictionary<int, item>();

		// Token: 0x04005342 RID: 21314
		public List<Texture2D> PingZhi;

		// Token: 0x04005343 RID: 21315
		public List<Sprite> PingZhiUp;
	}
}
