using System;
using System.Collections.Generic;
using UnityEngine;

namespace script.Steam
{
	// Token: 0x020009D4 RID: 2516
	public class ModInfo
	{
		// Token: 0x060045E4 RID: 17892 RVA: 0x001D9830 File Offset: 0x001D7A30
		public void ShowImg(Action<Sprite> callBack)
		{
			if (this.sprite == null)
			{
				WorkShopMag.Inst.downUtils.DownSpriteByUrl(this.ImgUrl, delegate(Sprite _)
				{
					this.sprite = _;
					callBack(this.sprite);
				});
				return;
			}
			callBack(this.sprite);
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x001D9894 File Offset: 0x001D7A94
		public void SetTags(string tags)
		{
			if (string.IsNullOrEmpty(tags))
			{
				this.Tags = "无";
				return;
			}
			foreach (string text in WorkShopMag.TagsDict.Keys)
			{
				if (tags.Contains(WorkShopMag.TagsDict[text]))
				{
					tags = tags.Replace(WorkShopMag.TagsDict[text], text);
				}
			}
			this.Tags = tags;
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x001D9928 File Offset: 0x001D7B28
		public void SetAuthor(string author)
		{
			if (string.IsNullOrEmpty(author))
			{
				this.Author = "未知";
				return;
			}
			this.Author = author;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x001D9948 File Offset: 0x001D7B48
		public string GetLv()
		{
			if (this.UpNum + this.DownNum <= 10)
			{
				return "暂无";
			}
			int num = (int)((float)this.UpNum / (float)(this.UpNum + this.DownNum) * 100f);
			return string.Format("{0}%", num);
		}

		// Token: 0x0400474B RID: 18251
		public ulong Id;

		// Token: 0x0400474C RID: 18252
		public List<ulong> DependencyList;

		// Token: 0x0400474D RID: 18253
		public string Author;

		// Token: 0x0400474E RID: 18254
		public string Name;

		// Token: 0x0400474F RID: 18255
		public string Desc;

		// Token: 0x04004750 RID: 18256
		public string Tags;

		// Token: 0x04004751 RID: 18257
		public string ImgUrl;

		// Token: 0x04004752 RID: 18258
		public ulong Subscriptions;

		// Token: 0x04004753 RID: 18259
		public bool IsUp;

		// Token: 0x04004754 RID: 18260
		public bool IsDown;

		// Token: 0x04004755 RID: 18261
		public int UpNum;

		// Token: 0x04004756 RID: 18262
		public int DownNum;

		// Token: 0x04004757 RID: 18263
		private Sprite sprite;
	}
}
