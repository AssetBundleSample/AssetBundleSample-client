using UnityEngine;
using System.Collections;

public class CachingLoadExample : MonoBehaviour
{
	public string bundleURL;
	public string assetName;
	public int version;

	void Start()
	{
		StartCoroutine(DownloadAndCache());
	}

	IEnumerator DownloadAndCache()
	{
		while(!Caching.ready)
			yield return null;

		using(WWW www = WWW.LoadFromCacheOrDownload(bundleURL, version)) {
			yield return www;

			if (www.error != null) {
				throw new UnityException("WWW download had an error" + www.error);
			}

			AssetBundle bundle = www.assetBundle;

			if (assetName == "") {
				Instantiate(bundle.mainAsset);
			} else {
				Instantiate(bundle.LoadAsset(assetName));
			}

			bundle.Unload(false);
		}
	}
}
