﻿using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier value1Multiplier;
        private UnitSymbol value1Unit;
        private UnitMultiplier value2Multiplier;
        private UnitSymbol value2Unit;

        public DateTime StartTime { get => startTime; set => startTime = value; }
        public UnitMultiplier Value1Multiplier { get => value1Multiplier; set => value1Multiplier = value; }
        public UnitSymbol Value1Unit { get => value1Unit; set => value1Unit = value; }
        public UnitMultiplier Value2Multiplier { get => value2Multiplier; set => value2Multiplier = value; }
        public UnitSymbol Value2Unit { get => value2Unit; set => value2Unit = value; }

        public BasicIntervalSchedule(long globalId) : base(globalId)
        {
        }
        


        public override bool Equals(object x)
        {
            if (Object.ReferenceEquals(x, null))
            {
                return false;
            }
            else
            {
                BasicIntervalSchedule bs = (BasicIntervalSchedule)x;
                return ((bs.StartTime == this.StartTime) && (bs.Value1Multiplier == this.Value1Multiplier) &&
                        (bs.Value1Unit == this.Value1Unit) && (bs.Value2Multiplier == this.Value2Multiplier) && (bs.Value2Unit == this.Value2Unit));


            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }




        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    property.SetValue(StartTime);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTIPLIER:
                    property.SetValue((short)Value1Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                    property.SetValue((short)Value1Unit);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTIPLIER:
                    property.SetValue((short)Value2Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    property.SetValue((short)Value2Unit);
                    break;


                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    StartTime = property.AsDateTime();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTIPLIER:
                    Value1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                    Value1Unit = (UnitSymbol)property.AsEnum();
                    break;
                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTIPLIER:
                    Value2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    Value2Unit = (UnitSymbol)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

    }
}
