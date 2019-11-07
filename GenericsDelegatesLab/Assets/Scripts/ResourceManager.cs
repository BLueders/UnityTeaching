using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager {

	public ResourceManager instance;

	void Awake(){
		instance = this;
	}

	// this method can be called to load a sprite ressource from the url. onComplete should be executed once the download is completed.
	// you can use http://benno-lueders.de/img/victory_yoda.jpg for testing.
	public static void LoadSpriteRessource(string url, Action<Sprite> onComplete){
		throw new NotImplementedException ();
	}
}
