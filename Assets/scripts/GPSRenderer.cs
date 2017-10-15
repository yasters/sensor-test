using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPSRenderer : MonoBehaviour {
	public GPSUpdater GUpdater;
	public Text text;

	void Update() {
		text.text = GUpdater.Status.ToString()
				  + "\n" + "lat:" + GUpdater.Location.latitude.ToString()
				  + "\n" + "lng:" + GUpdater.Location.longitude.ToString()
				  + "\n" + "speed:" + GUpdater.speed.ToString()
				  + "\n" + "timestamp:" + GUpdater.Location.timestamp.ToString()
				  + "\n" + "time:" + GUpdater.time.ToString();
	}
}