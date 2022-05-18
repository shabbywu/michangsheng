using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class PlayerCharacterUI : MonoBehaviour
{
	// Token: 0x06000C70 RID: 3184 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterInventory>();
		this.skill = base.gameObject.GetComponent<CharacterSkillDeployer>();
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0000E619 File Offset: 0x0000C819
	private void Update()
	{
		if (Screen.lockCursor && Input.GetKey(101))
		{
			this.uiState = PlayerCharacterUI.UIState.Item;
		}
		Screen.lockCursor = (this.uiState == PlayerCharacterUI.UIState.None);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00097A44 File Offset: 0x00095C44
	private void OnGUI()
	{
		if (this.skin)
		{
			GUI.skin = this.skin;
		}
		if (this.character)
		{
			GUI.skin.label.fontSize = 18;
			GUI.skin.label.alignment = 0;
			GUI.Label(new Rect(60f, (float)(Screen.height - 130), 300f, 30f), "Equiped");
			GUI.BeginGroup(new Rect(50f, (float)(Screen.height - 100), (float)Screen.width, 100f));
			for (int i = 0; i < this.character.ItemsEquiped.Length; i++)
			{
				if (this.character.ItemsEquiped[i] != null)
				{
					this.DrawItemBox(this.character.ItemsEquiped[i], new Vector2((float)(i * 60), 0f));
				}
			}
			GUI.EndGroup();
			if (this.uiState == PlayerCharacterUI.UIState.Item)
			{
				Screen.lockCursor = false;
				GUI.BeginGroup(new Rect((float)(Screen.width - 330), 30f, 300f, 350f));
				GUI.skin.label.fontSize = 20;
				GUI.skin.label.alignment = 0;
				GUI.Label(new Rect(10f, 10f, 150f, 30f), "Item Lists");
				this.scrollPosition = GUI.BeginScrollView(new Rect(0f, 50f, 300f, 300f), this.scrollPosition, new Rect(0f, 50f, 280f, (float)(this.character.ItemSlots.Count * 50)));
				for (int j = 0; j < this.character.ItemSlots.Count; j++)
				{
					this.DrawItemBoxDetail(this.character.ItemSlots[j], new Vector2(0f, (float)(j * 60 + 50)));
				}
				GUI.EndScrollView();
				if (GUI.Button(new Rect(270f, 0f, 30f, 30f), "X"))
				{
					this.uiState = PlayerCharacterUI.UIState.None;
				}
				GUI.EndGroup();
			}
			else
			{
				GUI.skin.label.fontSize = 17;
				GUI.skin.label.alignment = 2;
				GUI.Label(new Rect((float)(Screen.width - 330), 30f, 300f, 30f), "Press 'E' Open Inventory");
			}
		}
		if (this.skill)
		{
			for (int k = 0; k < this.skill.Skill.Length; k++)
			{
				this.DrawSkill(k, new Vector2((float)(Screen.width - this.skill.Skill.Length * 60 - 30 + k * 60), (float)(Screen.height - 100)));
			}
		}
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00097D24 File Offset: 0x00095F24
	private void DrawSkill(int index, Vector2 position)
	{
		if (this.skill.indexSkill == index)
		{
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = 0;
			GUI.Label(new Rect(10f + position.x, position.y - 10f, 55f, 50f), "Selected");
		}
		if (GUI.Button(new Rect(10f + position.x, 10f + position.y, 50f, 50f), this.skill.SkillIcon[index]))
		{
			this.skill.indexSkill = index;
		}
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x00097DD8 File Offset: 0x00095FD8
	private void DrawItemBox(ItemSlot itemslot, Vector2 position)
	{
		if (itemslot != null)
		{
			ItemCollector itemCollector = this.character.itemManager.Items[itemslot.Index];
			GUI.Box(new Rect(10f + position.x, 10f + position.y, 50f, 50f), "");
			GUI.DrawTexture(new Rect(10f + position.x, 10f + position.y, 50f, 50f), itemCollector.Icon);
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = 0;
			GUI.Label(new Rect(14f + position.x, 14f + position.y, 30f, 30f), itemslot.Num.ToString());
		}
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00097EC4 File Offset: 0x000960C4
	private void DrawItemBoxDetail(ItemSlot itemslot, Vector2 position)
	{
		if (itemslot != null)
		{
			ItemCollector itemCollector = this.character.itemManager.Items[itemslot.Index];
			GUI.Box(new Rect(10f + position.x, 10f + position.y, 50f, 50f), "");
			GUI.DrawTexture(new Rect(10f + position.x, 10f + position.y, 50f, 50f), itemCollector.Icon);
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = 0;
			GUI.Label(new Rect(14f + position.x, 14f + position.y, 30f, 30f), itemslot.Num.ToString());
			GUI.skin.label.alignment = 3;
			GUI.Label(new Rect(position.x + 70f, position.y, 100f, 60f), itemCollector.Name);
			ItemType itemType = itemCollector.ItemType;
			if (itemType != ItemType.Weapon)
			{
				if (itemType != ItemType.Edible)
				{
					return;
				}
				if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "Use"))
				{
					this.character.UseItem(itemslot);
				}
			}
			else if (this.character.CheckEquiped(itemslot))
			{
				if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "UnEquipped"))
				{
					this.character.UnEquipItem(itemslot);
					return;
				}
			}
			else if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "Equip"))
			{
				this.character.EquipItem(itemslot);
				return;
			}
		}
	}

	// Token: 0x04000993 RID: 2451
	public GUISkin skin;

	// Token: 0x04000994 RID: 2452
	private CharacterInventory character;

	// Token: 0x04000995 RID: 2453
	private CharacterSkillDeployer skill;

	// Token: 0x04000996 RID: 2454
	private Vector2 scrollPosition;

	// Token: 0x04000997 RID: 2455
	private PlayerCharacterUI.UIState uiState;

	// Token: 0x02000168 RID: 360
	private enum UIState
	{
		// Token: 0x04000999 RID: 2457
		None,
		// Token: 0x0400099A RID: 2458
		Item
	}
}
