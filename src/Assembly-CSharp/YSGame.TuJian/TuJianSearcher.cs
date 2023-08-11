using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class TuJianSearcher : MonoBehaviour
{
	public static TuJianSearcher Inst;

	public InputField input;

	private string[] searchStrs;

	public int SearchCount
	{
		get
		{
			if (searchStrs == null)
			{
				return 0;
			}
			return searchStrs.Length;
		}
	}

	private void Awake()
	{
		Inst = this;
	}

	private void Start()
	{
		input.text = "";
		((UnityEventBase)input.onValueChanged).RemoveAllListeners();
		((UnityEvent<string>)(object)input.onValueChanged).AddListener((UnityAction<string>)Search);
	}

	public void Search(string str)
	{
		searchStrs = str.Split(new char[1] { ' ' });
		TuJianManager.Inst.NeedRefreshDataList = true;
		TuJianManager.TabDict[TuJianManager.Inst.NowTuJianTab].RefreshPanel();
	}

	public void ClearSearchStrAndNoSearch()
	{
		searchStrs = null;
	}

	public bool IsContansSearch(string str)
	{
		if (searchStrs == null || searchStrs.Length == 0)
		{
			return true;
		}
		string[] array = searchStrs;
		foreach (string value in array)
		{
			if (str.Contains(value))
			{
				return true;
			}
		}
		return false;
	}
}
