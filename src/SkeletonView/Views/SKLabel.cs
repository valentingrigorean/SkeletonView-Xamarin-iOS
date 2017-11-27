//
// SKLabel.cs
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
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using SkeletonView.Extensions;
using UIKit;
using SkeletonView.Helpers;

namespace SkeletonView.Views
{
    [Register(nameof(SKLabel))]
    public class SKLabel : UILabel, ISKView
    {
        public SKLabel()
        {
        }

        public SKLabel(IntPtr handle) : base(handle)
        {
        }

        public SKLabel(CGRect frame) : base(frame)
        {
        }

        public bool IsSkeletonActive => this.IsSkeletonActive();

        [Export(SKView.IsSkeletonableKey), Browsable(true)]
        public bool IsSkeletonable
        {
            get => this.GetIsSkeletonable();
            set => this.SetIsSkeletonable(value);
        }

        public SkeletonLayer SkeletonLayer
        {
            get => this.GetSkeletonLayer();
            set => this.SetSkeletonLayer(value);
        }

        public Status Status
        {
            get => this.GetSkeletonStatus();
            set => this.SetSkeletonStatus(value);
        }

        [Export("lastLineFillPercent"),Browsable(true)]
        public int LastLineFillPercent
        {
            get => this.GetLastLineFillingPercent();
            set => this.SetLastLineFillingPercent(value);
        }
    }
}
