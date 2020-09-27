using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEditor;

class Light2dHack
{
    static void SetFieldValue<T>(object obj, string name, T val)
    {
        var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        field?.SetValue(obj, val);
    }

    public static void SetShapePath(Light2D light, Vector3[] path)
    {
        SetFieldValue<Vector3[]>(light, "m_ShapePath", path);
    }
}

namespace IStreamYouScream
{
    [ExecuteInEditMode]
    public class LightShapedByCollider : MonoBehaviour
    {
        private Light2D light2d { get { return gameObject.GetComponent<Light2D>(); } }
        private Vector2[] points { get { return polygon.points; } }
        [SerializeField] PolygonCollider2D polygon;

        private void Awake()
        {
            DrawLight();
        }

        private void Start()
        {
            DrawLight();
        }

        private void OnDrawGizmosSelected()
        {
            DrawLight();
        }
        private void DrawLight()
        {
            Vector3[] tmp = new Vector3[points.Length];

            for (int i = 0; i < points.Length; ++i)
            {
                Vector2 p = points[i];
                tmp[i] = new Vector3(p.x, p.y, 0);
            }

            Light2dHack.SetShapePath(light2d, tmp);
        }
    }
}