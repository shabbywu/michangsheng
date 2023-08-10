using System.Collections.Generic;
using UnityEngine;

public class UIselect : MonoBehaviour
{
	public UIButton left;

	public UIButton right;

	public EventDelegate leftEvent;

	public EventDelegate rightEvent;

	public List<string> list = new List<string>();

	public UILabel label;

	private int nowIndex;

	public int NowIndex => nowIndex;

	private void Start()
	{
		left.onClick.Add(leftEvent);
		right.onClick.Add(rightEvent);
	}

	public void setIndex(int index)
	{
		nowIndex = index;
		label.text = list[index];
	}

	public void setVoiceLeft()
	{
		clickLeft();
		setVoidSprite();
	}

	public void setVoidSprite()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Transform obj = ((Component)this).transform.Find("percent");
		int num = 0;
		foreach (Transform item in ((Component)obj).transform)
		{
			Transform val = item;
			if (num < nowIndex)
			{
				((Component)val).GetComponent<UISprite>().spriteName = "tiaozheng";
			}
			else
			{
				((Component)val).GetComponent<UISprite>().spriteName = "weixuanzhong";
			}
			num++;
		}
	}

	public void setVoiceRight()
	{
		clickRight();
		setVoidSprite();
	}

	public void clickLeft()
	{
		nowIndex--;
		if (nowIndex < 0)
		{
			nowIndex = list.Count - 1;
		}
		label.text = list[nowIndex];
	}

	public void clickRight()
	{
		nowIndex++;
		if (nowIndex >= list.Count)
		{
			nowIndex = 0;
		}
		label.text = list[nowIndex];
	}
}
