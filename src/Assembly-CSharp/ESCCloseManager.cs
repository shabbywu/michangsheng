using System;
using System.Collections.Generic;
using GetWay;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004CD RID: 1229
public class ESCCloseManager : MonoBehaviour
{
	// Token: 0x1700029F RID: 671
	// (get) Token: 0x0600203C RID: 8252 RVA: 0x00112F68 File Offset: 0x00111168
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

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x0600203D RID: 8253 RVA: 0x0001A71A File Offset: 0x0001891A
	public bool ReadyESC
	{
		get
		{
			return this.cd <= 0f;
		}
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x0001A72C File Offset: 0x0001892C
	public void RegisterClose(IESCClose close)
	{
		this.tmpClose = close;
		base.Invoke("InsideRegisterClose", 0.1f);
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x00112FC8 File Offset: 0x001111C8
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

	// Token: 0x06002040 RID: 8256 RVA: 0x0001A745 File Offset: 0x00018945
	public void UnRegisterClose(IESCClose close)
	{
		if (this.closeList.Contains(close))
		{
			this.closeList.Remove(close);
		}
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x00113018 File Offset: 0x00111218
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

	// Token: 0x06002042 RID: 8258 RVA: 0x00113078 File Offset: 0x00111278
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

	// Token: 0x04001BBD RID: 7101
	private static ESCCloseManager inst;

	// Token: 0x04001BBE RID: 7102
	private List<IESCClose> closeList = new List<IESCClose>();

	// Token: 0x04001BBF RID: 7103
	private IESCClose tmpClose;

	// Token: 0x04001BC0 RID: 7104
	private float cd;
}
