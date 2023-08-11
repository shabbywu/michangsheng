using System.Collections.Generic;
using GetWay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCCloseManager : MonoBehaviour
{
	private static ESCCloseManager inst;

	private List<IESCClose> closeList = new List<IESCClose>();

	private IESCClose tmpClose;

	private float cd;

	public static ESCCloseManager Inst
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			if ((Object)(object)inst == (Object)null)
			{
				GameObject val = new GameObject("ESCCloseManager");
				Object.DontDestroyOnLoad((Object)val);
				inst = val.AddComponent<ESCCloseManager>();
				SceneManager.activeSceneChanged += delegate
				{
					inst.closeList.Clear();
				};
			}
			return inst;
		}
	}

	public bool ReadyESC => cd <= 0f;

	public void RegisterClose(IESCClose close)
	{
		tmpClose = close;
		((MonoBehaviour)this).Invoke("InsideRegisterClose", 0.1f);
	}

	public void CloseAll()
	{
		for (int num = closeList.Count - 1; num >= 0; num--)
		{
			IESCClose iESCClose = closeList[num];
			if (iESCClose != null && iESCClose.TryEscClose())
			{
				cd = 0.1f;
			}
		}
		UToolTip.Close();
	}

	public void UnRegisterClose(IESCClose close)
	{
		if (closeList.Contains(close))
		{
			closeList.Remove(close);
		}
	}

	private void InsideRegisterClose()
	{
		if (tmpClose != null)
		{
			if (closeList.Contains(tmpClose))
			{
				closeList.Remove(tmpClose);
				closeList.Add(tmpClose);
			}
			else
			{
				closeList.Add(tmpClose);
			}
		}
	}

	private void Update()
	{
		if (cd > 0f)
		{
			cd -= Time.deltaTime;
		}
		if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
			MapGetWay.Inst.IsStop = true;
		}
		if (!Input.GetKeyUp((KeyCode)27) || !ReadyESC)
		{
			return;
		}
		for (int num = closeList.Count - 1; num >= 0; num--)
		{
			IESCClose iESCClose = closeList[num];
			if (iESCClose != null && iESCClose.TryEscClose())
			{
				UToolTip.Close();
				cd = 0.1f;
				break;
			}
		}
	}
}
