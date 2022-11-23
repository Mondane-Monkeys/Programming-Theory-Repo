using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//In-Editor only
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NewGame(){
        IOInterface.DefaultGame();
        SceneManager.LoadScene(1);
    }
    
    public void ResumeGame(){
        SceneManager.LoadScene(1);
    }
    
    public void Quit(){
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); //editor exit command
#else
        Application.Quit(); //compiled exit command
#endif
    }
}
