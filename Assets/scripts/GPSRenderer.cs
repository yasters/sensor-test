using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GPSRenderer : MonoBehaviour {
	public GPSUpdater updater;
	public Text text;

	void Update() {
		text.text = updater.Status.ToString()
				  + "\n" + "lat:" + updater.Location.latitude.ToString()
				  + "\n" + "lng:" + updater.Location.longitude.ToString()
				  + "\n" + "speed:" + updater.speed.ToString()
				  + "\n" + "timestamp:" + updater.Location.timestamp.ToString()
				  + "\n" + "time:" + updater.time.ToString();
	}
}