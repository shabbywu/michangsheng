using System;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000297 RID: 663
public class ChuanYingMessage : MonoBehaviour
{
	// Token: 0x060017DA RID: 6106 RVA: 0x000A5318 File Offset: 0x000A3518
	public void init()
	{
		this.isInit = false;
		this.isShow = false;
		this.BtnSelect.onClick.RemoveAllListeners();
		this.BtnSelect.onClick.AddListener(new UnityAction(this.selectClick));
		if (this.isRead)
		{
			this.Bg.sprite = ChuanYingSystemManager.inst.sprites[0];
			this.statusImage.sprite = ChuanYingSystemManager.inst.sprites[1];
			this.deleteBtn.gameObject.SetActive(true);
			this.getTimeText.gameObject.SetActive(false);
			this.Title.text = " <color=#9fd7ac>" + this.ChuanYingData["AvatarName"].str + "的传音符</color>";
		}
		else
		{
			this.Bg.sprite = ChuanYingSystemManager.inst.sprites[2];
			this.statusImage.sprite = ChuanYingSystemManager.inst.sprites[3];
			this.deleteBtn.gameObject.SetActive(false);
			this.getTimeText.gameObject.SetActive(true);
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			int num = Mathf.Abs((DateTime.Parse(this.ChuanYingData["sendTime"].str) - nowTime).Days);
			if (num >= 365)
			{
				int num2 = num / 365;
				this.getTimeText.text = string.Format("{0}年前", num2);
			}
			else if (num >= 30)
			{
				int num3 = num / 30;
				this.getTimeText.text = string.Format("{0}月前", num3);
			}
			else
			{
				this.getTimeText.text = string.Format("{0}天前", num);
			}
			this.Title.text = " <color=#ffdd7f>" + this.ChuanYingData["AvatarName"].str + "的传音符</color>";
		}
		this.statusImage.SetNativeSize();
		this.updateSelfHeight();
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x000A5548 File Offset: 0x000A3748
	private void selectClick()
	{
		if (this.isRead && this.deleteBtn.isIn)
		{
			ChuanYingSystemManager.inst.deleteMessage(this.ChuanYingData, base.gameObject);
			return;
		}
		this.isShow = !this.isShow;
		if (this.isShow)
		{
			if (!this.isInit)
			{
				this.TextContent.text = "\u3000" + this.ChuanYingData["info"].str;
				Avatar player = Tools.instance.getPlayer();
				this.isInit = true;
				if (this.ChuanYingData.HasField("ItemID") && this.ChuanYingData["ItemID"].I > 0)
				{
					if (!this.ChuanYingData["ItemHasGet"].b)
					{
						this.ChuanYingData.SetField("ItemHasGet", true);
						player.addItem(this.ChuanYingData["ItemID"].I, 1, Tools.CreateItemSeid(this.ChuanYingData["ItemID"].I), false);
					}
					this.ItemGameObject.SetActive(true);
					ItemDatebase component = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
					this.item.Item = component.items[this.ChuanYingData["ItemID"].I].Clone();
					this.item.Item.itemNum = 1;
					this.item.hasItem = !this.ChuanYingData["ItemHasGet"].b;
					this.item.init();
				}
				else
				{
					this.ItemGameObject.SetActive(false);
				}
				if (this.ChuanYingData["CanCaoZuo"].b)
				{
					this.ChuanYingData.SetField("CanCaoZuo", false);
					if (this.ChuanYingData.HasField("TaskID"))
					{
						int i = this.ChuanYingData["TaskID"].I;
						if (!player.taskMag.isHasTask(i))
						{
							player.taskMag.addTask(i);
							string name = TaskJsonData.DataDict[i].Name;
							string msg = (TaskJsonData.DataDict[i].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启");
							UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
						}
					}
					if (this.ChuanYingData.HasField("WeiTuo"))
					{
						int i2 = this.ChuanYingData["WeiTuo"].I;
						if (!player.nomelTaskMag.IsNTaskStart(i2))
						{
							player.nomelTaskMag.StartNTask(i2, 0);
							UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
						}
					}
					if (this.ChuanYingData.HasField("TaskIndex"))
					{
						int i3 = this.ChuanYingData["TaskIndex"][0].I;
						int i4 = this.ChuanYingData["TaskIndex"][1].I;
						player.taskMag.setTaskIndex(i3, i4);
						string name2 = TaskJsonData.DataDict[i3].Name;
						UIPopTip.Inst.Pop("<color=#FF0000> " + name2 + " </color> 进度已更新", PopTipIconType.任务进度);
					}
					if (this.ChuanYingData.HasField("valueID"))
					{
						for (int j = 0; j < this.ChuanYingData["valueID"].Count; j++)
						{
							GlobalValue.Set(this.ChuanYingData["valueID"][j].I, this.ChuanYingData["value"][j].I, "ChuanYingMessage.selectClick");
						}
					}
				}
				if (!this.isRead)
				{
					if (this.ChuanYingData.HasField("IsDelete") && this.ChuanYingData["IsDelete"].I == 1)
					{
						player.NewChuanYingList.RemoveField(this.ChuanYingData["id"].I.ToString());
						this.isRead = true;
					}
					else
					{
						player.HasReadChuanYingList.SetField(this.ChuanYingData["id"].I.ToString(), this.ChuanYingData);
						player.NewChuanYingList.RemoveField(this.ChuanYingData["id"].I.ToString());
						this.isRead = true;
					}
				}
			}
			this.content.SetActive(true);
		}
		else
		{
			this.content.SetActive(false);
		}
		ChuanYingSystemManager.inst.clickSelect(this.isShow, this);
		this.updateSelfHeight();
		ChuanYingSystemManager.inst.checkHasRead();
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000A5A1C File Offset: 0x000A3C1C
	public void updateSelfHeight()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.childContentRectTransform);
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.contentRectTransform);
		this.content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		if (this.content.activeSelf)
		{
			base.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.startRectTransform.sizeDelta.x, this.startRectTransform.sizeDelta.y + this.contentRectTransform.sizeDelta.y);
		}
		else
		{
			base.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(this.startRectTransform.sizeDelta.x, this.startRectTransform.sizeDelta.y);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(ChuanYingSystemManager.inst.rectTransform);
	}

