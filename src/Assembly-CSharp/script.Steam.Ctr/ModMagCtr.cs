using System.Collections.Generic;
using Steamworks;
using script.Steam.UI;
using script.Steam.UI.Base;

namespace script.Steam.Ctr;

public class ModMagCtr
{
	private readonly List<ulong> subscribeModList = new List<ulong>();

	private List<ModUI> list = new List<ModUI>();

	public int ModNum;

	private bool isFirstQuery = true;

	private bool isQuerying;

	public uint CurPage = 1u;

	public uint MaxPage = 1u;

	private UGCQueryHandle_t ugcQueryHandle;

	public ModMagUI UI => WorkShopMag.Inst.ModMagUI;

	public bool IsQuerying
	{
		get
		{
			return isQuerying;
		}
		set
		{
			UI.Loading.SetActive(value);
			isQuerying = value;
		}
	}

	public List<ulong> GetSubscribeModList()
	{
		return subscribeModList;
	}

	public ModMagCtr()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		ModNum = (int)SteamUGC.GetNumSubscribedItems();
		PublishedFileId_t[] array = new PublishedFileId_t[ModNum];
		SteamUGC.GetSubscribedItems((PublishedFileId_t[])(object)array, (uint)ModNum);
		PublishedFileId_t[] array2 = (PublishedFileId_t[])(object)array;
		foreach (PublishedFileId_t val in array2)
		{
			subscribeModList.Add(val.m_PublishedFileId);
		}
		MaxPage = (uint)ModNum / 50u;
		if (ModNum % 50 != 0)
		{
			MaxPage++;
		}
	}

	public void AddSubscribeMod(ulong id)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		if (!subscribeModList.Contains(id))
		{
			SteamUGC.DownloadItem(new PublishedFileId_t(id), false);
			subscribeModList.Add(id);
		}
	}

	public void UnSubscribeMod(ulong id)
	{
		if (subscribeModList.Contains(id))
		{
			subscribeModList.Remove(id);
		}
	}

	public bool IsSubscribe(ulong id)
	{
		return subscribeModList.Contains(id);
	}

	public void OpenMod(ulong id)
	{
		WorkshopTool.OpenMod(id.ToString());
		UIPopTip.Inst.Pop("Mod已启用");
	}

	public void CloseMod(ulong id)
	{
		WorkshopTool.CloseMod(id.ToString());
		UIPopTip.Inst.Pop("Mod已取消启用");
	}

	public void VoteMod(ulong id, bool flag)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SteamUGC.SetUserItemVote(new PublishedFileId_t(id), flag);
	}

	public bool IsOpen(ulong id)
	{
		return !WorkshopTool.CheckModIsDisable(id.ToString());
	}

	public void AddPage()
	{
		if (CurPage + 1 <= MaxPage)
		{
			CurPage++;
			UI.CurPage.SetText($"{CurPage}/{MaxPage}");
			UpdateList();
		}
	}

	public void ReducePage()
	{
		if (CurPage - 1 >= 1)
		{
			CurPage--;
			UI.CurPage.SetText($"{CurPage}/{MaxPage}");
			UpdateList();
		}
	}

	public void UpdateList(bool isWithOutIsQuerying = false)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (!isWithOutIsQuerying)
		{
			if (IsQuerying)
			{
				UIPopTip.Inst.Pop("正在查询中，请稍等");
				return;
			}
			if (!isFirstQuery)
			{
				SteamUGC.ReleaseQueryUGCRequest(ugcQueryHandle);
			}
		}
		WorkShopMag.Inst.MoreModInfoUI.Hide();
		Clear();
		list = new List<ModUI>();
		CallResult<SteamUGCQueryCompleted_t> obj = CallResult<SteamUGCQueryCompleted_t>.Create((APIDispatchDelegate<SteamUGCQueryCompleted_t>)null);
		CSteamID steamID = SteamUser.GetSteamID();
		ugcQueryHandle = SteamUGC.CreateQueryUserUGCRequest(((CSteamID)(ref steamID)).GetAccountID(), (EUserUGCList)6, (EUGCMatchingUGCType)2, (EUserUGCListSortOrder)0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), CurPage);
		SteamUGC.SetRankedByTrendDays(ugcQueryHandle, 90u);
		SteamUGC.SetReturnChildren(ugcQueryHandle, true);
		SteamUGC.SetReturnMetadata(ugcQueryHandle, true);
		SteamUGC.SetReturnKeyValueTags(ugcQueryHandle, true);
		SteamUGC.SetReturnLongDescription(ugcQueryHandle, true);
		SteamAPICall_t val = SteamUGC.SendQueryUGCRequest(ugcQueryHandle);
		IsQuerying = true;
		obj.Set(val, (APIDispatchDelegate<SteamUGCQueryCompleted_t>)delegate(SteamUGCQueryCompleted_t t, bool failure)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			if (!UI.IsActive())
			{
				isFirstQuery = false;
				IsQuerying = false;
			}
			else if ((int)t.m_eResult == 1)
			{
				SteamUGCDetails_t val2 = default(SteamUGCDetails_t);
				for (uint num = 0u; num < t.m_unNumResultsReturned; num++)
				{
					ModInfo modInfo = null;
					SteamUGC.GetQueryUGCResult(ugcQueryHandle, num, ref val2);
					if (!string.IsNullOrEmpty(val2.m_rgchTitle))
					{
						if (WorkShopMag.Inst.ModInfoDict.ContainsKey(val2.m_nPublishedFileId.m_PublishedFileId))
						{
							modInfo = WorkShopMag.Inst.ModInfoDict[val2.m_nPublishedFileId.m_PublishedFileId];
						}
						else
						{
							modInfo = new ModInfo();
							modInfo.Name = val2.m_rgchTitle;
							modInfo.Id = val2.m_nPublishedFileId.m_PublishedFileId;
							modInfo.Desc = val2.m_rgchDescription;
							modInfo.SetTags(val2.m_rgchTags);
							SteamUGC.GetQueryUGCPreviewURL(ugcQueryHandle, num, ref modInfo.ImgUrl, (uint)val2.m_nPreviewFileSize);
							string author = "";
							SteamUGC.GetQueryUGCMetadata(ugcQueryHandle, num, ref author, 5000u);
							modInfo.SetAuthor(author);
							modInfo.UpNum = (int)val2.m_unVotesUp;
							modInfo.DownNum = (int)val2.m_unVotesDown;
							modInfo.Subscriptions = GetItemSubscriptions(ugcQueryHandle, num);
							modInfo.DependencyList = GetDependencies(ugcQueryHandle, num, val2.m_unNumChildren);
							WorkShopMag.Inst.ModInfoDict.Add(val2.m_nPublishedFileId.m_PublishedFileId, modInfo);
						}
						ModUI modUI = WorkShopMag.Inst.ModPoolUI.Ctr.GetModUI();
						modUI.BindingInfo(modInfo);
						list.Add(modUI);
					}
				}
				IsQuerying = false;
				if (isFirstQuery)
				{
					isFirstQuery = false;
				}
			}
			else
			{
				UIPopTip.Inst.Pop("获取MOD列表失败，请重试");
			}
		});
	}

	private ulong GetItemSubscriptions(UGCQueryHandle_t handle, uint index)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		ulong result = 1uL;
		SteamUGC.GetQueryUGCStatistic(handle, index, (EItemStatistic)0, ref result);
		return result;
	}

	private List<ulong> GetDependencies(UGCQueryHandle_t handle, uint index, uint count)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		PublishedFileId_t[] array = (PublishedFileId_t[])(object)new PublishedFileId_t[count];
		SteamUGC.GetQueryUGCChildren(handle, index, array, count);
		List<ulong> list = new List<ulong>();
		PublishedFileId_t[] array2 = array;
		foreach (PublishedFileId_t val in array2)
		{
			list.Add(val.m_PublishedFileId);
		}
		return list;
	}

	public void Clear()
	{
		if (UI.CurSelect != null)
		{
			UI.CurSelect.CancelSelect();
		}
		WorkShopMag.Inst.MoreModInfoUI.Hide();
		UI.CurSelect = null;
		foreach (ModUI item in list)
		{
			WorkShopMag.Inst.ModPoolUI.Ctr.BackMod(item);
		}
		UI.CurSelect = null;
	}
}
