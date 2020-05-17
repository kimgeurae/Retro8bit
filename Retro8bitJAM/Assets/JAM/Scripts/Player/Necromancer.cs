using JAM.Scripts.Input;
using JAM.Scripts.Interfaces;
using JAM.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JAM.Scripts.Player
{
    public class Necromancer : MonoBehaviour, IKilleable
    {
        public void Kill()
        {
            if(Application.isEditor) Debug.Log($"Necromancer Kill Called");
            CharacterManager.RemoveCharacter(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}