using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HardwarePool
{
    /// <summary>
    /// 电机名称
    /// </summary>
    public enum HardwareAxisName
    {
        //Googol GTS-400#1
        //0-3
        LoadInConveyor = 0,     //上料传送皮带轴
        LoadPNPY = 1,           //上料机械手Y轴
        LoadPNPZ = 2,           //上料机械手Z轴
        TopAlignSide = 3,       //顶封边定位侧向定位轴
        //Googol GTS-400#2
        //4-7
        TopAlignBottom = 4,     //顶封边定位底部定位轴
        TopAlignXSide = 5,      //顶封边定位X轴
        TopAlignZClamp = 6,     //顶封边定位Z夹紧轴
        TopAlignTop = 7,        //顶封边定位头部调整轴
        //Googol GTS-400#3
        //8-11
        TransPNPX = 8,          //传送机械手X轴
        ThicknessMeasY = 9,     //厚度测量X轴
        OutlineMeasX = 10,      //CCD尺寸检测X轴
        UnloadPNPY = 11,        //下料机械手Y轴
        //Googol GTS-400#4
        //12-15
        UnloadOutConveyor = 12, //下料传送皮带轴
        SortingPNPY = 13,       //NG挑选机械手Y轴
        SortingPNPZ = 14,       //NG挑选机械手Z轴
    }
    /// <summary>
    /// 输入端口名称
    /// </summary>
    public enum HardwareInportName
    {
        //Googol GTS-400#1
        //0-7
        LoadInConvLoad = 0,                 //上料传送来料
        LoadInConvInPosRight = 1,           //上料传送到位右
        LoadPNPNGBoxFull = 2,               //上料机械手NG盒满
        LoadPNPCyLeftUp = 3,                //上料机械手左气缸上
        LoadPNPCyLeftDown = 4,              //上料机械手左气缸下
        LoadPNPCyMidUp = 5,                 //上料机械手中气缸上
        LoadPNPCyMidDown = 6,               //上料机械手中气缸下
        LoadPNPCyRightUp = 7,               //上料机械手右气缸上

        //8-15
        LoadPNPCyRightDown = 8,             //上料机械手右气缸下
        LoadPNPVacSensLeft = 9,             //上料机械手左吸头真空
        LoadPNPVacSensMid = 10,             //上料机械手中吸头真空
        LoadPNPVacSensRight = 11,           //上料机械手右吸头真空
        TopAlignCyLeftUp = 12,              //顶封边定位左气缸上
        TopAlignCyLeftDown = 13,            //顶封边定位左气缸下
        TopAlignCyMidUp = 14,               //顶封边定位中气缸上
        TopAlignCyMidDown = 15,             //顶封边定位中气缸下

        //Googol GTS-400#2
        //16-23
        TopAlignCyRightUp = 16,             //顶封边定位右气缸上
        TopAlignCyRightDown = 17,           //顶封边定位右气缸下
        TopAlignVacSensLeft = 18,           //顶封边定位左吸头真空
        TopAlignVacSensMid = 19,            //顶封边定位中吸头真空
        TopAlignVacSensRight = 20,          //顶封边定位右吸头真空
        TopAlignSoftCySensLeft = 21,        //顶封边定位左柔性气缸
        TopAlignSoftCySensMid = 22,         //顶封边定位中柔性气缸
        TopAlignSoftCySensRight = 23,       //顶封边定位右柔性气缸

        //24-31
        TransPNPCyUp = 24,                  //传送机械手气缸上
        TransPNPCyDown = 25,                //传送机械手气缸下
        TransPNPVacSensLoadLeft = 26,       //传送机械手进料左吸头真空
        TransPNPVacSensLoadMid = 27,        //传送机械手进料中吸头真空
        TransPNPVacSensLoadRight = 28,      //传送机械手进料右吸头真空
        TransPNPVacSensUnloadLeft = 29,     //传送机械手出料左吸头真空
        TransPNPVacSensUnloadMid = 30,      //传送机械手出料中吸头真空
        TransPNPVacSensUnloadRight = 31,    //传送机械手出料右吸头真空

        //Googol GTS-400#3
        //32-39
        ThicknessMeasCyLeftUp = 32,         //厚度测量左气缸上
        ThicknessMeasCyLeftDown = 33,       //厚度测量左气缸下
        ThicknessMeasCyMidUp = 34,          //厚度测量中气缸上
        ThicknessMeasCyMidDown = 35,        //厚度测量中气缸下
        ThicknessMeasCyRightUp = 36,        //厚度测量右气缸上
        ThicknessMeasCyRightDown = 37,      //厚度测量右气缸下
        ThicknessMeasVacSensLeft = 38,      //厚度测量左吸头真空
        ThicknessMeasVacSensMid = 39,       //厚度测量中吸头真空

        //40-47
        ThicknessMeasVacSensRight = 40,     //厚度测量右吸头真空
        OutlineMeasVacSensLeft = 41,        //CCD尺寸测量左吸头真空
        OutlineMeasVacSensMid = 42,         //CCD尺寸测量中吸头真空
        OutlineMeasVacSensRight = 43,       //CCD尺寸测量右吸头真空
        UnloadPNPCyUp = 44,                 //下料机械手气缸上
        UnloadPNPCyDown = 45,               //下料机械手气缸下
        UnloadPNPVacSensLeft = 46,          //下料机械手左吸头真空
        UnloadPNPVacSensMid = 47,           //下料机械手中吸头真空

        //Googol GTS-400#4
        //48-55
        UnloadPNPVacSensRight = 48,         //下料机械手右吸头真空
        UnloadOutHavePartLeft = 49,         //下料传送左有物料
        UnloadOutHavePartMid = 50,          //下料传送中有物料
        UnloadOutHavePartRight = 51,        //下料传送右有物料
        UnloadOutUnloadRight = 52,          //下料传送下料右
        SortingPNPCyLeftUp = 53,            //NG挑选机械手左气缸上
        SortingPNPCyLeftDown = 54,          //NG挑选机械手左气缸下
        SortingPNPCyMidUp = 55,             //NG挑选机械手中气缸上

        //56-63
        SortingPNPCyMidDown = 56,           //NG挑选机械手中气缸下
        SortingPNPCyRightUp = 57,           //NG挑选机械手右气缸上
        SortingPNPCyRightDown = 58,         //NG挑选机械手右气缸下
        SortingPNPVacSensLeft = 59,         //NG挑选机械手左吸头真空
        SortingPNPVacSensMid = 60,          //NG挑选机械手中吸头真空
        SortingPNPVacSensRight = 61,        //NG挑选机械手右吸头真空
        SortingPNPNGBoxFront = 62,          //NG挑选机械手前NG盒
        SortingPNPNGBoxBack = 63,           //NG挑选机械手后NG盒

        //Googol HCB2-1616#1
        //64-71
        SortingPNPNGBoxFull1 = 64,          //NG挑选机械手NG盒1满
        SortingPNPNGBoxFull2 = 65,          //NG挑选机械手NG盒2满
        SortingPNPNGBoxFull3 = 66,          //NG挑选机械手NG盒3满
        SortingPNPNGBoxFull4 = 67,          //NG挑选机械手NG盒4满
        FrameButtonStart = 68,              //外框Start按钮
        FrameButtonPause = 69,              //外框Pause按钮
        FrameButtonStop = 70,               //外框Stop按钮
        FrameButtonEmergency = 71,          //外框急停按钮1

        //72-79
        FrameAirPressSens = 72,             //外框总气压
        FrameVacuumSens = 73,               //外框总真空
        LoadInSMEMALoadReady = 74,          //上料传送SMEMA上料Ready
        UnloadOutSMEMAUnloadAvailable = 75, //下料传送SMEMA下料Available
        FrameDoorOpen = 76,                 //外框门禁
        FrameButtonEmergency2 = 77,         //外框急停按钮2
        LoadPNPNGBox = 78,                  //扫码NG盒到位
        LoadInConvInPosMid = 79,            //上料传送到位中

        //Googol HCB2-1616#2
        //80-87
        LoadInConvInPosLeft = 80,           //上料传送到位左
        UnloadOutUnloadMid = 81,            //下料传送下料中
        UnloadOutUnloadLeft = 82,           //下料传送下料左
        EXI83 = 83,
        EXI84 = 84,
        EXI85 = 85,
        EXI86 = 86,
        EXI87 = 87,

        //88-95
        EXI88 = 88,
        EXI89 = 89,
        EXI90 = 90,
        EXI91 = 91,
        EXI92 = 92,
        EXI93 = 93,
        EXI94 = 94,
        EXI95 = 95,

        //Googol HCB2-1616#3
        //96-103
        EXI96 = 96,
        EXI97 = 97,
        EXI98 = 98,
        EXI99 = 99,
        EXI100 = 100,
        EXI101 = 101,
        EXI102 = 102,
        EXI103 = 103,

        //104-111
        EXI104 = 104,
        EXI105 = 105,
        EXI106 = 106,
        EXI107 = 107,
        EXI108 = 108,
        EXI109 = 109,
        EXI110 = 110,
        EXI111 = 111,

        //Googol HCB2-1616#4
        //112-119
        EXI112 = 112,
        EXI113 = 113,
        EXI114 = 114,
        EXI115 = 115,
        EXI116 = 116,
        EXI117 = 117,
        EXI118 = 118,
        EXI119 = 119,

        //120-127
        EXI120 = 120,
        EXI121 = 121,
        EXI122 = 122,
        EXI123 = 123,
        EXI124 = 124,
        EXI125 = 125,
        EXI126 = 126,
        EXI127 = 127,
    }
    /// <summary>
    /// 输出端口名称
    /// </summary>
    public enum HardwareOutportName
    {
        //Googol GTS-400#1
        //0-7
        EXI00 = 0,
        LoadPNPCyLeftUp = 1,                //上料机械手左气缸上控制
        LoadPNPCyLeftDown = 2,              //上料机械手左气缸下控制
        LoadPNPCyMidUp = 3,                 //上料机械手中气缸上控制
        LoadPNPCyMidDown = 4,               //上料机械手中气缸下控制
        LoadPNPCyRightUp = 5,               //上料机械手右气缸上控制
        LoadPNPCyRightDown = 6,             //上料机械手右气缸下控制
        LoadPNPVacLeftBack = 7,             //上料机械手左吸头后腔真空

        //8-15
        LoadPNPVacLeftCent = 8,             //上料机械手左吸头中腔真空
        LoadPNPVacLeftFront = 9,            //上料机械手左吸头前腔真空
        LoadPNPBlowLeft = 10,               //上料机械手左吸头吹气
        LoadPNPVacMidBack = 11,             //上料机械手中吸头后腔真空
        LoadPNPVacMidCent = 12,             //上料机械手中吸头中腔真空
        LoadPNPVacMidFront = 13,            //上料机械手中吸头前腔真空
        LoadPNPBlowMid = 14,                //上料机械手中吸头吹气
        LoadPNPVacRightBack = 15,           //上料机械手右吸头后腔真空

        //Googol GTS-400#2
        //16-23
        LoadPNPVacRightCent = 16,           //上料机械手右吸头中腔真空
        LoadPNPVacRightFront = 17,          //上料机械手右吸头前腔真空
        LoadPNPBlowRight = 18,              //上料机械手右吸头吹气
        TopAlignCyLeftUp = 19,              //顶封边定位左气缸上控制
        TopAlignCyLeftDown = 20,            //顶封边定位左气缸下控制
        TopAlignCyMidUp = 21,               //顶封边定位中气缸上控制
        TopAlignCyMidDown = 22,             //顶封边定位中气缸下控制
        TopAlignCyRightUp = 23,             //顶封边定位右气缸上控制

        //24-31
        TopAlignCyRightDown = 24,           //顶封边定位右气缸下控制
        TopAlignVacLeft = 25,               //顶封边定位左吸头真空
        TopAlignVacMid = 26,                //顶封边定位中吸头真空
        TopAlignVacRight = 27,              //顶封边定位右吸头真空
        TopAlignBlow = 28,                  //顶封边定位吹气
        TopAlignSoftCyLeft = 29,            //顶封边定位柔性左气缸控制
        TopAlignSoftCyMid = 30,             //顶封边定位柔性中气缸控制
        TopAlignSoftCyRight = 31,           //顶封边定位柔性右气缸控制

        //Googol GTS-400#3
        //32-39
        TransPNPCyUp = 32,                  //传送机械手气缸上控制
        TransPNPCyDown = 33,                //传送机械手气缸下控制
        TransPNPVacLoadLeftBack = 34,       //传送机械手进料左吸头后腔真空
        TransPNPVacLoadLeftCent = 35,       //传送机械手进料左吸头中腔真空
        TransPNPVacLoadLeftFront = 36,      //传送机械手进料左吸头前腔真空
        TransPNPVacLoadMidBack = 37,        //传送机械手进料中吸头后腔真空
        TransPNPVacLoadMidCent = 38,        //传送机械手进料中吸头中腔真空
        TransPNPVacLoadMidFront = 39,       //传送机械手进料中吸头前腔真空

        //40-47
        TransPNPVacLoadRightBack = 40,      //传送机械手进料右吸头后腔真空
        TransPNPVacLoadRightCent = 41,      //传送机械手进料右吸头中腔真空
        TransPNPVacLoadRightFront = 42,     //传送机械手进料右吸头前腔真空
        TransPNPBlowLoad = 43,              //传送机械手进料吹气
        TransPNPVacUnloadLeftBack = 44,     //传送机械手出料左吸头后腔真空
        TransPNPVacUnloadLeftCent = 45,     //传送机械手出料左吸头中腔真空
        TransPNPVacUnloadLeftFront = 46,    //传送机械手出料左吸头前腔真空
        TransPNPVacUnloadMidBack = 47,      //传送机械手出料中吸头后腔真空

        //Googol GTS-400#4
        //48-55
        TransPNPVacUnloadMidCent = 48,      //传送机械手出料中吸头中腔真空
        TransPNPVacUnloadMidFront = 49,     //传送机械手出料中吸头前腔真空
        TransPNPVacUnloadRightBack = 50,    //传送机械手出料右吸头后腔真空
        TransPNPVacUnloadRightCent = 51,    //传送机械手出料右吸头中腔真空
        TransPNPVacUnloadRightFront = 52,   //传送机械手出料右吸头前腔真空
        TransPNPBlowUnload = 53,            //传送机械手出料吹气
        ThicknessMeasCyLeftUp = 54,         //厚度检测左气缸上控制
        ThicknessMeasCyLeftDown = 55,       //厚度检测左气缸下控制

        //56-63
        ThicknessMeasCyMidUp = 56,          //厚度检测中气缸上控制
        ThicknessMeasCyMidDown = 57,        //厚度检测中气缸下控制
        ThicknessMeasCyRightUp = 58,        //厚度检测右气缸上控制
        ThicknessMeasCyRightDown = 59,      //厚度检测右气缸下控制
        ThicknessMeasVacLeft = 60,          //厚度检测左吸头真空
        ThicknessMeasVacMid = 61,           //厚度检测中吸头真空
        ThicknessMeasVacRight = 62,         //厚度检测右吸头真空
        ThicknessMeasBlow = 63,             //厚度检测吹气

        //Googol HCB2-1616#1
        //64-71
        OutlineMeasVacLeft = 64,            //CCD尺寸检测左吸头真空
        OutlineMeasVacMid = 65,             //CCD尺寸检测中吸头真空
        OutlineMeasVacRight = 66,           //CCD尺寸检测右吸头真空
        OutlineMeasBlow = 67,               //CCD尺寸检测吹气
        UnloadPNPCyUp = 68,                 //下料机械手气缸上控制
        UnloadPNPCyDown = 69,               //下料机械手气缸下控制
        UnloadPNPVacLeftBack = 70,          //下料机械手左吸头后腔真空
        UnloadPNPVacLeftCent = 71,          //下料机械手左吸头中腔真空

        //72-79
        UnloadPNPVacLeftFront = 72,         //下料机械手左吸头前腔真空
        UnloadPNPVacMidBack = 73,           //下料机械手中吸头后腔真空
        UnloadPNPVacMidCent = 74,           //下料机械手中吸头中腔真空
        UnloadPNPVacMidFront = 75,          //下料机械手中吸头前腔真空
        UnloadPNPVacRightBack = 76,         //下料机械手右吸头后腔真空
        UnloadPNPVacRightCent = 77,         //下料机械手右吸头中腔真空
        UnloadPNPVacRightFront = 78,        //下料机械手右吸头前腔真空
        UnloadPNPBlow = 79,                 //下料机械手吹气

        //Googol HCB2-1616#2
        //80-87
        SortingPNPCyLeftUp = 80,            //NG挑选机械手左气缸上控制
        SortingPNPCyLeftDown = 81,          //NG挑选机械手左气缸下控制
        SortingPNPCyMidUp = 82,             //NG挑选机械手中气缸上控制
        SortingPNPCyMidDown = 83,           //NG挑选机械手中气缸下控制
        SortingPNPCyRightUp = 84,           //NG挑选机械手右气缸上控制
        SortingPNPCyRightDown = 85,         //NG挑选机械手右气缸下控制
        SortingPNPVacLeftBack = 86,         //NG挑选机械手左吸头后腔真空
        SortingPNPVacLeftCent = 87,         //NG挑选机械手左吸头中腔真空

        //88-95
        SortingPNPVacLeftFront = 88,        //NG挑选机械手左吸头前腔真空
        SortingPNPVacMidBack = 89,          //NG挑选机械手中吸头后腔真空
        SortingPNPVacMidCent = 90,          //NG挑选机械手中吸头中腔真空
        SortingPNPVacMidFront = 91,         //NG挑选机械手中吸头前腔真空
        SortingPNPVacRightBack = 92,        //NG挑选机械手右吸头后腔真空
        SortingPNPVacRightCent = 93,        //NG挑选机械手右吸头中腔真空
        SortingPNPVacRightFront = 94,       //NG挑选机械手右吸头前腔真空
        SortingPNPBlow = 95,                //NG挑选机械手吹气

        //Googol HCB2-1616#3
        //96-103
        FrameButtonLightStart = 96,         //外框Start按钮灯
        FrameButtonLightPause = 97,         //外框Pause按钮灯
        FrameButtonLightStop = 98,          //外框Stop按钮灯
        FrameLightGreen = 99,               //外框三色灯绿色
        FrameLightYellow = 100,             //外框三色灯黄色
        FrameLightRed = 101,                //外框三色灯红色
        FrameBuzz = 102,                    //外框三色灯蜂鸣器
        FramePowerOn = 103,                 //外框电源开

        //104-111
        FramePowerOff = 104,                //外框电源关
        LoadInSMEMALoadAvailable = 105,     //上料传送SMEMA上料Available
        LoadPNPVacLeft = 106,               //上料机械手左吸头总真空
        LoadPNPVacMid = 107,                //上料机械手中吸头总真空
        LoadPNPVacRight = 108,              //上料机械手右吸头总真空
        TransPNPVacLoadLeft = 109,          //传送机械手进料左吸头总真空
        TransPNPVacLoadMid = 110,           //传送机械手进料中吸头总真空
        TransPNPVacLoadRight = 111,         //传送机械手进料右吸头总真空

        //Googol HCB2-1616#4
        //112-119
        TransPNPVacUnloadLeft = 112,        //传送机械手出料左吸头总真空
        TransPNPVacUnloadMid = 113,         //传送机械手出料中吸头总真空
        TransPNPVacUnloadRight = 114,       //传送机械手出料右吸头总真空
        UnloadPNPVacLeft = 115,             //下料机械手左吸头总真空
        UnloadPNPVacMid = 116,              //下料机械手中吸头总真空
        UnloadPNPVacRight = 117,            //下料机械手右吸头总真空
        SortingPNPVacLeft = 118,            //NG挑选机械手左吸头总真空
        SortingPNPVacMid = 119,             //NG挑选机械手中吸头总真空

        //120-127
        SortingPNPVacRight = 120,           //NG挑选机械手右吸头总真空
        UnloadOutSMEMAUnloadReady = 121,    //下料传送SMEMA下料Ready
        EXO122 = 122,
        FrameKMSwitch = 123,                //2套键鼠切换
        EXO124 = 124,
        EXO125 = 125,
        EXO126 = 126,
        EXO127 = 127,
    }
    public enum HardwareSerialPortName
    {
        LoadInBarcode,
        ThicknessSensorLeft,
        ThicknessSensorMid,
        ThicknessSensorRight,
        OutlineMeasLightController,
    }
}