using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B38 RID: 2872
	public abstract class CDKBase : Entity
	{
		// Token: 0x060050A5 RID: 20645 RVA: 0x00220682 File Offset: 0x0021E882
		public CDKBase()
		{
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0022068A File Offset: 0x0021E88A
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_CDKBase(this.id, this.className);
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x002206A3 File Offset: 0x0021E8A3
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_CDKBase(this.id, this.className);
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x002206BC File Offset: 0x0021E8BC
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x002206C5 File Offset: 0x0021E8C5
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x002206CD File Offset: 0x0021E8CD
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x002206D8 File Offset: 0x0021E8D8
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00220758 File Offset: 0x0021E958
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDK"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				switch (property.properUtype)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				}
			}
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00220888 File Offset: 0x0021EA88
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["CDK"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property2 = idpropertys[1];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x04004F77 RID: 20343
		public EntityBaseEntityCall_CDKBase baseEntityCall;

		// Token: 0x04004F78 RID: 20344
		public EntityCellEntityCall_CDKBase cellEntityCall;
	}
}
