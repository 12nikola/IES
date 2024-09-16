namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

        #region Populate ResourceDescription
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
                }
            }
        }

        


        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
        {
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL1MULTIPLIER, (short)(cimBasicIntervalSchedule.Value1Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
                if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL2MULTIPLIER, (short)(cimBasicIntervalSchedule.Value2Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
                }
            }
        }

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);
            }
        }


        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
            }
        }

        public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimIrregularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimIrregularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimIrregularIntervalSchedule, rd);
            }
        }

        public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimOutageSchedule, ResourceDescription rd)
        {
            if ((cimOutageSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimOutageSchedule, rd);
            }
        }
        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);


                if (cimSwitch.SwitchingOperationsHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitch.SwitchingOperations.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitch.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitch.ID);
                        report.Report.Append("\" - Failed to set reference to SwitchingOperation: rdfID \"").Append(cimSwitch.SwitchingOperations.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHINGOPERATION, gid));
                }
            }
        }


        public static void PopulateDisconnectorProperties(FTN.Disconnector cimDisconnector, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {

            if ((cimDisconnector != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSwitchProperties(cimDisconnector, rd, importHelper,report);
            }

        }


            public static void PopulateSwitchingOperationProperties(FTN.SwitchingOperation cimSwitchingOperation, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitchingOperation != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimSwitchingOperation, rd);

                if (cimSwitchingOperation.NewStateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_NEWSTATE, (short)GetDMSSwitchState(cimSwitchingOperation.NewState)));
                }
                if (cimSwitchingOperation.OperationTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_OPERATIONTIME, cimSwitchingOperation.OperationTime));
                }

                if (cimSwitchingOperation.OutageScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitchingOperation.OutageSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitchingOperation.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchingOperation.ID);
                        report.Report.Append("\" - Failed to set reference to OutageSchedule: rdfID \"").Append(cimSwitchingOperation.OutageSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE, gid));
                }

            }
        }

        public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint cimIrregularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimIrregularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimIrregularTimePoint, rd);

                if (cimIrregularTimePoint.TimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_TIME, cimIrregularTimePoint.Time));
                }
                if (cimIrregularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE1, cimIrregularTimePoint.Value1));
                }
                if (cimIrregularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE2, cimIrregularTimePoint.Value2));
                }


                if (cimIrregularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimIrregularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimIrregularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimIrregularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimIrregularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_IRINTERVALSCHEDULE, gid));
                }

            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);

                if (cimRegularIntervalSchedule.EndTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_ENDTIME, cimRegularIntervalSchedule.EndTime));
                }
                if (cimRegularIntervalSchedule.TimeStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP, cimRegularIntervalSchedule.TimeStep));
                }
            }
        }


        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQNUM, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VAL1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VAL2, cimRegularTimePoint.Value2));
                }

                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to IntervalSchedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_RINTERVALSCHEDULE, gid));
                }

            }
        }

        #endregion Populate ResourceDescription

        #region Enums convert

        public static SwitchState GetDMSSwitchState(FTN.SwitchState switchState)
        {
            switch (switchState)
            {
                case FTN.SwitchState.close:
                    return SwitchState.close;
                case FTN.SwitchState.open:
                    return SwitchState.open;
                default:
                    return SwitchState.close;
            }
        }

        public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default:
                    return UnitSymbol.A;
            }
        }
        #endregion Enums convert
    }
}
