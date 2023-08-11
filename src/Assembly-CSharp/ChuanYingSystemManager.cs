using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChuanYingSystemManager : MonoBehaviour
{
	[SerializeField]
	private Canvas ChuanYingCanvas;

	public static ChuanYingSystemManager inst;

	[HideInInspector]
	public Inventory2 inventory;

	[SerializeField]
	private Button CloseBtn;

	public List<Sprite> sprites = new List<Sprite>();

	[HideInInspector]
	public ChuanYingMessage curSelectChuanYingMessage;

	[SerializeField]
	private GameObject ChuanYingMessageCell;

	public RectTransform rectTransform;

	private void Awake()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		inst = this;
		inventory = UI_Manager.inst.inventory2;
		ChuanYingCanvas.worldCamera = UI_Manager.inst.RootCamera;
		((Component)this).transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		((Component)this).transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		((Component)this).transform.localPosition = Vector3.zero;
		Tools.canClickFlag = false;
		((UnityEvent)CloseBtn.onClick).AddListener(new UnityAction(Close));
		init();
	}

	public void init()
	{
		Tools.ClearObj(ChuanYingMessageCell.transform);
		Avatar player = Tools.instance.getPlayer();
		JSONObject hasReadChuanYingList = player.HasReadChuanYingList;
		JSONObject newChuanYingList = player.NewChuanYingList;
		for (int i = 0; i < newChuanYingList.Count; i++)
		{
			ChuanYingMessage component = Tools.InstantiateGameObject(ChuanYingMessageCell, ChuanYingMessageCell.transform.parent).GetComponent<ChuanYingMessage>();
			component.isRead = false;
			component.ChuanYingData = newChuanYingList[i];
			component.init();
		}
		if (hasReadChuanYingList.Count > 0)
		{
			for (int num = hasReadChuanYingList.Count - 1; num >= 0; num--)
			{
				ChuanYingMessage component2 = Tools.InstantiateGameObject(ChuanYingMessageCell, ChuanYingMessageCell.transform.parent).GetComponent<ChuanYingMessage>();
				component2.isRead = true;
				component2.ChuanYingData = hasReadChuanYingList[num];
				component2.init();
			}
		}
	}

	public void Close()
	{
		inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符);
	}

	public void deleteMessage(JSONObject json, GameObject obj)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		selectBox.instence.LianDanChoice("确定要删除此传音符吗", new EventDelegate(delegate
		{
			int i = json["id"].I;
			Tools.instance.getPlayer().HasReadChuanYingList.RemoveField(i.ToString());
			if ((Object)(object)curSelectChuanYingMessage != (Object)null && curSelectChuanYingMessage.ChuanYingData["id"].I == i)
			{
				curSelectChuanYingMessage = null;
			}
			Object.Destroy((Object)(object)obj);
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}), null, new Vector3(0.8f, 0.8f, 0.8f));
	}

	public void clickSelect(bool isShow, ChuanYingMessage chuanYingMessage)
	{
		if (isShow)
		{
			if ((Object)(object)curSelectChuanYingMessage != (Object)null)
			{
				curSelectChuanYingMessage.isShow = false;
				curSelectChuanYingMessage.content.SetActive(false);
				curSelectChuanYingMessage.updateSelfHeight();
			}
			curSelectChuanYingMessage = chuanYingMessage;
		}
		else
		{
			curSelectChuanYingMessage = null;
		}
	}

	private void OnDestroy()
	{
		Tools.canClickFlag = true;
		inst = null;
		PanelMamager.inst.closePanel(PanelMamager.PanelType.传音符, 1);
	}

	public void checkHasRead()
	{
		if (Tools.instance.getPlayer().NewChuanYingList.Count < 1)
		{
			UIHeadPanel.Inst.ChuanYinFuPoint.SetActive(false);
		}
	}
}
