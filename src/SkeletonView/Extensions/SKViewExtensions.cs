//
// SKViewExtensions.cs
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
using System.Linq;

namespace SkeletonView.Extensions
{
    public static class SKViewExtensions
    {
        public static UIView[] SubviewsSkeletonables(this UIView This)
        {
            return This.Subviews
                       .Where(v => v.GetIsSkeletonable())
                       .ToArray();
        }

        public static void AddSkeletonLayer(
            this UIView This,
            UIColor[] colors, SkeletonType type,
            GradientDirection? direction = null, bool animated = false, SkeletonLayerAnimation animation = null)
        {
            var skeletonLayer = SkeletonLayerFactory.MakeLayer(type, colors, This);

            if (direction.HasValue && animation == null)
                animation = direction.Value.SlidingAnimation();

            This.SetSkeletonLayer(skeletonLayer);

            This.Layer.InsertSublayer(skeletonLayer.ContentLayer, int.MaxValue);
            if (animated)
                skeletonLayer.StartAnimation(animation);
            This.SetSkeletonStatus(Views.Status.On);
        }

        public static void RemoveSkeletonLayer(this UIView This)
        {
            if (!This.IsSkeletonActive())
                return;
            var layer = This.GetSkeletonLayer();
            layer.StopAnimation();
            layer.RemoveLayer();
            This.SetSkeletonStatus(Views.Status.Off);
        }

        public static void AddDummyDataSourceIfNeeded(this UIView This)
        {

        }

        public static void RemoveDummyDataSourceIfNeeded(this UIView This, bool reloadAfter = true)
        {

        }

        /// <summary>
        /// Shows the skeleton using DefaultConfig color
        /// </summary>
        /// <param name="This">This.</param>
        public static void ShowSkeleton(this UIView This)
        {
            ShowSkeleton(This, SkeletonConfig.Default.TintColor);
        }

        public static void ShowSkeleton(this UIView This, UIColor color)
        {
            ShowSkeleton(This, new[] { color });
        }

        /// <summary>
        /// Shows the gradient skeleton using DefaultConfig gradientColor
        /// </summary>
        /// <param name="This">This.</param>
        public static void ShowGradientSkeleton(this UIView This)
        {
            ShowGradientSkeleton(This, SkeletonConfig.Default.Gradient);
        }

        public static void ShowGradientSkeleton(this UIView This, SkeletonGradient gradient)
        {
            ShowSkeleton(This, gradient.Colors, SkeletonType.Grandient);
        }

        /// <summary>
        /// Shows the gradient skeleton using DefaultConfig color
        /// </summary>
        /// <param name="This">This.</param>
        public static void ShowAnimatedSkeleton(this UIView This)
        {
            ShowAnimatedSkeleton(This, SkeletonConfig.Default.TintColor);
        }

        public static void ShowAnimatedSkeleton(this UIView This, UIColor color)
        {
            ShowSkeleton(This, new[] { color }, animated: true);
        }

        /// <summary>
        /// Shows the gradient skeleton using DefaultConfig gradientColor
        /// </summary>
        /// <param name="This">This.</param>
        public static void ShowAnimatedGradientSkeleton(this UIView This)
        {
            ShowAnimatedGradientSkeleton(This, SkeletonConfig.Default.Gradient);
        }

        public static void ShowAnimatedGradientSkeleton(this UIView This, SkeletonGradient gradient, SkeletonLayerAnimation animation = null)
        {
            ShowSkeleton(This, gradient.Colors, animated: true, animation: animation);
        }

        public static void ShowSkeleton(
            this UIView This, UIColor[] colors,
            SkeletonType type = SkeletonType.Solid,
            bool animated = false,
            SkeletonLayerAnimation animation = null)
        {
            AddDummyDataSourceIfNeeded(This);
            var views = SubviewsSkeletonables(This);

            views.RecursiveSearch(() =>
            {
                if (This.IsSkeletonActive())
                    return;
                This.UserInteractionEnabled = false;
                This.PrepareForSkeleton();
                AddSkeletonLayer(This, colors, type, animated: animated, animation: animation);
            }, (v) => v.ShowSkeleton(colors, type, animated, animation));
        }

        public static void HideSkeleton(this UIView This, bool reloadDataAfter = true)
        {
            RemoveDummyDataSourceIfNeeded(This, reloadDataAfter);
            This.UserInteractionEnabled = true;

            var views = SubviewsSkeletonables(This);
            views.RecursiveSearch(This.RemoveSkeletonLayer, v => HideSkeleton(v, reloadDataAfter));
        }

        public static void StartSkeletonAnimation(this UIView This, SkeletonLayerAnimation anim = null)
        {
            var views = SubviewsSkeletonables(This);

            views.RecursiveSearch(
                () => StartSkeletonAnimation(This, anim),
                v => StartSkeletonAnimation(v, anim));
        }

        public static void StopSkeletonAnimation(this UIView This)
        {
            var views = SubviewsSkeletonables(This);

            views.RecursiveSearch(
                () => StopSkeletonAnimation(This),
                StopSkeletonLayerAnimationBlock);
        }

        private static void StartSkeletonLayerAnimationBlock(UIView view, SkeletonLayerAnimation anim = null)
        {
            view.GetSkeletonLayer()?.StartAnimation(anim);
        }

        private static void StopSkeletonLayerAnimationBlock(UIView view)
        {
            view.GetSkeletonLayer()?.StopAnimation();
        }
    }
}
