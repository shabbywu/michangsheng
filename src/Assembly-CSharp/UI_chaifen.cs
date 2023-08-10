using System;
using GUIPackage;
using UnityEngine;

public class UI_chaifen : MonoBehaviour
{
	[SerializeField]
	public UIInput inputNum;

	public item Item;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void reduceLabelNum()
	{
		try
		{
			int num = int.Parse(inputNum.value);
			inputNum.value = string.Concat(num - 1);
			InputOnChenge();
		}
		catch (Exception)
		{
		}
	}

	public void addLabelNum()
	{
		try
		{
			int num = int.Parse(inputNum.value);
			inputNum.value = string.Concat(num + 1);
			InputOnChenge();
		}
		catch (Exception)
		{
		}
	}

	public void InputOnChenge()
	{
		try
		{
			int num = int.Parse(inputNum.value);
			if (num < 1)
			{
				inputNum.value = "1";
			}
			else if (Item.itemNum < num)
			{
				inputNum.value = string.Concat(Item.itemNum);
			}
		}
		catch (Exception)
		{
			inputNum.value = "1";
		}
	}
}
