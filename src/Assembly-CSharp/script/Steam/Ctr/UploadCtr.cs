using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using script.Steam.UI;
using script.Steam.Utils;
using Steamworks;
using UnityEngine;

namespace script.Steam.Ctr
{
	// Token: 0x020009EA RID: 2538
	public class UploadCtr
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x001DC1A0 File Offset: 0x001DA3A0
		public UploadModUI UI
		{
			get
			{
				return WorkShopMag.Inst.UploadModUI;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x001DC1AC File Offset: 0x001DA3AC
		public UploadModProgress UploadModProgress
		{
			get
			{
				return WorkShopMag.Inst.UploadModUI.UploadModProgress;
			}
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x001DC1BD File Offset: 0x001DA3BD
		public void SelectMod()
		{
			this.WorkShopItem = new WorkShopItem();
			FileUtils.OpenDirectory(delegate(string path)
			{
				if (File.Exists(path + "/" + this.ConfigName))
				{
					this.LoadModConfig(path);
					if (this.WorkShopItem.SteamID != SteamUser.GetSteamID().m_SteamID)
					{
						UIPopTip.Inst.Pop("参数错误", PopTipIconType.叹号);
						return;
					}
					if (this.WorkShopItem.Dependencies == null)
					{
						this.WorkShopItem.Dependencies = new List<ulong>();
						this.WorkShopItem.LastDependencies = new List<ulong>();
					}
				}
				else
				{
					this.WorkShopItem.SteamID = SteamUser.GetSteamID().m_SteamID;
				}
				this.WorkShopItem.ModPath = path;
				this.UI.UpdateUI();
			});
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x001DC1DB File Offset: 0x001DA3DB
		public void SelectImg()
		{
			if (this.WorkShopItem == null)
			{
				UIPopTip.Inst.Pop("请先选择MOD文件夹", PopTipIconType.叹号);
				return;
			}
			FileUtils.OpenFile(delegate(string path)
			{
				if (File.Exists(path))
				{
					this.WorkShopItem.ImgPath = path;
					this.UI.UpdateUI();
					return;
				}
				UIPopTip.Inst.Pop("图片不存在", PopTipIconType.叹号);
			});
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x001DC208 File Offset: 0x001DA408
		public void ClickUpload()
		{
			if (this.WorkShopItem == null)
			{
				UIPopTip.Inst.Pop("请先选择MOD文件夹", PopTipIconType.叹号);
				return;
			}
			if (!Directory.Exists(this.WorkShopItem.ModPath))
			{
				UIPopTip.Inst.Pop("Mod路径不存在", PopTipIconType.叹号);
				return;
			}
			if (!File.Exists(this.WorkShopItem.ImgPath))
			{
				UIPopTip.Inst.Pop("封面图片不存在", PopTipIconType.叹号);
				return;
			}
			this.WorkShopItem.Title = this.UI.ModName.text;
			this.WorkShopItem.Des = this.UI.ModDesc.text;
			if (this.WorkShopItem.Title == "" || this.WorkShopItem.Des == "")
			{
				UIPopTip.Inst.Pop("mod标题和描述不能为空", PopTipIconType.叹号);
				return;
			}
			Debug.Log(this.WorkShopItem.PublishedFileId);
			if (this.WorkShopItem.SteamID != SteamUser.GetSteamID().m_SteamID)
			{
				UIPopTip.Inst.Pop("参数错误", PopTipIconType.叹号);
				return;
			}
			if (this.WorkShopItem.PublishedFileId.m_PublishedFileId > 0UL)
			{
				this.UploadMod();
				return;
			}
			this.CreateMod();
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x001DC348 File Offset: 0x001DA548
		private void CreateMod()
		{
			SteamAPICall_t steamAPICall_t = SteamUGC.CreateItem(SteamUtils.GetAppID(), 0);
			CallResult<CreateItemResult_t>.Create(null).Set(steamAPICall_t, delegate(CreateItemResult_t t, bool failure)
			{
				if (t.m_eResult == 1)
				{
					if (t.m_bUserNeedsToAcceptWorkshopLegalAgreement)
					{
						USelectBox.Show("检测到您未接受《Steam 创意工坊法律协议》,Mod将对其他用户不可见，点击确定将跳转至协议界面", delegate
						{
							SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
						}, null);
					}
					this.WorkShopItem.PublishedFileId = t.m_nPublishedFileId;
					Debug.Log(string.Format("m_nPublishedFileId:{0}", this.WorkShopItem.PublishedFileId));
					this.UploadMod();
					return;
				}
				Debug.LogError("创建Mod失败");
				UIPopTip.Inst.Pop("创建Mod失败", PopTipIconType.叹号);
			});
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x001DC37C File Offset: 0x001DA57C
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

		// Token: 0x06004666 RID: 18022 RVA: 0x001DC3CC File Offset: 0x001DA5CC
		private void UploadMod()
		{
			List<ulong> list = new List<ulong>();
			if (!this.IsEqual(this.WorkShopItem.LastDependencies, this.WorkShopItem.Dependencies))
			{
				foreach (ulong item in this.WorkShopItem.LastDependencies)
				{
					if (!this.WorkShopItem.Dependencies.Contains(item))
					{
						list.Add(item);
					}
				}
				this.WorkShopItem.LastDependencies = new List<ulong>();
				foreach (ulong item2 in this.WorkShopItem.Dependencies)
				{
					this.WorkShopItem.LastDependencies.Add(item2);
				}
			}
			this.SaveModConfig();
			if (!File.Exists(this.WorkShopItem.ModPath + "/" + this.ConfigName))
			{
				UIPopTip.Inst.Pop("参数错误,不存在配置文件", PopTipIconType.叹号);
				return;
			}
			if (this.ReadConfig(this.WorkShopItem.ModPath).SteamID != SteamUser.GetSteamID().m_SteamID)
			{
				UIPopTip.Inst.Pop("参数错误", PopTipIconType.叹号);
				return;
			}
			UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this.WorkShopItem.PublishedFileId);
			SteamUGC.SetItemTitle(ugcupdateHandle_t, this.WorkShopItem.Title);
			SteamUGC.SetItemDescription(ugcupdateHandle_t, this.WorkShopItem.Des);
			SteamUGC.SetItemContent(ugcupdateHandle_t, this.WorkShopItem.ModPath);
			List<string> list2 = new List<string>();
			foreach (string key in this.WorkShopItem.Tags)
			{
				list2.Add(WorkShopMag.TagsDict[key]);
			}
			string personaName = SteamFriends.GetPersonaName();
			SteamUGC.SetItemMetadata(ugcupdateHandle_t, personaName);
			SteamUGC.SetItemTags(ugcupdateHandle_t, list2);
			SteamUGC.AddDependency(this.WorkShopItem.PublishedFileId, new PublishedFileId_t((ulong)-1470617362));
			if (this.WorkShopItem.IsNeedNext)
			{
				SteamUGC.AddDependency(this.WorkShopItem.PublishedFileId, new PublishedFileId_t((ulong)-1470121939));
			}
			else
			{
				SteamUGC.RemoveDependency(this.WorkShopItem.PublishedFileId, new PublishedFileId_t((ulong)-1470121939));
			}
			foreach (ulong num in this.WorkShopItem.Dependencies)
			{
				SteamUGC.AddDependency(this.WorkShopItem.PublishedFileId, new PublishedFileId_t(num));
			}
			foreach (ulong num2 in list)
			{
				SteamUGC.RemoveDependency(this.WorkShopItem.PublishedFileId, new PublishedFileId_t(num2));
			}
			SteamUGC.SetItemPreview(ugcupdateHandle_t, this.WorkShopItem.ImgPath);
			SteamUGC.SetItemVisibility(ugcupdateHandle_t, this.WorkShopItem.Visibility);
			SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, "更新");
			this.UI.UpLoadBtn.gameObject.SetActive(false);
			this.UI.UpLoadingImg.gameObject.SetActive(true);
			WorkShopMag.Inst.ToggleGroup.SetAllCanClick(false);
			this.UploadModProgress.StartProgress(ugcupdateHandle_t, this.UI.Progress, delegate
			{
				UIPopTip.Inst.Pop("上传成功", PopTipIconType.叹号);
				this.UI.UpLoadBtn.gameObject.SetActive(true);
				this.UI.UpLoadingImg.gameObject.SetActive(false);
				WorkShopMag.Inst.ToggleGroup.SetAllCanClick(true);
			});
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x001DC78C File Offset: 0x001DA98C
		public void SaveModConfig()
		{
			FileStream fileStream = new FileStream(this.WorkShopItem.ModPath + "/" + this.ConfigName, FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, this.WorkShopItem);
			fileStream.Close();
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x001DC7D4 File Offset: 0x001DA9D4
		public void LoadModConfig(string path)
		{
			try
			{
				FileStream fileStream = new FileStream(path + "/" + this.ConfigName, FileMode.Open, FileAccess.Read, FileShare.Read);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				this.WorkShopItem = (WorkShopItem)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				Debug.LogError("读取配置文件失败");
				this.WorkShopItem = new WorkShopItem();
			}
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x001DC848 File Offset: 0x001DAA48
		public WorkShopItem ReadConfig(string path)
		{
			WorkShopItem result = new WorkShopItem();
			try
			{
				FileStream fileStream = new FileStream(path + "/" + this.ConfigName, FileMode.Open, FileAccess.Read, FileShare.Read);
				result = (WorkShopItem)new BinaryFormatter().Deserialize(fileStream);
				fileStream.Close();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				Debug.LogError("读取配置文件失败");
			}
			return result;
		}

		// Token: 0x040047E1 RID: 18401
		public WorkShopItem WorkShopItem;

		// Token: 0x040047E2 RID: 18402
		public string ConfigName = "Mod.bin";
	}
}
