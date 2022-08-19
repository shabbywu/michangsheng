using System;
using System.Collections.Generic;
using GetWay;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000351 RID: 849
public class ESCCloseManager : MonoBehaviour
{
	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x000CDFD0 File Offset: 0x000CC1D0
	public static ESCCloseManager Inst
	{
		get
		{
			if (ESCCloseManager.inst == null)
			{
				GameObject gameObject = new GameObject("ESCCloseManager");
				Object.DontDestroyOnLoad(gameObject);
				ESCCloseManager.inst = gameObject.AddComponent<ESCCloseManager>();
				SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
				{
					ESCCloseManager.inst.closeList.Clear();
				};
			}
			return ESCCloseManager.inst;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000CE02D File Offset: 0x000CC22D
	public bool ReadyESC
	{
		get
		{
			return this.cd <= 0f;
		}
	}

	// Token: 0x06001CD7 RID: 7383 RVA: 0x000CE03F File Offset: 0x000CC23F
	public void RegisterClose(IESCClose close)
	{
		this.tmpClose = close;
		base.Invoke("InsideRegisterClose", 0.1f);
	}

	// Token: 0x06001CD8 RID: 7384 RVA: 0x000CE058 File Offset: 0x000CC258
	public void CloseAll()
	{
		for (int i = this.closeList.Count - 1; i >= 0; i--)
		{
			IESCClose iescclose = this.closeList[i];
			if (iescclose != null && iescclose.TryEscClose())
			{
				this.cd = 0.1f;
			}
		}
		UToolTip.Close();
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x000CE0A5 File Offset: 0x000CC2A5
	public void UnRegisterClose(IESCClose close)
	{
		if (this.closeList.Contains(close))
		{
			this.closeList.Remove(close);
		}
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x000CE0C4 File Offset: 0x000CC2C4
	private void InsideRegisterClose()
	{
		if (this.tmpClose != null)
		{
			if (this.closeList.Contains(this.tmpClose))
			{
				this.closeList.Remove(this.tmpClose);
				this.closeList.Add(this.tmpClose);
				return;
			}
			this.closeList.Add(this.tmpClose);
		}
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x000CE124 File Offset: 0x000CC324
	private void Update()
	{
		if (this.cd > 0f)
		{
			this.cd -= Time.deltaTime;
		}
		if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
			MapGetWay.Inst.IsStop = true;
		}
		if (Input.GetKeyUp(27) && this.ReadyESC)
		{
			for (int i = this.closeList.Count - 1; i >= 0; i--)
			{
				IESCClose iescclose = this.closeList[i];
				if (iescclose != null && iescclose.TryEscClose())
				{
					UToolTip.Close();
					this.cd = 0.1f;
					return;
				}
			}
		}
	}

	// Token: 0x04001767 RID: 5991
	private static ESCCloseManager inst;

	// Token: 0x04001768 RID: 5992
	private List<IESCClose> closeList = new List<IESCClose>();

	// Token: 0x04001769 RID: 5993
	private IESCClose tmpClose;

	// Token: 0x0400176A RID: 5994
	private float cd;
}
