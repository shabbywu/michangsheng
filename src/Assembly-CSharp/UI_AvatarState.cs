using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

public class UI_AvatarState : MonoBehaviour
{
	public Text text_attack;

	public Text text_defence;

	public Text text_rating;

	public Text text_dodge;

	public Text text_strength;

	public Text text_dexterity;

	public Text text_exp;

	public Text text_level;

	public Text text_stamina;

	private int attack_max;

	private int attack_min;

	public PlayerEventHandler playerHandl;

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
		((Component)this).gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void closeInventory()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void set_Thirst(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		playerHandl.Thirst.Set(avatar.Thirst);
	}

	public void set_Hunger(short v)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		GameObject male_Player = UI_Game.instence.Male_Player;
		playerHandl = male_Player.GetComponent<PlayerEventHandler>();
		playerHandl.Hunger.Set(avatar.Hunger);
	}

	public void openInventory()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void setAttackMax(int v)
	{
		attack_max = v;
		text_attack.text = attack_min + " - " + attack_max;
	}

	public void setAttackMin(int v)
	{
		attack_min = v;
		text_attack.text = attack_min + " - " + attack_max;
	}

	public void setDefence(int v)
	{
		text_defence.text = string.Concat(v);
	}

	public void setRating(int v)
	{
		text_rating.text = string.Concat(v);
	}

	public void setDodge(int v)
	{
		text_dodge.text = string.Concat(v);
	}

	public void setStrength(int v)
	{
		text_strength.text = string.Concat(v);
	}

	public void setDexterity(int v)
	{
		text_dexterity.text = string.Concat(v);
	}

	public void setExp(ulong v)
	{
		text_exp.text = string.Concat(v);
	}

	public void setLevel(ushort v)
	{
		text_level.text = string.Concat(v);
	}

	public void setStamina(int v)
	{
		text_stamina.text = string.Concat(v);
	}
}
