﻿using ToySerialController.Config;
using ToySerialController.UI;
using UnityEngine;

namespace ToySerialController.MotionSource
{
    public interface IMotionSource : IUIProvider, IConfigProvider
    {
        Vector3 ReferencePosition { get; }
        Vector3 ReferenceUp { get; }
        Vector3 ReferenceRight { get; }
        Vector3 ReferenceForward { get; }
        float ReferenceLength { get; }
        Vector3 ReferencePlaneNormal { get; }

        Vector3 TargetPosition { get; }
        Vector3 TargetNormal { get; }

        bool Update();
    }
}