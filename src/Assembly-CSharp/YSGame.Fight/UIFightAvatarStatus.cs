using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightAvatarStatus : MonoBehaviour
{
	public bool NoRefresh;

	public Text NameText;

	public Image LevelImage;

	public Text LingQiText;

	public Text LingQiText2;

	public Text HPText;

	public Image HPBar;

	public RectTransform BuffList;

	public UTooltipSkillTrigger HuaShenTrigger;

	public UIFightBuffItem AvatarDaDaoBuff;

	[HideInInspector]
	public Avatar Avatar;

	private GameObject BuffPrefab;

	public List<Sprite> JingJieSprites;

	[HideInInspector]
	public int BuffCount;

	private int lingYuSkillID;

	private bool needRefreshBuff;

	private float cd;

	private void Awake()
	{
		BuffPrefab = Resources.Load<GameObject>("FightPrefab/UIFightBuffItem");
		Event.registerOut("UpdataBuff", this, "UpdateBuffEvent");
	}

	private void OnDestroy()
	{
		OnClear();
	}

	private void Update()
	{
		if (NoRefresh)
		{
			return;
		}
		UpdateStatus(null);
		if (cd < 0f)
		{
			cd = 0.1f;
			if (needRefreshBuff)
			{
				needRefreshBuff = false;
				UpdateBuff2();
			}
		}
		else
		{
			cd -= Time.deltaTime;
		}
	}

	public void Init(Avatar avatar)
	{
		Avatar = avatar;
		NameText.text = Avatar.name;
		int levelType = avatar.getLevelType();
		LevelImage.sprite = JingJieSprites[levelType - 1];
		AvatarDaDaoBuff.Avatar = avatar;
		AvatarDaDaoBuff.BuffID = 10008;
		lingYuSkillID = Avatar.HuaShenLingYuSkill.I;
		HuaShenTrigger.SkillID = lingYuSkillID;
	}

	private void UpdateStatus(MessageData message)
	{
		if (Avatar != null)
		{
			HPText.text = $"{Avatar.HP}/{Avatar.HP_Max}";
			HPBar.fillAmount = (float)Avatar.HP / (float)Avatar.HP_Max;
			string text = $"{Avatar.cardMag.getCardNum()}/{Avatar.NowCard}";
			if ((Object)(object)LingQiText != (Object)null)
			{
				LingQiText.text = text;
			}
			if ((Object)(object)LingQiText2 != (Object)null)
			{
				LingQiText2.text = text;
			}
			if (lingYuSkillID > 0 && Avatar.spell.HasBuff(10017))
			{
				LevelImage.sprite = JingJieSprites[JingJieSprites.Count - 1];
			}
		}
	}

	public void OnClear()
	{
		Event.deregisterOut(this);
	}

	public void UpdateBuffEvent()
	{
		needRefreshBuff = true;
	}

	public void UpdateBuff2()
	{
		if (Avatar == null)
		{
			return;
		}
		if (Avatar.isPlayer() && RoundManager.instance.PlayerFightEventProcessor != null)
		{
			RoundManager.instance.PlayerFightEventProcessor.OnUpdateBuff();
		}
		for (int num = ((Transform)BuffList).childCount - 1; num >= 0; num--)
		{
			Object.Destroy((Object)(object)((Component)((Transform)BuffList).GetChild(num)).gameObject);
		}
		foreach (List<int> item in Avatar.bufflist)
		{
			if (BuffIsHideBuff106(item))
			{
				CreateBuffIcon(item);
			}
		}
		int num2 = 0;
		foreach (List<int> item2 in Avatar.bufflist)
		{
			if (BuffIsHideBuff106(item2))
			{
				num2++;
			}
		}
	}

	public void UpdateBuff()
	{
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		if (Avatar == null)
		{
			return;
		}
		int num = 0;
		foreach (List<int> item in Avatar.bufflist)
		{
			if (BuffIsHideBuff106(item))
			{
				num++;
			}
		}
		if (BuffCount != num)
		{
			BuffCount = num;
			for (int num2 = ((Transform)BuffList).childCount - 1; num2 >= 0; num2--)
			{
				Transform child = ((Transform)BuffList).GetChild(num2);
				if (((Component)child).gameObject.activeSelf)
				{
					if (BuffIsHideBuff106(((Component)child).GetComponent<UIFightBuffItem>().AvatarBuff))
					{
						if (((Component)child).GetComponent<UIFightBuffItem>().AvatarBuff[1] <= 0)
						{
							Object.DestroyImmediate((Object)(object)((Component)child).gameObject);
						}
					}
					else
					{
						Object.DestroyImmediate((Object)(object)((Component)child).gameObject);
					}
				}
			}
			foreach (List<int> item2 in Avatar.bufflist)
			{
				if (!BuffIsHideBuff106(item2))
				{
					continue;
				}
				bool flag = false;
				for (int i = 0; i < ((Transform)BuffList).childCount; i++)
				{
					UIFightBuffItem component = ((Component)((Transform)BuffList).GetChild(i)).GetComponent<UIFightBuffItem>();
					if (CheckBuffEquals(component.AvatarBuff, item2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					CreateBuffIcon(item2);
				}
			}
			for (int j = 2; j < ((Transform)BuffList).childCount - 1; j++)
			{
				for (int k = 2; k < ((Transform)BuffList).childCount - 1 - j; k++)
				{
					int i2 = jsonData.instance.BuffJsonData[((Component)((Transform)BuffList).GetChild(j)).GetComponent<UIFightBuffItem>().AvatarBuff[2].ToString()]["bufftype"].I;
					int i3 = jsonData.instance.BuffJsonData[((Component)((Transform)BuffList).GetChild(j + 1)).GetComponent<UIFightBuffItem>().AvatarBuff[2].ToString()]["bufftype"].I;
					if (i2 > i3)
					{
						((Transform)BuffList).GetChild(k).SetSiblingIndex(k + 1);
					}
				}
			}
			return;
		}
		List<List<int>> list = new List<List<int>>();
		foreach (Transform item3 in (Transform)BuffList)
		{
			Transform val = item3;
			foreach (List<int> item4 in Avatar.bufflist)
			{
				UIFightBuffItem component2 = ((Component)val).GetComponent<UIFightBuffItem>();
				if (component2.BuffID == item4[2] && !list.Contains(item4))
				{
					list.Add(item4);
					if (_BuffJsonData.DataDict[item4[2]].ShowOnlyOne == 1)
					{
						component2.BuffCountText.text = "1";
					}
					else
					{
						component2.BuffCountText.text = item4[1].ToString();
					}
					((Component)val).GetComponent<UIFightBuffItem>().BuffRound = item4[1];
					((Component)val).GetComponent<UIFightBuffItem>().AvatarBuff = item4;
					break;
				}
			}
		}
	}

	private bool CheckBuffEquals(List<int> a, List<int> b)
	{
		if (a.Count != b.Count)
		{
			return false;
		}
		for (int i = 0; i < a.Count; i++)
		{
			if (a[i] != b[i])
			{
				return false;
			}
		}
		return true;
	}

	public void CreateBuffIcon(List<int> buff)
	{
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(BuffPrefab);
		UIFightBuffItem component = obj.GetComponent<UIFightBuffItem>();
		component.Avatar = Avatar;
		_BuffJsonData buffJsonData = _BuffJsonData.DataDict[buff[2]];
		if (buffJsonData.ShowOnlyOne == 1)
		{
			component.BuffCountText.text = "1";
		}
		else
		{
			component.BuffCountText.text = buff[1].ToString();
		}
		Sprite val = ((buffJsonData.BuffIcon != 0) ? ResManager.inst.LoadSprite("Buff Icon/" + buffJsonData.BuffIcon) : ResManager.inst.LoadSprite("Buff Icon/" + buff[2]));
		if (Avatar.isPlayer() && Avatar.fightTemp.LianQiEquipDictionary.Keys.Count > 0)
		{
			int key = buff[2];
			if (Avatar.fightTemp.LianQiEquipDictionary.ContainsKey(key))
			{
				JSONObject jSONObject = Avatar.fightTemp.LianQiEquipDictionary[key];
				if (jSONObject != null && jSONObject.HasField("ItemIcon"))
				{
					val = ResManager.inst.LoadSprite(jSONObject["ItemIcon"].str);
				}
			}
		}
		if (!Avatar.isPlayer() && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.Keys.Count > 0)
		{
			int key2 = buff[2];
			if (RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(key2))
			{
				JSONObject jSONObject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[key2];
				if (jSONObject2 != null && jSONObject2.HasField("ItemIcon"))
				{
					val = ResManager.inst.LoadSprite(jSONObject2["ItemIcon"].str);
				}
			}
		}
		if ((Object)(object)val == (Object)null)
		{
			val = ResManager.inst.LoadSprite("Buff Icon/0");
		}
		component.BuffIconImage.sprite = val;
		component.BuffID = buff[2];
		component.BuffRound = buff[1];
		component.AvatarBuff = buff;
		obj.transform.SetParent((Transform)(object)BuffList);
		obj.SetActive(true);
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	private bool BuffIsHideBuff106(List<int> i)
	{
		JSONObject jSONObject = jsonData.instance.BuffJsonData[i[2].ToString()];
		if (jSONObject["isHide"].I == 1)
		{
			if (jSONObject["seid"].list.Find((JSONObject aa) => aa.I == 106) != null && jsonData.instance.Buff[i[2]].CanRealized(Avatar, null, i))
			{
				return true;
			}
			return false;
		}
		return true;
	}
}
