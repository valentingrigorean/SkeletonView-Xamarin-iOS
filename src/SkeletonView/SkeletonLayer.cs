//
// SkeletonLayer.cs
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
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SkeletonView.Extensions;
using UIKit;
using SkeletonView.Helpers;

namespace SkeletonView
{
    public enum SkeletonType
    {
        Solid,
        Grandient
    }

    public static class SkeletonLayerFactory
    {
        public static SkeletonLayer MakeLayer(SkeletonType type, UIColor[] colors, UIView holder)
        {
            return new SkeletonLayer(type, colors, holder);
        }

        public static CALayer MakeMultilineLayer(SkeletonType type, int index, nfloat width, SkeletonConfig config)
        {
            var layer = type.GetLayer();
            layer.AnchorPoint = CGPoint.Empty;
            layer.Name = CALayerExtensions.SkeletonSubLayersName;
            layer.Frame = new CGRect(0, index * config.SpaceRequiredForEachLine, width, config.MultilineHeight);
            return layer;
        }
    }

    public static class SkeletonTypeExtension
    {
        public static CALayer GetLayer(this SkeletonType This)
        {
            switch (This)
            {
                case SkeletonType.Solid:
                    return new CALayer();
                case SkeletonType.Grandient:
                    return new CAGradientLayer();
                default:
                    throw new NotSupportedException();
            }
        }

        public static CAAnimation GetLayerAnimation(this SkeletonType This, CALayer layer)
        {
            switch (This)
            {
                case SkeletonType.Solid:
                    return layer.Pulse();
                case SkeletonType.Grandient:
                    return layer.Sliding();
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public delegate CAAnimation SkeletonLayerAnimation(CALayer layer);

    public class SkeletonLayer : NSObject
    {
        private static readonly string SkeletonAnimationKey = "skeletonAnimation";

        private readonly WeakReference<UIView> _weakRefHolder;

        public SkeletonLayer(SkeletonType type, UIColor[] colors, UIView holder)
        {
            SkeletonType = type;
            _weakRefHolder = new WeakReference<UIView>(holder);
            ContentLayer = type.GetLayer();
            ContentLayer.AnchorPoint = CGPoint.Empty;
            ContentLayer.Bounds = holder.MaxBoundsEstimated();
            AddMultilinesIfNeeded();
            ContentLayer.Tint(colors);
        }

        public SkeletonType SkeletonType { get; }

        public CALayer ContentLayer { get; }

        public void StartAnimation(SkeletonLayerAnimation anim = null)
        {
            var animation = anim?.Invoke(ContentLayer) ?? SkeletonType.GetLayerAnimation(ContentLayer);
            ContentLayer.AddAnimation(animation, SkeletonAnimationKey);
        }

        public void StopAnimation()
        {
            ContentLayer.RemoveAnimation(SkeletonAnimationKey);
        }

        public void RemoveLayer()
        {
            ContentLayer.RemoveFromSuperLayer();
        }

        public void AddMultilinesIfNeeded()
        {
            if (!_weakRefHolder.TryGetTarget(out UIView holder))
                return;
            var containsMultilineText = holder.GetContainsMultilineText();
            if (containsMultilineText == null)
                return;
            ContentLayer.AddMultilinesLayers(containsMultilineText.NumLines, SkeletonType, containsMultilineText.LastLineFillingPercent);
        }
    }
}