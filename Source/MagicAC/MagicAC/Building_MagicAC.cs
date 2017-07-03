using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;

namespace MagicAC
{
    public class Building_MagicAC : Building_TempControl, IWallAttachable
    {
        private const float EfficiencyFalloffSpan = 100f;

        public override void TickRare()
        {
            if (this.compPowerTrader.PowerOn)
            {
                float ambientTemperature = base.AmbientTemperature;
                float num;
                if (ambientTemperature < 20f)
                {
                    num = 1f;
                }
                else if (ambientTemperature > 120f)
                {
                    num = 0f;
                }
                else
                {
                    num = Mathf.InverseLerp(120f, 20f, ambientTemperature);
                }
                float energyLimit = this.compTempControl.Props.energyPerSecond * num * 4.16666651f;

                if (this.compTempControl.targetTemperature < ambientTemperature)
                {
                    energyLimit = energyLimit * -1;
                }

                float num2 = GenTemperature.ControlTemperatureTempChange(base.Position, base.Map, energyLimit, this.compTempControl.targetTemperature);

                bool flag = !Mathf.Approximately(num2, 0f);
                CompProperties_Power props = this.compPowerTrader.Props;
                if (flag)
                {
                    this.GetRoomGroup().Temperature += num2;
                    this.compPowerTrader.PowerOutput = -props.basePowerConsumption;
                }
                else
                {
                    this.compPowerTrader.PowerOutput = -props.basePowerConsumption * this.compTempControl.Props.lowPowerConsumptionFactor;
                }
                this.compTempControl.operatingAtHighPower = flag;
            }
        }
    }
}
