using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000458 RID: 1112
public class AvatarShowHpDamage : MonoBehaviour
{
	// Token: 0x060022EF RID: 8943 RVA: 0x000EE9E4 File Offset: 0x000ECBE4
	private void Awake()
	{
		if (this.ShowPointTransform == null)
		{
			this.ShowPointTransform = base.transform;
		}
		MessageMag.Instance.Register(MessageName.MSG_Fight_Effect_Special, new Action<MessageData>(this.OnSpecialHit));
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x000EEA1B File Offset: 0x000ECC1B
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_Fight_Effect_Special, new Action<MessageData>(this.OnSpecialHit));
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x000EEA38 File Offset: 0x000ECC38
	public void OnSpecialHit(MessageData data)
	{
		if (this.SpecialShowList.Count > 0)
		{
			foreach (AvatarShowHpDamage.FloatTextShowData floatTextShowData in this.SpecialShowList)
			{
				this.SetText(floatTextShowData.text, floatTextShowData.type);
			}
			this.SpecialShowList.Clear();
		}
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x000EEAB0 File Offset: 0x000ECCB0
	public virtual void show(int _demage, int type = 0)
	{
		AvatarShowHpDamage.FloatTextShowData floatTextShowData = this.CalcDamageShow(_demage, type);
		if (floatTextShowData != null)
		{
			this.SetText(floatTextShowData.text, floatTextShowData.type);
		}
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x000EEADB File Offset: 0x000ECCDB
	public virtual void show(string text)
	{
		this.SetText(text, 0);
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x000EEAE8 File Offset: 0x000ECCE8
	public void SpecialShow(int _demage, int type = 0)
	{
		AvatarShowHpDamage.FloatTextShowData floatTextShowData = this.CalcDamageShow(_demage, type);
		if (floatTextShowData != null)
		{
			this.SpecialShowList.Add(floatTextShowData);
		}
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x000EEB10 File Offset: 0x000ECD10
	public void SpecialShow(string text)
	{
		AvatarShowHpDamage.FloatTextShowData item = new AvatarShowHpDamage.FloatTextShowData(text, 0);
		this.SpecialShowList.Add(item);
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x000EEB34 File Offset: 0x000ECD34
	public AvatarShowHpDamage.FloatTextShowData CalcDamageShow(int _demage, int type = 0)
	{
		if (RoundManager.instance == null)
		{
			Debug.LogError("RoundManager为空，不显示伤害");
			return null;
		}
		if (!RoundManager.instance.IsVirtual)
		{
			string text = "";
			int type2 = 0;
			if (type == 0)
			{
				if (_demage > 0)
				{
					type2 = 1;
					text = "-" + _demage;
					Debug.Log("伤害:" + text);
				}
				else if (_demage == 0)
				{
					text = "";
				}
				else
				{
					type2 = 2;
					text = "+" + Math.Abs(_demage);
					Debug.Log("加血:" + text);
				}
			}
			else if (type == 1)
			{
				type2 = 3;
				text = "-" + _demage;
				Debug.Log("护盾:" + text);
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				return new AvatarShowHpDamage.FloatTextShowData(text, type2);
			}
		}
		return null;
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x000EEC0C File Offset: 0x000ECE0C
	public virtual void SetText(string _demage, int type = 0)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.DamageTemp);
		Vector3 vector;
		if (this.UseCustomOffset)
		{
			vector = this.CustomOffset + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
		}
		else
		{
			vector..ctor(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
		}
		gameObject.transform.position = this.ShowPointTransform.position + vector;
		Transform child = gameObject.transform.GetChild(type);
		child.GetComponent<TextMesh>().text = _demage;
		child.gameObject.SetActive(true);
		Debug.Log(string.Format("[{0}] 实例化伤害数字 数字:{1} 类型:{2}", Time.frameCount, _demage, type));
	}

	// Token: 0x04001C26 RID: 7206
	public Transform ShowPointTransform;

	// Token: 0x04001C27 RID: 7207
	public GameObject DamageTemp;

	// Token: 0x04001C28 RID: 7208
	public bool UseCustomOffset;

	// Token: 0x04001C29 RID: 7209
	public Vector3 CustomOffset;

	// Token: 0x04001C2A RID: 7210
	public List<AvatarShowHpDamage.FloatTextShowData> SpecialShowList = new List<AvatarShowHpDamage.FloatTextShowData>();

	// Token: 0x0200139E RID: 5022
	public class FloatTextShowData
	{
		// Token: 0x06007C68 RID: 31848 RVA: 0x000027FC File Offset: 0x000009FC
		public FloatTextShowData()
		{
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x002C3D75 File Offset: 0x002C1F75
		public FloatTextShowData(string text, int type)
		{
			this.text = text;
			this.type = type;
		}

		// Token: 0x040068E4 RID: 26852
		public string text;

		// Token: 0x040068E5 RID: 26853
		public int type;
	}
}
