using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using SharpDX.DirectWrite;
using ReadingDirection = SharpDX.DirectWrite.ReadingDirection;

namespace DirectLeak
{
    internal sealed class ScriptAnalysis : CallbackBase, TextAnalysisSink, TextAnalysisSource
    {
        private const string PunctuationSpanPattern = @"[\p{P}]+";

        public int CharCount => text.Length;

        public RangeMap<SharpDX.DirectWrite.ScriptAnalysis> ScriptAnalyses
        {
            get;
        }

        public RangeMap<byte> BidiRanges
        {
            get;
        }

        public RangeMap<bool> CursiveRanges
        {
            get;
        }

        public TextAnalysisSource TextAnalysisSource => this;

        public IDisposable Shadow
        {
            get;
            set;
        }

        public char[] Chars
        {
            get;
        }

        public int MarkerTextLength
        {
            get
            {
                // TODO: Calculate when we implement markers
                return 0;
            }
        }

        public short[] ToGlyphIndex
        {
            get;
        }

        public float[] CumulativeWidths
        {
            get;
        }

        public ShapingTextProperties[] Props
        {
            get;
        }

        public bool[] ClusterStart
        {
            get;
        }

        private FlowDirection baseFlowDirection;

        private readonly string text;

        private readonly Dictionary<SharpDX.DirectWrite.ScriptAnalysis, ScriptProperties> scriptProperties;
        private readonly TextAnalyzer1 analyzer;

        private readonly TextAnalyzer baseAnalyzer;

        public ScriptAnalysis(string text, FlowDirection baseFlowDirection, Factory factory)
        {
            this.text = text;
            this.baseFlowDirection = baseFlowDirection;
            BidiRanges = new RangeMap<byte>();
            CursiveRanges = new RangeMap<bool>();
            ScriptAnalyses = new RangeMap<SharpDX.DirectWrite.ScriptAnalysis>();
            scriptProperties = new Dictionary<SharpDX.DirectWrite.ScriptAnalysis, ScriptProperties>();

            Chars = text.ToCharArray();
            ToGlyphIndex = new short[text.Length];
            CumulativeWidths = new float[text.Length];
            Props = new ShapingTextProperties[text.Length];
            ClusterStart = Enumerable.Repeat(false, text.Length).ToArray();

            baseAnalyzer = new TextAnalyzer(factory);
            analyzer = baseAnalyzer.QueryInterface<TextAnalyzer1>();

            AnalyzeCore(new Range(0, text.Length));
        }

        public void GetGlyphs(Range range, FontFace fontFace, bool isRightToLeft, out short[] clusterMap, out ShapingTextProperties[] textProps, out short[] glyphIndices, out ShapingGlyphProperties[] glyphProps, out int actualGlyphCount)
        {
            var textString = text.Substring(range.Begin, range.Length);
            var maxGlyphCount = 3 * range.Length / 2 + 16;

            actualGlyphCount = 0;
            bool done;
            do
            {
                clusterMap = new short[textString.Length];
                textProps = new ShapingTextProperties[textString.Length];
                glyphIndices = new short[maxGlyphCount];
                glyphProps = new ShapingGlyphProperties[maxGlyphCount];
                done = true;
                try
                {
                    FontFeature[][] features = null;
                    int[] featureRangeLengths = null;
                    analyzer.GetGlyphs(textString, range.Length, fontFace, false, isRightToLeft, ScriptAnalyses.GetAt(range.Begin), string.Empty, null, features, featureRangeLengths, maxGlyphCount, clusterMap, textProps, glyphIndices, glyphProps, out actualGlyphCount);
                }
                catch (SharpDXException ex)
                {
                    done = false;
                    if (ex.ResultCode != Result.OutOfMemory)
                    {
                        throw;
                    }
                    maxGlyphCount = 3 * maxGlyphCount / 2 + 16;
                }
            } while (!done);
        }

