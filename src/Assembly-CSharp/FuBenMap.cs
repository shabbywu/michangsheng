using System.Collections.Generic;
using System.Linq;
using KBEngine;
using Newtonsoft.Json.Linq;

public class FuBenMap
{
	public enum NodeType
	{
		NULL,
		Road,
		Exit,
		Entrance,
		Award,
		Event
	}

	public enum FangXiang
	{
		Top = 1,
		Down,
		Left,
		Right
	}

	public int High;

	public int Wide;

	private const int JianGe = 2;

	public int[,] map;

	public int[,] mapIndex;

	public FuBenMap(int high, int wide)
	{
		map = new int[wide, high];
		mapIndex = CreateMap(wide, high);
		High = high;
		Wide = wide;
	}

	public void CreateAllNode(Avatar avatar, JToken FuBenJson, JToken mapJson)
	{
		CreateExitNode();
		CreateAward(FuBenJson, mapJson);
		CreatRoadXian();
		CreateRoadEvent(FuBenJson, mapJson);
	}

	public void CreateRoadEvent(JToken FuBenJson, JToken mapJson)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		getAllMapIndex(NodeType.Road, list, list2);
		int randomInt = Tools.getRandomInt((int)mapJson[(object)"EventNum"][(object)0], (int)mapJson[(object)"EventNum"][(object)1]);
		int num = ((randomInt > list.Count) ? list.Count : randomInt);
		foreach (int numRandom in Tools.getNumRandomList(list, num))
		{
			map[list[numRandom], list2[numRandom]] = 5;
		}
	}

	public static int getIndexX(int index, int wide)
	{
		return (index - 1) % wide;
	}

	public static int getIndexY(int index, int wide)
	{
		return (index - 1) / wide;
	}

	public static int getIndex(int x, int y, int wide)
	{
		return x + 1 + wide * y;
	}

	public List<List<int>> ToListList()
	{
		List<List<int>> list = new List<List<int>>();
		for (int i = 0; i < High; i++)
		{
			List<int> list2 = new List<int>();
			for (int j = 0; j < Wide; j++)
			{
				list2.Add(map[j, i]);
			}
			list.Add(list2);
		}
		return list;
	}

	public void GetIndexPosition(int index, ref int X, ref int Y)
	{
		for (int i = 0; i < High; i++)
		{
			for (int j = 0; j < Wide; j++)
			{
				if (mapIndex[j, i] == index)
				{
					X = j;
					Y = i;
					return;
				}
			}
		}
	}

	public void CreateAward(JToken FuBenJson, JToken mapJson)
	{
		int randomInt = Tools.getRandomInt((int)mapJson[(object)"AwakeNum"][(object)0], (int)mapJson[(object)"AwakeNum"][(object)1]);
		int num = 0;
		for (int i = 0; i < randomInt; i++)
		{
			if (num >= 1000)
			{
				break;
			}
			int emptyBian = getEmptyBian();
			if (emptyBian != -1)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				getBian((FangXiang)emptyBian, list, list2);
				map[Tools.getRandomInt(list[0], list[1]), Tools.getRandomInt(list2[0], list2[1])] = 4;
			}
			else
			{
				int randomInt2 = Tools.getRandomInt(1, 4);
				List<int> x = new List<int>();
				List<int> y = new List<int>();
				getBian((FangXiang)randomInt2, x, y);
				if (!SetAwardDieDia(x, y))
				{
					i--;
				}
			}
			num++;
		}
	}

	public void CreatRoadXian()
	{
		List<int> x = new List<int>();
		List<int> list = new List<int>();
		getAllMapIndex(NodeType.Exit, x, list);
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		getAllMapIndex(NodeType.Award, list2, list3);
		int num = list2.Max();
		for (int i = 0; i <= num; i++)
		{
			if (map[i, list[0]] == 0)
			{
				map[i, list[0]] = 1;
			}
		}
		for (int j = 0; j < list2.Count; j++)
		{
			if (list2[j] < 2 || list2[j] > Wide - 1 - 2)
			{
				continue;
			}
			if (list3[j] > list[0])
			{
				for (int num2 = list3[j]; num2 >= list[0]; num2--)
				{
					if (map[list2[j], num2] == 0)
					{
						map[list2[j], num2] = 1;
					}
				}
				continue;
			}
			for (int k = list3[j]; k <= list[0]; k++)
			{
				if (map[list2[j], k] == 0)
				{
					map[list2[j], k] = 1;
				}
			}
		}
		for (int l = 0; l < list2.Count; l++)
		{
			if (list2[l] < 2)
			{
				for (int m = list2[l] + 1; m <= num && map[m, list3[l]] == 0; m++)
				{
					map[m, list3[l]] = 1;
					if (GetMapAroundNodeNum(m, list3[l]) >= 2)
					{
						break;
					}
				}
			}
			if (list2[l] <= Wide - 1 - 2)
			{
				continue;
			}
			int num3 = list2[l] - 1;
			while (num3 >= 0 && map[num3, list3[l]] == 0)
			{
				map[num3, list3[l]] = 1;
				if (GetMapAroundNodeNum(num3, list3[l]) >= 2)
				{
					break;
				}
				num3--;
			}
		}
	}

	public List<int> getXiangLingRoad(int X, int Y)
	{
		List<int> list = new List<int>();
		if (map[(X > 0) ? (X - 1) : 0, Y] > 0)
		{
			list.Add(mapIndex[(X > 0) ? (X - 1) : 0, Y]);
		}
		if (map[(X >= Wide - 1) ? (Wide - 1) : (X + 1), Y] > 0)
		{
			list.Add(mapIndex[(X >= Wide - 1) ? (Wide - 1) : (X + 1), Y]);
		}
		if (map[X, (Y > 0) ? (Y - 1) : 0] > 0)
		{
			list.Add(mapIndex[X, (Y > 0) ? (Y - 1) : 0]);
		}
		if (map[X, (Y >= High - 1) ? (High - 1) : (Y + 1)] > 0)
		{
			list.Add(mapIndex[X, (Y >= High - 1) ? (High - 1) : (Y + 1)]);
		}
		return list;
	}

	public int GetMapAroundNodeNum(int X, int Y)
	{
		int num = 0;
		if (map[(X > 0) ? (X - 1) : 0, Y] > 0)
		{
			num++;
		}
		if (map[(X >= Wide - 1) ? (Wide - 1) : (X + 1), Y] > 0)
		{
			num++;
		}
		if (map[X, (Y > 0) ? (Y - 1) : 0] > 0)
		{
			num++;
		}
		if (map[X, (Y >= High - 1) ? (High - 1) : (Y + 1)] > 0)
		{
			num++;
		}
		return num;
	}

	public void getAllMapIndex(NodeType nodeType, List<int> X, List<int> Y)
	{
		for (int i = 0; i < Wide; i++)
		{
			for (int j = 0; j < High; j++)
			{
				if (map[i, j] == (int)nodeType)
				{
					X.Add(i);
					Y.Add(j);
				}
			}
		}
	}

	public bool SetAwardDieDia(List<int> X, List<int> Y)
	{
		int randomInt = Tools.getRandomInt(X[0], X[1]);
		int randomInt2 = Tools.getRandomInt(Y[0], Y[1]);
		for (int i = randomInt - 1; i <= randomInt + 1; i++)
		{
			if (i < 0 || i >= Wide)
			{
				continue;
			}
			for (int j = randomInt2 - 1; j <= randomInt2 + 1; j++)
			{
				if (j >= 0 && j < High && map[i, j] != 0)
				{
					return false;
				}
			}
		}
		map[randomInt, randomInt2] = 4;
		return true;
	}

	public int getEmptyBian()
	{
		if (IsEmpty(FangXiang.Top))
		{
			return 1;
		}
		if (IsEmpty(FangXiang.Down))
		{
			return 2;
		}
		if (IsEmpty(FangXiang.Left))
		{
			return 3;
		}
		if (IsEmpty(FangXiang.Right))
		{
			return 4;
		}
		return -1;
	}

	public void getBian(FangXiang fangXiang, List<int> X, List<int> Y)
	{
		switch (fangXiang)
		{
		case FangXiang.Down:
		{
			for (int k = 0; k < 2; k++)
			{
				Y.Add(k);
			}
			X.Add(2);
			X.Add(Wide - 1 - 2);
			break;
		}
		case FangXiang.Top:
		{
			for (int j = High - 2; j < High; j++)
			{
				Y.Add(j);
			}
			X.Add(2);
			X.Add(Wide - 1 - 2);
			break;
		}
		case FangXiang.Left:
		{
			for (int l = 0; l < 2; l++)
			{
				X.Add(l);
			}
			Y.Add(2);
			Y.Add(High - 1 - 2);
			break;
		}
		case FangXiang.Right:
		{
			for (int i = Wide - 2; i < Wide; i++)
			{
				X.Add(i);
			}
			Y.Add(2);
			Y.Add(High - 1 - 2);
			break;
		}
		}
	}

	public bool IsEmpty(FangXiang fangXiang)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		switch (fangXiang)
		{
		case FangXiang.Down:
			num = 0;
			num2 = 1;
			num3 = Wide;
			break;
		case FangXiang.Top:
			num = High - 1;
			num2 = High - 2;
			num3 = Wide;
			break;
		case FangXiang.Left:
			num = 0;
			num2 = 1;
			num3 = High;
			break;
		case FangXiang.Right:
			num = Wide - 1;
			num2 = Wide - 2;
			num3 = High;
			break;
		}
		switch (fangXiang)
		{
		case FangXiang.Top:
		case FangXiang.Down:
		{
			for (int j = 0; j < num3; j++)
			{
				if (map[j, num] > 0 || map[j, num2] > 0)
				{
					return false;
				}
			}
			break;
		}
		case FangXiang.Left:
		case FangXiang.Right:
		{
			for (int i = 0; i < num3; i++)
			{
				if (map[num, i] > 0 || map[num2, i] > 0)
				{
					return false;
				}
			}
			break;
		}
		}
		return true;
	}

	public void CreateExitNode()
	{
		int num = 0;
		int randomInt = Tools.getRandomInt(2, High - 1 - 2);
		map[num, randomInt] = 2;
		CreateEntranceNode(randomInt);
	}

	public void CreateEntranceNode(int high)
	{
		int num = 1;
		map[num, high] = 3;
	}

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
}
