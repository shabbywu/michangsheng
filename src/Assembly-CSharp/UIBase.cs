using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000388 RID: 904
[Serializable]
public class UIBase
{
	// Token: 0x06001DD6 RID: 7638 RVA: 0x000D24FC File Offset: 0x000D06FC
	protected T Get<T>(string path)
	{
		if (this._go == null)
		{
			Debug.LogError("_go对象为空，无法获取组件");
			return default(T);
		}
		string key = path + "_" + typeof(T).Name;
		if (!this._objDict.ContainsKey(key))
		{
			Transform transform = this._go.transform.Find(path);
			if (transform == null)
			{
				Debug.LogError("不存在该对象,路径:" + path);
				return default(T);
			}
			T component = transform.GetComponent<T>();
			if (component == null)
			{
				Debug.LogError("不存在该组件" + typeof(T).Name + ",路径:" + path);
				return default(T);
			}
			this._objDict.Add(key, component);
		}
		return (T)((object)this._objDict[key]);
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x000D25EC File Offset: 0x000D07EC
	protected GameObject Get(string path, bool showError = true)
	{
		if (this._go == null)
		{
			if (showError)
			{
				Debug.LogError("_go对象为空，无法获取组件");
			}
			return null;
		}
		string key = path + "_GameObject";
		if (!this._objDict.ContainsKey(key))
		{
			Transform transform = this._go.transform.Find(path);
			if (transform == null)
			{
				if (showError)
				{
					Debug.LogError("不存在该对象,路径:" + path);
				}
				return null;
			}
			this._objDict.Add(key, transform.gameObject);
		}
		return (GameObject)this._objDict[key];
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x000D2683 File Offset: 0x000D0883
	public Transform GetTransform()
	{
		return this._go.transform;
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x000D2690 File Offset: 0x000D0890
	public bool IsNull()
	{
		return this._go == null;
	}

	// Token: 0x04001879 RID: 6265
	protected GameObject _go;

	// Token: 0x0400187A RID: 6266
	private Dictionary<string, object> _objDict = new Dictionary<string, object>();
}
