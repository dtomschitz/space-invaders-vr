using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData {

    public float health;
    public float shieldHealth;

    public float enemyHealth;
    public float[] enemyPosition;

    public GameData (Player player, ForceField forceField, Enemy enemy)
    {
        health = player.CurrentHealth;
        shieldHealth = forceField.CurrentShieldPower;

        enemyHealth = enemy.CurrentHealth;
        enemyPosition = new float[3];
        enemyPosition[0] = enemy.transform.position.x;
        enemyPosition[1] = enemy.transform.position.y;
        enemyPosition[2] = enemy.transform.position.z;
    }

}
