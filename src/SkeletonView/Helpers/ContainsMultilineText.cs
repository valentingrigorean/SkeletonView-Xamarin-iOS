//
// ContainsMultilineText.cs
//
// Author:
//       valentingrigorean <v.grigorean@software-dep.net>
//
// Copyright (c) 2017 (c) Grigorean Valentin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Foundation;
using SkeletonView.Extensions;
using UIKit;

namespace SkeletonView.Helpers
{
    public interface IContainsMultilineText
    {
        int NumLines { get; }
        int LastLineFillingPercent { get; }
    }

    public static class ContainsMultilineText
    {
        private static readonly NSString LastLineFillingPercentKey = new NSString("lastLineFillingPercent");
        private static readonly NSString NumLinesKey = new NSString("numLines");

        public static int GetLastLineFillingPercent(this UILabel This)
        {
            return This.GetAssociatedObject<NSNumber>(LastLineFillingPercentKey)?.Int32Value ??
                       SkeletonConfig.Default.MultilineLastLineFillPercent;
        }

        [Export("lastLineFillingPercent:")]
        public static void SetLastLineFillingPercent(this UILabel This, int lastLineFillingPercent)
        {
            lastLineFillingPercent = Math.Min(100, lastLineFillingPercent);
            This.SetAssociatedObject(LastLineFillingPercentKey, new NSNumber(lastLineFillingPercent));
        }

        public static int GetNumLines(this UITextView This)
        {
            return This.GetAssociatedObject<NSNumber>(NumLinesKey)?.Int32Value ?? 0;
        }

        [Export("numLines:")]
        public static void SetNumLines(this UITextView This, int numLines)
        {
            numLines = Math.Max(0, numLines);
            This.SetAssociatedObject(LastLineFillingPercentKey, new NSNumber(numLines));
        }

        public static int GetLastLineFillingPercent(this UITextView This)
        {
            return This.GetAssociatedObject<NSNumber>(LastLineFillingPercentKey)?.Int32Value ??
                       SkeletonConfig.Default.MultilineLastLineFillPercent;
        }

        [Export("lastLineFillingPercent:")]
        public static void SetLastLineFillingPercent(this UITextView This, int lastLineFillingPercent)
        {
            lastLineFillingPercent = Math.Min(100, lastLineFillingPercent);
            This.SetAssociatedObject(LastLineFillingPercentKey, new NSNumber(lastLineFillingPercent));
        }

        public static IContainsMultilineText GetContainsMultilineText(this UIView This)
        {
            switch (This)
            {
                case UILabel label:
                    return new ContainsMultilineTextInternal((int)label.Lines, GetLastLineFillingPercent(label));
                case UITextView textView:
                    return new ContainsMultilineTextInternal(GetNumLines(textView), GetLastLineFillingPercent(textView));
                default:
                    return null;
            }
        }

        private class ContainsMultilineTextInternal : IContainsMultilineText
        {
            public ContainsMultilineTextInternal(int numLines, int lastLineFillingPercent)
            {
                NumLines = numLines;
                LastLineFillingPercent = lastLineFillingPercent;
            }

            public int NumLines { get; }

            public int LastLineFillingPercent { get; }
        }
    }
}