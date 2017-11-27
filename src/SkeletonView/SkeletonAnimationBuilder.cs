//
// SkeletonAnimationBuilder.cs
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
using CoreAnimation;
using Foundation;

namespace SkeletonView
{
    public enum GradientDirection
    {
        LeftRight,
        RightLeft,
        TopBottom,
        BottomTop,
        TopLeftBottomRight,
        BottomRightTopLeft
    }

    public static class SkeletonAnimationBuilder
    {
        public static SkeletonLayerAnimation MakeSlidingAnimation(GradientDirection direction, double duration = 1.5)
        {
            return (layer) =>
            {
                var startValues = direction.StartPoint();
                var endValues = direction.EndPoint();

                var startPointAnim = CABasicAnimation.FromKeyPath(nameof(CAGradientLayer.StartPoint));
                startPointAnim.From = NSValue.FromCGPoint(startValues.from);
                startPointAnim.To = NSValue.FromCGPoint(startValues.to);

                var endPointAnim = CABasicAnimation.FromKeyPath(nameof(CAGradientLayer.EndPoint));
                endPointAnim.From = NSValue.FromCGPoint(endValues.from);
                endPointAnim.To = NSValue.FromCGPoint(endValues.to);

                var animGroup = new CAAnimationGroup
                {
                    Animations = new[] { startPointAnim, endPointAnim },
                    Duration = duration,
                    TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseIn),
                    RepeatCount = float.MaxValue
                };
                return animGroup;
            };           
        }
    }

    public static class GradientDirectionExtensions
    {
        public static SkeletonLayerAnimation SlidingAnimation(this GradientDirection This,double duration = 1.5)
        {
            return SkeletonAnimationBuilder.MakeSlidingAnimation(This, duration);
        }

        public static (CGPoint @from, CGPoint to) StartPoint(this GradientDirection This)
        {
            switch (This)
            {
                case GradientDirection.LeftRight:
                    return (new CGPoint(-1, 0.5), new CGPoint(1, 0.5));
                case GradientDirection.RightLeft:
                    return (new CGPoint(1, 0.5), new CGPoint(-1, 0.5));
                case GradientDirection.TopBottom:
                    return (new CGPoint(0.5, -1), new CGPoint(0.5, 1));
                case GradientDirection.BottomTop:
                    return (new CGPoint(0.5, 1), new CGPoint(0.5, -1));
                case GradientDirection.TopLeftBottomRight:
                    return (new CGPoint(-1, -1), new CGPoint(1, 1));
                case GradientDirection.BottomRightTopLeft:
                    return (new CGPoint(1, 1), new CGPoint(-1, -1));
                default:
                    throw new NotSupportedException();
            }
        }

        public static (CGPoint @from, CGPoint to) EndPoint(this GradientDirection This)
        {
            switch (This)
            {
                case GradientDirection.LeftRight:
                    return (new CGPoint(0, 0.5), new CGPoint(2, 0.5));
                case GradientDirection.RightLeft:
                    return (new CGPoint(2, 0.5), new CGPoint(0, 0.5));
                case GradientDirection.TopBottom:
                    return (new CGPoint(0.5, 0), new CGPoint(0.5, 2));
                case GradientDirection.BottomTop:
                    return (new CGPoint(0.5, 2), new CGPoint(0.5, 0));
                case GradientDirection.TopLeftBottomRight:
                    return (new CGPoint(0, 0), new CGPoint(2, 2));
                case GradientDirection.BottomRightTopLeft:
                    return (new CGPoint(2, 2), new CGPoint(0, 0));
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
