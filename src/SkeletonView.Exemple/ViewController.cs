//
// ViewController.cs
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
using SkeletonView.Extensions;
using UIKit;
using CoreGraphics;
using System.Linq;

namespace SkeletonView.Exemple
{
    public partial class ViewController : UIViewController
    {
        private SkeletonType type => skeletonTypeSelector.SelectedSegment == 0 ? SkeletonType.Solid : SkeletonType.Grandient;

        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            switchAnimated.ValueChanged += ChangeAnimated;
            skeletonTypeSelector.ValueChanged += (sender, e) => RefreshSkeleton();
            button.TouchUpInside += (sender, e) => ShowAlertPicker();

            avatarImage.Layer.MasksToBounds = true;
            avatarImage.Layer.CornerRadius = avatarImage.Frame.Width / 2f;

            colorSelectedView.Layer.CornerRadius = 5f;
            colorSelectedView.Layer.MasksToBounds = true;
            colorSelectedView.BackgroundColor = SkeletonConfig.Default.TintColor;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            View.ShowAnimatedSkeleton();           
        }


        private void ChangeAnimated(object sender, EventArgs e)
        {
            if (switchAnimated.On)
                View.ShowAnimatedSkeleton();
            else
                View.StopSkeletonAnimation();
        }


        private void RefreshSkeleton()
        {
            View.HideSkeleton();

            if (type == SkeletonType.Grandient)
                ShowGradientSkeleton();
            else
                ShowSolidSkeleton();
        }

        private void ShowSolidSkeleton()
        {
            if (switchAnimated.On)
                View.ShowAnimatedSkeleton(colorSelectedView.BackgroundColor);
            else
                View.ShowAnimatedSkeleton(colorSelectedView.BackgroundColor);
        }

        private void ShowGradientSkeleton()
        {
            var gradient = new SkeletonGradient(colorSelectedView.BackgroundColor);
            if (switchAnimated.On)
                View.ShowAnimatedGradientSkeleton(gradient);
            else
                View.ShowGradientSkeleton();
        }

        private void ShowAlertPicker()
        {
            var alertView = UIAlertController.Create("Select color", "\n\n\n\n\n", UIAlertControllerStyle.Alert);

            var pickerView = new UIPickerView(new CGRect(0, 50, 260, 115));
            pickerView.DataSource = this;
            pickerView.Delegate = this;

            alertView.View.AddSubview(pickerView);

            var action = UIAlertAction.Create("OK", UIAlertActionStyle.Default, (obj) =>
            {
                var row = pickerView.SelectedRowInComponent(0);
                colorSelectedView.BackgroundColor = _colors.ElementAt((int)row).Key;
                RefreshSkeleton();
            });
            alertView.AddAction(action);


            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
            alertView.AddAction(cancelAction);

            PresentViewController(alertView, false, () =>
            {
                var frame = pickerView.Frame;
                frame.Width = alertView.View.Frame.Size.Width;
                pickerView.Frame = frame;
            });
        }
    }
}
