using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EE8 RID: 3816
	public class Entity
	{
		// Token: 0x06005BD9 RID: 23513 RVA: 0x000042DD File Offset: 0x000024DD
		public static void clear()
		{
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual object getDefinedProperty(string name)
		{
			return null;
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x00040A03 File Offset: 0x0003EC03
		public void destroy()
		{
			this.detachComponents();
			this.onDestroy();
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDestroy()
		{
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x00040A11 File Offset: 0x0003EC11
		public bool isPlayer()
		{
			return this.id == KBEngineApp.app.entity_id;
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRemoteMethodCall(MemoryStream stream)
		{
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUpdatePropertys(MemoryStream stream)
		{
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onGetBase()
		{
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onGetCell()
		{
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLoseCell()
		{
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onComponentsEnterworld()
		{
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual EntityCall getBaseEntityCall()
		{
			return null;
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual EntityCall getCellEntityCall()
		{
			return null;
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void __init__()
		{
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void callPropertysSetMethods()
		{
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void attachComponents()
		{
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void detachComponents()
		{
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00251F20 File Offset: 0x00250120
		public void baseCall(string methodname, params object[] arguments)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::baseCall(",
					methodname,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG("entity::baseCall:  entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs");
				return;
			}
			Method method = null;
			if (!scriptModule.base_methods.TryGetValue(methodname, out method))
			{
				Dbg.ERROR_MSG(this.className + "::baseCall(" + methodname + "), not found method!");
				return;
			}
			ushort methodUtype = method.methodUtype;
			if (arguments.Length != method.args.Count)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					this.className,
					"::baseCall(",
					methodname,
					"): args(",
					arguments.Length,
					"!= ",
					method.args.Count,
					") size is error!"
				}));
				return;
			}
			EntityCall baseEntityCall = this.getBaseEntityCall();
			baseEntityCall.newCall();
			baseEntityCall.bundle.writeUint16(0);
			baseEntityCall.bundle.writeUint16(methodUtype);
			try
			{
				for (int i = 0; i < method.args.Count; i++)
				{
					if (!method.args[i].isSameType(arguments[i]))
					{
						throw new Exception(string.Concat(new object[]
						{
							"arg",
							i,
							": ",
							method.args[i].ToString()
						}));
					}
					method.args[i].addToStream(baseEntityCall.bundle, arguments[i]);
				}
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::baseCall(method=",
					methodname,
					"): args is error(",
					ex.Message,
					")!"
				}));
				baseEntityCall.bundle = null;
				return;
			}
			baseEntityCall.sendCall(null);
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x0025216C File Offset: 0x0025036C
		public void cellCall(string methodname, params object[] arguments)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG("entity::cellCall:  entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs!");
				return;
			}
			Method method = null;
			if (!scriptModule.cell_methods.TryGetValue(methodname, out method))
			{
				Dbg.ERROR_MSG(this.className + "::cellCall(" + methodname + "), not found method!");
				return;
			}
			ushort methodUtype = method.methodUtype;
			if (arguments.Length != method.args.Count)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"): args(",
					arguments.Length,
					"!= ",
					method.args.Count,
					") size is error!"
				}));
				return;
			}
			EntityCall cellEntityCall = this.getCellEntityCall();
			if (cellEntityCall == null)
			{
				Dbg.ERROR_MSG(this.className + "::cellCall(" + methodname + "): no cell!");
				return;
			}
			cellEntityCall.newCall();
			cellEntityCall.bundle.writeUint16(0);
			cellEntityCall.bundle.writeUint16(methodUtype);
			try
			{
				for (int i = 0; i < method.args.Count; i++)
				{
					if (!method.args[i].isSameType(arguments[i]))
					{
						throw new Exception(string.Concat(new object[]
						{
							"arg",
							i,
							": ",
							method.args[i].ToString()
						}));
					}
					method.args[i].addToStream(cellEntityCall.bundle, arguments[i]);
				}
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::cellCall(",
					methodname,
					"): args is error(",
					ex.Message,
					")!"
				}));
				cellEntityCall.bundle = null;
				return;
			}
			cellEntityCall.sendCall(null);
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x002523D8 File Offset: 0x002505D8
		public void enterWorld()
		{
			this.inWorld = true;
			try
			{
				this.onEnterWorld();
				this.onComponentsEnterworld();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onEnterWorld: error=" + ex.ToString());
			}
			Event.fireOut("onEnterWorld", new object[]
			{
				this
			});
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEnterWorld()
		{
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0025243C File Offset: 0x0025063C
		public void leaveWorld()
		{
			this.inWorld = false;
			try
			{
				this.onLeaveWorld();
				this.onComponentsLeaveworld();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onLeaveWorld: error=" + ex.ToString());
			}
			Event.fireOut("onLeaveWorld", new object[]
			{
				this
			});
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLeaveWorld()
		{
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x002524A0 File Offset: 0x002506A0
		public virtual void enterSpace()
		{
			this.inWorld = true;
			try
			{
				this.onEnterSpace();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onEnterSpace: error=" + ex.ToString());
			}
			Event.fireOut("onEnterSpace", new object[]
			{
				this
			});
			Event.fireOut("set_position", new object[]
			{
				this
			});
			Event.fireOut("set_direction", new object[]
			{
				this
			});
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEnterSpace()
		{
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00252528 File Offset: 0x00250728
		public virtual void leaveSpace()
		{
			this.inWorld = false;
			try
			{
				this.onLeaveSpace();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(this.className + "::onLeaveSpace: error=" + ex.ToString());
			}
			Event.fireOut("onLeaveSpace", new object[]
			{
				this
			});
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLeaveSpace()
		{
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x00040A25 File Offset: 0x0003EC25
		public virtual void onPositionChanged(Vector3 oldValue)
		{
			if (this.isPlayer())
			{
				KBEngineApp.app.entityServerPos(this.position);
			}
			if (this.inWorld)
			{
				Event.fireOut("set_position", new object[]
				{
					this
				});
			}
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUpdateVolatileData()
		{
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00252588 File Offset: 0x00250788
		public virtual void onDirectionChanged(Vector3 oldValue)
		{
			if (this.inWorld)
			{
				this.direction.x = this.direction.x * 360f / 6.2831855f;
				this.direction.y = this.direction.y * 360f / 6.2831855f;
				this.direction.z = this.direction.z * 360f / 6.2831855f;
				Event.fireOut("set_direction", new object[]
				{
					this
				});
				return;
			}
			this.direction = oldValue;
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onControlled(bool isControlled_)
		{
		}

		// Token: 0x04005A28 RID: 23080
		public Vector3 _entityLastLocalPos = new Vector3(0f, 0f, 0f);

		// Token: 0x04005A29 RID: 23081
		public Vector3 _entityLastLocalDir = new Vector3(0f, 0f, 0f);

		// Token: 0x04005A2A RID: 23082
		public int id;

		// Token: 0x04005A2B RID: 23083
		public string className = "";

		// Token: 0x04005A2C RID: 23084
		public Vector3 position = new Vector3(0f, 0f, 0f);

		// Token: 0x04005A2D RID: 23085
		public Vector3 direction = new Vector3(0f, 0f, 0f);

		// Token: 0x04005A2E RID: 23086
		public float velocity;

		// Token: 0x04005A2F RID: 23087
		public bool isOnGround = true;

		// Token: 0x04005A30 RID: 23088
		public object renderObj;

		// Token: 0x04005A31 RID: 23089
		public bool inWorld;

		// Token: 0x04005A32 RID: 23090
		public bool isControlled;

		// Token: 0x04005A33 RID: 23091
		public bool inited;
	}
}
