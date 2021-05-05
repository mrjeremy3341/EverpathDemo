using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageCounter : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifetime = 1f;
    public float moveSpeed = 1f;
    public float placementJitter = 0.5f;

    private void Update()
    {
        Destroy(this.gameObject, lifetime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damage)
    {
        damage = -damage;
        damageText.text = damage.ToString();

        float posX = Random.Range(-placementJitter, placementJitter);
        float posY = Random.Range(-placementJitter, placementJitter);
        transform.position += new Vector3(posX, posY, 0f);
    }
}
