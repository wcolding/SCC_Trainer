using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;

namespace SCC_Trainer
{
    public static class Memory
    {
        #region PInvoke
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        const UInt32 WAIT_TIMEOUT = 0x00000102;

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public struct MODULEENTRY32
        {
            internal uint dwSize;
            internal uint th32ModuleID;
            internal uint th32ProcessID;
            internal uint GlblcntUsage;
            internal uint ProccntUsage;
            internal IntPtr modBaseAddr;
            internal uint modBaseSize;
            internal IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string szExePath;
        }

        [DllImport("kernel32.dll")]
        static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll")]
        static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }
        
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        #endregion

        public static ulong baseAddress = 0;
        public static IntPtr handle;
        public static ProcessMode processMode;

        public static int HookProgram(string windowName, ProcessMode mode)
        {
            processMode = mode;
            IntPtr hwnd = FindWindowA(null, windowName);
            if (hwnd == (IntPtr)0)
                return -1;

            int pid = 0;
            GetWindowThreadProcessId(hwnd, out pid);

            IntPtr snapshot = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, (uint)pid);
            if (snapshot == (IntPtr)0)
                return -2;

            MODULEENTRY32 module = new MODULEENTRY32();
            module.dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32));
            Module32First(snapshot, ref module);
            baseAddress = (ulong)module.modBaseAddr;

            //while (Module32Next(snapshot, ref module))
            //{

            //}

            CloseHandle(snapshot);

            IntPtr proc = OpenProcess(ProcessAccessFlags.All, false, pid);
            if (proc == (IntPtr)0)
                return -3;

            handle = proc;

            // DLL Injection
            IntPtr addr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (addr != IntPtr.Zero)
            {
                //Program.Log("LoadLibraryA found at {0:X8}", (int)addr);

                // Just Uplay for now
                IntPtr arg = VirtualAllocEx(handle, IntPtr.Zero, (uint)Program.uplayDLL.Length, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ReadWrite);

                if (arg != IntPtr.Zero)
                {
                    IntPtr output;
                    bool success = WriteProcessMemory(handle, arg, Encoding.ASCII.GetBytes(Program.uplayDLL), Program.uplayDLL.Length, out output);
                    
                    if (success)
                    {
                        IntPtr threadID = CreateRemoteThread(handle, IntPtr.Zero, 0, addr, arg, 0, out output);

                        if (threadID != IntPtr.Zero)
                            Program.Log("DLL Injected successfully at {0:X8}", (int)threadID);
                    }
                }
            }

            return 0;
        }

        public static ulong GetAddressFromPointer(ulong address, params ulong[] offsets)
        {
            IntPtr intPtr;
            ulong value;
            int bufferSize = (processMode == ProcessMode.x86) ? 4 : 8;
            byte[] buffer = new byte[bufferSize];
            ReadProcessMemory(handle, (IntPtr)(baseAddress + address), buffer, bufferSize, out intPtr);
            value = (bufferSize == 4) ? BitConverter.ToUInt32(buffer, 0) : BitConverter.ToUInt64(buffer, 0);


            int offsetCount = offsets.Length;

            while (offsetCount > 1)
            {
                ReadProcessMemory(handle, (IntPtr)(value + offsets[offsets.Length - offsetCount]), buffer, bufferSize, out intPtr);
                value = (bufferSize == 4) ? BitConverter.ToUInt32(buffer, 0) : BitConverter.ToUInt64(buffer, 0);
                offsetCount--;
            }

            value += offsets[offsets.Length - 1];

            return value;
        }

        public static bool GameIsRunning
        {
            get
            {
                return WaitForSingleObject(handle, 1000) == WAIT_TIMEOUT;
            }
        }

        public static void Close()
        {
            CloseHandle(handle);
        }
    }

    public enum ProcessMode
    {
        x86,
        x64
    }

    public class AddressObject<T>
    {
        public ulong address;
        public object Value
        {
            get
            {
                Memory.ReadProcessMemory(Memory.handle, (IntPtr)address, buffer, size, out intPtr);
                object t;

                switch (type)
                {
                    case SupportedType.Float:
                        t = BitConverter.ToSingle(buffer, 0);
                        break;
                    case SupportedType.u16:
                        t = BitConverter.ToUInt16(buffer, 0);
                        break;
                    case SupportedType.s32:
                        t = BitConverter.ToInt32(buffer, 0);
                        break;
                    case SupportedType.u32:
                        t = BitConverter.ToUInt32(buffer, 0);
                        break;
                    default:
                        t = 0;
                        break;
                }

                return t;
            }

            set
            {
                bool set = true;
                switch (type)
                {
                    case SupportedType.Float:
                        buffer = BitConverter.GetBytes((float)value); 
                        break;
                    case SupportedType.u16:
                        buffer = BitConverter.GetBytes((ushort)value);
                        break;
                    case SupportedType.s32:
                        buffer = BitConverter.GetBytes((int)value);
                        break;
                    case SupportedType.u32:
                        buffer = BitConverter.GetBytes((uint)value);
                        break;
                    default:
                        set = false;
                        break;
                }

                if (set)
                    Memory.WriteProcessMemory(Memory.handle, (IntPtr)address, buffer, size, out intPtr);
            }
        }

        private byte[] buffer;
        private IntPtr intPtr;
        private int size = 4;
        private SupportedType type;

        public AddressObject()
        {
            address = Memory.baseAddress;
            buffer = new byte[8];
            GetTypeAndSize();
        }

        public AddressObject(ulong _address)
        {
            address = _address;
            buffer = new byte[8];
            GetTypeAndSize();
        }

        private void GetTypeAndSize()
        {
            if (typeof(T) == typeof(SByte)) { type = SupportedType.s8; size = 1; }
            if (typeof(T) == typeof(short)) { type = SupportedType.s16; size = 2; }
            if (typeof(T) == typeof(int)) { type = SupportedType.s32; size = 4; }
            if (typeof(T) == typeof(long)) { type = SupportedType.s64; size = 8; }
            if (typeof(T) == typeof(Byte)) { type = SupportedType.u8; size = 1; }
            if (typeof(T) == typeof(ushort)) { type = SupportedType.u16; size = 2; }
            if (typeof(T) == typeof(uint)) { type = SupportedType.u32; size = 4; }
            if (typeof(T) == typeof(ulong)) { type = SupportedType.u64; size = 8; }
            if (typeof(T) == typeof(float)) { type = SupportedType.Float;  size = 4; }
            if (typeof(T) == typeof(double)) { type = SupportedType.Double; size = 8; }

            if (typeof(T) == typeof(byte)) { type = SupportedType.u8; size = 1; }
        }

        private enum SupportedType
        {
            s8,
            s16,
            s32,
            s64,
            u8,
            u16,
            u32,
            u64,
            Float,
            Double
        }
    }
}
