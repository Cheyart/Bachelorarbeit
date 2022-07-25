using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LerpPosition(Vector3 startPosition, Vector3 targetPosition, float duration, Space space, EasingFunction easing)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            //t = TransitionHelper.EaseIn(t);
            if (space == Space.Self)
            {
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, AnimationHelper.CalculateEasing(t, easing));
            }
            else if (space == Space.World)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, AnimationHelper.CalculateEasing(t, easing));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (space == Space.Self)
        {
            transform.localPosition = targetPosition;
        }
        else if (space == Space.World)
        {
            transform.position = targetPosition;
        }
    }


    public IEnumerator LerpRotation(Quaternion startRotation, Quaternion targetRotation, float duration, Space space, EasingFunction easing)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            if (space == Space.Self)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, AnimationHelper.CalculateEasing(t, easing));

            }
            else if (space == Space.World)
            {
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, AnimationHelper.CalculateEasing(t, easing));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (space == Space.Self)
        {
            transform.localRotation = targetRotation;
        }
        else if (space == Space.World)
        {
            transform.rotation = targetRotation;
        }
    }
}
