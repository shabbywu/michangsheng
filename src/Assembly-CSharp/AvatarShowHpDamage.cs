using System;
using System.Collections.Generic;
using UnityEngine;

public class AvatarShowHpDamage : MonoBehaviour
{
	public class FloatTextShowData
	{
		public string text;

		public int type;

		public FloatTextShowData()
		{
		}

		public FloatTextShowData(string text, int type)
		{
			this.text = text;
			this.type = type;
		}
	}

	public Transform ShowPointTransform;

	public GameObject DamageTemp;

	public bool UseCustomOffset;

	public Vector3 CustomOffset;

	public List<FloatTextShowData> SpecialShowList = new List<FloatTextShowData>();

	private void Awake()
	{
		if ((Object)(object)ShowPointTransform == (Object)null)
		{
			ShowPointTransform = ((Component)this).transform;
		}
		MessageMag.Instance.Register(MessageName.MSG_Fight_Effect_Special, OnSpecialHit);
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_Fight_Effect_Special, OnSpecialHit);
	}

	public void OnSpecialHit(MessageData data)
	{
		if (SpecialShowList.Count <= 0)
		{
			return;
		}
		foreach (FloatTextShowData specialShow in SpecialShowList)
		{
			SetText(specialShow.text, specialShow.type);
		}
		SpecialShowList.Clear();
	}

	public virtual void show(int _demage, int type = 0)
	{
		FloatTextShowData floatTextShowData = CalcDamageShow(_demage, type);
		if (floatTextShowData != null)
		{
			SetText(floatTextShowData.text, floatTextShowData.type);
		}
	}

	public virtual void show(string text)
	{
		SetText(text);
	}

	public void SpecialShow(int _demage, int type = 0)
	{
		FloatTextShowData floatTextShowData = CalcDamageShow(_demage, type);
		if (floatTextShowData != null)
		{
			SpecialShowList.Add(floatTextShowData);
		}
	}

	public void SpecialShow(string text)
	{
		FloatTextShowData item = new FloatTextShowData(text, 0);
		SpecialShowList.Add(item);
	}

	public FloatTextShowData CalcDamageShow(int _demage, int type = 0)
	{
		if ((Object)(object)RoundManager.instance == (Object)null)
		{
			Debug.LogError((object)"RoundManager为空，不显示伤害");
			return null;
		}
		if (!RoundManager.instance.IsVirtual)
		{
			string text = "";
			int type2 = 0;
			switch (type)
			{
			case 0:
				if (_demage > 0)
				{
					type2 = 1;
					text = "-" + _demage;
					Debug.Log((object)("伤害:" + text));
				}
				else if (_demage == 0)
				{
					text = "";
				}
				else
				{
					type2 = 2;
					text = "+" + Math.Abs(_demage);
					Debug.Log((object)("加血:" + text));
				}
				break;
			case 1:
				type2 = 3;
				text = "-" + _demage;
				Debug.Log((object)("护盾:" + text));
				break;
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				return new FloatTextShowData(text, type2);
			}
		}
		return null;
	}

	public virtual void SetText(string _demage, int type = 0)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(DamageTemp);
		Vector3 val = default(Vector3);
		if (UseCustomOffset)
		{
			val = CustomOffset + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
		}
		else
		{
			((Vector3)(ref val))._002Ector(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
		}
		obj.transform.position = ShowPointTransform.position + val;
		Transform child = obj.transform.GetChild(type);
		((Component)child).GetComponent<TextMesh>().text = _demage;
		((Component)child).gameObject.SetActive(true);
		Debug.Log((object)$"[{Time.frameCount}] 实例化伤害数字 数字:{_demage} 类型:{type}");
	}
}
