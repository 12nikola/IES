using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class RegularIntervalSchedule : BasicIntervalSchedule
    {

        private List<long> timePoints = new List<long>();

        private DateTime endTime;
        private float timeStep;
     
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public float TimeStep { get => timeStep; set => timeStep = value; }

        public List<long> TimePoints { get => timePoints; set => timePoints = value; }

        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }
        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                RegularIntervalSchedule temp = (RegularIntervalSchedule)x;

                return ((temp.endTime == this.endTime) && (temp.timeStep == this.timeStep) && CompareHelper.CompareLists(temp.TimePoints, this.TimePoints));


            }
            else
            {
                return false;
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
                case ModelCode.REGULARINTERVALSCHEDULE_RTIMEPOINTS:
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_RTIMEPOINTS:
                    prop.SetValue(TimePoints);
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    prop.SetValue(endTime);
                    break;


                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                    prop.SetValue(timeStep);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    endTime = property.AsDateTime();
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                    timeStep = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return TimePoints.Count != 0 || base.IsReferenced;
            }
        }

      

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE:
                    TimePoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE:

                    if (TimePoints.Contains(globalId))
                    {
                        TimePoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }


    }
}
