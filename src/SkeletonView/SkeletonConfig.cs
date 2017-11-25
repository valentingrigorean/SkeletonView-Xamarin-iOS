//
// SkeletonConfig.cs
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
using UIKit;
using System;

namespace SkeletonView
{
    public class SkeletonConfig : ICloneable
    {
        public static SkeletonConfig Default { get; } = new SkeletonConfig();

        public UIColor TintColor { get; set; } = SkeletonColors.Clouds;

        public SkeletonGrandient Gradient { get; set; } = new SkeletonGrandient(SkeletonColors.Clouds);

        public nfloat MultilineHeight { get; set; } = 15;

        public nfloat MultilineSpacing { get; set; } = 10;

        public int MultilineLastLineFillPercent { get; set; } = 70;

        public nfloat SpaceRequiredForEachLine => MultilineHeight + MultilineSpacing;

        public object Clone()
        {
            return new SkeletonConfig
            {
                TintColor = TintColor,
                Gradient = Gradient,
                MultilineHeight = MultilineHeight,
                MultilineSpacing = MultilineSpacing,
                MultilineLastLineFillPercent = MultilineLastLineFillPercent
            };
        }
    }
}