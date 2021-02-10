using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SCC_Trainer
{
    public static class XInput
    {
        #region PInvoke

        [DllImport("XInput1_4.dll", SetLastError = true)]
        public static extern int XInputGetState(int dwUserIndex, ref State pState);

        [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public struct State
        {
            public uint dwPacketNumber;
            public Gamepad gamepad;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public struct Gamepad
        {
            public Button wButtons;
            public byte bLeftTrigger;
            public byte bRightTrigger;
            public short sThumbLX;
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }

        [Flags]
        public enum Button : ushort
        {
            DPad_Up    = 0x0001,
            DPad_Down  = 0x0002,
            DPad_Left  = 0x0004,
            DPad_Right = 0x0008,
            Start      = 0x0010,
            Back       = 0x0020,
            LClick     = 0x0040,
            RClick     = 0x0080,
            LB         = 0x0100,
            RB         = 0x0200,
            A          = 0x1000,
            B          = 0x2000,
            X          = 0x4000,
            Y          = 0x8000
        }

        #endregion

        public static bool isButtonDown(Button button, State state)
        {
            return state.gamepad.wButtons.HasFlag(button);
        }
    }
}
