using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IKillable
{
    Animator animator;
    [SerializeField] GameManager gameManager;
    [SerializeField] Slider healthSlider, shieldSlider;
    [SerializeField] internal int totalHealt = 200;
    internal bool hasShield, isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //totalHealt = 200;
        hasShield = true;
        isDead = false;
    }
    private void Update()
    {
        if (totalHealt > 100)
        {
            hasShield = true;
            shieldSlider.gameObject.SetActive(true);
            healthSlider.gameObject.SetActive(true);
        }
        else if (totalHealt < 100 && totalHealt > 0)
        {
            hasShield = false;
            shieldSlider.gameObject.SetActive(false);
        }
        else if (totalHealt <= 0)
        {
            healthSlider.gameObject.SetActive(false);
            if (!isDead)
            {
                Kill();
            }
        }
        if (hasShield)
        {
            shieldSlider.value = totalHealt - 100;
            healthSlider.value = 100;
        }
        if (!hasShield)
        {
            healthSlider.value = totalHealt;
        }
    }
    public void Kill()
    {
        animator.SetTrigger("Die");
        isDead = true;
        StartCoroutine(DeadCoroutine());
    }
    IEnumerator DeadCoroutine()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(1);
            SpawnManager.Instance.Spawn(this.gameObject);
            this.isDead = false;
            this.totalHealt = 200;
            this.animator.Rebind();
            this.animator.Update(0f);
            gameManager.PlayerHealthUpdate();
        }

        if (this.gameObject.CompareTag("Enemy"))
        {
            yield return new WaitForSeconds(1);
            SpawnManager.Instance.Spawn(this.gameObject);
            this.isDead = false;
            this.totalHealt = 200;
            this.animator.Rebind();
            this.animator.Update(0f);
            gameManager.EnemyHealthUpdate();
        }
    }
}