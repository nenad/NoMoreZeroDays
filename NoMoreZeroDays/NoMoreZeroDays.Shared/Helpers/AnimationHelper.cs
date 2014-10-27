using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Media.Animation;

namespace NoMoreZeroDays.Helpers
{
    class AnimationHelper
    {

        public static void Play(Storyboard anim)
        {
            anim.AutoReverse = false;
            anim.BeginTime = new TimeSpan(0, 0, 0);
            anim.Begin();
        }

        public static void Reverse(Storyboard anim)
        {
            anim.AutoReverse = true;
            var time = new TimeSpan(0, 0, 1);
            anim.Seek(time);
            anim.Resume();
        }

    }
}
