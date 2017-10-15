using UnityEngine;
using System.Collections;

public class GPSLoader : MonoBehaviour {
	public int R_EARTH = 6378137;
	public float lat1, lat2 = 0, lon1, lon2 = 0, altitude, accuracy, RAD = Mathf.PI / 180, lat_c, dx, dy, distance, speed;
	public double timestamp1, timestamp2, time;
	public GUIStyle labelStyle;

	IEnumerator Start() {
		this.labelStyle = new GUIStyle();
		this.labelStyle.fontSize = Screen.height / 22;
		this.labelStyle.normal.textColor = Color.white;

		if (!Input.location.isEnabledByUser) {
			yield break;
		}
		Input.location.Start();
		int maxWait = 120;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			print("Timed out");
			yield break;
		}
		int i = 1;
		while (i > 0) {
			if (Input.location.status == LocationServiceStatus.Failed) {
				print("Unable to determine device location");
				yield break;
			} else {
				this.lat1 = Input.location.lastData.latitude;
				this.lon1 = Input.location.lastData.longitude;
				this.altitude = Input.location.lastData.altitude;
				this.accuracy = Input.location.lastData.horizontalAccuracy;
				this.timestamp1 = Input.location.lastData.timestamp;

				lat1 *= RAD;
				lon1 *= RAD;
				lat2 *= RAD;
				lon2 *= RAD;

				lat_c = (lat1 + lat2) / 2;
				dx = R_EARTH * (lon2 - lon1) * Mathf.Cos(lat_c);
				dy = R_EARTH * (lat2 - lat1);

				distance = Mathf.Sqrt(dx * dx + dy * dy);
				time = timestamp2 - timestamp1;

				speed = distance / (float)time;

				lat2 = lat1;
				lon2 = lon1;
				timestamp2 = timestamp1;

				yield return new WaitForSeconds(1);
			}
		}
		Input.location.Stop();
	}
	void OnGUI() {
		float x = Screen.width / 10;
		float y = 0;
		float w = Screen.width * 8 / 10;
		float h = Screen.height / 20;

		for (int i = 0; i < 7; i++) {
			y = Screen.height / 10 + h * i;
			string text = string.Empty;

			switch (i) {
				case 0://latitude
					text = string.Format("Latitude:{0}", this.lat1);
					break;
				case 1://longitude
					text = string.Format("Longitude:{0}", this.lon1);
					break;
				case 2://altitude
					text = string.Format("Altitude:{0}", this.altitude);
					break;
				case 3://accuracy
					text = string.Format("Accuracy:{0}", this.accuracy);
					break;
				case 4://Y
					text = string.Format("Speed:{0}", this.speed);
					break;
				case 5:
					text = string.Format("Timestamp:{0}", this.timestamp1);
					break;
				case 6:
					text = string.Format("time:{0}", this.time);
					break;
				default:
					throw new System.InvalidOperationException();
			}

			GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
		}
	}
}