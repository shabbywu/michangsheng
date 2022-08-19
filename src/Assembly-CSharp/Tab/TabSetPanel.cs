using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab
{
	// Token: 0x02000702 RID: 1794
	public class TabSetPanel : ISysPanelBase
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x00189CEC File Offset: 0x00187EEC
		public TabSetPanel(GameObject go)
		{
			this._go = go;
			this._isInit = false;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x00189D02 File Offset: 0x00187F02
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

		// Token: 0x0600398A RID: 14730 RVA: 0x00189D2C File Offset: 0x00187F2C
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

		// Token: 0x0600398B RID: 14731 RVA: 0x00189E54 File Offset: 0x00188054
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

		// Token: 0x0600398C RID: 14732 RVA: 0x00189FA0 File Offset: 0x001881A0
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

		// Token: 0x0600398D RID: 14733 RVA: 0x0018A059 File Offset: 0x00188259
		private void InitFullScreenDict()
		{
			this.FullScreenDict = new Dictionary<int, bool>();
			this.FullScreenDict.Add(0, false);
			this.FullScreenDict.Add(1, true);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0018A080 File Offset: 0x00188280
		private void InitSaveTimesDict()
		{
			this.SaveTimesDict = new Dictionary<int, int>();
			this.SaveTimesDict.Add(0, 0);
			this.SaveTimesDict.Add(1, 5);
			this.SaveTimesDict.Add(2, 10);
			this.SaveTimesDict.Add(3, -1);
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x0018A0CD File Offset: 0x001882CD
		private void InitNpcActionTimesDict()
		{
			this.NpcActionTimesDict = new Dictionary<int, int>();
			this.NpcActionTimesDict.Add(0, 0);
			this.NpcActionTimesDict.Add(1, 1);
			this.NpcActionTimesDict.Add(2, 2);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x0018A104 File Offset: 0x00188304
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

		// Token: 0x06003991 RID: 14737 RVA: 0x0018A230 File Offset: 0x00188430
		public void UpdateMusicEffect(float value)
		{
			MusicMag.instance.setEffectVolum(value);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0018A23D File Offset: 0x0018843D
		public void UpdateMusicBg(float value)
		{
			MusicMag.instance.setBackGroundVolume(value);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x0018A24A File Offset: 0x0018844A
		public void StopAllMusice()
		{
			this.YinXiaoSlider.value = 0f;
			this.BgSlider.value = 0f;
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0018A26C File Offset: 0x0018846C
		public void SaveConfig()
		{
			PlayerPrefs.SetFloat("MusicBg", MusicMag.instance.audioSource.volume);
			PlayerPrefs.SetFloat("MusicEffect", MusicMag.instance.audioSourceEffect.volume);
			Screen.SetResolution(this.ResolutionsDict[this.ResolutionDropdown.value].X, this.ResolutionsDict[this.ResolutionDropdown.value].Y, this.FullScreenDict[this.FullScreenDropdown.value]);
			SystemConfig.Inst.SetSaveTimes(this.SaveTimesDict[this.SaveTimesDropdown.value]);
			SystemConfig.Inst.SetActionTimes(this.NpcActionTimesDict[this.NpcActionTimesDropdown.value]);
		}

		// Token: 0x040031A2 RID: 12706
		private bool _isInit;

		// Token: 0x040031A3 RID: 12707
		public Slider YinXiaoSlider;

		// Token: 0x040031A4 RID: 12708
		public Slider BgSlider;

		// Token: 0x040031A5 RID: 12709
		public string CurResolutionTextStr;

		// Token: 0x040031A6 RID: 12710
		public List<string> ResolutionsList;

		// Token: 0x040031A7 RID: 12711
		public Dictionary<int, MResolution> ResolutionsDict;

		// Token: 0x040031A8 RID: 12712
		public Dropdown ResolutionDropdown;

		// Token: 0x040031A9 RID: 12713
		public Dictionary<int, bool> FullScreenDict;

		// Token: 0x040031AA RID: 12714
		public Dropdown FullScreenDropdown;

		// Token: 0x040031AB RID: 12715
		public Dictionary<int, int> SaveTimesDict;

		// Token: 0x040031AC RID: 12716
		public Dropdown SaveTimesDropdown;

		// Token: 0x040031AD RID: 12717
		public Dictionary<int, int> NpcActionTimesDict;

		// Token: 0x040031AE RID: 12718
		public Dropdown NpcActionTimesDropdown;
	}
}
