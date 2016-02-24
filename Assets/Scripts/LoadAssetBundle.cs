using UnityEngine;
using System.Collections;

public class LoadAssetBundle : MonoBehaviour
{
	IEnumerator Start()
	{
		var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/sprites");

		yield return new WaitWhile(() => resultAssetBundle.isDone == false);

		var assetbundle = resultAssetBundle.assetBundle;
		var resultObject = assetbundle.LoadAssetAsync<GameObject>("Test");

		yield return new WaitWhile(() => resultObject.isDone == false);

		GameObject.Instantiate(resultObject.asset);
		assetbundle.Unload(false);
	}
}
