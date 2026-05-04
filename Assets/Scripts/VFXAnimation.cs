using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VFXAnimation : MonoBehaviour
{
    [SerializeField] private string _animationName = "Default";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("VFXAnimation requires an Animator component!", gameObject);
            Destroy(gameObject);
            return;
        }

        // Play the animation
        _animator.Play(_animationName);

        // Wait for animation to finish and destroy
        StartCoroutine(WaitForAnimationAndDestroy());
    }

    private IEnumerator WaitForAnimationAndDestroy()
    {
        // Wait for the animation to start
        yield return null;

        // Get the current animation state info
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        // Wait for the animation to finish
        yield return new WaitForSeconds(stateInfo.length);

        // Destroy this gameobject
        Destroy(gameObject);
    }
}
