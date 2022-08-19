using System;
using System.Collections.Generic;
using Steamworks;

namespace script.Steam
{
	// Token: 0x020009D5 RID: 2517
	[Serializable]
	public class WorkShopItem
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x001D999A File Offset: 0x001D7B9A
		public WorkShopItem()
		{
			this.Tags = new List<string>();
			this.Dependencies = new List<ulong>();
			this.LastDependencies = new List<ulong>();
			this.Visibility = 2;
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x001D99D1 File Offset: 0x001D7BD1
		public void AddTags(string tag)
		{
			if (!this.Tags.Contains(tag))
			{
				this.Tags.Add(tag);
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x001D99ED File Offset: 0x001D7BED
		public void RemoveTag(string tag)
		{
			if (this.Tags.Contains(tag))
			{
				this.Tags.Remove(tag);
			}
		}

		// Token: 0x04004758 RID: 18264
		public PublishedFileId_t PublishedFileId;

		// Token: 0x04004759 RID: 18265
		public string Title;

		// Token: 0x0400475A RID: 18266
		public string Des;

		// Token: 0x0400475B RID: 18267
		public ulong SteamID;

		// Token: 0x0400475C RID: 18268
		public List<string> Tags;

		// Token: 0x0400475D RID: 18269
		public int Visibility;

		// Token: 0x0400475E RID: 18270
		public string ModPath;

		// Token: 0x0400475F RID: 18271
		public string ImgPath;

		// Token: 0x04004760 RID: 18272
		public bool IsNeedNext = true;

		// Token: 0x04004761 RID: 18273
		public List<ulong> Dependencies;

		// Token: 0x04004762 RID: 18274
		public List<ulong> LastDependencies;
	}
}
