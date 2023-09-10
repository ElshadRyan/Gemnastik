using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnsikloScene : MonoBehaviour
{
    public void GoToEnsiklo()
    {
        SceneManager.LoadScene("Ensiklopedia");
    }

    public void GetOutOfEnsiklo()
    {
        SceneManager.LoadScene("WorldMap");
    }

}
