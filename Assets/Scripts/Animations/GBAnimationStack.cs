using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    [CreateAssetMenu(fileName = "Animation Stack", menuName = "Gun Brawl/Animation Stack", order = 0)]
    public class GBAnimationStack : ScriptableObject {
        public List<GBAnimation> animations = new List<GBAnimation>(); // Animaciones
        public bool playOnStart;
        public string initialAnimation; // Animación inicial (central)

        public GBAnimation GetAnimation(string animationName)
        {
            GBAnimation animation = animations.Find((anim) => anim.name == animationName);

            if (animation == null)
            {
                Debug.LogError("No se encontró la animación " + animationName);
                return null;
            }

            return animation;
        }

        public GBAnimation GetInitialAnimation() {
            return GetAnimation(initialAnimation);
        }

        // Management

        public void AddAnimation(GBAnimation newAnimation) {
            animations.Add(newAnimation);
        }

        public void Clear() {
            animations.Clear();
        }
    }
}