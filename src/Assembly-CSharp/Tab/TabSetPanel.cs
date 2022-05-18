using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000A4D RID: 2637
	public class TabSetPanel : ISysPanelBase
	{
		// Token: 0x06004406 RID: 17414 RVA: 0x00030ACE File Offset: 0x0002ECCE
		public TabSetPanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00030AE4 File Offset: 0x0002ECE4
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this.UpdateUI();
			this._go.SetActive(true);
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x001D1608 File Offset: 0x001CF808
		protected virtual void Init()
		{
			this.InitDropdown();
			this.YinXiaoSlider = base.Get<Slider>("音效/Slider");
			this.BgSlider = base.Get<Slider>("音乐/Slider");
			base.Get<FpBtn>("一键静音").mouseUpEvent.AddListener(new UnityAction(this.StopAllMusice));
			base.Get<FpBtn>("应用").mouseUpEvent.AddListener(new UnityAction(this.SaveConfig));
			base.Get<FpBtn>("结算提示").mouseEnterEvent.AddListener(delegate()
			{
				UToolTip.Show("影响长期闭关、感悟或者突破功法（超过一年）时NPC结算的拟真程度。结算频率越高，花费的现实时间越长。", 600f, 200f);
			});
			base.Get<FpBtn>("结算提示").mouseOutEvent.AddListener(new UnityAction(UToolTip.Close));
			base.Get<FpBtn>("结算提示").mouseUpEvent.AddListener(new UnityAction(UToolTip.Close));
			this.YinXiaoSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateMusicEffect));
			this.BgSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateMusicBg));
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x001D1730 File Offset: 0x001CF930
		private void InitDropdown()
		{
			this.InitResolutionsDict();
			this.CurResolutionTextStr = string.Format("{0}x{1}", Screen.width, Screen.height);
			this.ResolutionDropdown = base.Get<Dropdown>("分辨率/Dropdown");
			this.ResolutionsList = new List<string>();
			foreach (int key in this.ResolutionsDict.Keys)
			{
				this.ResolutionsList.Add(string.Format("{0}x{1}", this.ResolutionsDict[key].X, this.ResolutionsDict[key].Y));
			}
			this.ResolutionDropdown.AddOptions(this.ResolutionsList);
			this.InitFullScreenDict();
			this.FullScreenDropdown = base.Get<Dropdown>("显示模式/Dropdown");
			this.FullScreenDropdown.onValueChanged.AddListener(delegate(int value)
			{
				if (value == 1)
				{
					this.ResolutionDropdown.interactable = false;
					this.ResolutionDropdown.value = this.ResolutionDropdown.options.Count - 1;
					return;
				}
				this.ResolutionDropdown.interactable = true;
			});
			this.InitSaveTimesDict();
			this.SaveTimesDropdown = base.Get<Dropdown>("存档间隔/Dropdown");
			this.InitNpcActionTimesDict();
			this.NpcActionTimesDropdown = base.Get<Dropdown>("结算频率/Dropdown");
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x001D187C File Offset: 0x001CFA7C
		private void InitResolutionsDict()
		{
			List<int> list = new List<int>();
			this.ResolutionsDict = new Dictionary<int, MResolution>();
			int num = 0;
			foreach (Resolution resolution in Screen.resolutions)
			{
				if (!list.Contains(resolution.width) && Mathf.Abs((float)resolution.height / (float)resolution.width - 0.5625f) <= 0.001f)
				{
					MResolution mresolution = new MResolution();
					mresolution.X = resolution.width;
					mresolution.Y = resolution.height;
					this.ResolutionsDict.Add(num, mresolution);
					num++;
					list.Add(mresolution.X);
				}
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x00030B0D File Offset: 0x0002ED0D
		private void InitFullScreenDict()
		{
			this.FullScreenDict = new Dictionary<int, bool>();
			this.FullScreenDict.Add(0, false);
			this.FullScreenDict.Add(1, true);
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x00030B34 File Offset: 0x0002ED34
		private void InitSaveTimesDict()
		{
			this.SaveTimesDict = new Dictionary<int, int>();
			this.SaveTimesDict.Add(0, 5);
			this.SaveTimesDict.Add(1, 10);
			this.SaveTimesDict.Add(2, -1);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x00030B69 File Offset: 0x0002ED69
		private void InitNpcActionTimesDict()
		{
			this.NpcActionTimesDict = new Dictionary<int, int>();
			this.NpcActionTimesDict.Add(0, 0);
			this.NpcActionTimesDict.Add(1, 1);
			this.NpcActionTimesDict.Add(2, 2);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x001D1938 File Offset: 0x001CFB38
		public void UpdateUI()
		{
			this.YinXiaoSlider.value = SystemConfig.Inst.GetEffectVolume();
			this.BgSlider.value = SystemConfig.Inst.GetBackGroundVolume();
			string item = string.Format("{0}x{1}", Screen.width, Screen.height);
			if (this.ResolutionsList.Contains(item))
			{
				this.ResolutionDropdown.value = this.ResolutionsList.IndexOf(item);
			}
			else
			{
				this.ResolutionDropdown.value = 0;
			}
			if (Screen.fullScreen)
			{
				this.FullScreenDropdown.value = 1;
			}
			else
			{
				this.FullScreenDropdown.value = 0;
			}
			int saveTimes = SystemConfig.Inst.GetSaveTimes();
			foreach (int num in this.SaveTimesDict.Keys)
			{
				if (this.SaveTimesDict[num] == saveTimes)
				{
					this.SaveTimesDropdown.value = num;
					break;
				}
			}
			this.NpcActionTimesDropdown.value = SystemConfig.Inst.GetNpcActionTimes();
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x00030B9D File Offset: 0x0002ED9D
		public void UpdateMusicEffect(float value)
		{
			MusicMag.instance.setEffectVolum(value);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00030BAA File Offset: 0x0002EDAA
		public void UpdateMusicBg(float value)
		{
			MusicMag.instance.setBackGroundVolume(value);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x00030BB7 File Offset: 0x0002EDB7
		public void StopAllMusice()
		{
			this.YinXiaoSlider.value = 0f;
			this.BgSlider.value = 0f;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001D1A64 File Offset: 0x001CFC64
		public void SaveConfig()
		{
			PlayerPrefs.SetFloat("MusicBg", MusicMag.instance.audioSource.volume);
			PlayerPrefs.SetFloat("MusicEffect", MusicMag.instance.audioSourceEffect.volume);
			Screen.SetResolution(this.ResolutionsDict[this.ResolutionDropdown.value].X, this.ResolutionsDict[this.ResolutionDropdown.value].Y, this.FullScreenDict[this.FullScreenDropdown.value]);
			SystemConfig.Inst.SetSaveTimes(this.SaveTimesDict[this.SaveTimesDropdown.value]);
			SystemConfig.Inst.SetActionTimes(this.NpcActionTimesDict[this.NpcActionTimesDropdown.value]);
		}

		// Token: 0x04003C1A RID: 15386
		private bool _isInit;

		// Token: 0x04003C1B RID: 15387
		public Slider YinXiaoSlider;

		// Token: 0x04003C1C RID: 15388
		public Slider BgSlider;

		// Token: 0x04003C1D RID: 15389
		public string CurResolutionTextStr;

		// Token: 0x04003C1E RID: 15390
		public List<string> ResolutionsList;

		// Token: 0x04003C1F RID: 15391
		public Dictionary<int, MResolution> ResolutionsDict;

		// Token: 0x04003C20 RID: 15392
		public Dropdown ResolutionDropdown;

		// Token: 0x04003C21 RID: 15393
		public Dictionary<int, bool> FullScreenDict;

		// Token: 0x04003C22 RID: 15394
		public Dropdown FullScreenDropdown;

		// Token: 0x04003C23 RID: 15395
		public Dictionary<int, int> SaveTimesDict;

		// Token: 0x04003C24 RID: 15396
		public Dropdown SaveTimesDropdown;

		// Token: 0x04003C25 RID: 15397
		public Dictionary<int, int> NpcActionTimesDict;

		// Token: 0x04003C26 RID: 15398
		public Dropdown NpcActionTimesDropdown;
	}
}
