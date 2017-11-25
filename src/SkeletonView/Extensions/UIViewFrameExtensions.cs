//
// UIViewFrameExtensions.cs
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
using CoreGraphics;
using UIKit;
using System.Linq;
using System.Collections.Generic;

namespace SkeletonView.Extensions
{
    internal static class UIViewFrameExtensions
    {
        public static CGRect MaxBoundsEstimated(this UIView This)
        {
            return new CGRect(CGPoint.Empty, MaxSizeEstimated(This));
        }

        public static CGSize MaxSizeEstimated(this UIView This)
        {
            return new CGSize(MaxWidthEstimated(This), MaxHeightEstimated(This));
        }

        public static nfloat MaxWidthEstimated(this UIView This)
        {
            var constraintWidth = This.Constraints.Where(c => c.FirstAttribute == NSLayoutAttribute.Width);
            return Max(This.Frame.Width, constraintWidth);
        }

        public static nfloat MaxHeightEstimated(this UIView This)
        {
            var constraintHeight = This.Constraints.Where(c => c.FirstAttribute == NSLayoutAttribute.Height);
            return Max(This.Frame.Height, constraintHeight);
        }

        private static nfloat Max(nfloat val, IEnumerable<NSLayoutConstraint> constraints)
        {
            nfloat max = constraints.Aggregate(val, (m, constraint) =>
            {
                var tempMax = m;
                if (constraint.Constant > tempMax)
                    tempMax = constraint.Constant;
                return tempMax;
            });

            return max;
        }
    }
}
