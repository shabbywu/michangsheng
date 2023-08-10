using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using script.Steam.UI;
using script.Steam.Utils;

namespace script.Steam.Ctr;

public class UploadCtr
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__9_1;

		internal void _003CCreateMod_003Eb__9_1()
		{
			SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
		}
	}

	public WorkShopItem WorkShopItem;

	public string ConfigName = "Mod.bin";

	public UploadModUI UI => WorkShopMag.Inst.UploadModUI;

	public UploadModProgress UploadModProgress => WorkShopMag.Inst.UploadModUI.UploadModProgress;

	public void SelectMod()
	{
		WorkShopItem = new WorkShopItem();
		FileUtils.OpenDirectory(delegate(string path)
		{
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			if (File.Exists(path + "/" + ConfigName))
			{
				LoadModConfig(path);
				if (WorkShopItem.SteamID != SteamUser.GetSteamID().m_SteamID)
				{
					UIPopTip.Inst.Pop("参数错误");
					return;
				}
				if (WorkShopItem.Dependencies == null)
				{
					WorkShopItem.Dependencies = new List<ulong>();
					WorkShopItem.LastDependencies = new List<ulong>();
				}
			}
			else
			{
				WorkShopItem.SteamID = SteamUser.GetSteamID().m_SteamID;
			}
			WorkShopItem.ModPath = path;
			UI.UpdateUI();
		});
	}

	public void SelectImg()
	{
		if (WorkShopItem == null)
		{
			UIPopTip.Inst.Pop("请先选择MOD文件夹");
			return;
		}
		FileUtils.OpenFile(delegate(string path)
		{
			if (File.Exists(path))
			{
				WorkShopItem.ImgPath = path;
				UI.UpdateUI();
			}
			else
			{
				UIPopTip.Inst.Pop("图片不存在");
			}
		});
	}

	public void ClickUpload()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (WorkShopItem == null)
		{
			UIPopTip.Inst.Pop("请先选择MOD文件夹");
			return;
		}
		if (!Directory.Exists(WorkShopItem.ModPath))
		{
			UIPopTip.Inst.Pop("Mod路径不存在");
			return;
		}
		if (!File.Exists(WorkShopItem.ImgPath))
		{
			UIPopTip.Inst.Pop("封面图片不存在");
			return;
		}
		WorkShopItem.Title = UI.ModName.text;
		WorkShopItem.Des = UI.ModDesc.text;
		if (WorkShopItem.Title == "" || WorkShopItem.Des == "")
		{
			UIPopTip.Inst.Pop("mod标题和描述不能为空");
			return;
		}
		Debug.Log((object)WorkShopItem.PublishedFileId);
		if (WorkShopItem.SteamID != SteamUser.GetSteamID().m_SteamID)
		{
			UIPopTip.Inst.Pop("参数错误");
		}
		else if (WorkShopItem.PublishedFileId.m_PublishedFileId != 0)
		{
			UploadMod();
		}
		else
		{
			CreateMod();
		}
	}

	private void CreateMod()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		SteamAPICall_t val = SteamUGC.CreateItem(SteamUtils.GetAppID(), (EWorkshopFileType)0);
		CallResult<CreateItemResult_t>.Create((APIDispatchDelegate<CreateItemResult_t>)null).Set(val, (APIDispatchDelegate<CreateItemResult_t>)delegate(CreateItemResult_t t, bool failure)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			if ((int)t.m_eResult == 1)
			{
				if (t.m_bUserNeedsToAcceptWorkshopLegalAgreement)
				{
					object obj = _003C_003Ec._003C_003E9__9_1;
					if (obj == null)
					{
						UnityAction val2 = delegate
						{
							SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
						};
						_003C_003Ec._003C_003E9__9_1 = val2;
						obj = (object)val2;
					}
					USelectBox.Show("检测到您未接受《Steam 创意工坊法律协议》,Mod将对其他用户不可见，点击确定将跳转至协议界面", (UnityAction)obj);
				}
				WorkShopItem.PublishedFileId = t.m_nPublishedFileId;
				Debug.Log((object)$"m_nPublishedFileId:{WorkShopItem.PublishedFileId}");
				UploadMod();
			}
			else
			{
				Debug.LogError((object)"创建Mod失败");
				UIPopTip.Inst.Pop("创建Mod失败");
			}
		});
	}

	public bool IsEqual(List<ulong> args, List<ulong> args2)
	{
		args.Sort();
		args2.Sort();
		if (args.Count != args2.Count)
		{
			return false;
		}
		for (int i = 0; i < args.Count; i++)
		{
			if (args[i] != args2[i])
			{
				return false;
			}
		}
		return true;
	}

	private void UploadMod()
	{
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Expected O, but got Unknown
		List<ulong> list = new List<ulong>();
		if (!IsEqual(WorkShopItem.LastDependencies, WorkShopItem.Dependencies))
		{
			foreach (ulong lastDependency in WorkShopItem.LastDependencies)
			{
				if (!WorkShopItem.Dependencies.Contains(lastDependency))
				{
					list.Add(lastDependency);
				}
			}
			WorkShopItem.LastDependencies = new List<ulong>();
			foreach (ulong dependency in WorkShopItem.Dependencies)
			{
				WorkShopItem.LastDependencies.Add(dependency);
			}
		}
		SaveModConfig();
		if (!File.Exists(WorkShopItem.ModPath + "/" + ConfigName))
		{
			UIPopTip.Inst.Pop("参数错误,不存在配置文件");
			return;
		}
		if (ReadConfig(WorkShopItem.ModPath).SteamID != SteamUser.GetSteamID().m_SteamID)
		{
			UIPopTip.Inst.Pop("参数错误");
			return;
		}
		UGCUpdateHandle_t val = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), WorkShopItem.PublishedFileId);
		SteamUGC.SetItemTitle(val, WorkShopItem.Title);
		SteamUGC.SetItemDescription(val, WorkShopItem.Des);
		SteamUGC.SetItemContent(val, WorkShopItem.ModPath);
		List<string> list2 = new List<string>();
		foreach (string tag in WorkShopItem.Tags)
		{
			list2.Add(WorkShopMag.TagsDict[tag]);
		}
		string personaName = SteamFriends.GetPersonaName();
		SteamUGC.SetItemMetadata(val, personaName);
		SteamUGC.SetItemTags(val, (IList<string>)list2);
		SteamUGC.AddDependency(WorkShopItem.PublishedFileId, new PublishedFileId_t(2824349934uL));
		if (WorkShopItem.IsNeedNext)
		{
			SteamUGC.AddDependency(WorkShopItem.PublishedFileId, new PublishedFileId_t(2824845357uL));
		}
		else
		{
			SteamUGC.RemoveDependency(WorkShopItem.PublishedFileId, new PublishedFileId_t(2824845357uL));
		}
		foreach (ulong dependency2 in WorkShopItem.Dependencies)
		{
			SteamUGC.AddDependency(WorkShopItem.PublishedFileId, new PublishedFileId_t(dependency2));
		}
		foreach (ulong item in list)
		{
			SteamUGC.RemoveDependency(WorkShopItem.PublishedFileId, new PublishedFileId_t(item));
		}
		SteamUGC.SetItemPreview(val, WorkShopItem.ImgPath);
		SteamUGC.SetItemVisibility(val, (ERemoteStoragePublishedFileVisibility)WorkShopItem.Visibility);
		SteamUGC.SubmitItemUpdate(val, "更新");
		((Component)UI.UpLoadBtn).gameObject.SetActive(false);
		((Component)UI.UpLoadingImg).gameObject.SetActive(true);
		WorkShopMag.Inst.ToggleGroup.SetAllCanClick(flag: false);
		UploadModProgress.StartProgress(val, UI.Progress, (UnityAction)delegate
		{
			UIPopTip.Inst.Pop("上传成功");
			((Component)UI.UpLoadBtn).gameObject.SetActive(true);
			((Component)UI.UpLoadingImg).gameObject.SetActive(false);
			WorkShopMag.Inst.ToggleGroup.SetAllCanClick(flag: true);
		});
	}

	public void SaveModConfig()
	{
		FileStream fileStream = new FileStream(WorkShopItem.ModPath + "/" + ConfigName, FileMode.Create);
		new BinaryFormatter().Serialize(fileStream, WorkShopItem);
		fileStream.Close();
	}

	public void LoadModConfig(string path)
	{
		try
		{
			FileStream fileStream = new FileStream(path + "/" + ConfigName, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			WorkShopItem = (WorkShopItem)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)"读取配置文件失败");
			WorkShopItem = new WorkShopItem();
		}
	}

	public WorkShopItem ReadConfig(string path)
	{
		WorkShopItem result = new WorkShopItem();
		try
		{
			FileStream fileStream = new FileStream(path + "/" + ConfigName, FileMode.Open, FileAccess.Read, FileShare.Read);
			result = (WorkShopItem)new BinaryFormatter().Deserialize(fileStream);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)"读取配置文件失败");
		}
		return result;
	}
}
