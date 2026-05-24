using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace D4Macro.Util;

public static class KeyboardSender
{
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

    /// <summary>
    /// WPF Key를 입력받아 누른 상태로 유지합니다. (Hold)
    /// </summary>
    public static void HoldKey(Key key)
    {
        byte vkCode = (byte)KeyInterop.VirtualKeyFromKey(key);
        byte scanCode = (byte)MapVirtualKey(vkCode, 0); // 가상키를 하드웨어 스캔 코드로 변환 (0: MAPVK_VK_TO_VSC)
        keybd_event(vkCode, scanCode, KEYEVENTF_KEYDOWN, IntPtr.Zero);
    }

    /// <summary>
    /// 누르고 있던 WPF Key를 뗍니다. (Release)
    /// </summary>
    public static void ReleaseKey(Key key)
    {
        byte vkCode = (byte)KeyInterop.VirtualKeyFromKey(key);
        byte scanCode = (byte)MapVirtualKey(vkCode, 0);
        keybd_event(vkCode, scanCode, KEYEVENTF_KEYUP, IntPtr.Zero);
    }

    public static void TapKey(Key key)
    {
        HoldKey(key);
        ReleaseKey(key);
    }

    // 마우스 이벤트 유틸리티
    public static void HoldLeftMouse() => mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
    public static void ReleaseLeftMouse() => mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
    public static void HoldRightMouse() => mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero);
    public static void ReleaseRightMouse() => mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero);
}