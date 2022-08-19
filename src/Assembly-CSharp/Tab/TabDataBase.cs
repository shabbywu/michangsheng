using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006FF RID: 1791
	public class TabDataBase : UIBase
	{
		// Token: 0x0600397A RID: 14714 RVA: 0x001897F0 File Offset: 0x001879F0
		public TabDataBase(GameObject go, int type = 0)
		{
			this._go = go;
			this.HasDataPanel = base.Get("HasData", true);
			this.NoDataPanel = base.Get("NoData", true);
			this.Icon = base.Get<Image>("HasData/境界/Img");
			this.JingJie = base.Get<Text>("HasData/境界/Value");
			this.GameTime = base.Get<Text>("HasData/游戏时间/Value");
			this.RealTime = base.Get<Text>("HasData/现实时间/Value");
			if (type == 0)
			{
				this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.Save));
			}
			else
			{
				this._go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(this.Load));
			}
			this.Id = PlayerPrefs.GetInt("NowPlayerFileAvatar");
			this.Index = int.Parse(this._go.name);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x001898E0 File Offset: 0x00187AE0
		public void UpdateDate()
		{
			this.data = YSNewSaveSystem.GetAvatarSaveData(this.Id, this.Index);
			if (this.data.HasSave)
			{
				this.JingJie.SetText(this.data.AvatarLevelText);
				this.Icon.sprite = this.data.AvatarLevelSprite;
				this.GameTime.SetText(this.data.GameTime);
				this.RealTime.SetText(this.data.RealSaveTime);
				if (!this.data.IsNewSaveSystem)
				{
					this.RealTime.text = this.RealTime.text + " <color=red><size=30>旧</size></color>";
				}
				this.NoDataPanel.SetActive(false);
				this.HasDataPanel.SetActive(true);
				return;
			}
			this.NoDataPanel.SetActive(true);
			this.HasDataPanel.SetActive(false);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x001899CC File Offset: 0x00187BCC
		private void Save()
		{
			if (this.Index == 0)
			{
				UIPopTip.Inst.Pop("不能覆盖自动存档", PopTipIconType.感悟);
				return;
			}
			if (this.data.HasSave)
			{
				TySelect.inst.Show("是否覆盖当前存档", delegate
				{
					if (SingletonMono<TabUIMag>.Instance != null)
					{
						SingletonMono<TabUIMag>.Instance.TryEscClose();
					}
					YSNewSaveSystem.SaveGame(this.Id, this.Index, null, false);
				}, null, true);
				return;
			}
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			YSNewSaveSystem.SaveGame(this.Id, this.Index, null, false);
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00189A48 File Offset: 0x00187C48
		private void Load()
		{
			if (this.data.HasSave)
			{
				TySelect.inst.Show("是否读取当前存档", delegate
				{
					try
					{
						if (this.data.IsNewSaveSystem)
						{
							YSNewSaveSystem.LoadSave(this.Id, this.Index, -1);
						}
						else
						{
							YSNewSaveSystem.LoadOldSave(this.Id, this.Index);
						}
					}
					catch (Exception ex)
					{
						Debug.LogError("读档失败");
						Debug.LogError(ex);
						UCheckBox.Show("存档读取失败，可能已损坏。如果订阅了模组，请检查是否有模组错误。", null);
					}
				}, null, false);
			}
		}

		// Token: 0x04003195 RID: 12693
		public int Id;

		// Token: 0x04003196 RID: 12694
		public int Index;

		// Token: 0x04003197 RID: 12695
		public readonly GameObject HasDataPanel;

		// Token: 0x04003198 RID: 12696
		public readonly GameObject NoDataPanel;

		// Token: 0x04003199 RID: 12697
		public readonly Image Icon;

		// Token: 0x0400319A RID: 12698
		public readonly Text JingJie;

		// Token: 0x0400319B RID: 12699
		public readonly Text GameTime;

		// Token: 0x0400319C RID: 12700
		public readonly Text RealTime;

		// Token: 0x0400319D RID: 12701
		public SaveSlotData data;
	}
}
