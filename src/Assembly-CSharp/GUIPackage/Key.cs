using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A60 RID: 2656
	public class Key : MonoBehaviour
	{
		// Token: 0x06004A89 RID: 19081 RVA: 0x001FAA0B File Offset: 0x001F8C0B
		private void Start()
		{
			this.draggingKey = false;
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x001FAA14 File Offset: 0x001F8C14
		private void Update()
		{
			if (this.draggingKey && Input.GetMouseButtonDown(1))
			{
				Singleton.inventory.Clear_dragedItem();
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x001FAA30 File Offset: 0x001F8C30
		public void Clear_ItemKey(item itemKey)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponent<KeyCell>().keyItem == itemKey)
				{
					transform.GetComponent<KeyCell>().keyItem = new item();
					break;
				}
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x001FAAA4 File Offset: 0x001F8CA4
		public void Clear_SkillKye(Skill skillKey)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponent<KeyCell>().keySkill == skillKey)
				{
					((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
					transform.GetComponent<KeyCell>().keySkill = new Skill();
					break;
				}
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001FAB3C File Offset: 0x001F8D3C
		public void Clear_MapSkillKye(Skill skillKey)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.GetComponent<KeyCellMapSkill>().keySkill == skillKey)
				{
					((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
					transform.GetComponent<KeyCellMapSkill>().keySkill = new Skill();
					break;
				}
			}
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x001FABD4 File Offset: 0x001F8DD4
		public void Clear_MapSkillPassKye(Skill skillKey)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				int skill_ID = transform.GetComponent<KeyCellMapPassSkill>().keySkill.skill_ID;
				if (skill_ID != -1 && jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["Skill_ID"].I == jsonData.instance.StaticSkillJsonData[skillKey.skill_ID.ToString()]["Skill_ID"].I)
				{
					((Avatar)KBEngineApp.app.player()).UnEquipStaticSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
					transform.GetComponent<KeyCellMapPassSkill>().keySkill = new Skill();
					break;
				}
			}
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x001FACCC File Offset: 0x001F8ECC
		public void SaveKey()
		{
			int num = 0;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				PlayerPrefs.SetInt("KeySkill" + num, Tools.instance.getSkillIDByKey(transform.GetComponent<KeyCell>().keySkill.skill_ID));
				PlayerPrefs.SetInt("KeyItem" + num, transform.GetComponent<KeyCell>().KeyItemID);
				num++;
			}
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x001FAD74 File Offset: 0x001F8F74
		public void SaveMapKey()
		{
			int num = 0;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				PlayerPrefs.SetInt("KeyMapSkill" + num, Tools.instance.getSkillIDByKey(transform.GetComponent<KeyCellMapSkill>().keySkill.skill_ID));
				PlayerPrefs.SetInt("KeyMapItem" + num, transform.GetComponent<KeyCellMapSkill>().KeyItemID);
				num++;
			}
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x001FAE1C File Offset: 0x001F901C
		public void SaveMapPassKey()
		{
			int num = 0;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				PlayerPrefs.SetInt("KeyMapPassSkill" + num, Tools.instance.getStaticSkillIDByKey(transform.GetComponent<KeyCellMapPassSkill>().keySkill.skill_ID));
				PlayerPrefs.SetInt("KeyMapPassItem" + num, transform.GetComponent<KeyCellMapPassSkill>().KeyItemID);
				num++;
			}
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x001FAEC4 File Offset: 0x001F90C4
		public void LoadMapKey()
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = 0;
			SkillDatebase component = jsonData.instance.gameObject.GetComponent<SkillDatebase>();
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				bool flag = true;
				foreach (SkillItem skillItem in avatar.equipSkillList)
				{
					if (skillItem.itemIndex == num)
					{
						flag = false;
						transform.GetComponent<KeyCellMapSkill>().keySkill = component.dicSkills[Tools.instance.getSkillKeyByID(skillItem.itemId, Tools.instance.getPlayer())];
					}
				}
				if (flag)
				{
					transform.GetComponent<KeyCellMapSkill>().keySkill = new Skill();
				}
				num++;
			}
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x001FAFDC File Offset: 0x001F91DC
		public void LoadMapPassKey()
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = 0;
			SkillStaticDatebase component = jsonData.instance.gameObject.GetComponent<SkillStaticDatebase>();
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				bool flag = true;
				foreach (SkillItem skillItem in avatar.equipStaticSkillList)
				{
					if (skillItem.itemIndex == num)
					{
						flag = false;
						transform.GetComponent<KeyCellMapPassSkill>().keySkill = component.dicSkills[Tools.instance.getStaticSkillKeyByID(skillItem.itemId)];
					}
				}
				if (flag)
				{
					transform.GetComponent<KeyCellMapPassSkill>().keySkill = new Skill();
				}
				num++;
			}
			StaticSkill.resetSeid(avatar);
			if (avatar.HP > avatar.HP_Max)
			{
				avatar.HP = avatar.HP_Max;
			}
			if (this.key0 != null && this.key6 != null)
			{
				if (avatar.level >= 10)
				{
					this.key0.transform.localPosition = new Vector3(-40f, -249f, 0f);
					this.key6.SetActive(true);
					this.key6.transform.localPosition = new Vector3(120f, -249f, 0f);
					return;
				}
				this.key0.transform.localPosition = new Vector3(40f, -249f, 0f);
				this.key6.SetActive(false);
			}
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x001FB1BC File Offset: 0x001F93BC
		public void LoadKey()
		{
			int num = 0;
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (PlayerPrefs.GetInt("KeySkill" + num, -1) >= 0 && Singleton.skillUI.GetSkillID(PlayerPrefs.GetInt("KeySkill" + num)) != -1)
				{
					transform.GetComponent<KeyCell>().keySkill = Singleton.skillUI.skill[Singleton.skillUI.GetSkillID(PlayerPrefs.GetInt("KeySkill" + num))];
				}
				else
				{
					transform.GetComponent<KeyCell>().keySkill = new Skill();
				}
				if (PlayerPrefs.GetInt("KeyItem" + num, -1) >= 0)
				{
					transform.GetComponent<KeyCell>().KeyItemID = PlayerPrefs.GetInt("KeyItem" + num);
					transform.GetComponent<KeyCell>().keyItem = Singleton.inventory.inventory[PlayerPrefs.GetInt("KeyItem" + num)];
				}
				else
				{
					transform.GetComponent<KeyCell>().KeyItemID = -1;
					transform.GetComponent<KeyCell>().keyItem = new item();
				}
				num++;
			}
		}

		// Token: 0x040049B5 RID: 18869
		public bool draggingKey;

		// Token: 0x040049B6 RID: 18870
		public GameObject key0;

		// Token: 0x040049B7 RID: 18871
		public GameObject key6;
	}
}