        public void GetGlyphPlacements(Range range, FontFace fontFace, float fontSize, short[] clusterMap, ShapingTextProperties[] textProps, short[] glyphIndices, ShapingGlyphProperties[] glyphProps, out float[] advances, out GlyphOffset[] offsets)
        {
            advances = new float[glyphIndices.Length];
            offsets = new GlyphOffset[glyphIndices.Length];

            analyzer.GetGlyphPlacements(
                text.Substring(range.Begin, range.Length),
                clusterMap,
                textProps,
                range.Length,
                glyphIndices,
                glyphProps,
                glyphIndices.Length,
                fontFace,
                fontSize,
                false,
                false,
                ScriptAnalyses.GetAt(range.Begin),
                string.Empty,
                null,
                null,
                advances,
                offsets
            );
        }

        public byte GetBidiLevel(Range range)
        {
            return BidiRanges.GetAt(range.Begin);
        }

        public void SetBidiLevel(Range range, byte bidiLevel)
        {
            SetBidiLevelCore(range, bidiLevel);
        }

        public ScriptShapes GetShaping(Range range)
        {
            return ScriptAnalyses.GetAt(range.Begin).Shapes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                analyzer.Dispose();
                baseAnalyzer.Dispose();
            }

            base.Dispose(disposing);
        }

        string TextAnalysisSource.GetTextAtPosition(int textPosition)
        {
            return text.Substring(textPosition);
        }

        string TextAnalysisSource.GetTextBeforePosition(int textPosition)
        {
            return text.Substring(0, textPosition);
        }

        string TextAnalysisSource.GetLocaleName(int textPosition, out int textLength)
        {
            textLength = 0;
            return string.Empty;
        }

        NumberSubstitution TextAnalysisSource.GetNumberSubstitution(int textPosition, out int textLength)
        {
            textLength = text.Length - textPosition;
            return null;
        }

        public ReadingDirection ReadingDirection
        {
            get
            {
                return ReadingDirection.LeftToRight;
            }
        }

        void TextAnalysisSink.SetScriptAnalysis(int textPosition, int textLength, SharpDX.DirectWrite.ScriptAnalysis scriptAnalysis)
        {
            ScriptAnalyses.Set(new Range(textPosition, textPosition + textLength), scriptAnalysis);
            if (!scriptProperties.ContainsKey(scriptAnalysis))
            {
                ScriptProperties scriptProps;
                analyzer.GetScriptProperties(scriptAnalysis, out scriptProps);
                scriptProperties[scriptAnalysis] = scriptProps;
            }

            CursiveRanges.Set(new Range(textPosition, textPosition + textLength), scriptProperties[scriptAnalysis].IsCursiveWriting);
        }

        void TextAnalysisSink.SetLineBreakpoints(int textPosition, int textLength, LineBreakpoint[] lineBreakpoints)
        {
            for (var iCharInfo = textPosition; iCharInfo < textLength; iCharInfo++)
            {
                var iLineBreakpoint = iCharInfo - textPosition;
            }
        }

        void TextAnalysisSink.SetBidiLevel(int textPosition, int textLength, byte explicitLevel, byte resolvedLevel)
        {
            SetBidiLevelCore(new Range(textPosition, textPosition + textLength), resolvedLevel);
        }

        void TextAnalysisSink.SetNumberSubstitution(int textPosition, int textLength, NumberSubstitution numberSubstitution)
        {
            throw new NotImplementedException();
        }

        private void AnalyzeCore(Range range)
        {
            analyzer.AnalyzeScript(this, range.Begin, range.Length, this);
            analyzer.AnalyzeBidi(this, range.Begin, range.Length, this);
            analyzer.AnalyzeLineBreakpoints(this, range.Begin, range.Length, this);
        }

        private void SetBidiLevelCore(Range range, byte bidiLevel)
        {
            BidiRanges.Set(range, bidiLevel);
        }
    }
}
