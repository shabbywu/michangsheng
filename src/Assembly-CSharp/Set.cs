using System;

// Token: 0x02000751 RID: 1873
public class Set
{
	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000234F8 File Offset: 0x000216F8
	// (set) Token: 0x06002FAB RID: 12203 RVA: 0x00023500 File Offset: 0x00021700
	public int TotalStarsInStage
	{
		get
		{
			return this.totalStarsInStage;
		}
		set
		{
			this.totalStarsInStage = value;
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06002FAC RID: 12204 RVA: 0x00023509 File Offset: 0x00021709
	// (set) Token: 0x06002FAD RID: 12205 RVA: 0x00023511 File Offset: 0x00021711
	public int CurrentStarsInStage
	{
		get
		{
			return this.currentStarsInStage;
		}
		set
		{
			this.currentStarsInStage = value;
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06002FAE RID: 12206 RVA: 0x0002351A File Offset: 0x0002171A
	// (set) Token: 0x06002FAF RID: 12207 RVA: 0x00023522 File Offset: 0x00021722
	public int CurrentStarsInStageNEW
	{
		get
		{
			return this.currentStarsInStageNEW;
		}
		set
		{
			this.currentStarsInStageNEW = value;
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0002352B File Offset: 0x0002172B
	// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x00023533 File Offset: 0x00021733
	public int StarRequirement
	{
		get
		{
			return this.starRequirement;
		}
		set
		{
			this.starRequirement = value;
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0002353C File Offset: 0x0002173C
	// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x00023544 File Offset: 0x00021744
	public string SetID
	{
		get
		{
			return this.setID;
		}
		set
		{
			this.setID = value;
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0002354D File Offset: 0x0002174D
	// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x00023555 File Offset: 0x00021755
	public int StagesOnSet
	{
		get
		{
			return this.stagesOnSet;
		}
		set
		{
			this.stagesOnSet = value;
		}
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x0002355E File Offset: 0x0002175E
	public int GetStarOnStage(int lvl)
	{
		if (lvl < this.stagesOnSet && this.starsPerStage != null)
		{
			return this.starsPerStage[lvl];
		}
		return -42;
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x0002357C File Offset: 0x0002177C
	public bool IsLvlUnlocked(int lvl)
	{
		if (lvl >= this.stagesOnSet)
		{
			throw new Exception("ERROR!");
		}
		if (this.starsPerStage != null)
		{
			return this.starsPerStage[lvl] > -1;
		}
		throw new Exception("ERROR!");
	}

	// Token: 0x06002FB8 RID: 12216 RVA: 0x0017D4EC File Offset: 0x0017B6EC
	public void SetStarOnStage(int lvl, int starN)
	{
		if (lvl < this.stagesOnSet)
		{
			if (this.starsPerStage != null)
			{
				if (this.starsPerStage[lvl] < starN)
				{
					this.CurrentStarsInStage -= ((this.starsPerStage[lvl] > 0) ? this.starsPerStage[lvl] : 0);
					StagesParser.currentStars -= ((this.starsPerStage[lvl] > 0) ? this.starsPerStage[lvl] : 0);
					this.starsPerStage[lvl] = starN;
					this.CurrentStarsInStage += ((starN > 0) ? starN : 0);
					StagesParser.currentStars += ((starN > 0) ? starN : 0);
					return;
				}
			}
			else
			{
				this.starsPerStage = new int[this.stagesOnSet];
				for (int i = 0; i < this.stagesOnSet; i++)
				{
					this.starsPerStage[i] = -42;
				}
				this.starsPerStage[lvl] = starN;
			}
		}
	}

	// Token: 0x06002FB9 RID: 12217 RVA: 0x000235B3 File Offset: 0x000217B3
	public Set()
	{
	}

	// Token: 0x06002FBA RID: 12218 RVA: 0x0017D5CC File Offset: 0x0017B7CC
	public Set(int numberOfStages)
	{
		this.starsPerStage = new int[numberOfStages];
		this.stagesOnSet = numberOfStages;
		for (int i = 0; i < this.stagesOnSet; i++)
		{
			this.starsPerStage[i] = -42;
		}
	}

	// Token: 0x04002AE1 RID: 10977
	private int totalStarsInStage;

	// Token: 0x04002AE2 RID: 10978
	private int currentStarsInStage;

	// Token: 0x04002AE3 RID: 10979
	private int currentStarsInStageNEW;

	// Token: 0x04002AE4 RID: 10980
	private int starRequirement;

	// Token: 0x04002AE5 RID: 10981
	private string setID = "NoTNamed";

	// Token: 0x04002AE6 RID: 10982
	private int stagesOnSet;

	// Token: 0x04002AE7 RID: 10983
	public int[] starsPerStage;
}
