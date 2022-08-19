using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EBE RID: 3774
	public class EventDispatcher : MonoBehaviour
	{
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06006AAC RID: 27308 RVA: 0x00293D00 File Offset: 0x00291F00
		// (remove) Token: 0x06006AAD RID: 27309 RVA: 0x00293D38 File Offset: 0x00291F38
		protected virtual event Action<string> onLog;

		// Token: 0x06006AAE RID: 27310 RVA: 0x00293D70 File Offset: 0x00291F70
		protected virtual List<Delegate> GetDelegateListCopy<T>(T evt)
		{
			Type typeFromHandle = typeof(T);
			if (!this.delegates.ContainsKey(typeFromHandle))
			{
				return null;
			}
			return new List<Delegate>(this.delegates[typeFromHandle]);
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x00293DA9 File Offset: 0x00291FA9
		protected virtual void Log(string message)
		{
			if (this.onLog != null)
			{
				this.onLog(message);
			}
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x00293DBF File Offset: 0x00291FBF
		public virtual void AddLog(Action<string> log)
		{
			this.onLog += log;
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x00293DC8 File Offset: 0x00291FC8
		public virtual void RemoveLog(Action<string> log)
		{
			this.onLog -= log;
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x00293DD4 File Offset: 0x00291FD4
		public virtual void AddListener<T>(EventDispatcher.TypedDelegate<T> listener) where T : class
		{
			Type typeFromHandle = typeof(T);
			if (!this.delegates.ContainsKey(typeFromHandle))
			{
				this.delegates.Add(typeFromHandle, new List<Delegate>());
			}
			List<Delegate> list = this.delegates[typeFromHandle];
			if (!list.Contains(listener))
			{
				list.Add(listener);
			}
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x00293E28 File Offset: 0x00292028
		public virtual void RemoveListener<T>(EventDispatcher.TypedDelegate<T> listener) where T : class
		{
			Type typeFromHandle = typeof(T);
			if (this.delegates.ContainsKey(typeFromHandle))
			{
				this.delegates[typeFromHandle].Remove(listener);
				return;
			}
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x00293E64 File Offset: 0x00292064
		public virtual void Raise<T>(T evt) where T : class
		{
			if (evt == null)
			{
				this.Log("Raised a null event");
				return;
			}
			List<Delegate> delegateListCopy = this.GetDelegateListCopy<T>(evt);
			if (delegateListCopy == null || delegateListCopy.Count < 1)
			{
				this.Log("Raised an event with no listeners");
				return;
			}
			for (int i = 0; i < delegateListCopy.Count; i++)
			{
				EventDispatcher.TypedDelegate<T> typedDelegate = delegateListCopy[i] as EventDispatcher.TypedDelegate<T>;
				if (typedDelegate != null)
				{
					try
					{
						typedDelegate(evt);
					}
					catch (Exception ex)
					{
						this.Log(ex.Message);
					}
				}
			}
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x00293EF0 File Offset: 0x002920F0
		public virtual void Raise<T>() where T : class, new()
		{
			this.Raise<T>(Activator.CreateInstance<T>());
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x00293EFD File Offset: 0x002920FD
		public virtual void UnregisterAll()
		{
			this.delegates.Clear();
		}

		// Token: 0x04005A01 RID: 23041
		protected readonly Dictionary<Type, List<Delegate>> delegates = new Dictionary<Type, List<Delegate>>();

		// Token: 0x02001711 RID: 5905
		// (Invoke) Token: 0x06008902 RID: 35074
		public delegate void TypedDelegate<T>(T e) where T : class;
	}
}
