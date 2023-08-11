using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

namespace Tab;

public class TabSetPanel : ISysPanelBase
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__15_0;

		internal void _003CInit_003Eb__15_0()
		{
			UToolTip.Show("影响长期闭关、感悟或者突破功法（超过一年）时NPC结算的拟真程度。结算频率越高，花费的现实时间越长。");
		}
	}

	private bool _isInit;

	public Slider YinXiaoSlider;

	public Slider BgSlider;

	public string CurResolutionTextStr;

	public List<string> ResolutionsList;

	public Dictionary<int, MResolution> ResolutionsDict;

	public Dropdown ResolutionDropdown;

	public Dictionary<int, bool> FullScreenDict;

	public Dropdown FullScreenDropdown;

	public Dictionary<int, int> SaveTimesDict;

	public Dropdown SaveTimesDropdown;

	public Dictionary<int, int> NpcActionTimesDict;

	public Dropdown NpcActionTimesDropdown;

	public TabSetPanel(GameObject go)
	{
		_go = go;
		_isInit = false;
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		UpdateUI();
		_go.SetActive(true);
	}

	protected virtual void Init()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		InitDropdown();
		YinXiaoSlider = Get<Slider>("音效/Slider");
		BgSlider = Get<Slider>("音乐/Slider");
		Get<FpBtn>("一键静音").mouseUpEvent.AddListener(new UnityAction(StopAllMusice));
		Get<FpBtn>("应用").mouseUpEvent.AddListener(new UnityAction(SaveConfig));
		UnityEvent mouseEnterEvent = Get<FpBtn>("结算提示").mouseEnterEvent;
		object obj = _003C_003Ec._003C_003E9__15_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				UToolTip.Show("影响长期闭关、感悟或者突破功法（超过一年）时NPC结算的拟真程度。结算频率越高，花费的现实时间越长。");
			};
			_003C_003Ec._003C_003E9__15_0 = val;
			obj = (object)val;
		}
		mouseEnterEvent.AddListener((UnityAction)obj);
		Get<FpBtn>("结算提示").mouseOutEvent.AddListener(new UnityAction(UToolTip.Close));
		Get<FpBtn>("结算提示").mouseUpEvent.AddListener(new UnityAction(UToolTip.Close));
		((UnityEvent<float>)(object)YinXiaoSlider.onValueChanged).AddListener((UnityAction<float>)UpdateMusicEffect);
		((UnityEvent<float>)(object)BgSlider.onValueChanged).AddListener((UnityAction<float>)UpdateMusicBg);
	}

	private void InitDropdown()
	{
		InitResolutionsDict();
		CurResolutionTextStr = $"{Screen.width}x{Screen.height}";
		ResolutionDropdown = Get<Dropdown>("分辨率/Dropdown");
		ResolutionsList = new List<string>();
		foreach (int key in ResolutionsDict.Keys)
		{
			ResolutionsList.Add($"{ResolutionsDict[key].X}x{ResolutionsDict[key].Y}");
		}
		ResolutionDropdown.AddOptions(ResolutionsList);
		InitFullScreenDict();
		FullScreenDropdown = Get<Dropdown>("显示模式/Dropdown");
		((UnityEvent<int>)(object)FullScreenDropdown.onValueChanged).AddListener((UnityAction<int>)delegate(int value)
		{
			if (value == 1)
			{
				((Selectable)ResolutionDropdown).interactable = false;
				ResolutionDropdown.value = ResolutionDropdown.options.Count - 1;
			}
			else
			{
				((Selectable)ResolutionDropdown).interactable = true;
			}
		});
		InitSaveTimesDict();
		SaveTimesDropdown = Get<Dropdown>("存档间隔/Dropdown");
		InitNpcActionTimesDict();
		NpcActionTimesDropdown = Get<Dropdown>("结算频率/Dropdown");
	}

	private void InitResolutionsDict()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		ResolutionsDict = new Dictionary<int, MResolution>();
		int num = 0;
		Resolution[] resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			Resolution val = resolutions[i];
			if (!list.Contains(((Resolution)(ref val)).width) && !(Mathf.Abs((float)((Resolution)(ref val)).height / (float)((Resolution)(ref val)).width - 0.5625f) > 0.001f))
			{
				MResolution mResolution = new MResolution();
				mResolution.X = ((Resolution)(ref val)).width;
				mResolution.Y = ((Resolution)(ref val)).height;
				ResolutionsDict.Add(num, mResolution);
				num++;
				list.Add(mResolution.X);
			}
		}
		list = null;
	}

	private void InitFullScreenDict()
	{
		FullScreenDict = new Dictionary<int, bool>();
		FullScreenDict.Add(0, value: false);
		FullScreenDict.Add(1, value: true);
	}

	private void InitSaveTimesDict()
	{
		SaveTimesDict = new Dictionary<int, int>();
		SaveTimesDict.Add(0, 0);
		SaveTimesDict.Add(1, 5);
		SaveTimesDict.Add(2, 10);
		SaveTimesDict.Add(3, -1);
	}

	private void InitNpcActionTimesDict()
	{
		NpcActionTimesDict = new Dictionary<int, int>();
		NpcActionTimesDict.Add(0, 0);
		NpcActionTimesDict.Add(1, 1);
		NpcActionTimesDict.Add(2, 2);
	}

	public void UpdateUI()
	{
		YinXiaoSlider.value = SystemConfig.Inst.GetEffectVolume();
		BgSlider.value = SystemConfig.Inst.GetBackGroundVolume();
		string item = $"{Screen.width}x{Screen.height}";
		if (ResolutionsList.Contains(item))
		{
			ResolutionDropdown.value = ResolutionsList.IndexOf(item);
		}
		else
		{
			ResolutionDropdown.value = 0;
		}
		if (Screen.fullScreen)
		{
			FullScreenDropdown.value = 1;
		}
		else
		{
			FullScreenDropdown.value = 0;
		}
		int saveTimes = SystemConfig.Inst.GetSaveTimes();
		foreach (int key in SaveTimesDict.Keys)
		{
			if (SaveTimesDict[key] == saveTimes)
			{
				SaveTimesDropdown.value = key;
				break;
			}
		}
		NpcActionTimesDropdown.value = SystemConfig.Inst.GetNpcActionTimes();
	}

	public void UpdateMusicEffect(float value)
	{
		MusicMag.instance.setEffectVolum(value);
	}

	public void UpdateMusicBg(float value)
	{
		MusicMag.instance.setBackGroundVolume(value);
	}

	public void StopAllMusice()
	{
		YinXiaoSlider.value = 0f;
		BgSlider.value = 0f;
	}

	public void SaveConfig()
	{
		PlayerPrefs.SetFloat("MusicBg", MusicMag.instance.audioSource.volume);
		PlayerPrefs.SetFloat("MusicEffect", MusicMag.instance.audioSourceEffect.volume);
		Screen.SetResolution(ResolutionsDict[ResolutionDropdown.value].X, ResolutionsDict[ResolutionDropdown.value].Y, FullScreenDict[FullScreenDropdown.value]);
		SystemConfig.Inst.SetSaveTimes(SaveTimesDict[SaveTimesDropdown.value]);
		SystemConfig.Inst.SetActionTimes(NpcActionTimesDict[NpcActionTimesDropdown.value]);
	}
}
