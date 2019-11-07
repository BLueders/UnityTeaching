using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager {

	// this method can be called to load a sprite ressource from the url. onComplete should be executed once the download is completed.
	// you can use http://benno-lueders.de/img/victory_yoda.jpg for testing.
	public static void LoadSpriteRessource(string url, Action<Sprite> onComplete){
		StaticCoroutine.RunCoroutine (LoadSpriteRoutine (url, onComplete));
	}

	public static IEnumerator LoadSpriteRoutine(string url, Action<Sprite> onComplete){
		WWW www = new WWW (url);
		yield return www;
		Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
		onComplete (sprite);
	}
}
