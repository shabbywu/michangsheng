using System;

// Token: 0x02000052 RID: 82
[Serializable]
public class InvStat
{
	// Token: 0x06000487 RID: 1159 RVA: 0x00008007 File Offset: 0x00006207
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0006ED38 File Offset: 0x0006CF38
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x0006EDA8 File Offset: 0x0006CFA8
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x0006EE58 File Offset: 0x0006D058
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x040002C4 RID: 708
	public InvStat.Identifier id;

	// Token: 0x040002C5 RID: 709
	public InvStat.Modifier modifier;

	// Token: 0x040002C6 RID: 710
	public int amount;

	// Token: 0x02000053 RID: 83
	public enum Identifier
	{
		// Token: 0x040002C8 RID: 712
		Strength,
		// Token: 0x040002C9 RID: 713
		Constitution,
		// Token: 0x040002CA RID: 714
		Agility,
		// Token: 0x040002CB RID: 715
		Intelligence,
		// Token: 0x040002CC RID: 716
		Damage,
		// Token: 0x040002CD RID: 717
		Crit,
		// Token: 0x040002CE RID: 718
		Armor,
		// Token: 0x040002CF RID: 719
		Health,
		// Token: 0x040002D0 RID: 720
		Mana,
		// Token: 0x040002D1 RID: 721
		Other
	}

	// Token: 0x02000054 RID: 84
	public enum Modifier
	{
		// Token: 0x040002D3 RID: 723
		Added,
		// Token: 0x040002D4 RID: 724
		Percent
	}
}
