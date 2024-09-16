using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ///
            
            ImportRegularIntervalSchedule();
            ImportRegularTimePoint();
            ImportOutageSchedule();
            ImportIrregularTimePoint();
            ImportSwitchingOperation();
            ImportDisconnector();



            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        private void ImportRegularIntervalSchedule()
        {
            SortedDictionary<string, object> cimRegularIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");
            if (cimRegularIntervalSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimRegularIntervalSchedulePair in cimRegularIntervalSchedules)
                {
                    FTN.RegularIntervalSchedule cimRegularIntervalSchedulePoint = cimRegularIntervalSchedulePair.Value as FTN.RegularIntervalSchedule;

                    ResourceDescription rd = CreateIntervalScheduleResourceDescription(cimRegularIntervalSchedulePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedulePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedulePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateIntervalScheduleResourceDescription(FTN.RegularIntervalSchedule cimRegularIntervalSchedulePoint)
        {
            ResourceDescription rd = null;
            if (cimRegularIntervalSchedulePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULARINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularIntervalSchedulePoint.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimRegularIntervalSchedulePoint, rd);
            }
            return rd;
        }

        private void ImportRegularTimePoint()
        {
            SortedDictionary<string, object> cimRegularTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimRegularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimRegularTimePointPair in cimRegularTimePoints)
                {
                    FTN.RegularTimePoint cimRegularTimePoint = cimRegularTimePointPair.Value as FTN.RegularTimePoint;

                    ResourceDescription rd = CreateRegularTimePointResourceDescription(cimRegularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }


        private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimRegularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimRegularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateRegularTimePointProperties(cimRegularTimePoint, rd, importHelper, report);
            }
            return rd;
        }



        private void ImportOutageSchedule()
        {
            SortedDictionary<string, object> cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
            if (cimOutageSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimOutageSchedulePair in cimOutageSchedules)
                {
                    FTN.OutageSchedule cimOutageSchedulePoint = cimOutageSchedulePair.Value as FTN.OutageSchedule;

                    ResourceDescription rd = CreateOutageScheduleResourceDescription(cimOutageSchedulePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedulePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedulePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateOutageScheduleResourceDescription(FTN.OutageSchedule cimOutageSchedulePoint)
        {
            ResourceDescription rd = null;
            if (cimOutageSchedulePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimOutageSchedulePoint.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedulePoint, rd);
            }
            return rd;
        }


        private void ImportIrregularTimePoint()
        {
            SortedDictionary<string, object> cimSwitchs = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimSwitchs != null)
            {
                foreach (KeyValuePair<string, object> cimSwitchPair in cimSwitchs)
                {
                    FTN.IrregularTimePoint cimSwitch = cimSwitchPair.Value as FTN.IrregularTimePoint;

                    ResourceDescription rd = CreateIrregularTimePointResourceDescription(cimSwitch);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SwitchingOperation ID = ").Append(cimSwitch.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SwitchingOperation ID = ").Append(cimSwitch.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }


        private ResourceDescription CreateIrregularTimePointResourceDescription(FTN.IrregularTimePoint cimSwitchingOperation)
        {
            ResourceDescription rd = null;
            if (cimSwitchingOperation != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSwitchingOperation.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateIrregularTimePointProperties(cimSwitchingOperation, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportSwitchingOperation()
        {
            SortedDictionary<string, object> cimSwitchs = concreteModel.GetAllObjectsOfType("FTN.SwitchingOperation");
            if (cimSwitchs != null)
            {
                foreach (KeyValuePair<string, object> cimSwitchPair in cimSwitchs)
                {
                    FTN.SwitchingOperation cimSwitch = cimSwitchPair.Value as FTN.SwitchingOperation;

                    ResourceDescription rd = CreateSwitchingOperationResourceDescription(cimSwitch);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SwitchingOperation ID = ").Append(cimSwitch.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SwitchingOperation ID = ").Append(cimSwitch.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }


        private ResourceDescription CreateSwitchingOperationResourceDescription(FTN.SwitchingOperation cimSwitchingOperation)
        {
            ResourceDescription rd = null;
            if (cimSwitchingOperation != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCHINGOPERATION, importHelper.CheckOutIndexForDMSType(DMSType.SWITCHINGOPERATION));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSwitchingOperation.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateSwitchingOperationProperties(cimSwitchingOperation, rd, importHelper, report);
            }
            return rd;
        }




        private void ImportDisconnector()
        {
            SortedDictionary<string, object> cimDisconnectors = concreteModel.GetAllObjectsOfType("FTN.Disconnector");
            if (cimDisconnectors != null)
            {
                foreach (KeyValuePair<string, object> cimDisconnectorPair in cimDisconnectors)
                {
                    FTN.Disconnector cimDisconnector = cimDisconnectorPair.Value as FTN.Disconnector;

                    ResourceDescription rd = CreateSwitchResourceDescription(cimDisconnector);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Disconnector ID = ").Append(cimDisconnector.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Disconnector ID = ").Append(cimDisconnector.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }


        private ResourceDescription CreateSwitchResourceDescription(FTN.Disconnector cimDisconnector)
        {
            ResourceDescription rd = null;
            if (cimDisconnector != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DISCONNECTOR, importHelper.CheckOutIndexForDMSType(DMSType.DISCONNECTOR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimDisconnector.ID, gid);

                //populate ResourceDescription
                PowerTransformerConverter.PopulateDisconnectorProperties(cimDisconnector, rd, importHelper, report);
            }
            return rd;
        }

    }
}

