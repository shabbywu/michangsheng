using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200135B RID: 4955
	public class EventDispatcher : MonoBehaviour
	{
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06007843 RID: 30787 RVA: 0x002B6244 File Offset: 0x002B4444
		// (remove) Token: 0x06007844 RID: 30788 RVA: 0x002B627C File Offset: 0x002B447C
		protected virtual event Action<string> onLog;

		// Token: 0x06007845 RID: 30789 RVA: 0x002B62B4 File Offset: 0x002B44B4
		protected virtual List<Delegate> GetDelegateListCopy<T>(T evt)
		{
			Type typeFromHandle = typeof(T);
			if (!this.delegates.ContainsKey(typeFromHandle))
			{
				return null;
			}
			return new List<Delegate>(this.delegates[typeFromHandle]);
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x00051BCC File Offset: 0x0004FDCC
		protected virtual void Log(string message)
		{
			if (this.onLog != null)
			{
				this.onLog(message);
			}
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x00051BE2 File Offset: 0x0004FDE2
		public virtual void AddLog(Action<string> log)
		{
			this.onLog += log;
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x00051BEB File Offset: 0x0004FDEB
		public virtual void RemoveLog(Action<string> log)
		{
			this.onLog -= log;
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x002B62F0 File Offset: 0x002B44F0
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

		// Token: 0x0600784A RID: 30794 RVA: 0x002B6344 File Offset: 0x002B4544
		public virtual void RemoveListener<T>(EventDispatcher.TypedDelegate<T> listener) where T : class
		{
			Type typeFromHandle = typeof(T);
			if (this.delegates.ContainsKey(typeFromHandle))
			{
				this.delegates[typeFromHandle].Remove(listener);
				return;
			}
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x002B6380 File Offset: 0x002B4580
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

		// Token: 0x0600784C RID: 30796 RVA: 0x00051BF4 File Offset: 0x0004FDF4
		public virtual void Raise<T>() where T : class, new()
		{
			this.Raise<T>(Activator.CreateInstance<T>());
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x00051C01 File Offset: 0x0004FE01
		public virtual void UnregisterAll()
		{
			this.delegates.Clear();
		}

		// Token: 0x04006860 RID: 26720
		protected readonly Dictionary<Type, List<Delegate>> delegates = new Dictionary<Type, List<Delegate>>();

		// Token: 0x0200135C RID: 4956
		// (Invoke) Token: 0x06007850 RID: 30800
		public delegate void TypedDelegate<T>(T e) where T : class;
	}
}
