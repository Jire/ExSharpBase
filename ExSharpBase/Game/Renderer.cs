using ExSharpBase.Modules;
using SharpDX;

namespace ExSharpBase.Game
{
    internal static class Renderer
    {
        private const int WIDTH = 0x10;
        private const int HEIGHT = WIDTH + 0x4;
        private static readonly int ViewMatrix = OffsetManager.Instances.ViewMatrix;
        private static readonly int ProjectionMatrix = ViewMatrix + 0x40;

        private static int Instance { get; } = Memory.Read<int>(OffsetManager.Instances.Renderer);

        private static Matrix GetViewProjectionMatrix()
        {
            return Matrix.Multiply(GetViewMatrix(), GetProjectionMatrix());
        }

        public static Matrix GetViewMatrix()
        {
            return Memory.ReadMatrix(ViewMatrix);
        }

        public static Matrix GetProjectionMatrix()
        {
            return Memory.ReadMatrix(ProjectionMatrix);
        }

        private static Vector2 GetScreenResolution()
        {
            return new Vector2 {X = Memory.Read<int>(Instance + WIDTH), Y = Memory.Read<int>(Instance + HEIGHT)};
        }

        public static Vector2 WorldToScreen(Vector3 pos)
        {
            var returnVec = Vector2.Zero;

            var screen = GetScreenResolution();
            var matrix = GetViewProjectionMatrix();

            Vector4 clipCoords;
            clipCoords.X = pos.X * matrix[0] + pos.Y * matrix[4] + pos.Z * matrix[8] + matrix[12];
            clipCoords.Y = pos.X * matrix[1] + pos.Y * matrix[5] + pos.Z * matrix[9] + matrix[13];
            clipCoords.Z = pos.X * matrix[2] + pos.Y * matrix[6] + pos.Z * matrix[10] + matrix[14];
            clipCoords.W = pos.X * matrix[3] + pos.Y * matrix[7] + pos.Z * matrix[11] + matrix[15];

            if (clipCoords[3] < 0.1f) return returnVec;

            Vector3 M;
            M.X = clipCoords.X / clipCoords.W;
            M.Y = clipCoords.Y / clipCoords.W;
            M.Z = clipCoords.Z / clipCoords.W;

            returnVec.X = (screen.X / 2 * M.X) + (M.X + screen.X / 2);
            returnVec.Y = -(screen.Y / 2 * M.Y) + (M.Y + screen.Y / 2);

            return returnVec;
        }
    }
}