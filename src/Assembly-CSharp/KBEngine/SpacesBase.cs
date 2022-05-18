using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FF0 RID: 4080
	public abstract class SpacesBase : Entity
	{
		// Token: 0x060060A4 RID: 24740 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x000430C3 File Offset: 0x000412C3
		public SpacesBase()
		{
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x000430DE File Offset: 0x000412DE
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_SpacesBase(this.id, this.className);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x000430F7 File Offset: 0x000412F7
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_SpacesBase(this.id, this.className);
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x00043110 File Offset: 0x00041310
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x00043119 File Offset: 0x00041319
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x00043121 File Offset: 0x00041321
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x0026A3C8 File Offset: 0x002685C8
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
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

		// Token: 0x060060B4 RID: 24756 RVA: 0x0026A448 File Offset: 0x00268648
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Spaces"];
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
				ushort properUtype2 = property.properUtype;
				switch (properUtype2)
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
				default:
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue);
						}
						break;
					}
					case 41004:
					{
						uint oldValue2 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue2);
						}
						break;
					}
					case 41005:
					{
						uint oldValue3 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue3);
						}
						break;
					}
					case 41006:
					{
						uint oldValue4 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue4);
						}
						break;
					}
					case 41007:
					{
						byte oldValue5 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue5);
						}
						break;
					}
					}
					break;
				}
			}
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x0026A71C File Offset: 0x0026891C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Spaces"].idpropertys;
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
			uint oldValue = this.modelID;
			Property property2 = idpropertys[4];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue);
			}
			byte oldValue2 = this.modelScale;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue2);
			}
			string oldValue3 = this.name;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue3);
			}
			Vector3 position = this.position;
			Property property5 = idpropertys[1];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue4 = this.uid;
			Property property6 = idpropertys[7];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue4);
			}
			uint oldValue5 = this.utype;
			Property property7 = idpropertys[8];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue5);
					return;
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue5);
			}
		}

		// Token: 0x04005BD2 RID: 23506
		public EntityBaseEntityCall_SpacesBase baseEntityCall;

		// Token: 0x04005BD3 RID: 23507
		public EntityCellEntityCall_SpacesBase cellEntityCall;

		// Token: 0x04005BD4 RID: 23508
		public uint modelID;

		// Token: 0x04005BD5 RID: 23509
		public byte modelScale = 30;

		// Token: 0x04005BD6 RID: 23510
		public string name = "";

		// Token: 0x04005BD7 RID: 23511
		public uint uid;

		// Token: 0x04005BD8 RID: 23512
		public uint utype;
	}
}
