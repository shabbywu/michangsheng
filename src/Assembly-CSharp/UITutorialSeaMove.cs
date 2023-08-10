using System.Collections.Generic;
using UnityEngine;

public class UITutorialSeaMove : MonoBehaviour
{
	public static UITutorialSeaMove Inst;

	public GameObject ScaleObj;

	private bool isShow;

	private List<KeyCode> triggerKeyList = new List<KeyCode>
	{
		(KeyCode)119,
		(KeyCode)97,
		(KeyCode)115,
		(KeyCode)100,
		(KeyCode)32
	};

	private void Awake()
	{
		Inst = this;
	}

	private void Update()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!isShow)
		{
			return;
		}
		foreach (KeyCode triggerKey in triggerKeyList)
		{
			if (Input.GetKeyDown(triggerKey))
			{
				Close();
			}
		}
	}

	public void Show()
	{
		ScaleObj.SetActive(true);
		isShow = true;
	}

	public void Close()
	{
		ScaleObj.SetActive(false);
		isShow = false;
	}
}
