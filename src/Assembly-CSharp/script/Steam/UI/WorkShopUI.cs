using System;
using System.Collections.Generic;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.UI.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI
{
	// Token: 0x020009E6 RID: 2534
	public class WorkShopUI : BasePanel
	{
		// Token: 0x06004634 RID: 17972 RVA: 0x001DB339 File Offset: 0x001D9539
		public WorkShopUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.Ctr = new WorkShopCtr();
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x001DB354 File Offset: 0x001D9554
		private void Init()
		{
			this.CreateTags();
			base.Get<FpBtn>("刷新按钮").mouseUpEvent.AddListener(delegate()
			{
				this.Ctr.UpdateList(true);
			});
			this.CurPage = base.Get<Text>("翻页/CurPage/Value");
			this.Loading = base.Get("加载中", true);
			base.Get<FpBtn>("翻页/下一页").mouseUpEvent.AddListener(new UnityAction(this.Ctr.AddPage));
			base.Get<FpBtn>("翻页/上一页").mouseUpEvent.AddListener(new UnityAction(this.Ctr.ReducePage));
			base.Get<Dropdown>("选项").onValueChanged.AddListener(new UnityAction<int>(this.Ctr.SetQueryType));
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x001DB41D File Offset: 0x001D961D
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			this.Ctr.UpdateList(false);
			WorkShopMag.Inst.ModPoolUI.Show();
			base.Show();
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x001DB455 File Offset: 0x001D9655
		public void Select(ModUI modUI)
		{
			if (this.CurSelect != null)
			{
				this.CurSelect.CancelSelect();
			}
			this.CurSelect = modUI;
			WorkShopMag.Inst.MoreModInfoUI.Show(this.CurSelect.Info);
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x001DB48B File Offset: 0x001D968B
		public override void Hide()
		{
			if (this.CurSelect != null)
			{
				this.CurSelect.CancelSelect();
			}
			WorkShopMag.Inst.MoreModInfoUI.Hide();
			this.CurSelect = null;
			this.Ctr.Clear();
			base.Hide();
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x001DB4C8 File Offset: 0x001D96C8
		private void CreateTags()
		{
			this.Toggles = new List<Toggle>();
			GameObject gameObject = base.Get("标签/0", true);
			Transform parent = gameObject.transform.parent;
			float x = gameObject.transform.localPosition.x;
			float num = gameObject.transform.localPosition.y;
			using (List<string>.Enumerator enumerator = WorkShopMag.Tags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tag = enumerator.Current;
					GameObject gameObject2 = gameObject.Inst(parent);
					gameObject2.transform.localPosition = new Vector2(x, num);
					Toggle component = gameObject2.GetComponent<Toggle>();
					gameObject2.name = tag;
					component.onValueChanged.AddListener(delegate(bool arg0)
					{
						string tag;
						int index = WorkShopMag.Tags.IndexOf(tag);
						tag = WorkShopMag.EnTags[index];
						if (arg0)
						{
							this.Ctr.AddTag(tag);
							return;
						}
						this.Ctr.RemoveTag(tag);
					});
					this.Toggles.Add(component);
					gameObject2.transform.GetChild(1).GetComponent<Text>().SetText(tag);
					gameObject2.SetActive(true);
					num -= 50f;
				}
			}
		}

		// Token: 0x040047C5 RID: 18373
		private bool isInit;

		// Token: 0x040047C6 RID: 18374
		public readonly WorkShopCtr Ctr;

		// Token: 0x040047C7 RID: 18375
		public ModUI CurSelect;

		// Token: 0x040047C8 RID: 18376
		public Text CurPage;

		// Token: 0x040047C9 RID: 18377
		public List<Toggle> Toggles;

		// Token: 0x040047CA RID: 18378
		public GameObject Loading;
	}
}
