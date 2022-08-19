using System;

// Token: 0x020004DB RID: 1243
public class Set
{
	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06002841 RID: 10305 RVA: 0x00130BBF File Offset: 0x0012EDBF
	// (set) Token: 0x06002842 RID: 10306 RVA: 0x00130BC7 File Offset: 0x0012EDC7
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

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06002843 RID: 10307 RVA: 0x00130BD0 File Offset: 0x0012EDD0
	// (set) Token: 0x06002844 RID: 10308 RVA: 0x00130BD8 File Offset: 0x0012EDD8
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

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06002845 RID: 10309 RVA: 0x00130BE1 File Offset: 0x0012EDE1
	// (set) Token: 0x06002846 RID: 10310 RVA: 0x00130BE9 File Offset: 0x0012EDE9
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

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06002847 RID: 10311 RVA: 0x00130BF2 File Offset: 0x0012EDF2
	// (set) Token: 0x06002848 RID: 10312 RVA: 0x00130BFA File Offset: 0x0012EDFA
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

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06002849 RID: 10313 RVA: 0x00130C03 File Offset: 0x0012EE03
	// (set) Token: 0x0600284A RID: 10314 RVA: 0x00130C0B File Offset: 0x0012EE0B
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

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x0600284B RID: 10315 RVA: 0x00130C14 File Offset: 0x0012EE14
	// (set) Token: 0x0600284C RID: 10316 RVA: 0x00130C1C File Offset: 0x0012EE1C
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

	// Token: 0x0600284D RID: 10317 RVA: 0x00130C25 File Offset: 0x0012EE25
	public int GetStarOnStage(int lvl)
	{
		if (lvl < this.stagesOnSet && this.starsPerStage != null)
		{
			return this.starsPerStage[lvl];
		}
		return -42;
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x00130C43 File Offset: 0x0012EE43
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

	// Token: 0x0600284F RID: 10319 RVA: 0x00130C7C File Offset: 0x0012EE7C
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

	// Token: 0x06002850 RID: 10320 RVA: 0x00130D59 File Offset: 0x0012EF59
	public Set()
	{
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x00130D6C File Offset: 0x0012EF6C
	public Set(int numberOfStages)
	{
		this.starsPerStage = new int[numberOfStages];
		this.stagesOnSet = numberOfStages;
		for (int i = 0; i < this.stagesOnSet; i++)
		{
			this.starsPerStage[i] = -42;
		}
	}

	// Token: 0x04002353 RID: 9043
	private int totalStarsInStage;

	// Token: 0x04002354 RID: 9044
	private int currentStarsInStage;

	// Token: 0x04002355 RID: 9045
	private int currentStarsInStageNEW;

	// Token: 0x04002356 RID: 9046
	private int starRequirement;

	// Token: 0x04002357 RID: 9047
	private string setID = "NoTNamed";

	// Token: 0x04002358 RID: 9048
	private int stagesOnSet;

	// Token: 0x04002359 RID: 9049
	public int[] starsPerStage;
}
