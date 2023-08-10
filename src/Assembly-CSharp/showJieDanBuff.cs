using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

public class showJieDanBuff : MonoBehaviour
{
	public List<int> showBuffID = new List<int>();

	public List<int> RoundBuffID;

	public Animator animator;

	private string splName = "RecipeJiedan";

	private int buffcount;

	private int RoundBuffcount;

	private float buffPalyTime = 1f;

	private void Start()
	{
		for (int i = 4014; i <= 4027; i++)
		{
			showBuffID.Add(i);
		}
		RoundBuffID = new List<int> { 4011, 4012, 4013 };
	}

	public void playMoveAdd(GameObject obj)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		iTween.MoveTo(obj.gameObject, iTween.Hash(new object[10]
		{
			"x",
			obj.transform.localPosition.x,
			"y",
			obj.transform.localPosition.y,
			"z",
			obj.transform.localPosition.z,
			"time",
			buffPalyTime,
			"islocal",
			true
		}));
		iTween.ScaleTo(obj.gameObject, iTween.Hash(new object[10] { "x", 1, "y", 1, "z", 1, "time", buffPalyTime, "islocal", true }));
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.zero;
	}

	public void playMoveRemove(GameObject obj)
	{
		iTween.MoveTo(obj.gameObject, iTween.Hash(new object[10] { "x", 0, "y", 0, "z", 0, "time", buffPalyTime, "islocal", true }));
		iTween.ScaleTo(obj.gameObject, iTween.Hash(new object[10] { "x", 0, "y", 0, "z", 0, "time", buffPalyTime, "islocal", true }));
	}

	private void Update()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		int showBuffCount = 0;
		player.bufflist.ForEach(delegate(List<int> _aa)
		{
			if (showBuffID.Contains(_aa[2]))
			{
				showBuffCount++;
			}
		});
		int _RoundBuffCount = 0;
		player.bufflist.ForEach(delegate(List<int> _aa)
		{
			if (RoundBuffID.Contains(_aa[2]))
			{
				_RoundBuffCount++;
			}
		});
		if (buffcount != showBuffCount)
		{
			buffcount = showBuffCount;
			List<int> list = new List<int>();
			foreach (Transform item in ((Component)this).transform)
			{
				UIFightBuffItem component = ((Component)item).GetComponent<UIFightBuffItem>();
				if (((Object)((Component)item).gameObject).name == getSqlName() && component.BuffID != 0)
				{
					list.Add(component.BuffID);
				}
			}
			foreach (List<int> item2 in player.bufflist)
			{
				if (!showBuffID.Contains(item2[2]))
				{
					continue;
				}
				if (list.Contains(item2[2]))
				{
					list.Remove(item2[2]);
					continue;
				}
				int notShow = getNotShow(item2[2]);
				if (notShow == -1)
				{
					break;
				}
				UIFightBuffItem component2 = ((Component)((Component)this).transform.GetChild(notShow)).GetComponent<UIFightBuffItem>();
				component2.BuffID = item2[2];
				component2.BuffRound = item2[1];
				component2.AvatarBuff = item2;
				int num = (int)jsonData.instance.BuffJsonData[string.Concat(item2[2])]["BuffIcon"].n;
				Transform obj = ((Component)component2).transform.Find("Image");
				((Component)obj).gameObject.SetActive(true);
				Sprite sprite = ((num != 0) ? ResManager.inst.LoadSprite("Buff Icon/" + num) : ResManager.inst.LoadSprite("Buff Icon/" + item2[2]));
				((Component)obj).GetComponent<Image>().sprite = sprite;
				if (JieDanManager.instence.jieDanBuff.Contains(item2[2]))
				{
					animator.Play(string.Concat(item2[2] - 4021));
				}
			}
		}
		if (RoundBuffcount == _RoundBuffCount)
		{
			return;
		}
		RoundBuffcount = _RoundBuffCount;
		animator.SetInteger("ShowType", RoundBuffcount);
		foreach (Transform item3 in ((Component)this).transform)
		{
			Transform val2 = item3;
			if (((Component)val2).gameObject.activeSelf)
			{
				playMoveRemove(((Component)val2).gameObject);
				Object.Destroy((Object)(object)((Component)val2).gameObject, buffPalyTime);
			}
		}
		foreach (Transform item4 in ((Component)this).transform)
		{
			Transform val3 = item4;
			if (((Object)((Component)val3).gameObject).name == getSqlName())
			{
				((Component)val3).gameObject.SetActive(true);
				playMoveAdd(((Component)val3).gameObject);
			}
		}
	}

	public List<GameObject> getShowObj()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		List<GameObject> list = new List<GameObject>();
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (((Object)((Component)val).gameObject).name == splName + RoundBuffID[RoundBuffcount - 1])
			{
				list.Add(((Component)val).gameObject);
			}
		}
		return list;
	}

	public List<GameObject> getNotShowObj()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		List<GameObject> list = new List<GameObject>();
		foreach (Transform item in ((Component)this).transform)
		{
			Transform val = item;
			if (((Object)((Component)val).gameObject).name != getSqlName())
			{
				list.Add(((Component)val).gameObject);
			}
		}
		return list;
	}

	public string getSqlName()
	{
		return splName + RoundBuffID[RoundBuffcount - 1];
	}

	public int getNotShow(int BuffID)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		if (RoundBuffcount == 3)
		{
			foreach (Transform item in ((Component)this).transform)
			{
				UIFightBuffItem component = ((Component)item).GetComponent<UIFightBuffItem>();
				if (((Object)((Component)item).gameObject).name == getSqlName() && component.BuffID == 0)
				{
					return num;
				}
				num++;
			}
		}
		foreach (Transform item2 in ((Component)this).transform)
		{
			UI_JieDanBuff component2 = ((Component)item2).GetComponent<UI_JieDanBuff>();
			if (((Object)((Component)item2).gameObject).name == getSqlName() && component2.jiedanID == BuffID)
			{
				return num;
			}
			num++;
		}
		return -1;
	}
}
