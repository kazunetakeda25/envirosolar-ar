using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace com.cortastudios.DynamicColorCorrection.DemoScene
{
    [RequireComponent(typeof(Image))]
    ///There are for images in the demo scene showing the active color curves affecting it.
    ///This script controls the transparency of those images (which, in turn, clue the user on
    ///how much each curve is affecting the scene, in realtime).
    ///This script is for the demo scene only and probably have no use on any other project in which
    ///Dynamic Color Correction is used.
    public class DemoCurveImageBehavior : MonoBehaviour
    {
        public bool InverseTransparency;
        public bool IsDepthCurve;
        private Image ImageComponent;

        void Start()
        {
            ImageComponent = GetComponent<Image>();
            if (IsDepthCurve && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionCurves>().mode == ColorCorrectionCurves.ColorCorrectionMode.Simple)
                ImageComponent.enabled = false;
        }

        public void SetTransparency(float transparency)
        {
            var newColor = ImageComponent.color;
            if (InverseTransparency)
                transparency = 1 - transparency;
            newColor.a = transparency;
            ImageComponent.color = newColor;
        }
    }
}