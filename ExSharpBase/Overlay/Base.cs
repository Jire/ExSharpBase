using System;
using System.Windows.Forms;
using ExSharpBase.Modules;
using ExSharpBase.Overlay.Drawing;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;

namespace ExSharpBase.Overlay
{
    public partial class Base : Form
    {
        private static bool IsInitialised;

        public Base()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void Base_Load(object sender, EventArgs e)
        {
            OnLoad();
        }

        private void Base_Paint(object sender, PaintEventArgs e)
        {
            OnPaint();
        }

        internal void OnDraw()
        {
            RenderLoop.Run(this, () =>
            {
                if (!ExSharpBase.Events.Drawing.IsMenuBeingDrawn) NativeImport.BringWindowToTop(Handle);

                DrawFactory.device.Clear(ClearFlags.Target, new RawColorBGRA(0, 0, 0, 0),
                    1.0f, 0);
                DrawFactory.device.SetRenderState(RenderState.ZEnable, false);
                DrawFactory.device.SetRenderState(RenderState.Lighting, false);
                DrawFactory.device.SetRenderState(RenderState.CullMode, Cull.None);
                DrawFactory.device.SetTransform(TransformState.Projection,
                    Matrix.OrthoOffCenterLH(0, Width, Height, 0, 0, 1));

                DrawFactory.device.BeginScene();

                ExSharpBase.Events.Drawing.OnDeviceDraw();

                DrawFactory.device.EndScene();
                DrawFactory.device.Present();
            });
        }

        internal void OnLoad()
        {
            NativeImport.SetWindowLong(Handle, DrawFactory.GWL_EXSTYLE,
                (IntPtr) (NativeImport.GetWindowLong(Handle, DrawFactory.GWL_EXSTYLE) ^ DrawFactory.WS_EX_LAYERED ^
                          DrawFactory.WS_EX_TRANSPARENT));

            NativeImport.SetLayeredWindowAttributes(Handle, 0, 255, DrawFactory.LWA_ALPHA);

            if (IsInitialised) return;
            var presentParameters = new PresentParameters
            {
                Windowed = true, SwapEffect = SwapEffect.Discard, BackBufferFormat = Format.A8R8G8B8
            };

            DrawFactory.device = new Device(DrawFactory.D3D, 0, DeviceType.Hardware, Handle,
                CreateFlags.HardwareVertexProcessing, presentParameters);

            DrawFactory.drawLine = new Line(DrawFactory.device);
            DrawFactory.drawBoxLine = new Line(DrawFactory.device);
            DrawFactory.drawCircleLine = new Line(DrawFactory.device);
            DrawFactory.drawFilledBoxLine = new Line(DrawFactory.device);
            DrawFactory.drawTriLine = new Line(DrawFactory.device);

            var fontDescription = new FontDescription
            {
                FaceName = "Fixedsys Regular",
                CharacterSet = FontCharacterSet.Default,
                Height = 20,
                Weight = FontWeight.Bold,
                MipLevels = 0,
                OutputPrecision = FontPrecision.Default,
                PitchAndFamily = FontPitchAndFamily.Default,
                Quality = FontQuality.ClearType
            };

            DrawFactory.font = new Font(DrawFactory.device, fontDescription);
            DrawFactory.InitialiseCircleDrawing(DrawFactory.device);

            IsInitialised = true;

            OnDraw();
        }

        public void OnPaint()
        {
            DrawFactory.Marg.Left = 0;
            DrawFactory.Marg.Top = 0;
            DrawFactory.Marg.Right = Width;
            DrawFactory.Marg.Bottom = Height;

            NativeImport.DwmExtendFrameIntoClientArea(Handle, ref DrawFactory.Marg);
        }
    }
}