using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionContral : MonoBehaviour
{
	public Text title;

	public Text Desc;

	public GameObject caijiPlan;

	public Inventory2 inventory2;

	public List<Button> btn;

	public static List<GameObject> OptionContralList = new List<GameObject>();

	public static GameObject GetOptionContral()
	{
		if (OptionContralList.Count == 0)
		{
			List<GameObject> optionContralList = OptionContralList;
			Object obj = Resources.Load("uiPrefab/Option");
			optionContralList.Add(Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null)));
		}
		if ((Object)(object)OptionContralList[0] == (Object)null)
		{
			List<GameObject> optionContralList2 = OptionContralList;
			Object obj2 = Resources.Load("uiPrefab/Option");
			optionContralList2[0] = Object.Instantiate<GameObject>((GameObject)(object)((obj2 is GameObject) ? obj2 : null));
		}
		return OptionContralList[0];
	}

	public void setTextBtn(string text1, int index)
	{
		((Component)((Component)btn[index]).transform.Find("Text")).GetComponent<Text>().text = text1;
		if (text1 == "")
		{
			((Component)btn[index]).gameObject.SetActive(false);
		}
		else
		{
			((Component)btn[index]).gameObject.SetActive(true);
		}
	}

	public void SetBtnCell(int index, UnityAction btnEvent)
	{
		((UnityEventBase)btn[index].onClick).RemoveAllListeners();
		((UnityEvent)btn[index].onClick).AddListener(btnEvent);
	}

	private void Start()
	{
		OptionContralList.Add(((Component)this).gameObject);
	}
}
