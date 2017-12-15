//
// ViewController+Picker.cs
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
using System.Collections.Generic;
using Foundation;
using UIKit;
using System.Linq;

namespace SkeletonView.Exemple
{
    public partial class ViewController : IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        private Dictionary<UIColor, string> _colors = new Dictionary<UIColor, string>
        {
            [SkeletonColors.Turquoise] = "turquoise",
            [SkeletonColors.Emerald] = "emerald",
            [SkeletonColors.PeterRiver] = "peterRiver",
            [SkeletonColors.Amethyst] = "amethyst",
            [SkeletonColors.WetAsphalt] = "wetAsphalt",
            [SkeletonColors.Nephritis] = "nephritis",
            [SkeletonColors.BelizeHole] = "belizeHole",
            [SkeletonColors.Wisteria] = "wisteria",
            [SkeletonColors.MidnightBlue] = "midnightBlue",
            [SkeletonColors.SunFlower] = "sunFlower",
            [SkeletonColors.Carrot] = "carrot",
            [SkeletonColors.Alizarin] = "alizarin",
            [SkeletonColors.Clouds] = "clouds",
            [SkeletonColors.Concrete] = "concrete",
            [SkeletonColors.FlatOrange] = "flatOrange",
            [SkeletonColors.Pumpkin] = "pumpkin",
            [SkeletonColors.Pomegranate] = "pomegranate",
            [SkeletonColors.Silver] = "silver",
            [SkeletonColors.Asbestos] = "asbestos",
        };


        public nint GetComponentCount(UIPickerView pickerView) => 1;


        public nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return _colors.Count;
        }

        [Export("pickerView:titleForRow:forComponent:")]
        public string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return _colors[_colors.Keys.ElementAt((int)row)];
        }       
    }
}
