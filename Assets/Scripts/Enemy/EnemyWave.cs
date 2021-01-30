using UnityEngine;
using UnityEngine.AI;

public class EnemyWave : MonoBehaviour{

    public static EnemyWave instance;
    public int enemyCount;

    public int currentWaveState;


    public struct WaveState
    {
        public const int Tutorial = 1;
        public const int Easy = 2;
        public const int Medium = 3;
        public const int Hard = 4;

    };


    void Awake()
    {
        instance = this;
        
    }

    void Update()
    {

        if(enemyCount == 0){
            currentWaveState = getNextWaveState();
        }
    }

    int getNextWaveState()
    {
        switch (currentWaveState)
        {
            case WaveState.Tutorial:
                return WaveState.Easy;
                break;
            case WaveState.Easy:
                return WaveState.Medium;
                break;
            case WaveState.Medium:
                return WaveState.Hard;
                break;
            case WaveState.Hard:
                return WaveState.Hard;
                break;
            default:
                return 0;
                break;
        }
    }


}