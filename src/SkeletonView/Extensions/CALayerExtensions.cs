//
// CALayerExtensions.cs
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
using System.Linq;
using CoreAnimation;
using UIKit;
using System;
using Foundation;
using CoreGraphics;

namespace SkeletonView.Extensions
{
    public static class CALayerExtensions
    {
        public readonly static string SkeletonSubLayersName = "SkeletonSubLayersName";

        public static void Tint(this CAGradientLayer This, UIColor[] colors)
        {
            This.SkeletonSublayers()
                .RecursiveSearch(() =>
                {
                    This.Colors = colors.Select(c => c.CGColor).ToArray();
                }, layer =>
                {
                    layer.Tint(colors);
                });
        }

        public static void Tint(this CALayer This, UIColor[] colors)
        {
            This.SkeletonSublayers()
                .RecursiveSearch(() =>
            {
                This.BackgroundColor = colors.First()?.CGColor;
            }, layer =>
            {
                layer.Tint(colors);
            });
        }

        public static CALayer[] SkeletonSublayers(this CALayer This)
        {
            return This.Sublayers.Where(l => l.Name == SkeletonSubLayersName).ToArray();
        }

        public static void AddMultilinesLayers(this CALayer This, int lines, SkeletonType type, int lastLineFillPercent)
        {
            var config = SkeletonConfig.Default.Clone() as SkeletonConfig;
            config.MultilineLastLineFillPercent = lastLineFillPercent;
            AddMultilinesLayers(This, lines, type, config);
        }

        public static void AddMultilinesLayers(this CALayer This, int lines, SkeletonType type, SkeletonConfig config)
        {
            var numberOfSublayers = CalculateNumLines(This, lines, config);
            var width = This.Bounds.Width;
            for (var index = 0; index < numberOfSublayers; index++)
            {
                if (index == numberOfSublayers - 1 && numberOfSublayers != 1)
                {
                    width = width * (config.MultilineLastLineFillPercent / 100f);
                }
                var layer = SkeletonLayerFactory.MakeMultilineLayer(type, index, width, config);
                This.AddSublayer(layer);
            }
        }

        public static CAAnimation Pulse(this CALayer This)
        {
            var pulseAnimation = CABasicAnimation.FromKeyPath(nameof(This.BackgroundColor));
            pulseAnimation.SetFrom(This.BackgroundColor);
            pulseAnimation.SetTo(new UIColor(This.BackgroundColor).ComplementaryColor().CGColor);
            pulseAnimation.Duration = 1;
            pulseAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
            pulseAnimation.AutoReverses = true;
            pulseAnimation.RepeatCount = float.MaxValue;
            return pulseAnimation;
        }

        public static CAAnimation Sliding(this CALayer This)
        {
            var startPointAnim = CABasicAnimation.FromKeyPath(nameof(CAGradientLayer.StartPoint));
            startPointAnim.From = NSValue.FromCGPoint(new CGPoint(-1, -0.5f));
            startPointAnim.To = NSValue.FromCGPoint(new CGPoint(1, 0.5f));

            var endPointAnim = CABasicAnimation.FromKeyPath(nameof(CAGradientLayer.EndPoint));
            endPointAnim.From = NSValue.FromCGPoint(new CGPoint(0, 0.5f));
            endPointAnim.To = NSValue.FromCGPoint(new CGPoint(2, 0.5f));

            var animGroup = new CAAnimationGroup
            {
                Animations = new[] { startPointAnim, endPointAnim },
                Duration = 1.5f,
                TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut),
                RepeatCount = float.MaxValue
            };
            return animGroup;
        }

        private static int CalculateNumLines(CALayer layer, int maxLines, SkeletonConfig config)
        {
            var spacspaceRequitedForEachLine = config.MultilineHeight + config.MultilineSpacing;
            var numberOfSublayers = (int)Math.Round(layer.Bounds.Height / spacspaceRequitedForEachLine);
            if (maxLines != 0 && maxLines <= numberOfSublayers)
                numberOfSublayers = maxLines;
            return numberOfSublayers;
        }
    }
}