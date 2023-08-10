using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class JieDanHuiHe : MonoBehaviour
{
	public Text text;

	private List<int> jieDanBuff;

	private List<string> jieDanBuffName;

	private void Start()
	{
		jieDanBuff = new List<int> { 4013, 4012, 4011 };
		jieDanBuffName = new List<string>();
		foreach (int item in jieDanBuff)
		{
			jieDanBuffName.Add(_BuffJsonData.DataDict[item].name);
		}
	}

	private void Update()
	{
		Avatar player = PlayerEx.Player;
		for (int i = 0; i < jieDanBuff.Count; i++)
		{
			if (player.buffmag.HasBuff(jieDanBuff[i]))
			{
				text.text = jieDanBuffName[i];
				break;
			}
		}
	}
}
