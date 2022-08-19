using System;
using System.Collections.Generic;
using script.Steam.UI;
using script.Steam.UI.Base;
using Steamworks;

namespace script.Steam.Ctr
{
	// Token: 0x020009EB RID: 2539
	public class WorkShopCtr
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600466F RID: 18031 RVA: 0x001DCA9D File Offset: 0x001DAC9D
		public WorkShopUI UI
		{
			get
			{
				return WorkShopMag.Inst.WorkShopUI;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x001DCAA9 File Offset: 0x001DACA9
		// (set) Token: 0x06004671 RID: 18033 RVA: 0x001DCAB1 File Offset: 0x001DACB1
		public bool IsQuerying
		{
			get
			{
				return this.isQuerying;
			}
			set
			{
				this.UI.Loading.SetActive(value);
				this.isQuerying = value;
			}
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x001DCACB File Offset: 0x001DACCB
		public void AddPage()
		{
			if (this.CurPage + 1U > this.MaxPage)
			{
				return;
			}
			this.CurPage += 1U;
			this.UpdateList(false);
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x001DCAF3 File Offset: 0x001DACF3
		public void ReducePage()
		{
			if (this.CurPage - 1U < 1U)
			{
				return;
			}
			this.CurPage -= 1U;
			this.UpdateList(false);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x001DCB16 File Offset: 0x001DAD16
		public void SetQueryType(int value)
		{
			switch (value)
			{
			case 0:
				this.queryType = 12;
				break;
			case 1:
				this.queryType = 1;
				break;
			case 2:
				this.queryType = 0;
				break;
			}
			this.UpdateList(false);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x001DCB4D File Offset: 0x001DAD4D
		public void AddTag(string tag)
		{
			if (this.tags.Contains(tag))
			{
				return;
			}
			this.tags.Add(tag);
			this.UpdateList(false);
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x001DCB71 File Offset: 0x001DAD71
		public void RemoveTag(string tag)
		{
			if (!this.tags.Contains(tag))
			{
				return;
			}
			this.tags.Remove(tag);
			this.UpdateList(false);
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x001DCB98 File Offset: 0x001DAD98
		public void UpdateList(bool isWithOutIsQuerying = false)
		{
			if (!isWithOutIsQuerying)
			{
				if (this.IsQuerying)
				{
					UIPopTip.Inst.Pop("正在查询中，请稍等", PopTipIconType.叹号);
					return;
				}
				if (!this.isFirstQuery)
				{
					SteamUGC.ReleaseQueryUGCRequest(this.ugcQueryHandle);
				}
			}
			this.Clear();
			this.list = new List<ModUI>();
			CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(null);
			this.ugcQueryHandle = SteamUGC.CreateQueryAllUGCRequest(this.queryType, 2, SteamUtils.GetAppID(), SteamUtils.GetAppID(), this.CurPage);
			foreach (string text in this.tags)
			{
				SteamUGC.AddRequiredTag(this.ugcQueryHandle, text);
			}
			SteamUGC.SetRankedByTrendDays(this.ugcQueryHandle, 90U);
			SteamUGC.SetAllowCachedResponse(this.ugcQueryHandle, 300U);
			SteamUGC.SetReturnChildren(this.ugcQueryHandle, true);
			SteamUGC.SetReturnMetadata(this.ugcQueryHandle, true);
			SteamUGC.SetReturnKeyValueTags(this.ugcQueryHandle, true);
			SteamUGC.SetReturnLongDescription(this.ugcQueryHandle, true);
			SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.ugcQueryHandle);
			this.IsQuerying = true;
			callResult.Set(steamAPICall_t, delegate(SteamUGCQueryCompleted_t t, bool failure)
			{
				if (!this.UI.IsActive())
				{
					this.isFirstQuery = false;
					this.IsQuerying = false;
					return;
				}
				if (t.m_eResult == 1)
				{
					this.SetMaxPage(t.m_unTotalMatchingResults);
					for (uint num = 0U; num < t.m_unNumResultsReturned; num += 1U)
					{
						SteamUGCDetails_t steamUGCDetails_t;
						SteamUGC.GetQueryUGCResult(this.ugcQueryHandle, num, ref steamUGCDetails_t);
						if (!string.IsNullOrEmpty(steamUGCDetails_t.m_rgchTitle))
						{
							ModInfo modInfo;
							if (WorkShopMag.Inst.ModInfoDict.ContainsKey(steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId))
							{
								modInfo = WorkShopMag.Inst.ModInfoDict[steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId];
							}
							else
							{
								modInfo = new ModInfo();
								modInfo.Name = steamUGCDetails_t.m_rgchTitle;
								modInfo.Id = steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId;
								modInfo.Desc = steamUGCDetails_t.m_rgchDescription;
								modInfo.SetTags(steamUGCDetails_t.m_rgchTags);
								SteamUGC.GetQueryUGCPreviewURL(this.ugcQueryHandle, num, ref modInfo.ImgUrl, (uint)steamUGCDetails_t.m_nPreviewFileSize);
								string author = "";
								SteamUGC.GetQueryUGCMetadata(this.ugcQueryHandle, num, ref author, 5000U);
								modInfo.SetAuthor(author);
								modInfo.UpNum = (int)steamUGCDetails_t.m_unVotesUp;
								modInfo.DownNum = (int)steamUGCDetails_t.m_unVotesDown;
								modInfo.Subscriptions = this.GetItemSubscriptions(this.ugcQueryHandle, num);
								modInfo.DependencyList = this.GetDependencies(this.ugcQueryHandle, num, steamUGCDetails_t.m_unNumChildren);
								WorkShopMag.Inst.ModInfoDict.Add(steamUGCDetails_t.m_nPublishedFileId.m_PublishedFileId, modInfo);
							}
							ModUI modUI = WorkShopMag.Inst.ModPoolUI.Ctr.GetModUI(1);
							modUI.BindingInfo(modInfo);
							this.list.Add(modUI);
						}
					}
					this.IsQuerying = false;
					this.UI.CurPage.SetText(string.Format("{0}/{1}", this.CurPage, this.MaxPage));
					this.IsQuerying = false;
					if (this.isFirstQuery)
					{
						this.isFirstQuery = false;
						return;
					}
				}
				else
				{
					UIPopTip.Inst.Pop("获取MOD列表失败，请重试", PopTipIconType.叹号);
				}
			});
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x001DCCD4 File Offset: 0x001DAED4
		public void SetMaxPage(uint num)
		{
			this.MaxPage = num / 50U;
			if (num % 50U != 0U)
			{
				this.MaxPage += 1U;
			}
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x001DCCF4 File Offset: 0x001DAEF4
		public void SubscriptionMod(ulong id, bool isShow = true)
		{
			SteamUGC.SubscribeItem(new PublishedFileId_t(id));
			WorkShopMag.Inst.ModMagUI.Ctr.AddSubscribeMod(id);
			if (isShow)
			{
				UIPopTip.Inst.Pop("订阅成功", PopTipIconType.叹号);
			}
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x001DCD2A File Offset: 0x001DAF2A
		public void UnSubscriptionMod(ulong id)
		{
			SteamUGC.UnsubscribeItem(new PublishedFileId_t(id));
			WorkShopMag.Inst.ModMagUI.Ctr.UnSubscribeMod(id);
			UIPopTip.Inst.Pop("取消订阅成功", PopTipIconType.叹号);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001DCD60 File Offset: 0x001DAF60
		public List<ulong> GetNoSubscriptDependency(ModInfo info)
		{
			List<ulong> list = new List<ulong>();
			foreach (ulong num in info.DependencyList)
			{
				if (!WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(num))
				{
					list.Add(num);
				}
			}
			return list;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001DCDD4 File Offset: 0x001DAFD4
		private ulong GetItemSubscriptions(UGCQueryHandle_t handle, uint index)
		{
			ulong result = 1UL;
			SteamUGC.GetQueryUGCStatistic(handle, index, 0, ref result);
			return result;
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001DCDF0 File Offset: 0x001DAFF0
		private List<ulong> GetDependencies(UGCQueryHandle_t handle, uint index, uint count)
		{
			PublishedFileId_t[] array = new PublishedFileId_t[count];
			SteamUGC.GetQueryUGCChildren(handle, index, array, count);
			List<ulong> list = new List<ulong>();
			foreach (PublishedFileId_t publishedFileId_t in array)
			{
				list.Add(publishedFileId_t.m_PublishedFileId);
			}
			return list;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001DCE3C File Offset: 0x001DB03C
		public void Clear()
		{
			if (this.UI.CurSelect != null)
			{
				this.UI.CurSelect.CancelSelect();
			}
			WorkShopMag.Inst.MoreModInfoUI.Hide();
			this.UI.CurSelect = null;
			foreach (ModUI modUI in this.list)
			{
				WorkShopMag.Inst.ModPoolUI.Ctr.BackMod(modUI);
			}
			this.UI.CurSelect = null;
		}

		// Token: 0x040047E3 RID: 18403
		private EUGCQuery queryType = 12;

		// Token: 0x040047E4 RID: 18404
		private List<string> tags = new List<string>();

		// Token: 0x040047E5 RID: 18405
		private List<ModUI> list = new List<ModUI>();

		// Token: 0x040047E6 RID: 18406
		private bool isFirstQuery = true;

		// Token: 0x040047E7 RID: 18407
		private bool isQuerying;

		// Token: 0x040047E8 RID: 18408
		public uint CurPage = 1U;

		// Token: 0x040047E9 RID: 18409
		public uint MaxPage = 1U;

		// Token: 0x040047EA RID: 18410
		private UGCQueryHandle_t ugcQueryHandle;
	}
}
