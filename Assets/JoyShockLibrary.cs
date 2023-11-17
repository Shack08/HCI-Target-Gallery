// This doesn't reflect the latest features in JoyShockLibrary 3.0. But this is a good starting point for filling in the new functions and structs found in JoyShockLibrary.h.
namespace JoyShockLibrary
{
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public static class JSL
{
    public const int JS_TYPE_JOYCON_LEFT = 1;
    public const int JS_TYPE_JOYCON_RIGHT = 2;
    public const int JS_TYPE_PRO_CONTROLLER = 3;
    public const int JS_TYPE_DS4 = 4;
    public const int JS_TYPE_DS = 5;

    public const int JS_SPLIT_TYPE_LEFT = 1;
    public const int JS_SPLIT_TYPE_RIGHT = 2;
    public const int JS_SPLIT_TYPE_FULL = 3;

    public const int JSMASK_UP = 0x00001;
    public const int JSMASK_DOWN = 0x00002;
    public const int JSMASK_LEFT = 0x00004;
    public const int JSMASK_RIGHT = 0x00008;
    public const int JSMASK_PLUS = 0x00010;
    public const int JSMASK_OPTIONS = 0x00010;
    public const int JSMASK_MINUS = 0x00020;
    public const int JSMASK_SHARE = 0x00020;
    public const int JSMASK_LCLICK = 0x00040;
    public const int JSMASK_RCLICK = 0x00080;
    public const int JSMASK_L = 0x00100;
    public const int JSMASK_R = 0x00200;
    public const int JSMASK_ZL = 0x00400;
    public const int JSMASK_ZR = 0x00800;
    public const int JSMASK_S = 0x01000;
    public const int JSMASK_E = 0x02000;
    public const int JSMASK_W = 0x04000;
    public const int JSMASK_N = 0x08000;
    public const int JSMASK_HOME = 0x10000;
    public const int JSMASK_PS = 0x10000;
    public const int JSMASK_CAPTURE = 0x20000;
    public const int JSMASK_TOUCHPAD_CLICK = 0x20000;
    public const int JSMASK_MIC = 0x40000;
    public const int JSMASK_SL = 0x40000;
    public const int JSMASK_SR = 0x80000;

   public const int JSOFFSET_UP = 0;
    public const int JSOFFSET_DOWN = 1;
    public const int JSOFFSET_LEFT = 2;
    public const int JSOFFSET_RIGHT = 3;
    public const int JSOFFSET_PLUS = 4;
    public const int JSOFFSET_OPTIONS = 4;
    public const int JSOFFSET_MINUS = 5;
    public const int JSOFFSET_SHARE = 5;
    public const int JSOFFSET_LCLICK = 6;
    public const int JSOFFSET_RCLICK = 7;
    public const int JSOFFSET_L = 8;
    public const int JSOFFSET_R = 9;
    public const int JSOFFSET_ZL = 10;
    public const int JSOFFSET_ZR = 11;
    public const int JSOFFSET_S = 12;
    public const int JSOFFSET_E = 13;
    public const int JSOFFSET_W = 14;
    public const int JSOFFSET_N = 15;
    public const int JSOFFSET_HOME = 16;
    public const int JSOFFSET_PS = 16;
    public const int JSOFFSET_CAPTURE = 17;
    public const int JSOFFSET_TOUCHPAD_CLICK = 17;
    public const int JSOFFSET_MIC = 18;
    public const int JSOFFSET_SL = 18;
    public const int JSOFFSET_SR = 19;

    public const int DS5_PLAYER_1 = 4;
    public const int DS5_PLAYER_2 = 10;
    public const int DS5_PLAYER_3 = 21;
    public const int DS5_PLAYER_4 = 27;
    public const int DS5_PLAYER_5 = 31;

    [StructLayout(LayoutKind.Sequential)]
    public struct JOY_SHOCK_STATE
    {
        public int buttons;
        public float lTrigger;
        public float rTrigger;
        public float stickLX;
        public float stickLY;
        public float stickRX;
        public float stickRY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMU_STATE
    {
        public float accelX;
        public float accelY;
        public float accelZ;
        public float gyroX;
        public float gyroY;
        public float gyroZ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOTION_STATE
    {
        public float quatW;
        public float quatX;
        public float quatY;
        public float quatZ;
        public float accelX;
        public float accelY;
        public float accelZ;
        public float gravX;
        public float gravY;
        public float gravZ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TOUCH_STATE
    {
        public int t0Id;
        public int t1Id;
        public bool t0Down;
        public bool t1Down;
        public float t0X;
        public float t0Y;
        public float t1X;
        public float t1Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JSL_AUTO_CALIBRATION
    {
        public float confidence;
        [MarshalAs(UnmanagedType.I1)]
        public bool autoCalibrationEnabled;
        [MarshalAs(UnmanagedType.I1)]
        public bool isSteady;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JSL_SETTINGS
    {
        public int gyroSpace;
        public int colour;
        public int playerNumber;
        public int controllerType;
        public int splitType;
        [MarshalAs(UnmanagedType.I1)]
        public bool isCalibrating;
        [MarshalAs(UnmanagedType.I1)]
        public bool autoCalibrationEnabled;
        [MarshalAs(UnmanagedType.I1)]
        public bool isConnected;
    }

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslConnectDevices();

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetConnectedDeviceHandles(int[] deviceHandleArray, int size);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslDisconnectAndDisposeAll();

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.I1)]
public static extern bool JslStillConnected(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern JOY_SHOCK_STATE JslGetSimpleState(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern IMU_STATE JslGetIMUState(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern MOTION_STATE JslGetMotionState(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern TOUCH_STATE JslGetTouchState(int deviceId, [MarshalAs(UnmanagedType.I1)] bool previous = false);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.I1)]
public static extern bool JslGetTouchpadDimension(int deviceId, out int sizeX, out int sizeY);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetButtons(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetLeftX(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetLeftY(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetRightX(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetRightY(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetLeftTrigger(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetRightTrigger(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetGyroX(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetGyroY(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetGyroZ(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslGetAndFlushAccumulatedGyro(int deviceId, out float gyroX, out float gyroY, out float gyroZ);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetGyroSpace(int deviceId, int gyroSpace);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetAccelX(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetAccelY(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetAccelZ(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetTouchId(int deviceId, [MarshalAs(UnmanagedType.I1)] bool secondTouch = false);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.I1)]
public static extern bool JslGetTouchDown(int deviceId, [MarshalAs(UnmanagedType.I1)] bool secondTouch = false);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetTouchX(int deviceId, [MarshalAs(UnmanagedType.I1)] bool secondTouch = false);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetTouchY(int deviceId, [MarshalAs(UnmanagedType.I1)] bool secondTouch = false);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetStickStep(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetTriggerStep(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetPollRate(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern float JslGetTimeSinceLastUpdate(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslResetContinuousCalibration(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslStartContinuousCalibration(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslPauseContinuousCalibration(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetAutomaticCalibration(int deviceId, [MarshalAs(UnmanagedType.I1)] bool enabled);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslGetCalibrationOffset(int deviceId, out float xOffset, out float yOffset, out float zOffset);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetCalibrationOffset(int deviceId, float xOffset, float yOffset, float zOffset);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern JSL_AUTO_CALIBRATION JslGetAutoCalibrationStatus(int deviceId);

// For the callback functions, you need to define a delegate type in C# that matches the function signature
public delegate void JslCallback(int deviceId, JOY_SHOCK_STATE currentState, JOY_SHOCK_STATE prevState, IMU_STATE currentIMU, IMU_STATE prevIMU, float deltaTime);
public delegate void JslTouchCallback(int deviceId, TOUCH_STATE currentState, TOUCH_STATE prevState, float deltaTime);
public delegate void JslConnectCallback(int deviceId);
public delegate void JslDisconnectCallback(int deviceId, [MarshalAs(UnmanagedType.I1)] bool manual);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetCallback(JslCallback callback);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetTouchCallback(JslTouchCallback callback);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetConnectCallback(JslConnectCallback callback);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetDisconnectCallback(JslDisconnectCallback callback);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern JSL_SETTINGS JslGetControllerInfoAndSettings(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetControllerType(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetControllerSplitType(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern int JslGetControllerColour(int deviceId);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetLightColour(int deviceId, int colour);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetRumble(int deviceId, int smallRumble, int bigRumble);

[DllImport("JoyShockLibrary", CallingConvention = CallingConvention.Cdecl)]
public static extern void JslSetPlayerNumber(int deviceId, int number);

}
}