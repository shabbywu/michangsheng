using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x02000570 RID: 1392
public class WASDMove : MonoBehaviour
{
	// Token: 0x0600235F RID: 9055 RVA: 0x0001CA1F File Offset: 0x0001AC1F
	private void Start()
	{
		WASDMove.Inst = this;
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x00123AC4 File Offset: 0x00121CC4
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

	// Token: 0x06002361 RID: 9057 RVA: 0x0001CA27 File Offset: 0x0001AC27
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

	// Token: 0x06002362 RID: 9058 RVA: 0x00123B80 File Offset: 0x00121D80
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

	// Token: 0x06002363 RID: 9059 RVA: 0x00123BB4 File Offset: 0x00121DB4
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

	// Token: 0x06002364 RID: 9060 RVA: 0x0001CA4F File Offset: 0x0001AC4F
	private void SetHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x00123C18 File Offset: 0x00121E18
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

	// Token: 0x06002366 RID: 9062 RVA: 0x00011F27 File Offset: 0x00010127
	public int GetNowIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x00123CD4 File Offset: 0x00121ED4
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

	// Token: 0x06002368 RID: 9064 RVA: 0x0001CA62 File Offset: 0x0001AC62
	public bool CanMove()
	{
		return Tools.instance.canClick(false, true);
	}

	// Token: 0x04001E7C RID: 7804
	public static WASDMove Inst;

	// Token: 0x04001E7D RID: 7805
	[HideInInspector]
	public MoveDir MoveDir;

	// Token: 0x04001E7E RID: 7806
	[HideInInspector]
	public bool IsMoved = true;

	// Token: 0x04001E7F RID: 7807
	private bool moveFlag;

	// Token: 0x04001E80 RID: 7808
	private float moveCD;

	// Token: 0x04001E81 RID: 7809
	public static float waitTime;

	// Token: 0x04001E82 RID: 7810
	public static bool needWait;
}
