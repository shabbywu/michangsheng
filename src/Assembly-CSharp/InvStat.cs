using System;

// Token: 0x0200003D RID: 61
[Serializable]
public class InvStat
{
	// Token: 0x0600043F RID: 1087 RVA: 0x0001787F File Offset: 0x00015A7F
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00017890 File Offset: 0x00015A90
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

	// Token: 0x06000441 RID: 1089 RVA: 0x00017900 File Offset: 0x00015B00
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

	// Token: 0x06000442 RID: 1090 RVA: 0x000179B0 File Offset: 0x00015BB0
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

	// Token: 0x04000266 RID: 614
	public InvStat.Identifier id;

	// Token: 0x04000267 RID: 615
	public InvStat.Modifier modifier;

	// Token: 0x04000268 RID: 616
	public int amount;

	// Token: 0x020011D9 RID: 4569
	public enum Identifier
	{
		// Token: 0x040063AE RID: 25518
		Strength,
		// Token: 0x040063AF RID: 25519
		Constitution,
		// Token: 0x040063B0 RID: 25520
		Agility,
		// Token: 0x040063B1 RID: 25521
		Intelligence,
		// Token: 0x040063B2 RID: 25522
		Damage,
		// Token: 0x040063B3 RID: 25523
		Crit,
		// Token: 0x040063B4 RID: 25524
		Armor,
		// Token: 0x040063B5 RID: 25525
		Health,
		// Token: 0x040063B6 RID: 25526
		Mana,
		// Token: 0x040063B7 RID: 25527
		Other
	}

	// Token: 0x020011DA RID: 4570
	public enum Modifier
	{
		// Token: 0x040063B9 RID: 25529
		Added,
		// Token: 0x040063BA RID: 25530
		Percent
	}
}
