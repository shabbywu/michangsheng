using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EFE RID: 3838
	public class LuaStore : LuaBindingsBase
	{
		// Token: 0x06006C06 RID: 27654 RVA: 0x00297B27 File Offset: 0x00295D27
		protected virtual void Start()
		{
			this.Init();
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x00297B30 File Offset: 0x00295D30
		protected virtual bool Init()
		{
			if (this.initialized)
			{
				return true;
			}
			if (LuaStore.instance == null)
			{
				LuaStore.instance = this;
			}
			else if (LuaStore.instance != this)
			{
				Object.Destroy(base.gameObject);
				return false;
			}
			this.primeTable = DynValue.NewPrimeTable().Table;
			base.transform.parent = null;
			Object.DontDestroyOnLoad(this);
			this.initialized = true;
			return true;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06006C08 RID: 27656 RVA: 0x00297BA0 File Offset: 0x00295DA0
		public virtual Table PrimeTable
		{
			get
			{
				return this.primeTable;
			}
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x00297BA8 File Offset: 0x00295DA8
		public override void AddBindings(LuaEnvironment luaEnv)
		{
			if (!this.Init())
			{
				return;
			}
			Table globals = luaEnv.Interpreter.Globals;
			if (globals == null)
			{
				Debug.LogError("Lua globals table is null");
				return;
			}
			Table table = globals.Get("fungus").Table;
			if (table != null)
			{
				table["store"] = this.primeTable;
				return;
			}
			globals["store"] = this.primeTable;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06006C0A RID: 27658 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public override List<BoundObject> BoundObjects
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04005AE3 RID: 23267
		protected Table primeTable;

		// Token: 0x04005AE4 RID: 23268
		protected bool initialized;

		// Token: 0x04005AE5 RID: 23269
		protected static LuaStore instance;
	}
}
