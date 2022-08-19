using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class PlayerCharacterUI : MonoBehaviour
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x00045FA2 File Offset: 0x000441A2
	private void Start()
	{
		this.character = base.gameObject.GetComponent<CharacterInventory>();
		this.skill = base.gameObject.GetComponent<CharacterSkillDeployer>();
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00045FC6 File Offset: 0x000441C6
	private void Update()
	{
		if (Screen.lockCursor && Input.GetKey(101))
		{
			this.uiState = PlayerCharacterUI.UIState.Item;
		}
		Screen.lockCursor = (this.uiState == PlayerCharacterUI.UIState.None);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00045FF0 File Offset: 0x000441F0
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

	// Token: 0x06000B84 RID: 2948 RVA: 0x000462D0 File Offset: 0x000444D0
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

	// Token: 0x06000B85 RID: 2949 RVA: 0x00046384 File Offset: 0x00044584
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

	// Token: 0x06000B86 RID: 2950 RVA: 0x00046470 File Offset: 0x00044670
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

	// Token: 0x040007B8 RID: 1976
	public GUISkin skin;

	// Token: 0x040007B9 RID: 1977
	private CharacterInventory character;

	// Token: 0x040007BA RID: 1978
	private CharacterSkillDeployer skill;

	// Token: 0x040007BB RID: 1979
	private Vector2 scrollPosition;

	// Token: 0x040007BC RID: 1980
	private PlayerCharacterUI.UIState uiState;

	// Token: 0x0200123B RID: 4667
	private enum UIState
	{
		// Token: 0x0400652B RID: 25899
		None,
		// Token: 0x0400652C RID: 25900
		Item
	}
}
