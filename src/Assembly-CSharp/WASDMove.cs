using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x020003D5 RID: 981
public class WASDMove : MonoBehaviour
{
	// Token: 0x06001FDF RID: 8159 RVA: 0x000E0F96 File Offset: 0x000DF196
	private void Start()
	{
		WASDMove.Inst = this;
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
	private void Update()
	{
		if (this.moveCD < 0f)
		{
			if (Input.GetKey(119))
			{
				this.MoveDir = MoveDir.Up;
			}
			else if (Input.GetKey(115))
			{
				this.MoveDir = MoveDir.Down;
			}
			else if (Input.GetKey(97))
			{
				this.MoveDir = MoveDir.Left;
			}
			else if (Input.GetKey(100))
			{
				this.MoveDir = MoveDir.Right;
			}
			else
			{
				this.MoveDir = MoveDir.None;
			}
			if (this.MoveDir != MoveDir.None && this.CanMove())
			{
				this.Move();
				if (WASDMove.needWait)
				{
					this.moveCD = WASDMove.waitTime;
					WASDMove.needWait = false;
					return;
				}
				this.moveCD = 1f;
				return;
			}
		}
		else
		{
			this.moveCD -= Time.deltaTime;
		}
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x000E1059 File Offset: 0x000DF259
	private void LateUpdate()
	{
		if (this.IsMoved)
		{
			if (this.moveFlag)
			{
				this.IsMoved = false;
				this.moveFlag = false;
				return;
			}
			this.moveFlag = true;
		}
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x000E1084 File Offset: 0x000DF284
	public static void DelMoreComponent(GameObject obj)
	{
		WASDMove[] components = obj.GetComponents<WASDMove>();
		if (components != null && components.Length != 0)
		{
			WASDMove[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				Object.DestroyImmediate(array[i]);
			}
		}
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x000E10B8 File Offset: 0x000DF2B8
	public void Move()
	{
		Dictionary<MoveDir, int> moveIndexs = this.GetMoveIndexs();
		if (moveIndexs.ContainsKey(this.MoveDir))
		{
			MapInstComport component = base.transform.Find(moveIndexs[this.MoveDir].ToString()).GetComponent<MapInstComport>();
			Flowchart componentInChildren = component.GetComponentInChildren<Flowchart>();
			if (componentInChildren != null)
			{
				this.InitFungus(componentInChildren);
			}
			component.EventRandom();
		}
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x000E111A File Offset: 0x000DF31A
	private void SetHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x000E1130 File Offset: 0x000DF330
	private void InitFungus(Flowchart flowchart)
	{
		Avatar player = Tools.instance.getPlayer();
		this.SetHasVariable("ShenShi", player.shengShi, flowchart);
		this.SetHasVariable("JinJie", (int)player.level, flowchart);
		this.SetHasVariable("DunSu", player.dunSu, flowchart);
		this.SetHasVariable("ZiZhi", player.ZiZhi, flowchart);
		this.SetHasVariable("WuXin", (int)player.wuXin, flowchart);
		this.SetHasVariable("ShaQi", (int)player.shaQi, flowchart);
		this.SetHasVariable("MenPai", (int)player.menPai, flowchart);
		this.SetHasVariable("ChengHao", player.chengHao, flowchart);
		this.SetHasVariable("Sex", player.Sex, flowchart);
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x00062BA5 File Offset: 0x00060DA5
	public int GetNowIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x000E11EC File Offset: 0x000DF3EC
	public Dictionary<MoveDir, int> GetMoveIndexs()
	{
		Dictionary<MoveDir, int> dictionary = new Dictionary<MoveDir, int>();
		int nowIndex = this.GetNowIndex();
		BaseMapCompont component = base.transform.Find(nowIndex.ToString()).GetComponent<MapInstComport>();
		Vector3 localPosition = base.transform.Find(nowIndex.ToString()).localPosition;
		foreach (int value in component.nextIndex)
		{
			Vector3 localPosition2 = base.transform.Find(value.ToString()).localPosition;
			float num = localPosition2.x - localPosition.x;
			float num2 = localPosition2.y - localPosition.y;
			if (num > 0.2f)
			{
				dictionary.Add(MoveDir.Right, value);
			}
			else if (num < -0.2f)
			{
				dictionary.Add(MoveDir.Left, value);
			}
			else if (num2 > 0.2f)
			{
				dictionary.Add(MoveDir.Up, value);
			}
			else if (num2 < -0.2f)
			{
				dictionary.Add(MoveDir.Down, value);
			}
		}
		return dictionary;
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x000E1300 File Offset: 0x000DF500
	public bool CanMove()
	{
		return Tools.instance.canClick(false, true);
	}

	// Token: 0x040019EA RID: 6634
	public static WASDMove Inst;

	// Token: 0x040019EB RID: 6635
	[HideInInspector]
	public MoveDir MoveDir;

	// Token: 0x040019EC RID: 6636
	[HideInInspector]
	public bool IsMoved = true;

	// Token: 0x040019ED RID: 6637
	private bool moveFlag;

	// Token: 0x040019EE RID: 6638
	private float moveCD;

	// Token: 0x040019EF RID: 6639
	public static float waitTime;

	// Token: 0x040019F0 RID: 6640
	public static bool needWait;
}
