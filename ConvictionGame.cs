using System;
using System.Collections.Generic;
using System.Text;

namespace SCC_Trainer
{
    public class ConvictionGame
    {
        public PlayerData player;
        public string MapName
        {
            get
            {
                Memory.ReadProcessMemory(Memory.handle, mapNamePtr, buffer, 32, out numBytesRead);
                string working = ASCIIEncoding.ASCII.GetString(buffer);
                int end = working.IndexOf(".umd");
                if (end > 0)
                    working = working.Substring(0, end);
                return working;
            }
        }

        private IntPtr mapNamePtr;

        private byte[] buffer = new byte[64];
        private IntPtr numBytesRead;

        private ulong oldSceneCounterAddr;
        private ulong sceneCounterAddr;


        public ConvictionGame()
        {
            if (Memory.handle == null)
                throw new Exception("Must hook game before instantiating ConvictionGame object!");

            sceneCounterAddr = Memory.GetAddressFromPointer(0xFCB49C, 0x18, 0x28);
            oldSceneCounterAddr = sceneCounterAddr;
            Initialize();
        }

        public void Initialize()
        {
            player = new PlayerData();

            mapNamePtr = (IntPtr)Memory.GetAddressFromPointer(0xF96294, 5);
        }

        public void CheckIfReloaded()
        {
            sceneCounterAddr = Memory.GetAddressFromPointer(0xFCB49C, 0x18, 0x28);
            if (sceneCounterAddr != oldSceneCounterAddr)
            {
                Initialize();
                oldSceneCounterAddr = sceneCounterAddr;
            }
        }
    }

    public struct PositionVector
    {
        public float X
        {
            get { return x.Value; }
            set { x.Value = value;  }
        }
        public float Y
        {
            get { return y.Value; }
            set { y.Value = value; }
        }
        public float Z
        {
            get { return z.Value; }
            set { z.Value = value; }
        }

        private AddressObject x;
        private AddressObject y;
        private AddressObject z;

        public PositionVector(ulong xPosAddress, params ulong[] offsets)
        {
            x = new AddressObject();
            x.address = Memory.GetAddressFromPointer(xPosAddress, offsets);

            y = new AddressObject();
            y.address = x.address + 8;

            z = new AddressObject();
            z.address = x.address + 4;
        }
    }

    public class PlayerData
    {
        public PositionVector position;

        public PlayerData()
        {
            position = new PositionVector(0xFCB49C, 0x18, 0x94);
        }
    }
}
