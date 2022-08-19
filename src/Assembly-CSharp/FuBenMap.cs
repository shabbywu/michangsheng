using System;
using System.Collections.Generic;
using System.Linq;
using KBEngine;
using Newtonsoft.Json.Linq;

// Token: 0x020003B9 RID: 953
public class FuBenMap
{
	// Token: 0x06001EF0 RID: 7920 RVA: 0x000D8958 File Offset: 0x000D6B58
	public FuBenMap(int high, int wide)
	{
		this.map = new int[wide, high];
		this.mapIndex = FuBenMap.CreateMap(wide, high);
		this.High = high;
		this.Wide = wide;
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x000D8988 File Offset: 0x000D6B88
	public void CreateAllNode(Avatar avatar, JToken FuBenJson, JToken mapJson)
	{
		this.CreateExitNode();
		this.CreateAward(FuBenJson, mapJson);
		this.CreatRoadXian();
		this.CreateRoadEvent(FuBenJson, mapJson);
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x000D89A8 File Offset: 0x000D6BA8
	public void CreateRoadEvent(JToken FuBenJson, JToken mapJson)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		this.getAllMapIndex(FuBenMap.NodeType.Road, list, list2);
		int randomInt = Tools.getRandomInt((int)mapJson["EventNum"][0], (int)mapJson["EventNum"][1]);
		int num = (randomInt > list.Count) ? list.Count : randomInt;
		foreach (int index in Tools.getNumRandomList(list, num))
		{
			this.map[list[index], list2[index]] = 5;
		}
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x000D8A78 File Offset: 0x000D6C78
	public static int getIndexX(int index, int wide)
	{
		return (index - 1) % wide;
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x000D8A7F File Offset: 0x000D6C7F
	public static int getIndexY(int index, int wide)
	{
		return (index - 1) / wide;
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x000D8A86 File Offset: 0x000D6C86
	public static int getIndex(int x, int y, int wide)
	{
		return x + 1 + wide * y;
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x000D8A90 File Offset: 0x000D6C90
	public List<List<int>> ToListList()
	{
		List<List<int>> list = new List<List<int>>();
		for (int i = 0; i < this.High; i++)
		{
			List<int> list2 = new List<int>();
			for (int j = 0; j < this.Wide; j++)
			{
				list2.Add(this.map[j, i]);
			}
			list.Add(list2);
		}
		return list;
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x000D8AE8 File Offset: 0x000D6CE8
	public void GetIndexPosition(int index, ref int X, ref int Y)
	{
		for (int i = 0; i < this.High; i++)
		{
			for (int j = 0; j < this.Wide; j++)
			{
				if (this.mapIndex[j, i] == index)
				{
					X = j;
					Y = i;
					return;
				}
			}
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x000D8B30 File Offset: 0x000D6D30
	public void CreateAward(JToken FuBenJson, JToken mapJson)
	{
		int randomInt = Tools.getRandomInt((int)mapJson["AwakeNum"][0], (int)mapJson["AwakeNum"][1]);
		int num = 0;
		int num2 = 0;
		while (num2 < randomInt && num < 1000)
		{
			int emptyBian = this.getEmptyBian();
			if (emptyBian != -1)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				this.getBian((FuBenMap.FangXiang)emptyBian, list, list2);
				this.map[Tools.getRandomInt(list[0], list[1]), Tools.getRandomInt(list2[0], list2[1])] = 4;
			}
			else
			{
				int randomInt2 = Tools.getRandomInt(1, 4);
				List<int> x = new List<int>();
				List<int> y = new List<int>();
				this.getBian((FuBenMap.FangXiang)randomInt2, x, y);
				if (!this.SetAwardDieDia(x, y))
				{
					num2--;
				}
			}
			num++;
			num2++;
		}
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x000D8C28 File Offset: 0x000D6E28
	public void CreatRoadXian()
	{
		List<int> x = new List<int>();
		List<int> list = new List<int>();
		this.getAllMapIndex(FuBenMap.NodeType.Exit, x, list);
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		this.getAllMapIndex(FuBenMap.NodeType.Award, list2, list3);
		int num = list2.Max();
		for (int i = 0; i <= num; i++)
		{
			if (this.map[i, list[0]] == 0)
			{
				this.map[i, list[0]] = 1;
			}
		}
		for (int j = 0; j < list2.Count; j++)
		{
			if (list2[j] >= 2 && list2[j] <= this.Wide - 1 - 2)
			{
				if (list3[j] > list[0])
				{
					for (int k = list3[j]; k >= list[0]; k--)
					{
						if (this.map[list2[j], k] == 0)
						{
							this.map[list2[j], k] = 1;
						}
					}
				}
				else
				{
					for (int l = list3[j]; l <= list[0]; l++)
					{
						if (this.map[list2[j], l] == 0)
						{
							this.map[list2[j], l] = 1;
						}
					}
				}
			}
		}
		for (int m = 0; m < list2.Count; m++)
		{
			if (list2[m] < 2)
			{
				int num2 = list2[m] + 1;
				while (num2 <= num && this.map[num2, list3[m]] == 0)
				{
					this.map[num2, list3[m]] = 1;
					if (this.GetMapAroundNodeNum(num2, list3[m]) >= 2)
					{
						break;
					}
					num2++;
				}
			}
			if (list2[m] > this.Wide - 1 - 2)
			{
				int num3 = list2[m] - 1;
				while (num3 >= 0 && this.map[num3, list3[m]] == 0)
				{
					this.map[num3, list3[m]] = 1;
					if (this.GetMapAroundNodeNum(num3, list3[m]) >= 2)
					{
						break;
					}
					num3--;
				}
			}
		}
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x000D8E78 File Offset: 0x000D7078
	public List<int> getXiangLingRoad(int X, int Y)
	{
		List<int> list = new List<int>();
		if (this.map[(X > 0) ? (X - 1) : 0, Y] > 0)
		{
			list.Add(this.mapIndex[(X > 0) ? (X - 1) : 0, Y]);
		}
		if (this.map[(X >= this.Wide - 1) ? (this.Wide - 1) : (X + 1), Y] > 0)
		{
			list.Add(this.mapIndex[(X >= this.Wide - 1) ? (this.Wide - 1) : (X + 1), Y]);
		}
		if (this.map[X, (Y > 0) ? (Y - 1) : 0] > 0)
		{
			list.Add(this.mapIndex[X, (Y > 0) ? (Y - 1) : 0]);
		}
		if (this.map[X, (Y >= this.High - 1) ? (this.High - 1) : (Y + 1)] > 0)
		{
			list.Add(this.mapIndex[X, (Y >= this.High - 1) ? (this.High - 1) : (Y + 1)]);
		}
		return list;
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x000D8F98 File Offset: 0x000D7198
	public int GetMapAroundNodeNum(int X, int Y)
	{
		int num = 0;
		if (this.map[(X > 0) ? (X - 1) : 0, Y] > 0)
		{
			num++;
		}
		if (this.map[(X >= this.Wide - 1) ? (this.Wide - 1) : (X + 1), Y] > 0)
		{
			num++;
		}
		if (this.map[X, (Y > 0) ? (Y - 1) : 0] > 0)
		{
			num++;
		}
		if (this.map[X, (Y >= this.High - 1) ? (this.High - 1) : (Y + 1)] > 0)
		{
			num++;
		}
		return num;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x000D9038 File Offset: 0x000D7238
	public void getAllMapIndex(FuBenMap.NodeType nodeType, List<int> X, List<int> Y)
	{
		for (int i = 0; i < this.Wide; i++)
		{
			for (int j = 0; j < this.High; j++)
			{
				if (this.map[i, j] == (int)nodeType)
				{
					X.Add(i);
					Y.Add(j);
				}
			}
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x000D9088 File Offset: 0x000D7288
	public bool SetAwardDieDia(List<int> X, List<int> Y)
	{
		int randomInt = Tools.getRandomInt(X[0], X[1]);
		int randomInt2 = Tools.getRandomInt(Y[0], Y[1]);
		for (int i = randomInt - 1; i <= randomInt + 1; i++)
		{
			if (i >= 0 && i < this.Wide)
			{
				for (int j = randomInt2 - 1; j <= randomInt2 + 1; j++)
				{
					if (j >= 0 && j < this.High && this.map[i, j] != 0)
					{
						return false;
					}
				}
			}
		}
		this.map[randomInt, randomInt2] = 4;
		return true;
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x000D9117 File Offset: 0x000D7317
	public int getEmptyBian()
	{
		if (this.IsEmpty(FuBenMap.FangXiang.Top))
		{
			return 1;
		}
		if (this.IsEmpty(FuBenMap.FangXiang.Down))
		{
			return 2;
		}
		if (this.IsEmpty(FuBenMap.FangXiang.Left))
		{
			return 3;
		}
		if (this.IsEmpty(FuBenMap.FangXiang.Right))
		{
			return 4;
		}
		return -1;
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x000D9148 File Offset: 0x000D7348
	public void getBian(FuBenMap.FangXiang fangXiang, List<int> X, List<int> Y)
	{
		switch (fangXiang)
		{
		case FuBenMap.FangXiang.Top:
			for (int i = this.High - 2; i < this.High; i++)
			{
				Y.Add(i);
			}
			X.Add(2);
			X.Add(this.Wide - 1 - 2);
			return;
		case FuBenMap.FangXiang.Down:
			for (int j = 0; j < 2; j++)
			{
				Y.Add(j);
			}
			X.Add(2);
			X.Add(this.Wide - 1 - 2);
			return;
		case FuBenMap.FangXiang.Left:
			for (int k = 0; k < 2; k++)
			{
				X.Add(k);
			}
			Y.Add(2);
			Y.Add(this.High - 1 - 2);
			return;
		case FuBenMap.FangXiang.Right:
			for (int l = this.Wide - 2; l < this.Wide; l++)
			{
				X.Add(l);
			}
			Y.Add(2);
			Y.Add(this.High - 1 - 2);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x000D9234 File Offset: 0x000D7434
	public bool IsEmpty(FuBenMap.FangXiang fangXiang)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		switch (fangXiang)
		{
		case FuBenMap.FangXiang.Top:
			num = this.High - 1;
			num2 = this.High - 2;
			num3 = this.Wide;
			break;
		case FuBenMap.FangXiang.Down:
			num = 0;
			num2 = 1;
			num3 = this.Wide;
			break;
		case FuBenMap.FangXiang.Left:
			num = 0;
			num2 = 1;
			num3 = this.High;
			break;
		case FuBenMap.FangXiang.Right:
			num = this.Wide - 1;
			num2 = this.Wide - 2;
			num3 = this.High;
			break;
		}
		if (fangXiang == FuBenMap.FangXiang.Down || fangXiang == FuBenMap.FangXiang.Top)
		{
			for (int i = 0; i < num3; i++)
			{
				if (this.map[i, num] > 0 || this.map[i, num2] > 0)
				{
					return false;
				}
			}
		}
		else if (fangXiang == FuBenMap.FangXiang.Left || fangXiang == FuBenMap.FangXiang.Right)
		{
			for (int j = 0; j < num3; j++)
			{
				if (this.map[num, j] > 0 || this.map[num2, j] > 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x000D9324 File Offset: 0x000D7524
	public void CreateExitNode()
	{
		int num = 0;
		int randomInt = Tools.getRandomInt(2, this.High - 1 - 2);
		this.map[num, randomInt] = 2;
		this.CreateEntranceNode(randomInt);
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x000D935C File Offset: 0x000D755C
	public void CreateEntranceNode(int high)
	{
		int num = 1;
		this.map[num, high] = 3;
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x000D937C File Offset: 0x000D757C
	public static int[,] CreateMap(int wide, int high)
	{
		int[,] array = new int[wide, high];
		int num = 1;
		for (int i = 0; i < high; i++)
		{
			for (int j = 0; j < wide; j++)
			{
				array[j, i] = num;
				num++;
			}
		}
		return array;
	}

	// Token: 0x0400194C RID: 6476
	public int High;

	// Token: 0x0400194D RID: 6477
	public int Wide;

	// Token: 0x0400194E RID: 6478
	private const int JianGe = 2;

	// Token: 0x0400194F RID: 6479
	public int[,] map;

	// Token: 0x04001950 RID: 6480
	public int[,] mapIndex;

	// Token: 0x02001366 RID: 4966
	public enum NodeType
	{
		// Token: 0x0400684E RID: 26702
		NULL,
		// Token: 0x0400684F RID: 26703
		Road,
		// Token: 0x04006850 RID: 26704
		Exit,
		// Token: 0x04006851 RID: 26705
		Entrance,
		// Token: 0x04006852 RID: 26706
		Award,
		// Token: 0x04006853 RID: 26707
		Event
	}

	// Token: 0x02001367 RID: 4967
	public enum FangXiang
	{
		// Token: 0x04006855 RID: 26709
		Top = 1,
		// Token: 0x04006856 RID: 26710
		Down,
		// Token: 0x04006857 RID: 26711
		Left,
		// Token: 0x04006858 RID: 26712
		Right
	}
}
