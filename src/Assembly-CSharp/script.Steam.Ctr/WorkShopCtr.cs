using System.Collections.Generic;
using Steamworks;
using script.Steam.UI;
using script.Steam.UI.Base;

namespace script.Steam.Ctr;

public class WorkShopCtr
{
	private EUGCQuery queryType = (EUGCQuery)12;

	private List<string> tags = new List<string>();

	private List<ModUI> list = new List<ModUI>();

	private bool isFirstQuery = true;

	private bool isQuerying;

	public uint CurPage = 1u;

	public uint MaxPage = 1u;

	private UGCQueryHandle_t ugcQueryHandle;

	public WorkShopUI UI => WorkShopMag.Inst.WorkShopUI;

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

	public void AddPage()
	{
		if (CurPage + 1 <= MaxPage)
		{
			CurPage++;
			UpdateList();
		}
	}

	public void ReducePage()
	{
		if (CurPage - 1 >= 1)
		{
			CurPage--;
			UpdateList();
		}
	}

	public void SetQueryType(int value)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		switch (value)
		{
		case 0:
			queryType = (EUGCQuery)12;
			break;
		case 1:
			queryType = (EUGCQuery)1;
			break;
		case 2:
			queryType = (EUGCQuery)0;
			break;
		}
		UpdateList();
	}

	public void AddTag(string tag)
	{
		if (!tags.Contains(tag))
		{
			tags.Add(tag);
			UpdateList();
		}
	}

	public void RemoveTag(string tag)
	{
		if (tags.Contains(tag))
		{
			tags.Remove(tag);
			UpdateList();
		}
	}

	public void UpdateList(bool isWithOutIsQuerying = false)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
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
		Clear();
		list = new List<ModUI>();
		CallResult<SteamUGCQueryCompleted_t> val = CallResult<SteamUGCQueryCompleted_t>.Create((APIDispatchDelegate<SteamUGCQueryCompleted_t>)null);
		ugcQueryHandle = SteamUGC.CreateQueryAllUGCRequest(queryType, (EUGCMatchingUGCType)2, SteamUtils.GetAppID(), SteamUtils.GetAppID(), CurPage);
		foreach (string tag in tags)
		{
			SteamUGC.AddRequiredTag(ugcQueryHandle, tag);
		}
		SteamUGC.SetRankedByTrendDays(ugcQueryHandle, 90u);
		SteamUGC.SetAllowCachedResponse(ugcQueryHandle, 300u);
		SteamUGC.SetReturnChildren(ugcQueryHandle, true);
		SteamUGC.SetReturnMetadata(ugcQueryHandle, true);
		SteamUGC.SetReturnKeyValueTags(ugcQueryHandle, true);
		SteamUGC.SetReturnLongDescription(ugcQueryHandle, true);
		SteamAPICall_t val2 = SteamUGC.SendQueryUGCRequest(ugcQueryHandle);
		IsQuerying = true;
		val.Set(val2, (APIDispatchDelegate<SteamUGCQueryCompleted_t>)delegate(SteamUGCQueryCompleted_t t, bool failure)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			if (!UI.IsActive())
			{
				isFirstQuery = false;
				IsQuerying = false;
			}
			else if ((int)t.m_eResult == 1)
			{
				SetMaxPage(t.m_unTotalMatchingResults);
				SteamUGCDetails_t val3 = default(SteamUGCDetails_t);
				for (uint num = 0u; num < t.m_unNumResultsReturned; num++)
				{
					ModInfo modInfo = null;
					SteamUGC.GetQueryUGCResult(ugcQueryHandle, num, ref val3);
					if (!string.IsNullOrEmpty(val3.m_rgchTitle))
					{
						if (WorkShopMag.Inst.ModInfoDict.ContainsKey(val3.m_nPublishedFileId.m_PublishedFileId))
						{
							modInfo = WorkShopMag.Inst.ModInfoDict[val3.m_nPublishedFileId.m_PublishedFileId];
						}
						else
						{
							modInfo = new ModInfo();
							modInfo.Name = val3.m_rgchTitle;
							modInfo.Id = val3.m_nPublishedFileId.m_PublishedFileId;
							modInfo.Desc = val3.m_rgchDescription;
							modInfo.SetTags(val3.m_rgchTags);
							SteamUGC.GetQueryUGCPreviewURL(ugcQueryHandle, num, ref modInfo.ImgUrl, (uint)val3.m_nPreviewFileSize);
							string author = "";
							SteamUGC.GetQueryUGCMetadata(ugcQueryHandle, num, ref author, 5000u);
							modInfo.SetAuthor(author);
							modInfo.UpNum = (int)val3.m_unVotesUp;
							modInfo.DownNum = (int)val3.m_unVotesDown;
							modInfo.Subscriptions = GetItemSubscriptions(ugcQueryHandle, num);
							modInfo.DependencyList = GetDependencies(ugcQueryHandle, num, val3.m_unNumChildren);
							WorkShopMag.Inst.ModInfoDict.Add(val3.m_nPublishedFileId.m_PublishedFileId, modInfo);
						}
						ModUI modUI = WorkShopMag.Inst.ModPoolUI.Ctr.GetModUI(1);
						modUI.BindingInfo(modInfo);
						list.Add(modUI);
					}
				}
				IsQuerying = false;
				UI.CurPage.SetText($"{CurPage}/{MaxPage}");
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

	public void SetMaxPage(uint num)
	{
		MaxPage = num / 50;
		if (num % 50 != 0)
		{
			MaxPage++;
		}
	}

	public void SubscriptionMod(ulong id, bool isShow = true)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		SteamUGC.SubscribeItem(new PublishedFileId_t(id));
		WorkShopMag.Inst.ModMagUI.Ctr.AddSubscribeMod(id);
		if (isShow)
		{
			UIPopTip.Inst.Pop("订阅成功");
		}
	}

	public void UnSubscriptionMod(ulong id)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		SteamUGC.UnsubscribeItem(new PublishedFileId_t(id));
		WorkShopMag.Inst.ModMagUI.Ctr.UnSubscribeMod(id);
		UIPopTip.Inst.Pop("取消订阅成功");
	}

	public List<ulong> GetNoSubscriptDependency(ModInfo info)
	{
		List<ulong> list = new List<ulong>();
		foreach (ulong dependency in info.DependencyList)
		{
			if (!WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(dependency))
			{
				list.Add(dependency);
			}
		}
		return list;
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
