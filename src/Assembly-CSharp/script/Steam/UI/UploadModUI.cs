using System;
using System.Collections.Generic;
using script.NewLianDan.Base;
using script.Steam.Ctr;
using script.Steam.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.UI
{
	// Token: 0x020009E5 RID: 2533
	public class UploadModUI : BasePanel
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x001DAF0F File Offset: 0x001D910F
		// (set) Token: 0x0600462C RID: 17964 RVA: 0x001DAF17 File Offset: 0x001D9117
		public UploadCtr UploadCtr { get; private set; }

		// Token: 0x0600462D RID: 17965 RVA: 0x001DAF20 File Offset: 0x001D9120
		public UploadModUI(GameObject gameObject)
		{
			this._go = gameObject;
			this.UploadCtr = new UploadCtr();
			this.UploadModProgress = base.Get<UploadModProgress>("上传过程");
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x001DAF4B File Offset: 0x001D914B
		public override void Show()
		{
			if (!this.isInit)
			{
				this.Init();
				this.isInit = true;
			}
			WorkShopMag.Inst.ModPoolUI.Hide();
			base.Show();
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x001DAF78 File Offset: 0x001D9178
		private void Init()
		{
			this.VisibilitySelect = base.Get<Dropdown>("可见性/选项");
			this.VisibilitySelect.onValueChanged.AddListener(delegate(int value)
			{
				if (this.UploadCtr.WorkShopItem == null)
				{
					UIPopTip.Inst.Pop("请先选择Mod文件", PopTipIconType.叹号);
					return;
				}
				this.UploadCtr.WorkShopItem.Visibility = value;
			});
			this.ModPath = base.Get<Text>("Mod文件夹/路径/Value");
			this.ImgPath = base.Get<Text>("封面/路径/Value");
			this.ModName = base.Get<InputField>("标题/Value");
			this.ModDesc = base.Get<TMP_InputField>("描述/Value");
			this.Progress = base.Get<Text>("上传过程/Value");
			this.CreateTags();
			base.Get<FpBtn>("Mod文件夹/选择").mouseUpEvent.AddListener(new UnityAction(this.UploadCtr.SelectMod));
			base.Get<FpBtn>("封面/选择").mouseUpEvent.AddListener(new UnityAction(this.UploadCtr.SelectImg));
			this.UpLoadBtn = base.Get<FpBtn>("上传过程/上传按钮");
			this.UpLoadBtn.mouseUpEvent.AddListener(new UnityAction(this.UploadCtr.ClickUpload));
			this.UpLoadingImg = base.Get<Image>("上传过程/Mask");
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x001DB09C File Offset: 0x001D929C
		public override void Hide()
		{
			if (this.isInit && this.UploadModProgress.IsUploading)
			{
				UIPopTip.Inst.Pop("正在上传Mod中，禁止关闭", PopTipIconType.叹号);
				return;
			}
			base.Hide();
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x001DB0CC File Offset: 0x001D92CC
		private void CreateTags()
		{
			this.Toggles = new List<Toggle>();
			GameObject gameObject = base.Get("类型/0", true);
			Transform parent = gameObject.transform.parent;
			float num = gameObject.transform.localPosition.x;
			float y = gameObject.transform.localPosition.y;
			using (List<string>.Enumerator enumerator = WorkShopMag.Tags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tag = enumerator.Current;
					GameObject gameObject2 = gameObject.Inst(parent);
					gameObject2.transform.localPosition = new Vector2(num, y);
					Toggle component = gameObject2.GetComponent<Toggle>();
					gameObject2.name = tag;
					component.onValueChanged.AddListener(delegate(bool arg0)
					{
						if (arg0)
						{
							this.UploadCtr.WorkShopItem.AddTags(tag);
							return;
						}
						this.UploadCtr.WorkShopItem.RemoveTag(tag);
					});
					this.Toggles.Add(component);
					gameObject2.transform.GetChild(1).GetComponent<Text>().SetText(tag);
					gameObject2.SetActive(true);
					num += 146f;
				}
			}
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x001DB200 File Offset: 0x001D9400
		public void UpdateUI()
		{
			this.VisibilitySelect.value = this.UploadCtr.WorkShopItem.Visibility;
			this.ModPath.SetTextWithEllipsis(this.UploadCtr.WorkShopItem.ModPath);
			this.ImgPath.SetTextWithEllipsis(this.UploadCtr.WorkShopItem.ImgPath);
			this.ModName.text = this.UploadCtr.WorkShopItem.Title;
			this.ModDesc.text = this.UploadCtr.WorkShopItem.Des;
			foreach (Toggle toggle in this.Toggles)
			{
				if (this.UploadCtr.WorkShopItem.Tags.Contains(toggle.gameObject.name))
				{
					toggle.isOn = true;
				}
				else
				{
					toggle.isOn = false;
				}
			}
		}

		// Token: 0x040047B9 RID: 18361
		private bool isInit;

		// Token: 0x040047BB RID: 18363
		public Dropdown VisibilitySelect;

		// Token: 0x040047BC RID: 18364
		public Text ModPath;

		// Token: 0x040047BD RID: 18365
		public Text ImgPath;

		// Token: 0x040047BE RID: 18366
		public InputField ModName;

		// Token: 0x040047BF RID: 18367
		public TMP_InputField ModDesc;

		// Token: 0x040047C0 RID: 18368
		public List<Toggle> Toggles;

		// Token: 0x040047C1 RID: 18369
		public Text Progress;

		// Token: 0x040047C2 RID: 18370
		public FpBtn UpLoadBtn;

		// Token: 0x040047C3 RID: 18371
		public Image UpLoadingImg;

		// Token: 0x040047C4 RID: 18372
		public UploadModProgress UploadModProgress;
	}
}
