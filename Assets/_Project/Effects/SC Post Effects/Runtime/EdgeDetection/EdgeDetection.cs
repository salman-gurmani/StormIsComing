﻿using UnityEngine.Rendering;

using System;
using UnityEngine;
using System.ComponentModel;
#if PPS
using UnityEngine.Rendering.PostProcessing;
using TextureParameter = UnityEngine.Rendering.PostProcessing.TextureParameter;
using BoolParameter = UnityEngine.Rendering.PostProcessing.BoolParameter;
using FloatParameter = UnityEngine.Rendering.PostProcessing.FloatParameter;
using IntParameter = UnityEngine.Rendering.PostProcessing.IntParameter;
using ColorParameter = UnityEngine.Rendering.PostProcessing.ColorParameter;
using MinAttribute = UnityEngine.Rendering.PostProcessing.MinAttribute;
using DisplayName = UnityEngine.Rendering.PostProcessing.DisplayNameAttribute;
#endif

namespace SCPE
{
#if PPS
    [PostProcess(typeof(EdgeDetectionRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Stylized/Edge Detection", true)]
#endif
    [Serializable]
    public sealed class EdgeDetection : PostProcessEffectSettings
    {
#if PPS
        [Range(0f, 1f), DisplayName("Edges Only"), Tooltip("Shows only the effect, to allow for finetuning")]
        public BoolParameter debug = new BoolParameter { value = false };

        public enum EdgeDetectMode
        {
            DepthNormals = 0,
            CrossDepthNormals = 1,
            SobelDepth = 2,
            LuminanceBased = 3,
        }

        [Serializable]
        public sealed class EdgeDetectionMode : ParameterOverride<EdgeDetectMode> { }

        [Space]
        [Tooltip("Choose one of the different edge solvers")]
        public EdgeDetectionMode mode = new EdgeDetectionMode { value = EdgeDetectMode.DepthNormals };

        public BoolParameter invertFadeDistance = new BoolParameter { value = false };
        [Min(0.01f), Range(0.01f, 10000)]
        [Tooltip("Fades out the effect between the cameras near and far clipping plane")]
        public FloatParameter fadeDistance = new FloatParameter { value = 1000f };

        [Header("Sensitivity")]
        [DisplayName("Depth"), Range(0f, 1f), Tooltip("Sets how much difference in depth between pixels contribute to drawing an edge")]
        public FloatParameter sensitivityDepth = new FloatParameter { value = 0f };

        [DisplayName("Normals"), Range(0f, 1f), Tooltip("Sets how much difference in normals between pixels contribute to drawing an edge")]
        public FloatParameter sensitivityNormals = new FloatParameter { value = 1f };

        [Range(0.01f, 1f), DisplayName("Luminance Threshold"), Tooltip("Luminance threshold, pixels above this threshold will contribute to the effect")]
        public FloatParameter lumThreshold = new FloatParameter { value = 0.01f };

        [Header("Edges")]
        [DisplayName("Color"), Tooltip("")]
        public ColorParameter edgeColor = new ColorParameter { value = Color.black };

        [Range(1f, 50f), Tooltip("Edge Exponent")]
        public FloatParameter edgeExp = new FloatParameter { value = 1f };

        [DisplayName("Size"), Range(1, 4), Tooltip("Edge Distance")]
        public IntParameter edgeSize = new IntParameter { value = 1 };

        [DisplayName("Opacity"), Range(0f, 1f), Tooltip("Opacity")]
        public FloatParameter edgeOpacity = new FloatParameter { value = 0f };

        [DisplayName("Thin"), Tooltip("Limit the effect to inward edges only")]
        public BoolParameter sobelThin = new BoolParameter { value = false };

        public override bool IsEnabledAndSupported(PostProcessRenderContext context)
        {
            if (enabled.value)
            {
                if (edgeOpacity > 0)
                    return true;
            }

            return false;
        }
#endif
    }
}