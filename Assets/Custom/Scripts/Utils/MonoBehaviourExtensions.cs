using System;
using UnityEngine;

namespace Assets.Custom.Scripts.Utils
{
    public static class MonoBehaviourExtensions
    {
        public static void Do<T>(this T monoBehaviour, Action<T> action) where T : MonoBehaviour
        {
            if (monoBehaviour != null & action != null)
            {
                action(monoBehaviour);
            }
        }
    }
}