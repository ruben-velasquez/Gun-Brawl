using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Animation
{
    public class GBAnimator : MonoBehaviour
    {
        public List<GBAnimation> animations;

        [HideInInspector]
        public GBAnimation currentAnimation;
        [HideInInspector]
        public Frame currentFrame;
        [HideInInspector]
        public SpriteRenderer spr;
        [HideInInspector]
        public bool running;

        private float startTime;
        private Coroutine currentCoroutine;

        private void Start()
        {
            spr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Play("Destroy");
            }
        }

        public GBAnimation GetAnimation(string animationName)
        {
            return animations.Find((anim) => anim.name == animationName);
        }

        public void Stop(bool force)
        {
            if (running)
            {
                if (force || currentFrame.exitFrame) StopCoroutine(currentCoroutine);
            }
        }

        public void Play(string animationName)
        {
            // Verificamos si hay alguna animación ejecutandose
            // Si el actual frame no permite salir de la animación
            // No interrumpimos su ejecución
            if (running && !currentFrame.exitFrame) return;

            // Buscamos y ejecutamos la animación
            GBAnimation animation = animations.Find((anim) => anim.name == animationName);
            StartCoroutine(Animate(animation));
            currentAnimation = animation;
        }

        public void Play(GBAnimation animation)
        {
            // Verificamos si hay alguna animación ejecutandose
            // Si el actual frame no permite salir de la animación
            // No interrumpimos su ejecución
            if (running && !currentFrame.exitFrame) return;

            // Ejecutamos la animación
            StartCoroutine(Animate(animation));
            currentAnimation = animation;
        }

        public IEnumerator Animate(GBAnimation animation)
        {
            animation.AnimationStart(transform);
            startTime = Time.time;
            running = true;
            foreach (Frame frame in animation.frames)
            {
                currentFrame = frame; // Asignamos el frame como el frame actual

                if (frame.actionFrame)
                {
                    animation.FrameAction(transform);
                }

                spr.sprite = frame.sprite; // Asignamos el sprite del frame

                yield return new WaitForSeconds(frame.time); // Esperamos a que pase el tiempo indicado
            }
            running = false;
            startTime = 0f;
            animation.AnimationEnd(transform);
        }

        // Obtener el tiempo transcurrido de la animación
        public float GetElapsedTime()
        {
            return Time.time - startTime;
        }

        // Obtener el tiempo restante de la animación
        public float GetRemainingTime()
        {
            return currentAnimation.GetDuration() - GetElapsedTime();
        }
    }
}