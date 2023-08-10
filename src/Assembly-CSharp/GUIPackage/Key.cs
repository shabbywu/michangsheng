using KBEngine;
using UnityEngine;

namespace GUIPackage;

public class Key : MonoBehaviour
{
	public bool draggingKey;

	public GameObject key0;

	public GameObject key6;

	private void Start()
	{
		draggingKey = false;
	}

	private void Update()
	{
		if (draggingKey && Input.GetMouseButtonDown(1))
		{
			Singleton.inventory.Clear_dragedItem();
		}
	}

	public void Clear_ItemKey(item itemKey)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (((Component)val).GetComponent<KeyCell>().keyItem == itemKey)
			{
				((Component)val).GetComponent<KeyCell>().keyItem = new item();
				break;
			}
		}
	}

	public void Clear_SkillKye(Skill skillKey)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (((Component)val).GetComponent<KeyCell>().keySkill == skillKey)
			{
				((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
				((Component)val).GetComponent<KeyCell>().keySkill = new Skill();
				break;
			}
		}
	}

	public void Clear_MapSkillKye(Skill skillKey)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (((Component)val).GetComponent<KeyCellMapSkill>().keySkill == skillKey)
			{
				((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
				((Component)val).GetComponent<KeyCellMapSkill>().keySkill = new Skill();
				break;
			}
		}
	}

	public void Clear_MapSkillPassKye(Skill skillKey)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			int skill_ID = ((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill.skill_ID;
			if (skill_ID != -1 && jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["Skill_ID"].I == jsonData.instance.StaticSkillJsonData[skillKey.skill_ID.ToString()]["Skill_ID"].I)
			{
				((Avatar)KBEngineApp.app.player()).UnEquipStaticSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
				((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = new Skill();
				break;
			}
		}
	}

	public void SaveKey()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		int num = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			PlayerPrefs.SetInt("KeySkill" + num, Tools.instance.getSkillIDByKey(((Component)val).GetComponent<KeyCell>().keySkill.skill_ID));
			PlayerPrefs.SetInt("KeyItem" + num, ((Component)val).GetComponent<KeyCell>().KeyItemID);
			num++;
		}
	}

	public void SaveMapKey()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		int num = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			PlayerPrefs.SetInt("KeyMapSkill" + num, Tools.instance.getSkillIDByKey(((Component)val).GetComponent<KeyCellMapSkill>().keySkill.skill_ID));
			PlayerPrefs.SetInt("KeyMapItem" + num, ((Component)val).GetComponent<KeyCellMapSkill>().KeyItemID);
			num++;
		}
	}

	public void SaveMapPassKey()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		int num = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			PlayerPrefs.SetInt("KeyMapPassSkill" + num, Tools.instance.getStaticSkillIDByKey(((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill.skill_ID));
			PlayerPrefs.SetInt("KeyMapPassItem" + num, ((Component)val).GetComponent<KeyCellMapPassSkill>().KeyItemID);
			num++;
		}
	}

	public void LoadMapKey()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		int num = 0;
		SkillDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillDatebase>();
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			bool flag = true;
			foreach (SkillItem equipSkill in avatar.equipSkillList)
			{
				if (equipSkill.itemIndex == num)
				{
					flag = false;
					((Component)val).GetComponent<KeyCellMapSkill>().keySkill = component.dicSkills[Tools.instance.getSkillKeyByID(equipSkill.itemId, Tools.instance.getPlayer())];
				}
			}
			if (flag)
			{
				((Component)val).GetComponent<KeyCellMapSkill>().keySkill = new Skill();
			}
			num++;
		}
	}

	public void LoadMapPassKey()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		int num = 0;
		SkillStaticDatebase component = ((Component)jsonData.instance).gameObject.GetComponent<SkillStaticDatebase>();
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			bool flag = true;
			foreach (SkillItem equipStaticSkill in avatar.equipStaticSkillList)
			{
				if (equipStaticSkill.itemIndex == num)
				{
					flag = false;
					((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = component.dicSkills[Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId)];
				}
			}
			if (flag)
			{
				((Component)val).GetComponent<KeyCellMapPassSkill>().keySkill = new Skill();
			}
			num++;
		}
		StaticSkill.resetSeid(avatar);
		if (avatar.HP > avatar.HP_Max)
		{
			avatar.HP = avatar.HP_Max;
		}
		if ((Object)(object)key0 != (Object)null && (Object)(object)key6 != (Object)null)
		{
			if (avatar.level >= 10)
			{
				key0.transform.localPosition = new Vector3(-40f, -249f, 0f);
				key6.SetActive(true);
				key6.transform.localPosition = new Vector3(120f, -249f, 0f);
			}
			else
			{
				key0.transform.localPosition = new Vector3(40f, -249f, 0f);
				key6.SetActive(false);
			}
		}
	}

	public void LoadKey()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		int num = 0;
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (PlayerPrefs.GetInt("KeySkill" + num, -1) >= 0 && Singleton.skillUI.GetSkillID(PlayerPrefs.GetInt("KeySkill" + num)) != -1)
			{
				((Component)val).GetComponent<KeyCell>().keySkill = Singleton.skillUI.skill[Singleton.skillUI.GetSkillID(PlayerPrefs.GetInt("KeySkill" + num))];
			}
			else
			{
				((Component)val).GetComponent<KeyCell>().keySkill = new Skill();
			}
			if (PlayerPrefs.GetInt("KeyItem" + num, -1) >= 0)
			{
				((Component)val).GetComponent<KeyCell>().KeyItemID = PlayerPrefs.GetInt("KeyItem" + num);
				((Component)val).GetComponent<KeyCell>().keyItem = Singleton.inventory.inventory[PlayerPrefs.GetInt("KeyItem" + num)];
			}
			else
			{
				((Component)val).GetComponent<KeyCell>().KeyItemID = -1;
				((Component)val).GetComponent<KeyCell>().keyItem = new item();
			}
			num++;
		}
	}
}
