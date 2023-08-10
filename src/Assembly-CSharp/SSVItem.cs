using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

[RequireComponent(typeof(Button))]
public class SSVItem : MonoBehaviour
{
	[HideInInspector]
	public YSGame.TuJian.SuperScrollView SSV;

	[HideInInspector]
	public List<Dictionary<int, string>> DataList;

	protected int _DataIndex;

	protected Button _button;

	protected Text _text;

	protected bool _needUpdateText;

	protected string _updateTextCache;

	public int DataIndex
	{
		get
		{
			return _DataIndex;
		}
		set
		{
			_DataIndex = value;
			ShowText = DataList[_DataIndex].Values.First();
		}
	}

	public string ShowText
	{
		get
		{
			if ((Object)(object)_text == (Object)null)
			{
				return "";
			}
			return _text.text;
		}
		set
		{
			if ((Object)(object)_text != (Object)null)
			{
				_text.text = value;
				return;
			}
			_updateTextCache = value;
			_needUpdateText = true;
		}
	}

	public virtual void Awake()
	{
		_button = ((Component)this).GetComponent<Button>();
		_text = ((Component)this).GetComponentInChildren<Text>();
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (_needUpdateText && (Object)(object)_text != (Object)null)
		{
			_text.text = _updateTextCache;
			_needUpdateText = false;
		}
	}
}
