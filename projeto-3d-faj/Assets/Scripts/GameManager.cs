using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public void EndGame()
    {


        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            //Reiniciar jogo
            Invoke("Restart", restartDelay);
        }



    }

    public void VerificaVitoria()
    {
        bool areAllEnemiesDead = EnemyAI.count == 0;
        Debug.Log("LOG DE CONTAGEM DE INIMIGOS: " + EnemyAI.count);
        if (areAllEnemiesDead)
        {
            Invoke("victory", restartDelay);
        }
        Invoke("VerificaVitoria", restartDelay);
    }
    void victory()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }


    void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
