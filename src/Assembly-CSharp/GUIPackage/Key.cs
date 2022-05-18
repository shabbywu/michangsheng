using System;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D89 RID: 3465
	public class Key : MonoBehaviour
	{
		// Token: 0x0600538B RID: 21387 RVA: 0x0003BC24 File Offset: 0x00039E24
		private void Start()
		{
			this.draggingKey = false;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0003BC2D File Offset: 0x00039E2D
		private void Update()
		{
			if (this.draggingKey && Input.GetMouseButtonDown(1))
			{
				Singleton.inventory.Clear_dragedItem();
			}
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0022CDE4 File Offset: 0x0022AFE4
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

		// Token: 0x0600538E RID: 21390 RVA: 0x0022CE58 File Offset: 0x0022B058
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

		// Token: 0x0600538F RID: 21391 RVA: 0x0022CEF0 File Offset: 0x0022B0F0
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

		// Token: 0x06005390 RID: 21392 RVA: 0x0022CF88 File Offset: 0x0022B188
		public void Clear_MapSkillPassKye(Skill skillKey)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				int skill_ID = transform.GetComponent<KeyCellMapPassSkill>().keySkill.skill_ID;
				if (skill_ID != -1 && (int)jsonData.instance.StaticSkillJsonData[skill_ID.ToString()]["Skill_ID"].n == (int)jsonData.instance.StaticSkillJsonData[skillKey.skill_ID.ToString()]["Skill_ID"].n)
				{
					((Avatar)KBEngineApp.app.player()).UnEquipStaticSkill(Tools.instance.getSkillIDByKey(skillKey.skill_ID));
					transform.GetComponent<KeyCellMapPassSkill>().keySkill = new Skill();
					break;
				}
			}
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0022D080 File Offset: 0x0022B280
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

		// Token: 0x06005392 RID: 21394 RVA: 0x0022D128 File Offset: 0x0022B328
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

		// Token: 0x06005393 RID: 21395 RVA: 0x0022D1D0 File Offset: 0x0022B3D0
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

		// Token: 0x06005394 RID: 21396 RVA: 0x0022D278 File Offset: 0x0022B478
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

		// Token: 0x06005395 RID: 21397 RVA: 0x0022D390 File Offset: 0x0022B590
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

		// Token: 0x06005396 RID: 21398 RVA: 0x0022D570 File Offset: 0x0022B770
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

		// Token: 0x04005346 RID: 21318
		public bool draggingKey;

		// Token: 0x04005347 RID: 21319
		public GameObject key0;

		// Token: 0x04005348 RID: 21320
		public GameObject key6;
	}
}
