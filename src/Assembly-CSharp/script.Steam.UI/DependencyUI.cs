using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.NewLianDan.Base;
using script.Steam.Utils;

namespace script.Steam.UI;

public class DependencyUI : BasePanel, IESCClose
{
	private Toggle next;

	private GameObject dependencyPrefab;

	private Transform dependencyParent;

	private List<Toggle> toggles;

	private WorkShopItem WorkShopItem => WorkShopMag.Inst.UploadModUI.UploadCtr.WorkShopItem;

	public DependencyUI(GameObject gameObject)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		_go = gameObject;
		toggles = new List<Toggle>();
		next = Get<Toggle>("Next");
		Get<FpBtn>("Close").mouseUpEvent.AddListener(new UnityAction(Hide));
		dependencyPrefab = Get("Scroll View/Viewport/Content/Base");
		dependencyParent = Get("Scroll View/Viewport/Content").transform;
		Get<FpBtn>("应用").mouseUpEvent.AddListener(new UnityAction(Save));
	}

	private void CreateList()
	{
		Tools.ClearChild(dependencyParent);
		toggles = new List<Toggle>();
		foreach (ulong subscribeMod in WorkShopMag.Inst.ModMagUI.Ctr.GetSubscribeModList())
		{
			if (subscribeMod == 2824349934u || subscribeMod == 2824845357u)
			{
				continue;
			}
			string text = $"{WorkshopTool.WorkshopRootPath}/{subscribeMod}";
			string path = text + "/Mod.bin";
			if (!Directory.Exists(text) || !File.Exists(path))
			{
				continue;
			}
			try
			{
				GameObject obj = dependencyPrefab.Inst(dependencyParent);
				((Object)obj).name = subscribeMod.ToString();
				Text component = ((Component)obj.transform.GetChild(1)).GetComponent<Text>();
				string title = WorkShopMag.Inst.UploadModUI.UploadCtr.ReadConfig(text).Title;
				component.SetTextWithEllipsis(title);
				Toggle component2 = obj.GetComponent<Toggle>();
				if (WorkShopItem.Dependencies.Contains(subscribeMod))
				{
					component2.isOn = true;
				}
				else
				{
					component2.isOn = false;
				}
				obj.SetActive(true);
				toggles.Add(component2);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
				UIPopTip.Inst.Pop("初始化订阅模组列表失败");
			}
		}
	}

	public override void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		UpdateUI();
		base.Show();
	}

	private void UpdateUI()
	{
		next.isOn = WorkShopItem.IsNeedNext;
		CreateList();
	}

	private void Save()
	{
		WorkShopItem.IsNeedNext = next.isOn;
		WorkShopItem.Dependencies = new List<ulong>();
		foreach (Toggle toggle in toggles)
		{
			if (toggle.isOn)
			{
				WorkShopItem.Dependencies.Add(ulong.Parse(((Object)((Component)toggle).gameObject).name));
			}
		}
	}

	public bool TryEscClose()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		Hide();
		return true;
	}
}
