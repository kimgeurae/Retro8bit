using System.Collections.Generic;
using JAM.Scripts.Input;
using JAM.Scripts.Player;
using TMPro;
using UnityEngine;

namespace JAM.Scripts.Managers
{
    public class CharacterManager : MonoBehaviour
    {
        private static List<GameObject> _characters = new List<GameObject>();
        private static int _id;

        public static void AddCharacter(GameObject entity)
        {
            _characters.Add(entity);
            if(Application.isEditor) Debug.Log($"AddCharacter executed");
            if(Application.isEditor) Debug.Log($"Added Entity {entity}");
            SwitchCharacter();
        }
        
        public static void RemoveCharacter(GameObject entity)
        {
            _characters.Remove(entity);
            if(Application.isEditor) Debug.Log($"RemoveCharacter executed");
            if(Application.isEditor) Debug.Log($"Removed Entity {entity}");
            SwitchCharacter();
        }

        public static GameObject ActiveCharacter()
        {
            return _characters[_id];
        }

        public static void SwitchCharacter()
        {
            if (_characters.Count < 1) return;
            foreach (var characters in _characters)
            {
                if (characters.GetComponent<PlayerNecromancerInput>() != null)
                {
                    characters.GetComponent<PlayerNecromancerInput>().enabled = false;
                }
                else
                {
                    characters.GetComponent<PlayerMinionInput>().enabled = false;
                }
            }
            if(Application.isEditor) Debug.Log($"_id + 1 < _characters.Count = {_id + 1 < _characters.Count}");
            _id = _id + 1 < _characters.Count ? _id = _id+1 : _id = 0;
            if (_characters[_id].GetComponent<PlayerNecromancerInput>() != null)
            {
                _characters[_id].GetComponent<PlayerNecromancerInput>().enabled = true;
            }
            else
            {
                _characters[_id].GetComponent<PlayerMinionInput>().enabled = true;
            }
            if(Application.isEditor) Debug.Log($"SwitchCharacter executed");
            if(Application.isEditor) Debug.Log($"Character count = {_characters.Count}");
            if(Application.isEditor) Debug.Log($"id = {_id}");
        }
    }
}