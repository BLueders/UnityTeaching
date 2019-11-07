using UnityEngine;
using System.Collections;

public class Singleton<TSingleton> : MonoBehaviour where TSingleton : MonoBehaviour
{
	private static TSingleton _instance;

	public static TSingleton Instance { 
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<TSingleton> ();	
			}
			return _instance; 
		} 
	}
}

