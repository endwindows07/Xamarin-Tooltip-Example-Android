﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Tomergoldst.Tooltips;
using static Com.Tomergoldst.Tooltips.ToolTipsManager;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using App6.Droid;
using App6;

[assembly: ResolutionGroupName("CrossGeeks")]
[assembly: ExportEffect(typeof(DroidTooltipEffect), nameof(TooltipEffect))]
namespace App6.Droid
{
        public class DroidTooltipEffect : PlatformEffect
        {
            ToolTip toolTipView;
            ToolTipsManager _toolTipsManager;
            ITipListener listener;

            public DroidTooltipEffect()
            {
                listener = new TipListener();
                _toolTipsManager = new ToolTipsManager(listener);
            }

            void OnTap(object sender, EventArgs e)
            {
                var control = Control ?? Container;

                var text = TooltipEffect.GetText(Element);

                if (!string.IsNullOrEmpty(text))
                {
                    ToolTip.Builder builder;
                    var parentContent = control.RootView;
                    var position = TooltipEffect.GetPosition(Element);
                    switch (position)
                    {
                        case TooltipPosition.Top:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionAbove);
                            break;
                        case TooltipPosition.Left:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionLeftTo);
                            break;
                        case TooltipPosition.Right:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionRightTo);
                            break;
                        default:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionBelow);
                            break;
                    }
                    builder.SetAlign(ToolTip.AlignLeft);
                    builder.SetBackgroundColor(TooltipEffect.GetBackgroundColor(Element).ToAndroid());
                    builder.SetTextColor(TooltipEffect.GetTextColor(Element).ToAndroid());
                    toolTipView = builder.Build();
                    _toolTipsManager?.Show(toolTipView);
                }

            }


            protected override void OnAttached()
            {
                var control = Control ?? Container;
                control.Click += OnTap;
            }


            protected override void OnDetached()
            {
                var control = Control ?? Container;
                control.Click -= OnTap;
                _toolTipsManager.FindAndDismiss(control);
            }

            class TipListener : Java.Lang.Object, ITipListener
            {
                public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
                {

                }
            }
        }
}