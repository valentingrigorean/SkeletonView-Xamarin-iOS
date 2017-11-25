//
// UIViewExtensions.cs
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
using Foundation;
using ObjCRuntime;
using UIKit;
using System.Linq;

namespace SkeletonView.Extensions
{
    [Category(typeof(UIView))]
    public static class UIViewExtensions
    {
        private static class AssociatedKeys
        {
            public static readonly NSString Skeletonable = new NSString("skeletonable");
            public static readonly NSString Status = new NSString("status");
            public static readonly NSString SkeletonLayer = new NSString("skeletonLayer");
        }

        public enum Status
        {
            On,
            Off
        }

        public static bool IsSkeletonActive(this UIView This)
        {
            return This.GetSkeletonStatus() == Status.On ||
                       (This.Subviews.FirstOrDefault(_ => _.IsSkeletonActive())) != null;
        }

        public static bool GetIsSkeletonable(this UIView This)
        {
            if (This is SkeletonView)
                return true;
            var isSkeletonable = This.GetAssociatedObject<NSNumber>(AssociatedKeys.Skeletonable);
            return isSkeletonable != null && isSkeletonable.BoolValue;
        }

        [Export("isSkeletonable")]
        public static void GetIsSkeletonable(this UIView This, bool isSkeletonable)
        {
            if (This is SkeletonView)
                return;
            This.SetAssociatedObject(AssociatedKeys.Skeletonable, new NSNumber(isSkeletonable));
        }

        public static Status GetSkeletonStatus(this UIView This)
        {
            var status = This.GetAssociatedObject<NSNumber>(AssociatedKeys.Status);
            return status != null ? Status.Off : (Status)status.Int32Value;
        }

        [Export("status")]
        public static void SetSkeletonStatus(this UIView This, Status status)
        {
            This.SetAssociatedObject(AssociatedKeys.Skeletonable, new NSNumber((int)status));
        }

        public static SkeletonLayer GetSkeletonLayer(this UIView This)
        {
            return This.GetAssociatedObject<SkeletonLayer>(AssociatedKeys.SkeletonLayer);
        }

        public static void SetSkeletonLayer(this UIView This, SkeletonLayer skeletonLayer)
        {
            This.SetAssociatedObject(AssociatedKeys.SkeletonLayer, skeletonLayer);
        }

        public static void PrepareForSkeleton(this UIView This)
        {
            switch (This)
            {
                case UILabel lbl:
                    lbl.Text = "";
                    lbl.ResignFirstResponder();
                    break;
                case UITextView textView:
                    textView.Text = "";
                    textView.ResignFirstResponder();
                    break;
                case UIImageView imageView:
                    imageView.Image = null;
                    break;
            }
        }
    }
}
