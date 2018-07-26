using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule;
using CDI.StateMachine;

namespace CDI.Zone
{
    public class MotorSafetyCheck
    {
        private ClassWorkZones zones;
        public MotorSafetyCheck(ClassWorkZones WorkZones)
        {
            zones = WorkZones;
        }
        public void AssignMotorSafety()
        {
            zones.WorkZoneNG挑选机械手.AxisSortingPNPY.NeedToCheckSafety = true;
            zones.WorkZoneNG挑选机械手.AxisSortingPNPY.CheckSafetyCallBack = AxisSortingPNPY_CheckSafety;
            zones.WorkZoneNG挑选机械手.AxisSortingPNPY.NeedToCheckHomeSafety = true;
            zones.WorkZoneNG挑选机械手.AxisSortingPNPY.CheckHomeSafetyCallBack = AxisSortingPNPY_CheckSafety;
            zones.WorkZoneNG挑选机械手.AxisSortingPNPZ.NeedToCheckSafety = true;
            zones.WorkZoneNG挑选机械手.AxisSortingPNPZ.CheckSafetyCallBack = AxisSortingPNPZ_CheckSafety;
            zones.WorkZone上料传送.AxisLoadInConveyor.NeedToCheckSafety = true;
            zones.WorkZone上料传送.AxisLoadInConveyor.CheckSafetyCallBack = AxisLoadInConveyor_CheckSafety;
            zones.WorkZone上料机械手.AxisLoadPNPY.NeedToCheckSafety = true;
            zones.WorkZone上料机械手.AxisLoadPNPY.CheckSafetyCallBack = AxisLoadPNPY_CheckSafety;
            zones.WorkZone上料机械手.AxisLoadPNPY.NeedToCheckHomeSafety = true;
            zones.WorkZone上料机械手.AxisLoadPNPY.CheckHomeSafetyCallBack = AxisLoadPNPY_CheckSafety;
            zones.WorkZone上料机械手.AxisLoadPNPZ.NeedToCheckSafety = true;
            zones.WorkZone上料机械手.AxisLoadPNPZ.CheckSafetyCallBack = AxisLoadPNPZ_CheckSafety;
            zones.WorkZone下料传送.AxisUnloadOutConveyor.NeedToCheckSafety = true;
            zones.WorkZone下料传送.AxisUnloadOutConveyor.CheckSafetyCallBack = AxisUnloadOutConveyor_CheckSafety;
            zones.WorkZone下料机械手.AxisUnloadPNPY.NeedToCheckSafety = true;
            zones.WorkZone下料机械手.AxisUnloadPNPY.CheckSafetyCallBack = AxisUnloadPNPY_CheckSafety;
            zones.WorkZone下料机械手.AxisUnloadPNPY.NeedToCheckHomeSafety = true;
            zones.WorkZone下料机械手.AxisUnloadPNPY.CheckHomeSafetyCallBack = AxisUnloadPNPY_CheckSafety;
            zones.WorkZone传送机械手.AxisTransPNPX.NeedToCheckSafety = true;
            zones.WorkZone传送机械手.AxisTransPNPX.CheckSafetyCallBack = AxisTransPNPX_CheckSafety;
            zones.WorkZone厚度测量.AxisThicknessMeasY.NeedToCheckSafety = true;
            zones.WorkZone厚度测量.AxisThicknessMeasY.CheckSafetyCallBack = AxisThicknessMeasY_CheckSafety;
            zones.WorkZone厚度测量.AxisThicknessMeasY.NeedToCheckHomeSafety = true;
            zones.WorkZone厚度测量.AxisThicknessMeasY.CheckHomeSafetyCallBack = AxisThicknessMeasY_CheckSafety;
            zones.WorkZone尺寸测量.AxisOutlineMeasX.NeedToCheckSafety = true;
            zones.WorkZone尺寸测量.AxisOutlineMeasX.CheckSafetyCallBack = AxisOutlineMeasX_CheckSafety;
            zones.WorkZone顶封边定位.AxisTopAlignBottom.NeedToCheckSafety = true;
            zones.WorkZone顶封边定位.AxisTopAlignBottom.CheckSafetyCallBack = AxisTopAlignBottom_CheckSafety;
            zones.WorkZone顶封边定位.AxisTopAlignSide.NeedToCheckSafety = true;
            zones.WorkZone顶封边定位.AxisTopAlignSide.CheckSafetyCallBack = AxisTopAlignSide_CheckSafety;
            zones.WorkZone顶封边定位.AxisTopAlignTop.NeedToCheckSafety = true;
            zones.WorkZone顶封边定位.AxisTopAlignTop.CheckSafetyCallBack = AxisTopAlignTop_CheckSafety;
            zones.WorkZone顶封边定位.AxisTopAlignXSide.NeedToCheckSafety = true;
            zones.WorkZone顶封边定位.AxisTopAlignXSide.CheckSafetyCallBack = AxisTopAlignXSide_CheckSafety;
            zones.WorkZone顶封边定位.AxisTopAlignZClamp.NeedToCheckSafety = true;
            zones.WorkZone顶封边定位.AxisTopAlignZClamp.CheckSafetyCallBack = AxisTopAlignZClamp_CheckSafety;
        }
        private void ShowMotorError(CAxisBase axis, string error)
        {
            ClassErrorHandle.ShowError(axis.Name, "电机复位或移动安全检测失败: " + error, ErrorLevel.Alarm);
        }
        public static bool CheckErrPosTimeOut(Func<bool> action, int timeout = ClassErrorHandle.TIMEOUT)
        {
            bool res;
            TimeClass timer = new TimeClass();
            timer.StartAlarmClock(timeout);
            do
            {
                res = action();
                Application.DoEvents();
            } while (!res && !timer.TimeOut);
            return res;
        }
        //public static bool GreaterThanPosition(CAxisBase axis, double TargetPos, double Offset)
        //{
        //    return GreaterThanPosition(axis, axis.CurrentPosition, TargetPos, Offset);
        //}
        public static bool GreaterThanPosition(CAxisBase axis, object TargetPoint, double Offset)
        {
            return GreaterThanTarget(axis.CurrentPosition, axis.PointList[TargetPoint].Position, Offset);
        }
        public static bool GreaterThanPosition(CAxisBase axis, double SourcePos, object TargetPoint, double Offset)
        {
            return GreaterThanTarget(SourcePos, axis.PointList[TargetPoint].Position, Offset);
        }
        private static bool GreaterThanTarget(double SourcePos, double TargetPos, double Offset)
        {
            return SourcePos > TargetPos + Offset;
        }
        //public static bool LessThanPosition(CAxisBase axis, double TargetPos, double Offset)
        //{
        //    return LessThanPosition(axis, axis.CurrentPosition, TargetPos, Offset);
        //}
        public static bool LessThanPosition(CAxisBase axis, object TargetPoint, double Offset)
        {
            return LessThanTarget(axis.CurrentPosition, axis.PointList[TargetPoint].Position, Offset);
        }
        public static bool LessThanPosition(CAxisBase axis, double SourcePos, object TargetPoint, double Offset)
        {
            return LessThanTarget(SourcePos, axis.PointList[TargetPoint].Position, Offset);
        }
        private static bool LessThanTarget(double SourcePos, double TargetPos, double Offset)
        {
            return SourcePos < TargetPos + Offset;
        }
        //public static bool InPositionRange(CAxisBase axis, double TargetPos, double Tolerance = 5)
        //{
        //    return InPositionRange(axis, axis.CurrentPosition, TargetPos, Tolerance);
        //}
        public static bool InPositionRange(CAxisBase axis, object TargetPoint, double Offset = 0, double Tolerance = 5)
        {
            return InPositionTarget(axis.CurrentPosition, axis.PointList[TargetPoint].Position + Offset, Tolerance);
        }
        //public static bool OutPositionRange(CAxisBase axis, object TargetPoint, double Tolerance = 5)
        //{
        //    return CheckErrPosTimeOut(() => { return !InPositionTarget(axis.CurrentPosition, axis.PointList[TargetPoint].Position, Tolerance); }, 1000);
        //}
        public static bool InPositionRange(CAxisBase axis, double SourcePos, object TargetPoint, double Offset = 0, double Tolerance = 5)
        {
            return InPositionTarget(SourcePos, axis.PointList[TargetPoint].Position + Offset, Tolerance);
        }
        public static bool InPositionTarget(double SourcePos, double TargetPos, double Tolerance = 5)
        {
            return Math.Abs(SourcePos - TargetPos) <= Math.Abs(Tolerance);
        }
        private bool IgnoreSmallStep(CAxisBase axis, double TargetPos)
        {
            return Math.Abs(axis.CurrentPosition - TargetPos) <= 5;
        }
        //private bool IgnoreSmallStep(CAxisBase axis, object TargetPoint)
        //{
        //    return InPositionRange(axis, TargetPoint);
        //}
        private bool CheckMotorStatus(CAxisBase source, CAxisBase axis)
        {
            bool res = axis.ServoOn && axis.HomeFinish;
            if (!res)
                ShowMotorError(source, axis.Name + "电机未上电或是未复位。");
            return res;
        }

