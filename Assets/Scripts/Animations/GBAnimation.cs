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
        public new string name; // Nombre de la animación
        public bool loop; // ¿La animación se reproduce en bucle?
        [HideInInspector]
        public List<Frame> frames = new List<Frame>(); // Los frames de la animación
        public event Action<Transform> onAnimationStart; // Evento que se ejecuta cuando empieza la animación
        public event Action<Transform> onAnimationEnd; // Evento que se ejecuta cuando termina la animación
        public event Action<Transform> onFrameAction; // Evento que se ejecuta cuando hay un "Frame Action"
        public event Action<Transform> onLoop; // Evento que se ejecuta cuando se repite una animación que se ejecuta en bucle
        public int priority = 1;

        // Obtener la duración de la animación
        public float GetDuration() {
            float duration = 0;

            if(frames == null) new List<Frame>();

            foreach (Frame frame in frames)
            {
                duration += frame.time;
            }

            return duration;
        }

        public void AnimationStart(Transform transform) {
            if(onAnimationStart != null)
                onAnimationStart(transform);
        }
        public void AnimationEnd(Transform transform) {
            if (onAnimationEnd != null)
                onAnimationEnd(transform);
        }
        public void FrameAction(Transform transform) {
            if (onFrameAction != null)
                onFrameAction(transform);
        }
        public void Loop(Transform transform) {
            if (onLoop != null)
                onLoop(transform);
        }

        // Management

        public void AddFrame(Frame newFrame) {
            frames.Add(newFrame);
        }

        public void Clear() {
            frames.Clear();
        }
    }
}