using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

public class WASDMove : MonoBehaviour
{
	public static WASDMove Inst;

	[HideInInspector]
	public MoveDir MoveDir;

	[HideInInspector]
	public bool IsMoved = true;

	private bool moveFlag;

	private float moveCD;

	public static float waitTime;

	public static bool needWait;

	private void Start()
	{
		Inst = this;
	}

	private void Update()
	{
		if (moveCD < 0f)
		{
			if (Input.GetKey((KeyCode)119))
			{
				MoveDir = MoveDir.Up;
			}
			else if (Input.GetKey((KeyCode)115))
			{
				MoveDir = MoveDir.Down;
			}
			else if (Input.GetKey((KeyCode)97))
			{
				MoveDir = MoveDir.Left;
			}
			else if (Input.GetKey((KeyCode)100))
			{
				MoveDir = MoveDir.Right;
			}
			else
			{
				MoveDir = MoveDir.None;
			}
			if (MoveDir != 0 && CanMove())
			{
				Move();
				if (needWait)
				{
					moveCD = waitTime;
					needWait = false;
				}
				else
				{
					moveCD = 1f;
				}
			}
		}
		else
		{
			moveCD -= Time.deltaTime;
		}
	}

	private void LateUpdate()
	{
		if (IsMoved)
		{
			if (moveFlag)
			{
				IsMoved = false;
				moveFlag = false;
			}
			else
			{
				moveFlag = true;
			}
		}
	}

	public static void DelMoreComponent(GameObject obj)
	{
		WASDMove[] components = obj.GetComponents<WASDMove>();
		if (components != null && components.Length != 0)
		{
			WASDMove[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				Object.DestroyImmediate((Object)(object)array[i]);
			}
		}
	}

	public void Move()
	{
		Dictionary<MoveDir, int> moveIndexs = GetMoveIndexs();
		if (moveIndexs.ContainsKey(MoveDir))
		{
			MapInstComport component = ((Component)((Component)this).transform.Find(moveIndexs[MoveDir].ToString())).GetComponent<MapInstComport>();
			Flowchart componentInChildren = ((Component)component).GetComponentInChildren<Flowchart>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				InitFungus(componentInChildren);
			}
			component.EventRandom();
		}
	}

	private void SetHasVariable(string name, int num, Flowchart flowchart)
	{
		if (flowchart.HasVariable(name))
		{
			flowchart.SetIntegerVariable(name, num);
		}
	}

	private void InitFungus(Flowchart flowchart)
	{
		Avatar player = Tools.instance.getPlayer();
		SetHasVariable("ShenShi", player.shengShi, flowchart);
		SetHasVariable("JinJie", player.level, flowchart);
		SetHasVariable("DunSu", player.dunSu, flowchart);
		SetHasVariable("ZiZhi", player.ZiZhi, flowchart);
		SetHasVariable("WuXin", (int)player.wuXin, flowchart);
		SetHasVariable("ShaQi", (int)player.shaQi, flowchart);
		SetHasVariable("MenPai", player.menPai, flowchart);
		SetHasVariable("ChengHao", player.chengHao, flowchart);
		SetHasVariable("Sex", player.Sex, flowchart);
	}

	public int GetNowIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	public Dictionary<MoveDir, int> GetMoveIndexs()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<MoveDir, int> dictionary = new Dictionary<MoveDir, int>();
		int nowIndex = GetNowIndex();
		MapInstComport component = ((Component)((Component)this).transform.Find(nowIndex.ToString())).GetComponent<MapInstComport>();
		Vector3 localPosition = ((Component)this).transform.Find(nowIndex.ToString()).localPosition;
		foreach (int item in component.nextIndex)
		{
			Vector3 localPosition2 = ((Component)this).transform.Find(item.ToString()).localPosition;
			float num = localPosition2.x - localPosition.x;
			float num2 = localPosition2.y - localPosition.y;
			if (num > 0.2f)
			{
				dictionary.Add(MoveDir.Right, item);
			}
			else if (num < -0.2f)
			{
				dictionary.Add(MoveDir.Left, item);
			}
			else if (num2 > 0.2f)
			{
				dictionary.Add(MoveDir.Up, item);
			}
			else if (num2 < -0.2f)
			{
				dictionary.Add(MoveDir.Down, item);
			}
		}
		return dictionary;
	}

	public bool CanMove()
	{
		return Tools.instance.canClick();
	}
}
