using UnityEngine;
using System.Collections;

public class GPSUpdater : MonoBehaviour {
	public float IntervalSeconds = 1.0f;
	public LocationServiceStatus Status;
	public LocationInfo Location;

	public int R_EARTH = 6378137;
	public float lat1, lat2, lon1, lon2, RAD = Mathf.PI / 180, lat_c, dx, dy, distance, speed;
	public double timestamp1, timestamp2, time;

	IEnumerator Start() {
		while (true) {
			this.Status = Input.location.status;
			if (Input.location.isEnabledByUser) {
				switch (this.Status) {
					case LocationServiceStatus.Stopped:
						Input.location.Start();
						break;
					case LocationServiceStatus.Running:
						this.Location = Input.location.lastData;
						break;
					default:
						break;
				}
			} else {
				Debug.Log("location is disabled by user");
			}

			lat1 = Location.latitude;
			lon1 = Location.longitude;
			timestamp1 = Location.timestamp;

			lat1 *= RAD;
			lon1 *= RAD;
			lat2 *= RAD;
			lon2 *= RAD;

			lat_c = (lat1 + lat2) / 2;
			dx = R_EARTH * (lon2 - lon1) * Mathf.Cos(lat_c);
			dy = R_EARTH * (lat2 - lat1);

			distance = Mathf.Sqrt(dx * dx + dy * dy);
			time = timestamp2 - timestamp1;
			if (time == 0) {
				speed = 0;
			} else {
				speed = distance / (float)time;
			}
			lat2 = Location.latitude;
			lon2 = Location.longitude;
			timestamp2 = Location.timestamp;


			// 指定した秒数後に再度判定を走らせる
			yield return new WaitForSeconds(IntervalSeconds);
		}
	}
}