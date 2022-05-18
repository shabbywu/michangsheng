using System;
using System.Collections.Generic;
using System.Linq;
using KBEngine;
using Newtonsoft.Json.Linq;

// Token: 0x02000545 RID: 1349
public class FuBenMap
{
	// Token: 0x0600226E RID: 8814 RVA: 0x0001C369 File Offset: 0x0001A569
	public FuBenMap(int high, int wide)
	{
		this.map = new int[wide, high];
		this.mapIndex = FuBenMap.CreateMap(wide, high);
		this.High = high;
		this.Wide = wide;
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x0001C399 File Offset: 0x0001A599
	public void CreateAllNode(Avatar avatar, JToken FuBenJson, JToken mapJson)
	{
		this.CreateExitNode();
		this.CreateAward(FuBenJson, mapJson);
		this.CreatRoadXian();
		this.CreateRoadEvent(FuBenJson, mapJson);
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x0011B9C4 File Offset: 0x00119BC4
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

	// Token: 0x06002271 RID: 8817 RVA: 0x0001C3B7 File Offset: 0x0001A5B7
	public static int getIndexX(int index, int wide)
	{
		return (index - 1) % wide;
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x0001C3BE File Offset: 0x0001A5BE
	public static int getIndexY(int index, int wide)
	{
		return (index - 1) / wide;
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x0001C3C5 File Offset: 0x0001A5C5
	public static int getIndex(int x, int y, int wide)
	{
		return x + 1 + wide * y;
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x0011BA94 File Offset: 0x00119C94
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

	// Token: 0x06002275 RID: 8821 RVA: 0x0011BAEC File Offset: 0x00119CEC
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

	// Token: 0x06002276 RID: 8822 RVA: 0x0011BB34 File Offset: 0x00119D34
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

	// Token: 0x06002277 RID: 8823 RVA: 0x0011BC2C File Offset: 0x00119E2C
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

	// Token: 0x06002278 RID: 8824 RVA: 0x0011BE7C File Offset: 0x0011A07C
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

	// Token: 0x06002279 RID: 8825 RVA: 0x0011BF9C File Offset: 0x0011A19C
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

	// Token: 0x0600227A RID: 8826 RVA: 0x0011C03C File Offset: 0x0011A23C
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

	// Token: 0x0600227B RID: 8827 RVA: 0x0011C08C File Offset: 0x0011A28C
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

	// Token: 0x0600227C RID: 8828 RVA: 0x0001C3CE File Offset: 0x0001A5CE
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

	// Token: 0x0600227D RID: 8829 RVA: 0x0011C11C File Offset: 0x0011A31C
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

	// Token: 0x0600227E RID: 8830 RVA: 0x0011C208 File Offset: 0x0011A408
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

	// Token: 0x0600227F RID: 8831 RVA: 0x0011C2F8 File Offset: 0x0011A4F8
	public void CreateExitNode()
	{
		int num = 0;
		int randomInt = Tools.getRandomInt(2, this.High - 1 - 2);
		this.map[num, randomInt] = 2;
		this.CreateEntranceNode(randomInt);
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x0011C330 File Offset: 0x0011A530
	public void CreateEntranceNode(int high)
	{
		int num = 1;
		this.map[num, high] = 3;
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x0011C350 File Offset: 0x0011A550
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

	// Token: 0x04001DB9 RID: 7609
	public int High;

	// Token: 0x04001DBA RID: 7610
	public int Wide;

	// Token: 0x04001DBB RID: 7611
	private const int JianGe = 2;

	// Token: 0x04001DBC RID: 7612
	public int[,] map;

	// Token: 0x04001DBD RID: 7613
	public int[,] mapIndex;

	// Token: 0x02000546 RID: 1350
	public enum NodeType
	{
		// Token: 0x04001DBF RID: 7615
		NULL,
		// Token: 0x04001DC0 RID: 7616
		Road,
		// Token: 0x04001DC1 RID: 7617
		Exit,
		// Token: 0x04001DC2 RID: 7618
		Entrance,
		// Token: 0x04001DC3 RID: 7619
		Award,
		// Token: 0x04001DC4 RID: 7620
		Event
	}

	// Token: 0x02000547 RID: 1351
	public enum FangXiang
	{
		// Token: 0x04001DC6 RID: 7622
		Top = 1,
		// Token: 0x04001DC7 RID: 7623
		Down,
		// Token: 0x04001DC8 RID: 7624
		Left,
		// Token: 0x04001DC9 RID: 7625
		Right
	}
}
