using System;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005AE RID: 1454
public class UI_AvatarState : MonoBehaviour
{
	// Token: 0x060024A8 RID: 9384 RVA: 0x00129588 File Offset: 0x00127788
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

	// Token: 0x060024A9 RID: 9385 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void closeInventory()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x00129670 File Offset: 0x00127870
	public void set_Thirst(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		this.playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		this.playerHandl.Thirst.Set((float)avatar.Thirst);
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x001296BC File Offset: 0x001278BC
	public void set_Hunger(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		this.playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		this.playerHandl.Hunger.Set((float)avatar.Hunger);
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void openInventory()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x0001D700 File Offset: 0x0001B900
	public void setAttackMax(int v)
	{
		this.attack_max = v;
		this.text_attack.text = this.attack_min + " - " + this.attack_max;
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x0001D734 File Offset: 0x0001B934
	public void setAttackMin(int v)
	{
		this.attack_min = v;
		this.text_attack.text = this.attack_min + " - " + this.attack_max;
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x0001D768 File Offset: 0x0001B968
	public void setDefence(int v)
	{
		this.text_defence.text = string.Concat(v);
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x0001D780 File Offset: 0x0001B980
	public void setRating(int v)
	{
		this.text_rating.text = string.Concat(v);
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x0001D798 File Offset: 0x0001B998
	public void setDodge(int v)
	{
		this.text_dodge.text = string.Concat(v);
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
	public void setStrength(int v)
	{
		this.text_strength.text = string.Concat(v);
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
	public void setDexterity(int v)
	{
		this.text_dexterity.text = string.Concat(v);
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
	public void setExp(ulong v)
	{
		this.text_exp.text = string.Concat(v);
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
	public void setLevel(ushort v)
	{
		this.text_level.text = string.Concat(v);
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x0001D810 File Offset: 0x0001BA10
	public void setStamina(int v)
	{
		this.text_stamina.text = string.Concat(v);
	}

	// Token: 0x04001F78 RID: 8056
	public Text text_attack;

	// Token: 0x04001F79 RID: 8057
	public Text text_defence;

	// Token: 0x04001F7A RID: 8058
	public Text text_rating;

	// Token: 0x04001F7B RID: 8059
	public Text text_dodge;

	// Token: 0x04001F7C RID: 8060
	public Text text_strength;

	// Token: 0x04001F7D RID: 8061
	public Text text_dexterity;

	// Token: 0x04001F7E RID: 8062
	public Text text_exp;

	// Token: 0x04001F7F RID: 8063
	public Text text_level;

	// Token: 0x04001F80 RID: 8064
	public Text text_stamina;

	// Token: 0x04001F81 RID: 8065
	private int attack_max;

	// Token: 0x04001F82 RID: 8066
	private int attack_min;

	// Token: 0x04001F83 RID: 8067
	public PlayerEventHandler playerHandl;
}
