using System;

public class Set
{
	private int totalStarsInStage;

	private int currentStarsInStage;

	private int currentStarsInStageNEW;

	private int starRequirement;

	private string setID = "NoTNamed";

	private int stagesOnSet;

	public int[] starsPerStage;

	public int TotalStarsInStage
	{
		get
		{
			return totalStarsInStage;
		}
		set
		{
			totalStarsInStage = value;
		}
	}

	public int CurrentStarsInStage
	{
		get
		{
			return currentStarsInStage;
		}
		set
		{
			currentStarsInStage = value;
		}
	}

	public int CurrentStarsInStageNEW
	{
		get
		{
			return currentStarsInStageNEW;
		}
		set
		{
			currentStarsInStageNEW = value;
		}
	}

	public int StarRequirement
	{
		get
		{
			return starRequirement;
		}
		set
		{
			starRequirement = value;
		}
	}

	public string SetID
	{
		get
		{
			return setID;
		}
		set
		{
			setID = value;
		}
	}

	public int StagesOnSet
	{
		get
		{
			return stagesOnSet;
		}
		set
		{
			stagesOnSet = value;
		}
	}

	public int GetStarOnStage(int lvl)
	{
		if (lvl < stagesOnSet && starsPerStage != null)
		{
			return starsPerStage[lvl];
		}
		return -42;
	}

	public bool IsLvlUnlocked(int lvl)
	{
		if (lvl < stagesOnSet)
		{
			if (starsPerStage != null)
			{
				if (starsPerStage[lvl] > -1)
				{
					return true;
				}
				return false;
			}
			throw new Exception("ERROR!");
		}
		throw new Exception("ERROR!");
	}

	public void SetStarOnStage(int lvl, int starN)
	{
		if (lvl >= stagesOnSet)
		{
			return;
		}
		if (starsPerStage != null)
		{
			if (starsPerStage[lvl] < starN)
			{
				CurrentStarsInStage -= ((starsPerStage[lvl] > 0) ? starsPerStage[lvl] : 0);
				StagesParser.currentStars -= ((starsPerStage[lvl] > 0) ? starsPerStage[lvl] : 0);
				starsPerStage[lvl] = starN;
				CurrentStarsInStage += ((starN > 0) ? starN : 0);
				StagesParser.currentStars += ((starN > 0) ? starN : 0);
			}
		}
		else
		{
			starsPerStage = new int[stagesOnSet];
			for (int i = 0; i < stagesOnSet; i++)
			{
				starsPerStage[i] = -42;
			}
			starsPerStage[lvl] = starN;
		}
	}

	public Set()
	{
	}

	public Set(int numberOfStages)
	{
		starsPerStage = new int[numberOfStages];
		stagesOnSet = numberOfStages;
		for (int i = 0; i < stagesOnSet; i++)
		{
			starsPerStage[i] = -42;
		}
	}
}
