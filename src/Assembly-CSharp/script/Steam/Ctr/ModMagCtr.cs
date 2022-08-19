using System;
using System.Collections.Generic;
using script.Steam.UI;
using script.Steam.UI.Base;
using Steamworks;

namespace script.Steam.Ctr
{
	// Token: 0x020009E8 RID: 2536
	public class ModMagCtr
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x001DBA69 File Offset: 0x001D9C69
		public ModMagUI UI
		{
			get
			{
				return WorkShopMag.Inst.ModMagUI;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x001DBA75 File Offset: 0x001D9C75
		// (set) Token: 0x0600464A RID: 17994 RVA: 0x001DBA7D File Offset: 0x001D9C7D
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

		// Token: 0x0600464B RID: 17995 RVA: 0x001DBA97 File Offset: 0x001D9C97
		public List<ulong> GetSubscribeModList()
		{
			return this.subscribeModList;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x001DBAA0 File Offset: 0x001D9CA0
		public ModMagCtr()
		{
			this.ModNum = (int)SteamUGC.GetNumSubscribedItems();
			PublishedFileId_t[] array = new PublishedFileId_t[this.ModNum];
			SteamUGC.GetSubscribedItems(array, (uint)this.ModNum);
			foreach (PublishedFileId_t publishedFileId_t in array)
			{
				this.subscribeModList.Add(publishedFileId_t.m_PublishedFileId);
			}
			this.MaxPage = (uint)(this.ModNum / 50);
			if (this.ModNum % 50 != 0)
			{
				this.MaxPage += 1U;
			}
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x001DBB51 File Offset: 0x001D9D51
		public void AddSubscribeMod(ulong id)
		{
			if (this.subscribeModList.Contains(id))
			{
				return;
			}
			SteamUGC.DownloadItem(new PublishedFileId_t(id), false);
			this.subscribeModList.Add(id);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x001DBB7B File Offset: 0x001D9D7B
		public void UnSubscribeMod(ulong id)
		{
			if (!this.subscribeModList.Contains(id))
			{
				return;
			}
			this.subscribeModList.Remove(id);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x001DBB99 File Offset: 0x001D9D99
		public bool IsSubscribe(ulong id)
		{
			return this.subscribeModList.Contains(id);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x001DBBA7 File Offset: 0x001D9DA7
		public void OpenMod(ulong id)
		{
			WorkshopTool.OpenMod(id.ToString());
			UIPopTip.Inst.Pop("Mod已启用", PopTipIconType.叹号);
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x001DBBC5 File Offset: 0x001D9DC5
		public void CloseMod(ulong id)
		{
			WorkshopTool.CloseMod(id.ToString());
			UIPopTip.Inst.Pop("Mod已取消启用", PopTipIconType.叹号);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x001DBBE3 File Offset: 0x001D9DE3
		public void VoteMod(ulong id, bool flag)
		{
			SteamUGC.SetUserItemVote(new PublishedFileId_t(id), flag);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x001DBBF2 File Offset: 0x001D9DF2
		public bool IsOpen(ulong id)
		{
			return !WorkshopTool.CheckModIsDisable(id.ToString());
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x001DBC04 File Offset: 0x001D9E04
		public void AddPage()
		{
			if (this.CurPage + 1U > this.MaxPage)
			{
				return;
			}
			this.CurPage += 1U;
			this.UI.CurPage.SetText(string.Format("{0}/{1}", this.CurPage, this.MaxPage));
			this.UpdateList(false);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x001DBC68 File Offset: 0x001D9E68
		public void ReducePage()
		{
			if (this.CurPage - 1U < 1U)
			{
				return;
			}
			this.CurPage -= 1U;
			this.UI.CurPage.SetText(string.Format("{0}/{1}", this.CurPage, this.MaxPage));
			this.UpdateList(false);
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x001DBCC8 File Offset: 0x001D9EC8
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
			WorkShopMag.Inst.MoreModInfoUI.Hide();
			this.Clear();
			this.list = new List<ModUI>();
			CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(null);
			this.ugcQueryHandle = SteamUGC.CreateQueryUserUGCRequest(SteamUser.GetSteamID().GetAccountID(), 6, 2, 0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), this.CurPage);
			SteamUGC.SetRankedByTrendDays(this.ugcQueryHandle, 90U);
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
							ModUI modUI = WorkShopMag.Inst.ModPoolUI.Ctr.GetModUI(0);
							modUI.BindingInfo(modInfo);
							this.list.Add(modUI);
						}
					}
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

		// Token: 0x06004657 RID: 18007 RVA: 0x001DBDC0 File Offset: 0x001D9FC0
		private ulong GetItemSubscriptions(UGCQueryHandle_t handle, uint index)
		{
			ulong result = 1UL;
			SteamUGC.GetQueryUGCStatistic(handle, index, 0, ref result);
			return result;
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x001DBDDC File Offset: 0x001D9FDC
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

		// Token: 0x06004659 RID: 18009 RVA: 0x001DBE28 File Offset: 0x001DA028
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

		// Token: 0x040047D6 RID: 18390
		private readonly List<ulong> subscribeModList = new List<ulong>();

		// Token: 0x040047D7 RID: 18391
		private List<ModUI> list = new List<ModUI>();

		// Token: 0x040047D8 RID: 18392
		public int ModNum;

		// Token: 0x040047D9 RID: 18393
		private bool isFirstQuery = true;

		// Token: 0x040047DA RID: 18394
		private bool isQuerying;

		// Token: 0x040047DB RID: 18395
		public uint CurPage = 1U;

		// Token: 0x040047DC RID: 18396
		public uint MaxPage = 1U;

		// Token: 0x040047DD RID: 18397
		private UGCQueryHandle_t ugcQueryHandle;
	}
}
