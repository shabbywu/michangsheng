using UnityEngine;

public class PlayerCharacterUI : MonoBehaviour
{
	private enum UIState
	{
		None,
		Item
	}

	public GUISkin skin;

	private CharacterInventory character;

	private CharacterSkillDeployer skill;

	private Vector2 scrollPosition;

	private UIState uiState;

	private void Start()
	{
		character = ((Component)this).gameObject.GetComponent<CharacterInventory>();
		skill = ((Component)this).gameObject.GetComponent<CharacterSkillDeployer>();
	}

	private void Update()
	{
		if (Screen.lockCursor && Input.GetKey((KeyCode)101))
		{
			uiState = UIState.Item;
		}
		Screen.lockCursor = uiState == UIState.None;
	}

	private void OnGUI()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit((Object)(object)skin))
		{
			GUI.skin = skin;
		}
		if (Object.op_Implicit((Object)(object)character))
		{
			GUI.skin.label.fontSize = 18;
			GUI.skin.label.alignment = (TextAnchor)0;
			GUI.Label(new Rect(60f, (float)(Screen.height - 130), 300f, 30f), "Equiped");
			GUI.BeginGroup(new Rect(50f, (float)(Screen.height - 100), (float)Screen.width, 100f));
			for (int i = 0; i < character.ItemsEquiped.Length; i++)
			{
				if (character.ItemsEquiped[i] != null)
				{
					DrawItemBox(character.ItemsEquiped[i], new Vector2((float)(i * 60), 0f));
				}
			}
			GUI.EndGroup();
			if (uiState == UIState.Item)
			{
				Screen.lockCursor = false;
				GUI.BeginGroup(new Rect((float)(Screen.width - 330), 30f, 300f, 350f));
				GUI.skin.label.fontSize = 20;
				GUI.skin.label.alignment = (TextAnchor)0;
				GUI.Label(new Rect(10f, 10f, 150f, 30f), "Item Lists");
				scrollPosition = GUI.BeginScrollView(new Rect(0f, 50f, 300f, 300f), scrollPosition, new Rect(0f, 50f, 280f, (float)(character.ItemSlots.Count * 50)));
				for (int j = 0; j < character.ItemSlots.Count; j++)
				{
					DrawItemBoxDetail(character.ItemSlots[j], new Vector2(0f, (float)(j * 60 + 50)));
				}
				GUI.EndScrollView();
				if (GUI.Button(new Rect(270f, 0f, 30f, 30f), "X"))
				{
					uiState = UIState.None;
				}
				GUI.EndGroup();
			}
			else
			{
				GUI.skin.label.fontSize = 17;
				GUI.skin.label.alignment = (TextAnchor)2;
				GUI.Label(new Rect((float)(Screen.width - 330), 30f, 300f, 30f), "Press 'E' Open Inventory");
			}
		}
		if (Object.op_Implicit((Object)(object)skill))
		{
			for (int k = 0; k < skill.Skill.Length; k++)
			{
				DrawSkill(k, new Vector2((float)(Screen.width - skill.Skill.Length * 60 - 30 + k * 60), (float)(Screen.height - 100)));
			}
		}
	}

	private void DrawSkill(int index, Vector2 position)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (skill.indexSkill == index)
		{
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = (TextAnchor)0;
			GUI.Label(new Rect(10f + position.x, position.y - 10f, 55f, 50f), "Selected");
		}
		if (GUI.Button(new Rect(10f + position.x, 10f + position.y, 50f, 50f), (Texture)(object)skill.SkillIcon[index]))
		{
			skill.indexSkill = index;
		}
	}

	private void DrawItemBox(ItemSlot itemslot, Vector2 position)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		if (itemslot != null)
		{
			ItemCollector itemCollector = character.itemManager.Items[itemslot.Index];
			GUI.Box(new Rect(10f + position.x, 10f + position.y, 50f, 50f), "");
			GUI.DrawTexture(new Rect(10f + position.x, 10f + position.y, 50f, 50f), (Texture)(object)itemCollector.Icon);
			GUI.skin.label.fontSize = 13;
			GUI.skin.label.alignment = (TextAnchor)0;
			GUI.Label(new Rect(14f + position.x, 14f + position.y, 30f, 30f), itemslot.Num.ToString());
		}
	}

	private void DrawItemBoxDetail(ItemSlot itemslot, Vector2 position)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		if (itemslot == null)
		{
			return;
		}
		ItemCollector itemCollector = character.itemManager.Items[itemslot.Index];
		GUI.Box(new Rect(10f + position.x, 10f + position.y, 50f, 50f), "");
		GUI.DrawTexture(new Rect(10f + position.x, 10f + position.y, 50f, 50f), (Texture)(object)itemCollector.Icon);
		GUI.skin.label.fontSize = 13;
		GUI.skin.label.alignment = (TextAnchor)0;
		GUI.Label(new Rect(14f + position.x, 14f + position.y, 30f, 30f), itemslot.Num.ToString());
		GUI.skin.label.alignment = (TextAnchor)3;
		GUI.Label(new Rect(position.x + 70f, position.y, 100f, 60f), itemCollector.Name);
		switch (itemCollector.ItemType)
		{
		case ItemType.Weapon:
			if (character.CheckEquiped(itemslot))
			{
				if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "UnEquipped"))
				{
					character.UnEquipItem(itemslot);
				}
			}
			else if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "Equip"))
			{
				character.EquipItem(itemslot);
			}
			break;
		case ItemType.Edible:
			if (GUI.Button(new Rect(200f + position.x, position.y + 10f, 80f, 30f), "Use"))
			{
				character.UseItem(itemslot);
			}
			break;
		}
	}
}
