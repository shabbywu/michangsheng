using System;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FE RID: 1022
public class UI_AvatarState : MonoBehaviour
{
	// Token: 0x060020F6 RID: 8438 RVA: 0x000E75F8 File Offset: 0x000E57F8
	private void Start()
	{
		Event.registerOut("set_attack_Max", this, "setAttackMax");
		Event.registerOut("set_attack_Min", this, "setAttackMin");
		Event.registerOut("set_defence", this, "setDefence");
		Event.registerOut("set_rating", this, "setRating");
		Event.registerOut("set_dodge", this, "setDodge");
		Event.registerOut("set_strength", this, "setStrength");
		Event.registerOut("set_dexterity", this, "setDexterity");
		Event.registerOut("set_exp", this, "setExp");
		Event.registerOut("set_level", this, "setLevel");
		Event.registerOut("set_stamina", this, "setStamina");
		Event.registerOut("set_Thirst", this, "set_Thirst");
		Event.registerOut("set_Hunger", this, "set_Hunger");
		base.gameObject.SetActive(false);
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void closeInventory()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000E76E0 File Offset: 0x000E58E0
	public void set_Thirst(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		this.playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		this.playerHandl.Thirst.Set((float)avatar.Thirst);
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000E772C File Offset: 0x000E592C
	public void set_Hunger(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		this.playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		this.playerHandl.Hunger.Set((float)avatar.Hunger);
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void openInventory()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x000E7777 File Offset: 0x000E5977
	public void setAttackMax(int v)
	{
		this.attack_max = v;
		this.text_attack.text = this.attack_min + " - " + this.attack_max;
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x000E77AB File Offset: 0x000E59AB
	public void setAttackMin(int v)
	{
		this.attack_min = v;
		this.text_attack.text = this.attack_min + " - " + this.attack_max;
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x000E77DF File Offset: 0x000E59DF
	public void setDefence(int v)
	{
		this.text_defence.text = string.Concat(v);
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x000E77F7 File Offset: 0x000E59F7
	public void setRating(int v)
	{
		this.text_rating.text = string.Concat(v);
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x000E780F File Offset: 0x000E5A0F
	public void setDodge(int v)
	{
		this.text_dodge.text = string.Concat(v);
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x000E7827 File Offset: 0x000E5A27
	public void setStrength(int v)
	{
		this.text_strength.text = string.Concat(v);
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x000E783F File Offset: 0x000E5A3F
	public void setDexterity(int v)
	{
		this.text_dexterity.text = string.Concat(v);
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x000E7857 File Offset: 0x000E5A57
	public void setExp(ulong v)
	{
		this.text_exp.text = string.Concat(v);
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x000E786F File Offset: 0x000E5A6F
	public void setLevel(ushort v)
	{
		this.text_level.text = string.Concat(v);
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x000E7887 File Offset: 0x000E5A87
	public void setStamina(int v)
	{
		this.text_stamina.text = string.Concat(v);
	}

	// Token: 0x04001ABC RID: 6844
	public Text text_attack;

	// Token: 0x04001ABD RID: 6845
	public Text text_defence;

	// Token: 0x04001ABE RID: 6846
	public Text text_rating;

	// Token: 0x04001ABF RID: 6847
	public Text text_dodge;

	// Token: 0x04001AC0 RID: 6848
	public Text text_strength;

	// Token: 0x04001AC1 RID: 6849
	public Text text_dexterity;

	// Token: 0x04001AC2 RID: 6850
	public Text text_exp;

	// Token: 0x04001AC3 RID: 6851
	public Text text_level;

	// Token: 0x04001AC4 RID: 6852
	public Text text_stamina;

	// Token: 0x04001AC5 RID: 6853
	private int attack_max;

	// Token: 0x04001AC6 RID: 6854
	private int attack_min;

	// Token: 0x04001AC7 RID: 6855
	public PlayerEventHandler playerHandl;
}
