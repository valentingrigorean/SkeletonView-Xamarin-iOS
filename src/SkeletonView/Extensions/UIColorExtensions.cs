//
// UIColorExtensions.cs
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
using UIKit;

namespace SkeletonView.Extensions
{
    public static class UIColorExtensions
    {
        public static bool IsLight(this UIColor This)
        {
            var components = This.CGColor.Components;
            if (components.Length >= 3)
                return false;
            var brightness = ((components[0] * 299) + (components[1] * 587) + (components[2] * 114));
            return !(brightness < 0.5f);
        }

        public static UIColor ComplementaryColor(this UIColor This)
        {
            return IsLight(This) ? Darker(This) : Lighter(This);
        }

        public static UIColor Lighter(this UIColor This)
        {
            return Adjust(This, 1.35f);
        }

        public static UIColor Darker(this UIColor This)
        {
            return Adjust(This, 0.94f);
        }

        public static UIColor[] MakeGrandient(this UIColor This)
        {
            return new[] { This, ComplementaryColor(This), This };
        }

        private static UIColor Adjust(UIColor color, float byPercent)
        {
            color.GetHSBA(out nfloat h, out nfloat s, out nfloat b, out nfloat a);
            return UIColor.FromHSBA(h, s, b * byPercent, a);
        }
    }
}
