﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IStreamYouScream
{
    [System.Serializable] public class Vector3Event : UnityEvent<Vector3> { };
    [System.Serializable] public class FloatEvent : UnityEvent<float> { }
    [System.Serializable] public class StringEvent : UnityEvent<string> { }
    [System.Serializable] public class StringsEvent : UnityEvent<List<string>> { }
    [System.Serializable] public class IntEvent : UnityEvent<int> { };
}
