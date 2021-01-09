using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpDX;

namespace ExSharpBase.Modules
{
    class Memory
    {
        public static T Read<T>(int address)
        {
            var size = Marshal.SizeOf<T>();
            var buffer = new byte[size];
            var result = NativeImport.ReadProcessMemory(
                Process.GetProcessesByName("League of Legends").FirstOrDefault().Handle, (IntPtr) address, buffer, size,
                out var lpRead);
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            var ptrToStructure = Marshal.PtrToStructure<T>(ptr);
            Marshal.FreeHGlobal(ptr);
            return ptrToStructure;
        }

        public static string ReadString(int address, Encoding Encoding)
        {
            var dataBuffer = new byte[512];

            NativeImport.ReadProcessMemory(
                System.Diagnostics.Process.GetProcessesByName("League of Legends").FirstOrDefault().Handle,
                (IntPtr) address, dataBuffer, dataBuffer.Length, out var bytesRead);

            return bytesRead == IntPtr.Zero ? string.Empty : Encoding.GetString(dataBuffer).Split('\0')[0];
        }

        public static Matrix ReadMatrix(int address)
        {
            var tmp = Matrix.Zero;

            var buffer = new byte[64];

            NativeImport.ReadProcessMemory(Process.GetProcessesByName("League of Legends").FirstOrDefault().Handle,
                (IntPtr) address, buffer, 64, out var byteRead);

            if (byteRead == IntPtr.Zero)
            {
                //Console.WriteLine($"[ReadMatrix] No bytes has been read at 0x{address.ToString("X")}");
                return new Matrix();
            }

            tmp.M11 = BitConverter.ToSingle(buffer, (0 * 4));
            tmp.M12 = BitConverter.ToSingle(buffer, (1 * 4));
            tmp.M13 = BitConverter.ToSingle(buffer, (2 * 4));
            tmp.M14 = BitConverter.ToSingle(buffer, (3 * 4));

            tmp.M21 = BitConverter.ToSingle(buffer, (4 * 4));
            tmp.M22 = BitConverter.ToSingle(buffer, (5 * 4));
            tmp.M23 = BitConverter.ToSingle(buffer, (6 * 4));
            tmp.M24 = BitConverter.ToSingle(buffer, (7 * 4));

            tmp.M31 = BitConverter.ToSingle(buffer, (8 * 4));
            tmp.M32 = BitConverter.ToSingle(buffer, (9 * 4));
            tmp.M33 = BitConverter.ToSingle(buffer, (10 * 4));
            tmp.M34 = BitConverter.ToSingle(buffer, (11 * 4));

            tmp.M41 = BitConverter.ToSingle(buffer, (12 * 4));
            tmp.M42 = BitConverter.ToSingle(buffer, (13 * 4));
            tmp.M43 = BitConverter.ToSingle(buffer, (14 * 4));
            tmp.M44 = BitConverter.ToSingle(buffer, (15 * 4));

            return tmp;
        }

        public static Vector3 Read3DVector(int address)
        {
            var tmp = new Vector3();

            var buffer = new byte[12];

            NativeImport.ReadProcessMemory(Process.GetProcessesByName("League of Legends").FirstOrDefault().Handle,
                (IntPtr) (address + Game.OffsetManager.Object.POS), buffer, 12, out _);

            tmp.X = BitConverter.ToSingle(buffer, (0 * 4));
            tmp.Y = BitConverter.ToSingle(buffer, (1 * 4));
            tmp.Z = BitConverter.ToSingle(buffer, (2 * 4));

            return tmp;
        }
    }
}