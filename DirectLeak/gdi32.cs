using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace DirectLeak {
    /// <summary>
    /// Summary description for gdi32.
    /// </summary>
    ///

    #region Enums

    public enum FontWeight : int
    {
        DontCare = 0,
        Thin = 100,
        ExtraLight = 200,
        Light = 300,
        Normal = 400,
        Medium = 500,
        SemiBold = 600,
        Bold = 700,
        UltraBold = 800,
        Black = 900
    }

    public enum TranslateCharsetInfoSource : uint
    {
        SrcCharSet = 1,
        SrcCodePage = 2,
        SrcFontSig = 3
    }

    public enum ObjectType : int
    {
        Unknown = 0,
        Pen = 1,
        Brush = 2,
        DC = 3,
        MetaDC = 4,
        Palette = 5,
        Font = 6,
        Bitmap = 7,
        Region = 8,
        Metafile = 9,
        MemDC = 10,
        ExtPen = 11,
        EnhMetaDC = 12,
        EnhMetafile = 13
    }

    public enum GraphicsMode : int
    {
        Compatible = 1,
        Advanced = 2,
        Last = 2
    }

    public enum MapMode : int
    {
        Text = 1,
        LoMetric = 2,
        HiMetric = 3,
        LoEnglish = 4,
        HiEnglish = 5,
        Twips = 6,
        Isotropic = 7,
        AniIsotropic = 8
    }

    public enum RegionCombineMode : int
    {
        RGN_AND = 1,
        RGN_OR = 2,
        RGN_XOR = 3,
        RGN_DIFF = 4,
        RGN_COPY = 5,
        RGN_MIN = RGN_AND,
        RGN_MAX = RGN_COPY
    }

    public enum CDRF : int
    {
        CDRF_DODEFAULT = 0x00000000,
        CDRF_NEWFONT = 0x00000002,
        CDRF_SKIPDEFAULT = 0x00000004,
        CDRF_NOTIFYPOSTPAINT = 0x00000010,
        CDRF_NOTIFYITEMDRAW = 0x00000020,
        CDRF_NOTIFYSUBITEMDRAW = 0x00000020,
        CDRF_NOTIFYPOSTERASE = 0x00000040,
        CDRF_NOTIFYITEMERASE = 0x00000080
    }

    public enum TBCDRF : int
    {
        TBCDRF_NOEDGES = 0x00010000,  /* Don't draw button edges       */
        TBCDRF_HILITEHOTTRACK = 0x00020000,  /* Use color of the button bkgnd */
        TBCDRF_NOOFFSET = 0x00040000,  /* No offset button if pressed   */
        TBCDRF_NOMARK = 0x00080000,  /* Don't draw default highlight  */
        TBCDRF_NOETCHEDEFFECT = 0x00100000,  /* No etched effect for          */
        TBCDRF_BLENDICON = 0x00200000,  /* ILD_BLEND50 on the icon image */
        TBCDRF_NOBACKGROUND = 0x00400000
    }

    public enum CDDS : int
    {
        CDDS_PREPAINT = 0x00000001,
        CDDS_POSTPAINT = 0x00000002,
        CDDS_PREERASE = 0x00000003,
        CDDS_POSTERASE = 0x00000004,
        CDDS_ITEM = 0x00010000,
        CDDS_ITEMPREPAINT = CDDS_ITEM | CDDS_PREPAINT,
        CDDS_ITEMPOSTPAINT = CDDS_ITEM | CDDS_POSTPAINT,
        CDDS_ITEMPREERASE = CDDS_ITEM | CDDS_PREERASE,
        CDDS_ITEMPOSTERASE = CDDS_ITEM | CDDS_POSTERASE,
        CDDS_SUBITEM = 0x00020000
    }

    public enum CustomDrawItemStates : int
    {
        Selected = 0x0001,
        Grayed = 0x0002,
        Disabled = 0x0004,
        Checked = 0x0008,
        Focus = 0x0010,
        Default = 0x0020,
        Hot = 0x0040,
        Marked = 0x0080,
        Indeterminate = 0x0100
    }

    [Flags()]
    public enum ToolButtonState
    {
        Checked = 0x01,    // radio button is checked
        Pressed = 0x02,    // button is being depressed (any style)
        Enabled = 0x04,    // button is enabled
        Hidden = 0x08,    // button is hidden
        Indeterminate = 0x10,    // button is indeterminate
        Selected = 0x20
    }

    public enum BrushStyle
    {
        BS_SOLID = 0,
        BS_HOLLOW = 1,
        BS_NULL = 1,
        BS_HATCHED = 2,
        BS_PATTERN = 3,
        BS_INDEXED = 4,
        BS_DIBPATTERN = 5,
        BS_DIBPATTERNPT = 6,
        BS_PATTERN8X8 = 7,
        BS_DIBPATTERN8X8 = 8,
        BS_MONOPATTERN = 9
    }

    public enum HatchStyle
    {
        HS_HORIZONTAL = 0,
        HS_VERTICAL = 1,
        HS_FDIAGONAL = 2,
        HS_BDIAGONAL = 3,
        HS_CROSS = 4,
        HS_DIAGCROSS = 5
    }

    public enum ClipboardFormat : uint
    {
        CF_METAFILEPICT = 3,
        CF_ENHMETAFILE = 14
    }

    public enum MappingMode : int
    {
        MM_TEXT = 1,
        MM_LOMETRIC = 2,
        MM_HIMETRIC = 3,
        MM_LOENGLISH = 4,
        MM_HIENGLISH = 5,
        MM_TWIPS = 6,
        MM_ISOTROPIC = 7,
        MM_ANISOTROPIC = 8
    }

    public enum RegionFlags
    {
        ERROR,
        NULLREGION,
        SIMPLEREGION,
        COMPLEXREGION
    }

    [Flags]
    public enum DrawTextFlags : int
    {
        DT_TOP = 0x0,
        DT_LEFT = 0x0,
        DT_CENTER = 0x1,
        DT_RIGHT = 0x2,
        DT_VCENTER = 0x4,
        DT_BOTTOM = 0x8,
        DT_WORDBREAK = 0x10,
        DT_SINGLELINE = 0x20,
        DT_EXPANDTABS = 0x40,
        DT_TABSTOP = 0x80,
        DT_NOCLIP = 0x100,
        DT_EXTERNALLEADING = 0x200,
        DT_CALCRECT = 0x400,
        DT_NOPREFIX = 0x800,
        DT_INTERNAL = 0x1000,
        DT_EDITCONTROL = 0x2000,
        DT_PATH_ELLIPSIS = 0x4000,
        DT_END_ELLIPSIS = 0x8000,
        DT_MODIFYSTRING = 0x10000,
        DT_RTLREADING = 0x20000,
        DT_WORD_ELLIPSIS = 0x40000,
        DT_NOFULLWIDTHCHARBREAK = 0x80000,
        DT_HIDEPREFIX = 0x100000,
        DT_PREFIXONLY = 0x200000
    }

    [Flags]
    public enum PrintingFlags : int
    {
        CheckVisible = 0x01,
        NonClient = 0x02,
        Client = 0x04,
        EraseBackground = 0x80,
        Children = 0x10,
        Owned = 0x20
    }

    public enum DeviceCap : int
    {
        DRIVERVERSION = 0,
        TECHNOLOGY = 2,
        HORZSIZE = 4,
        VERTSIZE = 6,
        HORZRES = 8,
        VERTRES = 10,
        BITSPIXEL = 12,
        PLANES = 14,
        NUMBRUSHES = 16,
        NUMPENS = 18,
        NUMMARKERS = 20,
        NUMFONTS = 22,
        NUMCOLORS = 24,
        PDEVICESIZE = 26,
        CURVECAPS = 28,
        LINECAPS = 30,
        POLYGONALCAPS = 32,
        TEXTCAPS = 34,
        CLIPCAPS = 36,
        RASTERCAPS = 38,
        ASPECTX = 40,
        ASPECTY = 42,
        ASPECTXY = 44,

        LOGPIXELSX = 88,
        LOGPIXELSY = 90,

        SIZEPALETTE = 104,
        NUMRESERVED = 106,
        COLORRES = 108,

        // Printing related DeviceCaps. These replace the appropriate Escapes
        PHYSICALWIDTH = 110,
        PHYSICALHEIGHT = 111,
        PHYSICALOFFSETX = 112,
        PHYSICALOFFSETY = 113,
        SCALINGFACTORX = 114,
        SCALINGFACTORY = 115,

        DESKTOPVERTRES = 117,
        DESKTOPHORZRES = 118,
    }

    /* Ternary raster operations */
    public enum RasterOp : int
    {
        SRCCOPY = 0x00CC0020, /* dest = source                   */
        SRCPAINT = 0x00EE0086, /* dest = source OR dest           */
        SRCAND = 0x008800C6, /* dest = source AND dest          */
        SRCINVERT = 0x00660046, /* dest = source XOR dest          */
        SRCERASE = 0x00440328, /* dest = source AND (NOT dest )   */
        NOTSRCCOPY = 0x00330008, /* dest = (NOT source)             */
        NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
        MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)     */
        MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest     */
        PATCOPY = 0x00F00021, /* dest = pattern                  */
        PATPAINT = 0x00FB0A09, /* dest = DPSnoo                   */
        PATINVERT = 0x005A0049, /* dest = pattern XOR dest         */
        DSTINVERT = 0x00550009, /* dest = (NOT dest)               */
        BLACKNESS = 0x00000042, /* dest = BLACK                    */
        WHITENESS = 0x00FF0062, /* dest = WHITE                    */
    }

    [Flags]
    public enum PenStyles : uint
    {
        PS_SOLID = 0x00000000,
        PS_DASH = 0x00000001,
        PS_DOT = 0x00000002,
        PS_DASHDOT = 0x00000003,
        PS_DASHDOTDOT = 0x00000004,
        PS_NULL = 0x00000005,
        PS_INSIDEFRAME = 0x00000006,
        PS_USERSTYLE = 0x00000007,
        PS_ALTERNATE = 0x00000008,
        PS_STYLE_MASK = 0x0000000f,

        PS_ENDCAP_ROUND = 0x00000000,
        PS_ENDCAP_SQUARE = 0x00000100,
        PS_ENDCAP_FLAT = 0x00000200,
        PS_ENDCAP_MASK = 0x00000f00,

        PS_JOIN_ROUND = 0x00000000,
        PS_JOIN_BEVEL = 0x00001000,
        PS_JOIN_MITER = 0x00002000,
        PS_JOIN_MASK = 0x0000f000,

        PS_COSMETIC = 0x00000000,
        PS_GEOMETRIC = 0x00010000,
        PS_TYPE_MASK = 0x000f0000
    }

    public enum RasterOp2 : int
    {
        R2_BLACK = 1,
        R2_NOTMERGEPEN = 2,
        R2_MASKNOTPEN = 3,
        R2_NOTCOPYPEN = 4,
        R2_MASKPENNOT = 5,
        R2_NOT = 6,
        R2_XORPEN = 7,
        R2_NOTMASKPEN = 8,
        R2_MASKPEN = 9,
        R2_NOTXORPEN = 10,
        R2_NOP = 11,
        R2_MERGENOTPEN = 12,
        R2_COPYPEN = 13,
        R2_MERGEPENNOT = 14,
        R2_MERGEPEN = 15,
        R2_WHITE = 16
    }

    public enum PanoseFontFamilyType : byte
    {
        Any = 0,
        NoFit = 1,
        TextDisplay = 2,
        FamilyScript = 3,
        Decorative = 4,
        Pictorial = 5
    }

    [Flags]
    public enum GCPFlags : uint
    {
        GCP_DBCS = 0x0001,
        GCP_REORDER = 0x0002,
        GCP_USEKERNING = 0x0008,
        GCP_GLYPHSHAPE = 0x0010,
        GCP_LIGATE = 0x0020,
        GCP_DIACRITIC = 0x0100,
        GCP_KASHIDA = 0x0400,
        GCP_ERROR = 0x8000,
        GCP_JUSTIFY = 0x00010000,
        GCP_CLASSIN = 0x00080000,
        GCP_MAXEXTENT = 0x00100000,
        GCP_JUSTIFYIN = 0x00200000,
        GCP_DISPLAYZWG = 0x00400000,
        GCP_SYMSWAPOFF = 0x00800000,
        GCP_NUMERICOVERRIDE = 0x01000000,
        GCP_NEUTRALOVERRIDE = 0x02000000,
        GCP_NUMERICSLATIN = 0x04000000,
        GCP_NUMERICSLOCAL = 0x08000000,
    }

    public enum DibColorMode : uint
    {
        RgbColors = 0,
        PalColors = 1
    }

    #endregion

    #region Structs

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ENUMLOGFONTEX
    {
        public LOGFONT elfLogFont;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string elfFullName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string elfStyle;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string elfScript;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYGONHEADER
    {
        public int cb;
        public int dwType;
        [MarshalAs(UnmanagedType.Struct)]
        public POINTFX pfxStart;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYCURVEHEADER
    {
        public short wType;
        public short cpfx;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FIXED
    {
        public ushort fract;
        public short value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MAT2
    {
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM11;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM12;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM21;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM22;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINTFX
    {
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED x;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GLYPHMETRICS
    {
        public int gmBlackBoxX;
        public int gmBlackBoxY;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT gmptGlyphOrigin;
        public short gmCellIncX;
        public short gmCellIncY;
    }

    public enum METRICSFORMAT
    {
        GGO_NATIVE = 2,
        GGO_GLYPH_INDEX = 128
    }

    public enum CURVETYPE
    {
        TT_PRIM_LINE = 1,
        TT_PRIM_QSPLINE = 2,
        TT_PRIM_CSPLINE = 3
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMR
    {
        public uint iType;
        public uint nSize;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode), CLSCompliant(false)]
    public sealed class EMREXTTEXTOUTW
    {
        //public EMR     emr;
        public RECT rclBounds;
        public uint iGraphicsMode;
        public float exScale;
        public float eyScale;
        public EMRTEXT emrtext;

        public override string ToString()
        {
            return string.Format("EMRTEXTOUTW: {{rclBounds: {0}, iGraphicsMode: {1}, exScale: {2}, eyScale: {3}, emrtext: {4}}}", rclBounds, iGraphicsMode, exScale, eyScale, emrtext);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRTEXT
    {
        public POINT ptlReference;
        public uint nChars;
        public uint offString;
        public uint fOptions;
        public RECT rcl;
        public uint offDx;

        public override string ToString()
        {
            return string.Format("EMRTEXT: {{ptlReference: ({0}, {1}), nChars: {2}, offString: {3}, fOptions: {4}, rcl: {5}, offDx: {6}}}", ptlReference.X, ptlReference.Y, nChars, offString, fOptions, rcl, offDx);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class EMRMOVETO
    {
        public POINT ptl;

        public override string ToString()
        {
            return string.Format("EMRMOVETO: {{ ptl: {0} }}", ptl);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRLINETO : EMRMOVETO
    {
        public override string ToString()
        {
            return string.Format("EMRLINETO: {{ ptl: {0} }}", ptl);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class EMRPOLYLINE
    {
        public RECT rclBounds;
        public uint cpts;
        //public POINTS aptl;

        public override string ToString()
        {
            return string.Format("EMRPOLYLINE: {{ rclBounds: {0}, cpts: {1} }}", rclBounds, cpts);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class WMFPOLYGON
    {
        public EMR emr;
        public RECT rclBounds;
        public uint cpts;
        //public POINTS aptl;

        public override string ToString()
        {
            return string.Format("WMFPOLYGON: {{ rclBounds: {0}, cpts: {1} }}", rclBounds, cpts);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRPOLYGON
    {
        public int cpts;
        //public POINTS aptl;

        //public override string ToString()
        //{
        //    return string.Format("EMRPOLYGONNE: {{ rclBounds: {0}, cpts: {1} }}", rclBounds, cpts);
        //}

    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRPOLYGON16 : EMRPOLYLINE
    {
        public override string ToString()
        {
            return string.Format("EMRPOLYGON16: {{ rclBounds: {0}, cpts: {1} }}", rclBounds, cpts);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class EMRPOLYPOLYGON16
    {
        public RECT rclBounds;
        public uint nPolys;
        public uint cpts;
        //public POINTS aptl;

        public override string ToString()
        {
            return string.Format("EMRPOLYPOLYGON16: {{ rclBounds: {0}, nPolys: {1},  cpts: {2} }}", rclBounds, nPolys, cpts);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRPOLYPOLYLINE16 : EMRPOLYPOLYGON16
    {
        public override string ToString()
        {
            return string.Format("EMRPOLYPOLYLINE16: {{ rclBounds: {0}, nPolys: {1},  cpts: {2} }}", rclBounds, nPolys, cpts);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMREXTCREATEFONTINDIRECTW
    {
        public uint ihFont;
        public EXTLOGFONTW elfw;

        public override string ToString()
        {
            return string.Format("EMREXTCREATEFONTINDIRECTW: {{ ihFont: {0}, elfw: {1} }}", ihFont, elfw);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETTEXTCOLOR
    {
        public COLORREF crColor;

        public override string ToString()
        {
            return string.Format("COLORREF: {{ crColor: {0} }}", crColor);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETBKCOLOR
    {
        public COLORREF crColor;

        public override string ToString()
        {
            return string.Format("EMRSETBKCOLOR: {{ crColor: {0} }}", crColor);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRCREATEBRUSHINDIRECT
    {
        public uint ihBrush;
        public LOGBRUSH lb;

        public override string ToString()
        {
            return string.Format("EMRCREATEBRUSHINDIRECT: {{ ihBrush: {0}, lb: {1} }}", ihBrush, lb);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMREXTCREATEPEN
    {
        public uint ihPen;
        public uint offBmi;
        public uint cbBmi;
        public uint offBits;
        public uint cbBits;
        public EXTLOGPEN elp;

        public override string ToString()
        {
            return string.Format("EMREXTCREATEPEN: {{ ihPen: {0}, offBmi: {1}, cbBmi: {2}, offBits: {3}, cbBits: {4}, elp: {5} }}",
                ihPen,
                offBits,
                cbBmi,
                offBits,
                cbBits,
                elp);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSTRETCHDIBITS
    {
        public RECT rclBounds;
        public int xDest;
        public int yDest;
        public int xSrc;
        public int ySrc;
        public int cxSrc;
        public int cySrc;
        public uint offBmiSrc;
        public uint cbBmiSrc;
        public uint offBitsSrc;
        public uint cbBitsSrc;
        public uint iUsageSrc;
        public uint dwRop;
        public int cxDest;
        public int cyDest;

        public override string ToString()
        {
            return string.Format("EMRSTRETCHDIBITS: {{ rclBounds: {0}, xDest: {1}, yDest: {2}, xSrc: {3}, ySrc: {4}, cxSrc: {5}, cySrc: {6}, offBmiSrc: {7}, cbBmiSrc: {8}, offBitsSrc: {9}, cbBitsSrc: {10}, iUsageSrc: {11}, dwRop: {12}, cxDest: {13}, cyDest: {14} }}",
                rclBounds,
                xDest,
                yDest,
                xSrc,
                ySrc,
                cxSrc,
                cySrc,
                offBmiSrc,
                cbBmiSrc,
                offBitsSrc,
                cbBitsSrc,
                iUsageSrc,
                dwRop,
                cxDest,
                cyDest);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRBITBLT
    {
        public RECT rclBounds;
        public int xDest;
        public int yDest;
        public int cxDest;
        public int cyDest;
        public uint dwRop;
        public int xSrc;
        public int ySrc;
        public XFORM xformSrc;
        public COLORREF crBkColorSrc;
        public uint iUsageSrc;
        public uint offBmiSrc;
        public uint cbBmiSrc;
        public uint offBitsSrc;
        public uint cbBitsSrc;

        public override string ToString()
        {
            return string.Format("EMRBITBLT: {{ rclBounds: {0}, xDest: {1}, yDest: {2}, cxDest: {3}, cyDest: {4}, dwRop: {5}, xSrc: {6}, ySrc: {7}, xformSrc: {8}, crBkColorSrc: {9}, iUsageSrc: {10}, offBmiSrc: {11}, cbBmiSrc: {12}, offBitsSrc: {13}, cbBitsSrc: {14} }}",
                rclBounds,
                xDest,
                yDest,
                cxDest,
                cyDest,
                dwRop,
                xSrc,
                ySrc,
                xformSrc,
                crBkColorSrc,
                iUsageSrc,
                offBitsSrc,
                cbBmiSrc,
                offBitsSrc,
                cbBitsSrc);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETWORLDTRANSFORM
    {
        public XFORM xform;

        public override string ToString()
        {
            return string.Format("EMRSETWORLDTRANSFORM: {{ xform: {0} }}", xform);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRMODIFYWORLDTRANSFORM
    {
        public XFORM xform;
        public uint iMode;

        public override string ToString()
        {
            return string.Format("EMRMODIFYWORLDTRANSFORM: {{ xform: {0}, iMode: {1} }}", xform, iMode);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETTEXTALIGN
    {
        public uint iMode;

        public override string ToString()
        {
            return string.Format("EMRSETTEXTALIGN: {{ iMode: {0} }}", iMode);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETWINDOWEXT
    {
        public ushort X;
        public ushort Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSETWINDOWORG
    {
        public POINT ptlOrigin;

        public override string ToString()
        {
            return string.Format("EMRSETWINDOWORG: {{ ptlOrigin: {0} }}", ptlOrigin);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRRECTANGLE
    {
        public RECT rclBounds;

        public override string ToString()
        {
            return string.Format("EMRRECTANGLE: {{ rclBounds: {0} }}", rclBounds);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRSELECTOBJECT
    {
        public uint ihObject;

        public override string ToString()
        {
            return string.Format("EMRSELECTOBJECT: {{ ihObject: {0} }}", ihObject);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class EMRDELETEOBJECT
    {
        public uint ihObject;

        public override string ToString()
        {
            return string.Format("EMRDELETEOBJECT: {{ ihObject: {0} }}", ihObject);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public sealed class EXTLOGFONTW
    {
        public LOGFONT elfLogFont;

        public override string ToString()
        {
            return string.Format("EXTLOGFONTW: {{ lfHeight: {0}, lfWidth: {1}, lfEscapement: {2}, lfOrientation: {3}, lfWeight: {4}, lfItalic: {5}, lfUnderline: {6}, lfStrikeOut: {7}, lfCharSet: {8}, lfOutPrecision: {9}, lfClipPrecision: {10}, lfQuality: {11}, lfPitchAndFamily: {12}, lfFaceName: {13} }}",
                elfLogFont.lfHeight,
                elfLogFont.lfWidth,
                elfLogFont.lfEscapement,
                elfLogFont.lfOrientation,
                elfLogFont.lfWeight,
                elfLogFont.lfItalic,
                elfLogFont.lfUnderline,
                elfLogFont.lfStrikeOut,
                elfLogFont.lfCharSet,
                elfLogFont.lfOutPrecision,
                elfLogFont.lfClipPrecision,
                elfLogFont.lfQuality,
                elfLogFont.lfPitchAndFamily,
                elfLogFont.lfFaceName);
        }
    }

    public struct FontRange
    {
        public ushort Low;
        public ushort High;

        public bool Contains(ushort value)
        {
            return value >= Low && value < High;
        }

        public bool IsEmpty
        {
            get
            {
                return Low == 0 && High == 0;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FONTSIGNATURE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] fsUsb;// = new uint[4];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] fsCsb;// = new uint[2];
    }

    [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CHARSETINFO
    {
        public uint ciCharset;
        public uint ciACP;
        public FONTSIGNATURE fs;
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class BITMAP
    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public short bmPlanes;
        public short bmBitsPixel;
        public IntPtr bmBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
        public BITMAPINFOHEADER()
        {
            biSize = 40;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO_FLAT
    {
        public int bmiHeader_biSize;
        public int bmiHeader_biWidth;
        public int bmiHeader_biHeight;
        public short bmiHeader_biPlanes;
        public short bmiHeader_biBitCount;
        public int bmiHeader_biCompression;
        public int bmiHeader_biSizeImage;
        public int bmiHeader_biXPelsPerMeter;
        public int bmiHeader_biYPelsPerMeter;
        public int bmiHeader_biClrUsed;
        public int bmiHeader_biClrImportant;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x400)]
        public byte[] bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PALETTEENTRY
    {
        public byte peRed;
        public byte peGreen;
        public byte peBlue;
        public byte peFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XFORM
    {
        public float eM11;
        public float eM12;
        public float eM21;
        public float eM22;
        public float eDx;
        public float eDy;

        public XFORM(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            eM11 = m11;
            eM12 = m12;
            eM21 = m21;
            eM22 = m22;
            eDx = dx;
            eDy = dy;
        }

        public static XFORM Empty
        {
            get
            {
                return new XFORM(1, 0, 0, 1, 0, 0);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return eM11 == 1 && eM12 == 0 && eM21 == 0 && eM22 == 1 && eDx == 0 && eDy == 0;
            }
        }

        public Matrix ToMatrix()
        {
            return new Matrix(eM11, eM12, eM21, eM22, eDx, eDy);
        }

        public override string ToString()
        {
            return string.Format("XFORM: {{ eM11: {0}, eM12: {1}, eM21: {2}, eM22: {3}, eDx: {4}, eDy: {5} }}",
                eM11,
                eM12,
                eM21,
                eM22,
                eDx,
                eDy);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public IntPtr hwndFrom;
        public int idFrom;
        public int code;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct NMCUSTOMDRAW
    {
        public NMHDR hdr;
        public int dwDrawStage;
        public IntPtr hdc;
        public RECT rc;
        public int dwItemSpec;
        public int uItemState;
        public int lItemlParam;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTBCUSTOMDRAW
    {
        public NMCUSTOMDRAW nmcd;
        public IntPtr hbrMonoDither;
        public IntPtr hbrLines;
        public IntPtr hpenLines;
        public int clrText;
        public int clrMark;
        public int clrTextHighlight;
        public int clrBtnFace;
        public int clrBtnHighlight;
        public int clrHighlightHotTrack;
        public RECT rcText;
        public int nStringBkMode;
        public int nHLStringBkMode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NMTBGETINFOTIP
    {
        public NMHDR hdr;
        public IntPtr pszText;
        public int cchTextMax;
        public int iItem;
        public int lParam;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRIC
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public char tmFirstChar;
        public char tmLastChar;
        public char tmDefaultChar;
        public char tmBreakChar;
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct NEWTEXTMETRIC
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public char tmFirstChar;
        public char tmLastChar;
        public char tmDefaultChar;
        public char tmBreakChar;
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
        public int ntmFlags;
        public int ntmSizeEM;
        public int ntmCellHeight;
        public int ntmAvgWidth;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public sealed class LOGFONT
    {
        public int lfHeight = 0;
        public int lfWidth = 0;
        public int lfEscapement = 0;
        public int lfOrientation = 0;
        public int lfWeight = 0;
        public byte lfItalic = 0;
        public byte lfUnderline = 0;
        public byte lfStrikeOut = 0;
        public byte lfCharSet = 0;
        public byte lfOutPrecision = 0;
        public byte lfClipPrecision = 0;
        public byte lfQuality = 0;
        public byte lfPitchAndFamily = 0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName = string.Empty;

        public LOGFONT()
        {
        }

        public static LOGFONT FromFont(Font font)
        {
            var logfont = new LOGFONT();
            font.ToLogFont(logfont);
            return logfont;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINTS
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public POINT(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public static implicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        public static implicit operator POINT(Point p)
        {
            return new POINT(p.X, p.Y);
        }

        public static POINT Empty
        {
            get
            {
                return new POINT(0, 0);
            }
        }
    }

    /*public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public int fErase;
        public Rectangle rcPaint;
        public int fRestore;
        public int fIncUpdate;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int Reserved4;
        public int Reserved5;
        public int Reserved6;
        public int Reserved7;
        public int Reserved8;
    }*/

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = Top + value;
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = Left + value;
            }
        }

        public Size Size { get { return new Size(Width, Height); } }

        public Point Location { get { return new Point(Left, Top); } }

        public Rectangle ToRectangle()
        { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

        public static RECT FromRectangle(Rectangle rectangle)
        {
            return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public static RECT FromLTWH(int left, int top, int width, int height)
        {
            return new RECT(left, top, left + width, top + height);
        }

        public override string ToString()
        {
            return string.Format("X={0}, Y={1}, Width={2}, Height={3}", new object[] { Left, Top, Width, Height });
        }

        public override int GetHashCode()
        {
            return Left ^ ((Top << 13) | (Top >> 0x13))
                ^ ((Width << 0x1a) | (Width >> 6))
                ^ ((Height << 7) | (Height >> 0x19));
        }

        public static implicit operator Rectangle(RECT rect)
        {
            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static implicit operator RECT(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public void Offset(int x, int y)
        {
            Left += x;
            Right += x;
            Top += y;
            Bottom += y;
        }

        public void Inflate(int width, int height)
        {
            Left -= width;
            Top -= height;
            Right += width;
            Bottom += height;
        }

        public static RECT Empty
        {
            get
            {
                return new RECT(0, 0, 0, 0);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Left == 0 && Top == 0 && Right == 0 && Bottom == 0;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;

        public SIZE(int x, int y)
        {
            cx = x;
            cy = y;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} x {1}", cx, cy);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF
    {
        public byte R;
        public byte G;
        public byte B;

        public override string ToString()
        {
            return string.Format("COLORREF: {{ R: {0}, G: {1}, B: {2} }}", R, G, B);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BITMAPFILEHEADER
    {
        public short bfType;
        public int bfSize;
        public short bfReserved1;
        public short bfReserved2;
        public int bfOffBits;
    };

    [StructLayout(LayoutKind.Sequential)]
    public sealed class BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        //public RGBQUAD[] bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public sealed class BITMAPINFO_S
    {
        public int bmiHeader_biSize = 40;
        public int bmiHeader_biWidth;
        public int bmiHeader_biHeight;
        public short bmiHeader_biPlanes;
        public short bmiHeader_biBitCount;
        public int bmiHeader_biCompression;
        public int bmiHeader_biSizeImage;
        public int bmiHeader_biXPelsPerMeter;
        public int bmiHeader_biYPelsPerMeter;
        public int bmiHeader_biClrUsed;
        public int bmiHeader_biClrImportant;
        public byte bmiColors_rgbBlue;
        public byte bmiColors_rgbGreen;
        public byte bmiColors_rgbRed;
        public byte bmiColors_rgbReserved;
        public BITMAPINFO_S()
        {
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct METAFILEPICT
    {
        public long mm;
        public long xExt;
        public long yExt;
        public IntPtr hMF;
    }

    public struct LOGBRUSH
    {
        public BrushStyle lbStyle;    //brush style
        public uint lbColor;    //colorref RGB(...)
        public HatchStyle lbHatch;    //hatch style

        public override string ToString()
        {
            return string.Format("LOGBRUSH: {{ lbStyle: {0}, lbColor: {1}, lbHatch: {2} }}", lbStyle, lbColor, lbHatch);
        }
    }

    public struct EXTLOGPEN
    {
        public PenStyles elpPenStyle;
        public uint elpWidth;
        public BrushStyle elpBrushStyle;
        public uint lbColor;    //colorref RGB(...)
        public HatchStyle lbHatch;    //hatch style
        public IntPtr elpHatch;
        public uint elpnumEntries;

        public override string ToString()
        {
            return string.Format("EXTLOGPEN: {{ elpPenStyle: {0}, elpWidth: {1}, elpBrushStyle: {2}, lbColor: {3}, lbHatch: {4}, elpHatch: {5}, elpnumEntries: {6} }}",
                elpPenStyle,
                elpWidth,
                elpBrushStyle,
                lbColor,
                lbHatch,
                elpHatch,
                elpnumEntries);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PANOSE
    {
        //public int ulculture;
        public PanoseFontFamilyType bFamilyType;
        public byte bSerifStyle;
        public byte bWeight;
        public byte bProportion;
        public byte bContrast;
        public byte bStrokeVariation;
        public byte bArmStyle;
        public byte bLetterform;
        public byte bMidline;
        public byte bXHeight;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OUTLINETEXTMETRIC
    {
        public uint otmSize;
        [MarshalAs(UnmanagedType.Struct)]
        public TEXTMETRIC otmTextMetrics;
        public byte otmFiller;
        public PANOSE otmPanoseNumber;
        public uint otmfsSelection;
        public uint otmfsType;
        public int otmsCharSlopeRise;
        public int otmsCharSlopeRun;
        public int otmItalicAngle;
        public uint otmEMSquare;
        public int otmAscent;
        public int otmDescent;
        public uint otmLineGap;
        public uint otmsCapEmHeight;
        public uint otmsXHeight;
        [MarshalAs(UnmanagedType.Struct)]
        public RECT otmrcFontBox;
        public int otmMacAscent;
        public int otmMacDescent;
        public uint otmMacLineGap;
        public uint otmusMinimumPPEM;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT otmptSubscriptSize;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT otmptSubscriptOffset;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT otmptSuperscriptSize;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT otmptSuperscriptOffset;
        public uint otmsStrikeoutSize;
        public int otmsStrikeoutPosition;
        public int otmsUnderscoreSize;
        public int otmsUnderscorePosition;
        public IntPtr otmpFamilyName;
        public IntPtr otmpFaceName;
        public IntPtr otmpStyleName;
        public IntPtr otmpFullName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GCP_RESULTS
    {
        public int StructSize;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string OutString;
        public IntPtr Order;
        public IntPtr Dx;
        public IntPtr CaretPos;
        public IntPtr Class;
        public IntPtr Glyphs;
        public int GlyphCount;
        public int MaxFit;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ABCFLOAT
    {
        public float A;
        public float B;
        public float C;
    }

    [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 1, CharSet = CharSet.Ansi)]
    internal struct GdiTableEntry
    {
        public uint pKernelInfo;
        public ushort ProcessId;
        public ushort nCount;
        public ushort nUpper;
        public ushort nType;
        public IntPtr pUserInfo;
    }

    public sealed class GdiObject
    {
        private readonly int index = 0;
        public int Index
        {
            get
            {
                return index;
            }
        }

        private readonly ObjectType type = ObjectType.DC;
        public ObjectType Type
        {
            get
            {
                return type;
            }
        }

        private readonly int handle = 0;
        public int Handle
        {
            get
            {
                return handle;
            }
        }

        public void Delete()
        {
            switch (Type)
            {
                case ObjectType.DC:
                case ObjectType.EnhMetaDC:
                case ObjectType.MemDC:
                case ObjectType.MetaDC:
                    gdi32.DeleteDC(Handle);
                    break;
                case ObjectType.EnhMetafile:
                    gdi32.DeleteEnhMetaFile(Handle);
                    break;
                case ObjectType.Metafile:
                    gdi32.DeleteMetaFile(Handle);
                    break;
            }

            gdi32.DeleteObject(Handle);
        }

        public override string ToString()
        {
            return type.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is GdiObject)
            {
                return (obj as GdiObject).Handle == Handle;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Handle;
        }

        internal GdiObject(int index, GdiTableEntry entry)
        {
            this.index = index;
            handle = gdi32.MakeLong(index, entry.nUpper);

            var _type = gdi32.LoByte((short)entry.nUpper) & 0x7F;

            switch (_type)
            {
                case 1:
                    type = ObjectType.DC;
                    break;
                case 4:
                    type = ObjectType.Region;
                    break;
                case 5:
                    type = ObjectType.Bitmap;
                    break;
                case 8:
                    type = ObjectType.Palette;
                    break;
                case 10:
                    type = ObjectType.Font;
                    break;
                case 16:
                    type = ObjectType.Brush;
                    break;
                case 33:
                    type = ObjectType.EnhMetafile;
                    break;
                case 48:
                    type = ObjectType.Pen;
                    break;
                case 80:
                    type = ObjectType.ExtPen;
                    break;
                default:
                    type = ObjectType.Unknown;
                    break;
            }
        }
    }
    #endregion

    public delegate int EnumFontFamExProc(ref ENUMLOGFONTEX lpelfe, ref NEWTEXTMETRIC lpntme, int fontType, IntPtr lParam);

    [SuppressUnmanagedCodeSecurity()]
    public static class gdi32
    {
        public const int WHITE_BRUSH = 0;
        public const int LTGRAY_BRUSH = 1;
        public const int DKGRAY_BRUSH = 3;
        public const int BLACK_BRUSH = 4;
        public const int NULL_BRUSH = 5;
        public const int HOLLOW_BRUSH = NULL_BRUSH;
        public const int WHITE_PEN = 6;
        public const int BLACK_PEN = 7;
        public const int NULL_PEN = 8;
        public const int DC_BRUSH = 18;

        public const int MWT_IDENTITY = 1;
        public const int MWT_LEFTMULTPLY = 2;
        public const int MWT_RIGHTMULTPLY = 3;
        public const int MWT_SET = 4;

        public const int GGI_MARK_NONEXISTING_GLYPHS = 0x0001;

        public const uint GDI_ERROR = uint.MaxValue;

        #region gdi32 Functions

        public static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
        {
            var halftonePalette = Graphics.GetHalftonePalette();
            var ptr2 = SelectPalette(new HandleRef(null, dc), new HandleRef(null, halftonePalette), force ? 0 : 1);
            if ((ptr2 != IntPtr.Zero) && realizePalette)
            {
                RealizePalette(new HandleRef(null, dc));
            }
            return ptr2;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int RealizePalette(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr SelectPalette(HandleRef hdc, HandleRef hpal, int bForceBackground);

        [DllImport("gdi32", CharSet = CharSet.Unicode)]
        public static extern int GetGlyphIndices(IntPtr hDC, string lpstr, int c, IntPtr pgi, uint fl);

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetGlyphOutlineW(IntPtr hdc, int uChar, int
            fuFormat, out GLYPHMETRICS lpgm, int cbBuffer,
            IntPtr lpBuffer, ref MAT2 lpmat2);

        [DllImport("gdi32")]
        public static extern int SetTextCharacterExtra(
            IntPtr hdc,       // handle to DC
            int nCharExtra   // extra-space value
            );

        [DllImport("gdi32")]
        public static extern int GetTextCharsetInfo(IntPtr hDC, ref FONTSIGNATURE lpSig, int dwFlags);

        [DllImport("gdi32.dll")]
        public static extern int GetTextFace(IntPtr hdc, int nCount,
           [Out] StringBuilder lpFaceName);

        public static string GetTextFace(IntPtr hDC)
        {
            StringBuilder sb = null;
            var count = GetTextFace(hDC, 0, null);
            if (count > 0)
            {
                sb = new StringBuilder(count);
                GetTextFace(hDC, count, sb);
                return sb.ToString();
            }

            return string.Empty;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool TranslateCharsetInfo(ref byte[] pSrc, out CHARSETINFO lpCs,
            TranslateCharsetInfoSource dwFlags);

        [DllImport("gdi32.dll")]
        public static extern uint GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs);

        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr CreateDIBSection(HandleRef hdc, ref BITMAPINFO_FLAT bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);

        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr CreateDIBSection(HandleRef hdc, ref BITMAPINFO bmi, int iUsage, ref IntPtr ppvBits, IntPtr hSection, int dwOffset);

        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, BITMAPINFO_S bmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern ObjectType GetObjectType(HandleRef hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern ObjectType GetObjectType(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern ObjectType GetObjectType(uint hObject);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool BeginPath(HandleRef hdc);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool EndPath(HandleRef hdc);

        [DllImport("gdi32.dll")]
        public static extern int GetDIBits(HandleRef hdc, HandleRef hbmp, int uStartScan, int cScanLines, IntPtr lpvBits, ref BITMAPINFO_FLAT bmi, int uUsage);

        [DllImport("gdi32.dll")]
        public static extern uint GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, byte[] lppe);

        [DllImport("gdi32")]
        public static extern int GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DPtoLP(IntPtr hdc, ref POINT[] lpPoints, int nCount);

        [DllImport("gdi32.dll")]
        public static extern int SetGraphicsMode(IntPtr hDC, GraphicsMode iMode);

        [DllImport("gdi32.dll")]
        public static extern int SetGraphicsMode(SafeHandle hDC, GraphicsMode iMode);

        [DllImport("gdi32.dll")]
        public static extern int SaveDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int RestoreDC(IntPtr hDC, int nSavedDC);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool GetWorldTransform(
            IntPtr hdc,            // Handle to a Win32 device context
            ref XFORM lpXform   // Transformation data
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool SetWorldTransform(
            IntPtr hdc,            // Handle to a Win32 device context
            ref XFORM lpXform   // Transformation data
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool ModifyWorldTransform(
            IntPtr hdc,            // Handle to a Win32 device context
            ref XFORM lpXform,  // Transformation data
            int iMode
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool CombineTransform(
            ref XFORM lpxformResult,
            ref XFORM lpXform1,  // Transformation data
            ref XFORM lpXform2  // Transformation data
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, [In, Out] SIZE size);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, IntPtr size);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "SetViewportExtEx", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetViewportExtEx(HandleRef hDC, int x, int y, [In, Out] SIZE size);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "SetViewportExtEx", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetViewportExtEx(HandleRef hDC, int x, int y, IntPtr size);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetViewportOrgEx(HandleRef hDC, [In, Out] POINT point);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool SetViewportOrgEx(
            IntPtr hdc, // Handle to a Win32 device context
            int x,
            int y,
            ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool SetWindowOrgEx(
            IntPtr hdc, // Handle to a Win32 device context
            int x,
            int y,
            ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool OffsetWindowOrgEx(
            IntPtr hdc, // Handle to a Win32 device context
            int x,
            int y,
            ref POINT lpPoint
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool ScaleViewportExtEx(
            IntPtr hdc, // Handle to a Win32 device context
            int xnum,
            int xdenom,
            int ynum,
            int ydenom,
            ref SIZE lpSize
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool ScaleWindowExtEx(
            IntPtr hdc, // Handle to a Win32 device context
            int xnum,
            int xdenom,
            int ynum,
            int ydenom,
            ref SIZE lpSize
            );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool Rectangle(
            IntPtr hdc,            // Handle to a Win32 device context
            int X1,                // x-coordinate of top left corner
            int Y1,                // y-cordinate of top left corner
            int X2,                // x-coordinate of bottom right corner
            int Y2                // y-coordinate of bottm right corner
            );

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateDC(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszDriver,
            [MarshalAs(UnmanagedType.LPTStr)] string lpszDevice,
            [MarshalAs(UnmanagedType.LPTStr)] string lpszOutput,
            IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        public static extern MapMode SetMapMode(IntPtr hdc, MapMode fnMapMode);

        [DllImport("gdi32.dll")]
        public static extern int SetDCBrushColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1,
            IntPtr hrgnSrc2, RegionCombineMode fnCombineMode);

        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(HandleRef hrgnDest, HandleRef hrgnSrc1,
            HandleRef hrgnSrc2, RegionCombineMode fnCombineMode);

        [DllImport("gdi32.dll")]
        public static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateBrushIndirect([In] ref LOGBRUSH lplb);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, uint crColor);

        [DllImport("gdi32.dll")]
        public static extern int AddFontResourceEx(string lpszFilename, uint fl, IntPtr pdv);

        //[DllImport("gdi32.dll")]
        //public static extern int AddFontResource(string lpszFilename);

        [DllImport("gdi32.dll")]
        public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("user32.dll", EntryPoint = "DrawTextW", CharSet = CharSet.Unicode)]
        public static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, DrawTextFlags uFormat);

        [DllImport("user32.dll", EntryPoint = "DrawTextW", CharSet = CharSet.Unicode)]
        public static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, TextFormatFlags uFormat);

        public static int DrawText(Graphics g, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues)
        {
            return DrawText(g, text, font, textColor, textAlign, r, useMnemonic, showKeyboardCues, true);
        }

        public static int DrawText(Graphics g, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues, bool wordWrap)
        {
            return DrawText(g, text, font, textColor, textAlign, r, useMnemonic, showKeyboardCues, wordWrap, DrawTextFlags.DT_LEFT);
        }

        public static int DrawText(Graphics g, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues, bool wordWrap,
            DrawTextFlags flags)
        {
            var clipbounds = System.Drawing.Rectangle.Round(g.ClipBounds);

            var pts = new Point[] {r.Location, new Point(r.Right, r.Bottom),
                clipbounds.Location, new Point(clipbounds.Right, clipbounds.Bottom)};

            using (var transform = g.Transform)
            {
                transform.TransformPoints(pts);
            }

            r = System.Drawing.Rectangle.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
            clipbounds = System.Drawing.Rectangle.FromLTRB(pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);

            var hdc = g.GetHdc();
            try
            {
                var savedc = SaveDC(hdc);
                try
                {

                    var hrgn = CreateRectRgn(clipbounds.Left, clipbounds.Top, clipbounds.Right, clipbounds.Bottom);
                    SelectClipRgn(hdc, hrgn);
                    DeleteObject(hrgn);

                    return DrawText(hdc, text, font, textColor, textAlign, r, useMnemonic, showKeyboardCues, wordWrap, flags);
                }
                finally
                {
                    RestoreDC(hdc, savedc);
                }
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
        }

        public static int DrawText(IntPtr hdc, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues)
        {
            return DrawText(hdc, text, font, textColor, textAlign, r, useMnemonic, showKeyboardCues, true);
        }

        public static int DrawText(Graphics g, string text, Font font, Color textColor, Rectangle r, StringFormat sf)
        {
            var flags = DrawTextFlags.DT_NOCLIP;

            // Word Wrap?
            if ((sf.FormatFlags & StringFormatFlags.NoWrap) != StringFormatFlags.NoWrap)
            {
                flags |= DrawTextFlags.DT_WORDBREAK;
            }
            else
            {
                flags |= DrawTextFlags.DT_SINGLELINE;
            }

            // Trimming?
            if (sf.Trimming == StringTrimming.EllipsisCharacter)
            {
                flags |= DrawTextFlags.DT_END_ELLIPSIS;
            }
            else if (sf.Trimming == StringTrimming.EllipsisPath)
            {
                flags |= DrawTextFlags.DT_PATH_ELLIPSIS;
            }
            else if (sf.Trimming == StringTrimming.EllipsisWord)
            {
                flags |= DrawTextFlags.DT_WORD_ELLIPSIS;
            }

            // Mnemonics?
            if (sf.HotkeyPrefix == HotkeyPrefix.Hide)
            {
                flags |= DrawTextFlags.DT_HIDEPREFIX;
            }
            else if (sf.HotkeyPrefix == HotkeyPrefix.None)
            {
                flags |= DrawTextFlags.DT_NOPREFIX;
            }

            // Alignment
            if (sf.Alignment == StringAlignment.Near)
            {
                flags |= DrawTextFlags.DT_LEFT;
            }
            else if (sf.Alignment == StringAlignment.Center)
            {
                flags |= DrawTextFlags.DT_CENTER;
            }
            else if (sf.Alignment == StringAlignment.Far)
            {
                flags |= DrawTextFlags.DT_RIGHT;
            }

            // Line Alignment
            flags |= DrawTextFlags.DT_TOP;
            if (sf.LineAlignment == StringAlignment.Center)
            {
                var h = (r.Height - DrawText(g, text, font, textColor, r, flags | DrawTextFlags.DT_CALCRECT)) / 2;
                if (h < 0)
                {
                    h = 0;
                    flags &= ~DrawTextFlags.DT_NOCLIP;
                    flags |= DrawTextFlags.DT_EDITCONTROL;
                }

                r.Offset(0, h);
            }
            else if (sf.LineAlignment == StringAlignment.Far)
            {
                r.Offset(0, r.Height - DrawText(g, text, font, textColor, r, flags | DrawTextFlags.DT_CALCRECT));
            }

            return DrawText(g, text, font, textColor, r, flags);
        }

        public static int DrawText(IntPtr hdc, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues, bool wordWrap)
        {
            return DrawText(hdc, text, font, textColor, textAlign, r, useMnemonic, showKeyboardCues, wordWrap, DrawTextFlags.DT_LEFT);
        }

        public static int DrawText(IntPtr hdc, string text, Font font, Color textColor, ContentAlignment textAlign, Rectangle r, bool useMnemonic, bool showKeyboardCues, bool wordWrap,
            DrawTextFlags defaultFlags)
        {
            var flags = defaultFlags | DrawTextFlags.DT_NOCLIP;

            if (wordWrap)
            {
                flags |= DrawTextFlags.DT_WORDBREAK;
            }
            else
            {
                flags |= DrawTextFlags.DT_SINGLELINE;
            }

            if (useMnemonic && !showKeyboardCues)
            {
                flags |= DrawTextFlags.DT_HIDEPREFIX;
            }
            else if (!useMnemonic)
            {
                flags |= DrawTextFlags.DT_NOPREFIX;
            }

            var rect = r;

            if (textAlign == ContentAlignment.TopCenter ||
                textAlign == ContentAlignment.MiddleCenter ||
                textAlign == ContentAlignment.BottomCenter)
            {
                flags |= DrawTextFlags.DT_CENTER;
            }
            else if (textAlign == ContentAlignment.TopRight ||
                textAlign == ContentAlignment.MiddleRight ||
                textAlign == ContentAlignment.BottomRight)
            {
                flags |= DrawTextFlags.DT_RIGHT;
            }

            if (textAlign == ContentAlignment.BottomLeft ||
                textAlign == ContentAlignment.BottomCenter ||
                textAlign == ContentAlignment.BottomRight)
            {
                flags |= DrawTextFlags.DT_BOTTOM;
            }
            else if (textAlign == ContentAlignment.MiddleLeft ||
                textAlign == ContentAlignment.MiddleCenter ||
                textAlign == ContentAlignment.MiddleRight)
            {
                var h = (rect.Height - DrawText(hdc, text, font, textColor, rect, flags | DrawTextFlags.DT_CALCRECT)) / 2;
                if (h < 0)
                {
                    h = 0;
                    flags &= ~DrawTextFlags.DT_NOCLIP;
                    flags |= DrawTextFlags.DT_EDITCONTROL;
                }

                rect.Offset(0, h);
            }

            return DrawText(hdc, text, font, textColor, rect, flags);
        }

        public static int DrawText(Graphics g, string text, Font font, POINT location, Color textColor, Color backgroundColor, TextFormatFlags format)
        {
            return DrawText(g, text, font, new Rectangle(location, new Size(int.MaxValue, int.MaxValue)), textColor, backgroundColor, format);
        }

        public static int DrawText(Graphics g, string text, Font font, Rectangle bounds, Color textColor, Color backgroundColor, TextFormatFlags format)
        {
            var clipbounds = System.Drawing.Rectangle.Round(g.ClipBounds);

            var pts = new Point[] {bounds.Location, new Point(bounds.Right, bounds.Bottom),
                clipbounds.Location, new Point(clipbounds.Right, clipbounds.Bottom)};

            using (var transform = g.Transform)
            {
                transform.TransformPoints(pts);
            }

            bounds = System.Drawing.Rectangle.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
            clipbounds = System.Drawing.Rectangle.FromLTRB(pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);

            var hdc = g.GetHdc();
            try
            {
                var savedc = SaveDC(hdc);
                try
                {

                    var hrgn = CreateRectRgn(clipbounds.Left, clipbounds.Top, clipbounds.Right, clipbounds.Bottom);
                    SelectClipRgn(hdc, hrgn);
                    DeleteObject(hrgn);

                    var transparantBackground = backgroundColor.IsEmpty || backgroundColor == Color.Transparent;
                    if (transparantBackground)
                    {
                        SetBkMode(hdc, TRANSPARENT);
                    }
                    else
                    {
                        SetBkMode(hdc, OPAQUE);
                        SetBkColor(hdc, backgroundColor.ToWin32Color());
                    }

                    return DrawText(hdc, text, font, textColor, bounds, format);
                }
                finally
                {
                    RestoreDC(hdc, savedc);
                }
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
        }

        public static int DrawText(Graphics g, string text, Font font, Rectangle bounds, Color textColor, TextFormatFlags format)
        {
            return DrawText(g, text, font, bounds, textColor, Color.Transparent, format);
        }

        public static int DrawText(Graphics g, string text, Font font, Point location, Color textColor, TextFormatFlags format)
        {
            return DrawText(g, text, font, new Rectangle(location, new Size(int.MaxValue, int.MaxValue)), textColor, format);
        }

        public static uint ToWin32Color(this Color color)
        {
            return (uint)color.R | (uint)color.G << 8 | (uint)color.B << 16;
        }

        public static int DrawText(Graphics g, string text, Font font, Color textColor, Rectangle r, DrawTextFlags format)
        {
            var clipbounds = System.Drawing.Rectangle.Round(g.ClipBounds);

            var pts = new Point[] {r.Location, new Point(r.Right, r.Bottom),
                clipbounds.Location, new Point(clipbounds.Right, clipbounds.Bottom)};

            using (var transform = g.Transform)
            {
                transform.TransformPoints(pts);
            }

            r = System.Drawing.Rectangle.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
            clipbounds = System.Drawing.Rectangle.FromLTRB(pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);

            var hdc = g.GetHdc();
            try
            {
                var savedc = SaveDC(hdc);
                try
                {

                    var hrgn = CreateRectRgn(clipbounds.Left, clipbounds.Top, clipbounds.Right, clipbounds.Bottom);
                    SelectClipRgn(hdc, hrgn);
                    DeleteObject(hrgn);

                    return DrawText(hdc, text, font, textColor, r, format);
                }
                finally
                {
                    RestoreDC(hdc, savedc);
                }
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
        }

        public static Size CalcTextSize(Graphics g, string text, Font font)
        {
            return CalcTextSize(g, text, font, 0x7fffffff);
        }

        public static Size CalcTextSize(Graphics g, string text, Font font, int width)
        {
            return CalcTextSize(g, text, font, width, true);
        }

        public static Size CalcTextSize(Graphics g, string text, Font font, int width, bool allowWrapping)
        {
            var drawTextFlags = allowWrapping ? DrawTextFlags.DT_CALCRECT | DrawTextFlags.DT_WORDBREAK : DrawTextFlags.DT_CALCRECT | DrawTextFlags.DT_SINGLELINE;
            var r = new RECT(0, 0, width, 0);
            var hdc = g.GetHdc();
            try
            {
                var hfont = font.ToHfont();
                var oldhfont = SelectObject(hdc, hfont);
                DrawText(hdc, text, text.Length, ref r, drawTextFlags);
                SelectObject(hdc, oldhfont);
                DeleteObject(hfont);
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
            return new Size(r.Width, r.Height);
        }

        public static int DrawText(IntPtr hdc, string text, Font font, Color textColor, Rectangle r, TextFormatFlags format)
        {
            RECT rect = r;
            var hfont = font.ToHfont();
            var oldhfont = SelectObject(hdc, hfont);
            SetBkMode(hdc, TRANSPARENT);
            SetTextColor(hdc, ColorTranslator.ToWin32(textColor));
            if (((format & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter) &
                ((format & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak))
            {
                // If we have Vertical Center and Wrapping we need to calculate the offset.
                //https://msdn.microsoft.com/en-us/library/windows/desktop/dd162498(v=vs.85).aspx
                rect = CalculateCenteredTextOffset(hdc, text, font, r);
            }

            var h = DrawText(hdc, text, text.Length, ref rect, format);
            SelectObject(hdc, oldhfont);
            DeleteObject(hfont);
            return h;
        }

        private static Rectangle CalculateCenteredTextOffset(IntPtr hdc, string text, Font font, Rectangle boundsRectangle)
        {
            using (var graphics = Graphics.FromHdc(hdc))
            {
                var noWrappedSize = CalcTextSize(graphics, text, font, boundsRectangle.Width, false);
                var topOffset = Math.Max(0, (boundsRectangle.Height - noWrappedSize.Height) / 2);

                if (noWrappedSize.Width > boundsRectangle.Width)
                {
                    var wrappedSize = CalcTextSize(graphics, text, font, boundsRectangle.Width, true);

                    topOffset = noWrappedSize.Width != wrappedSize.Width ? Math.Max(0, (boundsRectangle.Height - wrappedSize.Height) / 2) : 0;
                }

                boundsRectangle.Offset(0, topOffset);
                boundsRectangle.Height -= topOffset;
            }

            return boundsRectangle;
        }

        public static int DrawText(IntPtr hdc, string text, Font font, Color textColor, Rectangle r, DrawTextFlags format)
        {
            RECT rect = r;
            var hfont = font.ToHfont();
            var oldhfont = SelectObject(hdc, hfont);
            SetBkMode(hdc, TRANSPARENT);
            SetTextColor(hdc, ColorTranslator.ToWin32(textColor));
            var h = DrawText(hdc, text, text.Length, ref rect, format);
            SelectObject(hdc, oldhfont);
            DeleteObject(hfont);
            return h;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern int EnumFontFamiliesEx(IntPtr hdc, [In] IntPtr lpLogfont, EnumFontFamExProc lpEnumFontFamExProc, IntPtr lParam, uint dwFlags);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement,
            int nOrientation, FontWeight fnWeight, uint fdwItalic, uint fdwUnderline, uint
            fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint
            fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFontIndirect([In, Out, MarshalAs(UnmanagedType.AsAny)] object lf);

        [DllImport("gdi32.dll", EntryPoint = "GetPixel")]
        private static extern int IntGetPixel(IntPtr hDC, int x, int y);

        public static Color GetPixel(IntPtr hDC, int x, int y)
        {
            var colorRef = IntGetPixel(hDC, x, y);
            return Color.FromArgb(
                (int)(colorRef & 0x000000FF),
                (int)(colorRef & 0x0000FF00) >> 8,
                (int)(colorRef & 0x00FF0000) >> 16);
        }

        [DllImport("gdi32.dll", EntryPoint = "SetPixel")]
        private static extern int IntSetPixel(IntPtr hDC, int x, int y, int color);

        public static void SetPixel(IntPtr hDC, int x, int y, Color color)
        {
            var argb = color.ToArgb();
            var colorRef =
                (int)((argb & 0x00FF0000) >> 16) |
                (int)(argb & 0x0000FF00) |
                (int)((argb & 0x000000FF) << 16);
            IntSetPixel(hDC, x, y, colorRef);
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyles fnPenStyle, int nWidth, uint crColor);

        [DllImport("gdi32")]
        public static extern IntPtr ExtCreatePen(PenStyles dwPenStyle, uint dwWidth, ref LOGBRUSH lplb, uint dwStyleCount, uint[] lpStyle);

        [DllImport("gdi32")]
        public static extern IntPtr ExtCreatePen(PenStyles dwPenStyle, uint dwWidth, ref LOGBRUSH lplb, uint dwStyleCount, IntPtr lpStyle);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(HandleRef hdc, HandleRef hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(SafeHandle hdc, SafeHandle hgdiobj);

        public static IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj)
        {
            return SelectObject(new HandleRef(null, hdc), new HandleRef(null, hgdiobj));
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(int hObject);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(HandleRef hObject);

        public static bool DeleteObject(IntPtr hObject)
        {
            return DeleteObject(new HandleRef(null, hObject));
        }

        public static int RGB(Color color)
        {
            var RGBCOLORS = new byte[4];
            RGBCOLORS[0] = color.R;
            RGBCOLORS[1] = color.G;
            RGBCOLORS[2] = color.B;
            RGBCOLORS[3] = 0;
            return BitConverter.ToInt32(RGBCOLORS, 0);
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr IntersectClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        public static IntPtr IntersectClipRect(IntPtr hdc, Rectangle clipRect)
        {
            return IntersectClipRect(hdc, clipRect.Left, clipRect.Top, clipRect.Right + 1, clipRect.Bottom + 1);
        }

        public static IntPtr IntersectClipRect(IntPtr hdc, RectangleF clipRect)
        {
            var r = System.Drawing.Rectangle.Round(clipRect);
            return IntersectClipRect(hdc, r);
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2,
            int cx, int cy);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern int GetClipBox(IntPtr hdc, out RECT lprc);

        [DllImport("gdi32.dll")]
        public static extern int GetClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern int GetClipRgn(HandleRef hdc, HandleRef hrgn);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetRgnBox(HandleRef hRegion, ref RECT clipRect);

        [DllImport("gdi32.dll")]
        public static extern RegionFlags SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        public const int TRANSPARENT = 1;
        public const int OPAQUE = 2;

        public const int ETO_OPAQUE = 0x0002;
        public const int ETO_CLIPPED = 0x0004;
        public const int ETO_GLYPH_INDEX = 0x0010;
        public const int ETO_PDY = 0x2000;
        public const int ETO_RTLREADING = 0x0080;

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC(int hDc);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC(HandleRef hDc);

        public static bool DeleteDC(IntPtr hDc)
        {
            return DeleteDC(new HandleRef(null, hDc));
        }


        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest,
            int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(HandleRef hdcDest, int xDest, int yDest, int wDest, int hDest,
            HandleRef hdcSource, int xSrc, int ySrc, RasterOp rasterOp);

        public static bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
            IntPtr hdcSource, int xSrc, int ySrc, RasterOp rasterOp)
        {
            return BitBlt(new HandleRef(null, hdcDest), xDest, yDest, wDest, hDest, new HandleRef(null, hdcSource),
                xSrc, ySrc, rasterOp);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest,
            int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc,
            int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            RasterOp dwRop);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(HandleRef hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(HandleRef hdc);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        public const long CBM_INIT = 0x04L;
        public const uint DIB_RGB_COLORS = 0;

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBitmap(IntPtr hdc, [In] ref BITMAPINFOHEADER
            lpbmih, int fdwInit, IntPtr lpbInit, [In] ref BITMAPINFO lpbmi, uint fuUsage);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBitmap(IntPtr hdc, IntPtr lpbmih, int fdwInit,
            IntPtr lpbInit, IntPtr lpbmi, uint fuUsage);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, DeviceCap nIndex);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, string lpFilename,
            [In] ref RECT lpRect, string lpDescription);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, IntPtr lpFilename,
            [In] ref RECT lpRect, IntPtr lpDescription);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, string lpFilename,
            IntPtr lpRect, string lpDescription);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, IntPtr lpFilename,
            IntPtr lpRect, IntPtr lpDescription);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, string lpFilename,
            IntPtr lpRect, IntPtr lpDescription);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CloseEnhMetaFile(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern uint GetWinMetaFileBits(IntPtr hemf, uint cbBuffer,
            [Out] IntPtr lpbBuffer, MappingMode fnMapMode, IntPtr hdcRef);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool ExtTextOutW(HandleRef hdc, int x, int y, int options, ref RECT rect, string str, int length, int[] spacing);

        [DllImport("gdi32.dll")]
        public static extern int GetBitmapBits(IntPtr hbmp, int cbBuffer,
            [Out] IntPtr lpvBits);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] BITMAP bm);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In, Out] LOGFONT lf);

        public static int GetObject(HandleRef hObject, LOGFONT lp)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof(LOGFONT)), lp);
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr SetWinMetaFileBits(uint cbBuffer, IntPtr lpbBuffer,
            IntPtr hdcRef, [In] ref METAFILEPICT lpmfp);

        [DllImport("gdi32.dll")]
        public static extern int GetEnhMetaFileBits(IntPtr hemf, int cbBuffer, byte[] lpbBuffer);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CopyEnhMetaFile(  // Copy EMF to file
            IntPtr hemfSrc,   // Handle to EMF
            string lpszFile // Filelic
        );

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteMetaFile(int hmf);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteEnhMetaFile(int hemf);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteEnhMetaFile(IntPtr hemf);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool PlayEnhMetaFile(IntPtr hdc, IntPtr hemf, ref RECT lpRect);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool GetTextMetrics(IntPtr hdc, out TextMetrics lptm);

        public static TextMetrics GetTextMetrics(Graphics graphics, Font font)
        {
            var hDC = IntPtr.Zero;
            TextMetrics textMetric;
            var hFont = IntPtr.Zero;
            try
            {
                hDC = graphics.GetHdc();
                hFont = font.ToHfont();
                var hFontDefault = SelectObject(hDC, hFont);
                var result = GetTextMetrics(hDC, out textMetric);
                SelectObject(hDC, hFontDefault);
            }
            finally
            {
                if (hFont != IntPtr.Zero)
                {
                    DeleteObject(hFont);
                }
                if (hDC != IntPtr.Zero)
                {
                    graphics.ReleaseHdc(hDC);
                }
            }
            return textMetric;
        }

        public static TextMetrics GetTextMetrics(Font font)
        {
            using (var b = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(b))
                {
                    return GetTextMetrics(g, font);
                }
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool GetTextExtentPoint(IntPtr hdc, string lpString,
            int cbString, ref Size lpSize);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool GetTextExtentExPoint(IntPtr hdc, string lpszStr,
            int cchString, int nMaxExtent, ref int lpnFit, int[] alpDx, ref Size lpSize);

        // Use Interop to call the corresponding Win32 GDI functions
        [DllImport("gdi32.dll")]
        public static extern int SetROP2(
            IntPtr hdc,        // Handle to a Win32 device context
            RasterOp enDrawMode    // Drawing mode
            );

        [DllImport("gdi32.dll")]
        public static extern int SetROP2(
            IntPtr hdc,        // Handle to a Win32 device context
            RasterOp2 enDrawMode    // Drawing mode
            );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(
            PenStyles enPenStyle,    // Pen style from enum PenStyles
            int nWidth,                // Width of pen
            int crColor                // Color of pen
            );

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(int fnObject);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool Polyline(IntPtr hdc, POINT[] lppt, int cPoints);

        [DllImport("gdi32")]
        public static extern int PolyPolyline(IntPtr hdC, POINT[] lppt, int[] lpdwPolyPoints, int cCount);

        [DllImport("gdi32", CharSet = CharSet.Unicode)]
        public static extern int GetOutlineTextMetrics(
            IntPtr hdc,            // handle to DC
            int cbData,            // size in bytes for text metrics
            ref OUTLINETEXTMETRIC lpOtm  // pointer to buffer to receive outline text metrics structure
            );

        [DllImport("gdi32", CharSet = CharSet.Unicode)]
        public static extern int GetOutlineTextMetrics(
            IntPtr hdc,   // handle to DC
            int cbData,   // size in bytes for text metrics
            IntPtr lpOtm  // pointer to buffer to receive outline text metrics structure
            );

        [DllImport("gdi32.dll", EntryPoint = "GetCharacterPlacementW")]
        public static extern uint GetCharacterPlacement(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string lpString, int nCount, int nMaxExtent, ref GCP_RESULTS lpResults, uint dwFlags);

        [DllImport("gdi32.dll")]
        public static extern bool GetCharABCWidthsFloat(IntPtr hdc, uint iFirstChar, uint iLastChar, [Out] ABCFLOAT[] lpABCF);

        #endregion

        public static void DrawRoundRect(Graphics g, Rectangle r, Pen p, Color c, int ellipseWidth, int ellipseHeight)
        {
            var rect = new RECT(r.Left, r.Top, r.Right, r.Bottom);

            var dc = g.GetHdc();
            try
            {
                var pen = CreatePen(0, (int)p.Width, (uint)RGB(p.Color));
                IntPtr brush;
                if (c.IsEmpty)
                {
                    var lb = new LOGBRUSH();
                    lb.lbColor = (uint)RGB(c);
                    lb.lbHatch = HatchStyle.HS_HORIZONTAL;
                    lb.lbStyle = BrushStyle.BS_NULL;
                    brush = CreateBrushIndirect(ref lb);
                }
                else
                {
                    brush = CreateSolidBrush((uint)RGB(c));
                }

                var savepen = SelectObject(dc, pen);
                var savebrush = SelectObject(dc, brush);

                RoundRect(dc, r.Left, r.Top, r.Right, r.Bottom, ellipseWidth, ellipseHeight);

                SelectObject(dc, savepen);
                DeleteObject(pen);
                SelectObject(new HandleRef(null, dc), new HandleRef(null, savebrush));
                DeleteObject(brush);
            }
            finally
            {
                g.ReleaseHdc(dc);
            }
        }

        public static void DrawRectangle(IntPtr hdc, Rectangle r, Pen p, Color c)
        {
            var rect = new RECT(r.Left, r.Top, r.Right, r.Bottom);

            var pen = CreatePen(0, (int)p.Width, (uint)RGB(p.Color));
            IntPtr brush;
            if (c.IsEmpty)
            {
                var lb = new LOGBRUSH();
                lb.lbColor = (uint)RGB(c);
                lb.lbHatch = HatchStyle.HS_HORIZONTAL;
                lb.lbStyle = BrushStyle.BS_NULL;
                brush = CreateBrushIndirect(ref lb);
            }
            else
            {
                brush = CreateSolidBrush((uint)RGB(c));
            }

            var savepen = SelectObject(hdc, pen);
            var savebrush = SelectObject(hdc, brush);

            Rectangle(hdc, r.Left, r.Top, r.Right, r.Bottom);

            SelectObject(hdc, savepen);
            DeleteObject(pen);
            SelectObject(new HandleRef(null, hdc), new HandleRef(null, savebrush));
            DeleteObject(brush);
        }

        public static IntPtr AddFontResource(string resourceName)
        {
            using (var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                var bytes = (int)stream.Length;

                var data = Marshal.AllocCoTaskMem((int)stream.Length * 2);
                try
                {
                    var fontdata = new byte[bytes];

                    stream.Read(fontdata, 0, bytes);
                    Marshal.Copy(fontdata, 0, data, bytes);

                    uint pcfonts = 0;
                    return AddFontMemResourceEx(data, (uint)bytes, IntPtr.Zero, ref pcfonts);
                }
                finally
                {
                    Marshal.FreeCoTaskMem(data);
                }
            }
        }

        public static void AddFontResourceFromFile(string fileName)
        {
            AddFontResourceEx(fileName, 0, IntPtr.Zero);
        }

        public static int GetTextWidth(IntPtr hdc, string text)
        {
            var lpSize = Size.Empty;
            GetTextExtentPoint(hdc, text, text.Length, ref lpSize);
            return lpSize.Width;
        }

        public static int GetTextHeight(IntPtr hdc, string text)
        {
            var lpSize = Size.Empty;
            GetTextExtentPoint(hdc, text, text.Length, ref lpSize);
            return lpSize.Height;
        }

        public static int GetTextWidth(Graphics g, string text, Font font, int width)
        {
            var h = 0;

            var hdc = g.GetHdc();
            try
            {
                var hfont = font.ToHfont();
                var oldhfont = SelectObject(hdc, hfont);
                h = GetTextWidth(hdc, width, text);
                SelectObject(hdc, oldhfont);
                DeleteObject(hfont);
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
            return h;
        }

        public static int GetTextWidth(IntPtr hdc, int width, string text)
        {
            var lpnFit = 0;
            var alpDx = new int[text.Length];
            var lpSize = Size.Empty;
            GetTextExtentExPoint(hdc, text, text.Length, width, ref lpnFit, alpDx, ref lpSize);

            //lpSize.Width = 0;
            //foreach(int i in alpDx)
            //    if (i > lpSize.Width)
            //        lpSize.Width = i;

            //return lpSize.Width;
            return alpDx[lpnFit - 1];
        }

        public static Size GetTextExtent(Graphics g, string text, Font font, int width)
        {
            var lpSize = Size.Empty;

            var hdc = g.GetHdc();
            try
            {
                var hfont = font.ToHfont();
                var oldhfont = SelectObject(hdc, hfont);

                var lpnFit = 0;
                var alpDx = new int[text.Length];
                GetTextExtentExPoint(hdc, text, text.Length, width, ref lpnFit, alpDx, ref lpSize);

                SelectObject(hdc, oldhfont);
                DeleteObject(hfont);
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
            return lpSize;
        }

        public static int LoByte(short dwValue)
        {
            return dwValue & 0xFF;
        }

        public static int LoByte(IntPtr dwValue)
        {
            return LoByte((short)dwValue);
        }

        public static int HiByte(short dwValue)
        {
            return (dwValue >> 8) & 0xFF;
        }

        public static int HiByte(IntPtr dwValue)
        {
            return HiByte((short)dwValue);
        }

        public static int MakeWord(short LoByte, short HiByte)
        {
            return (HiByte << 8) | (LoByte & 0xff);
        }

        public static int LoWord(int dwValue)
        {
            return dwValue & 0xFFFF;
        }

        public static int LoWord(IntPtr dwValue)
        {
            return LoWord((int)dwValue);
        }

        public static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xFFFF;
        }

        public static int HiWord(IntPtr dwValue)
        {
            return HiWord((int)dwValue);
        }

        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static int MAKELANGID(int primary, int sub)
        {
            return ((ushort)sub << 10) | (ushort)primary;
        }

        public static int MAKELCID(int lgid)
        {
            return MAKELCID(lgid, 0);
        }

        public static int MAKELCID(int lgid, int sort)
        {
            return (0xffff & lgid) | ((15 & sort) << 0x10);
        }

        public static int GetLangID(int culture)
        {
            return culture & 0xffff;
        }

        public static int GetSortID(int lcid)
        {
            return (lcid >> 0x10) & 15;
        }

        public static int GetPrimaryLangID(int lcid)
        {
            return lcid & 0xAf;
        }

        public static int GetSubLangID(int lcid)
        {
            return (lcid >> 10) & 0x3f;
        }

        public static void DrawXORRectangle(Graphics g, Rectangle r)
        {
            DrawXORRectangle(g, r, 1);
        }

        public static void DrawXORRectangle(Graphics g, Rectangle r, int penWidth)
        {
            DrawXORRectangle(g, r.Left, r.Top, r.Right, r.Bottom, penWidth);
        }

        public static void DrawXORRectangle(IntPtr hdc, Rectangle r)
        {
            DrawXORRectangle(hdc, r, 1);
        }

        public static void DrawXORRectangle(IntPtr hdc, Rectangle r, int penWidth)
        {
            DrawXORRectangle(hdc, r.Left, r.Top, r.Right, r.Bottom);
        }

        public static void DrawXORRectangle(Graphics g, int x1, int y1, int x2, int y2)
        {
            DrawXORRectangle(g, x1, y1, x2, y2, 1);
        }

        public static void DrawXORRectangle(Graphics g, int x1, int y1, int x2, int y2, int penWidth)
        {
            var hdc = g.GetHdc();
            try
            {
                DrawXORRectangle(hdc, x1, y1, x2, y2, penWidth);
            }
            finally
            {
                g.ReleaseHdc(hdc);
            }
        }

        public static void DrawXORRectangle(IntPtr hdc, int x1, int y1, int x2, int y2)
        {
            DrawXORRectangle(hdc, x1, y1, x2, y2, 1);
        }

        public static void DrawXORRectangle(IntPtr hdc, int x1, int y1, int x2, int y2, int penWidth)
        {
            DrawXORRectangle(hdc, x1, y1, x2, y2, penWidth, 0);
        }

        public static void DrawXORRectangle(IntPtr hdc, int x1, int y1, int x2, int y2, int penWidth, int rgb)
        {
            var pen = CreatePen(penWidth == 1 ? PenStyles.PS_DOT : PenStyles.PS_SOLID, penWidth, rgb);

            if (penWidth > 1)
            {
                SetROP2(hdc, RasterOp2.R2_NOTXORPEN);
            }
            else
            {
                SetROP2(hdc, RasterOp2.R2_XORPEN);
            }

            var oldpen = SelectObject(new HandleRef(null, hdc), new HandleRef(null, pen));

            var oldbrush = SelectObject(new HandleRef(null, hdc), new HandleRef(null, GetStockObject(NULL_BRUSH)));

            Rectangle(hdc, x1, y1, x2, y2);

            SelectObject(new HandleRef(null, hdc), new HandleRef(null, oldbrush));
            SelectObject(new HandleRef(null, hdc), new HandleRef(null, oldpen));
            DeleteObject(pen);
        }

        public static void DrawXORLine(IntPtr hdc, PointF p1, PointF p2, int penWidth = 1, int rgb = BLACK_PEN)
        {
            var pen = CreatePen(penWidth == 1 ? PenStyles.PS_DOT : PenStyles.PS_SOLID, penWidth, rgb);

            if (penWidth > 1)
            {
                SetROP2(hdc, RasterOp2.R2_NOTXORPEN);
            }
            else
            {
                SetROP2(hdc, RasterOp2.R2_XORPEN);
            }

            var oldpen = SelectObject(new HandleRef(null, hdc), new HandleRef(null, pen));

            var oldbrush = SelectObject(new HandleRef(null, hdc), new HandleRef(null, GetStockObject(NULL_BRUSH)));

            Polyline(hdc, new POINT[] { new POINT((int)p1.X, (int)p1.Y), new POINT((int)p2.X, (int)p2.Y) }, 2);

            SelectObject(new HandleRef(null, hdc), new HandleRef(null, oldbrush));
            SelectObject(new HandleRef(null, hdc), new HandleRef(null, oldpen));
            DeleteObject(pen);
        }

        public static void DrawXORLine(IntPtr hdc, int x1, int y1, int x2, int y2)
        {
            DrawXORLine(hdc, new Point(x1, y1), new Point(x2, y2));
        }

        private const int MAX_GDI_HANDLE = 0x4000;

        [DllImport("gdi32.dll")]
        private static extern IntPtr GdiQueryTable();

        [DllImport("gdi32")]
        public static extern int GdiFlush();

        [DllImport("gdi32")]
        public static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, [Out] byte[] lpvBuffer, uint cbData);

        public static uint FontTableOS2 = 0x322F534F;
    }

    #region UnicodeSubset
    public enum UnicodeSubset
    {
        [UnicodeSubset(127, 0x0000, 0x0000, "None")] None,
        [UnicodeSubset(0, 0x0000, 0x007F, "Basic Latin")] BasicLatin,
        [UnicodeSubset(1, 0x0080, 0x00FF, "Latin-1 Supplement")] LatinSupplement,
        [UnicodeSubset(2, 0x0100, 0x017F, "Latin Extended-A")] LatinExtendedA,
        [UnicodeSubset(3, 0x0180, 0x024F, "Latin Extended-B")] LatinExtendedB,
        [UnicodeSubset(4, 0x0250, 0x02AF, "IPA Extensions")] IPAExtensions,
        [UnicodeSubset(4, 0x1D00, 0x1D7F, "Phonetic Extensions")] PhoneticExtensions,
        [UnicodeSubset(4, 0x1D80, 0x1DBF, "Phonetic Extensions Supplement")] PhoneticExtensionsSupplement,
        [UnicodeSubset(5, 0x02B0, 0x02FF, "Spacing Modifier Letters")] SpacingModifierLetters,
        [UnicodeSubset(5, 0xA700, 0xA71F, "Modifier Tone Letters")] ModifierToneLetters,
        [UnicodeSubset(6, 0x0300, 0x036F, "Combining Diacritical Marks")] CombiningDiacriticalMarks,
        [UnicodeSubset(6, 0x1DC0, 0x1DFF, "Combining Diacritical Marks Supplement")] CombiningDiacriticalMarksSupplement,
        [UnicodeSubset(7, 0x0370, 0x03FF, "Greek and Coptic")] GreekAndCoptic,
        [UnicodeSubset(8, 0x2C80, 0x2CFF, "Coptic")] Coptic,
        [UnicodeSubset(9, 0x0400, 0x04FF, "Cyrillic")] Cyrillic,
        [UnicodeSubset(9, 0x0500, 0x052F, "Cyrillic Supplementary")] CyrillicSupplementary,
        [UnicodeSubset(10, 0x0530, 0x058F, "Armenian")] Armenian,
        [UnicodeSubset(11, 0x0590, 0x05FF, "Basic Hebrew")] BasicHebrew,
        //12   Reserved
        [UnicodeSubset(13, 0x0600, 0x06FF, "Basic Arabic")] BasicArabic,
        [UnicodeSubset(13, 0x0750, 0x077F, "Arabic Supplement")] ArabicSupplement,
        [UnicodeSubset(14, 0x07C0, 0x07FF, "N'Ko")] NKo,
        [UnicodeSubset(15, 0x0900, 0x097F, "Devanagari")] Devanagari,
        [UnicodeSubset(16, 0x0980, 0x09FF, "Bengali")] Bengali,
        [UnicodeSubset(17, 0x0A00, 0x0A7F, "Gurmukhi")] Gurmukhi,
        [UnicodeSubset(18, 0x0A80, 0x0AFF, "Gujarati")] Gujarati,
        [UnicodeSubset(19, 0x0B00, 0x0B7F, "Oriya")] Oriya,
        [UnicodeSubset(20, 0x0B80, 0x0BFF, "Tamil")] Tamil,
        [UnicodeSubset(21, 0x0C00, 0x0C7F, "Telugu")] Telugu,
        [UnicodeSubset(22, 0x0C80, 0x0CFF, "Kannada")] Kannada,
        [UnicodeSubset(23, 0x0D00, 0x0D7F, "Malayalam")] Malayalam,
        [UnicodeSubset(24, 0x0E00, 0x0E7F, "Thai")] Thai,
        [UnicodeSubset(25, 0x0E80, 0x0EFF, "Lao")] Lao,
        [UnicodeSubset(26, 0x10A0, 0x10FF, "Georgian")] Georgian,
        [UnicodeSubset(26, 0x2D00, 0x2D2F, "Georgian Supplement")] GeorgianSupplement,
        [UnicodeSubset(27, 0x1B00, 0x1B7F, "Balinese")] Balinese,
        [UnicodeSubset(28, 0x1100, 0x11FF, "Hangul Jamo")] HangulJamo,
        [UnicodeSubset(29, 0x1E00, 0x1EFF, "Latin Extended Additional")] LatinExtendedAdditional,
        [UnicodeSubset(29, 0x2C60, 0x2C7F, "Latin Extended-C")] LatinExtendedC,
        [UnicodeSubset(30, 0x1F00, 0x1FFF, "Greek Extended")] GreekExtended,
        [UnicodeSubset(31, 0x2000, 0x206F, "General Punctuation")] GeneralPunctuation,
        [UnicodeSubset(31, 0x2E00, 0x2E7F, "Supplemental Punctuation")] SupplementalPunctuation,
        [UnicodeSubset(32, 0x2070, 0x209F, "Subscripts and Superscripts")] SubscriptsAndSuperscripts,
        [UnicodeSubset(33, 0x20A0, 0x20CF, "Currency Symbols")] CurrencySymbols,
        [UnicodeSubset(34, 0x20D0, 0x20FF, "Combining Diacritical Marks for Symbols")] CombiningDiacriticalMarksforSymbols,
        [UnicodeSubset(35, 0x2100, 0x214F, "Letter-like Symbols")] LetterlikeSymbols,
        [UnicodeSubset(36, 0x2150, 0x218F, "Number Forms")] NumberForms,
        [UnicodeSubset(37, 0x2190, 0x21FF, "Arrows")] Arrows,
        [UnicodeSubset(37, 0x27F0, 0x27FF, "Supplemental Arrows-A")] SupplementalArrowsA,
        [UnicodeSubset(37, 0x2900, 0x297F, "Supplemental Arrows-B")] SupplementalArrowsB,
        [UnicodeSubset(37, 0x2B00, 0x2BFF, "Miscellaneous Symbols and Arrows")] MiscellaneousSymbolsAndArrows,
        [UnicodeSubset(38, 0x2200, 0x22FF, "Mathematical Operators")] MathematicalOperators,
        [UnicodeSubset(38, 0x27C0, 0x27EF, "Miscellaneous Mathematical Symbols-A")] MiscellaneousMathematicalSymbolsA,
        [UnicodeSubset(38, 0x2980, 0x29FF, "Miscellaneous Mathematical Symbols-B")] MiscellaneousMathematicalSymbolsB,
        [UnicodeSubset(38, 0x2A00, 0x2AFF, "Supplemental Mathematical Operators")] SupplementalMathematicalOperators,
        [UnicodeSubset(39, 0x2300, 0x23FF, "Miscellaneous Technical")] MiscellaneousTechnical,
        [UnicodeSubset(40, 0x2400, 0x243F, "Control Pictures")] ControlPictures,
        [UnicodeSubset(41, 0x2440, 0x245F, "Optical Character Recognition")] OpticalCharacterRecognition,
        [UnicodeSubset(42, 0x2460, 0x24FF, "Enclosed Alphanumerics")] EnclosedAlphanumerics,
        [UnicodeSubset(43, 0x2500, 0x257F, "Box Drawing")] BoxDrawing,
        [UnicodeSubset(44, 0x2580, 0x259F, "Block Elements")] BlockElements,
        [UnicodeSubset(45, 0x25A0, 0x25FF, "Geometric Shapes")] GeometricShapes,
        [UnicodeSubset(46, 0x2600, 0x26FF, "Miscellaneous Symbols")] MiscellaneousSymbols,
        [UnicodeSubset(47, 0x2700, 0x27BF, "Dingbats")] Dingbats,
        [UnicodeSubset(48, 0x3000, 0x303F, "Chinese, Japanese, and Korean (CJK) Symbols and Punctuation")] CJKSymbolsAndPunctuation,
        [UnicodeSubset(49, 0x3040, 0x309F, "Hiragana")] Hiragana,
        [UnicodeSubset(50, 0x30A0, 0x30FF, "Katakana")] Katakana,
        [UnicodeSubset(50, 0x31F0, 0x31FF, "Katakana Phonetic Extensions")] KatakanaPhoneticExtensions,
        [UnicodeSubset(51, 0x3100, 0x312F, "Bopomofo")] Bopomofo,
        [UnicodeSubset(51, 0x31A0, 0x31BF, "Extended Bopomofo")] ExtendedBopomofo,
        [UnicodeSubset(52, 0x3130, 0x318F, "Hangul Compatibility Jamo")] HangulCompatibilityJamo,
        [UnicodeSubset(53, 0xA840, 0xA87F, "Phags-pa")] Phagspa,
        [UnicodeSubset(54, 0x3200, 0x32FF, "Enclosed CJK Letters and Months")] EnclosedCJKLettersAndMonths,
        [UnicodeSubset(55, 0x3300, 0x33FF, "CJK Compatibility")] CJKCompatibility,
        [UnicodeSubset(56, 0xAC00, 0xD7A3, "Hangul")] Hangul,
        [UnicodeSubset(57, 0xD800, 0xDFFF, "Surrogates")] Surrogates,
        [UnicodeSubset(58, 0x10900, 0x1091F, "Phoenician")] Phoenician,
        [UnicodeSubset(59, 0x2E80, 0x2EFF, "CJK Radicals Supplement")] CJKRadicalsSupplement,
        [UnicodeSubset(59, 0x2F00, 0x2FDF, "Kangxi Radicals")] KangxiRadicals,
        [UnicodeSubset(59, 0x2FF0, 0x2FFF, "Ideographic Description Characters")] IdeographicDescriptionCharacters,
        [UnicodeSubset(59, 0x3190, 0x319F, "Kanbun")] Kanbun,
        [UnicodeSubset(59, 0x3400, 0x4DBF, "CJK Unified Ideographs Extension A")] CJKUnifiedIdeographsExtensionA,
        [UnicodeSubset(59, 0x4E00, 0x9FFF, "CJK Unified Ideographs")] CJKUnifiedIdeographs,
        [UnicodeSubset(59, 0x20000, 0x2A6DF, "CJK Unified Ideographs Extension B")] CJKUnifiedIdeographsExtensionB,
        [UnicodeSubset(60, 0xE000, 0xF8FF, "Private Use Area")] PrivateUsePlane0,
        [UnicodeSubset(61, 0x31C0, 0x31EF, "CJK Base Strokes")] CJKBaseStrokes,
        [UnicodeSubset(61, 0xF900, 0xFAFF, "CJK Compatibility Ideographs")] CJKCompatibilityIdeographs,
        [UnicodeSubset(61, 0x2F800, 0x2FA1F, "CJK Compatibility Ideographs Supplement")] CJKCompatibilityIdeographsSupplement,
        [UnicodeSubset(62, 0xFB00, 0xFB4F, "Alphabetical Presentation Forms")] AlphabeticalPresentationForms,
        [UnicodeSubset(63, 0xFB50, 0xFDFF, "Arabic Presentation Forms-A")] ArabicPresentationFormsA,
        [UnicodeSubset(64, 0xFE20, 0xFE2F, "Combining Half Marks")] CombiningHalfMarks,
        [UnicodeSubset(65, 0xFE10, 0xFE1F, "Vertical Forms")] VerticalForms,
        [UnicodeSubset(65, 0xFE30, 0xFE4F, "CJK Compatibility Forms")] CJKCompatibilityForms,
        [UnicodeSubset(66, 0xFE50, 0xFE6F, "Small Form Variants")] SmallFormVariants,
        [UnicodeSubset(67, 0xFE70, 0xFEFE, "Arabic Presentation Forms-B")] ArabicPresentationFormsB,
        [UnicodeSubset(68, 0xFF00, 0xFFEF, "Halfwidth and Fullwidth Forms")] HalfwidthAndFullwidthForms,
        [UnicodeSubset(69, 0xFFF0, 0xFFFF, "Specials")] Specials,
        [UnicodeSubset(70, 0x0F00, 0x0FFF, "Tibetan")] Tibetan,
        [UnicodeSubset(71, 0x0700, 0x074F, "Syriac")] Syriac,
        [UnicodeSubset(72, 0x0780, 0x07BF, "Thaana")] Thaana,
        [UnicodeSubset(73, 0x0D80, 0x0DFF, "Sinhala")] Sinhala,
        [UnicodeSubset(74, 0x1000, 0x109F, "Myanmar")] Myanmar,
        [UnicodeSubset(75, 0x1200, 0x137F, "Ethiopic")] Ethiopic,
        [UnicodeSubset(75, 0x1380, 0x139F, "Ethiopic Supplement")] EthiopicSupplement,
        [UnicodeSubset(75, 0x2D80, 0x2DDF, "Ethiopic Extended")] EthiopicExtended,
        [UnicodeSubset(76, 0x13A0, 0x13FF, "Cherokee")] Cherokee,
        [UnicodeSubset(77, 0x1400, 0x167F, "Canadian Aboriginal Syllabics")] CanadianAboriginalSyllabics,
        [UnicodeSubset(78, 0x1680, 0x169F, "Ogham")] Ogham,
        [UnicodeSubset(79, 0x16A0, 0x16FF, "Runic")] Runic,
        [UnicodeSubset(80, 0x1780, 0x17FF, "Khmer")] Khmer,
        [UnicodeSubset(80, 0x19E0, 0x19FF, "Khmer Symbols")] KhmerSymbols,
        [UnicodeSubset(81, 0x1800, 0x18AF, "Mongolian")] Mongolian,
        [UnicodeSubset(82, 0x2800, 0x28FF, "Braille")] Braille,
        [UnicodeSubset(83, 0xA000, 0xA48F, "Yi")] Yi,
        [UnicodeSubset(83, 0xA490, 0xA4CF, "Yi Radicals")] YiRadicals,
        [UnicodeSubset(84, 0x1700, 0x171F, "Tagalog")] Tagalog,
        [UnicodeSubset(84, 0x1720, 0x173F, "Hanunoo")] Hanunoo,
        [UnicodeSubset(84, 0x1740, 0x175F, "Buhid")] Buhid,
        [UnicodeSubset(84, 0x1760, 0x177F, "Tagbanwa")] Tagbanwa,
        [UnicodeSubset(85, 0x10300, 0x1032F, "Old Italic")] OldItalic,
        [UnicodeSubset(86, 0x10330, 0x1034F, "Gothic")] Gothic,
        [UnicodeSubset(87, 0x10440, 0x1044F, "Deseret")] Deseret,
        [UnicodeSubset(88, 0x1D000, 0x1D0FF, "Byzantine Musical Symbols")] ByzantineMusicalSymbols,
        [UnicodeSubset(88, 0x1D100, 0x1D1FF, "Musical Symbols")] MusicalSymbols,
        [UnicodeSubset(88, 0x1D200, 0x1D24F, "Ancient Greek Musical Notation")] AncientGreekMusicalNotation,
        [UnicodeSubset(89, 0x1D400, 0x1D7FF, "Mathematical Alphanumeric Symbols")] MathematicalAlphanumericSymbols,
        [UnicodeSubset(90, 0xFF000, 0xFFFFD, "Private Use Area")] PrivateUsePlane15,
        [UnicodeSubset(90, 0x100000, 0x10FFFD, "Private Use Area")] PrivateUsePlane16,
        [UnicodeSubset(91, 0xFE00, 0xFE0F, "Variation Selectors")] VariationSelectors,
        [UnicodeSubset(91, 0xE0100, 0xE01EF, "Variation Selectors Supplement")] VariationSelectorsSupplement,
        [UnicodeSubset(92, 0xE0000, 0xE007F, "Tags")] Tags,
        [UnicodeSubset(93, 0x1900, 0x194F, "Limbu")] Limbu,
        [UnicodeSubset(94, 0x1950, 0x197F, "Tai Le")] TaiLe,
        [UnicodeSubset(95, 0x1980, 0x19DF, "New Tai Lue")] NewTaiLue,
        [UnicodeSubset(96, 0x1A00, 0x1A1F, "Buginese")] Buginese,
        [UnicodeSubset(97, 0x2C00, 0x2C5F, "Glagolitic")] Glagolitic,
        [UnicodeSubset(98, 0x2D40, 0x2D7F, "Tifinagh")] Tifinagh,
        [UnicodeSubset(99, 0x4DC0, 0x4DFF, "Yijing Hexagram Symbols")] YijingHexagramSymbols,
        [UnicodeSubset(100, 0xA800, 0xA82F, "Syloti Nagri")] SylotiNagri,
        [UnicodeSubset(101, 0x10000, 0x1007F, "Linear B Syllabary")] LinearBSyllabary,
        [UnicodeSubset(101, 0x10080, 0x100FF, "Linear B Ideograms")] LinearBIdeograms,
        [UnicodeSubset(101, 0x10100, 0x1013F, "Aegean Numbers")] AegeanNumbers,
        [UnicodeSubset(102, 0x10140, 0x1018F, "Ancient Greek Numbers")] AncientGreekNumbers,
        [UnicodeSubset(103, 0x10380, 0x1039F, "Ugaritic")] Ugaritic,
        [UnicodeSubset(104, 0x103A0, 0x103DF, "Old Persian")] OldPersian,
        [UnicodeSubset(105, 0x10450, 0x1047F, "Shavian")] Shavian,
        [UnicodeSubset(106, 0x10480, 0x104AF, "Osmanya")] Osmanya,
        [UnicodeSubset(107, 0x10800, 0x1083F, "Cypriot Syllabary")] CypriotSyllabary,
        [UnicodeSubset(108, 0x0A00, 0x10A5F, "Kharoshthi")] Kharoshthi,
        [UnicodeSubset(109, 0x1D300, 0x1D35F, "Tai Xuan Jing Symbols")] TaiXuanJingSymbols,
        [UnicodeSubset(110, 0x12000, 0x123FF, "Cuneiform")] Cuneiform,
        [UnicodeSubset(110, 0x12400, 0x1247F, "Cuneiform Numbers and Punctuation")] CuneiformNumbersAndPunctuation,
        [UnicodeSubset(111, 0x1D360, 0x1D37F, "Counting Rod Numerals")] CountingRodNumerals
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class UnicodeSubsetAttribute : DescriptionAttribute
    {
        public readonly int bitIndex = 0;
        public readonly int subRangeLow = 0x0000;
        public readonly int subRangeHi = 0x007F;

        public UnicodeSubsetAttribute(int bitIndex, int subRangeLow, int subRangeHi, string description) : base(description)
        {
            this.bitIndex = bitIndex;
            this.subRangeLow = subRangeLow;
            this.subRangeHi = subRangeHi;
        }

        public static UnicodeSubsetAttribute GetUnicodeSubset(UnicodeSubset subset)
        {
            var fi = subset.GetType().GetField(subset.ToString());
            var attributes =
                (UnicodeSubsetAttribute[])fi.GetCustomAttributes(typeof(UnicodeSubsetAttribute), false);
            return attributes.Length > 0 ? attributes[0] : null;
        }
    }
    #endregion
}
