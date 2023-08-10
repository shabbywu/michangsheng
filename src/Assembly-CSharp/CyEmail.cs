using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CyEmail : MonoBehaviour
{
	public GameObject noEmail;

	public GameObject msgCell;

	public List<Sprite> titleSprites;

	public List<Color> titleColors;

	public List<Sprite> btnSprites;

	public List<Color> numColors;

	public CySendBtn cySendBtn;

	public List<CyEmailCell> ActiveList = new List<CyEmailCell>();

	public ScrollRect scrollRect;

	public Transform msgParent;

	public int npcId;

	public Avatar player;

	private ContentSizeFitter sizeFitter;

	private RectTransform rectTransform;

	public bool isDeath;

	public int curIndex = -1;

	public void Init(int npcId)
	{
		curIndex = -1;
		ActiveList = new List<CyEmailCell>();
		((UnityEvent<Vector2>)(object)scrollRect.onValueChanged).AddListener((UnityAction<Vector2>)AddMore);
		sizeFitter = ((Component)msgParent).gameObject.GetComponent<ContentSizeFitter>();
		rectTransform = ((Component)msgParent).gameObject.GetComponent<RectTransform>();
		player = Tools.instance.getPlayer();
		this.npcId = npcId;
		isDeath = NpcJieSuanManager.inst.IsDeath(npcId);
		Tools.ClearObj(msgCell.transform);
		string key = npcId.ToString();
		if (player.emailDateMag.newEmailDictionary.ContainsKey(key))
		{
			player.emailDateMag.NewToHasRead(key);
		}
		if (player.emailDateMag.hasReadEmailDictionary.ContainsKey(key) && player.emailDateMag.hasReadEmailDictionary[key].Count > 0)
		{
			List<EmailData> list = player.emailDateMag.hasReadEmailDictionary[key];
			int num = list.Count;
			if (num > 5)
			{
				num = 5;
			}
			curIndex = list.Count - 1;
			while (num > 0)
			{
				GameObject obj = Tools.InstantiateGameObject(msgCell, msgParent);
				obj.transform.SetAsFirstSibling();
				CyEmailCell component = obj.GetComponent<CyEmailCell>();
				component.Init(list[curIndex], isDeath);
				ActiveList.Add(component);
				curIndex--;
				num--;
			}
			UpDateSize();
			noEmail.SetActive(false);
			((Component)msgParent).gameObject.SetActive(true);
		}
		else
		{
			noEmail.SetActive(true);
			((Component)msgParent).gameObject.SetActive(false);
		}
		if (isDeath || player.emailDateMag.IsStopAll)
		{
			((Component)cySendBtn).gameObject.SetActive(false);
		}
		else if (CyTeShuNpc.DataDict.ContainsKey(npcId))
		{
			if (CyTeShuNpc.DataDict[npcId].Type == 2)
			{
				((Component)cySendBtn).gameObject.SetActive(false);
			}
			else
			{
				((Component)cySendBtn).gameObject.SetActive(true);
			}
		}
		else
		{
			((Component)cySendBtn).gameObject.SetActive(true);
		}
	}

	private void AddMore(Vector2 arg0)
	{
		if (scrollRect.verticalNormalizedPosition >= 0.95f && curIndex >= 0)
		{
			int num = curIndex + 1;
			if (num > 5)
			{
				num = 5;
			}
			while (num > 0)
			{
				GameObject obj = Tools.InstantiateGameObject(msgCell, msgParent);
				obj.transform.SetAsFirstSibling();
				CyEmailCell component = obj.GetComponent<CyEmailCell>();
				component.Init(player.emailDateMag.hasReadEmailDictionary[npcId.ToString()][curIndex], isDeath);
				ActiveList.Add(component);
				curIndex--;
				num--;
			}
		}
	}

	public void Restart()
	{
		Tools.ClearObj(msgCell.transform);
		((Component)cySendBtn).gameObject.SetActive(false);
		CyUIMag.inst.No.SetActive(true);
	}

	public string GetContent(string msg, EmailData emailData)
	{
		if (msg.Contains("{FirstName}") && PlayerEx.IsDaoLv(emailData.npcId))
		{
			msg = msg.Replace("{FirstName}", "");
		}
		msg = msg.ReplaceTalkWord();
		_ = emailData.contentKey;
		if (msg.Contains("{daoyou}"))
		{
			msg = ((!PlayerEx.IsDaoLv(emailData.npcId)) ? msg.Replace("{daoyou}", emailData.daoYaoStr) : ((!player.DaoLvChengHu.HasField(emailData.npcId.ToString())) ? msg.Replace("{daoyou}", player.lastName) : msg.Replace("{daoyou}", player.DaoLvChengHu[emailData.npcId.ToString()].Str)));
		}
		if (msg.Contains("{xiaoyou}"))
		{
			msg = ((!PlayerEx.IsDaoLv(emailData.npcId)) ? msg.Replace("{xiaoyou}", emailData.xiaoYaoStr) : ((!player.DaoLvChengHu.HasField(emailData.npcId.ToString())) ? msg.Replace("{xiaoyou}", player.lastName) : msg.Replace("{xiaoyou}", player.DaoLvChengHu[emailData.npcId.ToString()].Str)));
		}
		if (msg.Contains("{DiDian}"))
		{
			msg = msg.Replace("{DiDian}", emailData.sceneName);
		}
		if (msg.Contains("{npcname}"))
		{
			msg = msg.Replace("{npcname}", emailData.npcName);
		}
		if (msg.Contains("{item}"))
		{
			msg = msg.Replace("{item}", jsonData.instance.ItemJsonData[emailData.item[0].ToString()]["name"].Str);
		}
		if (msg.Contains("{DongFuName}"))
		{
			msg = msg.Replace("{DongFuName}", Tools.instance.getPlayer().DongFuData[$"DongFu{emailData.DongFuId}"]["DongFuName"].Str);
		}
		msg = msg.ReplaceTalkWord();
		return msg;
	}

	public void UpDateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
	}

	public int GetPlayerItemNum(int id)
	{
		int num = 0;
		foreach (ITEM_INFO value in Tools.instance.getPlayer().itemList.values)
		{
			if (value.itemId == id)
			{
				num += (int)value.itemCount;
			}
		}
		return num;
	}

	public void SendMsgCallBack()
	{
		int num = player.emailDateMag.hasReadEmailDictionary[npcId.ToString()].Count - 2;
		if (num < 0)
		{
			num = 0;
		}
		for (int i = 0; i < 2; i++)
		{
			Tools.InstantiateGameObject(msgCell, msgParent).GetComponent<CyEmailCell>().Init(player.emailDateMag.hasReadEmailDictionary[npcId.ToString()][num], isDeath);
			num++;
		}
		noEmail.SetActive(false);
		((Component)msgParent).gameObject.SetActive(true);
		UpDateSize();
	}

	public void TiJiaoCallBack()
	{
		int num = player.emailDateMag.hasReadEmailDictionary[npcId.ToString()].Count - 1;
		if (num < 0)
		{
			num = 0;
		}
		Tools.InstantiateGameObject(msgCell, msgParent).GetComponent<CyEmailCell>().Init(player.emailDateMag.hasReadEmailDictionary[npcId.ToString()][num], isDeath);
		noEmail.SetActive(false);
		((Component)msgParent).gameObject.SetActive(true);
		UpDateSize();
	}

	public void SendItemBack()
	{
		foreach (CyEmailCell active in ActiveList)
		{
			active.UpdateTiJiao();
		}
	}
}
