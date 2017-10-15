using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SensorRenderer : MonoBehaviour {
	public SensorUpdater SUpdater;
	public Text text;

	void Update() {
		text.text ="accel-X:" + SUpdater.acceleration.x.ToString()
				  + "\n" + "accel-Y:" + SUpdater.acceleration.y.ToString()
				  + "\n" + "accel-Z:" + SUpdater.acceleration.z.ToString()
				  + "\n" + "gyro-X:" + SUpdater.gyro.x.ToString()
				  + "\n" + "gyro-Y:" + SUpdater.gyro.y.ToString()
				  + "\n" + "gyro-Z:" + SUpdater.gyro.z.ToString();
	}
}