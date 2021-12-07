using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Bitmap = System.Drawing.Bitmap;
using FlowDirection = SharpDX.DirectWrite.FlowDirection;
using FontFamily = SharpDX.DirectWrite.FontFamily;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace DirectLeak
{
    public partial class Form1 : Form
    {
        private SharpDX.DirectWrite.Factory factory;
        private DeviceContextRenderTarget dcTarget;
        private bool useDW;

        public Form1()
        {
            InitializeComponent();

            factory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
            using (var direct2DFactory = new SharpDX.Direct2D1.Factory(SharpDX.Direct2D1.FactoryType.SingleThreaded))
            {
                dcTarget = new DeviceContextRenderTarget(direct2DFactory, new RenderTargetProperties(RenderTargetType.Default, new SharpDX.Direct2D1.PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied), 0, 0, RenderTargetUsage.GdiCompatible, FeatureLevel.Level_DEFAULT));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Painting");

            using (var b = new Bitmap(200, 200, PixelFormat.Format32bppRgb))
            {
                using (var g = Graphics.FromImage(b))
                {
                    using (var bc = new System.Drawing.BufferedGraphicsContext())
                    {
                        using (var bg = bc.Allocate(g, new Rectangle(0, 0, b.Width, b.Height)))
                        {
                            var scale = 1F; //(float)b.Width / (float)sz.Width;
                            bg.Graphics.ScaleTransform(scale, scale);

                            if (useDW)
                            {
                                Draw(bg.Graphics);
                            }

                            bg.Render();
                        }
                    }
                }

                e.Graphics.DrawImage(b, Point.Empty);
            }
        }

        private void Draw(Graphics graphics)
        {
            var hdc = graphics.GetHdc();
            try
            {
                gdi32.SetGraphicsMode(hdc, GraphicsMode.Advanced);
                gdi32.SetMapMode(hdc, MapMode.Text);

                dcTarget.BindDeviceContext(hdc, new SharpDX.Mathematics.Interop.RawRectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Right, ClientRectangle.Bottom));
                dcTarget.BeginDraw();
                dcTarget.AntialiasMode = AntialiasMode.Aliased;
                dcTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Grayscale;

                using (var brush = new SolidColorBrush(dcTarget, new SharpDX.Mathematics.Interop.RawColor4(1, 0, 1, 1)))
                {
                    dcTarget.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Right, ClientRectangle.Bottom), brush);
                }

                using (var brush = new SolidColorBrush(dcTarget, new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 1)))
                using (var textFormat = new TextFormat(factory, "Open Sans", 24F))
                {
                    const string textString = "T";
                    dcTarget.DrawText(textString, textFormat, new RawRectangleF(100, 100, 200, 200), brush);
                    FontFace fontFace;
                    using (var interop = factory.GdiInterop)
                    {
                        var newLogFont = new LOGFONT
                        {
                            lfCharSet = 0,
                            lfFaceName = "Open Sans"
                        };

                        using (var font = interop.FromLogFont(newLogFont))
                            fontFace = new FontFace(font);
                    }

                    using (var analysis = new ScriptAnalysis(textString, FlowDirection.LeftToRight, factory))
                    {
                        analysis.GetGlyphs(new Range(0, 1), fontFace, false, out var clusterMap, out var textProps,
                            out var glyphIndices, out var glyphProps, out var glyphCount);
                        analysis.GetGlyphPlacements(new Range(0, 1), fontFace, 24, clusterMap, textProps, glyphIndices,
                            glyphProps, out var advances, out var offsets);

                        using (var glyphRun = new GlyphRun
                        {
                            FontSize = 24F,
                            FontFace = fontFace,
                            Advances = advances.Take(glyphCount).ToArray(),
                            BidiLevel = 0,
                            Indices = glyphIndices.Take(glyphCount).ToArray(),
                            IsSideways = false,
                            Offsets = offsets.Take(glyphCount).ToArray()
                        })
                        {
                            dcTarget.DrawGlyphRun(new RawVector2(100, 100), glyphRun, brush, MeasuringMode.Natural);
                        }
                    }
                    

                    fontFace.Dispose();
                }

                dcTarget.EndDraw();
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            useDW = true;
            this.Invalidate();
        }
    }
}
