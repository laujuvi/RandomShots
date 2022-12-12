using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMEnu : MonoBehaviour
{
    // Start is called before the first frame update
    public void CargarJuego()
    {
        SceneManager.LoadScene(1);
    }
    public void CargarNivel2()
    {
        SceneManager.LoadScene(2);
    }
    public void CargarNivel3()
    {
        SceneManager.LoadScene(3);
    }
}
