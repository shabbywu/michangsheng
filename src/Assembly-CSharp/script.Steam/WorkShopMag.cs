using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using script.Steam.UI;
using script.Steam.Utils;

namespace script.Steam;

public class WorkShopMag : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__16_1;

		internal void _003CStart_003Eb__16_1()
		{
			Application.OpenURL("https://docs.qq.com/doc/DSUNjZnhvbnlEb0tR");
		}
	}

	public static WorkShopMag Inst;

	public ModPoolUI ModPoolUI;

	public DownUtils downUtils;

	public UIToggleGroup ToggleGroup;

	public UploadModUI UploadModUI;

	public ModMagUI ModMagUI;

	public WorkShopUI WorkShopUI;

	public MoreModInfoUI MoreModInfoUI;

	public DependencyUI DependencyUI;

	public static readonly List<string> Tags = new List<string> { "插件", "优化", "物品", "技能", "剧情", "玩法", "立绘", "综合", "其他" };

	public static readonly List<string> EnTags = new List<string> { "plugin", "optimization", "item", "skill", "plot", "play", "Lihua", "comprehensive", "other" };

	public static readonly Dictionary<string, string> TagsDict = new Dictionary<string, string>();

	public Dictionary<ulong, ModInfo> ModInfoDict = new Dictionary<ulong, ModInfo>();

	public bool IsChange;

	public static void Open()
	{
		try
		{
			if (!SteamAPI.IsSteamRunning())
			{
				Debug.LogError((object)"Steam未运行，请从steam启动游戏");
				UIPopTip.Inst.Pop("Steam异常或未联网,请重启Steam");
				return;
			}
		}
		catch (Exception)
		{
			Debug.LogError((object)"Steam异常，请检查网络");
			return;
		}
		if ((Object)(object)MainUIMag.inst == (Object)null)
		{
			Debug.LogError((object)"MainUIMag不存在,无法打开创意工坊");
		}
		else if ((Object)(object)Inst == (Object)null)
		{
			ResManager.inst.LoadPrefab("WorkShopPanel").Inst(((Component)MainUIMag.inst).transform);
			((Component)Inst).transform.SetAsLastSibling();
			if (TagsDict.Count <= 0)
			{
				TagsDict.Add("插件", "plugin");
				TagsDict.Add("优化", "optimization");
				TagsDict.Add("物品", "item");
				TagsDict.Add("技能", "skill");
				TagsDict.Add("剧情", "plot");
				TagsDict.Add("玩法", "play");
				TagsDict.Add("立绘", "Lihua");
				TagsDict.Add("综合", "comprehensive");
				TagsDict.Add("其他", "other");
			}
		}
		else
		{
			((Component)Inst).gameObject.SetActive(true);
		}
	}

	private void Awake()
	{
		Inst = this;
	}

	private void Start()
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0115: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0164: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b3: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		MoreModInfoUI = new MoreModInfoUI(((Component)((Component)this).transform.Find("详细信息")).gameObject);
		ModMagUI = new ModMagUI(((Component)((Component)this).transform.Find("模组管理界面")).gameObject);
		WorkShopUI = new WorkShopUI(((Component)((Component)this).transform.Find("创意工坊界面")).gameObject);
		UploadModUI = new UploadModUI(((Component)((Component)this).transform.Find("上传模组界面")).gameObject);
		ModPoolUI = new ModPoolUI(((Component)((Component)this).transform.Find("Mod对象池")).gameObject);
		DependencyUI = new DependencyUI(((Component)((Component)this).transform.Find("依赖界面")).gameObject);
		ToggleGroup = new UIToggleGroup();
		UIToggleGroup toggleGroup = ToggleGroup;
		GameObject gameObject = ((Component)((Component)this).transform.Find("选择界面按钮/管理模组按钮")).gameObject;
		UIToggleGroup toggleGroup2 = ToggleGroup;
		ModMagUI modMagUI = ModMagUI;
		UnityAction val = modMagUI.Show;
		ModMagUI modMagUI2 = ModMagUI;
		toggleGroup.AddToggle(new UIToggleA(gameObject, toggleGroup2, val, new UnityAction(modMagUI2.Hide)));
		UIToggleGroup toggleGroup3 = ToggleGroup;
		GameObject gameObject2 = ((Component)((Component)this).transform.Find("选择界面按钮/创意工坊按钮")).gameObject;
		UIToggleGroup toggleGroup4 = ToggleGroup;
		WorkShopUI workShopUI = WorkShopUI;
		UnityAction val2 = workShopUI.Show;
		WorkShopUI workShopUI2 = WorkShopUI;
		toggleGroup3.AddToggle(new UIToggleA(gameObject2, toggleGroup4, val2, new UnityAction(workShopUI2.Hide)));
		UIToggleGroup toggleGroup5 = ToggleGroup;
		GameObject gameObject3 = ((Component)((Component)this).transform.Find("选择界面按钮/上传模组按钮")).gameObject;
		UIToggleGroup toggleGroup6 = ToggleGroup;
		UploadModUI uploadModUI = UploadModUI;
		UnityAction val3 = uploadModUI.Show;
		UploadModUI uploadModUI2 = UploadModUI;
		toggleGroup5.AddToggle(new UIToggleA(gameObject3, toggleGroup6, val3, new UnityAction(uploadModUI2.Hide)));
		ToggleGroup.SelectDefault();
		downUtils = ((Component)this).gameObject.AddComponent<DownUtils>();
		((Component)((Component)this).transform.Find("上传模组界面/设置依赖按钮")).GetComponent<FpBtn>().mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (UploadModUI.UploadCtr.WorkShopItem == null || string.IsNullOrEmpty(UploadModUI.UploadCtr.WorkShopItem.ModPath))
			{
				UIPopTip.Inst.Pop("请先选择MOD路径");
			}
			else
			{
				DependencyUI.Show();
			}
		});
		UnityEvent mouseUpEvent = ((Component)((Component)this).transform.Find("上传模组界面/上传说明")).GetComponent<FpBtn>().mouseUpEvent;
		object obj = _003C_003Ec._003C_003E9__16_1;
		if (obj == null)
		{
			UnityAction val4 = delegate
			{
				Application.OpenURL("https://docs.qq.com/doc/DSUNjZnhvbnlEb0tR");
			};
			_003C_003Ec._003C_003E9__16_1 = val4;
			obj = (object)val4;
		}
		mouseUpEvent.AddListener((UnityAction)obj);
	}

	public void Close()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		if (UploadModUI.UploadCtr.UploadModProgress.IsUploading)
		{
			UIPopTip.Inst.Pop("正在上传Mod,请稍等");
			return;
		}
		if (IsChange)
		{
			UCheckBox.Show("修改创意工坊设置后，需要重启生效,点击确定关闭游戏", new UnityAction(Application.Quit));
		}
		((Component)this).gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Inst = null;
	}
}
