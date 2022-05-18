using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

// Token: 0x02000628 RID: 1576
public class showJieDanBuff : MonoBehaviour
{
	// Token: 0x06002728 RID: 10024 RVA: 0x00132980 File Offset: 0x00130B80
	private void Start()
	{
		for (int i = 4014; i <= 4027; i++)
		{
			this.showBuffID.Add(i);
		}
		this.RoundBuffID = new List<int>
		{
			4011,
			4012,
			4013
		};
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x001329DC File Offset: 0x00130BDC
	public void playMoveAdd(GameObject obj)
	{
		iTween.MoveTo(obj.gameObject, iTween.Hash(new object[]
		{
			"x",
			obj.transform.localPosition.x,
			"y",
			obj.transform.localPosition.y,
			"z",
			obj.transform.localPosition.z,
			"time",
			this.buffPalyTime,
			"islocal",
			true
		}));
		iTween.ScaleTo(obj.gameObject, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"z",
			1,
			"time",
			this.buffPalyTime,
			"islocal",
			true
		}));
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.zero;
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x00132B1C File Offset: 0x00130D1C
	public void playMoveRemove(GameObject obj)
	{
		iTween.MoveTo(obj.gameObject, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			0,
			"z",
			0,
			"time",
			this.buffPalyTime,
			"islocal",
			true
		}));
		iTween.ScaleTo(obj.gameObject, iTween.Hash(new object[]
		{
			"x",
			0,
			"y",
			0,
			"z",
			0,
			"time",
			this.buffPalyTime,
			"islocal",
			true
		}));
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x00132C10 File Offset: 0x00130E10
	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		int showBuffCount = 0;
		player.bufflist.ForEach(delegate(List<int> _aa)
		{
			if (this.showBuffID.Contains(_aa[2]))
			{
				int showBuffCount = showBuffCount;
				showBuffCount++;
			}
		});
		int _RoundBuffCount = 0;
		player.bufflist.ForEach(delegate(List<int> _aa)
		{
			if (this.RoundBuffID.Contains(_aa[2]))
			{
				int roundBuffCount = _RoundBuffCount;
				_RoundBuffCount = roundBuffCount + 1;
			}
		});
		if (this.buffcount != showBuffCount)
		{
			this.buffcount = showBuffCount;
			List<int> list = new List<int>();
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				UIFightBuffItem component = transform.GetComponent<UIFightBuffItem>();
				if (transform.gameObject.name == this.getSqlName() && component.BuffID != 0)
				{
					list.Add(component.BuffID);
				}
			}
			foreach (List<int> list2 in player.bufflist)
			{
				if (this.showBuffID.Contains(list2[2]))
				{
					if (list.Contains(list2[2]))
					{
						list.Remove(list2[2]);
					}
					else
					{
						int notShow = this.getNotShow(list2[2]);
						if (notShow == -1)
						{
							break;
						}
						UIFightBuffItem component2 = base.transform.GetChild(notShow).GetComponent<UIFightBuffItem>();
						component2.BuffID = list2[2];
						component2.BuffRound = list2[1];
						component2.AvatarBuff = list2;
						int num = (int)jsonData.instance.BuffJsonData[string.Concat(list2[2])]["BuffIcon"].n;
						Transform transform2 = component2.transform.Find("Image");
						transform2.gameObject.SetActive(true);
						Sprite sprite;
						if (num == 0)
						{
							sprite = ResManager.inst.LoadSprite("Buff Icon/" + list2[2]);
						}
						else
						{
							sprite = ResManager.inst.LoadSprite("Buff Icon/" + num);
						}
						transform2.GetComponent<Image>().sprite = sprite;
						if (JieDanManager.instence.jieDanBuff.Contains(list2[2]))
						{
							this.animator.Play(string.Concat(list2[2] - 4021));
						}
					}
				}
			}
		}
		if (this.RoundBuffcount != _RoundBuffCount)
		{
			this.RoundBuffcount = _RoundBuffCount;
			this.animator.SetInteger("ShowType", this.RoundBuffcount);
			foreach (object obj2 in base.transform)
			{
				Transform transform3 = (Transform)obj2;
				if (transform3.gameObject.activeSelf)
				{
					this.playMoveRemove(transform3.gameObject);
					Object.Destroy(transform3.gameObject, this.buffPalyTime);
				}
			}
			foreach (object obj3 in base.transform)
			{
				Transform transform4 = (Transform)obj3;
				if (transform4.gameObject.name == this.getSqlName())
				{
					transform4.gameObject.SetActive(true);
					this.playMoveAdd(transform4.gameObject);
				}
			}
		}
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x00133008 File Offset: 0x00131208
	public List<GameObject> getShowObj()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.name == this.splName + this.RoundBuffID[this.RoundBuffcount - 1])
			{
				list.Add(transform.gameObject);
			}
		}
		return list;
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x001330A4 File Offset: 0x001312A4
	public List<GameObject> getNotShowObj()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.name != this.getSqlName())
			{
				list.Add(transform.gameObject);
			}
		}
		return list;
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x0001F180 File Offset: 0x0001D380
	public string getSqlName()
	{
		return this.splName + this.RoundBuffID[this.RoundBuffcount - 1];
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x00133124 File Offset: 0x00131324
	public int getNotShow(int BuffID)
	{
		int num = 0;
		if (this.RoundBuffcount == 3)
		{
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				UIFightBuffItem component = transform.GetComponent<UIFightBuffItem>();
				if (transform.gameObject.name == this.getSqlName() && component.BuffID == 0)
				{
					return num;
				}
				num++;
			}
		}
		foreach (object obj2 in base.transform)
		{
			Transform transform2 = (Transform)obj2;
			UI_JieDanBuff component2 = transform2.GetComponent<UI_JieDanBuff>();
			if (transform2.gameObject.name == this.getSqlName() && component2.jiedanID == BuffID)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x04002144 RID: 8516
	public List<int> showBuffID = new List<int>();

	// Token: 0x04002145 RID: 8517
	public List<int> RoundBuffID;

	// Token: 0x04002146 RID: 8518
	public Animator animator;

	// Token: 0x04002147 RID: 8519
	private string splName = "RecipeJiedan";

	// Token: 0x04002148 RID: 8520
	private int buffcount;

	// Token: 0x04002149 RID: 8521
	private int RoundBuffcount;

	// Token: 0x0400214A RID: 8522
	private float buffPalyTime = 1f;
}