        private bool AxisSortingPNPY_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisSortingPNPY = zones.WorkZoneNG挑选机械手.AxisSortingPNPY;
            CAxisBase AxisSortingPNPZ = zones.WorkZoneNG挑选机械手.AxisSortingPNPZ;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisSortingPNPY, AxisSortingPNPZ)) return false;
                if (!CheckMotorStatus(AxisSortingPNPY, AxisSortingPNPY)) return false;
                if (IgnoreSmallStep(AxisSortingPNPY, TargetPos)) return true;
            }
            //Z position is lower than Up point.
            if (!ClassCommonSetting.CheckTimeOut(() => GreaterThanPosition(AxisSortingPNPZ, ClassZoneNG挑选机械手.EnumPointPNPZ.Up, -5)))
            {
                ErrorStr = "NG挑选PNPZ电机低于Up位置5mm。";
                ShowMotorError(AxisSortingPNPY, ErrorStr);
                return false;
            }
            //Cylinders are not at up position.
            if (!zones.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPCyLeftUp).status ||
               !zones.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPCyMidUp).status ||
               !zones.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPCyRightUp).status)
            {
                ErrorStr = "NG挑选PNP气缸不在上位。";
                ShowMotorError(AxisSortingPNPY, ErrorStr);
                return false;
            }
            return true;
        }
        private bool AxisSortingPNPZ_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisSortingPNPZ = zones.WorkZoneNG挑选机械手.AxisSortingPNPZ;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisSortingPNPZ, AxisSortingPNPZ)) return false;
                if (IgnoreSmallStep(AxisSortingPNPZ, TargetPos)) return true;
            }
            return true;
        }
        private bool AxisLoadInConveyor_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisLoadInConveyor = zones.WorkZone上料传送.AxisLoadInConveyor;
            CAxisBase AxisLoadPNPZ = zones.WorkZone上料机械手.AxisLoadPNPZ;
            CAxisBase AxisLoadPNPY = zones.WorkZone上料机械手.AxisLoadPNPY;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisLoadInConveyor, AxisLoadPNPZ)) return false;
                if (!CheckMotorStatus(AxisLoadInConveyor, AxisLoadPNPY)) return false;
                if (!CheckMotorStatus(AxisLoadInConveyor, AxisLoadInConveyor)) return false;
                if (IgnoreSmallStep(AxisLoadInConveyor, TargetPos)) return true;
            }
            //Load PNP Y is at Pick point and Z position is lower than Idle point.
            if (InPositionRange(AxisLoadPNPY, ClassZone上料机械手.EnumPointY.Pick, 0, 15) && !ClassCommonSetting.CheckTimeOut(() =>
                GreaterThanPosition(AxisLoadPNPZ, ClassZone上料机械手.EnumPointZ.Idle, -5)))
            {
                ErrorStr = "上料PNP位于Pick位置，但是Z电机低于Idle位置5mm。";
                ShowMotorError(AxisLoadInConveyor, ErrorStr);
                return false;
            }
            if (TargetPos > AxisLoadInConveyor.CurrentPosition)
                //InPosition has part
                if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑 && zones.WorkZone上料传送.ThisInport(ClassZone上料传送.EnumInportName.LoadInConvInPosRight).status)
                {
                    ErrorStr = "右边到位传感器检测有物料。";
                    ShowMotorError(AxisLoadInConveyor, ErrorStr);
                    return false;
                }
            return true;
        }
        private bool AxisLoadPNPY_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisLoadPNPY = zones.WorkZone上料机械手.AxisLoadPNPY;
            CAxisBase AxisLoadPNPZ = zones.WorkZone上料机械手.AxisLoadPNPZ;
            CAxisBase AxisTransPNPX = zones.WorkZone传送机械手.AxisTransPNPX;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisLoadPNPY, AxisLoadPNPZ)) return false;
                if (!CheckMotorStatus(AxisLoadPNPY, AxisTransPNPX)) return false;
                if (!CheckMotorStatus(AxisLoadPNPY, AxisLoadPNPY)) return false;
                if (IgnoreSmallStep(AxisLoadPNPY, TargetPos)) return true;
            }

            //Z position is lower than Idle point.
            if (!ClassCommonSetting.CheckTimeOut(() => GreaterThanPosition(AxisLoadPNPZ, ClassZone上料机械手.EnumPointZ.Idle, -5)))
            {
                ErrorStr = "上料PNPZ电机低于Idle位置5mm。";
                ShowMotorError(AxisLoadPNPY, ErrorStr);
                return false;
            }
            //Cylinders are not at up position.
            if (!zones.WorkZone上料机械手.ThisInport(ClassZone上料机械手.EnumInportName.LoadPNPCyLeftUp).status ||
               !zones.WorkZone上料机械手.ThisInport(ClassZone上料机械手.EnumInportName.LoadPNPCyMidUp).status ||
               !zones.WorkZone上料机械手.ThisInport(ClassZone上料机械手.EnumInportName.LoadPNPCyRightUp).status)
            {
                ErrorStr = "上料PNP有气缸不在上位。";
                ShowMotorError(AxisLoadPNPY, ErrorStr);
                return false;
            }
            //Move to place point but TransPNP is at block position.
            if (GreaterThanPosition(AxisLoadPNPY, TargetPos, ClassZone上料机械手.EnumPointY.SafeLimit, 0) && !ClassCommonSetting.CheckTimeOut(() =>
                GreaterThanPosition(AxisTransPNPX, ClassZone传送机械手.EnumPointPNPX.SafeLimit, 0)))
            {
                ErrorStr = "传送PNP位置阻挡上料PNPY移到Place位置。";
                ShowMotorError(AxisLoadPNPY, ErrorStr);
                return false;
            }
            return true;
        }
        private bool AxisLoadPNPZ_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisLoadPNPZ = zones.WorkZone上料机械手.AxisLoadPNPZ;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisLoadPNPZ, AxisLoadPNPZ)) return false;
                if (IgnoreSmallStep(AxisLoadPNPZ, TargetPos)) return true;
            }
            return true;
        }
        private bool AxisUnloadOutConveyor_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisUnloadOutConveyor = zones.WorkZone下料传送.AxisUnloadOutConveyor;
            CAxisBase AxisSortingPNPZ = zones.WorkZoneNG挑选机械手.AxisSortingPNPZ;
            CAxisBase AxisSortingPNPY = zones.WorkZoneNG挑选机械手.AxisSortingPNPY;
            CAxisBase AxisUnloadPNPY = zones.WorkZone下料机械手.AxisUnloadPNPY;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisUnloadOutConveyor, AxisUnloadPNPY)) return false;
                if (!CheckMotorStatus(AxisUnloadOutConveyor, AxisSortingPNPZ)) return false;
                if (!CheckMotorStatus(AxisUnloadOutConveyor, AxisSortingPNPY)) return false;
                if (!CheckMotorStatus(AxisUnloadOutConveyor, AxisUnloadOutConveyor)) return false;
                if (IgnoreSmallStep(AxisUnloadOutConveyor, TargetPos)) return true;
            }

            //Sorting PNP Y is at pick position and Z position is lower than Up point.
            if (InPositionRange(AxisSortingPNPY, ClassZoneNG挑选机械手.EnumPointPNPY.Pick) && !ClassCommonSetting.CheckTimeOut(() =>
                GreaterThanPosition(AxisSortingPNPZ, ClassZoneNG挑选机械手.EnumPointPNPZ.Up, -5)))
            {
                ErrorStr = "NG挑选PNP在Pick位置，但是Z电机低于Up位置5mm。";
                ShowMotorError(AxisUnloadOutConveyor, ErrorStr);
                return false;
            }
            //Unload PNP Y is at place position and cylinder is not at up position.
            if (InPositionRange(AxisUnloadPNPY, ClassZone下料机械手.EnumPoint.Place) &&
                !zones.WorkZone下料机械手.ThisInport(ClassZone下料机械手.EnumInportName.UnloadPNPCyUp).status)
            {
                ErrorStr = "下料PNP气缸不在上位。";
                ShowMotorError(AxisUnloadOutConveyor, ErrorStr);
                return false;
            }
            ////Unload position has part
            //if (zones.WorkZone下料传送.ThisInport(ClassZone下料传送.EnumInportName.UnloadOutUnload).status)
            //{
            //    ErrorStr = "右边下料传感器检测有物料。";
            //    ShowMotorError(AxisUnloadOutConveyor, ErrorStr);
            //    return false;
            //}
            return true;
        }
        private bool AxisUnloadPNPY_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisUnloadPNPY = zones.WorkZone下料机械手.AxisUnloadPNPY;
            //CAxisBase AxisTransPNPX = zones.WorkZone传送机械手.AxisTransPNPX;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisUnloadPNPY, AxisUnloadPNPY)) return false;
                //if (!CheckMotorStatus(AxisUnloadPNPY, AxisTransPNPX)) return false;
                if (IgnoreSmallStep(AxisUnloadPNPY, TargetPos)) return true;
            }

            //Cylinder is not at up position.
            if (!zones.WorkZone下料机械手.ThisInport(ClassZone下料机械手.EnumInportName.UnloadPNPCyUp).status)
            {
                ErrorStr = "下料气缸不在上位。";
                ShowMotorError(AxisUnloadPNPY, ErrorStr);
                return false;
            }
            ////Move to Pick point but Trans PNP blocks on the way.
            //if (GreaterThanPosition(AxisUnloadPNPY, TargetPos, ClassZone下料机械手.EnumPoint.Pick, -15) &&
            //    GreaterThanPosition(AxisTransPNPX, ClassZone传送机械手.EnumPointPNPX.Load, 15))
            //{
            //    ShowMotorError(AxisUnloadPNPY, "传送PNP位置阻挡下料PNPY移到Pick位置。");
            //    return false;
            //}
            return true;
        }
        private bool AxisTransPNPX_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTransPNPX = zones.WorkZone传送机械手.AxisTransPNPX;
            CAxisBase AxisLoadPNPY = zones.WorkZone上料机械手.AxisLoadPNPY;
            //CAxisBase AxisUnloadPNPY = zones.WorkZone下料机械手.AxisUnloadPNPY;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTransPNPX, AxisTransPNPX)) return false;
                if (!CheckMotorStatus(AxisTransPNPX, AxisLoadPNPY)) return false;
                //if (!CheckMotorStatus(AxisTransPNPX, AxisUnloadPNPY)) return false;
                if (IgnoreSmallStep(AxisTransPNPX, TargetPos)) return true;
            }
            //传送PNP气缸不在上位
            if (!zones.WorkZone传送机械手.ThisInport(ClassZone传送机械手.EnumInportName.TransPNPCyUp).status)
            {
                System.Threading.Thread.Sleep(100);
                if (!zones.WorkZone传送机械手.ThisInport(ClassZone传送机械手.EnumInportName.TransPNPCyUp).status)
                {
                    ErrorStr = "传送PNP气缸不在上位。";
                    ShowMotorError(AxisTransPNPX, ErrorStr);
                    return false;
                }
            }
            //上料时上料PNP在Place位置
            if (LessThanPosition(AxisTransPNPX, TargetPos, ClassZone传送机械手.EnumPointPNPX.SafeLimit, 0) && !ClassCommonSetting.CheckTimeOut(() =>
                 LessThanPosition(AxisLoadPNPY, ClassZone上料机械手.EnumPointY.SafeLimit, 0)))
            {
                ErrorStr = "上料PNPY阻挡传送PNPX移到Load位置。";
                ShowMotorError(AxisTransPNPX, ErrorStr);
                return false;
            }
            ////下料时下料PNP在Pick位置
            //if (!GreaterThanPosition(AxisTransPNPX, TargetPos, ClassZone传送机械手.EnumPointPNPX.Unload, -15) &&
            //    GreaterThanPosition(AxisUnloadPNPY, ClassZone下料机械手.EnumPoint.Pick, -15))
            //{
            //    ShowMotorError(AxisTransPNPX, "下料PNPY阻挡传送PNPX移到Unload位置。");
            //    return false;
            //}
            return true;
        }
        private bool AxisThicknessMeasY_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisThicknessMeasY = zones.WorkZone厚度测量.AxisThicknessMeasY;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisThicknessMeasY, AxisThicknessMeasY)) return false;
                if (IgnoreSmallStep(AxisThicknessMeasY, TargetPos)) return true;
            }

            //Trans PNP cylinder is not at up position.
            if (!zones.WorkZone传送机械手.ThisInport(ClassZone传送机械手.EnumInportName.TransPNPCyUp).status)
            {
                System.Threading.Thread.Sleep(100);
                if (!zones.WorkZone传送机械手.ThisInport(ClassZone传送机械手.EnumInportName.TransPNPCyUp).status)
                {
                    ErrorStr = "传送PNP气缸不在上位。";
                    ShowMotorError(AxisThicknessMeasY, ErrorStr);
                    return false;
                }
            }
            //Thickness cylinder is not at up position.
            if (!zones.WorkZone厚度测量.ThisInport(ClassZone厚度测量.EnumInportName.ThicknessMeasCyLeftUp).status ||
                !zones.WorkZone厚度测量.ThisInport(ClassZone厚度测量.EnumInportName.ThicknessMeasCyMidUp).status ||
                !zones.WorkZone厚度测量.ThisInport(ClassZone厚度测量.EnumInportName.ThicknessMeasCyRightUp).status)
            {
                ErrorStr = "厚度检测气缸不在上位。";
                ShowMotorError(AxisThicknessMeasY, ErrorStr);
                return false;
            }

            return true;
        }
        private bool AxisOutlineMeasX_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisOutlineMeasX = zones.WorkZone尺寸测量.AxisOutlineMeasX;
            CAxisBase AxisUnloadPNPY = zones.WorkZone下料机械手.AxisUnloadPNPY;
            CAxisBase AxisTransPNPX = zones.WorkZone传送机械手.AxisTransPNPX;

            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisOutlineMeasX, AxisOutlineMeasX)) return false;
                if (!CheckMotorStatus(AxisOutlineMeasX, AxisUnloadPNPY)) return false;
                if (IgnoreSmallStep(AxisOutlineMeasX, TargetPos)) return true;
            }

            //Trans PNP cylinder is not at up position when Trans PNP is at unload position.
            if (InPositionRange(AxisTransPNPX, ClassZone传送机械手.EnumPointPNPX.Unload, 0, 15) && !zones.WorkZone传送机械手.ThisInport(ClassZone传送机械手.EnumInportName.TransPNPCyUp).status)
            {
                ErrorStr = "传送PNP在Unload位置但是气缸不在上位。";
                ShowMotorError(AxisOutlineMeasX, ErrorStr);
                return false;
            }
            //Unload PNP is at pick position and cylinder is not at up position.
            bool pos = InPositionRange(AxisUnloadPNPY, ClassZone下料机械手.EnumPoint.Pick, 0, 15);
            bool cylin = !zones.WorkZone下料机械手.ThisInport(ClassZone下料机械手.EnumInportName.UnloadPNPCyUp).status;
            if (pos && cylin)
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    pos = InPositionRange(AxisUnloadPNPY, ClassZone下料机械手.EnumPoint.Pick, 0, 15);
                    cylin = !zones.WorkZone下料机械手.ThisInport(ClassZone下料机械手.EnumInportName.UnloadPNPCyUp).status;
                    if (!pos || !cylin) break;
                }
            }
            if (pos && cylin)
            {
                ErrorStr = "下料PNP在Pick位置(" + AxisUnloadPNPY.CurrentPosition.ToString() + ")，但是气缸不在上位。";
                ShowMotorError(AxisOutlineMeasX, ErrorStr);
                return false;
            }

            return true;
        }
        private bool AxisTopAlignXSide_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTopAlignXSide = zones.WorkZone顶封边定位.AxisTopAlignXSide;
            CAxisBase AxisTopAlignZClamp = zones.WorkZone顶封边定位.AxisTopAlignZClamp;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTopAlignXSide, AxisTopAlignXSide)) return false;
                if (!CheckMotorStatus(AxisTopAlignXSide, AxisTopAlignZClamp)) return false;
                if (IgnoreSmallStep(AxisTopAlignXSide, TargetPos)) return true;
            }

            if (!zones.WorkZone顶封边定位.ThisInport(ClassZone顶封边定位.EnumInportName.TopAlignCyLeftUp).status ||
               !zones.WorkZone顶封边定位.ThisInport(ClassZone顶封边定位.EnumInportName.TopAlignCyMidUp).status ||
               !zones.WorkZone顶封边定位.ThisInport(ClassZone顶封边定位.EnumInportName.TopAlignCyRightUp).status)
            {
                ErrorStr = "顶封边Z夹紧气缸不在上位。";
                ShowMotorError(AxisTopAlignXSide, ErrorStr);
                return false;
            }
            return true;
        }
        private bool AxisTopAlignTop_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTopAlignTop = zones.WorkZone顶封边定位.AxisTopAlignTop;
            CAxisBase AxisTopAlignZClamp = zones.WorkZone顶封边定位.AxisTopAlignZClamp;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTopAlignTop, AxisTopAlignTop)) return false;
                if (!CheckMotorStatus(AxisTopAlignTop, AxisTopAlignZClamp)) return false;
                if (IgnoreSmallStep(AxisTopAlignTop, TargetPos)) return true;
            }

            //if (ClassCommonSetting.GreaterThanPosition(AxisTopAlignZClamp, ClassZone顶封边定位.EnumPointAlign.Clamp, -1))
            //{
            //    ErrorStr = "顶封边Z夹紧电机在Clamp位置。";
            //    ShowMotorError(AxisTopAlignTop, ErrorStr);
            //    return false;
            //}
            return true;
        }
        private bool AxisTopAlignBottom_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTopAlignBottom = zones.WorkZone顶封边定位.AxisTopAlignBottom;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTopAlignBottom, AxisTopAlignBottom)) return false;
                if (IgnoreSmallStep(AxisTopAlignBottom, TargetPos)) return true;
            }
            return true;
        }
        private bool AxisTopAlignSide_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTopAlignSide = zones.WorkZone顶封边定位.AxisTopAlignSide;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTopAlignSide, AxisTopAlignSide)) return false;
                if (IgnoreSmallStep(AxisTopAlignSide, TargetPos)) return true;
            }
            return true;
        }
        private bool AxisTopAlignZClamp_CheckSafety(bool IsHome, double TargetPos, ref string ErrorStr)
        {
            CAxisBase AxisTopAlignZClamp = zones.WorkZone顶封边定位.AxisTopAlignZClamp;
            if (!IsHome)
            {
                if (!CheckMotorStatus(AxisTopAlignZClamp, AxisTopAlignZClamp)) return false;
                if (IgnoreSmallStep(AxisTopAlignZClamp, TargetPos)) return true;
            }
            return true;
        }
    }
}