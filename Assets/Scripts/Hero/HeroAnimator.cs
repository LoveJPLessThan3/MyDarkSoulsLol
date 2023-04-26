using UnityEngine;

internal class HeroAnimator : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;

    private void Update()
    {
        Animator.SetFloat("Walking", CharacterController.velocity.magnitude, 0.1f, Time.deltaTime); //возвращаем текущую скорость.длину вектора
    }
}
