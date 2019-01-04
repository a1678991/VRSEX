﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VRSEX
{
    public static class VRChatRPC
    {
        [DllImport("VRChatRPC", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool VRChatRPC_000();

        [DllImport("VRChatRPC", CallingConvention = CallingConvention.Cdecl)]
        private static extern int VRChatRPC_001([Out] byte[] data, int size);

        [DllImport("VRChatRPC", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr VRChatRPC_002();

        public static bool Update()
        {
            return VRChatRPC_000();
        }

        public static string GetAuthSessionTicket()
        {
            var a = new byte[1024];
            var n = VRChatRPC_001(a, 1024);
            return BitConverter.ToString(a, 0, n).Replace("-", string.Empty);
        }

        public static string GetPersonaName()
        {
            var ptr = VRChatRPC_002();
            if (ptr != IntPtr.Zero)
            {
                int n = 0;
                while (Marshal.ReadByte(ptr, n) != 0)
                {
                    ++n;
                }
                if (n > 0)
                {
                    var a = new byte[n];
                    Marshal.Copy(ptr, a, 0, a.Length);
                    return Encoding.UTF8.GetString(a);
                }
            }
            return string.Empty;
        }
    }
}