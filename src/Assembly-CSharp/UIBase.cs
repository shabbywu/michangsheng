using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050C RID: 1292
[Serializable]
public class UIBase
{
	// Token: 0x0600214F RID: 8527 RVA: 0x00116208 File Offset: 0x00114408
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

	// Token: 0x06002150 RID: 8528 RVA: 0x001162F8 File Offset: 0x001144F8
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

	// Token: 0x06002151 RID: 8529 RVA: 0x0001B791 File Offset: 0x00019991
	public Transform GetTransform()
	{
		return this._go.transform;
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x0001B79E File Offset: 0x0001999E
	public bool IsNull()
	{
		return this._go == null;
	}

	// Token: 0x04001CD7 RID: 7383
	protected GameObject _go;

	// Token: 0x04001CD8 RID: 7384
	private Dictionary<string, object> _objDict = new Dictionary<string, object>();
}
