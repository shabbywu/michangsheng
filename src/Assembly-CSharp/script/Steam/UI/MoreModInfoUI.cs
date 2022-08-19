using System;
using System.Diagnostics;
using System.IO;
using script.NewLianDan.Base;
using script.Steam.Utils;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI
{
	// Token: 0x020009E4 RID: 2532
	public class MoreModInfoUI : BasePanel
	{
		// Token: 0x06004621 RID: 17953 RVA: 0x001DAA8E File Offset: 0x001D8C8E
		public MoreModInfoUI(GameObject gameObject)
		{
			this._go = gameObject;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x001DAAA0 File Offset: 0x001D8CA0
		private void Init()
		{
			this.ModName = base.Get<Text>("名称/Value");
			this.Up = base.Get<FpBtn>("点赞");
			this.Down = base.Get<FpBtn>("点踩");
			this.OpenFile = base.Get<FpBtn>("打开文件夹");
			this.OpenFile.mouseUpEvent.AddListener(new UnityAction(this.OpenModFile));
			this.Up.mouseUpEvent.AddListener(delegate()
			{
				WorkShopMag.Inst.ModMagUI.Ctr.VoteMod(this.modId, true);
				this.Up.SetCanClick(false);
				this.Down.SetCanClick(true);
			});
			this.Down.mouseUpEvent.AddListener(delegate()
			{
				WorkShopMag.Inst.ModMagUI.Ctr.VoteMod(this.modId, false);
				this.Down.SetCanClick(false);
				this.Up.SetCanClick(true);
			});
			this.ModTag = base.Get<Text>("类型/Value");
			this.Author = base.Get<Text>("作者/Value");
			this.Img = base.Get<Image>("封面/Value");
			this.DependencyPrefab = base.Get<Text>("依赖模组/Scroll View/Viewport/Content/Value");
			this.DependencyParent = this.DependencyPrefab.transform.parent;
			this.Desc = base.Get<Text>("mod描述/Scroll View/Viewport/Value");
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x001DABB0 File Offset: 0x001D8DB0
		private void OpenModFile()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Format("{0}/{1}", WorkshopTool.WorkshopRootPath, this.modId));
			if (!directoryInfo.Exists)
			{
				UIPopTip.Inst.Pop("没有找到该模组文件夹", PopTipIconType.叹号);
				return;
			}
			Process.Start("Explorer.exe", directoryInfo.FullName);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x001DAC08 File Offset: 0x001D8E08
		private void UpdateIsUpOrDown(ulong id)
		{
			this.Up.SetCanClick(false);
			this.Down.SetCanClick(false);
			SteamAPICall_t userItemVote = SteamUGC.GetUserItemVote(new PublishedFileId_t(id));
			CallResult<GetUserItemVoteResult_t>.Create(null).Set(userItemVote, delegate(GetUserItemVoteResult_t t, bool failure)
			{
				if (t.m_eResult == 1)
				{
					if (t.m_bVotedUp)
					{
						this.Up.SetCanClick(false);
					}
					else
					{
						this.Up.SetCanClick(true);
					}
					if (t.m_bVotedDown)
					{
						this.Down.SetCanClick(false);
						return;
					}
					this.Down.SetCanClick(true);
				}
			});
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x001DAC54 File Offset: 0x001D8E54
		public void Show(ModInfo modInfo)
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			this.modId = modInfo.Id;
			this.UpdateIsUpOrDown(modInfo.Id);
			this.ClearDependency();
			this.ModName.SetTextWithEllipsis(modInfo.Name);
			this.ModTag.SetTextWithEllipsis(modInfo.Tags);
			this.Author.SetTextWithEllipsis(modInfo.Author);
			modInfo.ShowImg(delegate(Sprite sprite)
			{
				this.Img.sprite = sprite;
			});
			if (WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(modInfo.Id))
			{
				this.OpenFile.gameObject.SetActive(true);
			}
			else
			{
				this.OpenFile.gameObject.SetActive(false);
			}
			foreach (ulong num in modInfo.DependencyList)
			{
				GameObject gameObject = this.DependencyPrefab.gameObject.Inst(this.DependencyParent).gameObject;
				gameObject.SetActive(true);
				Text component = gameObject.GetComponent<Text>();
				string value;
				if (WorkShopMag.Inst.ModInfoDict.ContainsKey(num))
				{
					value = WorkShopMag.Inst.ModInfoDict[num].Name;
				}
				else
				{
					value = num.ToString();
				}
				if (WorkShopMag.Inst.ModMagUI.Ctr.IsSubscribe(num))
				{
					component.color = new Color32(60, 115, 111, byte.MaxValue);
				}
				else
				{
					component.color = new Color32(167, 98, 48, byte.MaxValue);
				}
				component.SetTextWithEllipsis(value);
			}
			this.Desc.SetText(modInfo.Desc);
			this._go.SetActive(true);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x001DAE30 File Offset: 0x001D9030
		private void ClearDependency()
		{
			Tools.ClearChild(this.DependencyParent);
		}

		// Token: 0x040047AD RID: 18349
		private bool isInit;

		// Token: 0x040047AE RID: 18350
		private ulong modId;

		// Token: 0x040047AF RID: 18351
		public Text ModName;

		// Token: 0x040047B0 RID: 18352
		public Text ModTag;

		// Token: 0x040047B1 RID: 18353
		public Text Author;

		// Token: 0x040047B2 RID: 18354
		public Image Img;

		// Token: 0x040047B3 RID: 18355
		public Text DependencyPrefab;

		// Token: 0x040047B4 RID: 18356
		public Transform DependencyParent;

		// Token: 0x040047B5 RID: 18357
		public Text Desc;

		// Token: 0x040047B6 RID: 18358
		public FpBtn Up;

		// Token: 0x040047B7 RID: 18359
		public FpBtn Down;

		// Token: 0x040047B8 RID: 18360
		public FpBtn OpenFile;
	}
}
