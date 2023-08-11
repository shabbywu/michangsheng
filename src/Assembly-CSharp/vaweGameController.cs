using System;
using UnityEngine;
using UnityEngine.UI;

public class vaweGameController : MonoBehaviour
{
	[SerializeField]
	private Text pageNumber;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private PageView pageView;

	private void Start()
	{
		pageNumber.text = $"当前页码：0";
		pageView.OnPageChanged = pageChanged;
	}

	private void pageChanged(int index)
	{
		pageNumber.text = $"当前页码：{index.ToString()}";
	}

	public void onClick()
	{
		try
		{
			int index = int.Parse(inputField.text);
			pageView.pageTo(index);
		}
		catch (Exception ex)
		{
			Debug.LogWarning((object)("请输入数字" + ex.ToString()));
		}
	}

	private void Destroy()
	{
		pageView.OnPageChanged = null;
	}
}
