using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;

namespace MagicAC
{
    class Building_MagicACWall : Building_TempControl
    {
        public override void TickRare()
        {
            if (this.compPowerTrader.PowerOn)
            {
                IntVec3 vecNorth = base.Position + IntVec3.North.RotatedBy(base.Rotation);
                bool hotIsHot = false;
                Room roomNorth = vecNorth.GetRoom(this.Map);
                if (!vecNorth.Impassable(base.Map) && (roomNorth != null))
                {
                    var temperature = roomNorth.Temperature;
                    float energyMod;
                    if (temperature < 20f)
                    {
                        energyMod = 1f;
                    }
                    else
                    {
                        energyMod = temperature > 120f
                            ? 0f
                            : Mathf.InverseLerp(120f, 20f, temperature);
                    }
                    var energyLimit = compTempControl.Props.energyPerSecond * energyMod * 4.16666651f;

                    if (temperature > this.compTempControl.targetTemperature)
                    {
                        energyLimit *= -1;
                    }

                    var hotAir = GenTemperature.ControlTemperatureTempChange(vecNorth, this.Map, energyLimit,
                                                          compTempControl.targetTemperature);

                    hotIsHot = !Mathf.Approximately(hotAir, 0f);
                    if (hotIsHot)
                    {
                        vecNorth.GetRoomGroup(base.Map).Temperature += hotAir;
                    }
                }
                CompProperties_Power props = this.compPowerTrader.Props;
                if (hotIsHot)
                {
                    this.compPowerTrader.PowerOutput = -props.basePowerConsumption;
                }
                else
                {
                    this.compPowerTrader.PowerOutput = -props.basePowerConsumption * this.compTempControl.Props.lowPowerConsumptionFactor;
                }
                this.compTempControl.operatingAtHighPower = hotIsHot;
            }
        }
    }
}
