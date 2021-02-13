using System;
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

        private SCCVersion version;

        private IntPtr mapNamePtr;

        private byte[] buffer = new byte[64];
        private IntPtr numBytesRead;

        public ulong oldSceneCounterAddr;
        public ulong sceneCounterAddr;

        public int SceneCounter
        {
            get 
            {
                cachedSceneCounter = (int)sceneCounter.Value;
                return cachedSceneCounter; 
            }
        }
        private AddressObject<int> sceneCounter;
        private int cachedSceneCounter = 0;


        public ConvictionGame(SCCVersion ver)
        {
            if (Memory.handle == null)
                throw new Exception("Must hook game before instantiating ConvictionGame object!");

            version = ver;
            sceneCounterAddr = Memory.GetAddressFromPointer(0xFCB49C, 0x18, 0x28);
            oldSceneCounterAddr = sceneCounterAddr;
            sceneCounter = new AddressObject<int>(sceneCounterAddr);
            Initialize();
            Program.Log("Program hooked! Version: {0}", version.ToString());
        }

        public void Initialize()
        {
            player = new PlayerData();

            switch (version)
            {
                case SCCVersion.Steam:
                {
                    mapNamePtr = (IntPtr)Memory.GetAddressFromPointer(0xF961D4, 5);
                    break;
                }

                case SCCVersion.Uplay:
                {
                    mapNamePtr = (IntPtr)Memory.GetAddressFromPointer(0xF96294, 5);
                    break;
                }

                default:
                    throw new Exception("Game version not set!");
                    break;
            }
        }

        public void CheckIfReloaded()
        {
            sceneCounterAddr = Memory.GetAddressFromPointer(0xFCB49C, 0x18, 0x28);
            if (sceneCounterAddr != oldSceneCounterAddr)
            {
                Initialize();
                sceneCounter = new AddressObject<int>(sceneCounterAddr);
                if (cachedSceneCounter > 0)
                    Program.Log("Scene reloaded - previous counter at {0}", cachedSceneCounter);
                oldSceneCounterAddr = sceneCounterAddr;
            }
        }
    }

    public struct PlayerTransform
    {
        public float PosX
        {
            get { return (float)posx.Value; }
            set { posx.Value = value; }
        }
        public float PosY
        {
            get { return (float)posy.Value; }
            set { posy.Value = value; }
        }
        public float PosZ
        {
            get { return (float)posz.Value; }
            set { posz.Value = value; }
        }
        public ushort RotY
        {
            get { return (ushort)roty.Value; }
            set { roty.Value = value; }
        }
        public float VelX
        {
            get { return (float)velx.Value; }
            set { velx.Value = value; }
        }
        public float VelY
        {
            get { return (float)vely.Value; }
            set { vely.Value = value; }
        }
        public float VelZ
        {
            get { return (float)velz.Value; }
            set { velz.Value = value; }
        }

        private AddressObject<float> posx;
        private AddressObject<float> posy;
        private AddressObject<float> posz;
        private AddressObject<ushort> roty;

        private AddressObject<float> velx;
        private AddressObject<float> vely;
        private AddressObject<float> velz;

        public PlayerTransform(ulong playerObjectAddr, params ulong[] offsets)
        {
            ulong objectAddr = Memory.GetAddressFromPointer(playerObjectAddr, offsets);

            posx = new AddressObject<float>();
            posx.address = objectAddr + 0x94;

            posy = new AddressObject<float>();
            posy.address = objectAddr + 0x9C;

            posz = new AddressObject<float>();
            posz.address = objectAddr + 0x98;

            roty = new AddressObject<ushort>();
            roty.address = objectAddr + 0xA4;

            velx = new AddressObject<float>();
            velx.address = objectAddr + 0x1C0;

            vely = new AddressObject<float>();
            vely.address = objectAddr + 0x1C8;

            velz = new AddressObject<float>();
            velz.address = objectAddr + 0x1C4;
        }
    }

    public struct Transform
    {
        public float  PosX;
        public float  PosY;
        public float  PosZ;
        public ushort RotY;
    }

    public class PlayerData
    {
        public PlayerTransform transform;

        public PlayerData()
        {
            transform = new PlayerTransform(0xFCB49C, 0x18, 0x0);
        }
    }

    public enum SCCVersion
    {
        Steam,
        Uplay
    }
}
