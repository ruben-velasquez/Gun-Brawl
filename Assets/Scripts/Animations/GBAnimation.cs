using System.Collections.Generic;
using System;
using UnityEngine;

namespace Animation
{
    // GB es una abreviación del proyecto "Gun Brawl"
    // Esto sirve para diferenciar entre el animador
    // built-in de Unity
    [CreateAssetMenu(fileName = "Animation", menuName = "Gun Brawl/Animation", order = 0)]
    public class GBAnimation : ScriptableObject {
        public new string name;
        public List<Frame> frames;
        public event Action<Transform> onAnimationStart;
        public event Action<Transform> onAnimationEnd;
        public event Action<Transform> onFrameAction;

        // Obtener la duración de la animación
        public float GetDuration() {
            float duration = 0;

            foreach (Frame frame in frames)
            {
                duration += frame.time;
            }

            return duration;
        }

        public void AnimationStart(Transform transform) {
            onAnimationStart(transform);
        }
        public void AnimationEnd(Transform transform) {
            onAnimationEnd(transform);
        }
        public void FrameAction(Transform transform) {
            onFrameAction(transform);
        }
    }
}