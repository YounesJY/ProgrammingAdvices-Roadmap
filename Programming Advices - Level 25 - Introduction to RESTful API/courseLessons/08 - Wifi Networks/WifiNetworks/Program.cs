using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WifiScanner
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanInterfaceInfoListHeader
    {
        public uint dwNumberOfItems;
        public uint dwIndex;
        // WLAN_INTERFACE_INFO InterfaceInfo[] follows immediately after this header
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WlanInterfaceInfo
    {
        public Guid InterfaceGuid;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string strInterfaceDescription;

        public uint isState;
    }

    internal static class NativeWlan
    {
        [DllImport("Wlanapi.dll")]
        public static extern uint WlanOpenHandle(
            uint dwClientVersion,
            IntPtr pReserved,
            out uint pdwNegotiatedVersion,
            out IntPtr phClientHandle);

        [DllImport("Wlanapi.dll")]
        public static extern uint WlanEnumInterfaces(
            IntPtr hClientHandle,
            IntPtr pReserved,
            out IntPtr ppInterfaceList);

        [DllImport("Wlanapi.dll")]
        public static extern uint WlanCloseHandle(
            IntPtr hClientHandle,
            IntPtr pReserved);

        [DllImport("Wlanapi.dll")]
        public static extern void WlanFreeMemory(IntPtr pMemory);
    }

    internal sealed class WifiScanner : IDisposable
    {
        private const uint WLAN_CLIENT_VERSION_VISTA = 2;

        private IntPtr _clientHandle = IntPtr.Zero;

        public WifiScanner()
        {
            uint negotiatedVersion;
            uint result = NativeWlan.WlanOpenHandle(
                WLAN_CLIENT_VERSION_VISTA,
                IntPtr.Zero,
                out negotiatedVersion,
                out _clientHandle);

            if (result != 0)
                throw new InvalidOperationException($"WlanOpenHandle failed with error code {result}.");
        }

        public List<string> GetWirelessAdapters()
        {
            var adapters = new List<string>();

            IntPtr interfaceListPtr = IntPtr.Zero;
            uint result = NativeWlan.WlanEnumInterfaces(_clientHandle, IntPtr.Zero, out interfaceListPtr);

            if (result != 0)
                throw new InvalidOperationException($"WlanEnumInterfaces failed with error code {result}.");

            try
            {
                var header = Marshal.PtrToStructure<WlanInterfaceInfoListHeader>(interfaceListPtr);
                int headerSize = Marshal.SizeOf<WlanInterfaceInfoListHeader>();
                int infoSize = Marshal.SizeOf<WlanInterfaceInfo>();

                for (int i = 0; i < header.dwNumberOfItems; i++)
                {
                    IntPtr infoPtr = IntPtr.Add(interfaceListPtr, headerSize + i * infoSize);
                    var info = Marshal.PtrToStructure<WlanInterfaceInfo>(infoPtr);
                    adapters.Add(info.strInterfaceDescription);
                }
            }
            finally
            {
                NativeWlan.WlanFreeMemory(interfaceListPtr);
            }

            return adapters;
        }

        public void Dispose()
        {
            if (_clientHandle != IntPtr.Zero)
            {
                NativeWlan.WlanCloseHandle(_clientHandle, IntPtr.Zero);
                _clientHandle = IntPtr.Zero;
            }
        }
    }

    internal static class Program
    {
        static void Main()
        {
            WifiScanner wifiScanner = new WifiScanner();
            List<string> adapters = wifiScanner.GetWirelessAdapters();

            if (adapters.Count == 0)
                Console.WriteLine("No wireless adapters found.");
            else
            {
                foreach (string adapter in adapters)
                    Console.WriteLine($"Adapter: {adapter}");
            }

            Console.ReadKey();
        }
    }
}