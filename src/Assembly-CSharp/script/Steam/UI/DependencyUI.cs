using System;
using System.Collections.Generic;
using System.IO;
using script.NewLianDan.Base;
using script.Steam.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI
{
	// Token: 0x020009E1 RID: 2529
	public class DependencyUI : BasePanel, IESCClose
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x001DA558 File Offset: 0x001D8758
		private WorkShopItem WorkShopItem
		{
			get
			{
				return WorkShopMag.Inst.UploadModUI.UploadCtr.WorkShopItem;
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x001DA570 File Offset: 0x001D8770
		public DependencyUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.toggles = new List<Toggle>();
			this.next = base.Get<Toggle>("Next");
			base.Get<FpBtn>("Close").mouseUpEvent.AddListener(new UnityAction(this.Hide));
			this.dependencyPrefab = base.Get("Scroll View/Viewport/Content/Base", true);
			this.dependencyParent = base.Get("Scroll View/Viewport/Content", true).transform;
			base.Get<FpBtn>("应用").mouseUpEvent.AddListener(new UnityAction(this.Save));
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x001DA614 File Offset: 0x001D8814
		private void CreateList()
		{
			Tools.ClearChild(this.dependencyParent);
			this.toggles = new List<Toggle>();
			foreach (ulong num in WorkShopMag.Inst.ModMagUI.Ctr.GetSubscribeModList())
			{
				if (num != (ulong)-1470617362 && num != (ulong)-1470121939)
				{
					string text = string.Format("{0}/{1}", WorkshopTool.WorkshopRootPath, num);
					string path = text + "/Mod.bin";
					if (Directory.Exists(text) && File.Exists(path))
					{
						try
						{
							GameObject gameObject = this.dependencyPrefab.Inst(this.dependencyParent);
							gameObject.name = num.ToString();
							Text component = gameObject.transform.GetChild(1).GetComponent<Text>();
							string title = WorkShopMag.Inst.UploadModUI.UploadCtr.ReadConfig(text).Title;
							component.SetTextWithEllipsis(title);
							Toggle component2 = gameObject.GetComponent<Toggle>();
							if (this.WorkShopItem.Dependencies.Contains(num))
							{
								component2.isOn = true;
							}
							else
							{
								component2.isOn = false;
							}
							gameObject.SetActive(true);
							this.toggles.Add(component2);
						}
						catch (Exception ex)
						{
							Debug.LogError(ex);
							UIPopTip.Inst.Pop("初始化订阅模组列表失败", PopTipIconType.叹号);
						}
					}
				}
			}
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x001DA7AC File Offset: 0x001D89AC
		public override void Show()
		{
			ESCCloseManager.Inst.RegisterClose(this);
			this.UpdateUI();
			base.Show();
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x001DA7C5 File Offset: 0x001D89C5
		private void UpdateUI()
		{
			this.next.isOn = this.WorkShopItem.IsNeedNext;
			this.CreateList();
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x001DA7E4 File Offset: 0x001D89E4
		private void Save()
		{
			this.WorkShopItem.IsNeedNext = this.next.isOn;
			this.WorkShopItem.Dependencies = new List<ulong>();
			foreach (Toggle toggle in this.toggles)
			{
				if (toggle.isOn)
				{
					this.WorkShopItem.Dependencies.Add(ulong.Parse(toggle.gameObject.name));
				}
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x001DA880 File Offset: 0x001D8A80
		public bool TryEscClose()
		{
			ESCCloseManager.Inst.UnRegisterClose(this);
			this.Hide();
			return true;
		}

		// Token: 0x040047A0 RID: 18336
		private Toggle next;

		// Token: 0x040047A1 RID: 18337
		private GameObject dependencyPrefab;

		// Token: 0x040047A2 RID: 18338
		private Transform dependencyParent;

		// Token: 0x040047A3 RID: 18339
		private List<Toggle> toggles;
	}
}