	// Token: 0x04001290 RID: 4752
	public GameObject content;

	// Token: 0x04001291 RID: 4753
	[SerializeField]
	private Button BtnSelect;

	// Token: 0x04001292 RID: 4754
	[SerializeField]
	private Image Bg;

	// Token: 0x04001293 RID: 4755
	[SerializeField]
	private Image statusImage;

	// Token: 0x04001294 RID: 4756
	public bool isRead;

	// Token: 0x04001295 RID: 4757
	public JSONObject ChuanYingData;

	// Token: 0x04001296 RID: 4758
	public bool isShow;

	// Token: 0x04001297 RID: 4759
	[SerializeField]
	private Text getTimeText;

	// Token: 0x04001298 RID: 4760
	[SerializeField]
	private Text Title;

	// Token: 0x04001299 RID: 4761
	[SerializeField]
	private Text TextContent;

	// Token: 0x0400129A RID: 4762
	[SerializeField]
	private GameObject ItemGameObject;

	// Token: 0x0400129B RID: 4763
	[SerializeField]
	private ChuanYingItem item;

	// Token: 0x0400129C RID: 4764
	private bool isInit;

	// Token: 0x0400129D RID: 4765
	[SerializeField]
	private RectTransform startRectTransform;

	// Token: 0x0400129E RID: 4766
	[SerializeField]
	private RectTransform contentRectTransform;

	// Token: 0x0400129F RID: 4767
	[SerializeField]
	private RectTransform childContentRectTransform;

	// Token: 0x040012A0 RID: 4768
	[SerializeField]
	private BtnISShow deleteBtn;
}
