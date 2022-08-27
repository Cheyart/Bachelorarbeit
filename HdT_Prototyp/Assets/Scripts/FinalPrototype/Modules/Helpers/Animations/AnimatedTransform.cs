using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class AnimatedTransform This class contains functions to animate the Transform component.
 */
public class AnimatedTransform : MonoBehaviour
{

    /** Lerp the Position of the Transform component
     * @param startValue Start Position
     * @param targetValue Target Position
     * @param duration Duration of the animation
     * @param space indicates in which space the position change should take place in (World or Self)
     * @param easing Easing function which will be used for the animation
     */
    public IEnumerator LerpPosition(Vector3 startPosition, Vector3 targetPosition, float duration, Space space, EasingFunction easing)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            if (space == Space.Self)
            {
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, EasingFunctionCalculator.CalculateEasing(t, easing));
            }
            else if (space == Space.World)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, EasingFunctionCalculator.CalculateEasing(t, easing));
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

    /** Lerp the Rotation of the Transform component
    * @param startValue Start Rotation
    * @param targetValue Target Rotation
    * @param duration Duration of the animation
    * @param space indicates in which space the position change should take place in (World or Self)
    * @param easing Easing function which will be used for the animation
    */
    public IEnumerator LerpRotation(Quaternion startRotation, Quaternion targetRotation, float duration, Space space, EasingFunction easing)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            if (space == Space.Self)
            {
                transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, EasingFunctionCalculator.CalculateEasing(t, easing));

            }
            else if (space == Space.World)
            {
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, EasingFunctionCalculator.CalculateEasing(t, easing));
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
