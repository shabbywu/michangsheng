using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013A3 RID: 5027
	public class LuaStore : LuaBindingsBase
	{
		// Token: 0x060079BA RID: 31162 RVA: 0x00053196 File Offset: 0x00051396
		protected virtual void Start()
		{
			this.Init();
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x002B8DF4 File Offset: 0x002B6FF4
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

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060079BC RID: 31164 RVA: 0x0005319F File Offset: 0x0005139F
		public virtual Table PrimeTable
		{
			get
			{
				return this.primeTable;
			}
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x002B8E64 File Offset: 0x002B7064
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

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060079BE RID: 31166 RVA: 0x0000B171 File Offset: 0x00009371
		public override List<BoundObject> BoundObjects
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400695C RID: 26972
		protected Table primeTable;

		// Token: 0x0400695D RID: 26973
		protected bool initialized;

		// Token: 0x0400695E RID: 26974
		protected static LuaStore instance;
	}
}
