using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeHareket : MonoBehaviour
{
    #region Field Region
    [SerializeField] Joystick moverJoystick;
    [SerializeField] Transform playerMovementDot;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 2f;
    Health playerHealth;
    #endregion

    #region Unity Mehtods
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    private void Update()
    {
        if (!playerHealth.isDead)
        {
            if (moverJoystick.Horizontal > 0.3 || moverJoystick.Horizontal < -0.3 || moverJoystick.Vertical > 0.3 || moverJoystick.Vertical < -0.3)
            {
                MoveDot();
                LookAtDot();
                //transform.Translate(speed * Time.deltaTime * Vector3.forward);
                //animator.SetBool("Walk", true);
            }
            //else
                //animator.SetBool("Walk", false);
        }
    }
    #endregion

    #region Private Methods
    private void LookAtDot()
    {
        transform.LookAt(new Vector3(playerMovementDot.position.x, 0f, playerMovementDot.position.z));
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
    private void MoveDot()
    {
        playerMovementDot.position = new Vector3
            (moverJoystick.Horizontal + transform.position.x, playerMovementDot.transform.position.y, moverJoystick.Vertical + transform.position.z);
    }
    #endregion
}