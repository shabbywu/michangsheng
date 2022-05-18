using System;
using CaiYao;
using Fungus;
using JSONClass;
using KBEngine;
using PaiMai;
using QiYu;
using script.NewLianDan;
using script.Submit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000A4A RID: 2634
	public class TabDataBase : UIBase
	{
		// Token: 0x060043F7 RID: 17399 RVA: 0x001D0F20 File Offset: 0x001CF120
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
			this.HasData = false;
			this.Id = PlayerPrefs.GetInt("NowPlayerFileAvatar");
			this.Index = int.Parse(this._go.name);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x001D1018 File Offset: 0x001CF218
		public void UpdateDate()
		{
			this.CheckHasData();
			if (this.HasData)
			{
				JSONObject jsonObject = YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(this.Id, this.Index), null);
				this.JingJie.SetText(LevelUpDataJsonData.DataDict[jsonObject["avatarLevel"].I].Name);
				this.Icon.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", jsonObject["avatarLevel"].I));
				DateTime dateTime = DateTime.Parse(jsonObject["gameTime"].Str);
				this.GameTime.SetText(string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day));
				string @string = YSGame.YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(this.Id, this.Index), "");
				this.RealTime.SetText(@string);
				this.NoDataPanel.SetActive(false);
				this.HasDataPanel.SetActive(true);
				return;
			}
			this.NoDataPanel.SetActive(true);
			this.HasDataPanel.SetActive(false);
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x001D1174 File Offset: 0x001CF374
		private void CheckHasData()
		{
			if (YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(this.Id, this.Index)))
			{
				if (YSGame.YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(this.Id, this.Index), null).IsNull)
				{
					this.HasData = false;
				}
				else
				{
					this.HasData = true;
				}
				if (FactoryManager.inst.SaveLoadFactory.GetInt("GameVersion" + Tools.instance.getSaveID(this.Id, this.Index)) > 4 && !YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "IsComplete" + Tools.instance.getSaveID(this.Id, this.Index)))
				{
					this.HasData = false;
					return;
				}
			}
			else
			{
				this.HasData = false;
			}
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x001D125C File Offset: 0x001CF45C
		private void Save()
		{
			if (this.Index == 0)
			{
				UIPopTip.Inst.Pop("不能覆盖自动存档", PopTipIconType.感悟);
				return;
			}
			if (this.HasData)
			{
				TySelect.inst.Show("是否覆盖当前存档", delegate
				{
					if (SingletonMono<TabUIMag>.Instance != null)
					{
						SingletonMono<TabUIMag>.Instance.TryEscClose();
					}
					Tools.instance.playerSaveGame(this.Id, this.Index, null);
				}, null, true);
				return;
			}
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			Tools.instance.playerSaveGame(this.Id, this.Index, null);
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000309E2 File Offset: 0x0002EBE2
		private void Load()
		{
			if (this.HasData)
			{
				TySelect.inst.Show("是否读取当前存档", delegate
				{
					try
					{
						if (SayDialog.GetSayDialog().gameObject != null)
						{
							Object.Destroy(SayDialog.GetSayDialog().gameObject);
						}
						if (SubmitUIMag.Inst != null)
						{
							SubmitUIMag.Inst.Close();
						}
						if (FpUIMag.inst != null)
						{
							Object.Destroy(FpUIMag.inst.gameObject);
						}
						if (TpUIMag.inst != null)
						{
							Object.Destroy(TpUIMag.inst.gameObject);
						}
						if (QiYuUIMag.Inst != null)
						{
							Object.Destroy(QiYuUIMag.Inst.gameObject);
						}
						if (CaiYaoUIMag.Inst != null)
						{
							Object.Destroy(CaiYaoUIMag.Inst.gameObject);
						}
						if (PanelMamager.inst.UISceneGameObject != null)
						{
							PanelMamager.inst.UISceneGameObject.SetActive(false);
						}
						if (SingletonMono<TabUIMag>.Instance != null)
						{
							SingletonMono<TabUIMag>.Instance.TryEscClose();
						}
						if (LianDanUIMag.Instance != null)
						{
							Object.Destroy(LianDanUIMag.Instance.gameObject);
						}
						if (LianQiTotalManager.inst != null)
						{
							Object.Destroy(LianQiTotalManager.inst.gameObject);
						}
						if (SingletonMono<PaiMaiUiMag>.Instance != null)
						{
							Object.Destroy(SingletonMono<PaiMaiUiMag>.Instance.gameObject);
							Time.timeScale = 1f;
						}
						ESCCloseManager.Inst.CloseAll();
						PlayerPrefs.GetInt("NowPlayerFileAvatar");
						YSGame.YSSaveGame.Reset();
						KBEngineApp.app.entities[10] = null;
						KBEngineApp.app.entities.Remove(10);
						GameObject gameObject = new GameObject();
						gameObject.AddComponent<StartGame>();
						gameObject.GetComponent<StartGame>().startGame(this.Id, this.Index, -1);
					}
					catch (Exception ex)
					{
						Debug.LogError("读档失败");
						Debug.LogError(ex);
						UIPopTip.Inst.Pop("存档可能因云存档已损坏，无法读取", PopTipIconType.叹号);
					}
				}, null, false);
			}
		}

		// Token: 0x04003C0D RID: 15373
		public int Id;

		// Token: 0x04003C0E RID: 15374
		public int Index;

		// Token: 0x04003C0F RID: 15375
		public bool HasData;

		// Token: 0x04003C10 RID: 15376
		public readonly GameObject HasDataPanel;

		// Token: 0x04003C11 RID: 15377
		public readonly GameObject NoDataPanel;

		// Token: 0x04003C12 RID: 15378
		public readonly Image Icon;

		// Token: 0x04003C13 RID: 15379
		public readonly Text JingJie;

		// Token: 0x04003C14 RID: 15380
		public readonly Text GameTime;

		// Token: 0x04003C15 RID: 15381
		public readonly Text RealTime;
	}
}
