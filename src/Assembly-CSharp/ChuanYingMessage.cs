using System;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChuanYingMessage : MonoBehaviour
{
	public GameObject content;

	[SerializeField]
	private Button BtnSelect;

	[SerializeField]
	private Image Bg;

	[SerializeField]
	private Image statusImage;

	public bool isRead;

	public JSONObject ChuanYingData;

	public bool isShow;

	[SerializeField]
	private Text getTimeText;

	[SerializeField]
	private Text Title;

	[SerializeField]
	private Text TextContent;

	[SerializeField]
	private GameObject ItemGameObject;

	[SerializeField]
	private ChuanYingItem item;

	private bool isInit;

	[SerializeField]
	private RectTransform startRectTransform;

	[SerializeField]
	private RectTransform contentRectTransform;

	[SerializeField]
	private RectTransform childContentRectTransform;

	[SerializeField]
	private BtnISShow deleteBtn;

	public void init()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		isInit = false;
		isShow = false;
		((UnityEventBase)BtnSelect.onClick).RemoveAllListeners();
		((UnityEvent)BtnSelect.onClick).AddListener(new UnityAction(selectClick));
		if (isRead)
		{
			Bg.sprite = ChuanYingSystemManager.inst.sprites[0];
			statusImage.sprite = ChuanYingSystemManager.inst.sprites[1];
			((Component)deleteBtn).gameObject.SetActive(true);
			((Component)getTimeText).gameObject.SetActive(false);
			Title.text = " <color=#9fd7ac>" + ChuanYingData["AvatarName"].str + "的传音符</color>";
		}
		else
		{
			Bg.sprite = ChuanYingSystemManager.inst.sprites[2];
			statusImage.sprite = ChuanYingSystemManager.inst.sprites[3];
			((Component)deleteBtn).gameObject.SetActive(false);
			((Component)getTimeText).gameObject.SetActive(true);
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			int num = Mathf.Abs((DateTime.Parse(ChuanYingData["sendTime"].str) - nowTime).Days);
			int num2 = 0;
			int num3 = 0;
			if (num >= 365)
			{
				num2 = num / 365;
				getTimeText.text = $"{num2}年前";
			}
			else if (num >= 30)
			{
				num3 = num / 30;
				getTimeText.text = $"{num3}月前";
			}
			else
			{
				getTimeText.text = $"{num}天前";
			}
			Title.text = " <color=#ffdd7f>" + ChuanYingData["AvatarName"].str + "的传音符</color>";
		}
		((Graphic)statusImage).SetNativeSize();
		updateSelfHeight();
	}

	private void selectClick()
	{
		if (isRead && deleteBtn.isIn)
		{
			ChuanYingSystemManager.inst.deleteMessage(ChuanYingData, ((Component)this).gameObject);
			return;
		}
		isShow = !isShow;
		if (isShow)
		{
			if (!isInit)
			{
				TextContent.text = "\u3000" + ChuanYingData["info"].str;
				Avatar player = Tools.instance.getPlayer();
				isInit = true;
				if (ChuanYingData.HasField("ItemID") && ChuanYingData["ItemID"].I > 0)
				{
					if (!ChuanYingData["ItemHasGet"].b)
					{
						ChuanYingData.SetField("ItemHasGet", val: true);
						player.addItem(ChuanYingData["ItemID"].I, 1, Tools.CreateItemSeid(ChuanYingData["ItemID"].I));
					}
					ItemGameObject.SetActive(true);
					ItemDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
					item.Item = component.items[ChuanYingData["ItemID"].I].Clone();
					item.Item.itemNum = 1;
					item.hasItem = !ChuanYingData["ItemHasGet"].b;
					item.init();
				}
				else
				{
					ItemGameObject.SetActive(false);
				}
				if (ChuanYingData["CanCaoZuo"].b)
				{
					ChuanYingData.SetField("CanCaoZuo", val: false);
					if (ChuanYingData.HasField("TaskID"))
					{
						int i = ChuanYingData["TaskID"].I;
						if (!player.taskMag.isHasTask(i))
						{
							player.taskMag.addTask(i);
							string name = TaskJsonData.DataDict[i].Name;
							string msg = ((TaskJsonData.DataDict[i].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启"));
							UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
						}
					}
					if (ChuanYingData.HasField("WeiTuo"))
					{
						int i2 = ChuanYingData["WeiTuo"].I;
						if (!player.nomelTaskMag.IsNTaskStart(i2))
						{
							player.nomelTaskMag.StartNTask(i2, 0);
							UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
						}
					}
					if (ChuanYingData.HasField("TaskIndex"))
					{
						int i3 = ChuanYingData["TaskIndex"][0].I;
						int i4 = ChuanYingData["TaskIndex"][1].I;
						player.taskMag.setTaskIndex(i3, i4);
						string name2 = TaskJsonData.DataDict[i3].Name;
						UIPopTip.Inst.Pop("<color=#FF0000> " + name2 + " </color> 进度已更新", PopTipIconType.任务进度);
					}
					if (ChuanYingData.HasField("valueID"))
					{
						for (int j = 0; j < ChuanYingData["valueID"].Count; j++)
						{
							GlobalValue.Set(ChuanYingData["valueID"][j].I, ChuanYingData["value"][j].I, "ChuanYingMessage.selectClick");
						}
					}
				}
				if (!isRead)
				{
					if (ChuanYingData.HasField("IsDelete") && ChuanYingData["IsDelete"].I == 1)
					{
						player.NewChuanYingList.RemoveField(ChuanYingData["id"].I.ToString());
						isRead = true;
					}
					else
					{
						player.HasReadChuanYingList.SetField(ChuanYingData["id"].I.ToString(), ChuanYingData);
						player.NewChuanYingList.RemoveField(ChuanYingData["id"].I.ToString());
						isRead = true;
					}
				}
			}
			content.SetActive(true);
		}
		else
		{
			content.SetActive(false);
		}
		ChuanYingSystemManager.inst.clickSelect(isShow, this);
		updateSelfHeight();
		ChuanYingSystemManager.inst.checkHasRead();
	}

	public void updateSelfHeight()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		LayoutRebuilder.ForceRebuildLayoutImmediate(childContentRectTransform);
		LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
		content.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		if (content.activeSelf)
		{
			((Component)((Component)this).transform).GetComponent<RectTransform>().sizeDelta = new Vector2(startRectTransform.sizeDelta.x, startRectTransform.sizeDelta.y + contentRectTransform.sizeDelta.y);
		}
		else
		{
			((Component)((Component)this).transform).GetComponent<RectTransform>().sizeDelta = new Vector2(startRectTransform.sizeDelta.x, startRectTransform.sizeDelta.y);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(ChuanYingSystemManager.inst.rectTransform);
	}
}
