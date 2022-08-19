using System;
using System.Collections.Generic;
using script.Steam.UI;
using script.Steam.Utils;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace script.Steam
{
	// Token: 0x020009D6 RID: 2518
	public class WorkShopMag : MonoBehaviour
	{
		// Token: 0x060045EC RID: 17900 RVA: 0x001D9A0C File Offset: 0x001D7C0C
		public static void Open()
		{
			try
			{
				if (!SteamAPI.IsSteamRunning())
				{
					Debug.LogError("Steam未运行，请从steam启动游戏");
					UIPopTip.Inst.Pop("Steam异常或未联网,请重启Steam", PopTipIconType.叹号);
					return;
				}
			}
			catch (Exception)
			{
				Debug.LogError("Steam异常，请检查网络");
				return;
			}
			if (MainUIMag.inst == null)
			{
				Debug.LogError("MainUIMag不存在,无法打开创意工坊");
				return;
			}
			if (WorkShopMag.Inst == null)
			{
				ResManager.inst.LoadPrefab("WorkShopPanel").Inst(MainUIMag.inst.transform);
				WorkShopMag.Inst.transform.SetAsLastSibling();
				if (WorkShopMag.TagsDict.Count <= 0)
				{
					WorkShopMag.TagsDict.Add("插件", "plugin");
					WorkShopMag.TagsDict.Add("优化", "optimization");
					WorkShopMag.TagsDict.Add("物品", "item");
					WorkShopMag.TagsDict.Add("技能", "skill");
					WorkShopMag.TagsDict.Add("剧情", "plot");
					WorkShopMag.TagsDict.Add("玩法", "play");
					WorkShopMag.TagsDict.Add("立绘", "Lihua");
					WorkShopMag.TagsDict.Add("综合", "comprehensive");
					WorkShopMag.TagsDict.Add("其他", "other");
					return;
				}
			}
			else
			{
				WorkShopMag.Inst.gameObject.SetActive(true);
			}
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x001D9B8C File Offset: 0x001D7D8C
		private void Awake()
		{
			WorkShopMag.Inst = this;
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x001D9B94 File Offset: 0x001D7D94
		private void Start()
		{
			this.MoreModInfoUI = new MoreModInfoUI(base.transform.Find("详细信息").gameObject);
			this.ModMagUI = new ModMagUI(base.transform.Find("模组管理界面").gameObject);
			this.WorkShopUI = new WorkShopUI(base.transform.Find("创意工坊界面").gameObject);
			this.UploadModUI = new UploadModUI(base.transform.Find("上传模组界面").gameObject);
			this.ModPoolUI = new ModPoolUI(base.transform.Find("Mod对象池").gameObject);
			this.DependencyUI = new DependencyUI(base.transform.Find("依赖界面").gameObject);
			this.ToggleGroup = new UIToggleGroup();
			this.ToggleGroup.AddToggle(new UIToggleA(base.transform.Find("选择界面按钮/管理模组按钮").gameObject, this.ToggleGroup, new UnityAction(this.ModMagUI.Show), new UnityAction(this.ModMagUI.Hide)));
			this.ToggleGroup.AddToggle(new UIToggleA(base.transform.Find("选择界面按钮/创意工坊按钮").gameObject, this.ToggleGroup, new UnityAction(this.WorkShopUI.Show), new UnityAction(this.WorkShopUI.Hide)));
			this.ToggleGroup.AddToggle(new UIToggleA(base.transform.Find("选择界面按钮/上传模组按钮").gameObject, this.ToggleGroup, new UnityAction(this.UploadModUI.Show), new UnityAction(this.UploadModUI.Hide)));
			this.ToggleGroup.SelectDefault();
			this.downUtils = base.gameObject.AddComponent<DownUtils>();
			base.transform.Find("上传模组界面/设置依赖按钮").GetComponent<FpBtn>().mouseUpEvent.AddListener(delegate()
			{
				if (this.UploadModUI.UploadCtr.WorkShopItem == null || string.IsNullOrEmpty(this.UploadModUI.UploadCtr.WorkShopItem.ModPath))
				{
					UIPopTip.Inst.Pop("请先选择MOD路径", PopTipIconType.叹号);
					return;
				}
				this.DependencyUI.Show();
			});
			base.transform.Find("上传模组界面/上传说明").GetComponent<FpBtn>().mouseUpEvent.AddListener(delegate()
			{
				Application.OpenURL("https://docs.qq.com/doc/DSUNjZnhvbnlEb0tR");
			});
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x001D9DE0 File Offset: 0x001D7FE0
		public void Close()
		{
			if (this.UploadModUI.UploadCtr.UploadModProgress.IsUploading)
			{
				UIPopTip.Inst.Pop("正在上传Mod,请稍等", PopTipIconType.叹号);
				return;
			}
			if (this.IsChange)
			{
				UCheckBox.Show("修改创意工坊设置后，需要重启生效,点击确定关闭游戏", new UnityAction(Application.Quit));
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x001D9E3F File Offset: 0x001D803F
		private void OnDestroy()
		{
			WorkShopMag.Inst = null;
		}

		// Token: 0x04004763 RID: 18275
		public static WorkShopMag Inst;

		// Token: 0x04004764 RID: 18276
		public ModPoolUI ModPoolUI;

		// Token: 0x04004765 RID: 18277
		public DownUtils downUtils;

		// Token: 0x04004766 RID: 18278
		public UIToggleGroup ToggleGroup;

		// Token: 0x04004767 RID: 18279
		public UploadModUI UploadModUI;

		// Token: 0x04004768 RID: 18280
		public ModMagUI ModMagUI;

		// Token: 0x04004769 RID: 18281
		public WorkShopUI WorkShopUI;

		// Token: 0x0400476A RID: 18282
		public MoreModInfoUI MoreModInfoUI;

		// Token: 0x0400476B RID: 18283
		public DependencyUI DependencyUI;

		// Token: 0x0400476C RID: 18284
		public static readonly List<string> Tags = new List<string>
		{
			"插件",
			"优化",
			"物品",
			"技能",
			"剧情",
			"玩法",
			"立绘",
			"综合",
			"其他"
		};

		// Token: 0x0400476D RID: 18285
		public static readonly List<string> EnTags = new List<string>
		{
			"plugin",
			"optimization",
			"item",
			"skill",
			"plot",
			"play",
			"Lihua",
			"comprehensive",
			"other"
		};

		// Token: 0x0400476E RID: 18286
		public static readonly Dictionary<string, string> TagsDict = new Dictionary<string, string>();

		// Token: 0x0400476F RID: 18287
		public Dictionary<ulong, ModInfo> ModInfoDict = new Dictionary<ulong, ModInfo>();

		// Token: 0x04004770 RID: 18288
		public bool IsChange;
	}
}
