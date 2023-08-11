using System.Collections.Generic;
using UnityEngine;

namespace YSGame.TuJian;

public class InfoPanelBase : MonoBehaviour, ITuJianHyperlink
{
	[HideInInspector]
	public bool NeedRefresh;

	[HideInInspector]
	public List<Dictionary<int, string>> DataList = new List<Dictionary<int, string>>();

	protected int[] hylinkArgs;

	protected bool isOnHyperlink;

	public virtual void Update()
	{
		if (NeedRefresh)
		{
			NeedRefresh = false;
			RefreshPanelData();
		}
	}

	public virtual void OnHyperlink(int[] args)
	{
		isOnHyperlink = true;
		hylinkArgs = args;
	}

	public virtual void RefreshPanelData()
	{
	}

	public virtual void RefreshDataList()
	{
	}
}
