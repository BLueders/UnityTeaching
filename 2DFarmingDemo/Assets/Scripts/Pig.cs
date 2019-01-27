using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Animal {

	public override void Feed(){
		transform.localScale = transform.localScale * 1.2f;
	}
}
