using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Animation
{
    public class GBAnimator : MonoBehaviour
    {
        [SerializeField]
        public GBAnimationStack animationStack;

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

        public event Action onAnimationStart; // Evento que se ejecuta cuando una animación empieza
        public event Action onAnimationEnd; // Evento que se ejecuta cuando una animación termina
        public event Action<GBAnimation> onAnimationRequest; // Evento que se ejecuta cuando una animación no se ejecuta


        private void Start()
        {
            spr = GetComponent<SpriteRenderer>();

            if(animationStack.playOnStart) {
                Play(animationStack.GetInitialAnimation());
            }
        }

        public GBAnimation GetAnimation(string animationName)
        {
            return animationStack.GetAnimation(animationName);
        }

        public void Stop(bool force)
        {
            if (running)
            {
                if (force || currentFrame.exitFrame) {
                    running = false;
                    startTime = 0f;
                    currentAnimation = null;
                    currentFrame = null;

                    StopCoroutine(currentCoroutine);
                    currentCoroutine = null;
                };
            }
        }

        public void Play(string animationName)
        {
            // Verificamos si hay alguna animación ejecutandose
            // Si el actual frame no permite salir de la animación
            // No interrumpimos su ejecución
            if (running && !currentFrame.exitFrame) {
                if(onAnimationRequest != null) onAnimationRequest(GetAnimation(animationName));
                return;
            }

            if(running && currentAnimation.name == animationName) {
                return;
            }

            if(currentCoroutine != null) {
                StopCoroutine(currentCoroutine);
            }

            // Buscamos y ejecutamos la animación
            GBAnimation animation = GetAnimation(animationName);

            if(animation == null) return;

            currentAnimation = animation;
            currentCoroutine = StartCoroutine(Animate(animation));
        }

        public void Play(GBAnimation animation)
        {
            // Verificamos si hay alguna animación ejecutandose
            // Si el actual frame no permite salir de la animación
            // No interrumpimos su ejecución
            if (running && !currentFrame.exitFrame) {
                if (onAnimationRequest != null) onAnimationRequest(animation);
                return;
            }

            if (running && currentAnimation.name == animation.name)
            {
                return;
            }

            if (currentCoroutine != null) {
                StopCoroutine(currentCoroutine);
            }

            if (animation == null) return;

            // Ejecutamos la animación
            currentAnimation = animation;
            currentCoroutine = StartCoroutine(Animate(animation));
        }

        private IEnumerator Animate(GBAnimation animation)
        {
            AnimationStart(animation);

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

            AnimationEnd(animation);
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

        public void AnimationStart(GBAnimation animation) {
            if(onAnimationStart != null) onAnimationStart();
            animation.AnimationStart(transform);
            startTime = Time.time;
            running = true;
        }
        public void AnimationEnd(GBAnimation animation) {
            running = false;
            startTime = 0f;
            currentAnimation = null;
            currentFrame = null;

            StopCoroutine(currentCoroutine);
            currentCoroutine = null;

            if (animation.loop)
            { // Si la animación hace un loop volvemos a ejecutar la animación
                animation.Loop(transform);
                Play(animation);
            }
            else
            { // Si no hace loop simplemente terminamos la animación
                if (onAnimationEnd != null) onAnimationEnd();
                animation.AnimationEnd(transform);
            }
        }
    }
}