//
// MyClass.cs
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
using SkeletonView.Views;
using UIKit;

namespace SkeletonView
{
    [Register("SkeletonView"), DesignTimeVisible(true)]
    public class SKView : UIView, ISKView
    {
        private readonly ISKView _sdkView;

        public SKView()
        {
            _sdkView = new SKViewAdapter(this);
        }

        public SKView(IntPtr handle) : base(handle)
        {
            _sdkView = new SKViewAdapter(this);
        }

        public SKView(CGRect frame) : base(frame)
        {
            _sdkView = new SKViewAdapter(this);
        }

        public bool IsSkeletonActive => _sdkView.IsSkeletonActive;

        [Export("isSkeletonable")]
        public bool IsSkeletonable
        {
            get => _sdkView.IsSkeletonable;
            set => _sdkView.IsSkeletonable = value;
        }

        public SkeletonLayer SkeletonLayer
        {
            get => _sdkView.SkeletonLayer;
            set => _sdkView.SkeletonLayer = value;
        }

        public Status Status
        {
            get => _sdkView.Status;
            set => _sdkView.Status = value;
        }
    }
}
